import { Component, ElementRef, ViewChild, signal } from '@angular/core';
import { ContactService } from '../../services/contact.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SpinnerService } from '../../services/spinner.service';
import { GlobalService } from '../../services/global.service';
import { UserService } from '../../services/user.service';
import { ModalService } from '../../services/modal.service';
import { Router } from '@angular/router';
import { ModalComponent } from '../../components/modal/modal.component';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-contact-us',
  standalone: true,
  imports: [FormsModule, CommonModule, ModalComponent],
  templateUrl: './contact-us.component.html',
  styleUrl: './contact-us.component.css',
})
export class ContactUsComponent {
  readonly MAX_MESSAGE_LENGTH = 1500;
  readonly MAX_SINGLE_FILE_SIZE = 4194304;
  readonly MAX_TOTAL_SIZE = 15728640;

  titleTeacher: string = '';
  messageTeacher: string = '';
  titleAdmin: string = '';
  messageAdmin: string = '';

  teacherMessageCount = signal<number>(0);
  adminMessageCount = signal<number>(0);

  userIsConnected: boolean = false;

  @ViewChild('fileInputTeacher', { static: false }) fileInputTeacher!: ElementRef<HTMLInputElement>;
  @ViewChild('fileInputAdmin', { static: false }) fileInputAdmin!: ElementRef<HTMLInputElement>;

  teacherFiles: File[] = [];
  adminFiles: File[] = [];

  constructor(
    public contactService: ContactService,
    public spinner: SpinnerService,
    public global: GlobalService,
    public userService: UserService,
    public modalService: ModalService,
    public route: Router,
    public toastService: ToastService,
  ) {}

  updateTeacherFiles() {
    this.updateSelectedFiles(this.fileInputTeacher, this.teacherFiles);
  }

  updateAdminFiles() {
    this.updateSelectedFiles(this.fileInputAdmin, this.adminFiles);
  }

  onTeacherMessageChange(value: string) {
    this.messageTeacher = value;
    this.teacherMessageCount.set(value ? value.length : 0);
  }

  onAdminMessageChange(value: string) {
    this.messageAdmin = value;
    this.adminMessageCount.set(value ? value.length : 0);
  }

  updateSelectedFiles(input: ElementRef<HTMLInputElement>, fileList: File[]) {
    if (!input.nativeElement.files) return;

    for (let f of Array.from(input.nativeElement.files)) {
      if (fileList.includes(f)) continue;

      if (f.size > this.MAX_SINGLE_FILE_SIZE) {
        this.toastService.error(`${f.name} exceeds the 4MB file size limit.`);
        continue;
      }

      const currentTotal = fileList.reduce((sum, file) => sum + file.size, 0);
      if (currentTotal + f.size > this.MAX_TOTAL_SIZE) {
        this.toastService.error(`Adding ${f.name} would exceed the 15MB total limit.`);
        break;
      }

      fileList.push(f);
    }
    input.nativeElement.value = '';
  }

  removeTeacherFile(index: number) {
    this.teacherFiles.splice(index, 1);
  }

  removeAdminFile(index: number) {
    this.adminFiles.splice(index, 1);
  }

  //TODO Error messages and handling
  async postQuestion() {
    this.userIsConnected = this.userService.token() != null;
    if (!this.userIsConnected) {
      this.modalService.openModal('errorConnectionModal');
      return;
    }

    if (this.titleTeacher == '' || this.messageTeacher == '') {
      this.modalService.openModal('errorEmptyModal');
      return;
    }

    this.spinner.show();

    let formData = new FormData();
    formData.append('title', this.titleTeacher);
    formData.append('message', this.messageTeacher);

    let i = 1;
    for (let f of this.teacherFiles) {
      if (f != null) {
        formData.append('file' + i, f, f.name);
      }
      i++;
    }

    try {
      await this.contactService.postQuestion(formData);
      this.toastService.success('Question sent successfully! A teacher will reach out to you shortly');

      // reset
      this.titleTeacher = '';
      this.messageTeacher = '';
      this.teacherMessageCount.set(0);
      this.teacherFiles = [];
      if (this.fileInputTeacher) {
        this.fileInputTeacher.nativeElement.value = '';
      }
    } catch (e) {
      this.toastService.error('Something went wrong while sending your question, try again later');
    } finally {
      this.spinner.hide();
    }
  }

  async postFeedback() {
    this.userIsConnected = this.userService.token() != null;
    if (!this.userIsConnected) {
      this.modalService.openModal('errorConnectionModal');
      return;
    }

    if (this.titleAdmin == '' || this.messageAdmin == '') {
      this.modalService.openModal('errorEmptyModal');
      return;
    }

    this.spinner.show();

    let formData = new FormData();
    formData.append('title', this.titleAdmin);
    formData.append('message', this.messageAdmin);

    let i = 1;
    for (let f of this.adminFiles) {
      if (f != null) {
        formData.append('file' + i, f, f.name);
      }
      i++;
    }

    try {
      await this.contactService.postFeedback(formData);
      this.toastService.success('Feedback sent successfully. Thank you for helping improve EduMeilleur!');

      // reset
      this.titleAdmin = '';
      this.messageAdmin = '';
      this.adminMessageCount.set(0);
      this.adminFiles = [];
      if (this.fileInputAdmin) {
        this.fileInputAdmin.nativeElement.value = '';
      }
    } catch (e) {
      this.toastService.error('Something went wrong while sending feedback, please try again later');
    } finally {
      this.spinner.hide();
    }
  }

  redirectLogin() {
    this.route.navigate(['/login']);
  }
}
