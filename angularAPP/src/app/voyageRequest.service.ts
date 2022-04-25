import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Voyage } from './Voyage';
@Injectable({
  providedIn: 'root',
})
export class VoyageRequestService implements OnInit {
  constructor(public http: HttpClient) {}
  ngOnInit(): void {
    this.getVoyagePublic();
  }

  voyagesPublic: Voyage[] = [];

  getVoyagePublic() {
    return this.http.get<any>('http://localhost:16029/api/Voyages/Public');
  }
}
