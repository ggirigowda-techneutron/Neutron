import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from "./guards/auth-guard.service";

import { UsersComponent } from './users/users.component';
import { LoginComponent } from './login/login.component';



const routes: Routes = [
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent }
  ];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes ,{ enableTracing: true })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
