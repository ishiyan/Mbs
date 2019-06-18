import { Component, Input } from '@angular/core';
import { SinusoidalParameters } from '../../../../data/generators/sinusoidal/sinusoidal-parameters';

@Component({
    selector: 'app-mbs-data-generators-sinusoidal-parameters',
    templateUrl: './sinusoidal-parameters.component.html',
    styleUrls: ['./sinusoidal-parameters.component.scss']
})
export class SinusoidalParametersComponent {
    @Input() sinusoidalParameters: SinusoidalParameters;

    eq1 = '$mid_t=α\\cdot\\cos(\\frac{2\\pi}{\\lambda}t+\\varphi\\cdot\\pi)+β,$';
    eq2 = '$sample_t=mid_t+noise_t$';
}
