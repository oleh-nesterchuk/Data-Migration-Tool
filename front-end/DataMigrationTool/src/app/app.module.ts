import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DragulaModule, DragulaService } from 'ng2-dragula';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { AppComponent } from './components/app/app.component';
import { DbTableComponent } from './components/db-table/db-table.component';
import { EmailsModalComponent } from './components/modals/emails-modal/emails-modal.component';
import { AddUserModalComponent } from './components/modals/add-user-modal/add-user-modal.component';
import { EditUserModalComponent } from './components/modals/edit-user-modal/edit-user-modal.component';


@NgModule({
  declarations: [
    AppComponent,
    DbTableComponent,
    EmailsModalComponent,
    AddUserModalComponent,
    EditUserModalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgbModule,
    FormsModule,
    DragulaModule.forRoot()
  ],
  providers: [DragulaService],
  bootstrap: [AppComponent],
  entryComponents: [
    EmailsModalComponent, 
    AddUserModalComponent, 
    EditUserModalComponent
  ]
})
export class AppModule { }
