import { Component, ElementRef, ViewChild } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SpinnerService } from '../services/spinner.service';
import { GlobalService } from '../services/global.service';

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
  titleAdmin: string = ""
  messageAdmin: string = ""

  @ViewChild("fileInputTeacher", {static: false}) fileInputTeacher!: ElementRef<HTMLInputElement>
  @ViewChild("fileInputAdmin", {static: false}) fileInputAdmin!: ElementRef<HTMLInputElement>

  teacherFiles: File[] = []
  adminFiles: File[] = []

  constructor(public contactService: ContactService, public spinner: SpinnerService, public global: GlobalService) {}

  updateTeacherFiles() {
    this.updateSelectedFiles(this.fileInputTeacher, this.teacherFiles);
  }

  updateAdminFiles() {
    this.updateSelectedFiles(this.fileInputAdmin, this.adminFiles);
  }

  updateSelectedFiles(input: ElementRef<HTMLInputElement>, fileList: File[]){
    if (input.nativeElement.files){
      for (let f of Array.from(input.nativeElement.files)){
        if (!fileList.includes(f)){
          fileList.push(f)
        }
      }
      input.nativeElement.value = "";
    } 
  }

  removeTeacherFile(index: number) {
    this.teacherFiles.splice(index, 1);
  }

  removeAdminFile(index: number) {
    this.adminFiles.splice(index, 1);
  }

  //TODO Error messages and handling
  async postQuestion(){
    if (this.titleTeacher == "" || this.messageTeacher == ""){
      alert("hmmm sir you cant do that")
      return
    }

    this.spinner.show()

    let formData = new FormData()
    formData.append("title", this.titleTeacher)
    formData.append("message", this.messageTeacher)

    
    let i = 1
    for (let f of this.teacherFiles){
      if (f != null){
        formData.append("file" + i, f, f.name)
      }
      i++
    }

    await this.contactService.postQuestion(formData)

    //reset
    this.spinner.hide()
    this.titleTeacher = ""
    this.messageTeacher = ""
    this.teacherFiles = []
    if (this.fileInputTeacher){
      this.fileInputTeacher.nativeElement.value = "";
    } 
  }

  async postFeedback(){
    if (this.titleAdmin == "" || this.messageAdmin == ""){
      alert("hmmm sir you cant do that")
      return
    }

    this.spinner.show()

    let formData = new FormData()
    formData.append("title", this.titleAdmin)
    formData.append("message", this.messageAdmin)

    
    let i = 1
    for (let f of this.adminFiles){
      if (f != null){
        formData.append("file" + i, f, f.name)
      }
      i++
    }

    await this.contactService.postFeedback(formData)

    //reset
    this.spinner.hide()
    this.titleAdmin = ""
    this.messageAdmin = ""
    this.adminFiles = []
    if (this.fileInputAdmin){
      this.fileInputAdmin.nativeElement.value = "";
    } 
  }

}

