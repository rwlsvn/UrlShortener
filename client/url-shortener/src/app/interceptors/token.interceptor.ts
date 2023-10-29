import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { TokenModel } from '../models/auth/token.model';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private auth: AuthService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.auth.getToken();
    request = request.clone({
      setHeaders: {Authorization: `Bearer ${token}`}
    });

    return next.handle(request).pipe(
      catchError((err: any) => {
        if (err instanceof HttpErrorResponse){
          if(err.status === 401 || err.status === 403)
            return this.handleUnauthorizedError(request, next);
        }
        return throwError(() => Error(err.error.message));
      })
    );
  }

  handleUnauthorizedError(request: HttpRequest<any>, next: HttpHandler){
    const tokenModel: TokenModel ={
      accessToken: this.auth.getToken()!,
      refreshToken: this.auth.getRefreshToken()!
    };
    return this.auth.refreshToken(tokenModel).pipe(
      switchMap((data: TokenModel) => {
        console.log()
        this.auth.setRefreshToken(data.refreshToken);
        this.auth.setToken(data.accessToken);
        request = request.clone({
          setHeaders: {Authorization: `Bearer ${data.accessToken}`}
        });
        return next.handle(request);
      }),
      catchError(err => {
        return throwError(() => {
          this.auth.signOut();
          this.router.navigate(['sign-in']);
        })      
      })
    );
  }
}
