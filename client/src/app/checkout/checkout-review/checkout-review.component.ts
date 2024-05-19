import { Component } from '@angular/core';
import { BasketSummaryComponent, IItem } from '../../shared/components/basket-summary/basket-summary.component';
import { RouterModule } from '@angular/router';
import { CdkStepper, CdkStepperModule } from '@angular/cdk/stepper';
import { StepperComponent } from '../../shared/components/stepper/stepper.component';
import { CommonModule } from '@angular/common';
import { BasketService } from '../../basket/basket.service';
import { Observable } from 'rxjs';
import { IBasket } from '../../shared/models/basket';

@Component({
  selector: 'app-checkout-review',
  standalone: true,
  imports: [BasketSummaryComponent, CommonModule, RouterModule, CdkStepperModule],
  templateUrl: './checkout-review.component.html',
  styleUrl: './checkout-review.component.scss',
  providers: [{ provide: CdkStepper, useExisting: StepperComponent }]
})
export class CheckoutReviewComponent {
  basket$: Observable<IBasket>;
  items: IItem[] = [];

  constructor(private stepper: CdkStepper, private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.basket$.subscribe((basket) => {
      this.items = basket.items;
    });
  }

  onSubmit() {
    this.stepper.next();
  }
}
