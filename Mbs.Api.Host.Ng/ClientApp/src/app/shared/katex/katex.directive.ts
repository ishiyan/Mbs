import { Directive, ElementRef, EventEmitter, Input, Output, } from '@angular/core';
import * as katex from 'katex';

import { KatexService } from './katex.service';

@Directive({
  selector: 'katex, [katex]',
})
export class KatexDirective {
  @Input('katex') equation: string;
  @Input('katex-options') options: katex.KatexOptions;
  @Output() onError = new EventEmitter<any>();

  constructor(private element: ElementRef, private katexService: KatexService) { }

  ngOnChanges() {
    try {
      this.katexService.render(this.equation, this.element, this.options);
    } catch (e) {
      this.onError.emit(e);
    }
  }
}
