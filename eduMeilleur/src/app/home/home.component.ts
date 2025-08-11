import { CommonModule } from '@angular/common';
import { Component, AfterViewInit, OnInit } from '@angular/core';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { ImageSliderComponent } from '../image-slider/image-slider.component';
import { SujetService } from '../services/sujet.service';
import { DisplaySujet } from '../models/displaySujet';
import { RouterModule } from '@angular/router';
import { GlobalService } from '../services/global.service';
import { UserService } from '../services/user.service';
import { environment } from '../../environments/environment';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgbCarouselModule, CommonModule, ImageSliderComponent, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit  {
  teachersNames: string[] = []

  domain = environment.apiUrl
  timestamp: number = Date.now();
 
  constructor (public global: GlobalService, public userService: UserService) {}
  
  async ngOnInit() {
   this.teachersNames = await this.userService.getTeachers()
  }
}
