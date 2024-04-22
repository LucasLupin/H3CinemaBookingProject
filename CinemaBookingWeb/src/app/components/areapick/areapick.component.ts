import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Area } from 'src/app/models/area/area';
import { AreaService } from 'src/app/services/area.service';
import { GenericService } from 'src/app/services/generic.services';

@Component({
  selector: 'app-areapick',
  templateUrl: './areapick.component.html',
  styleUrls: ['./areapick.component.css']
})
export class AreapickComponent {
  AreaList : Area[] = [];

  constructor(private router: Router, private service: GenericService<Area>) {}

  ngOnInit() {
    this.service.getAll("Area").subscribe(data => {
      this.AreaList = data;
      console.log("Data: ", data);
      console.log("AreaList: ", this.AreaList);
    })
  }

  onAreaButtonClick(city?: string, cityID? : number) {
    if (city) {
      // Tjek med API om den valgte by findes, gør den det, så gem byen i localstorage og redirect til forsiden
      localStorage.setItem('selectedCity', city);
      this.router.navigateByUrl('/');
    }
  }
}
