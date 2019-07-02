import { ConvertableToJSON } from '../convertable-to-json';

/** An [open, high, low, close, volume] bar. */
export class Ohlcv implements ConvertableToJSON {
  /** The date and time.
   *
   * For _ohlcv_ bar entities it corresponds to the closing time, so that an _ohlcv_ bar accumulates lower-level entities
   * up to the closing date and time. */
  time: Date;

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

  constructor(data?: Ohlcv) {
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
    data['open'] = this.open;
    data['high'] = this.high;
    data['low'] = this.low;
    data['close'] = this.close;
    data['volume'] = this.volume;
    return data;
  }
}
