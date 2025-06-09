import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,CommonModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  username: string = ""
  password: string = ""

  constructor(public userService: UserService, public router: Router) {}

  async login() {
    await this.userService.login(this.username, this.password)
    if (this.userService.token() != null){
      console.log(this.userService.token());
      
      this.router.navigate(['/profile']);
    }
  }
}
