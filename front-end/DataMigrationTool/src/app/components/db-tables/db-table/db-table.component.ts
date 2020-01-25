import { Component, OnInit, Input, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DragulaService } from 'ng2-dragula';
import { Subscription } from 'rxjs';

import { RequestService } from 'src/app/services/request.service';
import { DataService } from 'src/app/services/data.service';
import { EmailsModalComponent } from 'src/app/components/modals/emails-modal/emails-modal.component';
import { EditUserModalComponent } from 'src/app/components/modals/edit-user-modal/edit-user-modal.component';
import { User } from 'src/app/interfaces/user';
import { PaginationService } from 'src/app/services/pagination.service';


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
  @Input() neighbourString: string;
  @Input() neighbourTable: string;
  @Input() tableName: string;
  @Input() tableHeader: string;
  @ViewChild('tbody', {static: false}) tbody: ElementRef;
  @ViewChild('errorElement', {static: false}) errorElement: ElementRef;
  isLoading: boolean;
  wasTransferred: boolean;
  errorMessage: string;
  dragula$ = new Subscription();

  constructor(private httpService: RequestService, protected dataService: DataService,
              private modalService: NgbModal, private dragulaService: DragulaService,
              private paginationService: PaginationService) {
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
    this.dragula$.add(this.dragulaService.drop('COPYABLE')
      .subscribe(({ el, target }) => {
        if (target === null || target === this.tbody.nativeElement) {
          this.clearDragulasTable();
          return;
        }
        this.wasTransferred = false;

        const query = this.transferString + '/' + el.firstElementChild
          .firstElementChild.getAttribute('title');
        this.httpService.transferUser(query).subscribe(data => {
            this.loadUsers(this.neighbourString, this.neighbourTable);

            this.wasTransferred = true;
          }, error => {
            this.errorMessage = this.httpService.getErrorMessage(error);
            this.scrollToElement(this.errorElement);

            this.wasTransferred = false;
        });
      })
    );

    this.paginationService.setUserSize(this.userString);
    this.loadUsers();
  }

  get users(): User[] {
    return this.dataService[this.tableName];
  }

  pageChange(newPage: number) {
    this.paginationService[this.userString + 'PageNumber'] = newPage;
    this.loadUsers();
  }

  loadUsers(userString: string = this.userString, tableName: string = this.tableName) {
    this.isLoading = true;
    this.errorMessage = null;

    this.httpService.fetchUsers(userString, 
      this.paginationService[userString + 'PageNumber'], 
      this.paginationService[userString + 'PageSize']
    ).subscribe(data => {
      this.dataService[tableName] = data;

      this.isLoading = false;
    }, error => {
      this.errorMessage = this.httpService.getErrorMessage(error);
      this.scrollToElement(this.errorElement);

      this.isLoading = false;
    });
  }

  editUser(index: number) {
    const query = this.userString + '/' + this.dataService[this.tableName][index].id;

    const modalRef = this.modalService.open(EditUserModalComponent);
    modalRef.componentInstance.query = query;
    modalRef.componentInstance.userIndex = index;
    modalRef.componentInstance.table = this.tableName;
    modalRef.componentInstance.apiAction = this.userString;
  }

  deleteUser(index: number) {
    const query = this.userString + '/' + this.dataService[this.tableName][index].id;

    this.httpService.deleteUser(query).subscribe(() => {
      this.dataService[this.tableName].splice(index, 1);
      this.paginationService.setUserSize(this.userString);
      this.loadUsers();
    });
  }

  transfer(index: number) {
    const query = this.transferString + '/' + this.dataService[this.tableName][index].id;
    this.wasTransferred = false;

    this.httpService.transferUser(query).subscribe(data => {
      this.paginationService.setUserSize(this.neighbourString);

      this.wasTransferred = true;
    }, error => {
      this.errorMessage = this.httpService.getErrorMessage(error);
      this.scrollToElement(this.errorElement);

      this.wasTransferred = false;
    });
  }

  loadEmails(index: number) {
    const modalRef = this.modalService.open(EmailsModalComponent);

    modalRef.componentInstance.emailString = this.emailString;
    modalRef.componentInstance.userIndex = index;
    modalRef.componentInstance.table = this.tableName;
  }

  ngOnDestroy() {
    this.dragula$.unsubscribe();
  }

  private scrollToElement(elementRef: ElementRef) {
    if (elementRef) {
      elementRef.nativeElement.scrollIntoView();
    }
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
