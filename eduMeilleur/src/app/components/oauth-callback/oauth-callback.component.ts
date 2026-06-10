import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-oauth-callback',
  standalone: true,
  imports: [],
  templateUrl: './oauth-callback.component.html',
  styleUrl: './oauth-callback.component.css',
})
export class OauthCallbackComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private router: Router,
  ) {}

  ngOnInit() {
    let token = this.route.snapshot.queryParamMap.get('token');
    let refreshToken = this.route.snapshot.queryParamMap.get('refreshToken');
    let username = this.route.snapshot.queryParamMap.get('username');

    let data: any = {
      token: token,
      refreshToken: refreshToken,
      username: username,
    };

    if (token && refreshToken && username) {
      data.roles = this.retrieveRoles(token);
      this.userService.updateSignals(data);

      window.history.replaceState({}, '', '/');
      this.router.navigate(['/profile']);
    } else {
      this.router.navigate(['/login']);
    }
  }

  retrieveRoles(token: string): string[] {
    let decoded: any = jwtDecode(token);
    let rawRoles = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    let roles: string[] = Array.isArray(rawRoles) ? rawRoles : rawRoles ? [rawRoles] : [];

    return roles;
  }
}
