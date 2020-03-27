import { Component, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { FormlyFormOptions, FormlyFieldConfig } from "@ngx-formly/core";
import { Router } from "@angular/router";
import { first } from 'rxjs/operators';
import { LoginService } from "./login.service";
import { AuthenticationService } from '../_services';

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent {
  form = new FormGroup({});
  options: FormlyFormOptions = {};

  model;
  fields: FormlyFieldConfig[];

  constructor(private router: Router, private loginService: LoginService, private authenticationService: AuthenticationService) {
    this.loginService.getLoginData().subscribe(([fields]) => {
      this.model = {};
      this.fields = fields;
    });
  }

  submit() {
    if (this.form.valid) {
      //alert(JSON.stringify(this.model));
      // const token = "xyzxxyyzzz";
      //  localStorage.setItem("jwt", token);
      //  this.router.navigate(["/"]);
      this.authenticationService.login(this.model.userName, this.model.password)
        .pipe(first())
        .subscribe(
          data => {
            //console.log(data);
            if (data) {
              this.router.navigate(["/"]);
            }
          },
          error => {
            console.log(error);
          });
    }
  }
}
