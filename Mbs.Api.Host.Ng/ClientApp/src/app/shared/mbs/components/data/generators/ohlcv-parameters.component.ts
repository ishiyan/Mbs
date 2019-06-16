import { Component, Input, OnInit } from '@angular/core';
import { OhlcvParameters } from '../../../data/generators/ohlcv-parameters';
// import { MathJaxDirective } from '../../../../math-jax/math-jax.directive';

@Component({
    selector: 'app-mbs-data-generators-ohlcv-parameters',
    templateUrl: './ohlcv-parameters.component.html',
    styleUrls: ['./ohlcv-parameters.component.scss']
})
export class OhlcvParametersComponent implements OnInit {
    @Input() ohlcvParameters: OhlcvParameters;
    eq1 = '$(high,low)_t=mid_t\\cdot (1 \\pm ρ_s),$';
    eq2 = '$(open,close)_t=mid_t\\cdot (1 \\pm ρ_b),$';
    eq3 = '$v_t=\\nu$';

    ngOnInit() {
        // MathJax.Hub.Queue(['Typeset', MathJax.Hub, 'MathJax']);
    }
}
