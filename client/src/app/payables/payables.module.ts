import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PayablesComponent } from './payables.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { PayableDetailsComponent } from './payable-details/payable-details.component';
import { PayablesRoutingModule } from './payables-routing.module';

@NgModule({
  declarations: [
    PayablesComponent,
    PayableDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PayablesRoutingModule
  ]
})
export class PayablesModule { }
