import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { IBand } from '../shared/models/band';
import { IGig } from '../shared/models/gig';
import { GigParams } from '../shared/models/gigParams';
import { IPagination } from '../shared/models/pagination';
import { PaymentParams } from '../shared/models/paymentParams';
import { IReceivable } from '../shared/models/receivable';
import { IVenue } from '../shared/models/venue';

@Injectable({
  providedIn: 'root'  //app module = root module. Initialized as a Singleton when our app starts. Holds data we need to share when we start the application
})
export class GigsService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getGigs(gigParams: GigParams) {
    let params = new HttpParams();
    
    if(gigParams.venueId != 0) {
      params = params.append('venueId', gigParams.venueId.toString());
    }

    if(gigParams.month != 0) {
      params = params.append('month', gigParams.month.toString());
    }

    if(gigParams.year != 0) {
      params = params.append('year', gigParams.year.toString());
    }

    if(gigParams.bandId != 0) {
      params = params.append('bandId', gigParams.bandId.toString());
    }

    if(gigParams.search) {
      params = params.append('search', gigParams.search);
    }

    params = params.append('sort', gigParams.sort);
    params = params.append('pageIndex', gigParams.pageNumber.toString());
    params = params.append('pageIndex', gigParams.pageSize.toString());
    
    return this.http.get<IPagination>(this.baseUrl + 'gigs', {observe: 'response', params})
      .pipe (
        map(response => {
          return response.body;
        })
      );
  }

  getPayables(paymentParams: PaymentParams) {
    let params = new HttpParams();

    if(paymentParams.gigId != 0) {
      params = params.append('gigId', paymentParams.gigId.toString());
    }

    params = params.append('pageIndex', paymentParams.pageNumber.toString());
    params = params.append('pageIndex', paymentParams.pageSize.toString());
    return this.http.get<IPagination>(this.baseUrl + 'payables', {observe: 'response', params})
      .pipe (
        map(response => {
          return response.body;
        })
      );
  }

  getReceivables(paymentParams: PaymentParams) {
    let params = new HttpParams();

    if(paymentParams.gigId != 0) {
      params = params.append('gigId', paymentParams.gigId.toString());
    }
    params = params.append('pageIndex', paymentParams.pageNumber.toString());
    params = params.append('pageIndex', paymentParams.pageSize.toString());
    return this.http.get<IPagination>(this.baseUrl + 'receivables', {observe: 'response', params})
      .pipe (
        map(response => {
          return response.body;
        })
      );
  }

  getGig(id: number) {
    return this.http.get<IGig>(this.baseUrl + 'gigs/' + id);
  }

  getVenues() {
    return this.http.get<IVenue[]>(this.baseUrl + 'venues');
  }

  getBands() {
    return this.http.get<IBand[]>(this.baseUrl + 'bands');
  }
}
