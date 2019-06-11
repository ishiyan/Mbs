import { Component, Input } from '@angular/core';
import { QuoteParameters } from '../../../data/generators/quote-parameters';

@Component({
    selector: 'app-mbs-data-generators-quote-parameters',
    templateUrl: './quote-parameters.component.html',
    styleUrls: ['./quote-parameters.component.scss']
})
export class QuoteParametersComponent {
    @Input() quoteParameters: QuoteParameters;
}
