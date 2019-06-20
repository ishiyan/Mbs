import { GeometricBrownianMotionParameters } from './geometric-brownian-motion-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { OhlcvParameters } from '../ohlcv-parameters';
import { QuoteParameters } from '../quote-parameters';
import { TradeParameters } from '../trade-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, gbmParametersName, ohlcvParametersName,
    quoteParametersName, tradeParametersName, objectName } from '../constants';

/** The input parameters for the geometric Brownian motion generator. */
export class GeometricBrownianMotionGeneratorParameters {
    static readonly defaultSampleCount: number = 128;

    /** The number of samples to generate. */
    sampleCount: number = GeometricBrownianMotionGeneratorParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The geometric Brownian motion related input parameters. */
    gbmParameters: GeometricBrownianMotionParameters = new GeometricBrownianMotionParameters();

    /** The ohlcv related input parameters. */
    ohlcvParameters: OhlcvParameters = new OhlcvParameters();

    /** The quote related input parameters. */
    quoteParameters: QuoteParameters = new QuoteParameters();

    /** The trade related input parameters. */
    tradeParameters: TradeParameters = new TradeParameters();

    constructor(data?: GeometricBrownianMotionGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): GeometricBrownianMotionGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new GeometricBrownianMotionGeneratorParameters();
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
            this.gbmParameters = data[gbmParametersName] ? GeometricBrownianMotionParameters.fromJS(data[gbmParametersName]) :
                new GeometricBrownianMotionParameters();
            this.ohlcvParameters = data[ohlcvParametersName] ? OhlcvParameters.fromJS(data[ohlcvParametersName]) :
                new OhlcvParameters();
            this.quoteParameters = data[quoteParametersName] ? QuoteParameters.fromJS(data[quoteParametersName]) :
                new QuoteParameters();
            this.tradeParameters = data[tradeParametersName] ? TradeParameters.fromJS(data[tradeParametersName]) :
                new TradeParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[gbmParametersName] = this.gbmParameters ? this.gbmParameters.toJSON() : <any>undefined;
        data[ohlcvParametersName] = this.ohlcvParameters ? this.ohlcvParameters.toJSON() : <any>undefined;
        data[quoteParametersName] = this.quoteParameters ? this.quoteParameters.toJSON() : <any>undefined;
        data[tradeParametersName] = this.tradeParameters ? this.tradeParameters.toJSON() : <any>undefined;
        return data;
    }
}
