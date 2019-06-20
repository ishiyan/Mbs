import { SawtoothParameters } from './sawtooth-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { SawtoothGeneratorParameters } from './sawtooth-generator-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, sawtoothParametersName,
    objectName } from '../constants';

/** The input parameters for the sawtooth scalar generator. */
export class SawtoothScalarGeneratorParameters {
    /** The number of samples to generate. */
    sampleCount: number = SawtoothGeneratorParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The sawtooth related input parameters. */
    sawtoothParameters: SawtoothParameters = new SawtoothParameters();

    constructor(data?: SawtoothScalarGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): SawtoothScalarGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new SawtoothScalarGeneratorParameters();
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
            this.sawtoothParameters = data[sawtoothParametersName] ? SawtoothParameters.fromJS(data[sawtoothParametersName]) :
                new SawtoothParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[sawtoothParametersName] = this.sawtoothParameters ? this.sawtoothParameters.toJSON() : <any>undefined;
        return data;
    }
}
