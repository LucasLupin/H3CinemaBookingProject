import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { Area } from 'src/app/models/area/area';
import { Cinema } from 'src/app/models/cinema/cinema';
import { Movie } from 'src/app/models/movie/movie';
import { Region } from 'src/app/models/region/region.module';
import { Show } from 'src/app/models/show/show';
import { GenericService } from 'src/app/services/generic.services';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent {
  cinemaList : Cinema[] = [];
  movieList : Movie[] = [];
  showList : Show[] = [];
  areaList: Area[] = [];
  regionList: Region[] = [];
  cinemasByRegion: { [key: string]: Cinema[] } = {}; 
// Dette er en syntax i Typescript som fungere lidt som et Dictonary. Den bruger regionName som Key og Value fra Cinema som Values
  
  chosenCity: string = '';
  showCinemaList: boolean = false;

  constructor(
    private cinemaService: GenericService<Cinema>,
    private movieService: GenericService<Movie>,
    private showService: GenericService<Show>,
    private regionService: GenericService<Region>,
    private areaService: GenericService<Area>
  ) {}



  ngOnInit() {

    this.regionService.getAll('region').pipe(
      switchMap(regions => {
        this.regionList = regions;
        return this.areaService.getAll('area');
      }),
      switchMap(areas => {
        this.areaList = areas;
        return this.cinemaService.getAll('cinema');
      }),
      switchMap(cinemas => {
        this.cinemaList = cinemas;
        return this.showService.getAll('show');
      }),
      switchMap(shows => {
        this.showList = shows;
        return this.movieService.getAll('movie');
      })
    ).subscribe(movies => {
      this.movieList = movies;
      this.mapCinemasToRegions();
    });

    //Check om brugeren allerede har en valgt by i deres session, hvis ikke, så skal de fremvises areapick
    console.log('FrontpageComponent initialized');
    const city = localStorage.getItem('selectedCity');
    if (city) {
      this.chosenCity = city;
      // Hent alt data der er for den valgte by
      console.log('Selected city:', city);
    }
  }
  
  mapCinemasToRegions() {
    this.cinemasByRegion = {};
    this.cinemaList.forEach(cinema => {
      // Denne finder det Area som matcher AreaID på de forskellige Cinemas
      const area = this.areaList.find(a => a.areaID === cinema.areaID);
      // Hvis den kan finde et Area der matcher, prøver den finde den Region der matcher det area
      if(area) {
        const region = this.regionList.find(r => r.regionID === area.regionID);
        //Hvis den kan finde finde en Region der matcher, laver den et array af cinema hvor RegionName er en key
        if(region) {
          //Hvis den ikke kan finde en Region der matcher, vil den bare lave et tomt CinamasByRegion
          if (!this.cinemasByRegion[region.regionName]) {
            this.cinemasByRegion[region.regionName] = [];
          }
          //Den bruger push, til at putte cinemas under deres Region. Så man har cinemasByRegion, som indeholder alle cinemas men under deres regions
          this.cinemasByRegion[region.regionName].push(cinema);
        }
      }
    });
    console.log("CinemasByRegion: ", this.cinemasByRegion);
  }

  toggleCinemaDisplay(): void {
      this.showCinemaList = !this.showCinemaList;
  }
}
