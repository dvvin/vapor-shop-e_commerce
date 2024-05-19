import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket';
import { IItem } from '../shared/components/basket-summary/basket-summary.component';
import { BasketService } from './basket.service';
import { CommonModule } from '@angular/common';
import { OrderTotalsComponent } from '../shared/components/order-totals/order-totals.component';
import { BasketSummaryComponent } from '../shared/components/basket-summary/basket-summary.component';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule, OrderTotalsComponent, BasketSummaryComponent],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss'
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket>;
  basketTotal$: Observable<IBasketTotals>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$
    this.basketTotal$ = this.basketService.basketTotal$
  }

  removeItemFromBasket(item: IItem) {
    this.basketService.removeItemFromBasket(item as IBasketItem);
  }

  incrementItemQuantity(item: IItem) {
    this.basketService.incrementItemQuantity(item as IBasketItem);
  }

  decrementItemQuantity(item: IItem) {
    this.basketService.decrementItemQuantity(item as IBasketItem);
  }
}
