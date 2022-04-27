import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccueilComponent } from './accueil/accueil.component';
import { Voyage } from './Voyage';
@Injectable({
  providedIn: 'root',
})
export class VoyageRequestService implements OnInit {
  errMsg?: string;

  tokenstored = localStorage.getItem('token');

  constructor(public http: HttpClient, public route: Router) {}

  ngOnInit(): void {}

  voyagesPublic: Voyage[] = [];
  voyagePrivate: Voyage[] = [];


  getPublicVoyages() {
    return this.http.get<any>('http://localhost:16029/api/Voyages/Public');
  }

  getCustomVoyage() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.tokenstored
      })
    };
    return this.http.get<any>('http://localhost:16029/api/Voyages/Custom',httpOptions)
  }

  async signInRequest(username: string, password: string) {
    return await this.http
      .post<any>('http://localhost:16029/api/Account/signIn', {
        UserName: username,
        Password: password,
      })
      .toPromise()
      .then((res) => {
        this.errMsg = undefined;
        this.tokenstored = res.token;
        localStorage.setItem('token', res.token);
        this.route.navigate(['']);
      })
      .catch((err) => {
        console.log(err.error);
        this.errMsg = err.error;
      });
  }
}
