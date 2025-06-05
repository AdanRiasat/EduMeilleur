import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { last, lastValueFrom } from 'rxjs';

const domain: string ="https://localhost:7027"

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  constructor(public http: HttpClient) { }

  async postQuestion(title: string, message: string){
    let token = localStorage.getItem("token");
    let httpOptions = {
        headers : new HttpHeaders({
        'Authorization' : `Bearer ${token}`
        })
    };

    let dto = {
      title: title,
      message: message
    }

    let x = await lastValueFrom(this.http.post<any>(domain + "/api/Questions/PostQuestionTeacher", dto, httpOptions))
    console.log(x);
  }
}
