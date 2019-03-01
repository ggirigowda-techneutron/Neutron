import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Neutron Angular 7';
  subtitle = '.NET Core + Angular CLI v7 + Bootstrap & FontAwesome + Swagger Template';
  datetime = Date.now();

  incrementValue: number[] = [];

  constructor() {
    for (let index = 1; index <= 3; index++) {
      this.incrementValue.push(index);
    }
  }
}
