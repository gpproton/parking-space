import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { ViewComponent } from './view/view.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
  { path: 'space', redirectTo: 'space/index', pathMatch: 'full' },
  { path: 'space/index', component: IndexComponent },
  { path: 'space/:postId/view', component: ViewComponent },
  { path: 'space/create', component: CreateComponent },
  { path: 'space/:postId/edit', component: EditComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StaffRoutingModule {}
