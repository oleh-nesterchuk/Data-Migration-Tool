import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../interfaces/user';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  users: User[];
  constructor(private http: HttpClient) { }

  getUsers(parameters: string) {
    const query = environment.connection + parameters;
    return this.http.get(query);
  }
}
