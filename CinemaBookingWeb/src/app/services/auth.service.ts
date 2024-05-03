import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authToken: string | null = null;
  private userRole: string | null = null;
  private name: string | null = null;
  private email: string | null = null;
  private jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient, private router: Router) {
    // Initialize from local storage if available
    const token = localStorage.getItem('authToken');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      this.authToken = token;
      this.decodeToken(token);
    } else {
      this.logout();  // Clean up if token is invalid
    }
  }

  login(email: string, password: string): Observable<boolean> {
    return this.http.post(`${environment.apiUrl}UserDetail/login`, { email, password }, { responseType: 'text' }).pipe(
      tap(token => this.storeAuthToken(token)),
      map(token => !!token),
      catchError(error => {
        console.error(error);
        return throwError(() => new Error(error.error || 'Login failed'));
      })
    );
  }

  signup(name: string, email: string, phoneNumber: string, password: string): Observable<boolean> {
    return this.http.post(`${environment.apiUrl}UserDetail/register`, { name, email, phoneNumber, password }, { responseType: 'text' }).pipe(
      tap(token => this.storeAuthToken(token)),
      map(token => !!token),
      catchError(error => {
        console.error('Signup failed:', error);
        return throwError(() => new Error( error.error || 'Signup failed'));
      })
    );
  }

  storeAuthToken(token: string): void {
    this.authToken = token;
    localStorage.setItem('authToken', token);
    this.decodeToken(token);
  }

  decodeToken(token: string): void {
    const decodedToken = this.jwtHelper.decodeToken(token);

    // Define the keys for the claims
    const emailKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    const nameKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    const roleKey = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

    // Extract the claims
    this.name = decodedToken[nameKey] || '';
    this.userRole = decodedToken[roleKey] || '';
    this.email = decodedToken[emailKey] || '';

    console.log("Decoded name", this.name);
    console.log("Decoded role", this.userRole);
    console.log("Decoded email", this.email);
}


  logout(): void {
    this.authToken = null;
    this.userRole = null;
    this.name = null;
    this.email = null;
    localStorage.removeItem('authToken');
    // this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return this.authToken != null && !this.jwtHelper.isTokenExpired(this.authToken);
  }

  getAuthToken(): string | null {
    return this.authToken;
  }

  getUserRole(): string | null {
    return this.userRole;
  }

  getName(): string | null {
    return this.name;
  }

  getEmail(): string | null {
    return this.email;
  }
}
