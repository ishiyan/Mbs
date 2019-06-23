import { SinusoidalParameters } from './sinusoidal-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { SyntheticDataParameters } from '../synthetic-data-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, sinusoidalParametersName, objectName } from '../constants';

/** The input parameters for the sinusoidal scalar generator. */
export class SinusoidalScalarGeneratorParameters {
    /** The number of samples to generate. */
    sampleCount: number = SyntheticDataParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The sinusoidal related input parameters. */
    sinusoidalParameters: SinusoidalParameters = new SinusoidalParameters();

    constructor(data?: SinusoidalScalarGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): SinusoidalScalarGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new SinusoidalScalarGeneratorParameters();
        result.init(data);
        return result;
    }

    private init(data?: any): void {
        if (data) {
            this.sampleCount = data[sampleCountName];
            this.timeParameters = data[timeParametersName] ? TimeParameters.fromJS(data[timeParametersName]) :
                new TimeParameters();
            this.waveformParameters = data[waveformParametersName] ? WaveformParameters.fromJS(data[waveformParametersName]) :
                new WaveformParameters();
            this.sinusoidalParameters = data[sinusoidalParametersName] ? SinusoidalParameters.fromJS(data[sinusoidalParametersName]) :
                new SinusoidalParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[sinusoidalParametersName] = this.sinusoidalParameters ? this.sinusoidalParameters.toJSON() : <any>undefined;
        return data;
    }
}
