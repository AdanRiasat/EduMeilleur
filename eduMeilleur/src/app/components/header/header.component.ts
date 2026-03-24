import { Component, effect } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router, RouterLink } from '@angular/router';
import { GlobalService } from '../../services/global.service';
import { ModalService } from '../../services/modal.service';
import { Profile } from '../../models/profile';
import { Modal } from 'bootstrap';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { ModalComponent } from '../modal/modal.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, CommonModule, SidebarComponent, ModalComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  title = 'eduMeilleur';

  username: string | null = '';
  userIsConnected: boolean = false;

  isSidebarOpen: boolean = false

  timestamp: number = Date.now();

  constructor(public userService: UserService, public route: Router, public global: GlobalService, public modalService: ModalService) {
    effect(() => {
      let token: string | null = userService.token();
      if (token) {
        let profileString: string | null = localStorage.getItem('profile');
        if (profileString) {
          let profile: Profile = JSON.parse(profileString);
          this.username = profile.username;
        }
      } else {
        this.username = null;
      }
    });
  }

  profile() {
    this.userIsConnected = this.userService.token() != null;

    if (this.userIsConnected) {
      this.route.navigate(['/profile']);
    } else {
      this.route.navigate(['/login']);
    }
  }

  openDisconnectModal() {
    this.userIsConnected = this.userService.token() != null;

    if (!this.userIsConnected) {
      this.route.navigate(['/login']);
      return;
    }

    this.modalService.openModal('disconnectModal');
  }

  disconnect() {
    this.userService.logout();

    let modalElement = document.getElementById('disconnectModal');
    if (modalElement) {
      let modal = Modal.getInstance(modalElement);
      modal?.hide();
    }

    this.route.navigate(['/home']);
  }

  openSidebar() {
    this.isSidebarOpen = true
    console.log(this.isSidebarOpen);
    
  }

  closeSidebar() {
    this.isSidebarOpen = false
  }
}
