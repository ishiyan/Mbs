import { Component, Input } from '@angular/core';
import { OhlcvParameters } from '../../../data/generators/ohlcv-parameters';

@Component({
    selector: 'app-mbs-data-generators-ohlcv-parameters',
    templateUrl: './ohlcv-parameters.component.html',
    styleUrls: ['./ohlcv-parameters.component.scss']
})
export class OhlcvParametersComponent {
    @Input() ohlcvParameters: OhlcvParameters;
}
