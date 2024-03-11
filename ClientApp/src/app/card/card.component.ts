import { Component, Input } from '@angular/core';
import { JobOffer } from '../home/home.component';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
})
export class CardComponent {
  @Input('jobOffer')
  public jobOffer!: JobOffer;

  constructor() {}
}
