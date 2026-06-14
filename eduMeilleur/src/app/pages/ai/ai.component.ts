import { Component, ElementRef, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { AiService } from '../../services/ai.service';
import { ChatMessage } from '../../models/chatMessage';
import { FormsModule } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { marked } from 'marked';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { Chat } from '../../models/chat';
import { Modal } from 'bootstrap';
import { ModalComponent } from '../../components/modal/modal.component';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { SpinnerService } from '../../services/spinner.service';
import { ModalService } from '../../services/modal.service';
import { ChatbotSidebarComponent } from '../../components/chatbot-sidebar/chatbot-sidebar.component';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-ai',
  standalone: true,
  imports: [FormsModule, CommonModule, DatePipe, ModalComponent, ChatbotSidebarComponent],
  templateUrl: './ai.component.html',
  styleUrl: './ai.component.css',
})
export class AiComponent implements OnInit {
  aiService = inject(AiService)

  userMessage: string = '';

  messages = this.aiService.messages;
  currentChat = this.aiService.currentChat;
  chats = this.aiService.chats;
  dropdownOpen = this.aiService.dropdownOpen;
  deleteId = this.aiService.deleteId

  isLoading: boolean = false;
  loadingId: number = -1;

  userIsConnected: boolean = false;

  isNearBottom = true;
  scrollThrottle: any = null;

  constructor(public sanitizer: DomSanitizer, public userService: UserService, public route: Router, public spinner: SpinnerService, public modalService: ModalService, public toastService: ToastService) {}

  @ViewChild('scrollContainer') private scrollContainer!: ElementRef;

  async ngOnInit() {
    this.spinner.show();
    await this.getChats();
    this.scrollToBottom()
    this.spinner.hide();
  }

  ngAfterViewInit() {
    const el = this.scrollContainer.nativeElement;
    el.addEventListener('scroll', () => {
      this.isNearBottom = el.scrollHeight - el.scrollTop - el.clientHeight < 50;
    });
  }

  scrollToBottom() {
    if (!this.isNearBottom) return;
    if (this.scrollThrottle) return;
    this.scrollThrottle = setTimeout(() => {
      this.scrollContainer.nativeElement.scrollTo({
        top: this.scrollContainer.nativeElement.scrollHeight,
        behavior: 'smooth'
      });
      this.scrollThrottle = null;
    }, 100);
  }

  async getChats() {
    try {
      await this.aiService.getChats();
    } catch (error: any) {
      if (error.status === 0) {
        this.modalService.openModal('error500Modal');
      }
    }
  }

  async getMessages(chat: Chat) {
    this.currentChat.set(chat);
    await this.aiService.getMessages(chat.id);
    this.scrollToBottom();
  }

  async deleteChat() {
    await this.aiService.deleteChat(this.deleteId);

     let chats = this.chats()

    for (let i = 0; i < chats.length; i++) {
      if (chats[i].id == this.deleteId) {
        chats.splice(i, 1);
      }
    }

    if (this.currentChat()?.id == this.deleteId) {
      this.currentChat.set(null);
      this.messages.set([]);
    }

    let modalElement = document.getElementById('deleteChatModal');
    if (modalElement) {
      let modal = Modal.getInstance(modalElement);
      modal?.hide();
    }
  }

  async sendMessage() {
    this.userIsConnected = this.userService.token() != null;
    if (!this.userIsConnected) {
      this.modalService.openModal('errorConnectionModal');
      return;
    }

    let text = this.userMessage.trim();
    console.log(text);

    if (text == '') {
      return;
    }

    let userMsg: ChatMessage = {
      text: text,
      isUser: true,
      timeStamp: new Date(),
    };
    this.messages().push(userMsg);

    this.userMessage = '';
    this.isLoading = true;

    if (this.currentChat() == null) {
      this.currentChat.set(await this.aiService.postChat(text));
      await this.getChats();
    }

    if (this.currentChat() != null) {
      this.isNearBottom = true;
      this.loadingId = this.currentChat()!.id;
      this.scrollToBottom();
      try {
        await this.aiService.streamMessage(text, this.currentChat()!, () => this.scrollToBottom())
      } catch {
        this.toastService.error('Something went wrong while sending your message, please try again later');
      }
    }

    this.isLoading = false;
    this.loadingId = -1;
  }

  formatMessage(message: string): SafeHtml {
    let rawHtml: string = marked.parse(message) as string;
    return this.sanitizer.bypassSecurityTrustHtml(rawHtml);
  }

  @HostListener('document:click', ['$event'])
  onOutsideClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (!target.closest('.list-group-item')) {
      this.dropdownOpen.set(null);
    }
  }

  redirectLogin() {
    this.route.navigate(['/login']);
  }
}
