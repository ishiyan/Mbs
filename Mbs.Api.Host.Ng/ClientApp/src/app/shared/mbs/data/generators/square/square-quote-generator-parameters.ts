import { SquareParameters } from './square-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { QuoteParameters } from '../quote-parameters';
import { SyntheticDataParameters } from '../synthetic-data-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, squareParametersName, quoteParametersName,
    objectName } from '../constants';

/** The input parameters for the square quote generator. */
export class SquareQuoteGeneratorParameters {
    static readonly defaultSampleCount: number = 128;

    /** The number of samples to generate. */
    sampleCount: number = SyntheticDataParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The square related input parameters. */
    squareParameters: SquareParameters = new SquareParameters();

    /** The quote related input parameters. */
    quoteParameters: QuoteParameters = new QuoteParameters();

    constructor(data?: SquareQuoteGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): SquareQuoteGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new SquareQuoteGeneratorParameters();
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
            this.squareParameters = data[squareParametersName] ? SquareParameters.fromJS(data[squareParametersName]) :
                new SquareParameters();
            this.quoteParameters = data[quoteParametersName] ? QuoteParameters.fromJS(data[quoteParametersName]) :
                new QuoteParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[squareParametersName] = this.squareParameters ? this.squareParameters.toJSON() : <any>undefined;
        data[quoteParametersName] = this.quoteParameters ? this.quoteParameters.toJSON() : <any>undefined;
        return data;
    }
}
