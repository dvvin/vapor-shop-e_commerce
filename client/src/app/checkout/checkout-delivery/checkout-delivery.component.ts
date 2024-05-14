import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CheckoutService } from '../checkout.service';
import { IDeliveryMethod } from '../../shared/models/deliveryMethod';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CdkStepper, CdkStepperModule } from '@angular/cdk/stepper';
import { StepperComponent } from '../../shared/components/stepper/stepper.component';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-checkout-delivery',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule, CdkStepperModule],
  templateUrl: './checkout-delivery.component.html',
  styleUrl: './checkout-delivery.component.scss',
  providers: [{ provide: CdkStepper, useExisting: StepperComponent }]

})
export class CheckoutDeliveryComponent implements OnInit {
  @Input() checkoutForm: FormGroup
  deliveryMethods: IDeliveryMethod[]

  constructor(private checkoutService: CheckoutService, private stepper: CdkStepper, private basketService: BasketService) { }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe((dm: IDeliveryMethod[]) => {
      this.deliveryMethods = dm;
    })
  }

  onSubmit() {
    this.stepper.next();
  }

  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    this.basketService.setShippingPrice(deliveryMethod);
  }
}
