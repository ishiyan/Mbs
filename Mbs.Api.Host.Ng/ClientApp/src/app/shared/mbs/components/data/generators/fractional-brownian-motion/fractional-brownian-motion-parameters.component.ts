import { Component, Input } from '@angular/core';
import { UniformRandomGeneratorKind } from '../../../../data/generators/uniform-random-generator-kind.enum';
import { NormalRandomGeneratorKind } from 'src/app/shared/mbs/data/generators/normal-random-generator-kind.enum';
// tslint:disable-next-line: max-line-length
import { FractionalBrownianMotionParameters } from '../../../../data/generators/fractional-brownian-motion/fractional-brownian-motion-parameters';
// tslint:disable-next-line: max-line-length
import { FractionalBrownianMotionAlgorithm } from 'src/app/shared/mbs/data/generators/fractional-brownian-motion/fractional-brownian-motion-algorithm.enum';
import { Enums } from '../../../enums';

@Component({
    selector: 'app-mbs-data-generators-fractional-brownian-motion-parameters',
    templateUrl: './fractional-brownian-motion-parameters.component.html',
    styleUrls: ['./fractional-brownian-motion-parameters.component.scss']
})
export class FractionalBrownianMotionParametersComponent {
    @Input() fractionalBrownianMotionParameters: FractionalBrownianMotionParameters;

    algorithms = Object.keys(FractionalBrownianMotionAlgorithm);
    normalRandomGenerators = Object.keys(NormalRandomGeneratorKind);
    uniformRandomGenerators = Object.keys(UniformRandomGeneratorKind);

    compare = Enums.compare;
}
