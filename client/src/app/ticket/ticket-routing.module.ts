import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { ViewComponent } from './view/view.component';
import { ParkComponent } from './park/park.component';
import { CompleteComponent } from './complete/complete.component';

const routes: Routes = [
  { path: 'ticket', component: IndexComponent },
  { path: 'ticket/:postId/view', component: ViewComponent },
  { path: 'ticket/create', component: ParkComponent },
  { path: 'ticket/:postId/edit', component: CompleteComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TicketRoutingModule {}
