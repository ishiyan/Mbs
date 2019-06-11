/** The trade related input parameters for the waveform generators. A waveform generator produces trades with a constant volume. */
export class TradeParameters {
    private static readonly defaultVolume: number = 100;

    /** The volume, which is the same for all trades; should be positive. */
    volume: number = TradeParameters.defaultVolume;

    constructor(data?: TradeParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): TradeParameters {
        data = typeof data === 'object' ? data : {};
        const result = new TradeParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.volume = data['volume'] !== undefined
                ? data['volume'] : TradeParameters.defaultVolume;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['volume'] = this.volume;
        return data;
    }
}
