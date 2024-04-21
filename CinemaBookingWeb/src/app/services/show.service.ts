import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Show } from '../models/show/show';

@Injectable({
  providedIn: 'root'
})
export class ShowService {

  url:string = "https://localhost:7092/api/Show"
  constructor(private http: HttpClient) { }

  getAll(): Observable<Show[]> {
    return this.http.get<Show[]>(this.url);
  }
}
