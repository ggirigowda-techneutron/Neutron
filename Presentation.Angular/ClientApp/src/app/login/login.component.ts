import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent {
  invalidLogin: boolean;

  constructor(private router: Router, private http: HttpClient) {}

  login(form: NgForm) {
    const credentials = JSON.stringify(form.value);
    this.http.post("https://neutronmiddlewarecorewebapigateway.azurewebsites.net/auth",
      credentials,
      {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      }).subscribe(response => {
        const token = (response as any).access_token;
        localStorage.setItem("jwt", token);
        this.invalidLogin = false;
        this.router.navigate(["/"]);
      },
      err => {
        this.invalidLogin = true;
      });
  }
}
