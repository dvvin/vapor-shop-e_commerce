import { Component, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { CommonModule } from '@angular/common';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from '../../core/breadcrumb.service';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss',
  providers: [ShopService]
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct | undefined;
  quantity = 1;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService, private basketService: BasketService) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')!).subscribe({
      next: (response) => {
        this.product = response
        this.bcService.updateProductTitle(this.product.name);
      },
      error: (error) => {
        console.log(error)
      }
    })
  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
}
