import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { DisplaySujet } from '../models/displaySujet';
import { Item } from '../models/Item';
import { SpinnerService } from './spinner.service';
import { environment } from '../../environments/environment';
import { MarkdownService } from './markdown.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

const domain: string = environment.apiUrl
console.log(domain);


@Injectable({
  providedIn: 'root'
})
export class SujetService {

  selectedContent = signal<SafeHtml>("")
  currentItem = signal<Item | null>(null)

  constructor(public http: HttpClient, public spinner: SpinnerService, public mdService: MarkdownService, public sanitizer: DomSanitizer) { }

  async getSujets(): Promise<DisplaySujet[]>{
    this.spinner.show()

    let x = await lastValueFrom(this.http.get<DisplaySujet[]>(domain + "/api/Subjects/GetSubjects"))
    console.log(x);

    this.spinner.hide()

    return x
  }

  async getSujet(id: number): Promise<DisplaySujet>{
    let x = await lastValueFrom(this.http.get<DisplaySujet>(domain + "/api/Subjects/GetSubject/" + id))
    console.log(x);

    return x
  } 

  async getAllItems(id: number, type: string): Promise<Item[]>{
    let x = await lastValueFrom(this.http.get<Item[]>(`${domain}/api/${type}/GetAll${type}/${id}`))
    console.log(x);

    return x
  }

  async getItem(id: number, type: string): Promise<Item>{
    let x = await lastValueFrom(this.http.get<Item>(`${domain}/api/${type}/Get${type}/${id}`))
    console.log(x);

    this.currentItem.set(x)
    return x
  }

  formatMessage(message: string) {
    let cleanMessage = message.replace(/\\r\\n/g, "\n");
    let rawHtml: string = this.mdService.parse(cleanMessage);

    this.selectedContent.set(this.sanitizer.bypassSecurityTrustHtml(rawHtml))
  }
}
