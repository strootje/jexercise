import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BackendService, Company, JobOffer } from '../BackendService';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
})
export class CompanyComponent {
  company!: Company;
  jobOffers!: JobOffer[];

  constructor(route: ActivatedRoute, backend: BackendService) {
    route.paramMap.subscribe((params) => {
      const companyId = parseInt(params.get('id')!);
      backend.getCompany(companyId).subscribe((company) => (this.company = company));
      backend.getJobOffersByCompanyId(companyId).subscribe((jobOffers) => (this.jobOffers = jobOffers));
    });
  }
}
