import { Component, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SpinnerService } from '../../services/spinner.service';
import { ModalComponent } from '../../components/modal/modal.component';
import { Modal } from 'bootstrap';
import { SignUpMainComponent } from '../../components/sign-up-main/sign-up-main.component';
import { SignUpExtraComponent } from '../../components/sign-up-extra/sign-up-extra.component';
import { passwordsMatch } from '../../validators/passwords-match';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [FormsModule, CommonModule, ModalComponent, SignUpMainComponent, SignUpExtraComponent, ReactiveFormsModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {
  errors: { [key: string]: string} = {}

  isFirstPage = signal(true)

  formGroupMain: FormGroup
  formGroupExtra: FormGroup

  constructor(public userService: UserService, public route: Router, public spinner: SpinnerService, public formBuilder: FormBuilder) {
    this.formGroupMain = formBuilder.group(
          {
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(5), Validators.pattern('(?=.*\\d).*')]],
            confirmPassword: ['', [Validators.required]],
          },
          { validators: passwordsMatch()}
        )

    this.formGroupExtra = formBuilder.group(
      {
        username: ['', [Validators.required]],
        school: ['', []],
        schoolYear: ['', []]
      }
    )
  }

  async register(){
    this.spinner.show()
    this.errors = {};
    
    try {
      let email = this.formGroupMain.get('email')?.value
      let password = this.formGroupMain.get('password')?.value
      let username = this.formGroupExtra.get('username')?.value
      let school = this.formGroupExtra.get('school')?.value
      let schoolYear = this.formGroupExtra.get('schoolYear')?.value

      await this.userService.register(username, email, password, parseFloat(school), parseFloat(schoolYear))

      if (localStorage.getItem("token") != null){
        this.spinner.hide()
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
      }
    } else {
      this.errors["general"] = "An unexpected error occurred.";
      
    }
    } finally{
      this.spinner.hide()
  }
 
  }

  openErrorModal(){
        let modalElement = document.getElementById('error500Modal')
        if (modalElement){
          let modal = new Modal(modalElement)
          modal.show()
        }
    }

}
