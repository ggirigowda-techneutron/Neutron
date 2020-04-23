import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, forkJoin, of } from "rxjs";
import { FormlyFieldConfig } from "@ngx-formly/core";

@Injectable({ providedIn: "root" })
export class DataService {
  constructor(private http: HttpClient) { }

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
      { id: "3", name: "District of Columbia" },
      { id: "4", name: "Delaware" }
    ]);
  }


  getUserCreateForm(): Observable<any> {
    return forkJoin([this.getUserCreateFormFields()]);
  }

  getUserCreateFormFields(): Observable<FormlyFieldConfig[]> {
    return this.http.get<FormlyFieldConfig[]>("assets/user-create-form.json");
  }
}
