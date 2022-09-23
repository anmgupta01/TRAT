import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { map,catchError, Observable } from 'rxjs';
import { throwError} from 'rxjs';
import { NotificationService } from '../Services/notification.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private service:NotificationService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
        catchError((error: HttpErrorResponse) => {
          const errorMessage = this.setError(error);
        console.log(error);
        this.service.showError(errorMessage,"Error");
        return throwError(() => new Error(errorMessage));
  })
  );
}

 setError(error:HttpErrorResponse):string{
   let errorMessage ='Unknown error occured';
   if(error.error instanceof ErrorEvent)
   {
      //Client Side error
      errorMessage = error.error.message;
   }
   else{
     //server side error
     if(error.status !==0)
     {
      errorMessage = error.error;
     }
     else{
      errorMessage = "Server is Down";
     }
   }
   return errorMessage; 
 }
}

