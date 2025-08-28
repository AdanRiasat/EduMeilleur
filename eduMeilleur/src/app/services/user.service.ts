import { HttpClient, HttpHeaders } from '@angular/common/http';
import { computed, Injectable, Signal, signal, WritableSignal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Profile } from '../models/profile';
import { environment } from '../../environments/environment';
import {jwtDecode} from 'jwt-decode'

// TODO Refactor function for DRY code

const domain: string = environment.apiUrl

interface DecodedToken {
  exp: number; 
  [key: string]: any; 
}

@Injectable({
  providedIn: 'root'
})
export class UserService { 

  private tokenSignal: WritableSignal<string | null> = signal(localStorage.getItem("token"))
  private refreshTokenSignal: WritableSignal<string | null> = signal(localStorage.getItem("refreshTokenSignal"))
  private rolesSignal: WritableSignal<string[]> = signal(JSON.parse(localStorage.getItem("roles") || "[]"))

  token: Signal<string | null> = this.tokenSignal.asReadonly()
  refreshToken: Signal<string | null> = this.refreshTokenSignal.asReadonly()
  roles: Signal<string[]> = this.rolesSignal.asReadonly()

  private logoutTimer: any

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

    this.updateSignals(x)
  }

  async login(username: string, password: string){
    let dto = {
      username : username,
      password : password
    }

    let x = await lastValueFrom(this.http.post<any>(domain + "/api/Users/Login", dto))
    console.log(x);

    this.updateSignals(x)
  }

  async logout(){
    this.tokenSignal.set(null)
    this.rolesSignal.set([])
    this.refreshTokenSignal.set(null)
    localStorage.removeItem("token")
    localStorage.removeItem("refreshToken")
    localStorage.removeItem("profile")
    localStorage.removeItem("roles")
  }

  async editProfile(dto: FormData){    
    let x = await lastValueFrom(this.http.put<Profile>(domain + "/api/Users/EditProfile", dto))
    console.log(x);
    localStorage.setItem("profile",JSON.stringify(x))
  }

  async refreshExpiredToken(){
    console.log(this.refreshTokenSignal());
    
    let dto = {
      refreshToken : this.refreshTokenSignal()
    }

    let x = await lastValueFrom(this.http.post<any>(domain + "/api/Users/RefreshExpiredToken", dto))
    console.log(x);

    this.updateSignals(x)
  }

  updateSignals(data: any){
    this.tokenSignal.set(data.token)
    this.refreshTokenSignal.set(data.refreshToken)
    this.rolesSignal.set(data.roles)
    localStorage.setItem("roles", JSON.stringify(data.roles))
    localStorage.setItem("refreshToken", data.refreshToken)
    localStorage.setItem("token", data.token)
    localStorage.setItem("profile",JSON.stringify(data.profile))

    this.startLogoutTimer()
  }

  isTokenExpired(): boolean {
    let token = this.tokenSignal();
    if (!token) return true;

    try {
      let decoded = jwtDecode<DecodedToken>(token);
      let now = Math.floor(Date.now() / 1000); // current time in seconds
      return decoded.exp < now;
    } catch (e) {
      return true; // invalid token => treat as expired
    }
  }

  private startLogoutTimer() {
    try {
      if (this.logoutTimer) clearTimeout(this.logoutTimer);

      let decoded = jwtDecode<DecodedToken>(this.tokenSignal()!!);
      let now = Math.floor(Date.now() / 1000);
      let expiresIn = decoded.exp - now;

      let refreshThreshold = (expiresIn - 30) * 1000

      this.logoutTimer = setTimeout(async () => {
        try{
          await this.refreshExpiredToken()

          this.startLogoutTimer()
        } catch (error: any){
          this.logout();
          alert("hmmmm something aint right")
        }
      }, refreshThreshold);
    } catch {
      this.logout();
    }
  }
}
