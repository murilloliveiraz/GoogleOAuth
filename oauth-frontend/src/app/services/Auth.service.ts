import { Injectable } from '@angular/core';
import { enviroment } from '../../enviroment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private path = enviroment.apiURL;

  constructor(private httpClient: HttpClient) { }

  signOutexternal(){
    localStorage.removeItem("token");
    console.log("token removed");
  }

  LoginWithGoogle(credentials: string): Observable<any> {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post(this.path + "Auth/LoginWithGoogle", JSON.stringify(credentials), {headers: header});
  }
}
