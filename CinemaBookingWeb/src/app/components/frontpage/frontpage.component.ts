import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent {
  constructor(private router: Router) {}
  chosenCity: string = '';
  ngOnInit() {
    //Check om brugeren allerede har en valgt by i deres session, hvis ikke, så skal de fremvises areapick
    console.log('FrontpageComponent initialized');
    const city = localStorage.getItem('selectedCity');
    if (city) {
      this.chosenCity = city;
      // Hent alt data der er for den valgte by
      console.log('Selected city:', city);
    }
  }
}
