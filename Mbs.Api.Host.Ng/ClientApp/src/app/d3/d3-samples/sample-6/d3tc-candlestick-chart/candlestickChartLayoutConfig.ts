import { CandlestickChartLayout } from './candlestickChartLayout';

/** Describes a candlestick chart layout configuration. */
export class CandlestickChartLayoutConfig {

    /**
     * Total width of a chart including margins.
     * Defines the width of all chart panes.
     * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
    */
    public width: string | number;

    /**
     * The height of the price pane.
     * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
     */
    public heightPricePane: string | number;

    /**
     * An optional height of the navigation pane.
     * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
     * If undefined, the navigation pane will not be created.
     */
    public heightNavigationPane?: string | number | undefined;

    /**
     * An optional array of heights of indicator panes.
     * Each height can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
     * If undefined or empty, indicator panes will not be created.
     */
    public heightIndicatorPanes?: Array<(string | number)> | undefined = undefined;

    /** The left margin of the chart. */
    public marginLeft: number = 0;

    /** The top margin of the chart. */
    public marginTop: number = 0;

    /** The right margin of the chart. */
    public marginRight: number = 0;

    /** The bottom margin of the chart. */
    public marginBottom: number = 0;

    /** Calculates chart layout from the configuration. */
    public static configToLayout(config: CandlestickChartLayoutConfig, referenceWidth: number): CandlestickChartLayout {
        const layout = new CandlestickChartLayout();
        layout.left = config.marginLeft;
        layout.totalWidth = CandlestickChartLayoutConfig.valueToPixels(config.width, referenceWidth);
        const width: number = layout.totalWidth - config.marginLeft - config.marginRight;
        layout.width = width;

        let height: number = CandlestickChartLayoutConfig.valueToPixels(config.heightPricePane, width);
        let top: number = config.marginTop;
        layout.pricePane = new CandlestickChartLayout.PaneLayout();
        layout.pricePane.top = top;
        layout.pricePane.height = height;
        top += height;

        if (config.heightIndicatorPanes && config.heightIndicatorPanes.length > 0) {
            const length = config.heightIndicatorPanes.length;
            layout.indicatorPanes = new Array<CandlestickChartLayout.PaneLayout>(length);
            for (let i = 0; i < length; ++i) {
                height = CandlestickChartLayoutConfig.valueToPixels(config.heightIndicatorPanes[i], width);
                const pane = new CandlestickChartLayout.PaneLayout();
                pane.top = top;
                pane.height = height;
                layout.indicatorPanes[i] = pane;
                top += height;
            }
        }

        if (config.heightNavigationPane) {
            layout.navigationPane = new CandlestickChartLayout.PaneLayout();
            layout.navigationPane.top = top;
            height = CandlestickChartLayoutConfig.valueToPixels(config.heightNavigationPane, width);
            layout.navigationPane.height = height;
            top += height;
        }

        layout.totalHeight = top + config.marginBottom;
        return layout;
    }
    
    private static valueToPixels(value: number | string, reference: number) : number {
        if (typeof value == 'number') {
            return +value;
        }

        const numeric = value.match(/\d+/);
        if (value.endsWith('%')) {
            return +numeric / 100 * reference;
        }

        return +numeric;
    } 
}
