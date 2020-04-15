import { fromEvent, of, Observable } from "rxjs";
import { scan, map, filter, first, tap } from "rxjs/operators";
import { ajax } from "rxjs/ajax";
import { Router } from "@angular/router";
import { Component, ViewEncapsulation } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { FormlyFormOptions, FormlyFieldConfig } from "@ngx-formly/core";
import { DataService } from "../_services";
import { UserService } from "../_services";

@Component({
  selector: "app-user-create",
  templateUrl: "./user-create.component.html",
  styleUrls: ["./user-create.component.css"],
  encapsulation: ViewEncapsulation.None
})
export class UserCreateComponent {
  form = new FormGroup({});
  model: any = {};
  options: FormlyFormOptions = {};
  fields: FormlyFieldConfig[];
  constructor(private router: Router,
    private dataService: DataService,
    private userService: UserService) {
    this.dataService.getUserCreateForm().subscribe(([fields]) => {
      this.fields = fields;
    });
  }

  submit() {
    if (this.form.valid) {
      //alert(JSON.stringify(this.model));
      this.userService
        .createUser(
          this.model.userName,
          this.model.password,
          this.model.firstName,
          this.model.lastName,
          this.model.email
        )
        //.pipe(first())
        .subscribe(
          data => {
            //console.log(data);
            if (data) {
              this.router.navigate(["/"]);
            }
          },
          error => {
            if (error.status === 400) {
              console.log(error.error);
            }
            //console.log(error);
          }
        );
    }
  }
}


