import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenericService<Tentity> {

  url : string = environment.apiUrl
  constructor(private http : HttpClient) { 

  }

  getAll(type:string): Observable<Tentity[]>{
    return this.http.get<Tentity[]>(this.url + type)
  }

  delete(type:string, entityToDelete:number): boolean {
    this.http.delete(this.url + type + '/' + entityToDelete)
    return true;
  }
}
