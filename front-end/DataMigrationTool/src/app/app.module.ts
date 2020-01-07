import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DragulaModule } from 'ng2-dragula';

import { AppComponent } from './components/app/app.component';
import { DbTableComponent } from './components/db-table/db-table.component';
import { DataService } from './services/data.service';
import { NgbdModalContent } from './components/modal/modal-component';


@NgModule({
  declarations: [
    AppComponent,
    DbTableComponent,
    NgbdModalContent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule,
    DragulaModule.forRoot()
  ],
  providers: [DataService],
  bootstrap: [AppComponent],
  entryComponents: [NgbdModalContent]
})
export class AppModule { }
