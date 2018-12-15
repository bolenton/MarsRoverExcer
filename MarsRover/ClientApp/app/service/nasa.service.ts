import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Rover } from '../model/Rover';
import { Photo, DefaultPhoto } from '../model/Photo';
import { Observable } from 'rxjs';
import { DOCUMENT } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class NasaService {
    rover: Rover[];
    baseUrl: string = "http://localhost:8841";

    constructor(private httpClient: HttpClient, @Inject(DOCUMENT) private document: Document) {
        this.baseUrl = this.document.location.href + "api/nasa/";
        console.log(this.baseUrl);
    }

    getRovers(): Observable<Rover[]> {
        let url = `${this.baseUrl}rovers`;
        return this.httpClient.get<Rover[]>(url);
    }

    getPhotos(rover: string, earthDate: string) {
        let url = `${this.baseUrl}photos?rover=${rover}&earthDate=${earthDate}`;
        return this.httpClient.get<Photo[]>(url);
    }

    getDefaultData(rover: string): Observable<DefaultPhoto> {
        let url = `${this.baseUrl}defaultDate?rover=${rover}`;
        return this.httpClient.get<DefaultPhoto>(url);
    }

    
}
