import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router:Router) { }

canActivate()
{

let isManager = sessionStorage.getItem('IsManager');
if(isManager?.toLowerCase() == "true"){
return true;
}
else{
alert("You are not authorized to this page")
this.router.navigate(["/Leave"])
return false;
}


}
  
}