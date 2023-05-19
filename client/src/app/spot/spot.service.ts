import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Spot } from '../interfaces/Spot';
import { PagedResponse } from '../interfaces/PagedResponse';

@Injectable({
  providedIn: 'root',
})
export class SpotService {
  private apiURL = '/api/v1';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<PagedResponse<Spot>> {
    return this.httpClient
      .get<PagedResponse<Spot>>(this.apiURL + '/spot')
      .pipe(catchError(this.errorHandler));
  }

  create(Spot: Spot): Observable<Spot> {
    return this.httpClient
      .post<Spot>(this.apiURL + '/spot', JSON.stringify(Spot), this.httpOptions)
      .pipe(catchError(this.errorHandler));
  }

  find(id: string): Observable<Spot> {
    return this.httpClient
      .get<Spot>(this.apiURL + '/spot/' + id)
      .pipe(catchError(this.errorHandler));
  }

  update(id: string, Spot: Spot): Observable<Spot> {
    return this.httpClient
      .put<Spot>(
        this.apiURL + '/spot' + id,
        JSON.stringify(Spot),
        this.httpOptions
      )
      .pipe(catchError(this.errorHandler));
  }

  delete(id: string) {
    return this.httpClient
      .delete<Spot>(this.apiURL + '/spot' + id, this.httpOptions)
      .pipe(catchError(this.errorHandler));
  }

  errorHandler(error: {
    error: { message: string };
    status: any;
    message: any;
  }) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }
}
