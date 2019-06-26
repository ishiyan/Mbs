/** An [open, high, low, close, volume] bar. */
export class Ohlcv {
    /** The opening price. */
    open: number;

    /** The highest price. */
    high: number;

    /** The lowest price. */
    low: number;

    /** The closing price. */
    close: number;

    /** The volume. */
    volume: number;

    /** The date and time.
     *
     * For _ohlcv_ bar entities it corresponds to the closing time, so that an _ohlcv_ bar accumulates lower-level entities
     * up to the closing date and time. */
    time: Date;

    constructor(data?: Ohlcv) {
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
        data['open'] = this.open;
        data['high'] = this.high;
        data['low'] = this.low;
        data['close'] = this.close;
        data['volume'] = this.volume;
        data['time'] = this.time ? this.time.toISOString() : <any>undefined;
        return data;
    }
}
