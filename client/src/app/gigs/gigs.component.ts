import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBand } from '../shared/models/band';
import { IGig } from '../shared/models/gig';
import { GigParams } from '../shared/models/gigParams';
import { IVenue } from '../shared/models/venue';
import { GigsService } from './gigs.service';

@Component({
  selector: 'app-gigs',
  templateUrl: './gigs.component.html',
  styleUrls: ['./gigs.component.scss']
})
export class GigsComponent implements OnInit {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  gigs: IGig[];
  bands: string[];
  venues: IVenue[];
  gigParams = new GigParams();
  totalCount: number;
  sortOptions = [
    { name: 'Date Descending', value: 'dateDesc' },
    { name: 'Date Ascending', value: 'dateAsc' },
    { name: 'Venue', value: 'venue' },
    { name: 'Band', value: 'band' },
    { name: 'Pay Ascending', value: 'payAsc' },
    { name: 'Pay Descending', value: 'payDesc' }
  ]
  months = [
    { name: 'All', value: 0 },
    { name: 'January', value: 1 },
    { name: 'February', value: 2 },
    { name: 'March', value: 3 },
    { name: 'April', value: 4 },
    { name: 'May', value: 5 },
    { name: 'June', value: 6 },
    { name: 'July', value: 7 },
    { name: 'August', value: 8 },
    { name: 'September', value: 9 },
    { name: 'October', value: 10 },
    { name: 'November', value: 11 },
    { name: 'December', value: 12 },
  ]
  years = [
    { name: 'All', value: 0 },
    { name: '2018', value: 2018 },
    { name: '2019', value: 2019 },
    { name: '2020', value: 2020 },
    { name: '2021', value: 2021 },
    { name: '2022', value: 2022 },
    { name: '2023', value: 2023 },
  ]

  constructor(private gigsService: GigsService) { }

  ngOnInit(): void {
    this.getGigs();
    this.getBands();
    this.getVenues();
  }

  getGigs() {
    this.gigsService.getGigs(this.gigParams).subscribe(response => {
      this.gigs = response.data;
      this.gigParams.pageNumber = response.pageIndex;
      this.gigParams.pageSize = response.pageSize;
      this.totalCount = response.count
    }, error => {
      console.log(error);
    });
  }

  getBands() {
    this.gigsService.getGigs(new GigParams()).subscribe(response => {
      this.bands = ['All', ...new Set(response.data.map(item => item.band))];
    })
  }

  getVenues() {
    this.gigsService.getVenues().subscribe(response => {
      this.venues = [{ id: 0, name: 'All', at: '', address: '', city: '', state: '', zip: '' }, ...response];
    }, error => {
      console.log(error);
    });
  }

  onBandSelected(band: string) {
    this.gigParams.band = band;
    this.gigParams.pageNumber = 1;
    this.getGigs();
  }

  onVenueSelected(venueId: number) {
    this.gigParams.venueId = venueId;
    this.gigParams.pageNumber = 1;
    this.getGigs();
  }

  onSortSelected(sort: string) {
    this.gigParams.sort = sort;
    this.getGigs();
  }

  onMonthSelected(month: number) {
    this.gigParams.month = month;
    this.getGigs();
  }

  onYearSelected(year: number) {
    this.gigParams.year = year;
    this.getGigs();
  }

  onPageChanged(event: any) {
    if (this.gigParams.pageNumber !== event) {
      this.gigParams.pageNumber = event;
      this.getGigs();
    }
  }

  onSearch() {
    this.gigParams.search = this.searchTerm.nativeElement.value;
    this.gigParams.pageNumber = 1;
    this.getGigs();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.gigParams = new GigParams();
    this.getGigs();
  }
}
