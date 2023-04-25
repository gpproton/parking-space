import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { ViewComponent } from './view/view.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
  { path: 'vehicle', component: IndexComponent },
  { path: 'vehicle/:postId/view', component: ViewComponent },
  { path: 'vehicle/create', component: CreateComponent },
  { path: 'vehicle/:postId/edit', component: EditComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VehicleRoutingModule {}
