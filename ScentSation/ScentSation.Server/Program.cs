using CoreApiAngular.Server.DataService;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.Admin.DataService;
using ScentSation.Server.Admin.IDataService;
using ScentSation.Server.firasServices.IDataService;
using ScentSation.Server.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionString")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddScoped<IMyDataService, MyDataService>()
                .AddScoped<IDataServicee, DataServicee>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache(); // ✅ ضروري لتخزين الجلسات بالذاكرة

builder.Services.AddSession(); // ✅ لتفعيل الجلسات

var app = builder.Build();

app.UseCors("AllowAll");
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseSession(); // ✅ لتفعيل الجلسات فعليًا

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
