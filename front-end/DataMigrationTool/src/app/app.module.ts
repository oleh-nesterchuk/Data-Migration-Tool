import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { MatInputModule, MatPaginatorModule, MatTableModule } from '@angular/material';

import { AppComponent } from './components/app/app.component';
import { DbTableComponent } from './components/db-table/db-table.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DbTableRowComponent } from './components/db-table-row/db-table-row.component';
import { DataService } from './services/data.service';


@NgModule({
  declarations: [
    AppComponent,
    DbTableComponent,
    DbTableRowComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    MatInputModule,
    MatPaginatorModule,
    MatTableModule,
    BrowserAnimationsModule
  ],
  providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
