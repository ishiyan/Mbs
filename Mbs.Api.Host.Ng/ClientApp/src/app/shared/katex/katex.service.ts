import { ElementRef, Injectable } from '@angular/core';
import * as katex from 'katex';

@Injectable()
export class KatexService {

  render(expression: string, element: ElementRef, options?: katex.KatexOptions) {
    return katex.render(expression, element.nativeElement, options);
  }

  renderToString(expression: string, options?: katex.KatexOptions): string {
    return katex.renderToString(expression, options);
  }
}
