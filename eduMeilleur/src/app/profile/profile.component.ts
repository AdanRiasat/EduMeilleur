import { Component, OnInit } from '@angular/core';
import { Profile } from '../models/profile';
import { UserService } from '../services/user.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

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
  school: string | undefined = ""
  schoolYear: number | undefined

  timestamp: number = Date.now();

  constructor(public userService: UserService) {}
  
  ngOnInit() {
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
