  import { Component } from '@angular/core';
  import { ActivatedRoute, Router } from '@angular/router';
  import { faTicket, faRefresh } from '@fortawesome/free-solid-svg-icons';
import { EMPTY, catchError, switchMap } from 'rxjs';
  import { Cinema } from 'src/app/models/cinema/cinema';
  import { Movie } from 'src/app/models/movie/movie';
  import { Show } from 'src/app/models/show/show';
  import { GenericService, LocalStorageGeneric } from 'src/app/services/generic.services';

  interface RouteState {
    movieId: string;
  }

  @Component({
    selector: 'app-movie-page',
    templateUrl: './movie-page.component.html',
    styleUrls: ['./movie-page.component.css']
  })
  export class MoviePageComponent {
    selectedCityData: string = "";
    faTicket = faTicket;
    faRefresh = faRefresh;
    selectedMovie: Movie | undefined;
    selectedGenreId: string =""
    selectedAreaID: string = ""
    SelectedCityName: string = "";
    movieId: string | null = null;
    toggleDetail: boolean = false;
    toggleBody: Boolean = false;
    showsList: Show[] = [];
    cinemaInSelectedArea: Cinema[] = [];
    dayShowsList: any[] = [];
    cinemasList: Cinema[] = [];
    moviesList: Movie[] = [];
    nextTenDays: Date[] = [];
    showsByDay: { [key: string]: Show[] } = {};
    filteredShows: Show[] = [];
    


    constructor(private router: Router, private route: ActivatedRoute, private showService:GenericService<Show>, private cinemaService:GenericService<Cinema>, private movieService:GenericService<Movie>, private storageService: LocalStorageGeneric) {}

    ngOnInit() {
      // Handling local storage and navigation based on the selected area
      this.storageService.handleLocalStorage().then(areaExists => {
        if (!areaExists) {
          console.log('No valid area selected, user redirected to area pick.');
          this.router.navigate(['/areapick']);
        } else {
          console.log('Valid area selected, user can proceed in the app.');
        }
      }).catch(error => {
        console.error('Error handling in localStorage service:', error);
        this.router.navigate(['/areapick']);
      });

  //Fetching Data before The Executing Methods
  this.movieService.getAll("movie").pipe(
    switchMap(movies => {
      this.moviesList = movies;
      console.log("MovieList: ", this.moviesList);

      this.movieId = this.route.snapshot.paramMap.get('movieId');
      if (this.movieId) {
        this.setSelectedMovie(this.movieId);
      } else {
        console.error('movieId is null');
        return EMPTY;
      }
      
      return this.showService.getAll("show");
    }),
    switchMap(shows => {
      this.showsList = shows;
      console.log("ShowsList: ", this.showsList);
      return this.cinemaService.getAll("cinema");
    }),
    catchError(error => {
      console.error('Error in observable chain:', error);
      return EMPTY;
    })
  ).subscribe(cinemas => {
    this.cinemasList = cinemas;
    console.log("CinemasList: ", this.cinemasList);

    // After all asynchronous operations are complete, run these methods:
    this.FindCinemaBySelectedArea();
    this.filterShowsByDate();
  });

  this.getSelectedArea();
  this.getNextTenDays();
}


  getNextTenDays(): void {
    const today = new Date(); 
    for (let i = 0; i < 10; i++) {
      const nextDay = new Date(today);
      nextDay.setDate(today.getDate() + i);
      this.nextTenDays.push(nextDay);
    }
  }

  getDayAbbreviation(date: Date): string {
    const days = ['Søn', 'Man', 'Tir', 'Ons', 'Tor', 'Fre', 'Lør'];
    return days[date.getDay()];
  }

  filterShowsByDate(): void {
    if (!this.selectedMovie) {
      console.error('No selected movie.');
      this.filteredShows = [];
      return;
    }
  
    const selectedMovieId = this.selectedMovie.movieID;
    const dateRange = this.nextTenDays.map(day => day.toDateString());
    console.log("DateRange: ", dateRange);
    
  
    this.filteredShows = this.showsList.filter(show => {
      if (!show.showDateTime) {
        return false; // Skip this show if the date-time is undefined
      }
      
      //Makes a string of the shows date that
      const showDateStr = new Date(show.showDateTime).toDateString();
      return show.movieID === selectedMovieId && dateRange.includes(showDateStr);
    });

    console.log("FilteredShows: ", this.filteredShows);
    
  
    this.organizeShowsByDate(); // Organize the filtered shows by date for display
  }
  
  

  organizeShowsByDate(): void {
    this.showsByDay = {}; // Resetting the map
  
    this.filteredShows.forEach(show => {
      // Ensure that showDateTime is defined before attempting to use it.
      if (show.showDateTime) {
        const showDate = new Date(show.showDateTime).toDateString(); // Safely format the date to a simple string
  
        if (!this.showsByDay[showDate]) {
          this.showsByDay[showDate] = []; // Initialize an empty array if no entry exists
        }
  
        this.showsByDay[showDate].push(show); // Push the show into the correct date entry
      } else {
        // Optionally handle or log cases where showDateTime is undefined
        console.warn("Skipping show with undefined showDateTime:", show);
      }
      
    });
    console.log("showByDay: ", this.showsByDay);
  }
  
  


  getSelectedArea(): void {
    const selectedCityData = this.storageService.getItem('selectedArea');
      if (selectedCityData) {
        console.log(selectedCityData);
        
        const selectedArea = selectedCityData;
        
        this.selectedAreaID = selectedArea.id;
        this.SelectedCityName = selectedArea.name;
      }
  }

  setSelectedMovie(movieId: string): void {
    if(movieId)
      {
        this.selectedMovie = this.moviesList.find(movie => movie.movieID?.toString() == movieId);
        console.log("SelectedMovie: ", this.selectedMovie);
        
      }
  }

  formatMinutesToHoursAndMinutes(minutes: number | undefined): string {
    if (minutes === undefined || minutes === null) {
      return "0h 0min";
    }
    const hours = Math.floor(minutes / 60);
    const remainingMinutes = minutes % 60;
    return `${hours}h ${remainingMinutes}min`;
  }

  FindCinemaBySelectedArea(): void {
      this.cinemaInSelectedArea = this.cinemasList.filter(cinema => {
        return String(cinema.areaID) === String(this.selectedAreaID); 
      });

      console.log("CinemaInSelectedArea: ", this.cinemaInSelectedArea);
      
    }



  toggleDetails(): void {
    this.toggleDetail = !this.toggleDetail;
  }

  toggleBodys(): void {
    this.toggleBody = !this.toggleBody;
  }
  }
