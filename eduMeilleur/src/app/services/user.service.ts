import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

const domain: string ="https://localhost:7027"

@Injectable({
  providedIn: 'root'
})
export class UserService { 

  constructor(public http: HttpClient) { }

  async register(username: string, email: string, password: string, school: string, schoolYear: string){
    let dto = {
      username: username,
      email: email,
      password: password,
      school: school,
      schoolYear: schoolYear
    }

    let x = await lastValueFrom(this.http.post<any>(domain + "/api/Users/Register", dto))
    console.log(x);
  }

  async login(username: string, password: string){
    let dto = {
      username : username,
      password : password
    }

    let x = await lastValueFrom(this.http.post<any>(domain + "/api/Users/Login", dto))
    console.log(x);

    localStorage.setItem("token", x.token)
    localStorage.setItem("profile",JSON.stringify(x.profile))
  }
}
