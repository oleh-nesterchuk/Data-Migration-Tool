import { Component, Input } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DataService } from 'src/app/services/data.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'ngbd-modal-content',
  template: `
    <div class="modal-header">
      <h4 class="modal-title">Emails</h4>
      <button type="button" class="close" aria-label="Close" (click)="activeModal.dismiss('Cross click')">
        <span aria-hidden="true">&times;</span>
      </button>
    </div>
    <div class="modal-body">
      <table class="table table-hover table-bordered">
        <thead class="thead-light">
          <tr>
            <th *ngFor="let col of columns" scope="col">
              {{col}}
            </th>
          </tr>
        </thead>
        <tr *ngFor="let email of data.emails; let i = index">
          <th scope="row">{{email.id}}</th>
          <td>{{email.value}}</td>
          <td>{{email.isConfirmed}}</td>
        </tr>
      </table>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-outline-dark" (click)="activeModal.close('Close click')">Close</button>
    </div>
  `
})
export class NgbdModalContent {
  @Input() name;
  columns = ['ID', 'Value', 'Is Confirmed'];

  constructor(public activeModal: NgbActiveModal, protected data: DataService) {}
}

// @Component({
//   selector: 'ngbd-modal-component',
//   templateUrl: './modal-component.html'
// })
// export class NgbdModalComponent {
//   constructor(private modalService: NgbModal,
//               private getSql: UserService,
//               private data: DataService) {}

//   open() {
//     const modalRef = this.modalService.open(NgbdModalContent);
//   }
// }
