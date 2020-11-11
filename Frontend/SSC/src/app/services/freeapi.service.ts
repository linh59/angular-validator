import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FreeapiService {

  constructor(private http: HttpClient) { }

  getcommnets(): Observable<any> {
    return this.http.get("http://jsonplaceholder.typicode.com/posts/1/comments");
  }


  getcommnetsbyparameter(par): Observable<any> {
    let params1 = new HttpParams().set('userId', par);
    return this.http.get("http://jsonplaceholder.typicode.com/posts", { params: params1 });
  }

}
