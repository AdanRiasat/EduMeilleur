import { AfterViewChecked, Component, ElementRef, ViewChild } from '@angular/core';

interface Message {
  text: string;
  isUser: boolean;
  timestamp: Date;
  options?: string[];
}

@Component({
  selector: 'app-ai',
  standalone: true,
  imports: [],
  templateUrl: './ai.component.html',
  styleUrl: './ai.component.css'
})
export class AiComponent implements AfterViewChecked{
  @ViewChild('scrollContainer') private scrollContainer!: ElementRef;
  @ViewChild('messageInput') private messageInput!: ElementRef;
  
  messages: Message[] = [
    {
      text: 'Hello! How can I help you today?',
      isUser: false,
      timestamp: new Date()
    }
  ];

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  scrollToBottom(): void {
    try {
      this.scrollContainer.nativeElement.scrollTop = 
        this.scrollContainer.nativeElement.scrollHeight;
    } catch(err) { }
  }

  sendMessage(text: string): void {
    if (!text.trim()) return;
    
    // Add user message
    this.messages.push({
      text: text,
      isUser: true,
      timestamp: new Date()
    });

    // Simulate bot response (you would replace this with actual API call)
    setTimeout(() => {
      this.messages.push({
        text: 'Thanks for your message! I\'ll get back to you shortly.',
        isUser: false,
        timestamp: new Date()
      });
    }, 1000);
  }

  // Optional: For handling option buttons
  handleOption(option: string): void {
    this.sendMessage(option);
  }
}

