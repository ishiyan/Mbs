import { Component, Input } from '@angular/core';
import { UniformRandomGeneratorKind } from '../../../data/generators/uniform-random-generator-kind.enum';
import { WaveformParameters } from '../../../data/generators/waveform-parameters';
import { Enums } from '../../enums';

@Component({
    selector: 'app-mbs-data-generators-waveform-parameters',
    templateUrl: './waveform-parameters.component.html',
    styleUrls: ['./waveform-parameters.component.scss']
})
export class WaveformParametersComponent {
    @Input() waveformParameters: WaveformParameters;

    eq1 = '$noise_t=mid_t\\cdot ρ_n\\cdot random(seed),$';

    uniformRandomGenerators: string[] = Object.keys(UniformRandomGeneratorKind);

    compare = Enums.compare;
}
