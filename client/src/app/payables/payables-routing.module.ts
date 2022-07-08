import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PayablesComponent } from './payables.component';
import { PayableDetailsComponent } from './payable-details/payable-details.component';

const routes: Routes = [
  //The root component for the Gig Module
  {path: '', component: PayablesComponent},//loadChildren: () => import('./gigs/gigs.module').then(mod => mod.GigsModule)},
  {path: ':id', component: PayableDetailsComponent, data: {breadcrumb: {alias: 'payableDetails'}}},
]

@NgModule({
  declarations: [],
  imports: [
        //These roots are not available in the app module and are only available in the Gigs Module
        RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class PayablesRoutingModule { }
