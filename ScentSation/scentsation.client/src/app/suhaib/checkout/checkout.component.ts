import { Component, OnInit } from '@angular/core';
import { OrderCartService } from '../order-cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {
  userId: number = 1;
  cartItems: any[] = [];
  totalPrice: number = 0;
  selectedPaymentMethod: string = 'cod';

  shippingDetails = {
    fullName: '',
    phone: '',
    address: ''
  };

  proofImageBase64: string = '';

  constructor(
    private cartService: OrderCartService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const sessionId = sessionStorage.getItem('userId');
    if (sessionId) this.userId = parseInt(sessionId);

    this.cartService.getCartByUser(this.userId).subscribe(items => {
      this.cartItems = Array.isArray(items) ? items : [];
      this.calculateTotal();
    });
  }

  calculateTotal() {
    this.totalPrice = this.cartItems.reduce((sum, item) => {
      return sum + item.quantity * (item.customPerfume?.price || 0);
    }, 0);
  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.proofImageBase64 = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  submitOrder() {
    if (!this.shippingDetails.fullName || !this.shippingDetails.phone || !this.shippingDetails.address) {
      alert('Please fill in all shipping details.');
      return;
    }

    if (this.selectedPaymentMethod === 'orange' && !this.proofImageBase64) {
      alert('Please upload Orange Money payment proof.');
      return;
    }

    const orderData = {
      userId: this.userId,
      deliveryAddress: this.shippingDetails.address,
      totalAmount: this.totalPrice,
      paymentMethod: this.selectedPaymentMethod,
      paymentProofImage: this.proofImageBase64,
      orderItems: this.cartItems.map(item => ({
        customPerfumeId: item.customPerfume.customPerfumeId,
        quantity: item.quantity,
        unitPrice: item.customPerfume.price
      }))
    };

    this.cartService.placeOrder(orderData).subscribe(() => {
      this.cartService.clearCartByUser(this.userId).subscribe(() => {
        alert('✅ Order confirmed! Redirecting...');
        this.router.navigate(['/order-history']);
      });
    }, error => {
      alert('❌ Failed to place order. Please try again.');
      console.error(error);
    });
  }

  goToCheckout() {
    this.router.navigate(['/checkout']);
  }
}
