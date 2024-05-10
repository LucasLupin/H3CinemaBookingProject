  import { Component } from '@angular/core';
  import { ActivatedRoute, Router } from '@angular/router';
  import { faTicket, faRefresh } from '@fortawesome/free-solid-svg-icons';
  import { EMPTY, catchError, finalize, switchMap } from 'rxjs';
  import { Cinema } from 'src/app/models/cinema/cinema';
  import { Movie } from 'src/app/models/movie/movie';
  import { Show } from 'src/app/models/show/show';
  import { GenericService, LocalStorageGeneric } from 'src/app/services/generic.services';
  import { ShowService } from 'src/app/services/show.service';
  import { CinemaShowMap } from '../../services/show.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

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
    selectedCinemaName: string | null = null;
    selectedDate: string = '';

    toggleDetail: boolean = false;
    toggleBody: Boolean = false;
    showsList: Show[] = [];
    cinemaInSelectedArea: Cinema[] = [];
    dayShowsList: any[] = [];
    cinemasList: Cinema[] = [];
    moviesList: Movie[] = [];
    nextTenDays: Date[] = [];
    showsByDay: { [key: string]: Show[] } = {};
    showsByCinemaAndDate: CinemaShowMap = {};
    showsByCinemaAndDateFiltered: CinemaShowMap = {};

    filteredShows: Show[] = [];
    


    constructor(private router: Router, private route: ActivatedRoute, private showService:GenericService<Show>, private showshowService: ShowService, private cinemaService:GenericService<Cinema>, private movieService:GenericService<Movie>, private storageService: LocalStorageGeneric, private sanitizer: DomSanitizer) {}

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

      this.selectedCinemaName = localStorage.getItem('SelectedCinemaName') || '';  
      this.selectedDate = localStorage.getItem('SelectedDate') || '';  

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
    this.FindCinemaBySelectedArea();

    if (this.movieId && this.selectedAreaID) {
      this.fetchFilteredShowsAndSort(parseInt(this.selectedAreaID), parseInt(this.movieId));
      
    } else {
      console.error('Area ID or movie ID is null');
      // handle error, maybe navigate away or show a message
    }
  });

  this.filterShowDisplayed();
  this.getSelectedArea();
  this.getNextTenDays();
}

objectKeys(obj: any): string[] {
  return Object.keys(obj);
}

toggleDetails(): void {
this.toggleDetail = !this.toggleDetail;
}

toggleBodys(): void {
this.toggleBody = !this.toggleBody;
}

fetchFilteredShowsAndSort(areaId: number, movieId: number): void {
  console.log("Test");
  
  this.showshowService.getFilteredShows("Show", areaId, movieId).subscribe({
    next: (cinemaShowMap) => {
      // Iterate over each cinema
      for (const cinema in cinemaShowMap) {
        // Iterate over each date within a cinema
        for (const date in cinemaShowMap[cinema]) {
          // Sort the shows by time
          cinemaShowMap[cinema][date] = cinemaShowMap[cinema][date]
            .sort((a, b) => new Date(a.showDateTime).getTime() - new Date(b.showDateTime).getTime());
        }
      }
      this.showsByCinemaAndDate = cinemaShowMap;
      this.filterShowDisplayed();

      console.log("Sorted and Filtered Shows by Cinema and Date And Movie: ", this.showsByCinemaAndDate);
      
    },
    error: (error) => console.error('Error fetching filtered shows:', error)
  });
}



  getNextTenDays(): void {
    const today = new Date(); 
    for (let i = 0; i < 10; i++) {
      const nextDay = new Date(today);
      nextDay.setDate(today.getDate() + i);
      this.nextTenDays.push(nextDay);
    }
  }

  getDayAbbreviation(dateStr: string): string {
    const date = new Date(dateStr);
    const days = ['Søn', 'Man', 'Tir', 'Ons', 'Tor', 'Fre', 'Lør'];
    return days[date.getDay()];
}

onCinemaChange(event: Event): void {
  const element = event.target as HTMLSelectElement;
  if (element) {
    this.selectedCinemaName = element.value;
    localStorage.setItem('SelectedCinemaName', this.selectedCinemaName);
    this.filterShowDisplayed();
  }
}

onDateChange(selectedDateString: string): void {
  this.selectedDate = selectedDateString;
  localStorage.setItem('SelectedDate', this.selectedDate);
}

// Determine if the day should be blurred
shouldBlur(date: string): boolean {
  return !!this.selectedDate && date !== this.selectedDate;
}



filterShowDisplayed(): void {
  // Clear previous filters
  this.showsByCinemaAndDateFiltered = {};

  if (this.selectedCinemaName) {
    // Filter to shows from the selected cinema
    if (this.showsByCinemaAndDate[this.selectedCinemaName]) {
      this.showsByCinemaAndDateFiltered[this.selectedCinemaName] = this.showsByCinemaAndDate[this.selectedCinemaName];
    } else {
      console.error('No shows found for the selected cinema:', this.selectedCinemaName);
    }
  } 
  else {
    // If no cinema is selected, show all shows for all cinemas
    Object.keys(this.showsByCinemaAndDate).forEach(cinemaName => {
      this.showsByCinemaAndDateFiltered[cinemaName] = this.showsByCinemaAndDate[cinemaName];
    });
    
  }
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

  sanitizeUrl(url: string): SafeResourceUrl {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
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

    resetDropdowns(): void {
      const dropdownKeys = ['SelectedCinemaName', 'SelectedDate'];
    
      dropdownKeys.forEach(key => {
        this.selectedCinemaName = '';
        this.selectedDate = '';
        this.storageService.removeItem(key);
      });
      this.filterShowDisplayed(); 
    }
  }
