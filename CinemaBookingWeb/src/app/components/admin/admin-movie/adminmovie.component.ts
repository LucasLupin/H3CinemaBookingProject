import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Genre } from 'src/app/models/genre/genre';
import { Movie } from 'src/app/models/movie/movie';
import { GenericService } from 'src/app/services/generic.services';

@Component({
  selector: 'app-admin-movie',
  templateUrl: './adminmovie.component.html',
  styleUrls: ['./adminmovie.component.css']
})
export class AdminMovieComponent {
movieList: Movie[] = [];
genreList: Genre[] =[];

movieForm: FormGroup = new FormGroup({
  movieID: new FormControl(''),
  title: new FormControl('',  Validators.required),
  duration: new FormControl('', Validators.required),
  director: new FormControl('', Validators.required),
  movieLink: new FormControl('', [Validators.required, Validators.minLength(5)]),
  genre: new FormGroup({
    genreID: new FormControl(''),
    genreName: new FormControl(''),
  })
});

constructor(private movieService: GenericService<Movie>, private genreSerice: GenericService<Genre>) {}

  ngOnInit() {
    this.fetchMovies();
  }

  fetchMovies() {
    this.movieService.getAll("movie").subscribe(data => {
      this.movieList = data;
    });

    this.genreSerice.getAll("Genre").subscribe(data => {
      this.genreList = data;
      console.log(this.genreList);
      
    });
  }

  public create(): void {
    if (this.movieForm.valid) {
      const formValue = this.movieForm.value;
      console.log("FormValue: ", formValue);
  
      const selectedGenre = this.genreList.find(genre => genre.genreID == formValue.genre.genreID);
      console.log("Genre: ", selectedGenre);
      
      if (selectedGenre) {
  
        const movieData = {
          title: formValue.title,
          duration: parseInt(formValue.duration, 10),
          director: formValue.director,
          movieLink: formValue.movieLink,
          genres: [{
            genreID: selectedGenre.genreID,
            genreName: selectedGenre.genreName
          }]
        };

        console.log("Data in the Component movie: ", movieData);
        
        this.movieService.create('movie/Complex', movieData).subscribe({
          next: (response) => {
            console.log('Complex movie saved:', response);
            this.movieList.push(response);
            this.movieForm.reset();
          },
          error: (error) => {
            console.error('Failed to create movie:', error);
            alert(`Failed to create movie: ${error.error.title}`);
          }
        });
      }
    }
    else {
      alert(`It's not valid data in the form`);
    }
  }
  
  deleteMovie(movie: Movie) {
    if (movie && movie.movieID !== undefined) {
        this.movieService.delete('movie', movie.movieID).subscribe(() => {
            this.fetchMovies();
        });
    }
  }
}
