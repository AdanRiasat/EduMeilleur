import { HttpClient } from '@angular/common/http';
import { computed, Injectable, Signal, signal, WritableSignal } from '@angular/core';
import { lastValueFrom } from 'rxjs';

const domain: string ="https://localhost:7027"

@Injectable({
  providedIn: 'root'
})
export class UserService { 

  private tokenSignal: WritableSignal<string | null> = signal(null)
  token: Signal<string | null> = this.tokenSignal.asReadonly()

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

    this.tokenSignal.set(x.value.token)
    localStorage.setItem("token", x.value.token)
    localStorage.setItem("profile",JSON.stringify(x.value.profile))
  }

  async login(username: string, password: string){
    let dto = {
      username : username,
      password : password
    }

    let x = await lastValueFrom(this.http.post<any>(domain + "/api/Users/Login", dto))
    console.log(x);

    this.tokenSignal.set(x.value.token)
    localStorage.setItem("token", x.value.token)
    localStorage.setItem("profile",JSON.stringify(x.value.profile))
  }

  async logout(){
    this.tokenSignal.set(null)
    localStorage.removeItem("token")
    localStorage.removeItem("profile")
  }
}
