import { Component, OnInit } from '@angular/core';
import { VoyageRequestService } from '../voyageRequest.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor(public service: VoyageRequestService) {}

  ngOnInit() {}

  Logout() {
    localStorage.removeItem('token');
    this.service.tokenstored = null;
  }
}
