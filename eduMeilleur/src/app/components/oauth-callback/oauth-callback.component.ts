import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user.service';

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
    let roles = this.route.snapshot.queryParamMap.get('roles');

    let data = {
      token: token,
      refreshToken: refreshToken,
      username: username,
      roles: roles,
    };

    if (token && refreshToken && username && roles) {
      this.userService.updateSignals(data);

      window.history.replaceState({}, '', '/');
      this.router.navigate(['/profile']);
    } else {
      this.router.navigate(['/login']);
    }
  }
}
