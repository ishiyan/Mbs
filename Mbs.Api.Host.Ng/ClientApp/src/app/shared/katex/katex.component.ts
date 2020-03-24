import { Component, EventEmitter, Input, Output } from '@angular/core';
import * as katex from 'katex';

@Component({
  selector: 'mb-katex',
  templateUrl: './katex.component.html',
  styleUrls: ['./katex.component.scss']
})
export class KatexComponent {
  @Input() expression: string;
  @Input() options?: katex.KatexOptions;
  @Output() hasError = new EventEmitter<any>();

  public outputError(error: any): void {
    this.hasError.emit(error);
  }
}
