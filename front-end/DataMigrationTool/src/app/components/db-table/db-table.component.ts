import { Component, OnInit, Input } from '@angular/core';

import { User } from 'src/app/interfaces/user';
import { UserService } from 'src/app/services/user.service';
import { DataService } from 'src/app/services/data.service';


@Component({
  selector: 'app-db-table',
  templateUrl: './db-table.component.html',
  styleUrls: ['./db-table.component.scss']
})
export class DbTableComponent implements OnInit {
  columns = ['ID', 'Name', 'Surname', 'Birth Date', 'Age', 'Emails', 'Transfer'];
  columnNames: ['ID', 'firstName', 'lastName', 'birthDate', 'age', 'emails'];
  isSql: boolean;
  @Input() tableName: string;
  @Input() tableHeader: string;
  @Input() queryParams: string;

  constructor(private getSql: UserService,
              protected data: DataService) { }

  ngOnInit() {
    this.isSql = this.tableHeader.includes('SQL') ? true : false;
    this.getSql.getUsers(this.queryParams).subscribe(data => {
      this.data[this.tableName] = data as User[];
    });
  }

  transfer(index: number) {
    let destination = this.isSql ? 'SendToMongoDb' : 'SendToSqlServer';
    destination += '/' + this.data[this.tableName][index].id;
    const neighbour = this.tableName === 'sqlUsers' ? 'mongoUsers' : 'sqlUsers';
    this.getSql.getUsers(destination).subscribe(data => {
      this.data[neighbour].push(data as User);
    }, data => {
      alert(data.error);
    });
  }

  loadEmails(index: number) {

  }
}
