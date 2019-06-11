import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MathJaxDirective } from './math-jax.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [MathJaxDirective],
  exports: [MathJaxDirective]
})
export class MathJaxModule { }
