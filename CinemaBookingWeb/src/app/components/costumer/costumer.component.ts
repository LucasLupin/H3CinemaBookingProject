import { Component } from '@angular/core';
import { Subscriber } from 'rxjs';
import { Costumer } from 'src/app/models/costumer/costumer';
import { CostumerService } from 'src/app/services/costumer.service';

@Component({
  selector: 'app-costumer',
  templateUrl: './costumer.component.html',
  styleUrls: ['./costumer.component.css']
})
export class CostumerComponent {

  costumer : Costumer = {};
  costumerList : Costumer[] = [];

  ngOnInit(): void {
    // this.costumerList = this.service.getAll();
    // console.log("This is Version3: ", this.costumerList);
    this.service.getAll().subscribe(data => {
      this.costumerList = data;
      console.log("data: ", data);
      console.log("CostumerList: ", this.costumerList);
    })
  }// end ngOnInit

  constructor(private service: CostumerService ){
    
  }

  getAll3(): void {

  }
}//end Class