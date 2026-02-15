import { CommonModule } from '@angular/common';
import { Component, computed, input, OnInit, output, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthExtraOptionsComponent } from '../auth-extra-options/auth-extra-options.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-sign-up-main',
  standalone: true,
  imports: [FormsModule, CommonModule, AuthExtraOptionsComponent, RouterLink, ReactiveFormsModule],
  templateUrl: './sign-up-main.component.html',
  styleUrl: './sign-up-main.component.css'
})
export class SignUpMainComponent {
  next = output<void>()
  formGroup = input<FormGroup>()

  passwordField = computed(() => this.formGroup()!.get('password')!)
  confirmPasswordField = computed(() => this.formGroup()!.get('confirmPassword')!)

  passwordFieldFocused = signal(false)

  // TODO Refactor the error functions

  passwordRequiredError(): boolean {
    return this.passwordField().hasError('required') 
    && this.passwordField().touched 
  }

  passwordInvalidError(): boolean {
    return this.passwordLengthError()
    || this.passwordDigitError()
    && !this.passwordRequiredError()
  }

  passwordLengthError(): boolean {
    return this.passwordField().hasError('minlength')
  }

  passwordDigitError(): boolean {
    return this.passwordField().hasError('pattern')
  }

  passwordMatchError(): boolean {
    return this.confirmPasswordField().hasError('passwordsMatch') 
    && !this.confirmPasswordField().hasError('required') 
    && this.confirmPasswordField().touched
  }

  hasMinLength(): boolean {
    const value = this.passwordField().value || '';
    return value.length >= 5;
  }

  hasDigit(): boolean {
    const value = this.passwordField().value || '';
    return /\d/.test(value);
  }
}
