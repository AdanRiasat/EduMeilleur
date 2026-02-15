import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SpinnerService } from '../../services/spinner.service';
import { ModalComponent } from '../../components/modal/modal.component';
import { Modal } from 'bootstrap';
import { SignUpMainComponent } from '../../components/sign-up-main/sign-up-main.component';
import { SignUpExtraComponent } from '../../components/sign-up-extra/sign-up-extra.component';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [FormsModule, CommonModule, ModalComponent, SignUpMainComponent, SignUpExtraComponent],
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

  isFirstPage = signal(true)

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
      console.log(error);
      if (error.status === 0){
        this.openErrorModal()
        this.spinner.hide()
        return
      }
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
    } finally{
      this.spinner.hide()
  }
 
  }

  handleMainData(data: any) {
    this.email = data.email;
    this.password = data.password;
    this.confirmPassword = data.confirmPassword;

    this.isFirstPage.set(false)
  }

  handleExtraData(data: any){
    this.username = data.username;
    this.school = data.school;
    this.schoolYear = data.schoolYear;

    this.register()
  }

  openErrorModal(){
        let modalElement = document.getElementById('error500Modal')
        if (modalElement){
          let modal = new Modal(modalElement)
          modal.show()
        }
    }

}
