import { CommonModule } from '@angular/common';
import { Component, effect, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterModule, RouterOutlet } from '@angular/router';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ModalComponent } from './components/modal/modal.component';
import { GlobalService } from './services/global.service';
import { ModalService } from './services/modal.service';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, CommonModule, NgxSpinnerModule, ModalComponent, HeaderComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  showMainOnly: boolean = false
  mainOnlyUrls: string[] = ['/login', '/signup']

  constructor(public modalService: ModalService, public global: GlobalService, public router: Router) {
   this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.showMainOnly = this.mainOnlyUrls.includes(event.urlAfterRedirects);
      });
  }
}
