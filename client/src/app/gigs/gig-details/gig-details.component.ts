import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IGig } from 'src/app/shared/models/gig';
import { IPayable } from 'src/app/shared/models/payable';
import { PaymentParams } from 'src/app/shared/models/paymentParams';
import { IReceivable } from 'src/app/shared/models/receivable';
import { GigsService } from '../gigs.service';

@Component({
  selector: 'app-gig-details',
  templateUrl: './gig-details.component.html',
  styleUrls: ['./gig-details.component.scss']
})
export class GigDetailsComponent implements OnInit {
  gig: IGig;
  payables: IPayable[];
  receivables: IReceivable[];
  totalPayableCount: number;
  totalReceivableCount: number;
  payableParams = new PaymentParams();
  receivableParams = new PaymentParams();
  minDate = new Date('0001-01-01T00:00:00.000Z');

  constructor(private gigService: GigsService, private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadGig();
    this.loadPayables();
    this.loadReceivables();
  }

  loadGig() {
    this.gigService.getGig(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(gig => {
      this.gig = gig;
    }, error => {
      console.log(error);
    });
  }

  loadPayables() {
      this.payableParams.gigId = +this.activateRoute.snapshot.paramMap.get('id');

      this.gigService.getPayables(this.payableParams).subscribe(response => {
        this.payables = (<IPayable[]>response.data);
        this.payableParams.pageNumber = response.pageIndex;
        this.payableParams.pageSize = response.pageSize;
        this.totalPayableCount = response.count
      }, error => {
        console.log(error);
      });
  }

  loadReceivables() {
      this.receivableParams.gigId = +this.activateRoute.snapshot.paramMap.get('id');

      this.gigService.getReceivables(this.receivableParams).subscribe(response => {
        this.receivables = (<IReceivable[]>response.data);
        this.receivableParams.pageNumber = response.pageIndex;
        this.receivableParams.pageSize = response.pageSize;
        this.totalReceivableCount = response.count
      }, error => {
        console.log(error);
      });
  }

  onPayablePageChanged(event: any) {
    if (this.payableParams.pageNumber !== event) {
      this.payableParams.pageNumber = event;
      this.loadPayables();
    }
  }

  onReceivablePageChanged(event: any) {
    if (this.receivableParams.pageNumber !== event) {
      this.receivableParams.pageNumber = event;
      this.loadReceivables();
    }
  }
}
