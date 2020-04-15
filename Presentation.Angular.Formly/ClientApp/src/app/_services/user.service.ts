import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, forkJoin } from "rxjs";
import { scan, map, filter, first, catchError } from "rxjs/operators";
import { FormlyFieldConfig } from "@ngx-formly/core";

import { environment } from "../../environments/environment";
import { User } from "../_models";
import { Claim } from "../_models";
import { Profile } from "../_models";

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

  createUser(
    userName: string,
    password: string,
    firstName: string,
    lastName: string,
    email: string
  ): Observable<string> {
    let u: User = new User(0, userName, email);
    u.password = password;
    u.profile = new Profile(0, "00000000-0000-0000-0000-000000000000", firstName, lastName, new Date(1940, 1, 1), "c78227a5-ca89-4b9d-aa6a-e5d779b94b20");
    u.claims = new Array<Claim>(new Claim("00000000-0000-0000-0000-000000000000", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "USER"));

    //console.log(JSON.stringify(u));

    return this.http
      .post<string>(`${environment.apiUrl}/core/v1/administration/user/create`, JSON.stringify(u));
    // .pipe(retryWhen(errors =>
    //     errors.pipe(
    //       //delay(3000), 
    //       retry(3),
    //       catchError((err: HttpErrorResponse) => {
    //         console.log(err);
    //         let error;
    //         if (err instanceof Error) {
    //           error = err; // client side error
    //         } else {
    //           error = new Error(`Error creating user.`);
    //         }
    //         console.log(error);
    //         // handle error locally
    //         return throwError(error);
    //         // recover?
    //         //return Observable.of({login:'x', bio:'x', company: 'x'});
    //       })
    //     )
    //   ),
    //   map(response => {
    //     console.log(response);
    //     //var user = response as User;
    //     return response;
    //   })
    // );
  }
}
