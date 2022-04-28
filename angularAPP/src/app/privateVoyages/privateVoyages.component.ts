import { Component, OnInit } from '@angular/core';
import { Voyage } from '../Voyage';
import { VoyageCustom } from '../VoyageCustom';
import { VoyageRequestService } from '../voyageRequest.service';

@Component({
  selector: 'app-privateVoyages',
  templateUrl: './privateVoyages.component.html',
  styleUrls: ['./privateVoyages.component.css'],
})
export class PrivateVoyagesComponent implements OnInit {
  voyageUser: VoyageCustom[] = [];
  paysVoyage: string = '';
  isPublic: boolean = false;
  userEmail?: string;
  constructor(public service: VoyageRequestService) {}

  ngOnInit() {
    this.service.getCustomVoyage().subscribe((res) => {
      res.forEach((v: VoyageCustom) => {
        var voyage = new VoyageCustom(v.id, v.pays, v.photo);
        this.voyageUser.push(voyage);
      });
      console.log(res);
    });
  }

  AjouterVoyage() {
    this.service.addVoyage(this.paysVoyage, this.isPublic).subscribe((res) => {
      console.log(res.pays);

      var voyage = new VoyageCustom(res.id, res.pays, res.photo);
      this.voyageUser.push(voyage);
    });
  }
  async inviteUser(voyageId: number) {
    let text;
    let person = prompt('Please enter the user e-mail :', 'user@example.com');
    if (person == null || person == '') {
      alert('Please enter a valid e-mail');
    } else {
      await this.service
        .inviteUser(voyageId, person)
        .then((res) => {
          alert('User added successfully');
        })
        .catch((err) => {
          alert(err.error);
        });
    }
  }

  deleteVoyage(voyageId: number) {
    this.service.deleteVoyage(voyageId).subscribe(
      (res) => {
        console.log(res);
        let index = this.voyageUser.findIndex((v) => v.id === voyageId); //find index in your array
        this.voyageUser.splice(index, 1); //remove element from array
      },
      (err) => {
        console.log(err);
      }
    );
  }

  test() {
    console.log(this.paysVoyage);
    console.log(this.isPublic);
  }
}
