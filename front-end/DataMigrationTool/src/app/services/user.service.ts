import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../interfaces/user';
import { environment } from 'src/environments/environment';

import { DataService } from './data.service';
import { Email } from '../interfaces/email';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  users: User[];
  constructor(private http: HttpClient, private dataService: DataService) { }

  fetchUsers(parameters: string, database: string) {
    const query = environment.connection + parameters;

    this.http
      .get<User[]>(query)
      .subscribe(data => {
        this.dataService[database] = data;
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
