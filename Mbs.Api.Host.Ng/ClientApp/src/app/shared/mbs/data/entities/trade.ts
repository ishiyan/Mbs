import { ConvertableToJSON } from '../convertable-to-json';

/** A _trade_ (price and volume) entity. */
export class Trade implements ConvertableToJSON {
  /** The date and time. */
  time: Date;

  /** The price. */
  price: number;

  /** The volume (quantity). */
  volume: number;

  constructor(data?: Trade) {
    if (data) {
      for (const property in data) {
        if (data.hasOwnProperty(property)) {
          (<any>this)[property] = (<any>data)[property];
        }
      }
    }
  }

  toJSON(data?: any): any {
    data = typeof data === 'object' ? data : {};
    data['time'] = this.time ? this.time.toISOString() : <any>undefined;
    data['price'] = this.price;
    data['volume'] = this.volume;
    return data;
  }
}
