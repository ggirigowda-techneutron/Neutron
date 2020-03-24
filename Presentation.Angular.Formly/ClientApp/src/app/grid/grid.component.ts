import { Component } from "@angular/core";

@Component({
  selector: "app-grid",
  templateUrl: "./grid.component.html",
  styleUrls: ["./grid.component.css"]
})
export class GridComponent {
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
