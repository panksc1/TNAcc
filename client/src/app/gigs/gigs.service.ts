import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { GigParams } from '../shared/models/gigParams';
import { IPagination } from '../shared/models/pagination';
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

    if(gigParams.band && gigParams.band !== 'All') {
      params = params.append('band', gigParams.band);
    }

    if(gigParams.search) {
      params = params.append('search', gigParams.search);
    }

    params = params.append('sort', gigParams.sort);
    params = params.append('pageIndex', gigParams.pageNumber.toString());
    params = params.append('pageIndex', gigParams.pageSize.toString());
    
    console.log(params);
    return this.http.get<IPagination>(this.baseUrl + 'gigs', {observe: 'response', params})
      .pipe (
        map(response => {
          return response.body;
        })
      );
  }

  getVenues() {
    return this.http.get<IVenue[]>(this.baseUrl + 'gigs/venues');
  }
}
