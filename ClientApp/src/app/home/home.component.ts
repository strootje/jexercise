import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public companies: Company[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Company[]>(`${baseUrl}api/v1/companies`).subscribe((companies) => {
      this.companies = companies;
    });
  }
}

export interface Company {
  id: number;
  name: string;
  address: Address;
}

export interface Address {
  street: string;
  city: string;
  zipcode: string;
  country: string;
}
