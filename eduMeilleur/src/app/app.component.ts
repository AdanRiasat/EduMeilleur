import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Route, Router, RouterModule, RouterOutlet } from '@angular/router';
import { UserService } from './services/user.service';
import { Modal } from 'bootstrap';

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

  openDisconnectModal(){
    this.userIsConnected = this.userService.token() != null
    
    if (!this.userIsConnected){
      this.route.navigate(['/login'])
      return
    } 

    let modalElement = document.getElementById('disconnectModal')
    if (modalElement){
      let modal = new Modal(modalElement)
      modal.show()
    }
  }

  async disconnect(){
    await this.userService.logout()

    let modalElement = document.getElementById('disconnectModal')
    if (modalElement){
      let modal = Modal.getInstance(modalElement)
      modal?.hide()
    }

    this.route.navigate(['/home'])

  }
}
