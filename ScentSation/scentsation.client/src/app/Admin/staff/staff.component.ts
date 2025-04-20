import { Component } from '@angular/core';
import { StaffDTO, StaffService } from '../service/staff.service';

@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrl: './staff.component.css'
})
export class StaffComponent {
  staffList: any[] = [];
  newStaff: StaffDTO = {
    fullName: '',
    email: '',
    phoneNumber: '',
    profileImageUrl: '',
    password: ''
  };

  constructor(private staffService: StaffService) { }

  ngOnInit(): void {
    this.loadStaff();
  }

  loadStaff(): void {
    this.staffService.getAllStaff().subscribe(data => {

      console.log('Staff loaded:', data);
      this.staffList = data;
    });
  }

  addStaff(): void {
    this.staffService.addStaff(this.newStaff).subscribe(success => {
      alert('Staff added successfully!');
      this.loadStaff();
      this.newStaff = {
        fullName: '',
        email: '',
        phoneNumber: '',
        profileImageUrl: '',
        password: ''

      }
    })
  }


  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.newStaff.profileImageUrl = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }
}
