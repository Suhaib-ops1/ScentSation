import { Component, OnInit } from '@angular/core';
import { PerfumeBuilderService } from '../perfume-builder.service';
import { OrderCartService } from '../../suhaib/order-cart.service';

@Component({
  selector: 'app-previous-perfumes',
  templateUrl: './previous-perfumes.component.html',
  styleUrls: ['./previous-perfumes.component.css']
})
export class PreviousPerfumesComponent implements OnInit {

  userId: number = 1;
  previousPerfumes: any[] = [];
  message: string = '';

  constructor(
    private perfumeService: PerfumeBuilderService,
    private cartService: OrderCartService
  ) { }

  ngOnInit(): void {
    const sessionId = sessionStorage.getItem('userId');
    this.userId = sessionId ? parseInt(sessionId) : 1;

    this.perfumeService.getPerfumesByUser(this.userId).subscribe(data => {
      this.previousPerfumes = data;
    });
  }


  addToCart(perfume: any) {
    const item = {
      userId: this.userId,
      customPerfumeId: perfume.customPerfumeId,
      quantity: 1
    };

    this.cartService.addToCart(item).subscribe(() => {
      this.message = '✅ Perfume added again!';
    });
  }
}
