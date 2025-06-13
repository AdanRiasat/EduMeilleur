import { CommonModule } from '@angular/common';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { Profile } from '../models/profile';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit {
  school: string = ""
  schoolYear: string = ""
  username: string = ""
  bio: string = ""
  email: string = ""

  imageSrc: string = ""
  timestamp: number = Date.now();


  file: File | null = null

  profile: Profile | null = null

  @ViewChild('profileImage', {static: false}) profileImage ?: ElementRef<HTMLImageElement>
  @ViewChild('fileInput', {static: false}) fileInput ?: ElementRef<HTMLInputElement>

  constructor(public route: Router, public userService: UserService) {}
  
  ngOnInit() {
    let profileStringData = localStorage.getItem("profile")  
    if (profileStringData != null){
      this.profile = JSON.parse(profileStringData)
      console.log(this.profile);
      this.username = this.profile!!.username
      this.bio = this.profile!!.bio
      this.email = this.profile!!.email
      this.school = this.profile!!.school
      this.schoolYear = this.profile!!.schoolYear
    }

    this.imageSrc = "https://localhost:7027/api/Users/GetProfilePicture/" + this.username
  }
  

  triggerFileInput(): void {
    if (this.fileInput != undefined)
      this.fileInput.nativeElement.click();
  }

  onFileSelected(event: Event){
   let input = event.target as HTMLInputElement
   if (input.files && input.files.length > 0){
    this.file = input.files[0]
    
      let reader = new FileReader();
      reader.onload = () => {
        if (this.profileImage != undefined){
          this.profileImage.nativeElement.src = reader.result as string;
        }
      };
      reader.readAsDataURL(this.file);
      
   }
  }

  async saveChanges(){
    let formData = new FormData()

    if (this.file != null){
      formData.append("pfp", this.file)
    }
    formData.append("email", this.email)
    formData.append("bio", this.bio)
    formData.append("school", this.school)
    formData.append("schoolYear", this.schoolYear)

    await this.userService.editProfile(formData)
    this.route.navigate(['/profile'])
  }
}
