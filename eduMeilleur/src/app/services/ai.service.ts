import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ChatMessage } from '../models/chatMessage';
import { Chat } from '../models/chat';
import { environment } from '../../environments/environment';
import { ModalService } from './modal.service';
import { UserService } from './user.service';

const domain: string = environment.apiUrl

@Injectable({
  providedIn: 'root'
})
export class AiService {
  chats = signal<Chat[]>([]);
  currentChat = signal<Chat | null>(null);
  dropdownOpen = signal<number | null>(null);
  messages = signal<ChatMessage[]>([]);

  deleteId: number = -1

  constructor(public http: HttpClient, public modalService: ModalService, public userService: UserService) { }

  async streamMessage(text: string, chat: Chat, onChunk?: () => void): Promise<void> {
     let dto = {
      id: 0,
      text: text,
      isUser: true,
      timeStamp: new Date,
      chatId: chat.id,
    }

    let res = await fetch(`${domain}/api/Chats/StreamMessage`, {
      method: 'POST',
      headers : {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.userService.token()}`
      },
      body: JSON.stringify(dto),
    })

    if (!res.ok || !res.body) {
      throw new Error(`Stream request failed: ${res.status}`);
    }

    let botMsg = new ChatMessage("", false, new Date)
    this.messages().push(botMsg)

    let reader = res.body.getReader()
    let decoder = new TextDecoder()

    while (true) {
      let { done, value} = await reader.read()
      if (done) break

      let raw = decoder.decode(value, { stream: true });

       for (let line of raw.split('\n')) {
        if (line.startsWith('data: ')) {
          try {
            botMsg.text += JSON.parse(line.slice(6));
          } catch {
            // skip malformed lines
          }
        }
      }

      onChunk?.();
    }
  }

  async postChat(message: string): Promise<Chat>{
    let dto = {
      initialMessage: message
    }

    let x = await lastValueFrom(this.http.post<Chat>(domain + "/api/Chats/CreateChat", dto))
    console.log(x);

    return x
  }

  async deleteChat(id: number){
    let x = await lastValueFrom(this.http.delete<any>(domain + "/api/Chats/DeleteChat/" + id))
    console.log(x);
  }

  async getMessages(chatId: number) {
    let x = await lastValueFrom(this.http.get<ChatMessage[]>(domain + "/api/Chats/GetMessages/" + chatId))
    console.log(x);

    this.messages.set(x)
    
    return x
  }

  async getChats() {
    let x = await lastValueFrom(this.http.get<Chat[]>(domain + "/api/Chats/GetChats"))
    console.log(x);

    this.chats.set(x)
  }

  newChat() {
    this.currentChat.set(null);
    this.messages.set([]);
  }

  toggleDropdown(id: number) {
    this.dropdownOpen.set(this.dropdownOpen() === id ? null : id);
  }

  openDeleteModal(id: number) {
    this.deleteId = id;
    this.modalService.openModal('deleteChatModal');
  }
}
