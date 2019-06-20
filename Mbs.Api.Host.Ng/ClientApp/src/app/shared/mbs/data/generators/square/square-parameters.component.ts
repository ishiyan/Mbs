import { Component, Input } from '@angular/core';
import { SquareParameters } from './square-parameters';

@Component({
    selector: 'app-mbs-data-generators-square-parameters',
    templateUrl: './square-parameters.component.html',
    styleUrls: ['./square-parameters.component.scss']
})
export class SquareParametersComponent {
    @Input() squareParameters: SquareParameters;

    eq1 = '$mid_t=\\left\\{\\begin{array}{rl}α+β&t\\in [1, \\lambda]\\\\β&t\\in [\\lambda+1, 2\\lambda]\\end{array}\\right.,$';
    eq2 = '$sample_t=mid_t+noise_t$';
}
