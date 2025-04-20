import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderCartService } from '../order-cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  userId: number = 1;
  cartItems: any[] = [];
  selectedItem: any = null;
  totalPrice: number = 0;

  constructor(private cartService: OrderCartService, private router: Router) { }

  ngOnInit(): void {
    const sessionId = sessionStorage.getItem('userId');
    if (sessionId) this.userId = parseInt(sessionId);
    this.loadCart();
  }

  loadCart() {
    this.cartService.getCartByUser(this.userId).subscribe(data => {
      this.cartItems = Array.isArray(data) ? data : [];
      this.calculateTotal();
    });
  }

  calculateTotal() {
    this.totalPrice = this.cartItems.reduce(
      (sum, item) => sum + item.quantity * (item.customPerfume?.price || 0),
      0
    );
  }

  updateQuantity(item: any, change: number) {
    const newQty = item.quantity + change;
    if (newQty < 1) return;

    // تحديث مباشر للعرض
    item.quantity = newQty;
    this.calculateTotal();

    // إرسال للسيرفر
    this.cartService.updateCartItem(item.cartId, item).subscribe();
  }

  removeItem(cartId: number) {
    this.cartService.deleteCartItem(cartId).subscribe(() => {
      this.loadCart();
    });
  }

  openPopup(item: any) {
    this.selectedItem = item;
  }

  closePopup() {
    this.selectedItem = null;
  }

  goToCheckout() {
    this.router.navigate(['/checkout']);
  }

 

}
