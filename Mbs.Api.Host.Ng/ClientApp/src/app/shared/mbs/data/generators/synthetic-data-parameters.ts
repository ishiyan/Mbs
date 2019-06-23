import { ChirpParameters } from './chirp/chirp-parameters';
import { FractionalBrownianMotionParameters } from './fractional-brownian-motion/fractional-brownian-motion-parameters';
import { GeometricBrownianMotionParameters } from './geometric-brownian-motion/geometric-brownian-motion-parameters';
import { SawtoothParameters } from './sawtooth/sawtooth-parameters';
import { SinusoidalParameters } from './sinusoidal/sinusoidal-parameters';
import { SquareParameters } from './square/square-parameters';
import { TimeParameters } from './time-parameters';
import { WaveformParameters } from './waveform-parameters';
import { OhlcvParameters } from './ohlcv-parameters';
import { QuoteParameters } from './quote-parameters';
import { TradeParameters } from './trade-parameters';
import { TemporalEntityKind } from '../entities/temporal-entity-kind.enum';
import { SyntheticDataKind } from './synthetic-data-kind.enum';
import { sampleCountName, timeParametersName, waveformParametersName, chirpParametersName, ohlcvParametersName, quoteParametersName,
    tradeParametersName, objectName, fbmParametersName, gbmParametersName, sawtoothParametersName, sinusoidalParametersName,
    squareParametersName, temporalEntityKindName, syntheticDataKindName} from './constants';

/** The input parameters for the synthetic data generation. */
export class SyntheticDataParameters {
    static readonly defaultSampleCount: number = 1024;
    static readonly defaultTemporalEntityKind: TemporalEntityKind = TemporalEntityKind.Ohlcv;
    static readonly defaultSyntheticDataKind: SyntheticDataKind = SyntheticDataKind.FractionalBrownianMotion;

    /** The selected temporal entity. */
    temporalEntityKind: TemporalEntityKind = SyntheticDataParameters.defaultTemporalEntityKind;

    /** The selected synthetic data kind. */
    syntheticDataKind: SyntheticDataKind = SyntheticDataParameters.defaultSyntheticDataKind;

    /** The number of samples to generate. */
    sampleCount: number = SyntheticDataParameters.defaultSampleCount;

    /** The chirp related input parameters. */
    chirpParameters: ChirpParameters = new ChirpParameters();

    /** The fractional Brownian motion related input parameters. */
    fbmParameters: FractionalBrownianMotionParameters = new FractionalBrownianMotionParameters();

    /** The geometric Brownian motion related input parameters. */
    gbmParameters: GeometricBrownianMotionParameters = new GeometricBrownianMotionParameters();

    /** The sawtooth related input parameters. */
    sawtoothParameters: SawtoothParameters = new SawtoothParameters();

    /** The sinusoid related input parameters. */
    sinusoidalParameters: SinusoidalParameters = new SinusoidalParameters();

    /** The square related input parameters. */
    squareParameters: SquareParameters = new SquareParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The ohlcv related input parameters. */
    ohlcvParameters: OhlcvParameters = new OhlcvParameters();

    /** The quote related input parameters. */
    quoteParameters: QuoteParameters = new QuoteParameters();

    /** The trade related input parameters. */
    tradeParameters: TradeParameters = new TradeParameters();

    constructor(data?: SyntheticDataParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): SyntheticDataParameters {
        data = typeof data === objectName ? data : {};
        const result = new SyntheticDataParameters();
        result.init(data);
        return result;
    }

    private init(data?: any): void {
        if (data) {
            this.sampleCount = data[sampleCountName];
            this.temporalEntityKind = data[temporalEntityKindName];
            this.syntheticDataKind = data[syntheticDataKindName];
            this.chirpParameters = data[chirpParametersName] ? ChirpParameters.fromJS(data[chirpParametersName]) :
                new ChirpParameters();
            this.fbmParameters = data[fbmParametersName] ? FractionalBrownianMotionParameters.fromJS(data[fbmParametersName]) :
                new FractionalBrownianMotionParameters();
            this.gbmParameters = data[gbmParametersName] ? GeometricBrownianMotionParameters.fromJS(data[gbmParametersName]) :
                new GeometricBrownianMotionParameters();
            this.sawtoothParameters = data[sawtoothParametersName] ? SawtoothParameters.fromJS(data[sawtoothParametersName]) :
                new SawtoothParameters();
            this.sinusoidalParameters = data[sinusoidalParametersName] ? SinusoidalParameters.fromJS(data[sinusoidalParametersName]) :
                new SinusoidalParameters();
            this.squareParameters = data[squareParametersName] ? SquareParameters.fromJS(data[squareParametersName]) :
                new SquareParameters();
            this.waveformParameters = data[waveformParametersName] ? WaveformParameters.fromJS(data[waveformParametersName]) :
                new WaveformParameters();
            this.timeParameters = data[timeParametersName] ? TimeParameters.fromJS(data[timeParametersName]) :
                new TimeParameters();
            this.ohlcvParameters = data[ohlcvParametersName] ? OhlcvParameters.fromJS(data[ohlcvParametersName]) :
                new OhlcvParameters();
            this.quoteParameters = data[quoteParametersName] ? QuoteParameters.fromJS(data[quoteParametersName]) :
                new QuoteParameters();
            this.tradeParameters = data[tradeParametersName] ? TradeParameters.fromJS(data[tradeParametersName]) :
                new TradeParameters();
        }
    }

    toJSON(data?: any): any {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[temporalEntityKindName] = this.temporalEntityKind;
        data[syntheticDataKindName] = this.syntheticDataKind;
        switch (this.syntheticDataKind) {
            case SyntheticDataKind.Chirp: {
                data[chirpParametersName] = this.chirpParameters ? this.chirpParameters.toJSON() : new ChirpParameters();
                break;
            }
            case SyntheticDataKind.FractionalBrownianMotion: {
                data[fbmParametersName] = this.fbmParameters ? this.fbmParameters.toJSON() : new FractionalBrownianMotionParameters();
                break;
            }
            case SyntheticDataKind.GeometricBrownianMotion: {
                data[gbmParametersName] = this.gbmParameters ? this.gbmParameters.toJSON() : new GeometricBrownianMotionParameters();
                break;
            }
            case SyntheticDataKind.Sawtooth: {
                data[sawtoothParametersName] = this.sawtoothParameters ? this.sawtoothParameters.toJSON() : new SawtoothParameters();
                break;
            }
            case SyntheticDataKind.Sinusoid: {
                data[sinusoidalParametersName] = this.sinusoidalParameters ? this.sinusoidalParameters.toJSON() :
                    new SinusoidalParameters();
                break;
            }
            case SyntheticDataKind.Square: {
                data[squareParametersName] = this.squareParameters ? this.squareParameters.toJSON() : new SquareParameters();
                break;
            }
        }
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : new WaveformParameters();
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : new TimeParameters();
        switch (this.temporalEntityKind) {
            case TemporalEntityKind.Ohlcv: {
                data[ohlcvParametersName] = this.ohlcvParameters ? this.ohlcvParameters.toJSON() : new OhlcvParameters();
                break;
            }
            case TemporalEntityKind.Quote: {
                data[quoteParametersName] = this.quoteParameters ? this.quoteParameters.toJSON() : new QuoteParameters();
                break;
            }
            case TemporalEntityKind.Trade: {
                data[tradeParametersName] = this.tradeParameters ? this.tradeParameters.toJSON() : new TradeParameters();
                break;
            }
        }
        return data;
    }
}
