import { Component, OnInit } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Component({
  selector: "app-users",
  templateUrl: "./users.component.html",
  styleUrls: ["./users.component.css"]
})
export class UsersComponent implements OnInit {
  users: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    const token = localStorage.getItem("jwt");
    this.http.get("https://neutronmiddlewarecorewebapigateway.azurewebsites.net/core/v1/administration/users",
      {
        headers: new HttpHeaders({
          "Authorization": `Bearer ${token}`,
          "Content-Type": "application/json"
        })
      }).subscribe(response => {
        this.users = response;
      },
      err => {
        console.log(err);
      });
  }

}
