import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({ providedIn: "root" })
export class DataService {
  sports = [
    { id: '1', name: 'Soccer' },
    { id: '2', name: 'Basketball' },
    { id: '3', name: 'Tennis' },
    { id: '4', name: 'Swimming' },
    { id: '5', name: 'Raquetball' },
  ];

  getSports(): Observable<any[]> {
    return of(this.sports);
  }

  getStates(): Observable<any[]> {
    return of([
      { id: "1", name: "Virginia" },
      { id: "2", name: "Maryland" },
      { id: "3", name: "District of Columbia" }
    ]);
  }
}