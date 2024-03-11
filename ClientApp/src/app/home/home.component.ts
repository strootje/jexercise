import { Component } from '@angular/core';
import { BackendService, Company, JobOffer } from '../BackendService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public companies: Company[] = [];
  public jobOffers: JobOffer[] = [];

  public selectedCompany: number = 0;

  constructor(backend: BackendService) {
    backend.getCompanies().subscribe((companies) => {
      this.companies = companies;
    });

    backend.getJobOffers().subscribe((jobOffers) => {
      this.jobOffers = jobOffers;
    });
  }

  public toggleCompany(company: Company) {
    if (this.selectedCompany === company.id) {
      this.selectedCompany = 0;
    } else {
      this.selectedCompany = company.id;
    }
  }

  public isSelected(company: Company) {
    return this.selectedCompany === 0 || this.selectedCompany === company.id;
  }

  public filterCompanies() {
    return this.companies.filter((c) => this.jobOffers.map((p) => p.company.id).find((p) => p === c.id));
  }

  public filterJobOffers() {
    return this.jobOffers.filter((p) => this.isSelected(p.company));
  }

  public jobOffersPerCompany(company: Company) {
    return this.jobOffers.filter((p) => p.company.id === company.id).length;
  }
}
