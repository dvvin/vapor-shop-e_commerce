import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./core/navbar/navbar.component";
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { IProduct } from './shared/models/product';
import { IPagination } from './shared/models/pagination';
import { CoreModule } from './core/core.module';
import { ShopModule } from './shop/shop.module';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [
    RouterOutlet,
    NavbarComponent,
    HttpClientModule,
    CommonModule,
    CoreModule,
    ShopModule
  ]
})
export class AppComponent implements OnInit {
  title = 'Vapor Shop';
  // products: IProduct[] = [];

  constructor() { }

  ngOnInit(): void {
    // this.http.get<IPagination>('http://localhost:5273/api/products?pageSize=50').subscribe(
    //   (response) => {
    //     this.products = response.data;
    //   }, error => {
    //     console.log(error);
    //   });
  }

}

