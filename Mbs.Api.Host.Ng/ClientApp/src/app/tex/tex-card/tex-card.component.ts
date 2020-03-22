import { Component, Input, ChangeDetectorRef, AfterViewChecked, ViewChild, ElementRef } from '@angular/core';

import { MathJaxDirective } from '../../shared/math-jax/math-jax.directive';
import { KatexComponent } from 'src/app/shared/katex/katex.component';
import { Sample } from '../samples/sample';

@Component({
  selector: 'app-tex-card',
  templateUrl: './tex-card.component.html',
  styleUrls: ['./tex-card.component.scss']
})
export class TexCardComponent implements AfterViewChecked {
  @Input()
  sample: Sample;
  @Input()
  showMathJax: boolean = true;
  @Input()
  showKatex: boolean = true;

  @ViewChild('renderMathJax', { static: true })
  renderMathJax: ElementRef;

  @ViewChild('renderMathJax', {read: MathJaxDirective, static: true})
  mathJaxDirective: MathJaxDirective;

  @ViewChild('renderKatex', {read: KatexComponent, static: true})
  katexomponent: KatexComponent;

  constructor(private changeDetectionRef: ChangeDetectorRef) { }

  ngAfterViewChecked() {
    this.changeDetectionRef.detectChanges();
  }

  updateMathJax(text: string) {
    if (this.showKatex) {
      this.katexomponent.equation = text;
    }
    if (this.showMathJax) {
      this.renderMathJax.nativeElement.innerHTML = '$$' + text + '$$';
      this.mathJaxDirective.typeset();  
    }
  }
}
