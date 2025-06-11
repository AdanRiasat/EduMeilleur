import { Component, ElementRef, ViewChild } from '@angular/core';
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

  @ViewChild("fileInputTeacher", {static: false}) fileInput ?: ElementRef

  constructor(public contactService: ContactService) {}

  postQuestion(){
    let formData = new FormData()
    formData.append("title", this.titleTeacher)
    formData.append("message", this.messageTeacher)

    if (this.fileInput != undefined){
      let i = 1
      for (let f of this.fileInput.nativeElement.files){
        if (f != null){
          formData.append("file" + i, f, f.name)
        }
        i++
      }
      
    }
    this.contactService.postQuestion(formData)
  }

}

