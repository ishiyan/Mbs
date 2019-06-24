import { SawtoothParameters } from './sawtooth-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { OhlcvParameters } from '../ohlcv-parameters';
import { QuoteParameters } from '../quote-parameters';
import { TradeParameters } from '../trade-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, sawtoothParametersName, ohlcvParametersName, quoteParametersName,
    tradeParametersName, objectName } from '../constants';

/** The input parameters for the sawtooth generator. */
export class SawtoothGeneratorParameters {
    static readonly defaultSampleCount: number = 128;

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

    /** The quote related input parameters. */
    quoteParameters: QuoteParameters = new QuoteParameters();

    /** The trade related input parameters. */
    tradeParameters: TradeParameters = new TradeParameters();

    constructor(data?: SawtoothGeneratorParameters) {
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
        data[sawtoothParametersName] = this.sawtoothParameters ? this.sawtoothParameters.toJSON() : <any>undefined;
        data[ohlcvParametersName] = this.ohlcvParameters ? this.ohlcvParameters.toJSON() : <any>undefined;
        data[quoteParametersName] = this.quoteParameters ? this.quoteParameters.toJSON() : <any>undefined;
        data[tradeParametersName] = this.tradeParameters ? this.tradeParameters.toJSON() : <any>undefined;
        return data;
    }
}
