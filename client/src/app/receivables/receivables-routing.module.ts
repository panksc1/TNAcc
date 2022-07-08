import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReceivablesComponent } from './receivables.component';
import { ReceivableDetailsComponent } from './receivable-details/receivable-details.component';

const routes: Routes = [
  //The root component for the Gig Module
  {path: '', component: ReceivablesComponent},//loadChildren: () => import('./gigs/gigs.module').then(mod => mod.GigsModule)},
  {path: ':id', component: ReceivableDetailsComponent, data: {breadcrumb: {alias: 'receivableDetails'}}},
]

@NgModule({
  declarations: [],
  imports: [
      //These roots are not available in the app module and are only available in the Gigs Module
      RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class ReceivablesRoutingModule { }
