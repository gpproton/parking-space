import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SpaceModule } from './space/space.module';
import { CustomerModule } from './customer/customer.module';
import { IncidentModule } from './incident/incident.module';
import { PriceModule } from './price/price.module';
import { StaffModule } from './staff/staff.module';
import { TicketModule } from './ticket/ticket.module';
import { VehicleModule } from './vehicle/vehicle.module';
import { SpotModule } from './spot/spot.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    CustomerModule,
    IncidentModule,
    PriceModule,
    SpaceModule,
    SpotModule,
    StaffModule,
    TicketModule,
    VehicleModule,
    HttpClientModule,
    NgbModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
