import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, forkJoin } from "rxjs";
import { scan, map, filter, first, catchError } from "rxjs/operators";
import { FormlyFieldConfig } from "@ngx-formly/core";

import { environment } from "../../environments/environment";
import { User } from "../_models";

@Injectable({ providedIn: "root" })
export class UserService {
  constructor(private http: HttpClient) { }

  getUserData(): Observable<any> {
    return forkJoin([this.getUser(), this.getFields()]);
  }

  getUser() {
    //return this.http.get<{ firstName: string, lastName: string, mac: string }>
    return this.http.get<User>("assets/user.json");
  }

  getFields() {
    return this.http.get<FormlyFieldConfig[]>("assets/user-form.json");
  }

  getAll(): Observable<User[]> {
    //console.log('API Call - Get All');
    //console.log(this.http.get<User[]>(`${environment.apiUrl}/core/v1/administration/users`));
    return this.http.get<User[]>(
      `${environment.apiUrl}/core/v1/administration/users`
    );
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(
      `${environment.apiUrl}/core/v1/administration/users`
    );
  }
}
