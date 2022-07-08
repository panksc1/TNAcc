import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IReceivable } from 'src/app/shared/models/receivable';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ReceivablesService } from '../receivables.service';

@Component({
  selector: 'app-receivable-details',
  templateUrl: './receivable-details.component.html',
  styleUrls: ['./receivable-details.component.scss']
})
export class ReceivableDetailsComponent implements OnInit {
  receivable: IReceivable;

  constructor(private receivableService: ReceivablesService, private activateRoute: ActivatedRoute,
    private bcService: BreadcrumbService) { 
      this.bcService.set('@receivableDetails', ' ');
    }

  ngOnInit(): void {
    this.loadReceivable();
  }

  loadReceivable() {
    this.receivableService.getReceivable(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(receivable => {
      this.receivable = receivable;
      this.bcService.set('@receivableDetails', receivable.entity);
    }, error => {
      console.log(error);
    });
  }

}
