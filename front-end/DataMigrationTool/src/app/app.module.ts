import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { DragulaModule, DragulaService } from 'ng2-dragula';

import { AppComponent } from './components/app/app.component';
import { DbTableComponent } from './components/db-tables/db-table/db-table.component';
import { DbTablesComponent } from './components/db-tables/db-tables.component';
import { EmailsModalComponent } from './components/modals/emails-modal/emails-modal.component';
import { AddUserModalComponent } from './components/modals/add-user-modal/add-user-modal.component';
import { EditUserModalComponent } from './components/modals/edit-user-modal/edit-user-modal.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';


const appRoutes = [
  { path: '', redirectTo: 'tables', pathMatch: 'full' },
  { path: 'tables', component: DbTablesComponent },
  { path: '**', component: PageNotFoundComponent },
]

@NgModule({
  declarations: [
    AppComponent,
    DbTableComponent,
    EmailsModalComponent,
    AddUserModalComponent,
    EditUserModalComponent,
    DbTablesComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgbModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
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
