import { Component, OnInit } from '@angular/core';
import { VoyageRequestService } from '../voyageRequest.service';

@Component({
  selector: 'app-privateVoyages',
  templateUrl: './privateVoyages.component.html',
  styleUrls: ['./privateVoyages.component.css'],
})
export class PrivateVoyagesComponent implements OnInit {
  constructor(public service: VoyageRequestService) {}

  ngOnInit() {
    this.service.getCustomVoyage();
  }
}
