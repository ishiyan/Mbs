import { FractionalBrownianMotionParameters } from './fractional-brownian-motion-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { TradeParameters } from '../trade-parameters';
import { SyntheticDataParameters } from '../synthetic-data-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, fbmParametersName, tradeParametersName,
    objectName } from '../constants';

/** The input parameters for the fractional Brownian motion trade generator. */
export class FractionalBrownianMotionTradeGeneratorParameters {
    /** The number of samples to generate. */
    sampleCount: number = SyntheticDataParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The fractional Brownian motion related input parameters. */
    fbmParameters: FractionalBrownianMotionParameters = new FractionalBrownianMotionParameters();

    /** The trade related input parameters. */
    tradeParameters: TradeParameters = new TradeParameters();

    constructor(data?: FractionalBrownianMotionTradeGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): FractionalBrownianMotionTradeGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new FractionalBrownianMotionTradeGeneratorParameters();
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
            this.tradeParameters = data[tradeParametersName] ? TradeParameters.fromJS(data[tradeParametersName]) :
                new TradeParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[fbmParametersName] = this.fbmParameters ? this.fbmParameters.toJSON() : <any>undefined;
        data[tradeParametersName] = this.tradeParameters ? this.tradeParameters.toJSON() : <any>undefined;
        return data;
    }
}
