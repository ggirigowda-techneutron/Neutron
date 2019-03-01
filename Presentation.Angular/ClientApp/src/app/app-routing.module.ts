import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { UsersComponent } from './users/users.component';

const routes: Routes = [{ path: 'users', component: UsersComponent }];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes ,{ enableTracing: true })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
