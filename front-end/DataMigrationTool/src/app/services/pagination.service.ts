import { Injectable, OnInit } from '@angular/core';

import { RequestService } from './request.service';
import { DataService } from './data.service';


@Injectable({
  providedIn: 'root'
})
export class PaginationService {

  SqlServerUserPageNumber = 1;
  SqlServerUserPageSize = 5;
  SqlServerUserSize: number;
  MongoDbUserPageNumber = 1;
  MongoDbUserPageSize = 5;
  MongoDbUserSize: number;
  
  constructor(private httpService: RequestService) { }

  setUserSize(apiAction: string) {
    this.httpService.getUsersSize(apiAction).subscribe(size => {
      this[apiAction + 'Size'] = size;
    });
  }
}
