import { Component, Input } from '@angular/core';
import { Company } from '../BackendService';

@Component({
  selector: 'app-badge',
  templateUrl: './badge.component.html',
})
export class BadgeComponent {
  @Input() company!: Company;
  @Input() isActive!: boolean;
  @Input() offerCount!: number;
}
