import { Component, Input } from '@angular/core';
import { SawtoothParameters } from '../../../../data/generators/sawtooth/sawtooth-parameters';
import { SawtoothShape } from '../../../../data/generators/sawtooth/sawtooth-shape.enum';
import { Enums } from '../../../enums';

@Component({
    selector: 'app-mbs-data-generators-sawtooth-parameters',
    templateUrl: './sawtooth-parameters.component.html',
    styleUrls: ['./sawtooth-parameters.component.scss']
})
export class SawtoothParametersComponent {
    @Input() sawtoothParameters: SawtoothParameters;

    eq1 = '$mid_t=α\\cdot shape_t\\cdot t+β,$';
    eq2 = '$sample_t=mid_t+noise_t$';

    sawtoothShapes = Object.keys(SawtoothShape);

    compare = Enums.compare;
}
