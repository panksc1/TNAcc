import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IEntity } from '../shared/models/entity';
import { PaymentParams } from '../shared/models/paymentParams';
import { IReceivable } from '../shared/models/receivable';
import { ReceivablesService } from './receivables.service';

@Component({
  selector: 'app-receivables',
  templateUrl: './receivables.component.html',
  styleUrls: ['./receivables.component.scss']
})
export class ReceivablesComponent implements OnInit {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  receivables: IReceivable[];
  entities: IEntity[];
  totalCount: number;
  totalPaid: number;
  receivableParams = new PaymentParams();
  minDate = new Date('0001-01-02T00:00:00.000Z');
  sortOptions = [
    { name: 'Date Descending', value: 'dateDesc' },
    { name: 'Date Ascending', value: 'dateAsc' },
    { name: 'Entity', value: 'entity' },
  ]
  paymentStatuses = [
    { name: 'All', value: 0 },
    { name: 'Paid', value: 1 },
    { name: 'Unpaid', value: 2 }
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


  constructor(private receivablesService: ReceivablesService) { }

  ngOnInit(): void {
    this.getReceivables();
    this.getEntities();
  }

  getReceivables() {
    this.receivablesService.getReceivables(this.receivableParams).subscribe(response => {
      this.receivables = (<IReceivable[]>response.data);
      this.receivableParams.pageNumber = response.pageIndex;
      this.receivableParams.pageSize = response.pageSize;
      this.totalCount = response.count;
      this.totalPaid = 0;
      for (let i = 0; i < this.receivables.length; i++) {
        this.totalPaid += this.receivables[i].amountPaid;
      }
    }, error => {
      console.log(error);
    });
  }

  getEntities() {
    this.receivablesService.getEntities().subscribe(response => {
      this.entities = [{ id: 0, company: '', name: 'All', address: '', city: '', state: '', zip: '', phone: '', email: '', notes: '', type: '' }, ...response];
    })
  }

  onEntitySelected(entityId: number) {
    this.receivableParams.entityId = entityId;
    this.receivableParams.pageNumber = 1;
    this.getReceivables();
  }

  onSortSelected(sort: string) {
    this.receivableParams.sort = sort;
    this.getReceivables();
  }

  onPaymentStatusSelected(paymentStatus: number) {
    this.receivableParams.paymentStatus = paymentStatus;
    this.getReceivables();
  }

  onMonthSelected(month: number) {
    this.receivableParams.month = month;
    this.getReceivables();
  }

  onYearSelected(year: number) {
    this.receivableParams.year = year;
    this.getReceivables();
  }

  onPageChanged(event: any) {
    if (this.receivableParams.pageNumber !== event) {
      this.receivableParams.pageNumber = event;
      this.getReceivables();
    }
  }

  onSearch() {
    this.receivableParams.search = this.searchTerm.nativeElement.value;
    this.receivableParams.pageNumber = 1;
    this.getReceivables();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.receivableParams = new PaymentParams();
    this.getReceivables();
  }

  getFormattedDate(date: Date): string {
    var d = new Date(date);
    if ((d.getTime()) > (this.minDate.getTime())) {
      return d.toLocaleDateString();
    }
    return '';
  }
}
