import { CommonModule } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthExtraOptionsComponent } from '../auth-extra-options/auth-extra-options.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-sign-up-extra',
  standalone: true,
  imports: [FormsModule, CommonModule, AuthExtraOptionsComponent, RouterLink, ReactiveFormsModule],
  templateUrl: './sign-up-extra.component.html',
  styleUrl: './sign-up-extra.component.css'
})
export class SignUpExtraComponent {
  register = output<void>()
  back = output<void>()

  formGroup = input<FormGroup>()

  errors: { [key: string]: string} = {}
}
