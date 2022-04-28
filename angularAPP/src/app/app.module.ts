import { Component, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { CardComponent } from './card/card.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AccueilComponent } from './accueil/accueil.component';
import { RouterModule } from '@angular/router';
import { SigninComponent } from './signin/signin.component';
import { FormsModule } from '@angular/forms';
import { PrivateVoyagesComponent } from './privateVoyages/privateVoyages.component';
import { SignupComponent } from './signup/signup.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { MonInterceptorInterceptor } from './mon-interceptor.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CardComponent,
    AccueilComponent,
    SigninComponent,
    PrivateVoyagesComponent,
    SignupComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/accueil', pathMatch: 'full' },
      { path: 'accueil', component: AccueilComponent },
      { path: 'signin', component: SigninComponent },
      { path: 'myVoyages', component: PrivateVoyagesComponent },
      { path: 'signup', component: SignupComponent },
    ]),
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MonInterceptorInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
