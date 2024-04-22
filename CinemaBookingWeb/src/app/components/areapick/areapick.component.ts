import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Area } from 'src/app/models/area/area';
import { AreaService } from 'src/app/services/area.service';

@Component({
  selector: 'app-areapick',
  templateUrl: './areapick.component.html',
  styleUrls: ['./areapick.component.css']
})
export class AreapickComponent {
  AreaList : Area[] = [];

  constructor(private router: Router, private service: AreaService) {}

  ngOnInit() {
    this.service.getAll().subscribe(data => {
      this.AreaList = data;
      console.log("Data: ", data);
      console.log("AreaList: ", this.AreaList);
    })
  }

  onAreaButtonClick(cityName?: string, cityID? : number) {
    if (cityName && cityID !== undefined) {
      const cityData = {
        name: cityName,
        id: cityID
      };
  
      const cityDataString = JSON.stringify(cityData);
  
      localStorage.setItem('selectedCity', cityDataString);
  
      this.router.navigateByUrl('/');
    }
  }  
}
