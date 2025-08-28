import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {

    teacherEmail: string = "edumeilleur@gmail.com"
    teacherNumber: string = "514-653-0788"
    adminEmail: string  = "edumeilleur@gmail.com"
    adminNumber: string = "438-925-3926"
    pfpSrc: string = environment.apiUrl + "/api/Users/GetProfilePicture/"

  constructor() { }
}
