import { NgModule } from '@angular/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';

@NgModule({
  declarations: [],
  imports: [
    PaginationModule.forRoot(),
    PagingHeaderComponent,
    PagerComponent
  ],
  exports: [
    PagingHeaderComponent,
    PagerComponent
  ]
})
export class SharedModule { }
