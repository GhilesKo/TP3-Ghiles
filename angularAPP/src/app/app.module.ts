import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { CardComponent } from './card/card.component';
import { HttpClientModule } from '@angular/common/http';
import { AccueilComponent } from './accueil/accueil.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CardComponent,
    AccueilComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/accueil', pathMatch: 'full' },
      { path: 'accueil', component: AccueilComponent },
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
