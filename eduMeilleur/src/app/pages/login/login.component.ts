import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router, RouterModule } from '@angular/router';
import { SpinnerService } from '../../services/spinner.service';
import { Modal } from 'bootstrap';
import { ModalComponent } from '../../components/modal/modal.component';
import { AuthExtraOptionsComponent } from '../../components/auth-extra-options/auth-extra-options.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule, ModalComponent, AuthExtraOptionsComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  username: string = ""
  password: string = ""

  errors: { [key: string]: string} = {}

  constructor(public userService: UserService, public router: Router, public spinner: SpinnerService) {}

  async login() {
    this.spinner.show()
    this.errors = {}
    let isInputEmpty: boolean = false

    if (this.username == ""){
      this.errors["username"] = "Username or email is required"
      isInputEmpty = true
    }

    if (this.password == ""){
      this.errors["password"] = "Password is required"
    }

    if (isInputEmpty) {
      this.spinner.hide()
      return
    }

    try {
      await this.userService.login(this.username, this.password)

      if (this.userService.token() != null){
        console.log(this.userService.token());
        this.router.navigate(['/profile']);
      }
    } catch (error: any){
      console.log(error);
      
      if (error.status === 0){
        this.openErrorModal()
        return
      }
      this.errors["badRequest"] = error.error.message
    }

    this.username = ""
    this.password = ""
    this.spinner.hide()
  }

  openErrorModal(){
      let modalElement = document.getElementById('error500Modal')
      if (modalElement){
        let modal = new Modal(modalElement)
        modal.show()
        this.spinner.hide()
      }
  }
}
