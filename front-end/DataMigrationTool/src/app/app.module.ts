import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DragulaModule } from 'ng2-dragula';

import { AppComponent } from './components/app/app.component';
import { DbTableComponent } from './components/db-table/db-table.component';
import { EmailsModalComponent } from './components/modal/emails-modal.component';


@NgModule({
  declarations: [
    AppComponent,
    DbTableComponent,
    EmailsModalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule,
    DragulaModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [EmailsModalComponent]
})
export class AppModule { }
