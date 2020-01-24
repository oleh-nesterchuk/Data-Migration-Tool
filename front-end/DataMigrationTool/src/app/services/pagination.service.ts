import { Injectable } from '@angular/core';

import { RequestService } from './request.service';
import { DataService } from './data.service';


@Injectable({
  providedIn: 'root'
})
export class PaginationService {

  constructor(private httpService: RequestService, private dataService: DataService) { }

  setUserSize(apiAction: string, tableName: string) {
    this.httpService.getUsersSize(apiAction).subscribe(size => {
      this.dataService[tableName + 'Size'] = size;
    });
  }

  
}
