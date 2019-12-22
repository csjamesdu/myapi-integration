import {
    HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor,
    HttpRequest
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from 'rxjs/operators';
import {Router} from "@angular/router";


@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((error) => {
                if (error instanceof HttpErrorResponse && error.status == 404) {
                    this.router.navigateByUrl('error', {replaceUrl: true});
                }
                else
                  return throwError(error);
            })) as any;
    }
}
