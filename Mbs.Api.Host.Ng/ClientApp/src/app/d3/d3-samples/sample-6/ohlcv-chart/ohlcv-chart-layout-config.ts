import { OhlcvChartLayout } from './ohlcv-chart-layout';

/** Describes an ohlcv chart layout configuration. */
export class OhlcvChartLayoutConfigZZZ {

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

    public axisLeft: boolean = true;

    public axisRight: boolean = false;

    /** The left margin of the chart. */
    public marginLeft: number = 0;

    /** The top margin of the chart. */
    public marginTop: number = 0;

    /** The right margin of the chart. */
    public marginRight: number = 0;

    /** The bottom margin of the chart. */
    public marginBottom: number = 0;

    /** Calculates chart layout from the configuration. */
    public static configToLayout(config: OhlcvChartLayoutConfig, referenceWidth: number): OhlcvChartLayout {
        const marginLeft = config.marginLeft + 30;
        const marginRight = config.marginRight + 30;
        const marginTop = config.marginTop + 10;
        const marginBottom = config.marginBottom + 10;
        const layout = new OhlcvChartLayout();
        layout.axisLeft = config.axisLeft;
        layout.axisRight = config.axisRight;
        layout.left = marginLeft;
        layout.totalWidth = OhlcvChartLayoutConfig.valueToPixels(config.width, referenceWidth);
        const width: number = layout.totalWidth - marginLeft - marginRight;
        layout.width = width;

        let height: number = OhlcvChartLayoutConfig.valueToPixels(config.heightPricePane, width);
        let top: number = marginTop;
        layout.pricePane = new OhlcvChartLayout.PaneLayout();
        layout.pricePane.top = top;
        layout.pricePane.height = height;
        top += height;

        if (config.heightIndicatorPanes && config.heightIndicatorPanes.length > 0) {
            const length = config.heightIndicatorPanes.length;
            layout.indicatorPanes = new Array<OhlcvChartLayout.PaneLayout>(length);
            for (let i = 0; i < length; ++i) {
                height = OhlcvChartLayoutConfig.valueToPixels(config.heightIndicatorPanes[i], width);
                const pane = new OhlcvChartLayout.PaneLayout();
                pane.top = top;
                pane.height = height;
                layout.indicatorPanes[i] = pane;
                top += height;
            }
        }

        if (config.heightNavigationPane) {
            layout.navigationPane = new OhlcvChartLayout.PaneLayout();
            layout.navigationPane.top = top;
            height = OhlcvChartLayoutConfig.valueToPixels(config.heightNavigationPane, width);
            layout.navigationPane.height = height;
            top += height;
        }

        layout.totalHeight = top + marginBottom;
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
