import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { Input } from '@angular/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent implements OnInit {
  @Input() product!: IProduct;

  constructor() { }

  ngOnInit(): void {

  }
}
