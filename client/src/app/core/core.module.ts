import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  declarations: [],
  imports: [
    NavbarComponent,
    CommonModule,
  ],
  exports: [NavbarComponent],
})
export class CoreModule {}
