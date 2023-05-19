import { Component, OnInit } from '@angular/core';
import { Spot } from 'src/app/interfaces/Spot';
import { SpotService } from '../spot.service';
import { PagedResponse } from 'src/app/interfaces/PagedResponse';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss'],
})
export class IndexComponent implements OnInit {
  spots: Spot[] = [];

  constructor(public spotService: SpotService) {}

  ngOnInit(): void {
    this.spotService.getAll().subscribe((data: PagedResponse<Spot>) => {
      this.spots = data.data;

      console.log(this.spots);
    });
  }
}
