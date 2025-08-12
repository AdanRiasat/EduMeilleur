import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { AiService } from '../services/ai.service';
import { ChatMessage } from '../models/chatMessage';
import { FormsModule } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { marked } from 'marked';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { Chat } from '../models/chat';
import { Modal } from 'bootstrap';
import { ModalComponent } from '../modal/modal.component';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { SpinnerService } from '../services/spinner.service';

@Component({
  selector: 'app-ai',
  standalone: true,
  imports: [FormsModule, CommonModule, DatePipe, ModalComponent],
  templateUrl: './ai.component.html',
  styleUrl: './ai.component.css'
})
export class AiComponent implements OnInit{

  userMessage: string = ""
  messages: ChatMessage[] = []
  chats: Chat[] = []
  currentChat: Chat | null = null

  isLoading: boolean = false
  loadingId: number = -1
  dropdownOpen: number | null = null
  deleteId: number = -1

  userIsConnected: boolean = false

  constructor(public aiService: AiService, public sanitizer: DomSanitizer, public userService: UserService, public route: Router, public spinner: SpinnerService) {}

  @ViewChild('scrollContainer') private scrollContainer!: ElementRef;
  
  async ngOnInit(){
    this.spinner.show()
    await this.getChats()
    this.spinner.hide()
  }

  scrollToBottom() {
  try {
    setTimeout(() => {
      this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
    }, 50);
  } catch (err) {}
}

  async getChats(){    
    try {
      this.chats = await this.aiService.getChats()
    } catch (error: any) {
      if (error.status === 0){
        this.openErrorModal()
      }
    }
  }

  async getMessages(chat: Chat){
    this.currentChat = chat
    this.messages = await this.aiService.getMessages(chat.id)
    this.scrollToBottom()
  }

  async deleteChat(){
    await this.aiService.deleteChat(this.deleteId)
    
    for (let i = 0; i < this.chats.length; i++){
      if (this.chats[i].id == this.deleteId){
        this.chats.splice(i, 1)
      }
    }

    if (this.currentChat?.id == this.deleteId) {
      this.currentChat = null;
      this.messages = [];
    }

    let modalElement = document.getElementById('deleteChatModal')
    if (modalElement){
      let modal = Modal.getInstance(modalElement)
      modal?.hide()
    }
  }

  async sendMessage() {
    this.userIsConnected = this.userService.token() != null
    if (!this.userIsConnected){
      this.openConnectionModal()
      return
    }

    let text = this.userMessage.trim()
    console.log(text);

    if (text == ""){
      console.log("ummmmmm sirr");
      return
    }

    let userMsg: ChatMessage = {
      text: text,
      isUser: true,
      timeStamp: new Date(),
    }
    this.messages.push(userMsg)

    this.userMessage = ""
    this.isLoading = true
    

    if (this.currentChat == null){
      this.currentChat = await this.aiService.postChat(text)
      await this.getChats()
    }
    
    if (this.currentChat != null){
      this.loadingId = this.currentChat.id
      let botReply = await this.aiService.sendMessage(text, this.currentChat)
      this.messages.push(botReply)
      this.scrollToBottom()
    }

    this.isLoading = false
    this.loadingId = -1

  }

  toggleDropdown(id: number){
    this.dropdownOpen = this.dropdownOpen === id ? null : id;
  }

  newChat(){
    this.currentChat = null
    this.messages = []
  }

  formatMessage(message: string): SafeHtml {
    let rawHtml: string = marked.parse(message) as string
    return this.sanitizer.bypassSecurityTrustHtml(rawHtml);
  }

  @HostListener('document:click', ['$event'])
  onOutsideClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (!target.closest('.list-group-item')) {
      this.dropdownOpen = null;
    }
  }

  // TODO Refactor the 3 modal functions into 1
  openDeleteModal(id: number){
    this.deleteId = id
    let modalElement = document.getElementById('deleteChatModal')
    if (modalElement){
      let modal = new Modal(modalElement)
      modal.show()
    }
  }

  openErrorModal(){
      let modalElement = document.getElementById('error500Modal')
      if (modalElement){
        let modal = Modal.getInstance(modalElement) || new Modal(modalElement);
        modal.show()
      }
      this.spinner.hide()
  }

  openConnectionModal(){
    let modalElement = document.getElementById('errorConnectionModal')
    if (modalElement){
      let modal = Modal.getInstance(modalElement) || new Modal(modalElement);
      modal.show()
    }
  }

  redirectLogin(){
    this.route.navigate(['/login'])
  }
}

