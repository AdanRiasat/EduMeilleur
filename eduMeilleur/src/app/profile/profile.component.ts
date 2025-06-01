import { Component, OnInit } from '@angular/core';
import { Profile } from '../models/profile';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit{
  userIsConnected: boolean = false
  profile: Profile | null = null
  username: string | undefined = ""
  bio: string | undefined = ""
  
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
