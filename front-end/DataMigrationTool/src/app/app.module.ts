import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DragulaModule } from 'ng2-dragula';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './components/app/app.component';
import { DbTableComponent } from './components/db-table/db-table.component';
import { EmailsModalComponent } from './components/modals/emails-modal/emails-modal.component';
import { AddUserModalComponent } from './components/modals/add-user-modal/add-user-modal.component';


@NgModule({
  declarations: [
    AppComponent,
    DbTableComponent,
    EmailsModalComponent,
    AddUserModalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgbModule,
    DragulaModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [EmailsModalComponent, AddUserModalComponent]
})
export class AppModule { }
