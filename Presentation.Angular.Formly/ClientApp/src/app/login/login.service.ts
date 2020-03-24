import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, forkJoin } from "rxjs";
import { FormlyFieldConfig } from "@ngx-formly/core";

@Injectable()
export class LoginService {
  constructor(private http: HttpClient) { }

  getLoginData(): Observable<any> {
    return forkJoin([this.getFields()]);
  }

  getFields() {
    return this.http.get<FormlyFieldConfig[]>("assets/login-form.json");
  }
}
