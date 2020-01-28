import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { DataService } from 'src/app/services/data.service';
import { AddUserModalComponent } from '../modals/add-user-modal/add-user-modal.component';


@Component({
  selector: 'app-db-tables',
  templateUrl: './db-tables.component.html',
  styleUrls: ['./db-tables.component.scss']
})
export class DbTablesComponent implements OnInit {

  constructor(protected dataService: DataService, private modalService: NgbModal) {}

  ngOnInit() {
  }

  addUser() {
    this.modalService.open(AddUserModalComponent, { scrollable: true });
  }

  editUser() {
    this.dataService.editMode = !this.dataService.editMode;
    this.dataService.deleteMode = false;
  }

  deleteUser() {
    this.dataService.deleteMode = !this.dataService.deleteMode;
    this.dataService.editMode = false;
  }
}
