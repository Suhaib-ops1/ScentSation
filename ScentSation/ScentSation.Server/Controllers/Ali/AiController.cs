using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using ScentSation.Server.Models;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.Controllers.Ali.DTOs;

[Route("api/[controller]")]
[ApiController]
public class AiController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly MyDbContext _context;

    public AiController(IHttpClientFactory httpClientFactory, MyDbContext context)
    {
        _httpClientFactory = httpClientFactory;
        _context = context;
    }

    [HttpPost("recommend-perfume")]
    public async Task<IActionResult> RecommendPerfume([FromBody] List<string> userAnswers)
    {
        var perfumes = await _context.CustomPerfumes
            .Include(p => p.CustomPerfumeNotes).ThenInclude(n => n.Note)
            .Include(p => p.Bottle)
            .ToListAsync();

        var prompt = $@"
أنت نظام توصية عطور. هذه قائمة العطور المتاحة:
{string.Join("\n", perfumes.Select(p => $"- {p.Name} يحتوي على: {string.Join(", ", p.CustomPerfumeNotes.Select(n => n.Note.Name))}"))}

إجابات المستخدم:
{string.Join("\n", userAnswers)}

اختر العطر الأنسب بناءً على ذلك.";

        var request = new
        {
            contents = new[] { new { parts = new[] { new { text = prompt } } } }
        };

        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "AIzaSyDhldS_zrE-Hrpr2D4gestolelOiTdFKzA");

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("?key=AIzaSyDhldS_zrE-Hrpr2D4gestolelOiTdFKzA", content);

        if (!response.IsSuccessStatusCode)
            return BadRequest("AI request failed");

        var json = await response.Content.ReadAsStringAsync();
        var resultText = JsonDocument.Parse(json)
            .RootElement.GetProperty("candidates")[0]
            .GetProperty("content").GetProperty("parts")[0]
            .GetProperty("text").GetString();

        var matched = perfumes.FirstOrDefault(p =>
            resultText != null && resultText.Contains(p.Name, StringComparison.OrdinalIgnoreCase));

        if (matched == null)
            return NotFound("لم يتم العثور على عطر مناسب.");

        return Ok(new
        {
            matched.Name,
            matched.Price,
            matched.Bottle?.ImageUrl,
            Notes = matched.CustomPerfumeNotes.Select(n => n.Note.Name).ToList()
        });
    }

    [HttpPost("save-ai-perfume")]
    public async Task<IActionResult> SaveAiPerfume([FromBody] CustomPerfumeDTO dto)
    {
        // إنشاء العطر
        var perfume = new CustomPerfume
        {
            UserId = dto.UserId,
            Name = dto.Name,
            BottleId = dto.BottleId,
            Price = dto.Price,
            CustomPerfumeNotes = new List<CustomPerfumeNote>(),
            CreatedAt = DateTime.Now
        };

        foreach (var noteId in dto.CustomPerfumeNoteIds)
        {
            perfume.CustomPerfumeNotes.Add(new CustomPerfumeNote { NoteId = noteId });
        }

        _context.CustomPerfumes.Add(perfume);
        await _context.SaveChangesAsync();

        // إضافة للسلة
        var cart = new Cart
        {
            UserId = dto.UserId,
            CustomPerfumeId = perfume.CustomPerfumeId,
            Quantity = 1,
            CreatedAt = DateTime.Now
        };

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            perfume.CustomPerfumeId,
            perfume.Name,
            perfume.Price,
            perfume.BottleId
        });
    }
}
