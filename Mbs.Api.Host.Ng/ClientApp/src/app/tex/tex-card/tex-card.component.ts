import { Component, Input, ChangeDetectorRef, AfterViewChecked, ViewChild, ElementRef } from '@angular/core';

import { MathJaxDirective } from '../../shared/math-jax/math-jax.directive';
import { Sample } from '../samples/sample';

@Component({
  selector: 'app-tex-card',
  templateUrl: './tex-card.component.html',
  styleUrls: ['./tex-card.component.scss']
})
export class TexCardComponent implements AfterViewChecked {
  @Input()
  sample: Sample;

  @ViewChild('render', { static: true })
  render: ElementRef;

  @ViewChild('render', {read: MathJaxDirective, static: true})
  mathJaxDirective: MathJaxDirective;

  constructor(private changeDetectionRef: ChangeDetectorRef) { }


  ngAfterViewChecked() {
    this.changeDetectionRef.detectChanges();
  }

  updateMathJax(text: string) {
    this.render.nativeElement.innerHTML = text;
    this.mathJaxDirective.typeset();
  }
}
