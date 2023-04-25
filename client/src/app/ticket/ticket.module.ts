import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TicketRoutingModule } from './ticket-routing.module';
import { IndexComponent } from './index/index.component';
import { ViewComponent } from './view/view.component';
import { ParkComponent } from './park/park.component';
import { CompleteComponent } from './complete/complete.component';


@NgModule({
  declarations: [
    IndexComponent,
    ViewComponent,
    ParkComponent,
    CompleteComponent
  ],
  imports: [
    CommonModule,
    TicketRoutingModule
  ]
})
export class TicketModule { }
