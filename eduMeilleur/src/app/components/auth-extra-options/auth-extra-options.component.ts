import { Component } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-auth-extra-options',
  standalone: true,
  imports: [],
  templateUrl: './auth-extra-options.component.html',
  styleUrl: './auth-extra-options.component.css',
})
export class AuthExtraOptionsComponent {
  loginWithGoogle() {
    window.location.href = `${environment.apiUrl}/api/Users/GoogleLogin`;
  }
}
