import { Component, ElementRef, ViewChild } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-contact-us',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './contact-us.component.html',
  styleUrl: './contact-us.component.css'
})
export class ContactUsComponent {

  titleTeacher: string = ""
  messageTeacher: string = ""

  @ViewChild("fileInputTeacher", {static: false}) fileInput ?: ElementRef

  selectedFiles: File[] = []

  constructor(public contactService: ContactService) {}

  updateSelectedFiles(){
    if (this.fileInput?.nativeElement.files){
      for (let f of this.fileInput.nativeElement.files){
        if (!this.selectedFiles.includes(f)){
          this.selectedFiles.push(f)
        }
      }
      this.fileInput.nativeElement.value = "";
    } 
  }

  removeFile(index: number){
    this.selectedFiles.splice(index, 1)
  }

  async postQuestion(){
    if (this.titleTeacher == "" || this.messageTeacher == ""){
      alert("hmmm sir you cant do that")
      return
    }

    let formData = new FormData()
    formData.append("title", this.titleTeacher)
    formData.append("message", this.messageTeacher)

    
    let i = 1
    for (let f of this.selectedFiles){
      if (f != null){
        formData.append("file" + i, f, f.name)
      }
      i++
    }

    await this.contactService.postQuestion(formData)

    //reset
    this.titleTeacher = ""
    this.messageTeacher = ""
    this.selectedFiles = []
    if (this.fileInput){
      this.fileInput.nativeElement.value = "";
    } 
  }

}

