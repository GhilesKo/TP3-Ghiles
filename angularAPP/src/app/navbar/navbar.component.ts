import { Component, OnInit } from '@angular/core';
import { VoyageRequestService } from '../voyageRequest.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isSuccesful?: boolean;
  constructor(public service: VoyageRequestService) {}

  ngOnInit() {
    this.isSuccesful = this.service.isSuccesful;
  }
}
