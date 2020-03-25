import { Component } from "@angular/core";
import { ajax } from "rxjs/ajax";

@Component({
  selector: "app-grid",
  templateUrl: "./grid.component.html",
  styleUrls: ["./grid.component.css"]
})
export class GridComponent {

  constructor() {
    const githubUsers = `https://api.github.com/users?per_page=2`;

    const users = ajax({
      url: githubUsers,
      method: "GET",
      headers: {
        /*some headers*/
      
      },
      body: {
        /*in case you need a body*/
      
      }
    });

    const subscribe = users.subscribe(
      res => console.log(res.response),
      err => console.error(err)
    );
  }

  columnDefs = [
    { field: "make" },
    { field: "model" },
    { field: "price", sortable: false }
  ];

  rowData = [
    { make: "Toyota", model: "Celica", price: 35000 },
    { make: "Ford", model: "Mondeo", price: 32000 },
    { make: "Porsche", model: "Boxter", price: 72000 }
  ];

  gridOptions = {
    // enable sorting on all columns by default
    defaultColDef: {
      sortable: true
    }
  };
}
