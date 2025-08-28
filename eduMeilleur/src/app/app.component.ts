import { CommonModule } from '@angular/common';
import { Component, effect, OnInit } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { UserService } from './services/user.service';
import { Modal } from 'bootstrap';
import { Profile } from './models/profile';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ModalComponent } from './modal/modal.component';
import { GlobalService } from './services/global.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, CommonModule, NgxSpinnerModule, ModalComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent{
  title = 'eduMeilleur';
  
  username: string | null = ""
  userIsConnected: boolean = false

  timestamp: number = Date.now();

  constructor(public userService: UserService, public route: Router, public global: GlobalService) {
    effect(() => {
      let token: string | null = userService.token(); 
      if (token) {
        let profileString: string | null = localStorage.getItem("profile");
        if (profileString) {
          let profile: Profile = JSON.parse(profileString);
          this.username = profile.username;
        }
      } else {
        this.username = null;
      }
    })
  }

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
