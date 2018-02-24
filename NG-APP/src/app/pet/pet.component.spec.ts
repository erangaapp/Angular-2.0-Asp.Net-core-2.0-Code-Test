import {
    async, ComponentFixture, fakeAsync, TestBed, tick
} from '@angular/core/testing';

import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { OWNERS, FakePetService } from '../services/fake-pet.service';

import { AppModule } from '../app.module';

import { PetComponent } from './pet.component';
import { PetService } from '../services/pet.service';

let comp: PetComponent;
let fixture: ComponentFixture<PetComponent>;
let page: Page;

/////// Tests //////

describe('PetComponent', () => {

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [PetComponent],
            providers: [
                { provide: PetService, useClass: FakePetService }
            ]
        })
            .compileComponents()
            .then(createComponent);
    }));

    it('should display owners gender', () => {

        expect(page.ownerGenderRows.length).toBeGreaterThan(0);

    });

    it('should display pets of owners gender', () => {

        expect(page.petsRows.length).toBeGreaterThan(0);

    });


    it('1st owner gender should be `Male`', () => {

        const expectedOwner = OWNERS[0];
        const actualGender = page.ownerGenderRows[0].textContent;
        expect(actualGender).toContain(expectedOwner.gender.toString(), 'Male');

    });

    it('2nd owner gender should be `Female`', () => {

        const expectedOwner = OWNERS[0];
        const actualGender = page.ownerGenderRows[0].textContent;
        expect(actualGender).toContain(expectedOwner.gender.toString(), 'Female');

    });

    it('total `Cats` should be 7', () => {

        const expectedOwnerPetsCount = OWNERS[0].pets.length + OWNERS[1].pets.length;
        const actualCount = page.petsRows.length;
        expect(actualCount).toEqual(expectedOwnerPetsCount);

    });

    it('should header elements with css class `badge`', () => {
        const h2 = page.ownerGenderRows[0];
        const className = h2.className;
        expect(className).toContain('badge');
    });

    it('should ul elements with css class `list-group`', () => {
        const ul = page.ulControls[0];
        const className = ul.className;
        expect(className).toBe('list-group');
    });

});

/////////// Helpers /////

/** Create the component and set the `page` test variables */
function createComponent() {

    fixture = TestBed.createComponent(PetComponent);
    comp = fixture.componentInstance;

    // change detection triggers ngOnInit which gets a pet
    fixture.detectChanges();

    return fixture.whenStable().then(() => {
        // got the pets and updated component
        // change detection updates the view
        fixture.detectChanges();
        page = new Page();

    });
}

class Page {

    /** Gender eliments line elements */
    ownerGenderRows: HTMLHeadElement[];

    /** Pets eliments line elements */
    petsRows: HTMLLIElement[];

    /** Pets eliments line elements */
    ulControls: HTMLUListElement[];

    constructor() {

        this.ownerGenderRows = fixture.debugElement.queryAll(By.css('h2')).map(de => de.nativeElement);
        this.petsRows = fixture.debugElement.queryAll(By.css('li')).map(de => de.nativeElement);
        this.ulControls = fixture.debugElement.queryAll(By.css('ul')).map(de => de.nativeElement);

    };
}


