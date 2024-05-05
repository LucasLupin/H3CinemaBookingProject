// src/app/services/visibility.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class VisibilityService {
  private showUserDetails = new BehaviorSubject<boolean>(false);
  public showUserDetails$ = this.showUserDetails.asObservable();

  constructor() { }

  toggleUserDetailsVisibility(): void {
    this.showUserDetails.next(!this.showUserDetails.value);
  }
}
