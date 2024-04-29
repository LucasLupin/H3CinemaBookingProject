import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Router } from '@angular/router';

interface Area {
  areaID: number;
  areaName: string;
}

const httpOptions = {
  headers: new HttpHeaders({
    'content-type': 'application/json'
  })
};


@Injectable({
  providedIn: 'root'
})
export class GenericService<Tentity> {

  url : string = environment.apiUrl
  constructor(private http : HttpClient) { 

  }

  getAll(endpoint: string): Observable<Tentity[]> {
    return this.http.get<Tentity[]>(`${environment.apiUrl}${endpoint}`, httpOptions);
  }

  create(endpoint: string, data: Tentity): Observable<Tentity> {
    console.log("Data in Service: ",data);
    console.log("endoint: ", `${environment.apiUrl}${endpoint}`);
    
    return this.http.post<Tentity>(`${environment.apiUrl}${endpoint}`, data, httpOptions);
  }

  delete(endpoint: string, id: number): Observable<boolean> {
    return this.http.delete(`${environment.apiUrl}${endpoint}/${id}`, httpOptions).pipe(
      map(() => true)
    );
  }

  exists(endpoint: string, id: number): Observable<Area | boolean> { 
    return this.http.get<Area | boolean>(`${environment.apiUrl}${endpoint}/${id}`);
  }
  

}

@Injectable({
  providedIn: 'root'
})
export class LocalStorageGeneric {
  constructor(private service: GenericService<any>, private router: Router) {
  }
  // handleLocalStorage(): void {
  //   const selectedArea = this.getItem('selectedArea');
  //   console.log("Localstorage selectedArea from generic service: ", selectedArea);
  //   if (selectedArea) {
  //     try {
  //       if (typeof selectedArea === 'object' && 'name' in selectedArea && 'id' in selectedArea) {
  //         this.service.exists('Region', selectedArea.id)
  //         .toPromise()
  //         .then((result: { areaID: number; areaName: string } | boolean | undefined) => {
  //           if (result === null || typeof result === 'undefined' || typeof result === 'boolean') {
  //             console.log("Handling the 404 scenario.");
  //             this.removeItem('selectedArea');
  //             this.router.navigate(['/areapick']);
  //           } else {
  //             console.log('Area result from generic service:', result);
  //             const formattedResult = {
  //               id: result.areaID,
  //               name: result.areaName
  //             };
  //             console.log("formattedResult", formattedResult)
  //             this.setItem('selectedArea', formattedResult);
  //             this.router.navigate(['/']);
  //           }
  //         })
  //         .catch((error: any) => {
  //           if (error.status === 404) {
  //             console.log("Selected area was not found, redirecting to areapick.")
  //             this.removeItem('selectedArea');
  //             this.router.navigate(['/areapick']);
  //           }
  //           else {
  //             console.error("An error occurred:", error);
  //           }
  //         });
         

  //       } else {
  //         console.error("Data format is incorrect:", selectedArea);
  //         this.removeItem('selectedArea');
  //         this.router.navigate(['/areapick']);
  //       }
  //     } catch (e) {
  //       console.error("Failed to parse JSON:", e);
  //     }
  //   } else {
  //     console.warn("No data found for 'selectedArea'");
  //     console.log('No area selected, redirecting to areapick');
  //     this.router.navigate(['/areapick']);
  //   }
  // }

  handleLocalStorage(): void {
    const selectedArea = this.getItem('selectedArea');
    console.log("Localstorage selectedArea from generic service:", selectedArea);
    
    if (selectedArea) {
      if (typeof selectedArea === 'object' && 'name' in selectedArea && 'id' in selectedArea) {
        this.checkAreaExists(selectedArea.id);
      } else {
        console.error("Data format is incorrect:", selectedArea);
        this.removeItem('selectedArea');
        this.router.navigate(['/areapick']);
      }
    } else {
      console.warn("No data found for 'selectedArea'");
      this.router.navigate(['/areapick']);
    }
  }
  
  async checkAreaExists(areaID: number): Promise<void> {
    try {
      const result: Area | boolean | undefined = await this.service.exists('Area', areaID).toPromise();
      if (result === null || result === undefined || typeof result === 'boolean') {
        console.log("Handling the 404 scenario.");
        this.removeItem('selectedArea');
        this.router.navigate(['/areapick']);
      } else {
        const formattedResult = {
          id: result.areaID,
          name: result.areaName
        };
        console.log("formattedResult:", formattedResult);
        this.setItem('selectedArea', formattedResult);
        this.router.navigate(['/']);
      }
    } catch (error) {
      this.removeItem('selectedArea');
      this.router.navigate(['/areapick']);
      console.error("An error occurred:", error);
    }
  }
  
  

  handleStorageChange(event: StorageEvent): void {
    console.log("LocalStorage change detected:", event)
    if (event.key === 'selectedArea') {
      console.log('LocalStorage change detected:', event);
    }
  }

  setItem(key: string, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }

  getItem(key: string): any {
    const item = localStorage.getItem(key);
    return item ? JSON.parse(item) : null;
  }

  removeItem(key: string): void {
    localStorage.removeItem(key);
  }

  clear(): void {
    localStorage.clear();
  }
}