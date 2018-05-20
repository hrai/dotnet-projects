import { Component } from '@angular/core';
import { HCardComponent } from './hcard.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  public card: Card;

  constructor() {
    this.card = new Card();
    this.card.givenName = "hang";
  }
}

export class Card {
  givenName: string;
  additionalName: string;
  familyName: string;
  org: string;
  photo: string;
  url: string;
  email: string;
  tel: string;
}