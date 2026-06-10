import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, EventEmitter, OnInit, Output } from '@angular/core';
import { DisplaySujet } from '../../models/displaySujet';
import { SujetService } from '../../services/sujet.service';
import { RouterModule } from '@angular/router';
import { SpinnerService } from '../../services/spinner.service';
import { ModalService } from '../../services/modal.service';

@Component({
  selector: 'app-image-slider',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './image-slider.component.html',
  styleUrl: './image-slider.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ImageSliderComponent implements OnInit {
  @Output() doneLoading = new EventEmitter<boolean>(false);

  sujets: DisplaySujet[] = [];

  constructor(
    public sujetService: SujetService,
    public spinner: SpinnerService,
    public modalService: ModalService,
  ) {}

  async ngOnInit() {
    try {
      this.doneLoading.emit(false);
      this.sujets = await this.sujetService.getSujets();
    } catch {
      this.modalService.openErrorModal(() => {
        window.location.reload();
      });
    }

    this.doneLoading.emit(true);
  }
}
