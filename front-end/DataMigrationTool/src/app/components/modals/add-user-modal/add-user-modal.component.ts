import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';

import { DataService } from 'src/app/services/data.service';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrls: ['./add-user-modal.component.scss']
})
export class AddUserModalComponent implements OnInit {

  newUserForm: FormGroup;
  destination = 'SqlServerUser';

  constructor(public activeModal: NgbActiveModal, protected data: DataService,
              private httpService: UserService) { }

  ngOnInit() {
    this.newUserForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      lastName: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      birthDate: new FormControl(null, Validators.required),
      emails: new FormArray([]),
    });
  }

  addUser() {
    this.newUserForm.markAllAsTouched();
    if (this.newUserForm.invalid) {
      return;
    }
    this.httpService.addUser(this.destination, this.newUserForm.value);
  }

  onAddEmail() {
    const email = new FormGroup({
      value: new FormControl(null, [Validators.required, Validators.email, Validators.maxLength(50)]),
      isConfirmed: new FormControl(false)
    });
    (this.newUserForm.get('emails') as FormArray).push(email);
  }

  deleteEmail(index: number) {
    console.log('deleting email ' + index);
    (this.newUserForm.get('emails') as FormArray).removeAt(index);
  }
}
