import { Component, Input } from '@angular/core';
import { QuoteParameters } from './quote-parameters';

@Component({
  selector: 'app-mbs-data-generators-quote-parameters',
  templateUrl: './quote-parameters.component.html',
  styleUrls: ['./quote-parameters.component.scss']
})
export class QuoteParametersComponent {
  @Input() quoteParameters: QuoteParameters;
  eq1 = '$(ask, bid)_t=mid_t\\cdot (1 \\pm ρ_s),$';
  eq2 = '$(ask size, bid size)_t=(α_s, β_s)=const,$';

  eq1k = '(ask, bid)_t=mid_t\\cdot (1 \\pm ρ_s),';
  eq2k = '(ask size, bid size)_t=(α_s, β_s)=const,';
}
