import { Component, OnInit } from '@angular/core';
import { VoyageRequestService } from '../voyageRequest.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent implements OnInit {
  email: string = '';
  password: string = '';
  constructor(public service: VoyageRequestService) {}

  ngOnInit() {}

  register() {
    this.service.register(this.email, this.password);
  }
}
