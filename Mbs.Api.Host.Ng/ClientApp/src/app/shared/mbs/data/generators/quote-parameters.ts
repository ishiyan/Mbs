/** The quote related input parameters for the waveform generators. A waveform generator produces quotes with the following traits.
 *
 *  ➊ The ask and bid prices are equidistant from the mid price.
 *  (a) The half-length of the spread is defined as a ratio ∈[0, 1].
 *  (b) When the ratio is 1, the bid price is zero and the ask price is twice the mid price.
 *  (c) When the ratio is 0, the both ask and bid prices are equal to the the mid price.
 *
 *  ➋ The ask and bid sizes are constant values. */
export class QuoteParameters {
    private static readonly defaultSpreadFraction: number = 0.3;
    private static readonly defaultAskSize: number = 100;
    private static readonly defaultBidSize: number = 100;

    /** The spread fraction, ρs, which determines the ask and bid prices as a fraction of the mid price. */
    spreadFraction: number = QuoteParameters.defaultSpreadFraction;

    /** The ask size, which is the same for all quotes; should be positive. */
    askSize: number = QuoteParameters.defaultAskSize;

    /** The bid size, which is the same for all quotes; should be positive. */
    bidSize: number = QuoteParameters.defaultBidSize;

    constructor(data?: QuoteParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): QuoteParameters {
        data = typeof data === 'object' ? data : {};
        const result = new QuoteParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.spreadFraction = data['spreadFraction'] !== undefined
                ? data['spreadFraction'] : QuoteParameters.defaultSpreadFraction;
            this.askSize = data['askSize'] !== undefined
                ? data['askSize'] : QuoteParameters.defaultAskSize;
            this.bidSize = data['bidSize'] !== undefined
                ? data['bidSize'] : QuoteParameters.defaultBidSize;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['spreadFraction'] = this.spreadFraction;
        data['askSize'] = this.askSize;
        data['bidSize'] = this.bidSize;
        return data;
    }
}
