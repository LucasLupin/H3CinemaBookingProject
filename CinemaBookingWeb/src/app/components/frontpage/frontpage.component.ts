import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Cinema } from 'src/app/models/cinema/cinema';
import { Movie } from 'src/app/models/movie/movie';
import { Show } from 'src/app/models/show/show';
import { CinemaService } from 'src/app/services/cinema.service';
import { MovieService } from 'src/app/services/movie.service';
import { ShowService } from 'src/app/services/show.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent {

  constructor(private router: Router, private movieService: MovieService, private showService: ShowService, private cinemaService: CinemaService) {}

  cinemaList : Cinema[] = [];
  movieList : Movie[] = [];
  showList : Show[] = [];
  chosenCity: string = '';
  showCinemaList: boolean = false;



  ngOnInit() {

    this.movieService.getAll().subscribe(data => {
      this.movieList = data;
      console.log("MovieList: ", this.movieList);

    this.showService.getAll().subscribe(data => {
      this.showList = data;
      console.log("ShowList: ", this.showList)

      this.cinemaService.getAll().subscribe(data => {
        this.cinemaList = data;
        console.log("CinemaList: ", this.cinemaList);
      })
    })
    })

    //Check om brugeren allerede har en valgt by i deres session, hvis ikke, så skal de fremvises areapick
    console.log('FrontpageComponent initialized');
    const city = localStorage.getItem('selectedCity');
    if (city) {
      this.chosenCity = city;
      // Hent alt data der er for den valgte by
      console.log('Selected city:', city);
    }
  }

  toggleCinemaDisplay(): void {
    
    const cinemaList = document.getElementById('cinemaList');
  
    if (cinemaList) {
      const isListVisible = cinemaList.style.display === 'flex';
      cinemaList.style.display = isListVisible ? 'none' : 'flex';
      this.showCinemaList = !isListVisible;
    }
  }
}
