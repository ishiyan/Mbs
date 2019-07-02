import { ConvertableToJSON } from '../convertable-to-json';

/** A price _quote_ (bid/ask price and size pair). */
export class Quote implements ConvertableToJSON {
  /** The date and time. */
  time: Date;

  /** The bid price. */
  bidPrice: number;

  /** The bid size. */
  bidSize: number;

  /** The ask price. */
  askPrice: number;

  /** The ask size. */
  askSize: number;

  constructor(data?: Quote) {
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
    data['bidPrice'] = this.bidPrice;
    data['bidSize'] = this.bidSize;
    data['askPrice'] = this.askPrice;
    data['askSize'] = this.askSize;
    return data;
  }
}
