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
  userIsConnected: boolean = false
  profile: Profile | null = null
  username: string | undefined = ""
  bio: string | undefined = ""

  constructor(public userService: UserService) {}
  
  ngOnInit() {
     this.userIsConnected = localStorage.getItem("token") != null;
     
     let profileStringData = localStorage.getItem("profile")
     if (profileStringData != null){
      this.profile = JSON.parse(profileStringData)
      this.username = this.profile?.username
      this.bio = this.profile?.bio
     }
 
  }


  
}
