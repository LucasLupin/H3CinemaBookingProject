import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Movie } from 'src/app/models/movie/movie';
import { GenericService } from 'src/app/services/generic.services';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent {

  movieForm: FormGroup = new FormGroup({
    movieID: new FormControl(''),
    title: new FormControl(''),
    duration: new FormControl(''),
    director: new FormControl(''),
    genre: new FormGroup({
      genreID: new FormControl(''),
      genreName: new FormControl(''),
    })
  })

  constructor(private genericService: GenericService<Movie>) {}

  public create(): void {
    if (this.movieForm.valid) {
      this.genericService.create('movies', this.movieForm.value).subscribe({
        next: (response) => {
          console.log('Movie saved:', response);
          this.movieForm.reset();
        }
    })
  }
}}