import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { IEntity } from '../shared/models/entity';
import { IPagination } from '../shared/models/pagination';
import { IPayable } from '../shared/models/payable';
import { PaymentParams } from '../shared/models/paymentParams';

@Injectable({
  providedIn: 'root'
})
export class PayablesService {
  baseUrl = 'https://localhost:5001/api/';
  constructor(private http: HttpClient) { }

  getPayables(payableParams: PaymentParams) {
    let params = new HttpParams();
    
    if(payableParams.gigId != 0) {
      params = params.append('gigId', payableParams.gigId.toString());
    }

    if(payableParams.month != 0) {
      params = params.append('month', payableParams.month.toString());
    }

    if(payableParams.year != 0) {
      params = params.append('year', payableParams.year.toString());
    }

    if(payableParams.entityId != 0) {
      params = params.append('entityId', payableParams.entityId.toString());
    }

    if(payableParams.paymentStatus != 0) {
      params = params.append('paymentStatus', payableParams.paymentStatus.toString());
    }

    if(payableParams.search) {
      params = params.append('search', payableParams.search);
    }

    params = params.append('sort', payableParams.sort);
    params = params.append('pageIndex', payableParams.pageNumber.toString());
    params = params.append('pageIndex', payableParams.pageSize.toString());
    
    return this.http.get<IPagination>(this.baseUrl + 'payables', {observe: 'response', params})
      .pipe (
        map(response => {
          return response.body;
        })
      );
  }

  getPayable(id: number) {
    return this.http.get<IPayable>(this.baseUrl + 'payables/' + id);
  }

  getEntities() {
    return this.http.get<IEntity[]>(this.baseUrl + 'entities');
  }

}
