import { CommonModule } from '@angular/common';
import { Component, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthExtraOptionsComponent } from '../auth-extra-options/auth-extra-options.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-sign-up-extra',
  standalone: true,
  imports: [FormsModule, CommonModule, AuthExtraOptionsComponent, RouterLink],
  templateUrl: './sign-up-extra.component.html',
  styleUrl: './sign-up-extra.component.css'
})
export class SignUpExtraComponent {
  register = output<void>()
  back = output<void>()

  username: string = ""
  school: string = ""
  schoolYear: string = ""

  errors: { [key: string]: string} = {}

}
