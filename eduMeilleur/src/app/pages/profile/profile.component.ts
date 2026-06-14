import { Component, OnInit } from '@angular/core';
import { Profile } from '../../models/profile';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SpinnerService } from '../../services/spinner.service';
import { GlobalService } from '../../services/global.service';
import { ModalService } from '../../services/modal.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent implements OnInit {
  profile: Profile | null = null;

  timestamp: number = Date.now();

  constructor(
    public userService: UserService,
    public spinner: SpinnerService,
    public global: GlobalService,
    public modalService: ModalService,
  ) {}

  async ngOnInit() {
    this.spinner.show();
    try {
      this.profile = await this.userService.getProfile();
    } catch {
      this.modalService.openErrorModal(() => this.ngOnInit());
    }
    this.spinner.hide();
  }
}
