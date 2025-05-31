import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';

@Component({
  selector: 'app-image-slider',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './image-slider.component.html',
  styleUrl: './image-slider.component.css'
})
export class ImageSliderComponent implements AfterViewInit {
  @Input() images: string[] = [];
  @ViewChild('sliderTrack', { static: false }) sliderTrack?: ElementRef<HTMLDivElement>;

  currentIndex = 0;
  private startX: number | null = null;
  private deltaX: number = 0;
  transitioning = false;

  ngAfterViewInit() {
    this.attachSwipeHandlers();
    this.resetTransform();
  }

  prev() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
      this.transitioning = true;
      this.resetTransform();
    }
  }

  next() {
    if (this.currentIndex < this.images.length - 1) {
      this.currentIndex++;
      this.transitioning = true;
      this.resetTransform();
    }
  }

  // Handle mouse/touch drag logic
  attachSwipeHandlers() {
    const track = this.sliderTrack?.nativeElement;
    if (!track) return;

    // Mouse events
    track.addEventListener('mousedown', (e: MouseEvent) => {
      this.startX = e.clientX;
      this.deltaX = 0;
      track.style.transition = 'none';
    });

    window.addEventListener('mousemove', (e: MouseEvent) => {
      if (this.startX !== null) {
        this.deltaX = e.clientX - this.startX;
        this.updateTransform();
      }
    });

    window.addEventListener('mouseup', () => {
      if (this.startX !== null) {
        this.finishSwipe();
      }
    });

    // Touch events
    track.addEventListener('touchstart', (e: TouchEvent) => {
      this.startX = e.touches[0].clientX;
      this.deltaX = 0;
      track.style.transition = 'none';
    });

    window.addEventListener('touchmove', (e: TouchEvent) => {
      if (this.startX !== null && e.touches && e.touches.length === 1) {
        this.deltaX = e.touches[0].clientX - this.startX;
        this.updateTransform();
      }
    });

    window.addEventListener('touchend', () => {
      if (this.startX !== null) {
        this.finishSwipe();
      }
    });
  }

  updateTransform() {
    const track = this.sliderTrack?.nativeElement;
    if (!track) return;
    const translate = -this.currentIndex * 100 + (this.deltaX / track.offsetWidth) * 100;
    track.style.transform = `translateX(${translate}%)`;
  }

  finishSwipe() {
    const track = this.sliderTrack?.nativeElement;
    if (!track) return;
    track.style.transition = 'transform 0.3s';
    if (this.deltaX > 50 && this.currentIndex > 0) {
      this.currentIndex--;
    } else if (this.deltaX < -50 && this.currentIndex < this.images.length - 1) {
      this.currentIndex++;
    }
    this.deltaX = 0;
    this.startX = null;
    this.transitioning = true;
    this.resetTransform();
  }

  resetTransform() {
    const track = this.sliderTrack?.nativeElement;
    if (!track) return;
    track.style.transform = `translateX(-${this.currentIndex * 100}%)`;
  }

  onTransitionEnd() {
    this.transitioning = false;
  }
}