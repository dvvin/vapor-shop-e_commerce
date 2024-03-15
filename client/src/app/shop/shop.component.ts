import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';
import { CommonModule } from '@angular/common';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [CommonModule, ProductItemComponent, SharedModule],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
  providers: [ShopService]
})

export class ShopComponent implements OnInit {
  @ViewChild('search', { static: false })
  searchTerm: ElementRef = {} as ElementRef
  products: IProduct[] = [];
  brands: IBrand[] = [];
  types: IType[] = [];
  shopParams = new ShopParams();

  totalCount = 0;
  isCollapseOneOpen = false;
  totalPages: number = 0;

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams)
      .subscribe({
        next: (response) => {
          console.log(response);
          this.products = response!.data;
          this.shopParams.pageNumber = response!.pageIndex;
          this.shopParams.pageSize = response!.pageSize;
          this.totalCount = response!.count;
          this.calculateTotalPages();
        },
        error: (error) => {
          console.log(error);
        }
      });
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: (response) => {
        console.log(response);
        this.brands = [{ id: 0, name: 'All' }, ...response];
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: (response) => {
        console.log(response);
        this.types = [{ id: 0, name: 'All' }, ...response];
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(pageNumber: number) {
    if (this.shopParams.pageNumber !== pageNumber) {
      this.shopParams.pageNumber = pageNumber;
      this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onFormSubmit(event: Event) {
    event.preventDefault();
    this.onSearch();
  }



  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }

  calculateTotalPages() {
    this.totalPages = Math.ceil(this.totalCount / this.shopParams.pageSize);
  }

  toggleCollapseOne() {
    this.isCollapseOneOpen = !this.isCollapseOneOpen;
  }
}
