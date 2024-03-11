import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { catchError } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public companies: Company[] = [];
  public jobOffers: JobOffer[] = [];

  public selectedCompany: number = 0;
  public createOfferForm: FormGroup;

  constructor(
    formBuilder: FormBuilder,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) {
    http.get<Company[]>(`${baseUrl}api/v1/companies`).subscribe((companies) => {
      this.companies = companies;
    });

    http.get<JobOffer[]>(`${baseUrl}api/v1/job-offers`).subscribe((jobOffers) => {
      this.jobOffers = jobOffers;
    });

    this.createOfferForm = formBuilder.group({
      title: '',
      companyId: 0,
      description: '',
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

  public onSubmit(): void {
    this.http
      .put(`${this.baseUrl}api/v1/job-offers`, {
        title: this.createOfferForm.value.title,
        description: this.createOfferForm.value.description,
        companyId: this.createOfferForm.value.companyId,
      })
      .subscribe(console.log);
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
