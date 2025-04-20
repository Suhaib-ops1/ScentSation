import { Component } from '@angular/core';
import { MyService } from '../MyServices/my.service';

@Component({
  selector: 'app-bookedsession',
  templateUrl: './bookedsession.component.html',
  styleUrl: './bookedsession.component.css'
})
export class BookedSessionComponent {

  ngOnInit() {
    this.getAllBookedSessions();
  }
  constructor(private _ser: MyService) { }

  Bookeds: any;
  getAllBookedSessions() {
    this._ser.getAllBookedSessions().subscribe((data: any) => {
      alert("حاب داتا ")
      this.Bookeds = data;
      alert(this.Bookeds[0].Id)
      console.log(this.Bookeds)

    });
  }
}
