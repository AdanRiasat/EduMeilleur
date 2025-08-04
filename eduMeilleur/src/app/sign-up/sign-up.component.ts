import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { parse } from 'marked';
import { CommonModule } from '@angular/common';
import { SpinnerService } from '../services/spinner.service';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [FormsModule, CommonModule],
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

  errors: { [key: string]: string} = {}

  constructor(public userService: UserService, public route: Router, public spinner: SpinnerService) {}

  async register(){
    this.spinner.show()
    this.errors = {};
    let isInputEmpty: boolean = false

    if (this.password != this.confirmPassword || this.password == "") {
    this.errors["confirmPassword"] = "Passwords do not match.";
    isInputEmpty = true
    }

    if (this.email == ""){
      this.errors["email"] = "Email field is empty"
      isInputEmpty = true
    }

    if (this.username == ""){
      this.errors["username"] = "Username field is empty"
      isInputEmpty = true
    }

    if (isInputEmpty){
      this.spinner.hide()
      return
    }

    try {
      await this.userService.register(this.username, this.email, this.password, parseFloat(this.school), parseFloat(this.schoolYear))

      if (localStorage.getItem("token") != null){
        this.route.navigate(['/profile'])
      }

    } catch (error: any) {
    if (error.error) {
      for (let e of error.error) {
        switch (e.code) {
          case "DuplicateUserName":
          case "InvalidUserName":
            this.errors["username"] = e.description;
            this.username = ""
            break;
          case "InvalidEmail":
          case "DuplicateEmail":
            this.errors["email"] = e.description;
            this.email = ""
            break;
          case "PasswordTooShort":
          case "PasswordRequiresDigit":
          case "PasswordRequiresNonAlphanumeric":
            this.errors["password"] = e.description;
            this.password = ""
            this.confirmPassword = ""
            break;
          default:
            this.errors["general"] = e.description;
            this.username = ""
            this.email = ""
            this.password = ""
            this.confirmPassword = ""
            break;
        }
      }
    } else {
      this.errors["general"] = "An unexpected error occurred.";
    }

    this.spinner.hide()
  }
 
  }

}
