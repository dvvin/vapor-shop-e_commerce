import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-pager',
  standalone: true,
  imports: [CommonModule, PaginationModule],
  templateUrl: './pager.component.html',
  styleUrl: './pager.component.scss'
})
export class PagerComponent implements OnInit {
  @Input() totalCount = 0;
  @Input() pageSize = 0;
  @Output() pageChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {

  }

  onPagerChange(event: any) {
    this.pageChanged.emit(event.page)
  }
}
