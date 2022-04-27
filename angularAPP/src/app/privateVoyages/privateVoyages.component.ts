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
  paysVoyage:string="";
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


  AjouterVoyage(){this.service.addVoyage(this.paysVoyage,this.isPublic).subscribe(res=>{

    console.log(res.pays);

    var voyage = new Voyage(res.pays,res.photo);
    this.voyageUser.push(voyage);


  })



  }
  inviteUser(){

    let text;
    let person = prompt("Please enter the user e-mail :", "user@example.com");
    if (person == null || person == "") {
     alert("Please enter a valid e-mail")
    } else {
     //send request with email given
    }


  }

  test(){

    console.log(this.paysVoyage);
    console.log(this.isPublic);


    }



}
