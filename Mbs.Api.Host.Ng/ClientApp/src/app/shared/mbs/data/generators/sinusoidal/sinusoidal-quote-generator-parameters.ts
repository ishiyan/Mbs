import { SinusoidalParameters } from './sinusoidal-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { QuoteParameters } from '../quote-parameters';
import { SyntheticDataParameters } from '../synthetic-data-parameters';
import {
  sampleCountName, timeParametersName, waveformParametersName, sinusoidalParametersName, quoteParametersName,
  objectName
} from '../constants';

/** The input parameters for the sinusoidal quote generator. */
export class SinusoidalQuoteGeneratorParameters {
  /** The number of samples to generate. */
  sampleCount: number = SyntheticDataParameters.defaultSampleCount;

  /** The time related input parameters. */
  timeParameters: TimeParameters = new TimeParameters();

  /** The waveform related input parameters. */
  waveformParameters: WaveformParameters = new WaveformParameters();

  /** The sinusoidal related input parameters. */
  sinusoidalParameters: SinusoidalParameters = new SinusoidalParameters();

  /** The quote related input parameters. */
  quoteParameters: QuoteParameters = new QuoteParameters();

  constructor(data?: SinusoidalQuoteGeneratorParameters) {
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
    data[sinusoidalParametersName] = this.sinusoidalParameters ? this.sinusoidalParameters.toJSON() : <any>undefined;
    data[quoteParametersName] = this.quoteParameters ? this.quoteParameters.toJSON() : <any>undefined;
    return data;
  }
}
