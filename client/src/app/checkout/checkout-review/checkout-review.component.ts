import { Component, Inject, Input, PLATFORM_ID } from '@angular/core';
import { BasketSummaryComponent, IItem } from '../../shared/components/basket-summary/basket-summary.component';
import { RouterModule } from '@angular/router';
import { CdkStepper, CdkStepperModule } from '@angular/cdk/stepper';
import { StepperComponent } from '../../shared/components/stepper/stepper.component';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { BasketService } from '../../basket/basket.service';
import { Observable } from 'rxjs';
import { IBasket } from '../../shared/models/basket';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-checkout-review',
  standalone: true,
  imports: [BasketSummaryComponent, CommonModule, RouterModule, CdkStepperModule],
  templateUrl: './checkout-review.component.html',
  styleUrl: './checkout-review.component.scss',
  providers: [{ provide: CdkStepper, useExisting: StepperComponent }]
})
export class CheckoutReviewComponent {
  @Input() appStepper: CdkStepper;
  basket$: Observable<IBasket>;
  items: IItem[] = [];

  constructor(private stepper: CdkStepper, private basketService: BasketService, private toastr: ToastrService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.basket$ = this.basketService.basket$;
      this.basket$.subscribe((basket) => {
        if (basket) {
          this.items = basket.items;
        } else {
          this.items = [];
        }
      });
    }
  }

  createPaymentIntent() {
    return this.basketService.createPaymentIntent().subscribe({
      next: (response: any) => {
        this.appStepper.next();
      },
      error: (error: any) => {
        console.log(error);
      }
    });
  }

  onSubmit() {
    this.stepper.next();
  }
}
