import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../interfaces/user';
import { environment } from 'src/environments/environment';

import { DataService } from './data.service';
import { Email } from '../interfaces/email';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  users: User[];

  constructor(private http: HttpClient, private dataService: DataService) { }

  addUser(parameters: string, user: User) {
    const query = environment.connection + parameters;
    const database = parameters.includes('Sql') ? 'sqlUsers' : 'mongoUsers';
    this.http
      .post<User>(query, user)
      .subscribe(data => {
        this.dataService[database].push(data);
      }, error => {
        console.log(error.error.title);
      });
  }

  fetchUsers(parameters: string): Observable<User[]> {
    const query = environment.connection + parameters;

    return this.http.get<User[]>(query);
  }

  editUser(parameters: string, user: User, database: string, index: number) {
    const query = environment.connection + parameters;

    this.http
      .put<User>(query, user)
      .subscribe(data => {
        this.dataService[database][index] = data;
      });
  }

  deleteUser(parameters: string, destination: string, index: number) {
    const query = environment.connection + parameters;

    this.http
      .delete<User>(query)
      .subscribe(() => {
        this.dataService[destination].splice(index, 1);
      });
  }

  transferUser(parameters: string, destination: string) {
    const query = environment.connection + parameters;

    this.http
      .get<User>(query)
      .subscribe(data => {
        this.dataService[destination].push(data);
      }, data => {
        alert(data.error);
      });
  }

  fetchEmails(parameters: string) {
    const query = environment.connection + parameters;

    this.http
      .get<Email[]>(query)
      .subscribe(data => {
        this.dataService.emails = data;
      });
  }
}
