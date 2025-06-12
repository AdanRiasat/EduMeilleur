import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

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

  constructor(public route: ActivatedRoute) {}
  
  ngOnInit() {
    let usernameData: string | null = this.route.snapshot.paramMap.get("username")
    if (usernameData != null){
      console.log(usernameData);
      
      this.username = usernameData
    }
  }

}
