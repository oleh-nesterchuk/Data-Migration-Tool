import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { DataService } from 'src/app/services/data.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/interfaces/user';
import { birthDateValidator } from 'src/app/validators/birthdate.validators';


@Component({
  selector: 'app-edit-user-modal',
  templateUrl: './edit-user-modal.component.html',
  styleUrls: ['./edit-user-modal.component.scss']
})
export class EditUserModalComponent implements OnInit {

  editUserForm: FormGroup;
  user: User;
  isLoading: boolean;
  errorMessage: string;
  @Input() query: string;
  @Input() userIndex: number;
  @Input() table: string;

  constructor(public activeModal: NgbActiveModal, protected dataService: DataService,
              private httpService: UserService) { }

  ngOnInit() {
    this.user = this.dataService[this.table][this.userIndex];
    let date = new Date(this.user.birthDate);

    this.editUserForm = new FormGroup({
      firstName: new FormControl(this.user.firstName, [Validators.required, Validators.maxLength(50)]),
      lastName: new FormControl(this.user.lastName, [Validators.required, Validators.maxLength(50)]),
      birthDate: new FormControl(this.convertDateToString(date), Validators.required),
    });
  }

  editUser() {
    const updatedUser = this.editUserForm.value as User;
    updatedUser.id = this.user.id;
    this.errorMessage = null;
    this.httpService.editUser(this.query, updatedUser).subscribe(data => {
      this.dataService[this.table][this.userIndex] = data;
      this.activeModal.close();
    }, error => {
      this.errorMessage = this.httpService.getErrorMessage(error);
    });
  }

  private convertDateToString(date: Date): string {
    let month: number | string = date.getMonth() + 1;
    if (month < 10) {
      month = '0' + month;
    }

    let day: number | string = date.getDate();
    if (date.getDate() < 10) {
      day = '0' + day;
    }

    return date.getFullYear() + '-' + month + '-' + day;
  }
}
