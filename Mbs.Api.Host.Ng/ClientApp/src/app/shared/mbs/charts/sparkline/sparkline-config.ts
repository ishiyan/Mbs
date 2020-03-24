import { Ohlcv } from '../../data/entities/ohlcv';
import { Scalar } from '../../data/entities/scalar';
import { Quote } from '../../data/entities/quote';
import { Trade } from '../../data/entities/trade';

/** Describes a sparkline configuration. */
export class SparklineConfig {
  /**
   * Total width of a sparkline including margins.
   * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
   */
  public width: string | number;

  /** An optional minimal width in pixels including margins. */
  public widthMin?: number | undefined = undefined;

  /** An optional maximal width in pixels including margins. */
  public widthMax?: number | undefined = undefined;

  /**
   * A height of the sparkline.
   * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
   */
  public height: string | number = 0;

  /** An optional minimal height in pixels. */
  public heightMin?: number | undefined = undefined;

  /** An optional maximal height in pixels. */
  public heightMax?: number | undefined = undefined;

  /** The left margine. */
  public marginLeft = 0;

  /** The top margin. */
  public marginTop = 0;

  /** The right margin. */
  public marginRight = 0;

  /** The bottom margin. */
  public marginBottom = 0;

  /** An optional fill color. */
  public fillColor: string | undefined = '#00000011';

  /** An optional color of the line stroke. */
  public strokeColor: string | undefined = 'black';

  /** An optional width of the line stroke in pixels. */
  public strokeWidth: number | undefined = 1;

  /** A line curve interpoltion method:
   * - linear
   * - natural
   * - basis
   * - camullRom
   * - cardinal
   * - step
   * - stepBefore
   * - stepAfter
   */
  public interpolation = 'natural';

  /** Data array. */
  public data: Ohlcv[] | Quote[] | Trade[] | Scalar[] = [];
}
