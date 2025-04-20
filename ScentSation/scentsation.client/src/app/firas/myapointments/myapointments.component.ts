import { Component } from '@angular/core';
import { MyService } from '../MyServices/my.service';

@Component({
  selector: 'app-myapointments',
  templateUrl: './myapointments.component.html',
  styleUrl: './myapointments.component.css'
})
export class MyApointmentsComponent {

  constructor(private _ser: MyService) { }

  ngOnInit() {
    this.AppointmentsById(1);
  }

  myAppointments: any

  AppointmentsById(id: any) {
    this._ser.AppointmentsById(id).subscribe((data: any) => {
      this.myAppointments = data;
    })
  }
}
