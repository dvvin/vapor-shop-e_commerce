import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ShopParams } from '../../models/shopParams';

@Component({
  selector: 'app-pager',
  standalone: true,
  imports: [CommonModule, PaginationModule],
  templateUrl: './pager.component.html',
  styleUrl: './pager.component.scss'
})
export class PagerComponent {
  @Input() totalPages: number = 0;
  @Input() shopParams: ShopParams = new ShopParams();
  @Output() pageChanged = new EventEmitter<number>();

  onPageChanged(pageNumber: number) {
    this.pageChanged.emit(pageNumber);
  }
}
