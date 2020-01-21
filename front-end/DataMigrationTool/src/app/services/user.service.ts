import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
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

  addUser(parameters: string, user: User): Observable<User> {
    return this.http.post<User>(environment.connection + parameters, user);
  }

  fetchUsers(parameters: string): Observable<User[]> {
    return this.http.get<User[]>(environment.connection + parameters);
  }

  editUser(parameters: string, user: User): Observable<User> {
    return this.http.put<User>(environment.connection + parameters, user)
  }

  deleteUser(parameters: string, destination: string, index: number) {
    this.http
      .delete<User>(environment.connection + parameters)
      .subscribe(() => {
        this.dataService[destination].splice(index, 1);
      });
  }

  transferUser(parameters: string): Observable<User> {
    return this.http.get<User>(environment.connection + parameters)
  }

  fetchEmails(parameters: string): Observable<Email[]> {
    return this.http.get<Email[]>(environment.connection + parameters);
  }

  addEmail(parameters: string, email: Email): Observable<Email> {
    return this.http.post<Email>(environment.connection + parameters, email);
  }

  editEmail(parameters: string, email: Email): Observable<Email> {
    return this.http.put<Email>(environment.connection + parameters, email);
  }

  deleteEmail(parameters: string, index: number) {
    this.http
      .delete<Email>(environment.connection + parameters)
      .subscribe(() => {
        this.dataService.emails.splice(index, 1);
      });
  }

  getErrorMessage(error: HttpErrorResponse): string {
    switch (error.status) {
      case 0: {
        return 'The server is not responding. Please, visit the page later.'
      }
      case 400: {
        if (typeof error.error === 'string') {
          return error.error;
        }
        return error.error.title;
      }
    }
  }
}
