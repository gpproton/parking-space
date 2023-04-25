import { Component, OnInit } from '@angular/core';
import { Space } from 'src/app/interfaces/Space';
import { SpaceService } from '../space.service';
import { PagedResponse } from 'src/app/interfaces/PagedResponse';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss'],
})
export class IndexComponent implements OnInit {
  spaces: Space[] = [];

  constructor(public spaceService: SpaceService) {}

  ngOnInit(): void {
    this.spaceService.getAll().subscribe((data: PagedResponse<Space>) => {
      this.spaces = data.data;

      console.log(this.spaces);
    });
  }
}
