import { Component } from '@angular/core';
import { MyService } from '../MyServices/my.service';

@Component({
  selector: 'app-availablesessions',
  templateUrl: './availablesessions.component.html',
  styleUrl: './availablesessions.component.css'
})
export class AvailableSessionsComponent {


  constructor(private _ser: MyService) { }


  ngOnInit() {
    this.getAllAvailableSessions();
  }
  sessions: any;

  getAllAvailableSessions() {

    this._ser.getAllAvailableSessions().subscribe((data: any) => {
      alert("جاب الداتا")
      console.log('Data from API:', data);
      this.sessions = data;
    });
  }

  reserveBtn(id: any, session: any) {

    this._ser.ReserveSession(id, session).subscribe(() => {
      alert("تم الحجز بنجاح")
      this.getAllAvailableSessions();
    });
  }
}
