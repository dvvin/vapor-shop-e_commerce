import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { IOrder } from '../../shared/models/order';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbService } from '../../core/breadcrumb.service';
import { OrdersService } from '../orders.service';
import { CommonModule } from '@angular/common';
import { BasketSummaryComponent } from '../../shared/components/basket-summary/basket-summary.component';
import { OrderTotalsComponent } from '../../shared/components/order-totals/order-totals.component';

@Component({
  selector: 'app-order-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, BasketSummaryComponent, OrderTotalsComponent],
  templateUrl: './order-detail.component.html',
  styleUrl: './order-detail.component.scss'
})
export class OrderDetailComponent implements OnInit {
  order: IOrder;

  constructor(
    private route: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private ordersService: OrdersService,
    private cdRef: ChangeDetectorRef
  ) {
    this.breadcrumbService.set('@OrderDetail', '');
  }

  ngOnInit(): void {
    this.ordersService.getOrderDetailed(+this.route.snapshot.paramMap.get('id')).subscribe({
      next: (order: IOrder) => {
        this.order = order;
        this.breadcrumbService.set('@OrderDetail', `Order #${order.id} - ${order.status}`);
        this.cdRef.markForCheck();
      },
      error: error => console.log(error)
    })
  }
}
