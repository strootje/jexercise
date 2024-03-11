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

  onSubmit(): void {
    const values = this.createOfferForm.value;
    if (this.createOfferForm.valid) {
      this.backend.newJobOffer(values.companyId, values.title, values.description).subscribe(console.log);
    }
  }

  get title() {
    return this.createOfferForm.get('title');
  }

  get companyId() {
    return this.createOfferForm.get('companyId');
  }

  get description() {
    return this.createOfferForm.get('description');
  }
}
