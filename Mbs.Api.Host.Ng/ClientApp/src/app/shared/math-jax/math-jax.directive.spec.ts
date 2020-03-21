import { MathJaxDirective } from './math-jax.directive';
import { ElementRef } from '@angular/core';

describe('MathJaxDirective', () => {
  it('should create an instance', () => {
    const directive = new MathJaxDirective(new ElementRef(null));
    expect(directive).toBeTruthy();
  });
});
