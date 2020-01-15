import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';

import { DataService } from '../../services/data.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'DataMigrationTool';

  constructor(protected data: DataService) {}

  ngOnInit() {
  }

  ngOnDestroy() {
  }
}
