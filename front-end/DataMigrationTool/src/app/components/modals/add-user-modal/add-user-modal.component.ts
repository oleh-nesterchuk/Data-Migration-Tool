import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';

import { DataService } from 'src/app/services/data.service';


@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrls: ['./add-user-modal.component.scss']
})
export class AddUserModalComponent implements OnInit {

  newUserForm: FormGroup;
  constructor(public activeModal: NgbActiveModal, protected data: DataService) { }

  ngOnInit() {
    this.newUserForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      lastName: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      birthDate: new FormControl(null, Validators.required),
      emails: new FormArray([]),
    });
  }

  addUser() {
    console.log(this.newUserForm);
  }

  onAddEmail() {
    const email = new FormGroup({
      value: new FormControl(null, [Validators.required, Validators.email, Validators.maxLength(50)]),
      isConfirmed: new FormControl(false)
    });
    (this.newUserForm.get('emails') as FormArray).push(email);
  }
}
