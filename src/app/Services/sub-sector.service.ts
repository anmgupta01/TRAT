import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Element } from '../Models/element';
import { Employee } from '../Models/leave-types';
import { Sector } from '../Models/sector';

@Injectable({
  providedIn: 'root'
})
export class SubSectorService {
  constructor(public http: HttpClient) { }
  private readonly url = "https://indijvtrat.deloitte.com/TRAT_TESTAPI/api";
  
  getTableDetails(id:any): Observable<any>{
    return this.http.get<Element[]>(this.url +"/SubSectorController/GetSubSectorbySectorId/"+id);
  }
  
  updateData(obj:any): Observable<any>{
    return this.http.put<boolean>(this.url +"/SubSectorController/UpdateSubSector/",obj);
  }

  addData(obj:any,id?:any): Observable<any>{
    if(id){
      obj.sectorId = id;
    }
    return this.http.post<boolean>(this.url +"/SubSectorController/AddSubSector/",obj);
  }
}