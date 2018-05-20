
import { Component, Input } from '@angular/core';
import { Card } from './app.component';

@Component({
  selector: 'hcard',
  templateUrl: './hcard.component.html',
})
export class HCardComponent {
  @Input() card: Card;
}
