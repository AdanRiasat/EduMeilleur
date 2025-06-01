import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title = 'eduMeilleur';

  userIsConnected: boolean = false

  ngOnInit() {
    this.userIsConnected = localStorage.getItem("token") != null
  }

  disconnect(){
    localStorage.clear()
    this.userIsConnected = false
  }
}
