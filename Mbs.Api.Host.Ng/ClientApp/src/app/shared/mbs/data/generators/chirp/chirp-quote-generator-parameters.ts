import { ChirpParameters } from './chirp-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { QuoteParameters } from '../quote-parameters';
import { ChirpGeneratorParameters } from './chirp-generator-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, chirpParametersName, quoteParametersName,
    objectName } from '../constants';

/** The input parameters for the chirp quote generator. */
export class ChirpQuoteGeneratorParameters {
    /** The number of samples to generate. */
    sampleCount: number = ChirpGeneratorParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The chirp related input parameters. */
    chirpParameters: ChirpParameters = new ChirpParameters();

    /** The quote related input parameters. */
    quoteParameters: QuoteParameters = new QuoteParameters();

    constructor(data?: ChirpQuoteGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): ChirpQuoteGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new ChirpQuoteGeneratorParameters();
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
            this.chirpParameters = data[chirpParametersName] ? ChirpParameters.fromJS(data[chirpParametersName]) :
                new ChirpParameters();
            this.quoteParameters = data[quoteParametersName] ? QuoteParameters.fromJS(data[quoteParametersName]) :
                new QuoteParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[chirpParametersName] = this.chirpParameters ? this.chirpParameters.toJSON() : <any>undefined;
        data[quoteParametersName] = this.quoteParameters ? this.quoteParameters.toJSON() : <any>undefined;
        return data;
    }
}
