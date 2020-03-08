// DELETE
// tslint:disable
/** Describes a candlestick chart layout. */
export class CandlestickChartLayout {

    /** Total chart width in pixels including margins. */
    public totalWidth: number;

    /** Total chart height in pixels including margins. */
    public totalHeight: number;

    /** The left margin of all chart panes in pixels. */
    public left: number;

    /** The width of all chart panes in pixels. */
    public width: number;

    /** A price pane layout. */
    public pricePane: CandlestickChartLayout.PaneLayout;

    /** An optional array of indicator pane layouts. */
    public indicatorPanes?: Array<CandlestickChartLayout.PaneLayout> | undefined;

    /** An optional navigation pane layout. */
    public navigationPane?: CandlestickChartLayout.PaneLayout | undefined;
}

export namespace CandlestickChartLayout {

    /** Describes a pane layout. */
    export class PaneLayout {
        /** Pane top in pixels. */
        public top: number;

        /** Pane height in pixels. */
        public height: number;
    }
}
// tslint:enable
