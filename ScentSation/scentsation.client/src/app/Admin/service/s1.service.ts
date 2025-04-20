import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class S1Service {

  constructor(private _http: HttpClient) { }

  getallusers() {
    return this._http.get<any>("https://localhost:7199/api/users/GetUsers");
  }
}
