import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
//   styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    isLoggedIn = false;
  
    constructor(private authService: AuthService) {}
  
    ngOnInit() {
      this.authService.currentUser.subscribe(user => {
        this.isLoggedIn = user != null;
      });
      if (!this.isLoggedIn) {
        console.log("user is not logged in, from app.component.ts")
      }
    }
  }