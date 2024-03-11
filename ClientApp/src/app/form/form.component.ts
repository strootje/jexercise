import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BackendService, Company } from '../BackendService';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
})
export class FormComponent {
  createOfferForm: FormGroup;
  @Input() companies!: Company[];

  constructor(
    formBuilder: FormBuilder,
    private backend: BackendService,
  ) {
    this.createOfferForm = formBuilder.group({
      title: '',
      companyId: 0,
      description: '',
    });
  }

  public onSubmit(): void {
    const values = this.createOfferForm.value;
    this.backend.newJobOffer(values.companyId, values.title, values.description).subscribe(console.log);
  }
}
