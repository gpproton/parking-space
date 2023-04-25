import { Component, OnInit } from '@angular/core';
import { PriceService } from '../praice.service';
import { Price } from 'src/app/interfaces/Price';
import { PagedResponse } from 'src/app/interfaces/PagedResponse';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss'],
})
export class IndexComponent implements OnInit {
  prices: Price[] = [];

  constructor(public spaceService: PriceService) {}

  ngOnInit(): void {
    this.spaceService.getAll().subscribe((data: PagedResponse<Price>) => {
      this.prices = data.data;

      console.log(this.prices);
    });
  }
}
