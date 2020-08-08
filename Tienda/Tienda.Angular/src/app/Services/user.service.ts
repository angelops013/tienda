import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) {
  }

  SaveUser(user: User) {
    localStorage.setItem('userdata', JSON.stringify(user));
  }

  RemoveUser() {
    localStorage.clear();
  }

  GetUser(): User {
    return (JSON.parse(localStorage.getItem('userdata')) as User);
  }

  GetJWTSessionValidation() {
    const header = { 'Authorization': "Bearer " + this.GetUser().token };
    const configuration = { headers: header };
    return this.http.get(environment.baseUrl + 'api/Users/ValidateToken', configuration);
  }

  ValidateUser() {
    let user = this.GetUser();
    if (user === null || user === undefined) {
      return true;
    } else {
      return false;
    }
  }

}
