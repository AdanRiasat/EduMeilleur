import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {
  username: string = ""
  password: string = ""
  confirmPassword: string = ""
  email: string = ""
  school: string = ""
  schoolYear: string = ""

  constructor(public userService: UserService) {}

  register(){
    if (this.password == this.confirmPassword){
      this.userService.register(this.username, this.email, this.password, this.school, this.schoolYear)
    } else {
      alert("Passwords do not match")
    }
  }

}
