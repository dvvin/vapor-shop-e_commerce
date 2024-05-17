import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { IOrder } from '../../shared/models/order';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-checkout-success',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './checkout-success.component.html',
  styleUrl: './checkout-success.component.scss'
})
export class CheckoutSuccessComponent implements OnInit {
  order: IOrder;
  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras?.state;

    if (state && state['order']) {
      this['order'] = state['order'] as IOrder;
    }
  }

  ngOnInit(): void {
    window.history.replaceState({}, document.title, window.location.pathname);
  }
}
