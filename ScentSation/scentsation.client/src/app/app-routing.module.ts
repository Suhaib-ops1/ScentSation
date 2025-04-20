import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PerfumeBuilderComponent } from './ali/perfume-builder/perfume-builder.component';
import { PreviousPerfumesComponent } from './ali/previous-perfumes/previous-perfumes.component';
import { CheckoutComponent } from './suhaib/checkout/checkout.component';
import { OrderHistoryComponent } from './ali/order-history/order-history.component';
import { FragranceAiComponent } from './ali/fragrance-ai/fragrance-ai.component';
import { BookedSessionComponent } from './firas/bookedsession/bookedsession.component';
import { StaffAppointmetsComponent } from './firas/staffappointmets/staffappointmets.component';
import { MyApointmentsComponent } from './firas/myapointments/myapointments.component';
import { AvailableSessionsComponent } from './firas/availablesessions/availablesessions.component';
import { CartComponent } from './suhaib/cart/cart.component';

const routes: Routes = [
  { path: "perfume-builder", component: PerfumeBuilderComponent },
  { path: "previous-perfumes", component: PreviousPerfumesComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'order-history', component: OrderHistoryComponent },
  { path: 'fragrance-ai', component: FragranceAiComponent },
  { path: 'availablesessions', component: AvailableSessionsComponent },
  { path: 'bookedsession', component: BookedSessionComponent },
  { path: 'StaffAppointmets', component: StaffAppointmetsComponent },
  { path: 'MyApointments', component: MyApointmentsComponent },
  { path: `cart`, component: CartComponent },
 



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
