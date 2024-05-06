import { Component } from '@angular/core';
import { faTicket } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-movie-page',
  templateUrl: './movie-page.component.html',
  styleUrls: ['./movie-page.component.css']
})
export class MoviePageComponent {
  faTicket = faTicket;
  toggleDetail: boolean = false;
  toggleBody: Boolean = false;



toggleDetails(): void {
  this.toggleDetail = !this.toggleDetail;
}

toggleBodys(): void {
  this.toggleBody = !this.toggleBody;
}
}
