import { ElementRef, Injectable } from '@angular/core';
import * as katex from 'katex';

@Injectable()
export class KatexService {

  render(equation: string, element: ElementRef, options?: katex.KatexOptions) {
    return katex.render(equation, element.nativeElement, options);
  }

  renderToString(equation: string, options?: katex.KatexOptions): string {
    return katex.renderToString(equation, options);
  }
}
