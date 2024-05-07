import { Component } from '@angular/core';
import { faCouch } from '@fortawesome/free-solid-svg-icons';
import { faWheelchairMove } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent {
  faCouch = faCouch;
  faWheelchairMove = faWheelchairMove;
}
