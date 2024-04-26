import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Movie } from 'src/app/models/movie/movie';
import { GenericService } from 'src/app/services/generic.services';

@Component({
  selector: 'app-admin-movie',
  templateUrl: './adminmovie.component.html',
  styleUrls: ['./adminmovie.component.css']
})
export class AdminMovieComponent {
movieList: Movie[] = [];

movieForm: FormGroup = new FormGroup({
  movieID: new FormControl(''),
  title: new FormControl(''),
  duration: new FormControl(''),
  director: new FormControl(''),
  genre: new FormGroup({
    genreID: new FormControl(''),
    genreName: new FormControl(''),
  })
});

constructor(private genericService: GenericService<Movie>) {}

  ngOnInit() {
    this.fetchMovies();
  }

  fetchMovies() {
    this.genericService.getAll("movie").subscribe(data => {
      this.movieList = data;
    });
  }

  public create(): void {
    if (this.movieForm.valid) {
      this.genericService.create('movie', this.movieForm.value).subscribe({
        next: (response) => { 
          console.log('Movie saved:', response);
          this.movieList.push(response); // Assuming response is the newly created movie
          this.movieForm.reset();
        }
      });
    }
  }

  deleteMovie(movie: Movie) {
    if (movie && movie.movieID !== undefined) {
        this.genericService.delete('movie', movie.movieID).subscribe(() => {
            this.fetchMovies();
        });
    }
  }
}
