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

    uniformRandomGenerators: string[] = Object.keys(UniformRandomGeneratorKind);

    compare = Enums.compare;
}
