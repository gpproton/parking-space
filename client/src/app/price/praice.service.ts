import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PagedResponse } from '../interfaces/PagedResponse';
import { Price } from '../interfaces/Price';

@Injectable({
  providedIn: 'root',
})
export class PriceService {
  private apiURL = '/api/v1';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<PagedResponse<Price>> {
    return this.httpClient
      .get<PagedResponse<Price>>(this.apiURL + '/price?page-size=25&page=1')
      .pipe(catchError(this.errorHandler));
  }

  create(space: Price): Observable<Price> {
    return this.httpClient
      .post<Price>(
        this.apiURL + '/price',
        JSON.stringify(space),
        this.httpOptions
      )
      .pipe(catchError(this.errorHandler));
  }

  find(id: string): Observable<Price> {
    return this.httpClient
      .get<Price>(this.apiURL + '/price/' + id)
      .pipe(catchError(this.errorHandler));
  }

  update(id: string, space: Price): Observable<Price> {
    return this.httpClient
      .put<Price>(
        this.apiURL + '/price' + id,
        JSON.stringify(space),
        this.httpOptions
      )
      .pipe(catchError(this.errorHandler));
  }

  delete(id: string) {
    return this.httpClient
      .delete<Price>(this.apiURL + '/price' + id, this.httpOptions)
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
