import { Inject, Injectable } from '@angular/core';
import { Headers, RequestOptions, Response } from '@angular/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import { catchError } from 'rxjs/operators';


@Injectable()
export class AppService {

  constructor(private _http: HttpClient) { }

  //#region PUBLIC METHODS
  public get(url: string): Observable<any> {
    return this._http.get<any>(url) // Specify the response type as `any`
      .catch(this.handleError);
  }

  //public save(url: string, data: any): Observable<any> {
  //  let headers = new Headers({ 'Content-Type': 'application/json' });
  //  let options = new RequestOptions({ headers: headers});
  //  return this._http.post(url, data, options)
  //    .map((response: Response) => <any>response.json())
  //    .catch(error => this.handleError(error));
  //}

  public save(url: string, data: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this._http.post<any>(url, data, httpOptions)
      .catch(this.handleError);
  }
  //#endregion

  private handleErrorWithZero(error: Response) {
    console.error('service API Error:', error);
    return Observable.of(0);
  }

  handleError(error: Response) {
    console.error('service API Error:', error);
    return Observable.throw(error);
  }
}
