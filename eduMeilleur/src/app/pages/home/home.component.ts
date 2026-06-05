import { CommonModule } from '@angular/common';
import { Component, AfterViewInit, OnInit } from '@angular/core';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { ImageSliderComponent } from '../../components/image-slider/image-slider.component';
import { SujetService } from '../../services/sujet.service';
import { DisplaySujet } from '../../models/displaySujet';
import { RouterModule } from '@angular/router';
import { GlobalService } from '../../services/global.service';
import { UserService } from '../../services/user.service';
import { environment } from '../../../environments/environment';
import { ModalComponent } from '../../components/modal/modal.component';
import { Modal } from 'bootstrap';
import { ModalService } from '../../services/modal.service';
import { HomeLoaderComponent } from '../../components/home-loader/home-loader.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgbCarouselModule, CommonModule, ImageSliderComponent, RouterModule, HomeLoaderComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  teachersNames: string[] = [];

  domain = environment.apiUrl;
  timestamp: number = Date.now();

  isLoading: boolean = true;
  isApiReady: boolean = false;
  isVideoReady: boolean = false;

  constructor(
    public global: GlobalService,
    public userService: UserService,
    public modalService: ModalService,
  ) {}

  async ngOnInit() {
    document.body.style.overflow = 'hidden';

    this.teachersNames = await this.userService.getTeachers();
    this.isApiReady = true;
    this.tryHideLoader();
  }

  ngAfterViewInit() {
    const video: HTMLVideoElement | null = document.querySelector('.hero-video');
    if (!video) return;

    video.muted = true;
    video.play().catch((err) => {
      console.log('Autoplay blocked:', err);
    });

    if (video.readyState >= 3) {
      this.isVideoReady = true;
    } else {
      video.addEventListener(
        'canplaythrough',
        () => {
          this.isVideoReady = true;
          this.tryHideLoader();
        },
        { once: true },
      );
    }
  }

  tryHideLoader() {
    if (!this.isApiReady || !this.isVideoReady) return;
    this.isLoading = false;
    document.body.style.overflow = '';
  }
}
