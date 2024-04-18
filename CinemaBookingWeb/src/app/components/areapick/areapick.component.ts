import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-areapick',
  templateUrl: './areapick.component.html',
  styleUrls: ['./areapick.component.css']
})
export class AreapickComponent {
  constructor(private router: Router) {}
  ngOnInit() {
  }

  onAreaButtonClick(city: string) {
    if (city) {
      // Tjek med API om den valgte by findes, gør den det, så gem byen i localstorage og redirect til forsiden
      localStorage.setItem('selectedCity', city);
      this.router.navigateByUrl('/');
    }
  }
}
