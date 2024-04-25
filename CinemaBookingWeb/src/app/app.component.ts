import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';
import { LocalStorageGeneric } from './services/generic.services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
//   styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    isLoggedIn = false;
  
    constructor(private authService: AuthService, private router: Router, private storageService : LocalStorageGeneric) {}

    ngOnInit() {
      this.storageService.handleLocalStorage();
      console.log('App component initialized');
      // this.authService.currentUser.subscribe(user => {
      //   this.isLoggedIn = user != null;
      // });
      // this.router.navigate(['']);
      // if (!this.isLoggedIn) {
      //   this.authService.login('guest', 'password').subscribe(
      //     () => {
      //       // Redirect to frontpage
      //       this.router.navigate(['']);
      //     },
      //     (error) => {
      //       console.log('Login failed:', error);
      //     }
      //   );
      // }
    }
  }