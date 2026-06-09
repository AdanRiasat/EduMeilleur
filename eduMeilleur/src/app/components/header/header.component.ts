import { Component, effect } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router, RouterLink, RouterLinkActive, RouterModule } from '@angular/router';
import { GlobalService } from '../../services/global.service';
import { ModalService } from '../../services/modal.service';
import { Profile } from '../../models/profile';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from '../sidebar/sidebar.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, CommonModule, SidebarComponent, RouterLinkActive, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  title = 'eduMeilleur';

  username: string | null = '';
  userIsConnected: boolean = false;

  isSidebarOpen: boolean = false;

  timestamp: number = Date.now();

  constructor(
    public userService: UserService,
    public route: Router,
    public global: GlobalService,
    public modalService: ModalService,
  ) {
    effect(() => {
      this.username = userService.username();
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

  openSidebar() {
    this.isSidebarOpen = true;
    console.log(this.isSidebarOpen);
  }

  closeSidebar() {
    this.isSidebarOpen = false;
  }
}
