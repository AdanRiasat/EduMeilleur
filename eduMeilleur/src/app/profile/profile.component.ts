import { Component, OnInit } from '@angular/core';
import { Profile } from '../models/profile';
import { UserService } from '../services/user.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SpinnerService } from '../services/spinner.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit{
  profile: Profile | null = null
  username: string | undefined = ""
  bio: string | undefined = ""
  email: string | undefined = ""
  school: string | undefined | null = ""
  schoolYear: number | undefined | null

  timestamp: number = Date.now();

  constructor(public userService: UserService, public spinner: SpinnerService) {}
  
  ngOnInit() {
    this.spinner.hide()
     let profileStringData = localStorage.getItem("profile")
     if (profileStringData != null){
      this.profile = JSON.parse(profileStringData)
      this.username = this.profile?.username
      this.bio = this.profile?.bio
      this.email = this.profile?.email
      this.school = this.profile?.school
      this.schoolYear = this.profile?.schoolYear
     }
     
  }
}
