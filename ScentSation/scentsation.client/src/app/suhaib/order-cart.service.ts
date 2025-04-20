import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderCartService {
  private cartUrl = 'https://localhost:7199/api/cart';
  private orderUrl = 'https://localhost:7199/api/order';
  private paymentUrl = 'https://localhost:7199/api/payment';
  private baseUrl = 'https://localhost:7199/api';

  constructor(private http: HttpClient) { }

  // ====================
  // 🛒 CART OPERATIONS
  // ====================

  // Get cart items by user ID
  getCartByUser(userId: number) {
    return this.http.get(`${this.cartUrl}/user/${userId}`);
  }

  // Add new item to cart
  addToCart(data: any) {
    return this.http.post(this.cartUrl, data);
  }

  // Update item in cart
  updateCartItem(cartId: number, data: any) {
    return this.http.put(`${this.cartUrl}/${cartId}`, data);
  }

  // Delete single item from cart
  deleteCartItem(cartId: number) {
    return this.http.delete(`${this.cartUrl}/${cartId}`);
  }

  // Clear all cart items for a user
  clearCartByUser(userId: number) {
    return this.http.delete(`${this.baseUrl}/cart/clear/${userId}`);
  }


  // =====================
  // 📦 ORDER OPERATIONS
  // =====================

  // Get orders by user
  getOrdersByUser(userId: number) {
    return this.http.get(`${this.baseUrl}/order/user/${userId}`);
  }


  // Get all orders (admin)
  getAllOrders() {
    return this.http.get(this.orderUrl);
  }

  // Place a new order
  placeOrder(orderData: any) {
    return this.http.post(`${this.baseUrl}/order`, orderData);
  }

  // Update order status
  updateOrderStatus(orderId: number, status: string) {
    return this.http.put(`${this.orderUrl}/${orderId}/status`, { status });
  }

  // =======================
  // 💳 PAYMENT OPERATIONS
  // =======================

  // Add payment to order
  addPayment(paymentData: any) {
    return this.http.post(this.paymentUrl, paymentData);
  }

  // Get payments for specific order
  getPaymentsByOrder(orderId: number) {
    return this.http.get(`${this.paymentUrl}/order/${orderId}`);
  }
}
