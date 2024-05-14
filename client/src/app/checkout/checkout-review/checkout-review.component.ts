import { Component } from '@angular/core';
import { BasketSummaryComponent } from '../../shared/components/basket-summary/basket-summary.component';
import { RouterModule } from '@angular/router';
import { CdkStepper, CdkStepperModule } from '@angular/cdk/stepper';
import { StepperComponent } from '../../shared/components/stepper/stepper.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-checkout-review',
  standalone: true,
  imports: [BasketSummaryComponent, CommonModule, RouterModule, CdkStepperModule],
  templateUrl: './checkout-review.component.html',
  styleUrl: './checkout-review.component.scss',
  providers: [{ provide: CdkStepper, useExisting: StepperComponent }]
})
export class CheckoutReviewComponent {

  constructor(private stepper: CdkStepper) { }
  
  onSubmit() {
    this.stepper.next();
  }
}
