import { Ohlcv } from './entities/ohlcv';

/** _Ohlcv_ historical data. */
export class HistoricalDataOhlcv {
    /** If data is adjusted or _undefined_ if unknown. */
    readonly isDataAdjusted?: boolean | undefined;

    /** The name of the data. */
    readonly name: string;

    /** The monickert which uniquely identifies the data. */
    moniker: string;

    /** The data array. */
    readonly data: Ohlcv[];

    constructor(data?: HistoricalDataOhlcv) {
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
        data['isDataAdjusted'] = this.isDataAdjusted;
        if (this.data && this.data.constructor === Array) {
            data['data'] = [];
            for (const item of this.data) {
                data['data'].push(item.toJSON());
            }
        }
        return data;
    }
}
