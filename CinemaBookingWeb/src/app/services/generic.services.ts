import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient} from '@angular/common/http';
import { Observable, catchError, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenericService<Tentity> {

  url : string = environment.apiUrl
  constructor(private http : HttpClient) { 

  }

  getAll(endpoint: string): Observable<Tentity[]> {
    return this.http.get<Tentity[]>(`${environment.apiUrl}${endpoint}`);
  }

  save(endpoint: string, data: Tentity): Observable<Tentity> {
    return this.http.post<Tentity>(`${environment.apiUrl}${endpoint}`, data);
  }

  delete(endpoint: string, id: number): Observable<boolean> {
    return this.http.delete(`${environment.apiUrl}${endpoint}/${id}`).pipe(
      map(() => true)
    );
}

 }
