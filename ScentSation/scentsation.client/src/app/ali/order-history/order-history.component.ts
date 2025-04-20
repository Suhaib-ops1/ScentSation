import { Component, OnInit } from '@angular/core';
import { OrderCartService } from '../../suhaib/order-cart.service';

interface Order {
  orderId: number;
  orderStatus: string;
  totalAmount: number;
  orderDate: string;
  deliveryAddress: string;
  orderItems: {
    orderItemId: number;
    quantity: number;
    unitPrice: number;
    customPerfume: {
      name: string;
      bottle: {
        name: string;
        imageUrl: string;
      };
      customPerfumeNotes: {
        note: {
          name: string;
          type: string;
        };
      }[];
    };
  }[];
  payments: {
    paymentMethod: string;
    paymentStatus: string;
    transactionDate: string;
  }[];
}

@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css']
})
export class OrderHistoryComponent implements OnInit {
  userId: number = 1;
  orders: Order[] = [];

  constructor(private orderService: OrderCartService) { }

  ngOnInit(): void {
    const sessionId = sessionStorage.getItem('userId');
    if (sessionId) this.userId = parseInt(sessionId);
    this.loadOrders();
  }

  loadOrders() {
    this.orderService.getOrdersByUser(this.userId).subscribe(data => {
      this.orders = Array.isArray(data) ? data : [];
    });
  }
}
