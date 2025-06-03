import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { DisplaySujet } from '../models/displaySujet';
import { Notes } from '../models/notes';

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

  async getAllNotes(id: number): Promise<Notes[]>{
    let x = await lastValueFrom(this.http.get<Notes[]>(domain + "/api/Notes/GetAllNotes/" + id))
    console.log(x);

    return x
  }

  async getNotes(id: number): Promise<Notes>{
    let x = await lastValueFrom(this.http.get<Notes>(domain + "/api/Notes/GetNotes/" + id))
    console.log(x);

    return x
  }
}
