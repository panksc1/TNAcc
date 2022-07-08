import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { GigsComponent } from './gigs.component';
import { GigDetailsComponent } from './gig-details/gig-details.component';

const routes: Routes = [
  //The root component for the Gig Module
  {path: '', component: GigsComponent},//loadChildren: () => import('./gigs/gigs.module').then(mod => mod.GigsModule)},
  {path: ':id', component: GigDetailsComponent, data: {breadcrumb: {alias: 'gigDetails'}}},
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
export class GigsRoutingModule { }
