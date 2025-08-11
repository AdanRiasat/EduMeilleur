import { HttpClient, HttpHeaders } from '@angular/common/http';
import { computed, Injectable, Signal, signal, WritableSignal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Profile } from '../models/profile';
import { environment } from '../../environments/environment';


const domain: string = environment.apiUrl

@Injectable({
  providedIn: 'root'
})
export class UserService { 

  private tokenSignal: WritableSignal<string | null> = signal(localStorage.getItem("token"))
  private rolesSignal: WritableSignal<string[]> = signal(JSON.parse(localStorage.getItem("roles") || "[]"))

  token: Signal<string | null> = this.tokenSignal.asReadonly()
  roles: Signal<string[]> = this.rolesSignal.asReadonly()


  constructor(public http: HttpClient) { }

  async getTeachers(){
    let x = await lastValueFrom(this.http.get<string[]>(domain + "/api/Users/GetTeachers"))
    console.log(x);
    
    return(x)
  }

  async register(username: string, email: string, password: string, schoolId: number, schoolYear: number){
    let dto = {
      username: username,
      email: email,
      password: password,
      schoolId: schoolId,
      schoolYear: schoolYear
    }

    let x = await lastValueFrom(this.http.post<any>(domain + "/api/Users/Register", dto))
    console.log(x);

    this.tokenSignal.set(x.value.token)
    this.rolesSignal.set(x.value.roles)
    localStorage.setItem("roles", JSON.stringify(x.value.roles))
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
    this.rolesSignal.set(x.value.roles)
    localStorage.setItem("roles", JSON.stringify(x.value.roles))
    localStorage.setItem("token", x.value.token)
    localStorage.setItem("profile",JSON.stringify(x.value.profile))
  }

  async logout(){
    this.tokenSignal.set(null)
    this.rolesSignal.set([])
    localStorage.removeItem("token")
    localStorage.removeItem("profile")
    localStorage.removeItem("roles")
  }

  async editProfile(dto: FormData){    
    let x = await lastValueFrom(this.http.put<Profile>(domain + "/api/Users/EditProfile", dto))
    console.log(x);
    localStorage.setItem("profile",JSON.stringify(x))
  }
}
