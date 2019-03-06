import { Component, OnInit } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";
import { FormBuilder, Validators } from "@angular/forms";

import { Usercreatedto } from "./usercreatedto.model";
import { Userclaimdto } from "./userclaimdto.model";
import { Userprofiledto } from "./userprofiledto.model";

@Component({
  selector: "app-create",
  templateUrl: "./create.component.html",
  styleUrls: ["./create.component.css"]
})
export class CreateComponent implements OnInit {
  // User form
  userForm;
  // Invalid login
  invalidLogin: boolean;
  // Countries
  countries = [{ 'id': 'c78227a5-ca89-4b9d-aa6a-e5d779b94b20', 'name': 'IN' }, { 'id': '2af6ff6c-8bb8-46f0-b27e-81def1b76b64', 'name': 'USA' }, { 'id': '70efb18e-b531-4acd-a784-70e91ee89d4c', 'name': 'GH' }];

  // Constructor
  constructor(private router: Router, private http: HttpClient, private formBuilder: FormBuilder) {

  }

  // Init
  ngOnInit() {
    this.userForm = this.formBuilder.group({
      firstName: ["", [Validators.required, Validators.pattern("^[a-zA-Z]+$")]],
      lastName: ["", [Validators.required, Validators.pattern("^[a-zA-Z]+$")]],
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required]],
      country: ["", [Validators.required]]
    });
  }

  // Submit
  onSubmit() {
    if (this.userForm.valid) {
      this.invalidLogin = false;
      const token = localStorage.getItem("jwt");
      const usercreatedto = new Usercreatedto(0,
        "00000000-0000-0000-0000-000000000000",
        this.userForm.get('email').value,
        this.userForm.get('email').value,
        this.userForm.get('password').value,
        12345,
        new Userprofiledto(0, "00000000-0000-0000-0000-000000000000", this.userForm.get('firstName').value, this.userForm.get('lastName').value, new Date(1940, 1, 1), this.userForm.get('country').value),
        new Array<Userclaimdto>(new Userclaimdto("00000000-0000-0000-0000-000000000000", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "USER")));
      const userDto = JSON.stringify(usercreatedto);
      this.http.post("https://neutronmiddlewarecorewebapigateway.azurewebsites.net/core/v1/administration/user/create",
        userDto,
        {
          headers: new HttpHeaders({
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json"
          })
        }).subscribe(response => {
          this.router.navigate(["/users"]);
        },
        err => {
          console.log(err);
        });
    } else {
      this.invalidLogin = true;
    }
  }

}
