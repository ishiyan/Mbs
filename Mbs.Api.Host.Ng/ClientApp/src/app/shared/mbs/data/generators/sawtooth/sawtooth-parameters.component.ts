import { Component, Input } from '@angular/core';
import { SawtoothParameters } from './sawtooth-parameters';
import { SawtoothShape } from './sawtooth-shape.enum';
import { Enums } from '../../../utils/enums';

@Component({
  selector: 'app-mbs-data-generators-sawtooth-parameters',
  templateUrl: './sawtooth-parameters.component.html',
  styleUrls: ['./sawtooth-parameters.component.scss']
})
export class SawtoothParametersComponent {
  @Input() sawtoothParameters: SawtoothParameters;

  eq1 = '$mid_t=\\alpha\\cdot shape_t\\cdot t+\\beta,$';
  eq2 = '$sample_t=mid_t+noise_t$';

  eq1k = 'mid_t=\\alpha\\cdot shape_t\\cdot t+\\beta,';
  eq2k = 'sample_t=mid_t+noise_t';

  sawtoothShapes = Object.keys(SawtoothShape);

  compare = Enums.compare;
}
