import { CommonModule } from '@angular/common';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
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

  profile: Profile | null = null

  formData: FormData = new FormData

  @ViewChild('profileImage', {static: false}) profileImage ?: ElementRef<HTMLImageElement>
  @ViewChild('fileInput', {static: false}) fileInput ?: ElementRef<HTMLInputElement>

  constructor(public route: ActivatedRoute, public userService: UserService) {}
  
  ngOnInit() {
    let usernameData: string | null = this.route.snapshot.paramMap.get("username")
    if (usernameData != null){
      console.log(usernameData);
      
      this.username = usernameData
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
    }
  }

  triggerFileInput(): void {
    if (this.fileInput != undefined)
      this.fileInput.nativeElement.click();
  }

  onFileSelected(event: Event){
   let input = event.target as HTMLInputElement
   if (input.files && input.files.length > 0){
    let file = input.files[0]
    console.log(file);
    

    this.formData.append("pfp", file)
   }
  }

  async saveChanges(){
    this.formData.append("username", this.username)
    this.formData.append("email", this.email)
    this.formData.append("bio", this.bio)
    this.formData.append("school", this.school)
    this.formData.append("schoolYear", this.schoolYear)

    await this.userService.editProfile(this.formData)
  }
}
