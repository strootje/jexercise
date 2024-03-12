import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BackendService, JobOffer } from '../BackendService';

@Component({
  selector: 'app-job-offer',
  templateUrl: './job-offer.component.html',
})
export class JobOfferComponent {
  jobOffer!: JobOffer;

  constructor(route: ActivatedRoute, backend: BackendService) {
    route.paramMap.subscribe((params) => {
      const jobOfferId = parseInt(params.get('id')!);
      backend.getJobOfferById(jobOfferId).subscribe((jobOffer) => (this.jobOffer = jobOffer));
    });
  }
}
