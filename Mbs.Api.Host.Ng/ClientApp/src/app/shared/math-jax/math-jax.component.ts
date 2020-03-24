import { Component, EventEmitter, Input, Output, OnChanges, ViewChild } from '@angular/core';
import { MathJaxDirective } from './math-jax.directive';

@Component({
  selector: 'mb-mathjax',
  templateUrl: './math-jax.component.html',
  styleUrls: ['./math-jax.component.scss']
})
export class MathJaxComponent implements OnChanges {
  @Input() expression: string;

  @ViewChild(MathJaxDirective)
  mathJaxDirective: MathJaxDirective;

  ngOnChanges(changes: any): void {
    if (this.mathJaxDirective) {
      this.mathJaxDirective.element.innerHTML = this.expression;
      this.mathJaxDirective.typeset();
    }
  }
}
