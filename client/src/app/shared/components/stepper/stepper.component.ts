import { CdkStepper } from '@angular/cdk/stepper';
import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-stepper',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stepper.component.html',
  styleUrl: './stepper.component.scss',
  providers: [{ provide: CdkStepper, useExisting: StepperComponent }]
})
export class StepperComponent extends CdkStepper implements OnInit {
  @Input() linearModeSelected: boolean;

  ngOnInit(): void {
    this.linear = this.linearModeSelected
  }

  onClick(index: number) {
    this.selectedIndex = index
  }

  isDisabled(index: number): boolean {
    // Implement your logic to determine if a step should be disabled
    // For example, you can compare the index with the selectedIndex
    return index !== this.selectedIndex;
  }
}
