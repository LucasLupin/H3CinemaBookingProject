import { Component } from '@angular/core';
import { Movie } from 'src/app/models/movie/movie';

@Component({
  selector: 'appadminfrontpage',
  templateUrl: './adminFrontpage.component.html',
  styleUrls: ['./adminFrontPage.component.css']
})
export class AdminFrontPageComponent {
  movies: Movie[] = [];
}
