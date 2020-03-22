import { Component, Input } from '@angular/core';
import { SquareParameters } from './square-parameters';

@Component({
  selector: 'app-mbs-data-generators-square-parameters',
  templateUrl: './square-parameters.component.html',
  styleUrls: ['./square-parameters.component.scss']
})
export class SquareParametersComponent {
  @Input() squareParameters: SquareParameters;

  // tslint:disable-next-line: max-line-length
  eq1 = '$mid_t=\\left\\{\\begin{array}{rl}\\alpha+\\beta&t\\in [1, \\lambda]\\\\\\beta&t\\in [\\lambda+1, 2\\lambda]\\end{array}\\right.,$';
  eq2 = '$sample_t=mid_t+noise_t$';

  // tslint:disable-next-line: max-line-length
  eq1k = 'mid_t=\\left\\{\\begin{array}{rl}\\alpha+\\beta&t\\in [1, \\lambda]\\\\\\beta&t\\in [\\lambda+1, 2\\lambda]\\end{array}\\right.,';
  eq2k = 'sample_t=mid_t+noise_t';
}
