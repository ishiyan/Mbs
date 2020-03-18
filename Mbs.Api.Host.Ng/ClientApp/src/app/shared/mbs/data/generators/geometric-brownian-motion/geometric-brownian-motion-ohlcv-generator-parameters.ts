import { GeometricBrownianMotionParameters } from './geometric-brownian-motion-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { OhlcvParameters } from '../ohlcv-parameters';
import { SyntheticDataParameters } from '../synthetic-data-parameters';
import {
  sampleCountName, timeParametersName, waveformParametersName, gbmParametersName, ohlcvParametersName,
  objectName
} from '../constants';

/** The input parameters for the geometric Brownian motion ohlcv generator. */
export class GeometricBrownianMotionOhlcvGeneratorParameters {
  /** The number of samples to generate. */
  sampleCount: number = SyntheticDataParameters.defaultSampleCount;

  /** The time related input parameters. */
  timeParameters: TimeParameters = new TimeParameters();

  /** The waveform related input parameters. */
  waveformParameters: WaveformParameters = new WaveformParameters();

  /** The geometric Brownian motion related input parameters. */
  gbmParameters: GeometricBrownianMotionParameters = new GeometricBrownianMotionParameters();

  /** The ohlcv related input parameters. */
  ohlcvParameters: OhlcvParameters = new OhlcvParameters();

  constructor(data?: GeometricBrownianMotionOhlcvGeneratorParameters) {
    if (data) {
      for (const property in data) {
        if (data.hasOwnProperty(property)) {
          (<any>this)[property] = (<any>data)[property];
        }
      }
    }
  }

  toJSON(data?: any) {
    data = typeof data === objectName ? data : {};
    data[sampleCountName] = this.sampleCount;
    data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
    data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
    data[gbmParametersName] = this.gbmParameters ? this.gbmParameters.toJSON() : <any>undefined;
    data[ohlcvParametersName] = this.ohlcvParameters ? this.ohlcvParameters.toJSON() : <any>undefined;
    return data;
  }
}
