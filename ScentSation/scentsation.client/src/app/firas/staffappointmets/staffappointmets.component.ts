import { Component } from '@angular/core';
import { MyService } from '../MyServices/my.service';


@Component({
  selector: 'app-staffappointmets',
  templateUrl: './staffappointmets.component.html',
  styleUrl: './staffappointmets.component.css'
})
export class StaffAppointmetsComponent {

  constructor(private _ser: MyService) { }
  ngOnInit() {
    this.getAllStaffAppointments(1);
  }
  StaffAppointments: any;

  getAllStaffAppointments(id: any) {
    this._ser.getAllStaffAppointments(id).subscribe((data: any) => {
      alert("جاب الداتا")
      console.log('Data from API:', data);
      this.StaffAppointments = data;
    });
  }
}
