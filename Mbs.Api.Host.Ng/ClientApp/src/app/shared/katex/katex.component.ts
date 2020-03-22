import { Component, EventEmitter, Input, Output } from '@angular/core';
import * as katex from 'katex';

@Component({
  selector: 'mb-katex',
  templateUrl: './katex.component.html',
  styleUrls: ['./katex.component.scss']
})
export class KatexComponent {
  @Input() equation: string;
  @Input() options?: katex.KatexOptions;
  @Output() onError = new EventEmitter<any>();

  public hasError(error: any): void {
    this.onError.emit(error);
  }
}
