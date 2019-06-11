import { Component, Input } from '@angular/core';
import { TradeParameters } from '../../../data/generators/trade-parameters';

@Component({
    selector: 'app-mbs-data-generators-trade-parameters',
    templateUrl: './trade-parameters.component.html',
    styleUrls: ['./trade-parameters.component.scss']
})
export class TradeParametersComponent {
    @Input() tradeParameters: TradeParameters;
}
