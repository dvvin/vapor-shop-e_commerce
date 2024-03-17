import { Component, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { CommonModule } from '@angular/common';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from '../../core/breadcrumb.service';

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

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService) { }

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
}
