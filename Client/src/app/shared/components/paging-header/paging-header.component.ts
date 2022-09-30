import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent implements OnInit {

  @Input() pageNumber: number;
  @Input() pageSize: number;
  @Input() totalCount: number;

  constructor() { }

  ngOnInit(): void {
  }

  getFirstNumberOnPage(): number {
    return (this.pageNumber - 1) * this.pageSize + 1;
  }

  getLastNumberOnPage(): number {
    let lastNumber = this.pageNumber * this.pageSize;
    return lastNumber > this.totalCount ? this.totalCount : lastNumber;
  }

  hasResults(): boolean {
    return this.totalCount > 0;
  }
}
