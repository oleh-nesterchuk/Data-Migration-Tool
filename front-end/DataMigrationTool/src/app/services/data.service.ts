import { Injectable } from '@angular/core';
import { User } from '../interfaces/user';


@Injectable({
  providedIn: 'root'
})
export class DataService {
  sqlUsers: User[];
  mongoUsers: User[];

  constructor() { }
}
