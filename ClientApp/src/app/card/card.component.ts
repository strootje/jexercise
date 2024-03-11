import { Component, Input } from '@angular/core';
import { JobOffer } from '../BackendService';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
})
export class CardComponent {
  @Input() jobOffer!: JobOffer;
}
