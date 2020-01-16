import { Component, Input } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { DataService } from 'src/app/services/data.service';


@Component({
  selector: 'app-ngbd-modal-content',
  templateUrl: './emails-modal.component.html'
})
export class EmailsModalComponent {
  // @Input() name;
  columns = ['ID', 'Value', 'Is Confirmed'];

  constructor(public activeModal: NgbActiveModal, protected data: DataService) {}
}
