import { AfterViewInit, Component, OnInit } from '@angular/core';
import Chart from 'chart.js/auto'; 


@Component({
  selector: 'app-dashboard-content',
  templateUrl: './dashboard-content.component.html',
  styleUrl: './dashboard-content.component.css'
})
export class DashboardContentComponent implements OnInit, AfterViewInit {
  dashboardStats = [
    { label: 'Users', count: 120, trend: 'up' },
    { label: 'Orders', count: 45, trend: 'down' },
    { label: 'Revenue', count: '$8k', trend: 'up' },
    { label: 'Tickets', count: 14, trend: 'down' }
  ];

  recentActivity = [
    'Admin logged in at 10:45 AM',
    'Order #1024 marked as shipped',
    'New user registered',
    'Settings updated by Admin'
  ];

  constructor() { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    this.renderOrdersChart();
  }

  renderOrdersChart(): void {
    new Chart('ordersChart', {
      type: 'line',
      data: {
        labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
        datasets: [{
          label: 'Orders',
          data: [12, 19, 14, 20, 18, 10, 16],
          backgroundColor: 'rgba(30, 136, 229, 0.2)',
          borderColor: '#1da1f2',
          borderWidth: 2,
          fill: true,
          tension: 0.4,
        }]
      },
      options: {
        responsive: true,
        scales: {
          y: { beginAtZero: true }
        },
        plugins: {
          legend: { display: false }
        }
      }
    });
  }
}
