import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { GigDetailsComponent } from './gigs/gig-details/gig-details.component';
import { GigsComponent } from './gigs/gigs.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'Home'}}, //Home Page
  {path: 'test-error', component: TestErrorComponent, data: {breadcrumb: 'Test Error'}}, 
  {path: 'server-error', component: ServerErrorComponent, data: {breadcrumb: 'Server Error'}}, 
  {path: 'not-found', component: NotFoundComponent, data: {breadcrumb: 'Not Found'}}, 
  {path: 'gigs', loadChildren: () => import('./gigs/gigs.module').then(mod => mod.GigsModule),
  data: {breadcrumb: 'Gigs'}}, //Lazy Loading, will only be activated and loaded when we access the Gig Path
  {path: 'payables', loadChildren: () => import('./payables/payables.module').then(mod => mod.PayablesModule),
  data: {breadcrumb: 'Accounts Payable'}},//component:PayablesComponent},
  {path: 'receivables', loadChildren: () => import('./receivables/receivables.module').then(mod => mod.ReceivablesModule),
  data: {breadcrumb: 'Accounts Receivable'}},//component:ReceivablesComponent},
  {path: '**', redirectTo: 'not-found', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
