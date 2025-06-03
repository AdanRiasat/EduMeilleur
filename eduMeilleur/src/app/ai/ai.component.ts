import { AfterViewChecked, Component, ElementRef, ViewChild } from '@angular/core';
import { AiService } from '../services/ai.service';
import { ChatMessage } from '../models/chatMessage';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { marked } from 'marked';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-ai',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ai.component.html',
  styleUrl: './ai.component.css'
})
export class AiComponent implements AfterViewChecked{

  userMessage: string = ""
  messages: ChatMessage[] = []

  constructor(public aiService: AiService, public sanitizer: DomSanitizer) {}

  @ViewChild('scrollContainer') private scrollContainer!: ElementRef;
  
  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  scrollToBottom(): void {
    try {
      this.scrollContainer.nativeElement.scrollTop = 
        this.scrollContainer.nativeElement.scrollHeight;
    } catch(err) { }
  }

  async sendMessage() {
    let text = this.userMessage.trim()
    console.log(text);
    
    // Add user message
    this.messages.push(new ChatMessage(text, true, new Date))

    //bot response
    let botReply = await this.aiService.sendMessage(text)
    this.messages.push(new ChatMessage(botReply, false, new Date))
    console.log(botReply);
  }

  formatMessage(message: string): SafeHtml {
    let rawHtml: string = marked.parse(message) as string
    return this.sanitizer.bypassSecurityTrustHtml(rawHtml);
  }
}

