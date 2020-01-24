import { Injectable } from '@angular/core';

import { RequestService } from './request.service';
import { DataService } from './data.service';


@Injectable({
  providedIn: 'root'
})
export class PaginationService {

  SqlServerUserPageNumber: 1;
  SqlServerUserPageSize: 5;
  MongoDbUserPageNumber: 1;
  MongoDbUserPageSize: 5;
  sqlUsersSize: number;
  mongoUsersSize: number;
  
  constructor(private httpService: RequestService, private dataService: DataService) { }

  setUserSize(apiAction: string, tableName: string) {
    this.httpService.getUsersSize(apiAction).subscribe(size => {
      this.dataService[tableName + 'Size'] = size;
    });
  }
}
