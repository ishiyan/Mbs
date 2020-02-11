import { Ohlcv } from './ohlcv';
import { Scalar } from './scalar';

/** Describes an ohlcv chart layout. */
export class OhlcvChartLayout {

    /** Total chart width in pixels including margins. */
    public totalWidth: number;

    /** Total chart height in pixels including margins. */
    public totalHeight: number;

    /** The left margin of all chart panes in pixels. */
    public left: number;

    /** The width of all chart panes in pixels. */
    public width: number;

    /** A price pane layout. */
    public pricePane: OhlcvChartLayout.PaneLayout;

    /** An optional array of indicator pane layouts. */
    public indicatorPanes?: Array<OhlcvChartLayout.PaneLayout> | undefined;

    /** An optional navigation pane layout. */
    public navigationPane?: OhlcvChartLayout.PaneLayout | undefined;

    public axisLeft: boolean = true;

    public axisRight: boolean = false;
}

export namespace OhlcvChartLayout {
    
    /** Describes a pane layout. */
    export class PaneLayout {
        /** Pane top in pixels. */
        public top: number;

        /** Pane height in pixels. */
        public height: number;

        
    }    
}
