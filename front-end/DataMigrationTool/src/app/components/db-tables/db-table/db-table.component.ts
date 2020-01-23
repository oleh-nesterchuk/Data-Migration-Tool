import { Component, OnInit, Input, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DragulaService } from 'ng2-dragula';
import { Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

import { RequestService } from 'src/app/services/request.service';
import { DataService } from 'src/app/services/data.service';
import { EmailsModalComponent } from 'src/app/components/modals/emails-modal/emails-modal.component';
import { EditUserModalComponent } from 'src/app/components/modals/edit-user-modal/edit-user-modal.component';


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

  constructor(private httpService: RequestService, protected dataService: DataService,
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
        const query = this.transferString + '/' + el.firstElementChild
          .firstElementChild.getAttribute('title');
        this.httpService.transferUser(query).subscribe(data => {
          this.dataService[this.neighbourTable].push(data);
          }, error => {
            this.errorMessage = this.httpService.getErrorMessage(error);
        });
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
      this.errorMessage = this.httpService.getErrorMessage(error);
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

    this.httpService.deleteUser(query).subscribe(() => {
      this.dataService[this.tableName].splice(index, 1);
    });
  }

  transfer(index: number) {
    const query = this.transferString + '/' + this.dataService[this.tableName][index].id;

    this.httpService.transferUser(query).subscribe(data => {
      this.dataService[this.neighbourTable].push(data);
    }, error => {
      this.errorMessage = this.httpService.getErrorMessage(error);
    });
  }

  loadEmails(index: number) {
    const modalRef = this.modalService.open(EmailsModalComponent);
    modalRef.componentInstance.emailString = this.emailString;
    modalRef.componentInstance.userIndex = index;
    modalRef.componentInstance.table = this.tableName;
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