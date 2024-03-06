import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./navbar/navbar.component";
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { IProduct } from './models/product';
import { IPagination } from './models/pagination';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [RouterOutlet, NavbarComponent, HttpClientModule, CommonModule]
})
export class AppComponent implements OnInit {
  title = 'Vapor Shop';
  products: IProduct[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<IPagination>('http://localhost:5273/api/products?pageSize=50').subscribe(
      (response) => {
        this.products = response.data;
      }, error => {
        console.log(error);
      });
  }

}
