import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { RouterModule } from '@angular/router';

export interface IItem {
  id?: number;
  productId?: number;
  productName: string;
  price: number;
  pictureUrl: string;
  quantity: number;
  type?: string;
}

export interface IBasketItem extends IItem {
  brand: string;
  type: string;
}

export interface IOrderItem extends IItem {
  productId: number;
}

@Component({
  selector: 'app-basket-summary',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './basket-summary.component.html',
  styleUrl: './basket-summary.component.scss'
})
export class BasketSummaryComponent implements OnInit {
  @Output() decrement: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>()
  @Output() increment: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>()
  @Output() remove: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>()

  @Input() isBasket = true
  @Input() isOrder = false
  @Input() items: IItem[] = [];

  constructor() { }

  ngOnInit(): void { }

  decrementItemQuantity(item: IItem) {
    this.decrement.emit(item as IBasketItem)
  }

  incrementItemQuantity(item: IItem) {
    this.increment.emit(item as IBasketItem)
  }

  removeItemFromBasket(item: IItem) {
    this.remove.emit(item as IBasketItem)
  }
}
