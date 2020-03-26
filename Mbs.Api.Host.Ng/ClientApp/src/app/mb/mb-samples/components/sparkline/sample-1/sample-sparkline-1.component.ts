import { Component } from '@angular/core';

import { SparklineConfiguration } from '../../../../../shared/mbs/charts/sparkline/sparkline-configuration.interface';

import { dataTestOhlcv } from '../../../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-ohlcv';
import { dataTestBw } from '../../../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-bw';
import { dataTestMa } from '../../../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-ma';

@Component({
  selector: 'mb-sample-sparkline-1',
  templateUrl: './sample-sparkline-1.component.html',
  styleUrls: ['./sample-sparkline-1.component.scss']
})
export class SampleSparkline1Component {

  dataOhlcv = dataTestOhlcv;
  dataScalar = dataTestBw;
  dataScalarWithNaN = dataTestMa;

  readonly dataArray = [
    { data: this.dataOhlcv, name: 'Ohlcv data' },
    { data: this.dataScalar, name: 'Scalar data' },
    { data: this.dataScalarWithNaN, name: 'Scalar data with NaN' }
  ];
  readonly configLine: SparklineConfiguration = { fillColor: undefined, strokeColor: 'steelblue', strokeWidth: 0.1 };
  readonly configFill: SparklineConfiguration = { fillColor: 'steelblue', strokeColor: undefined, strokeWidth: 0.1 };
  selectedData = this.dataArray[0];
  configurationIsLine = false;

  styleIsLineChanged() {
    this.configurationIsLine = !this.configurationIsLine;
  }
}
