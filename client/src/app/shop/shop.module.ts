import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';
import { ShopRoutingModule } from './shop-routing.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ShopComponent,
    HttpClientModule,
    SharedModule,
    ShopRoutingModule
  ],
})
export class ShopModule { }
