import { Component, OnInit, Input, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DragulaService } from 'ng2-dragula';
import { Subscription } from 'rxjs';

import { UserService } from 'src/app/services/user.service';
import { DataService } from 'src/app/services/data.service';
import { EmailsModalComponent } from 'src/app/components/modals/emails-modal/emails-modal.component';
import { User } from 'src/app/interfaces/user';


@Component({
  selector: 'app-db-table',
  templateUrl: './db-table.component.html',
  styleUrls: ['./db-table.component.scss']
})
export class DbTableComponent implements OnInit, OnDestroy {
  columns = ['ID', 'Name', 'Surname', 'Birth Date', 'Age', 'Emails', 'Transfer'];
  columnNames: ['ID', 'firstName', 'lastName', 'birthDate', 'age', 'emails'];
  isSql: boolean;
  @Input() transferString: string;
  @Input() emailString: string;
  @Input() userString: string;
  @Input() neighbourTable: string;
  @Input() tableName: string;
  @Input() tableHeader: string;
  @ViewChild('tbody', {static: false}) tbody: ElementRef;
  subs = new Subscription();

  constructor(private httpService: UserService, protected data: DataService,
              private modalService: NgbModal, private dragulaService: DragulaService) {
    const group = this.dragulaService.find('COPYABLE');
    if (group === undefined) {
      this.dragulaService.createGroup('COPYABLE', {
        accepts: ((el, target, source, sibling) => {
          if (this.data[this.neighbourTable].some((e: User) => e.id ===
              el.firstElementChild.innerHTML)) {
            return false;
          }
          return true;
        }),
        copy: true
      });
    }
    this.subs.add(this.dragulaService.drop('COPYABLE')
      .subscribe(({ el, target, source }) => {
        setTimeout(() => {
          const matches = document.querySelectorAll('tr[class]');
          const matchesArray = Array.prototype.slice.call(matches);
          matchesArray.forEach((element: Node) => {
            element.parentNode.removeChild(element);
          });
        }, 100);

        if (target === null || target === this.tbody.nativeElement) {
          return;
        }
        const query = this.transferString + '/' + el.firstElementChild.innerHTML;

        this.httpService.transferUser(query, this.neighbourTable);
        el = target = null;
      })
    );
  }

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.httpService.fetchUsers(this.userString, this.tableName);
  }

  deleteUser(index: number) {
    const query = this.userString + '/' + this.data[this.tableName][index].id;

    this.httpService.deleteUser(query, this.tableName, index);
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

  ngOnDestroy() {
    this.subs.unsubscribe();
  }
}
