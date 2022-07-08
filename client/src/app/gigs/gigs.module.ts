import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GigsComponent } from './gigs.component';
import { SharedModule } from '../shared/shared.module';
import { GigDetailsComponent } from './gig-details/gig-details.component';
import { RouterModule } from '@angular/router';
import { GigsRoutingModule } from './gigs-routing.module';

@NgModule({
  declarations: [
    GigsComponent,
    GigDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    GigsRoutingModule
  ]
})
export class GigsModule { }
