import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccueilComponent } from './accueil/accueil.component';
import { Voyage } from './Voyage';
@Injectable({
  providedIn: 'root',
})
export class VoyageRequestService implements OnInit {
  errMsg?: string;
  signUperrMsg?: string;

  tokenstored = localStorage.getItem('token');

  usernameLogged: string = '';

  constructor(
    public http: HttpClient,
    public route: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  voyagesPublic: Voyage[] = [];
  voyagePrivate: Voyage[] = [];

  getPublicVoyages() {
    return this.http.get<any>('http://localhost:16029/api/Voyages/Public');
  }

  getCustomVoyage() {
    return this.http.get<any>('http://localhost:16029/api/Voyages/Custom');
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
        this.toastr.success('Logged in Successfully!');
        this.usernameLogged = username;
      })
      .catch((err) => {
        console.log(err.error);
        this.errMsg = err.error;
      });
  }

  addVoyage(pays: string, Public: boolean) {
    return this.http.post<any>('http://localhost:16029/api/Voyages', {
      pays,
      Public,
    });
  }

  async inviteUser(voyageId: number, userEmail: string) {
    return await this.http
      .post<any>('http://localhost:16029/api/Voyages/InviteUser', {
        VoyageId: voyageId,
        UserEmail: userEmail,
      })
      .toPromise();
  }

  async register(email: string, password: string) {
    return await this.http
      .post(
        'http://localhost:16029/api/Account/register',
        {
          Email: email,
          Password: password,
        },
        { responseType: 'text' }
      )
      .toPromise()
      .then((res) => {
        this.toastr.success(res);
        this.signInRequest(email, password);
        this.route.navigate(['']);
        console.log(res);
        this.signUperrMsg = undefined;
      })
      .catch((err) => {
        console.log(err);
      });
  }

  deleteVoyage(voyageId: number) {
    return this.http.delete<any>(
      `http://localhost:16029/api/Voyages/${voyageId}`
    );
  }
}
