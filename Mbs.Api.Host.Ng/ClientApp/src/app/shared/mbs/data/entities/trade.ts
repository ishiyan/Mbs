/** A _trade_ (price and volume) entity. */
export class Trade {
    /** The price. */
    price: number;

    /** The volume (quantity). */
    volume: number;

    /** The date and time. */
    time: Date;

    constructor(data?: Trade) {
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
        data['price'] = this.price;
        data['volume'] = this.volume;
        data['time'] = this.time ? this.time.toISOString() : <any>undefined;
        return data;
    }
}
