
import { Injectable } from '@angular/core';

import { HttpClient, HttpHandler } from '@angular/common/http';

import { Observable, of } from 'rxjs';

import { switchMap } from 'rxjs/internal/operators';

//const API_URL = 'https://myapi-movie-world.azurewebsites.net/';
const API_URL = 'https://localhost:5001/';

@Injectable()
export class AppHttpClient extends HttpClient {

  constructor(handler: HttpHandler) {
    super(handler);
  }

  get(url: string): Observable<any> {
    return this.getWithProcess(url);
  }

  getWithProcess(url: string): Observable<any> {
    return super.get(API_URL + url).pipe(switchMap(source => {
      return of(source);
    }));
  }
}
