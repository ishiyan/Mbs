/** An [open, high, low, close, volume] bar. */
export class Ohlcv {
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
}
