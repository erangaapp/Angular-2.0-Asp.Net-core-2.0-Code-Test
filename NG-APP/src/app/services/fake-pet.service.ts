import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';

// re-export for tester convenience
export { Pet } from '../models/pet';
export { Owner } from '../models/owner';
export { PetService } from '../services/pet.service';

import { Pet } from '../models/pet';
import { Owner } from '../models/owner';
import { PetService } from '../services/pet.service';

export const OWNERS: Owner[] = [
  { gender: "Male", pets: [{ type: "Cat", name: "Garfield" }, { type: "Cat", name: "Tom" }, { type: "Cat", name: "Max" }, { type: "Cat", name: "Jim" }] },
  { gender: "Female", pets: [{ type: "Cat", name: "Simba" }, { type: "Cat", name: "Garfield" }, { type: "Cat", name: "Tabby" }] },
];

export class FakePetService {

  owners = OWNERS;

  getPetsByPetType(petType: string): Observable<Owner[]> {

    let _owneres = this.owners;
    return Observable.of(_owneres);
  }

}
