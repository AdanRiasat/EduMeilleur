import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router, RouterModule } from '@angular/router';
import { SpinnerService } from '../../services/spinner.service';
import { Modal } from 'bootstrap';
import { AuthExtraOptionsComponent } from '../../components/auth-extra-options/auth-extra-options.component';
import { ModalService } from '../../services/modal.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule, AuthExtraOptionsComponent, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  errors: { [key: string]: string } = {};

  formGroup: FormGroup;

  constructor(
    public userService: UserService,
    public spinnerService: SpinnerService,
    public router: Router,
    private formBuilder: FormBuilder,
    public modalService: ModalService,
  ) {
    this.formGroup = formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  async login() {
    this.spinnerService.show();
    this.errors = {};

    let username = this.formGroup.get('username')?.value;
    let password = this.formGroup.get('password')?.value;

    try {
      await this.userService.login(username, password);

      if (this.userService.token() != null) {
        this.spinnerService.hide();
        this.router.navigate(['/profile']);
      }
    } catch (error: any) {
      console.log(error);

      if (error.status === 0 || error.status === 500) {
        this.modalService.openErrorModal(() => this.login());
        return;
      }
      this.errors['badRequest'] = error.error.message;
    }

    this.spinnerService.hide();
  }
}
