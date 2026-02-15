import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router, RouterModule } from '@angular/router';
import { SpinnerService } from '../../services/spinner.service';
import { Modal } from 'bootstrap';
import { ModalComponent } from '../../components/modal/modal.component';
import { AuthExtraOptionsComponent } from '../../components/auth-extra-options/auth-extra-options.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule, ModalComponent, AuthExtraOptionsComponent, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  errors: { [key: string]: string} = {}

  formGroup: FormGroup

  constructor(public userService: UserService, public router: Router, public spinner: SpinnerService, private formBuilder: FormBuilder) {
    this.formGroup = formBuilder.group(
      {
        username: ['', [Validators.required]],
        password: ['', [Validators.required]]
      }
    )
  }

  async login() {
    this.spinner.show()
    this.errors = {}

    let username = this.formGroup.get('username')?.value
    let password = this.formGroup.get('password')?.value

    try {
      await this.userService.login(username, password)

      if (this.userService.token() != null){
        this.spinner.hide()
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
