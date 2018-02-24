import { Component, OnInit } from '@angular/core';

import { PetService } from '../services/pet.service';

import { Owner } from '../models/owner';

@Component({
  selector: 'app-pet',
  templateUrl: './pet.component.html',
  styleUrls: ['./pet.component.css']
})
export class PetComponent implements OnInit {

  owners: Owner[] = [];

  constructor(private petService: PetService) { }

  ngOnInit() {

    this.petService.getPetsByPetType("Cat")
      .subscribe(owners =>
        this.owners = owners
      );
  }

}
