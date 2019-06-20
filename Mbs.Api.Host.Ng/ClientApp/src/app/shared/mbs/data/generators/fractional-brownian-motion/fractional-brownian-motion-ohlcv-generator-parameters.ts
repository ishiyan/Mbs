import { FractionalBrownianMotionParameters } from './fractional-brownian-motion-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { OhlcvParameters } from '../ohlcv-parameters';
import { FractionalBrownianMotionGeneratorParameters } from './fractional-brownian-motion-generator-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, fbmParametersName, ohlcvParametersName,
    objectName } from '../constants';

/** The input parameters for the fractional Brownian motion ohlcv generator. */
export class FractionalBrownianMotionOhlcvGeneratorParameters {
    /** The number of samples to generate. */
    sampleCount: number = FractionalBrownianMotionGeneratorParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The fractional Brownian motion related input parameters. */
    fbmParameters: FractionalBrownianMotionParameters = new FractionalBrownianMotionParameters();

    /** The ohlcv related input parameters. */
    ohlcvParameters: OhlcvParameters = new OhlcvParameters();

    constructor(data?: FractionalBrownianMotionOhlcvGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): FractionalBrownianMotionOhlcvGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new FractionalBrownianMotionOhlcvGeneratorParameters();
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
            this.fbmParameters = data[fbmParametersName] ? FractionalBrownianMotionParameters.fromJS(data[fbmParametersName]) :
                new FractionalBrownianMotionParameters();
            this.ohlcvParameters = data[ohlcvParametersName] ? OhlcvParameters.fromJS(data[ohlcvParametersName]) :
                new OhlcvParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[fbmParametersName] = this.fbmParameters ? this.fbmParameters.toJSON() : <any>undefined;
        data[ohlcvParametersName] = this.ohlcvParameters ? this.ohlcvParameters.toJSON() : <any>undefined;
        return data;
    }
}
