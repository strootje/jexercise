import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class BackendService {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL')
    private baseUrl: string,
  ) {}

  public getCompanies() {
    return this.http.get<Company[]>(`${this.baseUrl}api/v1/companies`);
  }

  public getCompany(id: number) {
    return this.http.get<Company>(`${this.baseUrl}api/v1/companies/${id}`);
  }

  public getJobOffers() {
    return this.http.get<JobOffer[]>(`${this.baseUrl}api/v1/job-offers`);
  }

  public getJobOfferById(id: number) {
    return this.http.get<JobOffer>(`${this.baseUrl}api/v1/job-offers/${id}`);
  }

  public getJobOffersByCompanyId(id: number) {
    return this.http.get<JobOffer[]>(`${this.baseUrl}api/v1/job-offers/company/${id}`);
  }

  public newJobOffer(companyId: number, title: string, description: string) {
    return this.http.put(`${this.baseUrl}api/v1/job-offers`, { title, description, companyId });
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
