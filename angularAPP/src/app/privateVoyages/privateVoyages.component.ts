import { Component, OnInit } from '@angular/core';
import { Voyage } from '../Voyage';
import { VoyageRequestService } from '../voyageRequest.service';

@Component({
  selector: 'app-privateVoyages',
  templateUrl: './privateVoyages.component.html',
  styleUrls: ['./privateVoyages.component.css'],
})
export class PrivateVoyagesComponent implements OnInit {

  voyageUser:Voyage[]=[];
  paysVoyage?:string;
  isPublic:boolean=false;
  constructor(public service: VoyageRequestService) {}

  ngOnInit() {
    this.service.getCustomVoyage().subscribe(res=>{

      res.forEach((v:Voyage) => {

        var voyage = new Voyage(v.pays,v.photo);
        this.voyageUser.push(voyage);

      });
      console.log(res);




  });;
  }


  AjouterVoyage(){



  }

  test(){

    console.log(this.paysVoyage);
    if(this.isPublic===false){console.log("false");}
    else console.log("true");

    }



}
