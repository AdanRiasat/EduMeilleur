import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-home-loader',
  standalone: true,
  imports: [],
  templateUrl: './home-loader.component.html',
  styleUrl: './home-loader.component.css',
})
export class HomeLoaderComponent {
  @Input() visible: boolean = true;
}
