/** The input parameters for the waveform generators. A waveform generator produces samples with an optional noise. */
export class OhlcvParameters {

    private static readonly defaultCandlestickShadowFraction: number = 0.3;
    private static readonly defaultCandlestickBodyFraction: number = 0.2;
    private static readonly defaultVolume: number = 100;

    constructor(data?: OhlcvParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    /** The shadow fraction, ρs, which determines the length of the candlestick shadows as a fraction of the mid price; ρs∈[0, 1].
     *
     *  The value should be greater or equal to the candlestick body fraction. */
    candlestickShadowFraction: number = OhlcvParameters.defaultCandlestickShadowFraction;

    /** The body fraction, ρb, which determines the half-length of the candlestick body as a fraction of the mid price; ρb∈[0, 1].
     *
     *  The value should be less or equal to the candlestick shadow fraction. */
    candlestickBodyFraction: number = OhlcvParameters.defaultCandlestickBodyFraction;

    /** The volume, which is the same for all candlesticks; should be positive. */
    volume: number = OhlcvParameters.defaultVolume;

    static fromJS(data: any): OhlcvParameters {
        data = typeof data === 'object' ? data : {};
        const result = new OhlcvParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.candlestickShadowFraction = data['candlestickShadowFraction'] !== undefined
                ? data['candlestickShadowFraction'] : OhlcvParameters.defaultCandlestickShadowFraction;
            this.candlestickBodyFraction = data['candlestickBodyFraction'] !== undefined
                ? data['candlestickBodyFraction'] : OhlcvParameters.defaultCandlestickBodyFraction;
            this.volume = data['volume'] !== undefined
                ? data['volume'] : OhlcvParameters.defaultVolume;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['candlestickShadowFraction'] = this.candlestickShadowFraction;
        data['candlestickBodyFraction'] = this.candlestickBodyFraction;
        data['volume'] = this.volume;
        return data;
    }
}
