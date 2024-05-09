import { Component } from '@angular/core';
import { BookShow } from 'src/app/models/BookShow/bookshow';
import { BookShowService } from 'src/app/services/bookshow.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent {
  bookShow: BookShow = {};
  //Auth Global Variables
  isAuthenticated!: boolean;
  userId!: string | null;
  userRole!: string | null;
  email!: string | null;
  name!: string | null;

  totalPrice: number = 0;

  constructor(private bookShowService: BookShowService, private router: Router, private authService: AuthService) {
    this.bookShowService.currentBookShow.subscribe(bookShow => {
      if (bookShow) {
        this.bookShow = bookShow;
        console.log("BookShow in Payment: ", this.bookShow);

      }
      else {
        console.log("No BookShow in Payment");
        this.router.navigate(['404']);
      }
    });
    this.isAuthenticated = this.authService.isAuthenticated();
    this.userId = this.authService.getUserId();
    this.userRole = this.authService.getUserRole();
    this.email = this.authService.getEmail();
    this.name = this.authService.getName();
  }

  ngOnInit() {
    this.reserveSeats();
    this.calculateTotalPrice();
  }

  reserveSeats() {
    //Call generic service to reserve each selected seat

  }

  calculateTotalPrice() {
    if (this.bookShow && this.bookShow.selectedSeats && this.bookShow.price) {
      this.totalPrice = this.bookShow.selectedSeats.length * this.bookShow.price;
    }
  }
}
