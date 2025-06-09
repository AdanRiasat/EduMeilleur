import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

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

  constructor(public userService: UserService, public route: Router) {}

  async register(){
    if (this.password == this.confirmPassword && this.password != ""){
      await this.userService.register(this.username, this.email, this.password, this.school, this.schoolYear)
    } else {
      alert("Passwords do not match")
    }

    if (localStorage.getItem("token") != null){
      this.route.navigate(['/profile'])
    }
  }

}
