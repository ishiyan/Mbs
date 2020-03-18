import { SquareParameters } from './square-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { TradeParameters } from '../trade-parameters';
import { SyntheticDataParameters } from '../synthetic-data-parameters';
import {
  sampleCountName, timeParametersName, waveformParametersName, squareParametersName, tradeParametersName,
  objectName
} from '../constants';

/** The input parameters for the square trade generator. */
export class SquareTradeGeneratorParameters {
  /** The number of samples to generate. */
  sampleCount: number = SyntheticDataParameters.defaultSampleCount;

  /** The time related input parameters. */
  timeParameters: TimeParameters = new TimeParameters();

  /** The waveform related input parameters. */
  waveformParameters: WaveformParameters = new WaveformParameters();

  /** The square related input parameters. */
  squareParameters: SquareParameters = new SquareParameters();

  /** The trade related input parameters. */
  tradeParameters: TradeParameters = new TradeParameters();

  constructor(data?: SquareTradeGeneratorParameters) {
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
    data[squareParametersName] = this.squareParameters ? this.squareParameters.toJSON() : <any>undefined;
    data[tradeParametersName] = this.tradeParameters ? this.tradeParameters.toJSON() : <any>undefined;
    return data;
  }
}
