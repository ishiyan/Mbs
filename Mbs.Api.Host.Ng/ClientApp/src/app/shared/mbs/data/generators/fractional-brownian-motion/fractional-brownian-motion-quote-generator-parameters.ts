import { FractionalBrownianMotionParameters } from './fractional-brownian-motion-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { QuoteParameters } from '../quote-parameters';
import { FractionalBrownianMotionGeneratorParameters } from './fractional-brownian-motion-generator-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, fbmParametersName, quoteParametersName,
    objectName } from '../constants';

/** The input parameters for the fractional Brownian motion quote generator. */
export class FractionalBrownianMotionQuoteGeneratorParameters {
    /** The number of samples to generate. */
    sampleCount: number = FractionalBrownianMotionGeneratorParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The fractional Brownian motion related input parameters. */
    fbmParameters: FractionalBrownianMotionParameters = new FractionalBrownianMotionParameters();

    /** The quote related input parameters. */
    quoteParameters: QuoteParameters = new QuoteParameters();

    constructor(data?: FractionalBrownianMotionQuoteGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): FractionalBrownianMotionQuoteGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new FractionalBrownianMotionQuoteGeneratorParameters();
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
            this.quoteParameters = data[quoteParametersName] ? QuoteParameters.fromJS(data[quoteParametersName]) :
                new QuoteParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[fbmParametersName] = this.fbmParameters ? this.fbmParameters.toJSON() : <any>undefined;
        data[quoteParametersName] = this.quoteParameters ? this.quoteParameters.toJSON() : <any>undefined;
        return data;
    }
}
