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
movieForm!: FormGroup;
movieList: Movie[] = [];
genreList: Genre[] =[];
currentMovie: Movie | null = null;
isEditMode: boolean = false;
showForm: boolean = false;
showList: boolean = true;

initForm(movie?: Movie): void {
  this.movieForm = new FormGroup({
    movieID: new FormControl(movie ? movie.movieID : ''),
    title: new FormControl(movie ? movie.title : '', Validators.required),
    duration: new FormControl(movie ? movie.duration : '', Validators.required),
    director: new FormControl(movie ? movie.director : '', Validators.required),
    movieLink: new FormControl(movie ? movie.movieLink : '', Validators.required),
    genre: new FormGroup({
      genreID: new FormControl(movie && movie.genres && movie.genres.length > 0 ? movie.genres[0].genreID : ''),
      genreName: new FormControl(movie && movie.genres && movie.genres.length > 0 ? movie.genres[0].genreName : '')
    })
  });
}

constructor(private movieService: GenericService<Movie>, private genreSerice: GenericService<Genre>) {
  this.initForm();
}
  ngOnInit() {
    this.fetchMovies();
    this.initForm();
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

  editMovie(movie: Movie): void {
    this.currentMovie = movie;
    this.isEditMode = true;
    this.showForm = true;
    this.showList = false;
    this.initForm(movie);
  }

  resetForm(): void {
    this.movieForm.reset();
    this.showForm = false;
    this.showList = true;
    this.isEditMode = false;
    this.currentMovie = null;
  }

  toggleSave(): void {
    this.isEditMode = false;
    this.currentMovie = null;
    this.initForm();
    this.showForm = !this.showForm;
    this.showList = !this.showForm;
  }

  public save(): void {
    if (this.movieForm.valid) {
      const formdata = this.movieForm.value;
      const selectedGenre = this.genreList.find(genre => genre.genreID == formdata.genre.genreID);
      const movieData = {
        movieId: formdata.movieId,
        title: formdata.title,
        duration: parseInt(formdata.duration, 10),
        director: formdata.director,
        movieLink: formdata.movieLink,
        genres: [{
          genreID: selectedGenre?.genreID,
          genreName: selectedGenre?.genreName
        }]
      };
      if (this.isEditMode) {
        this.movieService.update('movies', movieData, movieData.movieId).subscribe({
          next: (response) => {
            console.log('Movie updated:', response);
            this.resetForm();
            this.fetchMovies();
          },
          error: (error) => {
            console.error('Failed to update movie:', error);
            alert(`Failed to update movie: ${error.error.title}`);
          }
        });
      }
         else
         {
          if (selectedGenre) {
            this.movieService.create('movie/Complex', movieData).subscribe({
              next: (response) => {
                console.log('Complex movie saved:', response);
                this.movieList.push(response);
                this.movieForm.reset();
                this.showForm = false;
                this.showList = true;
              },
              error: (error) => {
                console.error('Failed to create movie:', error);
                alert(`Failed to create movie: ${error.error.title}`);
              }
            });
        } else {
          alert(`It's not valid data in the form`);
        }
    }
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