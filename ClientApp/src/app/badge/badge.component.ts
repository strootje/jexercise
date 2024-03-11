import { Component, Input } from '@angular/core';
import { Company } from '../home/home.component';

@Component({
  selector: 'app-badge',
  templateUrl: './badge.component.html',
})
export class BadgeComponent {
  @Input('company')
  public company!: Company;
  @Input('isActive')
  public isActive!: boolean;
  @Input('offerCount')
  public offerCount!: number;
}
