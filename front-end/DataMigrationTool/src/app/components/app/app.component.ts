import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { DataService } from '../../services/data.service';
import { AddUserModalComponent } from '../modals/add-user-modal/add-user-modal.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'DataMigrationTool';

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
