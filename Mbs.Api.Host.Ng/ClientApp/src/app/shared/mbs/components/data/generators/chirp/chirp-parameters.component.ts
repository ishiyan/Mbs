import { Component, Input } from '@angular/core';
import { ChirpParameters } from '../../../../data/generators/chirp/chirp-parameters';
import { ChirpSweep } from '../../../../data/generators/chirp/chirp-sweep.enum';
import { Enums } from '../../../enums';

@Component({
    selector: 'app-mbs-data-generators-chirp-parameters',
    templateUrl: './chirp-parameters.component.html',
    styleUrls: ['./chirp-parameters.component.scss']
})
export class ChirpParametersComponent {
    @Input() chirpParameters: ChirpParameters;

    eq1 = '$mid_t=a\\cdot\\cos(sweep_t\\cdot t+\\varphi\\cdot\\pi)+b,$';
    eq2 = '$sweep_1=\\frac{2\\pi}{\\lambda_1},$';
    eq3 = '$sweep_L=\\frac{2\\pi}{\\lambda_L},$';
    eq4 = '$sample_t=mid_t+noise_t$';

    chirpSweeps = Object.keys(ChirpSweep);

    compare = Enums.compare;
}
