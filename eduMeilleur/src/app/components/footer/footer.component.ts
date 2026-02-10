import { Component } from '@angular/core';
import { ModalService } from '../../services/modal.service';
import { ModalComponent } from '../modal/modal.component';
import { GlobalService } from '../../services/global.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [ModalComponent, RouterLink],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent {
  constructor(public modalService: ModalService, public global: GlobalService) {}
}
