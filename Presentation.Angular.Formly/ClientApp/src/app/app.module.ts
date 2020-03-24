import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatButtonModule } from "@angular/material";
import {
  MAT_FORM_FIELD_DEFAULT_OPTIONS,
  MatFormFieldDefaultOptions
} from "@angular/material/form-field";
import { ReactiveFormsModule } from "@angular/forms";
import { FormlyModule } from "@ngx-formly/core";
import { FormlyMaterialModule } from "@ngx-formly/material";
import { HttpModule } from "@angular/http";
import { MatNativeDateModule } from "@angular/material/core";
import { FormlyMatDatepickerModule } from "@ngx-formly/material/datepicker";

// Guards
import { AuthGuard } from "./login/auth.guard";

// Services.
import { LoginService } from './login/login.service';
import { DataService } from './kitchen/data.service';

// Components.
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { KitchenComponent } from "./kitchen/kitchen.component";
import { LoginComponent } from './login/login.component';

const appearance: MatFormFieldDefaultOptions = {
  //appearance: "outline"
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    KitchenComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    HttpModule,
    ReactiveFormsModule,
    FormlyModule.forRoot({
      validationMessages: [
        { name: "required", message: "This field is required" }
      ]
    }),
    FormlyMaterialModule,
    MatNativeDateModule,
    FormlyMatDatepickerModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: "kitchen", component: KitchenComponent, canActivate: [AuthGuard] },
      { path: "login", component: LoginComponent }
    ])
  ],
  providers: [{
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: appearance
  },
    AuthGuard,
    LoginService,
    DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
