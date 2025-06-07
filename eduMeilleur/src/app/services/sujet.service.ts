import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { DisplaySujet } from '../models/displaySujet';
import { Item } from '../models/Item';

const domain: string ="https://localhost:7027"

@Injectable({
  providedIn: 'root'
})
export class SujetService {

  constructor(public http: HttpClient) { }

  async getSujets(): Promise<DisplaySujet[]>{
    let x = await lastValueFrom(this.http.get<DisplaySujet[]>(domain + "/api/Subjects/GetSubjects"))
    console.log(x);
    
    return x
  }

  async getSujet(id: number): Promise<DisplaySujet>{
    let x = await lastValueFrom(this.http.get<DisplaySujet>(domain + "/api/Subjects/GetSubject/" + id))
    console.log(x);

    return x
  } 

  async getAllItems(id: number, type: string): Promise<Item[]>{
    let x = await lastValueFrom(this.http.get<Item[]>(domain + "/api/" + type + "/GetAll" + type + "/" + id))
    console.log(x);

    return x
  }

  async getItem(id: number, type: string): Promise<Item>{
    let x = await lastValueFrom(this.http.get<Item>(domain + "/api/" + type + "/Get" + type + "/" + id))
    console.log(x);
    return x
  }
}
