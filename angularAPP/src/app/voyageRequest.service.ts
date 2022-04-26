import { HttpClient } from '@angular/common/http';
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
  errMsg: string = '';
  token?: string;
  isSuccesful: boolean = false;

  constructor(public http: HttpClient, private route: Router) {}
  ngOnInit(): void {
    this.getVoyagePublic();
  }

  voyagesPublic: Voyage[] = [];

  getVoyagePublic() {
    return this.http.get<any>('http://localhost:16029/api/Voyages/Public');
  }

  signInRequest(username: string, password: string) {
    this.http
      .post<any>('http://localhost:16029/api/Account/signIn', {
        UserName: username,
        Password: password,
      })
      .subscribe(
        (res) => {
          this.route.navigate(['']);
          this.isSuccesful = true;
          if (this.isSuccesful === true) {
            console.log('success');
          } else {
            console.log('fail');
          }
          this.token = res.token;
          console.log(this.token);
          this.errMsg = '';
        },
        (err) => {
          this.isSuccesful = false;
          console.log(err.error);
          this.errMsg = err.error;
        }
      );
  }
}
