import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { forkJoin } from "rxjs/observable/forkJoin";
import { catchError, map, tap } from 'rxjs/operators';

import { Owner } from '../models/owner';

import { MessageService } from '../services/message.service';
import { SharedService } from '../services/shared.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class PetService {

  constructor(private http: HttpClient,
    private messageService: MessageService,
    private sharedService: SharedService) {
  }

  /** GET pets from the server */
  getPetsByPetType(petType: string): Observable<Owner[]> {
    return this.http.get<Owner[]>(`${this.sharedService.API_BASE_PATH}/Pets/${petType}`)
      .pipe(
      tap(pets => this.log(`fetched owner`)),
      catchError(this.handleError('getPetsByPetType', []))
      );
  }

  /**
 * Handle Http operation that failed.
 * Let the app continue.
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for pet consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a PetService message with the MessageService */
  private log(message: string) {
    this.messageService.add('PetService: ' + message);
  }

}
