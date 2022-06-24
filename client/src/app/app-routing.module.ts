import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GigDetailsComponent } from './gigs/gig-details/gig-details.component';
import { GigsComponent } from './gigs/gigs.component';
import { HomeComponent } from './home/home.component';
import { PayablesComponent } from './payables/payables.component';
import { ReceivablesComponent } from './receivables/receivables.component';

const routes: Routes = [
  {path: '', component: HomeComponent}, //Home Page
  {path: 'gigs', component: GigsComponent},//loadChildren: () => import('./gigs/gigs.module').then(mod => mod.GigsModule)},
  {path: 'gigs/:id', component: GigDetailsComponent},
  {path: 'payables', component:PayablesComponent},
  {path: 'receivables', component:ReceivablesComponent},
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
