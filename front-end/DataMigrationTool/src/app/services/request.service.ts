import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { User } from '../interfaces/user';
import { Email } from '../interfaces/email';


@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(private http: HttpClient) { }

  getUsersSize(parameters: string): Observable<number> {
    return this.http.get<number>(environment.connection + parameters + '/GetSize');
  }

  addUser(parameters: string, user: User): Observable<User> {
    return this.http.post<User>(environment.connection + parameters, user);
  }

  fetchUsers(parameters: string, pageNumber: number, pageSize: number): Observable<User[]> {
    const query = parameters + '?' + 'PageNumber=' + pageNumber + '&PageSize=' + pageSize;
    return this.http.get<User[]>(environment.connection + query);
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
