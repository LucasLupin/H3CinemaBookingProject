import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BookShow } from '../models/BookShow/bookshow';

@Injectable({
  providedIn: 'root'
})
export class BookShowService {
  private bookShowSource = new BehaviorSubject<BookShow | null>(null);
  currentBookShow = this.bookShowSource.asObservable();

  constructor() {}

  updateBookShow(bookShow: BookShow) {
    this.bookShowSource.next(bookShow);
  }
}
