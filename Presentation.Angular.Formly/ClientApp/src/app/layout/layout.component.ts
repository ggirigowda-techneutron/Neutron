import { fromEvent, of, Observable } from "rxjs";
import { scan, map, filter, first, tap } from "rxjs/operators";
import { ajax } from "rxjs/ajax";
import { Component, ViewEncapsulation } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { FormlyFormOptions, FormlyFieldConfig } from "@ngx-formly/core";
import { DataService } from "../_services";

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.css"],
  encapsulation: ViewEncapsulation.None
})
export class LayoutComponent {
  constructor(private dataService: DataService) { }
  form = new FormGroup({});
  model: any = {};
  options: FormlyFormOptions = {};

  fields: FormlyFieldConfig[] = [
    {
      template: "<strong><div><h3>Name:</h3></div></strong><hr />"
    },
    {
      fieldGroupClassName: "display-flex",
      fieldGroup: [
        {
          className: "flex-1",
          type: "input",
          key: "firstName",
          templateOptions: {
            label: "First Name",
            required: true,
            minLength: 3
          }
        },
        {
          className: "flex-1",
          type: "input",
          key: "lastName",
          hideExpression: model => !this.model.firstName,
          templateOptions: {
            label: "Last Name",
            required: true,
            minLength: 3
          },
          expressionProperties: {
            "templateOptions.disabled": "!model.firstName"
          }
        }
      ]
    },
    {
      template: "<strong><div><h3>Address:</h3></div></strong><hr />"
    },
    {
      fieldGroupClassName: "display-flex",
      fieldGroup: [
        {
          className: "flex-2",
          type: "input",
          key: "street",
          templateOptions: {
            label: "Street",
            required: true,
          }
        },
        {
          className: "flex-1",
          type: "input",
          key: "cityName",
          templateOptions: {
            label: "City",
            required: true,
          }
        },
        {
          className: "flex-1",
          type: "input",
          key: "zip",
          templateOptions: {
            type: "number",
            label: "Zip",
            max: 99999,
            min: 0,
            pattern: "\\d{5}",
            required: true,
          }
        }
      ]
    },
    {
      type: "select",
      key: "State",
      templateOptions: {
        label: "State",
        options: this.dataService.getStates(),
        valueProp: "id",
        labelProp: "name",
        required: true,
      }
    },
    {
      template: "<div><br /></div>"
    }
  ];

  submit() {
    if (this.form.valid) {
      alert(JSON.stringify(this.model));
    }
  }
}


