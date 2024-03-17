import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { SectionHeaderComponent } from './section-header/section-header.component';

@NgModule({
  declarations: [],
  imports: [
    NavbarComponent,
    CommonModule,
    SectionHeaderComponent
  ],
  exports: [NavbarComponent],
})
export class CoreModule {}
