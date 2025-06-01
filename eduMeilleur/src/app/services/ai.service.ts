import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

const apikey = "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" //will have to move for security
const url = "https://openrouter.ai/api/v1/chat/completions"

@Injectable({
  providedIn: 'root'
})
export class AiService {

  constructor(public http: HttpClient) { }

  async sendMessage(message: string): Promise<string> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${apikey}`,
      'HTTP-Referer': 'https://localhost:4200',
      'X-Title': 'EduMeilleur',
      'Content-Type': 'application/json'
    });

    const body = {
      model: 'deepseek/deepseek-r1-0528:free',
      messages: [
        {
          role: 'user',
          content: message
        }
      ]
    };

    let x = await lastValueFrom(this.http.post<any>(url, body, { headers }));
    console.log(x);
    

    return x?.choices?.[0]?.message?.content || 'No response.';
  }
}
