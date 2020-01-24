import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';

import { DataService } from 'src/app/services/data.service';
import { RequestService } from 'src/app/services/request.service';
import { PaginationService } from 'src/app/services/pagination.service';


@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrls: ['./add-user-modal.component.scss']
})
export class AddUserModalComponent implements OnInit, OnDestroy {

  newUserForm: FormGroup;
  errorMessage: string;
  isLoading: boolean;
  wasAdded: boolean;
  table: string;
  destination = 'SqlServerUser';
  destinationSet = new Set<string>();

  constructor(public activeModal: NgbActiveModal, protected dataService: DataService,
              private httpService: RequestService, private paginationService: PaginationService) { }

  ngOnInit() {
    this.newUserForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      lastName: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      birthDate: new FormControl(null, Validators.required),
      emails: new FormArray([]),
    });
  }

  addUser() {
    this.isLoading = true;
    this.wasAdded = false;
    this.errorMessage = null;
    this.newUserForm.markAllAsTouched();
    if (this.newUserForm.invalid) {
      return;
    }
    if (this.destination.includes('Mongo')) {
      this.table = 'mongoUsers';
    }
    else {
      this.table = 'sqlUsers';
    }
    this.httpService.addUser(this.destination, this.newUserForm.value)
      .subscribe(data => {
        this.isLoading = false;
        this.wasAdded = true;
        this.newUserForm.reset();
        this.paginationService.setUserSize(this.destination, this.table);
        this.destinationSet.add(this.destination);
      }, error => {
        this.isLoading = false;
        this.errorMessage = this.httpService.getErrorMessage(error);
      });
  }

  onAddEmail() {
    const email = new FormGroup({
      value: new FormControl(null, [Validators.required, Validators.email, Validators.maxLength(50)]),
      isConfirmed: new FormControl(false)
    });
    (this.newUserForm.get('emails') as FormArray).push(email);
  }

  deleteEmail(index: number) {
    (this.newUserForm.get('emails') as FormArray).removeAt(index);
  }

  ngOnDestroy() {
    for (let item of this.destinationSet) {
      this.httpService.fetchUsers(item, this.paginationService[item + 'PageNumber'], 
        this.paginationService[item + 'PageSize'])
        .subscribe(data => {
          this.dataService[this.table] = data;
      }, error => {
        alert(this.httpService.getErrorMessage(error));
      });
    }
  }
}
