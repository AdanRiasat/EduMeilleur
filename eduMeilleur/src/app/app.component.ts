import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Route, Router, RouterModule, RouterOutlet } from '@angular/router';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent{
  title = 'eduMeilleur';

  userIsConnected: boolean = false

  constructor(public userService: UserService, public route: Router) {}

  profile(){
    this.userIsConnected = this.userService.token() != null

    if (this.userIsConnected){
      this.route.navigate(['/profile'])
    } else {
      this.route.navigate(['/login'])
    }
  }

  disconnect(){
    this.userService.logout()
  }
}
