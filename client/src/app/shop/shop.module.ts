import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ShopComponent,
    HttpClientModule,
    SharedModule
  ],
  exports: [ShopComponent]
})
export class ShopModule { }
