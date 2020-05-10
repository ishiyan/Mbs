import { Component } from '@angular/core';

import { Scalar } from '../../../../shared/mbs/data/entities/scalar';
import { SparklineConfiguration } from '../../../../shared/mbs/charts/sparkline/sparkline-configuration.interface';

import { TestInstrument } from '../../test-data/test-instrument.interface';
import { alexDefensive } from '../../test-data/alex-defensive';
import { alexCautious } from '../../test-data/alex-cautious';
import { alexOffensive } from '../../test-data/alex-offensive';
import { alexSpeculative } from '../../test-data/alex-speculative';
import { alexVerySpeculative } from '../../test-data/alex-very-speculative';
import { bamEuroBondSicav } from '../../test-data/bam-euro-bond-sicav';
import { bamGlDevMarketsEqSicav } from '../../test-data/bam-gl-dev-markets-eq-sicav';
import { bamWeightedLowRiskBenchmark } from '../../test-data/bam-weighted-low-risk-benchmark';
import { stoxxGlobal3000Nr } from '../../test-data/stoxx-global-3000-nr';
import { crudeOilFuture } from '../../test-data/crude-oil-future';

@Component({
  selector: 'mb-fixed-single',
  templateUrl: './single.component.html',
  styleUrls: ['./single.component.scss']
})
export class SingleComponent {

  readonly hrInstruments: TestInstrument[] = [
    alexSpeculative, alexVerySpeculative, bamGlDevMarketsEqSicav, stoxxGlobal3000Nr, crudeOilFuture
  ];
  readonly lrInstruments: TestInstrument[] = [
    alexDefensive, alexCautious, bamEuroBondSicav, bamWeightedLowRiskBenchmark,
  ];

  readonly hrFill: SparklineConfiguration = { fillColor: '#31B9B0', strokeColor: undefined, strokeWidth: 1 };
  readonly lrFill: SparklineConfiguration = { fillColor: '#5AF0BC', strokeColor: undefined, strokeWidth: 1 };
  readonly initialLine: SparklineConfiguration = { fillColor: undefined, strokeColor: 'red', strokeWidth: 1 };

  hrInstrument: TestInstrument = this.hrInstruments[0];
  lrInstrument: TestInstrument = this.lrInstruments[0];

  readonly alexInstruments: TestInstrument[] = [
    alexDefensive, alexCautious, alexOffensive, alexSpeculative, alexVerySpeculative,
    bamEuroBondSicav, bamGlDevMarketsEqSicav, bamWeightedLowRiskBenchmark,
    stoxxGlobal3000Nr, crudeOilFuture
  ];

  initialAmount = 10000;
  hrRatio = 55;

  get hrCalculated(): Scalar[] {
    const allocation = this.initialAmount * this.hrRatio / 100;
    const position = allocation / this.hrInstrument.data[0].value;
    const calculated: Scalar[] = [];
    for (let d of this.hrInstrument.data) {
      calculated.push({time: d.time, value: d.value * position});
    }
    return calculated;
  }

  get lrCalculated(): Scalar[] {
    const allocation = this.initialAmount * (1 - this.hrRatio / 100);
    const position = allocation / this.lrInstrument.data[0].value;
    const calculated: Scalar[] = [];
    for (let d of this.lrInstrument.data) {
      calculated.push({time: d.time, value: d.value * position});
    }
    return calculated;
  }

  get initialCalculated(): Scalar[] {
    const calculated: Scalar[] = [];
    for (let d of this.hrInstrument.data) {
      calculated.push({time: d.time, value: this.initialAmount});
    }
    return calculated;
  }
}
