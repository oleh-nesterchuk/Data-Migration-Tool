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

  constructor(protected data: DataService, private modalService: NgbModal) {}

  ngOnInit() {
  }

  addUser() {
    this.modalService.open(AddUserModalComponent, { scrollable: true });
  }

  editUser() {
    this.data.editMode = !this.data.editMode;
    this.data.deleteMode = false;
  }

  deleteUser() {
    this.data.deleteMode = !this.data.deleteMode;
    this.data.editMode = false;
  }
}
