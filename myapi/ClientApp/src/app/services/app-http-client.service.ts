
import { Injectable } from '@angular/core';

import { HttpClient, HttpHandler } from '@angular/common/http';

import { Observable, of } from 'rxjs';

import { switchMap } from 'rxjs/internal/operators';

@Injectable()
export class AppHttpClient extends HttpClient {

  constructor(handler: HttpHandler) {
    super(handler);
  }

  get(url: string): Observable<any> {
    console.log('request url: ' + url);
    return this.getWithProcess(url);
  }

  getWithProcess(url: string): Observable<any> {
    return super.get(url).pipe(switchMap(source => {
      return of(source);
    }));
  }
}
