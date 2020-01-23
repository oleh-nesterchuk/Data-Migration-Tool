import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

import { User } from '../interfaces/user';
import { Email } from '../interfaces/email';
import { DataService } from './data.service';


@Injectable({
  providedIn: 'root'
})
export class RequestService {

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

  deleteUser(parameters: string): Observable<any> {
    return this.http.delete(environment.connection + parameters);
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

  deleteEmail(parameters: string): Observable<any> {
    return this.http.delete(environment.connection + parameters);
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
