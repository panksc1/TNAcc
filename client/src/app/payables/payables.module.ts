import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PayablesComponent } from './payables.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    PayablesComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ],
  exports: [
    PayablesComponent
  ]
})
export class PayablesModule { }
