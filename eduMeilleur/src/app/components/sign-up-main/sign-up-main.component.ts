import { CommonModule } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthExtraOptionsComponent } from '../auth-extra-options/auth-extra-options.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-sign-up-main',
  standalone: true,
  imports: [FormsModule, CommonModule, AuthExtraOptionsComponent, RouterLink],
  templateUrl: './sign-up-main.component.html',
  styleUrl: './sign-up-main.component.css'
})
export class SignUpMainComponent {
  next = output<void>()

  email: string = ""
  password: string = ""
  confirmPassword: string = ""

  errors: { [key: string]: string} = {}
}
