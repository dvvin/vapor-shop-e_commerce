import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./core/navbar/navbar.component";
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { SectionHeaderComponent } from './core/section-header/section-header.component';
import { Observable } from 'rxjs';
import { BusyService } from './core/services/busy.service';
import { BasketService } from './basket/basket.service';
import { isPlatformBrowser } from '@angular/common';
import { PLATFORM_ID, Inject } from '@angular/core';

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
    HomeModule,
    SectionHeaderComponent
  ],
  providers: [BasketService],
})
export class AppComponent implements OnInit {
  title = 'Vapor Shop';

  isLoading$: Observable<boolean>;

  constructor(
    private loadingService: BusyService,
    private cdRef: ChangeDetectorRef,
    private basketService: BasketService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.isLoading$ = this.loadingService.loading$;
  }

  ngOnInit(): void {
    this.isLoading$.subscribe(() => {
      this.cdRef.detectChanges();
    });

    if (isPlatformBrowser(this.platformId)) {
      const basketId = localStorage.getItem('basket_id');
      if (basketId) {
        this.basketService.getBasket(basketId).subscribe({
          next: () => {
            console.log('Initialized basket');
          },
          error: (error) => {
            console.log(error);
          }
        });
      }
    }
  }
}

