import { Component, Input, OnInit } from '@angular/core';
import { Voyage } from '../Voyage';
import { VoyageRequestService } from '../voyageRequest.service';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
})
export class CardComponent implements OnInit {
  @Input() voyage?: Voyage;

  constructor(public service: VoyageRequestService) {}
  ngOnInit() {}
}
