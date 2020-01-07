import { Component } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { DataService } from '../../services/data.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'DataMigrationTool';

  constructor(protected data: DataService) {}
}
