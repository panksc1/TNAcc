import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IPayable } from 'src/app/shared/models/payable';
import { BreadcrumbService } from 'xng-breadcrumb';
import { PayablesService } from '../payables.service';

@Component({
  selector: 'app-payable-details',
  templateUrl: './payable-details.component.html',
  styleUrls: ['./payable-details.component.scss']
})
export class PayableDetailsComponent implements OnInit {
  payable: IPayable;

  constructor(private payableService: PayablesService, private activateRoute: ActivatedRoute, 
    private bcService: BreadcrumbService) {
      this.bcService.set('@payableDetails', ' ');
     }

  ngOnInit(): void {
    this.loadPayable();
  }

  loadPayable() {
    this.payableService.getPayable(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(payable => {
      this.payable = payable;
      this.bcService.set('@payableDetails', payable.entity);
    }, error => {
      console.log(error);
    });
  }

}
