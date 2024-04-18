import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
//   styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    isLoggedIn = false;
  
    constructor(private authService: AuthService, private router: Router) {}

    ngOnInit() {
      this.authService.currentUser.subscribe(user => {
        this.isLoggedIn = user != null;
      });
      const city = localStorage.getItem('selectedCity');
      if (!city) {
        console.log('No city selected, redirecting to select-city');
        this.router.navigate(['/areapick']);
      }
      if (!this.isLoggedIn) {
        this.authService.login('guest', 'password').subscribe(
          () => {
            // Redirect to frontpage
            this.router.navigate(['']);
          },
          (error) => {
            console.log('Login failed:', error);
          }
        );
      }
    }
  }