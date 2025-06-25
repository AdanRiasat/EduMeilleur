import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ChatMessage } from '../models/chatMessage';
import { Chat } from '../models/chat';

const domain: string ="https://localhost:7027"

@Injectable({
  providedIn: 'root'
})
export class AiService {

  constructor(public http: HttpClient) { }

  async sendMessage(text: string, chat: Chat): Promise<ChatMessage> {
    let dto = {
      id: 0,
      text: text,
      isUser: true,
      timeStamp: new Date,
      chatId: chat.id,
    }

    let x = await lastValueFrom(this.http.post<ChatMessage>(domain + "/api/Chats/SendMessage", dto))
    console.log(x);

    return x
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

  async getMessages(chatId: number): Promise<ChatMessage[]>{
    let x = await lastValueFrom(this.http.get<ChatMessage[]>(domain + "/api/Chats/GetMessages/" + chatId))
    console.log(x);
    
    return x
  }

  async getChats(): Promise<Chat[]>{
    let x = await lastValueFrom(this.http.get<Chat[]>(domain + "/api/Chats/GetChats"))
    console.log(x);

    return x
  }
}
