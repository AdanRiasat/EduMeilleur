import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

const domain: string ="https://localhost:7027"

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  constructor(public http: HttpClient) { }

  async postQuestion(title: string, message: string){
    let dto = {
      title: title,
      message: message
    }

    let x = await lastValueFrom(this.http.post<any>(domain + "/api/Questions/PostQuestionTeacher", dto))
    console.log(x);
  }
}
