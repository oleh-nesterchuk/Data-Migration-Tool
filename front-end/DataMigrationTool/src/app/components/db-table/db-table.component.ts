import { Component, OnInit, Input } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DragulaService } from 'ng2-dragula';

import { UserService } from 'src/app/services/user.service';
import { DataService } from 'src/app/services/data.service';
import { EmailsModalComponent } from 'src/app/components/modals/emails-modal/emails-modal.component';


@Component({
  selector: 'app-db-table',
  templateUrl: './db-table.component.html',
  styleUrls: ['./db-table.component.scss']
})
export class DbTableComponent implements OnInit {
  columns = ['ID', 'Name', 'Surname', 'Birth Date', 'Age', 'Emails', 'Transfer'];
  columnNames: ['ID', 'firstName', 'lastName', 'birthDate', 'age', 'emails'];
  isSql: boolean;
  transferString: string;
  emailString: string;
  userString: string;
  neighbourTable: string;
  @Input() tableName: string;
  @Input() tableHeader: string;

  constructor(private httpService: UserService,
              protected data: DataService,
              private modalService: NgbModal,
              private dragulaService: DragulaService) {
                const group = this.dragulaService.find('COPYABLE');
                if (group === undefined) {
                  this.dragulaService.createGroup('COPYABLE', {
                    copy: true
                  });
                }
              }

  ngOnInit() {
    if (this.tableHeader.includes('SQL')) {
      this.isSql = true;
      this.transferString = 'SendToMongoDb';
      this.emailString = 'SqlServerEmail';
      this.userString = 'SqlServerUser';
      this.neighbourTable = 'mongoUsers';
    } else {
      this.isSql = false;
      this.transferString = 'SendToSqlServer';
      this.emailString = 'MongoDbEmail';
      this.userString = 'MongoDbUser';
      this.neighbourTable = 'sqlUsers';
    }

    this.loadUsers();
  }

  loadUsers() {
    this.httpService.fetchUsers(this.userString, this.tableName);
  }

  transfer(index: number) {
    const query = this.transferString + '/' + this.data[this.tableName][index].id;

    this.httpService.transferUser(query, this.neighbourTable);
  }

  loadEmails(index: number) {
    const query = this.emailString + '?id=' + this.data[this.tableName][index].id;
    this.httpService.fetchEmails(query);

    this.modalService.open(EmailsModalComponent);
  }
}
