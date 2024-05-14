import { Component, Input } from '@angular/core';
import { NavigationExtras, Router, RouterConfigOptions, RouterModule } from '@angular/router';
import { CdkStepper, CdkStepperModule } from '@angular/cdk/stepper';
import { StepperComponent } from '../../shared/components/stepper/stepper.component';
import { CommonModule } from '@angular/common';
import { FormGroup } from '@angular/forms';
import { BasketService } from '../../basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { IBasket } from '../../shared/models/basket';
import { IOrder } from '../../shared/models/order';

@Component({
  selector: 'app-checkout-payment',
  standalone: true,
  imports: [CommonModule, RouterModule, CdkStepperModule],
  templateUrl: './checkout-payment.component.html',
  styleUrl: './checkout-payment.component.scss',
  providers: [{ provide: CdkStepper, useExisting: StepperComponent }]
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm: FormGroup;


  constructor(private stepper: CdkStepper,
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastr: ToastrService, private router: Router) { }

  onSubmit() {
    this.stepper.next();
  }

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket);

    if (orderToCreate.deliveryMethodId === 0) {
      this.toastr.error('Please select a delivery method');
      console.log(orderToCreate);
    }

    else if (!this.checkoutForm.get('addressForm').valid) {
      this.toastr.error('Please provide required information');
    }

    else {
      this.checkoutService.createOrder(orderToCreate).subscribe({
        next: (order: IOrder) => {
          this.toastr.success('Order created successfully');
          this.basketService.deleteBasket(basket);

          const navigationExtras: NavigationExtras = { state: order };
          this.router.navigate(['checkout/success', navigationExtras]);
        },
        error: (error) => {
          this.toastr.error(error.message);
          console.log(error);
        }
      });
    }

  }

  private getOrderToCreate(basket: IBasket) {
    return {
      basketId: basket.id,
      deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shipToAddress: this.checkoutForm.get('addressForm').value
    }
  }
}
