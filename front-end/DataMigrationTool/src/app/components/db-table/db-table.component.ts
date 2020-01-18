import { Component, OnInit, Input, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DragulaService } from 'ng2-dragula';
import { Subscription } from 'rxjs';

import { UserService } from 'src/app/services/user.service';
import { DataService } from 'src/app/services/data.service';
import { EmailsModalComponent } from 'src/app/components/modals/emails-modal/emails-modal.component';
import { EditUserModalComponent } from '../modals/edit-user-modal/edit-user-modal.component';


@Component({
  selector: 'app-db-table',
  templateUrl: './db-table.component.html',
  styleUrls: ['./db-table.component.scss']
})
export class DbTableComponent implements OnInit, OnDestroy {
  columns = ['ID', 'Name', 'Surname', 'Birth Date', 'Age', 'Emails', 'Transfer'];
  columnNames: ['ID', 'firstName', 'lastName', 'birthDate', 'age', 'emails'];
  @Input() transferString: string;
  @Input() emailString: string;
  @Input() userString: string;
  @Input() neighbourTable: string;
  @Input() tableName: string;
  @Input() tableHeader: string;
  @ViewChild('tbody', {static: false}) tbody: ElementRef;
  isLoading: boolean;
  errorMessage: string;
  subs = new Subscription();

  constructor(private httpService: UserService, protected dataService: DataService,
              private modalService: NgbModal, private dragulaService: DragulaService) {
    const group = this.dragulaService.find('COPYABLE');
    if (group === undefined) {
      this.dragulaService.createGroup('COPYABLE', {
        copy: true,
        moves: () => {
          return !(this.dataService.editMode || this.dataService.deleteMode);
        }
      });
    } 
  }

  ngOnInit() {
    this.subs.add(this.dragulaService.drop('COPYABLE')
      .subscribe(({ el, target }) => {
        if (target === null || target === this.tbody.nativeElement) {
          this.clearDragulasTable();
          return;
        }
        const query = this.transferString + '/' + el.firstElementChild.innerHTML;
        this.httpService.transferUser(query, this.neighbourTable);
      })
    );
    this.loadUsers();
  }

  loadUsers() {
    this.isLoading = true;
    this.errorMessage = null;
    this.httpService.fetchUsers(this.userString).subscribe(data => {
      this.dataService[this.tableName] = data;
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      this.errorMessage = error.message;
    });
  }

  editUser(index: number) {
    const query = this.userString + '/' + this.dataService[this.tableName][index].id;

    const modalRef = this.modalService.open(EditUserModalComponent);
    modalRef.componentInstance.query = query;
    modalRef.componentInstance.userIndex = index;
    modalRef.componentInstance.table = this.tableName;
  }

  deleteUser(index: number) {
    const query = this.userString + '/' + this.dataService[this.tableName][index].id;

    this.httpService.deleteUser(query, this.tableName, index);
  }

  transfer(index: number) {
    const query = this.transferString + '/' + this.dataService[this.tableName][index].id;

    this.httpService.transferUser(query, this.neighbourTable);
  }

  loadEmails(index: number) {
    const query = this.emailString + '?id=' + this.dataService[this.tableName][index].id;
    this.httpService.fetchEmails(query);

    this.modalService.open(EmailsModalComponent);
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

  private clearDragulasTable() {
    setTimeout(() => {
      const matches = document.querySelectorAll('tr[class]');
      const matchesArray = Array.prototype.slice.call(matches);
      matchesArray.forEach((element: Node) => {
        element.parentNode.removeChild(element);
      });
    }, 10);
  }
}
