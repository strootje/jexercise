import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CardComponent } from './card/card.component';
import { FormComponent } from './form/form.component';
import { BadgeComponent } from './badge/badge.component';

@NgModule({
  declarations: [AppComponent, HomeComponent, CardComponent, FormComponent, BadgeComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([{ path: '', component: HomeComponent, pathMatch: 'full' }]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
