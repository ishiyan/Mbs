import { SawtoothParameters } from './sawtooth-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { OhlcvParameters } from '../ohlcv-parameters';
import { SawtoothGeneratorParameters } from './sawtooth-generator-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, sawtoothParametersName, ohlcvParametersName,
    objectName } from '../constants';

/** The input parameters for the sawtooth ohlcv generator. */
export class SawtoothOhlcvGeneratorParameters {
    /** The number of samples to generate. */
    sampleCount: number = SawtoothGeneratorParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The sawtooth related input parameters. */
    sawtoothParameters: SawtoothParameters = new SawtoothParameters();

    /** The ohlcv related input parameters. */
    ohlcvParameters: OhlcvParameters = new OhlcvParameters();

    constructor(data?: SawtoothOhlcvGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): SawtoothOhlcvGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new SawtoothOhlcvGeneratorParameters();
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
            this.ohlcvParameters = data[ohlcvParametersName] ? OhlcvParameters.fromJS(data[ohlcvParametersName]) :
                new OhlcvParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[sawtoothParametersName] = this.sawtoothParameters ? this.sawtoothParameters.toJSON() : <any>undefined;
        data[ohlcvParametersName] = this.ohlcvParameters ? this.ohlcvParameters.toJSON() : <any>undefined;
        return data;
    }
}
