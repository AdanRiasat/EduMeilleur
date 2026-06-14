import { Component, OnInit } from '@angular/core';
import { DisplaySujet } from '../../models/displaySujet';
import { SujetService } from '../../services/sujet.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ModalService } from '../../services/modal.service';

@Component({
  selector: 'app-sujets',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sujets.component.html',
  styleUrl: './sujets.component.css',
})
export class SujetsComponent implements OnInit {
  sujets: DisplaySujet[] = [];
  displaySujets: DisplaySujet[] = [];

  constructor(
    public sujetService: SujetService,
    public modalService: ModalService,
  ) {}

  async ngOnInit() {
    await this.getSujets();
    this.displaySujets = this.sujets;
  }

  async getSujets() {
    try {
      this.sujets = await this.sujetService.getSujets();
    } catch {
      this.modalService.openErrorModal(() => this.ngOnInit());
    }
  }

  sort(type: string) {
    if (type == 'All') {
      this.displaySujets = this.sujets;
      return;
    }

    this.displaySujets = [];

    for (let s of this.sujets) {
      if (s.type == type) {
        this.displaySujets.push(s);
      }
    }
  }
}
