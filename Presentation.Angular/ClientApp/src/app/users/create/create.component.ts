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

  // Constructor
  constructor(private router: Router, private http: HttpClient, private formBuilder: FormBuilder) {

  }

  // Init
  ngOnInit() {
    this.userForm = this.formBuilder.group({
      firstName: ["", [Validators.required, Validators.pattern("^[a-zA-Z]+$")]],
      lastName: ["", [Validators.required, Validators.pattern("^[a-zA-Z]+$")]],
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required, Validators]],
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
        new Userprofiledto(0, "00000000-0000-0000-0000-000000000000", this.userForm.get('firstName').value, this.userForm.get('lastName').value, new Date(1940, 1, 1)),
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
