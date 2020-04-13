import { Component } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { FormlyFieldConfig } from "@ngx-formly/core";
import { of } from "rxjs";
import { DataService } from "../_services";

@Component({
  selector: "app-kitchen",
  templateUrl: "./kitchen.component.html",
  styleUrls: ["./kitchen.component.css"]
})
export class KitchenComponent {
  constructor(private dataService: DataService) { }
  form = new FormGroup({});
  model = {
    name: "John Doe",
    bio: "This is one large biography.",
    attending: true,
    ageGroup: "1",
    country: "1",
    arrivalDate: "2020-01-01T05:00:00.000Z",
    favoriteCharacter: ["1", "2"],
    sport: "1"
  };
  fields: FormlyFieldConfig[] = [
    {
      key: "name",
      type: "input",
      templateOptions: {
        label: "Full Name",
        placeholder: "Enter full name",
        required: true
      }
    },
    {
      key: "bio",
      type: "textarea",
      templateOptions: {
        label: "Biography",
        placeholder: "Enter Biography",
        required: true
      }
    },
    {
      key: "attending",
      type: "checkbox",
      templateOptions: {
        label: "Attending"
      }
    },
    {
      key: "ageGroup",
      type: "select",
      templateOptions: {
        label: "Age Group",
        placeholder: "Select age group",
        required: true,
        options: [
          { label: "[10 - 20]", value: "1" },
          { label: "[30 - 40]", value: "2" },
          { label: "[50 - 60]", value: "3" }
        ]
      }
    },
    {
      key: "country",
      type: "radio",
      templateOptions: {
        label: "Country",
        required: false,
        options: [
          { label: "US", value: "1" },
          { label: "India", value: "2" },
          { label: "Russia", value: "3" }
        ]
      }
    },
    {
      key: "arrivalDate",
      type: "datepicker",
      templateOptions: {
        label: "Arrival",
        placeholder: "Enter Arrival Date",
        description: "This is the date of arrival into the country.",
        required: true
      }
    },
    {
      key: "password",
      validators: {
        fieldMatch: {
          expression: control => {
            const value = control.value;
            return (
              value.passwordConfirm === value.password ||
              // avoid displaying the message error when values are empty
              (!value.passwordConfirm || !value.password)
            );
          },
          message: "Password Not Matching",
          errorPath: "passwordConfirm"
        }
      },
      fieldGroup: [
        {
          key: "password",
          type: "input",
          templateOptions: {
            type: "password",
            label: "Password",
            placeholder: "Must be at least 3 characters",
            required: true,
            minLength: 3
          }
        },
        {
          key: "passwordConfirm",
          type: "input",
          templateOptions: {
            type: "password",
            label: "Confirm Password",
            placeholder: "Please re-enter your password",
            required: true
          }
        }
      ]
    },
    {
      key: "favoriteCharacter",
      type: "select",
      templateOptions: {
        label: "Favorite Character",
        multiple: true,
        options: [
          { label: "Iron Man", value: "1" },
          { label: "Captain America", value: "2" },
          { label: "Black Widow", value: "3" },
          { label: "Hulk", value: "4" },
          { label: "Captain Marvel", value: "5" }
        ]
      }
    },
    {
      key: 'sport',
      type: 'select',
      templateOptions: {
        label: 'Sport',
        options: this.dataService.getSports(),
        valueProp: 'id',
        labelProp: 'name',
      },
    }
  ];

  onSubmit() {
    if (this.form.valid) {
      alert(JSON.stringify(this.model, null, 2));
    }
  }
}
