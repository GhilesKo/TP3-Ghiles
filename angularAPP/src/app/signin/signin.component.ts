import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccueilComponent } from '../accueil/accueil.component';
import { VoyageRequestService } from '../voyageRequest.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
})
export class SigninComponent implements OnInit {
  username: string = '';
  password: string = '';
  isSuccesful: boolean = false;
  cpt: number = 0;

  constructor(public service: VoyageRequestService, public router: Router) {}

  ngOnInit(): void {}

  async signInRequest() {
    await this.service.signInRequest(this.username, this.password);
  }
}
