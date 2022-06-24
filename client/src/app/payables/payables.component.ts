import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IEntity } from '../shared/models/entity';
import { IPayable } from '../shared/models/payable';
import { PaymentParams } from '../shared/models/paymentParams';
import { PayablesService } from './payables.service';

@Component({
  selector: 'app-payables',
  templateUrl: './payables.component.html',
  styleUrls: ['./payables.component.scss']
})
export class PayablesComponent implements OnInit {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  payables: IPayable[];
  entities: IEntity[];
  totalCount: number;
  totalPaid: number;
  payableParams = new PaymentParams();
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


  constructor(private payablesService: PayablesService) { }

  ngOnInit(): void {
    this.getPayables();
    this.getEntities();
  }

  getPayables() {
    this.payablesService.getPayables(this.payableParams).subscribe(response => {
      this.payables = (<IPayable[]>response.data);
      this.payableParams.pageNumber = response.pageIndex;
      this.payableParams.pageSize = response.pageSize;
      this.totalCount = response.count;
      this.totalPaid = 0;
      for (let i = 0; i < this.payables.length; i++) {
        this.totalPaid += this.payables[i].amountPaid;
      }
    }, error => {
      console.log(error);
    });
  }

  getEntities() {
    this.payablesService.getEntities().subscribe(response => {
      this.entities = [{ id: 0, company: '', name: 'All', address: '', city: '', state: '', zip: '', phone: '', email: '', notes: '', type: '' }, ...response];
    })
  }

  onEntitySelected(entityId: number) {
    this.payableParams.entityId = entityId;
    this.payableParams.pageNumber = 1;
    this.getPayables();
  }

  onSortSelected(sort: string) {
    this.payableParams.sort = sort;
    this.getPayables();
  }

  onPaymentStatusSelected(paymentStatus: number) {
    this.payableParams.paymentStatus = paymentStatus;
    this.getPayables();
  }

  onMonthSelected(month: number) {
    this.payableParams.month = month;
    this.getPayables();
  }

  onYearSelected(year: number) {
    this.payableParams.year = year;
    this.getPayables();
  }

  onPageChanged(event: any) {
    if (this.payableParams.pageNumber !== event) {
      this.payableParams.pageNumber = event;
      this.getPayables();
    }
  }

  onSearch() {
    this.payableParams.search = this.searchTerm.nativeElement.value;
    this.payableParams.pageNumber = 1;
    this.getPayables();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.payableParams = new PaymentParams();
    this.getPayables();
  }

  getFormattedDate(date: Date): string {
    var d = new Date(date);
    if ((d.getTime()) > (this.minDate.getTime())) {
      return d.toLocaleDateString();
    }
    return '';
  }
}
