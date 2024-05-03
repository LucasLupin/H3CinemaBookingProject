import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, map } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { AuthService } from './auth.service';





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

  private isLoggedIn = false; // Sæt denne til true ved login
  private userRole!: string;
  private username!: string;
  private authToken!: string;

  url : string = environment.apiUrl
  constructor(private http : HttpClient, private router: Router) { 

  }

  getAll(endpoint: string): Observable<Tentity[]> {
    return this.http.get<Tentity[]>(`${environment.apiUrl}${endpoint}`, httpOptions);
  }

  create(endpoint: string, data: Tentity): Observable<Tentity> {
    console.log("Data in Service: ",data);
    console.log("endoint: ", `${environment.apiUrl}${endpoint}`);
    
    return this.http.post<Tentity>(`${environment.apiUrl}${endpoint}`, data, httpOptions);
  }

  createBulk(endpoint: string, data: Tentity[]): Observable<any> {
    return this.http.post<Tentity[]>(`${environment.apiUrl}${endpoint}/bulk`, data, httpOptions)
  }

  update(endpoint: string, data: Tentity, id:number): Observable<Tentity> {
    if (id === undefined) {
      throw new Error("Cannot update entity without an ID.");
    }
    console.log("id", id);
    console.log("Data: ", data);
    
    return this.http.put<Tentity>(`${environment.apiUrl}${endpoint}/${id}`, data, httpOptions)
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

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authSevice: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authToken = this.authSevice.getAuthToken();
    if (authToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${authToken}`
        }
      });
    }
    return next.handle(request);
  }
}

// @Injectable({
//   providedIn: 'root'
// })
// export class UserService {
//   private authToken!: string;
//   private userRole!: string;
//   private email!: string;
//   private name!: string;

//   constructor(private http: HttpClient, private router: Router, private jwtHelper: JwtHelperService) {}

//   login(email: string, password: string): Observable<boolean> {
//     return this.http.post(`${environment.apiUrl}UserDetail/login`, { email, password }, { responseType: 'text' }).pipe(
//       tap(response => this.storeAuthToken(response)),
//       map(response => !!response),
//       catchError(error => {
//         console.error('Login failed:', error);
//         return throwError(() => new Error('Login failed'));
//       })
//     );
//   }

//   getAuthToken(): string | null {
//     if (this.authToken) {
//       return this.authToken;
//     }

//     const data = localStorage.getItem('authToken');
//     if (data) {
//       const convertedData = JSON.parse(data);
//       this.authToken = convertedData;
//       return this.authToken;
//     }

//     return null;
//   }

//   validateToken(authToken: string): Observable<UserDetails | null> {
//     return this.http.get<UserDetails>(`${environment.apiUrl}/check-token`, {
//       headers: new HttpHeaders({
//         'Authorization': `Bearer ${authToken}`
//       }),
//       observe: 'response'
//     }).pipe(
//       map(response => {
//         if (response.status === 200 && response.body) {
//           this.authToken = response.body.authToken;
//           this.userRole = response.body.role;
//           this.email = response.body.email;
//           localStorage.setItem('authToken', JSON.stringify(response.body));
//           return response.body;
//         }
//         return null;
//       }),
//       catchError(error => {
//         console.error('Token validation failed:', error.message);
//         this.logout();
//         return of(null);
//       })
//     );
//   }

//   logout(): void {
//     localStorage.removeItem('authToken');
//     this.router.navigate(['/login']);
//   }

//   storeAuthToken(token: string): void {
//     this.authToken = token;
//     localStorage.setItem('authToken', token);

//     // Decode the token to extract user information
//     const decodedToken = this.jwtHelper.decodeToken(token);
//     this.email = decodedToken?.email || '';
//     this.name = decodedToken?.name || '';
//     this.userRole = decodedToken?.role || '';
//     console.log('User role:', this.userRole);
//     console.log('Name:', this.name);
//     console.log('Email:', this.email);
//   }
// }