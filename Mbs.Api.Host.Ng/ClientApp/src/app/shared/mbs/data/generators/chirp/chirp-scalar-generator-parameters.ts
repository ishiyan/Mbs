import { ChirpParameters } from './chirp-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { ChirpGeneratorParameters } from './chirp-generator-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, chirpParametersName, objectName } from '../constants';

/** The input parameters for the chirp scalar generator. */
export class ChirpScalarGeneratorParameters {
    /** The number of samples to generate. */
    sampleCount: number = ChirpGeneratorParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The chirp related input parameters. */
    chirpParameters: ChirpParameters = new ChirpParameters();

    constructor(data?: ChirpScalarGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): ChirpScalarGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new ChirpScalarGeneratorParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.sampleCount = data[sampleCountName];
            this.timeParameters = data[timeParametersName] ? TimeParameters.fromJS(data[timeParametersName]) :
                new TimeParameters();
            this.waveformParameters = data[waveformParametersName] ? WaveformParameters.fromJS(data[waveformParametersName]) :
                new WaveformParameters();
            this.chirpParameters = data[chirpParametersName] ? ChirpParameters.fromJS(data[chirpParametersName]) :
                new ChirpParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[chirpParametersName] = this.chirpParameters ? this.chirpParameters.toJSON() : <any>undefined;
        return data;
    }
}
