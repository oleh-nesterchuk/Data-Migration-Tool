import { Injectable } from '@angular/core';
import { User } from '../interfaces/user';
import { Email } from '../interfaces/email';


@Injectable({
  providedIn: 'root'
})
export class DataService {
  sqlUsers: User[];
  mongoUsers: User[];
  emails: Email[];
  deleteMode: boolean;
  editMode: boolean;

  constructor() { }
}
