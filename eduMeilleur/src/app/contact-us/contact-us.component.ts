import { Component } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-contact-us',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './contact-us.component.html',
  styleUrl: './contact-us.component.css'
})
export class ContactUsComponent {

  titleTeacher: string = ""
  messageTeacher: string = ""

  constructor(public contactService: ContactService) {}

  postQuestion(){
    this.contactService.postQuestion(this.titleTeacher, this.messageTeacher)
  }

}

