import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PerfumeBuilderComponent } from './ali/perfume-builder/perfume-builder.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PreviousPerfumesComponent } from './ali/previous-perfumes/previous-perfumes.component';
import { CartComponent } from './suhaib/cart/cart.component';
import { CheckoutComponent } from './suhaib/checkout/checkout.component';
import { OrderHistoryComponent } from './ali/order-history/order-history.component';
import { FragranceAiComponent } from './ali/fragrance-ai/fragrance-ai.component';
import { StaffAppointmetsComponent } from './firas/staffappointmets/staffappointmets.component';
import { MyApointmentsComponent } from './firas/myapointments/myapointments.component';
import { BookedSessionComponent } from './firas/bookedsession/bookedsession.component';
import { AvailableSessionsComponent } from './firas/availablesessions/availablesessions.component';
import { RegisterComponent } from './hazem/register/register.component';
import { LoginComponent } from './hazem/login/login.component';
import { ProfileComponent } from './hazem/profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    PerfumeBuilderComponent,
    PreviousPerfumesComponent,
    CartComponent,
    CheckoutComponent,
    OrderHistoryComponent,
    FragranceAiComponent,
    AvailableSessionsComponent,
    BookedSessionComponent,
    MyApointmentsComponent,
    StaffAppointmetsComponent,
    RegisterComponent,
    LoginComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, FormsModule, BrowserAnimationsModule, ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
