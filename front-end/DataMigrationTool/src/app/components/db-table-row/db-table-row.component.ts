import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/interfaces/user';

@Component({
  selector: 'app-db-table-row',
  templateUrl: './db-table-row.component.html',
  styleUrls: ['./db-table-row.component.scss']
})
export class DbTableRowComponent implements OnInit {

  @Input() user: User;

  constructor() { }

  ngOnInit() {
  }

}
