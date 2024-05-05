import { Component } from '@angular/core';
import { Movie } from 'src/app/models/movie/movie';
import { VisibilityService } from 'src/app/services/visibility.service';

@Component({
  selector: 'appadminfrontpage',
  templateUrl: './adminFrontpage.component.html',
  styleUrls: ['./adminFrontPage.component.css']
})
export class AdminFrontPageComponent {

  constructor(public visibilityService: VisibilityService) {}

  toggleUserDetails() {
    this.visibilityService.toggleUserDetailsVisibility();
  }
}
