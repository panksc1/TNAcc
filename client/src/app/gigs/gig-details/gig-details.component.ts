import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IGig } from 'src/app/shared/models/gig';
import { IPayable } from 'src/app/shared/models/payable';
import { PaymentParams } from 'src/app/shared/models/paymentParams';
import { IReceivable } from 'src/app/shared/models/receivable';
import { BreadcrumbService } from 'xng-breadcrumb';
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
  totalUnpaid: number = 0;
  totalPaid: number = 0;
  totalUnreceived: number = 0;
  totalReceived: number = 0;

  // Activated Route gives us access to the route parameters so we can get the route we are activating
  // and use the information to pass it as the id of the gig to the API.
  constructor(private gigService: GigsService, private activateRoute: ActivatedRoute, 
    private bcService: BreadcrumbService) { 
      this.bcService.set('@gigDetails', ' ');
    }

  ngOnInit(): void {
    this.loadGig();
    this.loadPayables();
    this.loadReceivables();
  }

  loadGig() {
    this.gigService.getGig(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(gig => {
      this.gig = gig;
      this.bcService.set('@gigDetails', gig.band + ' @ ' + gig.venue);
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
        this.totalPaid = 0;
        this.totalUnpaid = 0;
        for (let i = 0; i < this.payables.length; i++) {
          this.totalPaid += this.payables[i].amountPaid;
          this.totalUnpaid += this.payables[i].amountDue;
        }
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
        this.totalReceived = 0;
        this.totalUnreceived = 0;
        for (let i = 0; i < this.receivables.length; i++) {
          this.totalReceived += this.receivables[i].amountPaid;
          this.totalUnreceived += this.receivables[i].amountDue;
        }
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
