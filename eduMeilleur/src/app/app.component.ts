import { CommonModule } from '@angular/common';
import { Component, effect, OnInit } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ModalComponent } from './components/modal/modal.component';
import { GlobalService } from './services/global.service';
import { ModalService } from './services/modal.service';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, CommonModule, NgxSpinnerModule, ModalComponent, SidebarComponent, HeaderComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  constructor(public modalService: ModalService, public global: GlobalService) {}
}
