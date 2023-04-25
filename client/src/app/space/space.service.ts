import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Space } from '../interfaces/Space';
import { PagedResponse } from '../interfaces/PagedResponse';

@Injectable({
  providedIn: 'root',
})
export class SpaceService {
  private apiURL = '/api/v1';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<PagedResponse<Space>> {
    return this.httpClient
      .get<PagedResponse<Space>>(this.apiURL + '/space')
      .pipe(catchError(this.errorHandler));
  }

  create(space: Space): Observable<Space> {
    return this.httpClient
      .post<Space>(
        this.apiURL + '/space',
        JSON.stringify(space),
        this.httpOptions
      )
      .pipe(catchError(this.errorHandler));
  }

  find(id: string): Observable<Space> {
    return this.httpClient
      .get<Space>(this.apiURL + '/space/' + id)
      .pipe(catchError(this.errorHandler));
  }

  update(id: string, space: Space): Observable<Space> {
    return this.httpClient
      .put<Space>(
        this.apiURL + '/space' + id,
        JSON.stringify(space),
        this.httpOptions
      )
      .pipe(catchError(this.errorHandler));
  }

  delete(id: string) {
    return this.httpClient
      .delete<Space>(this.apiURL + '/space' + id, this.httpOptions)
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
