/** A price _quote_ (bid/ask price and size pair). */
export class Quote {
    /** The bid price. */
    bidPrice: number;

    /** The bid size. */
    bidSize: number;

    /** The ask price. */
    askPrice: number;

    /** The ask size. */
    askSize!: number;

    /** The date and time. */
    time!: Date;

    constructor(data?: Quote) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['bidPrice'] = this.bidPrice;
        data['bidSize'] = this.bidSize;
        data['askPrice'] = this.askPrice;
        data['askSize'] = this.askSize;
        data['time'] = this.time ? this.time.toISOString() : <any>undefined;
        return data;
    }
}
