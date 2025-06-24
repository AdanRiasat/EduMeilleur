import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { last, lastValueFrom } from 'rxjs';

const domain: string ="https://localhost:7027"

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  constructor(public http: HttpClient) { }

  async postQuestion(dto: FormData){
    let x = await lastValueFrom(this.http.post<any>(domain + "/api/Questions/PostQuestionTeacher", dto))
    console.log(x);
  }

  async postFeedback(dto: FormData){
    let x = await lastValueFrom(this.http.post<any>(domain + "/api/Questions/PostFeedback", dto))
    console.log(x);
  }
}
