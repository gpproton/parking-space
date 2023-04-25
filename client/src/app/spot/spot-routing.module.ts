import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { ViewComponent } from './view/view.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
  { path: 'spot', component: IndexComponent },
  { path: 'spot/:postId/view', component: ViewComponent },
  { path: 'spot/create', component: CreateComponent },
  { path: 'spot/:postId/edit', component: EditComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SpotRoutingModule {}
