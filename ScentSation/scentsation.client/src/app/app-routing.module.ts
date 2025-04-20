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
import { DashboardComponent } from './Admin/dashboard/dashboard.component';
import { DashboardContentComponent } from './Admin/dashboard-content/dashboard-content.component';
import { AdminFeedbackComponent } from './Admin/admin-feedback/admin-feedback.component';
import { ShowusersComponent } from './Admin/showusers/showusers.component';
import { BookingComponent } from './Admin/booking/booking.component';
import { StaffComponent } from './Admin/staff/staff.component';

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
 
  //----------------Admin-----------------------//

  {
    path: 'dashboard',
    component: DashboardComponent,
    children: [
      { path: '', redirectTo: 'dashboardcontent', pathMatch: 'full' },
      { path: 'dashboardcontent', component: DashboardContentComponent },
      { path: 'feedback', component: AdminFeedbackComponent },
      { path: 'users', component: ShowusersComponent },
      { path: 'bookings', component: BookingComponent },
      { path: 'Staff', component: StaffComponent },



    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
