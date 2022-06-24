import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { IEntity } from '../shared/models/entity';
import { IPagination } from '../shared/models/pagination';
import { PaymentParams } from '../shared/models/paymentParams';

@Injectable({
  providedIn: 'root'
})
export class ReceivablesService {
  baseUrl = 'https://localhost:5001/api/';
  constructor(private http: HttpClient) { }

  getReceivables(receivableParams: PaymentParams) {
    let params = new HttpParams();
    
    if(receivableParams.gigId != 0) {
      params = params.append('gigId', receivableParams.gigId.toString());
    }

    if(receivableParams.month != 0) {
      params = params.append('month', receivableParams.month.toString());
    }

    if(receivableParams.year != 0) {
      params = params.append('year', receivableParams.year.toString());
    }

    if(receivableParams.entityId != 0) {
      params = params.append('entityId', receivableParams.entityId.toString());
    }

    if(receivableParams.paymentStatus != 0) {
      params = params.append('paymentStatus', receivableParams.paymentStatus.toString());
    }

    if(receivableParams.search) {
      params = params.append('search', receivableParams.search);
    }

    params = params.append('sort', receivableParams.sort);
    params = params.append('pageIndex', receivableParams.pageNumber.toString());
    params = params.append('pageIndex', receivableParams.pageSize.toString());
    
    return this.http.get<IPagination>(this.baseUrl + 'receivables', {observe: 'response', params})
      .pipe (
        map(response => {
          return response.body;
        })
      );
  }

  getEntities() {
    return this.http.get<IEntity[]>(this.baseUrl + 'entities');
  }
}
