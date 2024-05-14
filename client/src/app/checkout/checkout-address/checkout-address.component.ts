import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from '../../shared/text-input/text-input.component';
import { RouterModule } from '@angular/router';
import { CdkStepper, CdkStepperModule } from '@angular/cdk/stepper';
import { StepperComponent } from '../../shared/components/stepper/stepper.component';
import { AccountService } from '../../account/account.service';
import { ToastrService } from 'ngx-toastr';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-checkout-address',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule, TextInputComponent, CdkStepperModule, SharedModule],
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss'],
  providers: [{ provide: CdkStepper, useExisting: StepperComponent }]
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutForm: FormGroup;

  constructor(private stepper: CdkStepper, private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.stepper.next();
  }

  saveUserAddress() {
    this.accountService.updateUserAddress(this.checkoutForm.get('addressForm').value)
      .subscribe({
        next: () => {
          this.toastr.success('Address saved');
          // this.stepper.next();
        },
        error: (error) => {
          this.toastr.error(error.message);
          console.log(error);
        }
      })
  }
}
