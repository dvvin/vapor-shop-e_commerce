import { NgModule } from '@angular/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';

@NgModule({
  declarations: [],
  imports: [
    PaginationModule.forRoot(),
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent
  ],
  exports: [
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent
  ]
})
export class SharedModule { }
