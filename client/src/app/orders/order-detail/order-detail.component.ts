import { Component, OnInit } from '@angular/core';
import { IOrder } from '../../shared/models/order';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbService } from '../../core/breadcrumb.service';
import { OrdersService } from '../orders.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './order-detail.component.html',
  styleUrl: './order-detail.component.scss'
})
export class OrderDetailComponent implements OnInit {
  order: IOrder;

  constructor(private route: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private ordersService: OrdersService) {
    this.breadcrumbService.set('@OrderDetail', '');
  }

  ngOnInit(): void {
    this.ordersService.getOrderDetailed(+this.route.snapshot.paramMap.get('id')).subscribe({
      next: (order: IOrder) => {
        this.order = order;
        this.breadcrumbService.set('@OrderDetail', `Order #${order.id} - ${order.status}`);
      },
      error: error => console.log(error)
    })
  }
}
