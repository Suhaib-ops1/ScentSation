import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserlService } from '../userl.service';
import { ImageService } from '../image.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: any;
  editableUser: any = {};
  userId: string = '';
  selectedFile: File | null = null;
  uploadedImageUrl: string = '';
  loading: boolean = false;

  constructor(
    private userService: UserlService,
    private route: ActivatedRoute,
    private imageService: ImageService
  ) { }
  ngOnInit() {
    const storedId = sessionStorage.getItem('userId');
    this.userId = storedId && storedId !== 'null' ? storedId : '1'; // ✅ لو null أو فاضي، خذ 1

    this.userService.getUserById(this.userId).subscribe({
      next: (user) => {
        console.log("✅ Loaded User:", user);
        this.user = user;
        this.editableUser = { ...user };
      },
      error: (err) => {
        console.error("❌ Failed to load user", err);
        // ✅ fallback تلقائي إذا فشل الاتصال
        if (this.userId !== '1') {
          this.userService.getUserById('1').subscribe(defaultUser => {
            this.user = defaultUser;
            this.editableUser = { ...defaultUser };
          });
        }
      }
    });
  }
  onImageSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.loading = true;

      this.imageService.uploadImage(file).subscribe(
        (response: { data: { url: string } }) => {
          this.uploadedImageUrl = response.data.url;
          this.editableUser.profileImageUrl = this.uploadedImageUrl; // ✅ هون الزامي
          this.loading = false;
          console.log("✅ صورة مرفوعة:", this.uploadedImageUrl);
        },
        (error: any) => {
          console.error('Image Upload Error:', error);
          this.loading = false;
        }
      );
    }
  }

  uploadImage(file: File | null): void {
    if (!file) return;
    this.loading = true;

    this.imageService.uploadImage(file).subscribe(
      (response: { data: { url: string } }) => {
        this.uploadedImageUrl = response.data.url;
        this.editableUser.ProfileImageUrl = this.uploadedImageUrl;
        this.loading = false;
      },
      (error: any) => {
        console.error('Image Upload Error:', error);
        this.loading = false;
      }
    );
  }
  updateProfile() {
    const dto = {
      fullName: this.editableUser.fullName,
      email: this.editableUser.email,
      phoneNumber: this.editableUser.phoneNumber,
      profileImageUrl: this.editableUser.profileImageUrl
    };

    this.userService.updateUser(this.userId, dto).subscribe(() => {
      this.user = { ...this.editableUser };
      alert('✅ تم تحديث البيانات بنجاح!');
    }, err => {
      console.error('❌ Update Error:', err);
    });
  }


}
