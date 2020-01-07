import { Email } from './email';


export interface User {
  id: string;
  firstName: string;
  lastName: string;
  birthDate: Date;
  age: number;
  emails: Email[];
}
