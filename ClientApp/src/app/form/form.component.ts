import { HttpClient } from '@angular/common/http';
import { Component, Inject, Input } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Company } from '../home/home.component';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
})
export class FormComponent {
  public createOfferForm: FormGroup;

  @Input('companies')
  public companies!: Company[];

  constructor(
    formBuilder: FormBuilder,
    private http: HttpClient,
    @Inject('BASE_URL')
    private baseUrl: string,
  ) {
    this.createOfferForm = formBuilder.group({
      title: '',
      companyId: 0,
      description: '',
    });
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
