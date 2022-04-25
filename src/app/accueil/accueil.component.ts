import { Component, OnInit } from '@angular/core';
import { Voyage } from '../Voyage';
import { VoyageRequestService } from '../voyageRequest.service';

@Component({
  selector: 'app-accueil',
  templateUrl: './accueil.component.html',
  styleUrls: ['./accueil.component.css'],
})
export class AccueilComponent implements OnInit {
  voyagesPublic: Voyage[] = [];

  constructor(public service: VoyageRequestService) {}

  ngOnInit(): void {
    this.service.getVoyagePublic().subscribe((res) => {
      res.forEach((v: any) => {
        var newVoyage = new Voyage(v.pays, v.photo);
        this.voyagesPublic.push(newVoyage);
      });

      console.log(this.voyagesPublic);
    });
  }
}
