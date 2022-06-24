import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReceivablesComponent } from './receivables.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    ReceivablesComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ],
  exports: [
    ReceivablesComponent
  ]
})
export class ReceivablesModule { }
