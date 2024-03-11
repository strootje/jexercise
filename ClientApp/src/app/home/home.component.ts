import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public companies: Company[] = [];
  public jobOffers: JobOffer[] = [];

  public selectedCompany: number = 0;

  constructor(
    http: HttpClient,
    @Inject('BASE_URL')
    baseUrl: string,
  ) {
    http.get<Company[]>(`${baseUrl}api/v1/companies`).subscribe((companies) => {
      this.companies = companies;
    });

    http.get<JobOffer[]>(`${baseUrl}api/v1/job-offers`).subscribe((jobOffers) => {
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

export interface Company {
  id: number;
  name: string;
  address: Address;
  jobOffers: JobOffer[];
}

export interface Address {
  street: string;
  city: string;
  zipcode: string;
  country: string;
}

export interface JobOffer {
  id: number;
  title: string;
  description: string;
  company: Company;
}
