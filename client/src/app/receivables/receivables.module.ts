import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReceivablesComponent } from './receivables.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { ReceivableDetailsComponent } from './receivable-details/receivable-details.component';
import { ReceivablesRoutingModule } from './receivables-routing.module';

@NgModule({
  declarations: [
    ReceivablesComponent,
    ReceivableDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ReceivablesRoutingModule
  ]
})
export class ReceivablesModule { }
