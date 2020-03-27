import { Component } from "@angular/core";
import { fromEvent, of } from "rxjs";
import { scan, map, filter, first } from "rxjs/operators";

import { User } from "../_models";
import { UserService } from "../_services";

@Component({
  selector: "app-grid",
  templateUrl: "./grid.component.html",
  styleUrls: ["./grid.component.css"]
})
export class GridComponent {
  apiUsers: User[];
  constructor(private userService: UserService) {
    userService
      .getAll()
      .pipe(first())
      .subscribe((users: User[]) => {
        //console.log(users);
        this.apiUsers = users;
      });
    //console.log(this.apiUsers);
  }

  /*
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
  */
  columnDefs = [
    { headerName: 'CI', field: "ci", sortable: false, maxWidth: 100 },
    { headerName: 'UserName', field: "userName", minWidth: 200 },
    { headerName: 'Email', field: "email", minWidth: 300 },
  ];

  gridOptions = {
    // enable sorting on all columns by default
    defaultColDef: {
      sortable: true
    }
  };
}
