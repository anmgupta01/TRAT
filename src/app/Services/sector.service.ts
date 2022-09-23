import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Element } from '../Models/element';

@Injectable({
  providedIn: 'root'
})
export class SectorService {
  constructor(public http: HttpClient) { }
  private readonly url = "https://indijvtrat.deloitte.com/TRAT_TESTAPI/api";

  getTableDetails(): Observable<any>{
    return this.http.get<Element[]>(this.url+"/SectorController/GetAllSectors");
  }
  
  updateData(obj:any): Observable<any>{
    return this.http.put<Element>(this.url +"/SectorController/UpdateSector/",obj);
  }

  addData(obj:any): Observable<any>{
    return this.http.post<Element>(this.url +"/SectorController/AddSector/",obj);
  }
}