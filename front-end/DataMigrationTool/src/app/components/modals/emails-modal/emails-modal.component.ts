import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { DataService } from 'src/app/services/data.service';
import { UserService } from 'src/app/services/user.service';
import { Email } from 'src/app/interfaces/email';


@Component({
  selector: 'app-ngbd-modal-content',
  templateUrl: './emails-modal.component.html'
})
export class EmailsModalComponent implements OnInit, OnDestroy {
  columns = ['ID', 'Value', 'Is Confirmed'];
  isLoading: boolean;
  isAdding: boolean;
  isEditing: boolean;
  isDeleting: boolean;
  editIndex = -1;
  emailForm: FormGroup;
  @Input() emailString : string;
  @Input() userIndex : string;
  @Input() table : string;
  
  constructor(public activeModal: NgbActiveModal, protected dataService: DataService,
              private httpService: UserService) {}

  ngOnInit() {
    const query = this.emailString + '?id=' + this.dataService[this.table][this.userIndex].id;
    this.isLoading = true;
    this.httpService.fetchEmails(query).subscribe(data =>{
      this.dataService.emails = data;
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
    });

    this.emailForm = new FormGroup({
      value: new FormControl(null, [Validators.required, Validators.email]),
      isConfirmed: new FormControl(false)
    });
  }

  toggleAddMode() {
    this.isEditing = this.isDeleting = false;
    this.isAdding = !this.isAdding;
  }

  addEmail() {
    const query = this.emailString + '?userId=' + this.dataService[this.table][this.userIndex].id;
    this.httpService
      .addEmail(query, this.emailForm.value as Email)
      .subscribe(data => {
        this.dataService.emails.push(data)
        this.emailForm.reset();
      });
  }

  toggleEditMode() {
    this.isAdding = this.isDeleting = false;
    this.isEditing = !this.isEditing;
    this.editIndex = -1;
  }

  editEmail(index: number) {
    this.emailForm.get('value').setValue(this.dataService.emails[index].value);
    this.emailForm.get('isConfirmed').setValue(this.dataService.emails[index].isConfirmed);
    this.editIndex = index;
  }

  submitEditEmail() {
    const query = this.emailString + '/' + this.dataService[this.table][this.userIndex].id;
    let email = this.emailForm.value as Email;
    email.id = this.dataService.emails[this.editIndex].id;
    this.httpService.editEmail(query, email).subscribe(data => {
      this.dataService.emails[this.editIndex] = data;
      this.editIndex = -1;
    });
  }

  toggleDeleteMode() {
    this.isEditing = this.isAdding = false;
    this.isDeleting = !this.isDeleting;
  }

  deleteEmail(index: number) {
    const query = this.emailString + '/' + this.dataService.emails[index].id;
    this.httpService.deleteEmail(query, index);
  }

  ngOnDestroy() {
    this.dataService.emails = [];
  }
}
