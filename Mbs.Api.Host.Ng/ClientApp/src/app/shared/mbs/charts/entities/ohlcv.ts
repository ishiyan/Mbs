/** An [open, high, low, close, volume] bar. */
export class Ohlcv {
  /** The date and time.
   *
   * For _ohlcv_ bar entities it corresponds to the closing time, so that an _ohlcv_ bar accumulates lower-level entities
   * up to the closing date and time.
   */
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
}

export namespace Ohlcv {
  export function construct(json?: any): Ohlcv {
    const ohlcv = new Ohlcv();
    if (json) {
      for (const property in json) {
        if (json.hasOwnProperty(property)) {
          (ohlcv as any)[property] = (json as any)[property];
        }
      }
    }
    return ohlcv;
  }

  export function toJSON(ohlcv: Ohlcv, json?: any): any {
    json = typeof json === 'object' ? json : {};
    // tslint:disable:no-string-literal
    json['time'] = ohlcv.time ? ohlcv.time.toISOString() : (undefined as any);
    json['open'] = ohlcv.open;
    json['high'] = ohlcv.high;
    json['low'] = ohlcv.low;
    json['close'] = ohlcv.close;
    json['volume'] = ohlcv.volume;
    // tslint:enable:no-string-literal
    return json;
  }
}
