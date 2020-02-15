import { Ohlcv } from './ohlcv';
import { Scalar } from './scalar';
import { Band } from './band';
import { OhlcvChartLayout } from './ohlcv-chart-layout';

/** Describes an ohlcv chart layout configuration. */
export class OhlcvChartConfig {

    /**
     * Total width of a chart including margins.
     * Defines the width of all chart panes.
     * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
    */
    public width: string | number;

    /**
     * An optional height of the navigation pane.
     * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
     * If undefined, the navigation pane will not be created.
     */
    public heightNavigationPane?: string | number | undefined = undefined;

    /**
     * An optional array of heights of indicator panes.
     * Each height can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
     * If undefined or empty, indicator panes will not be created.
     */
    public heightIndicatorPanes?: Array<(string | number)> | undefined = undefined;

    /** If left axis should be visible on the price and indicator panes. */
    public axisLeft: boolean = true;

    /** If right axis should be visible on the price and indicator panes. */
    public axisRight: boolean = false;

    /** If an axis behind the navigation pane should be visible. */
    public axisNav: boolean = false;

    /** The margins of the chart, exclusive space needed for annotation. */
    public margin: OhlcvChartConfig.Margin = new OhlcvChartConfig.Margin();

    public ohlcv: OhlcvChartConfig.OhlcvData = new OhlcvChartConfig.OhlcvData();

    /** The price pane. */
    public pricePane: OhlcvChartConfig.Pane = new OhlcvChartConfig.Pane();

    /** An optional array of indicator panes. */
    public indicatorPanes: OhlcvChartConfig.Pane[] = [];

    /** If crosshair should be  visible */
    public crosshair: boolean = false;

    /** If volume in price pane should be  visible */
    public volumeInPricePane: boolean = false;

    /** The width of a vertical axis in pixels including annotation. */
    public static readonly verticalAxisWidth: number = 30;

}

export namespace OhlcvChartConfig {

    /** The margins of the chart, exclusive space needed for annotation. */
    export class Margin {
        public left: number = 0;
        public top: number = 0;
        public right: number = 0;
        public bottom: number = 0;
    }    

    /** Describes a line in a pane. */
    export class LineData {
        /** A name of the data. */
        public name: string = "";

        /** Data array. */
        public data: Scalar[] = [];        

        /** An index of an indicator in the output data. */
        public indicator: number = 0;

        /** An index of an output within an indicator in the output data. */
        public output: number = 0;

        /** A color of the line. */
        public color: string = "black";

        /** A stroke of the line. */
        public stroke: string = "";
    }    

    /** Describes a band in a pane. */
    export class BandData {
        /** A name of the data. */
        public name: string = "";

        /** Data array. */
        public data: Band[] = [];        

        /** An index of an indicator in the output data. */
        public indicator: number = 0;

        /** An index of an output within an indicator in the output data. */
        public output: number = 0;

        /** A color of the band. */
        public color: string = "black";

        /** Strokes of the band lines. */
        public stroke: string = "";
    }    

    /** Describes an ohlcv pane data. */
    export class OhlcvData {
        /** A name of the data. */
        public name: string = "";

        /** Data array. */
        public data: Ohlcv[] = [];        
        
        /** If data is displayed as candlesticks or as bars */
        public candlesticks: boolean = true;
    }    

    /** Describes a pane. */
    export class Pane {
        /**
         * A height of the pane.
         * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
         */
        public height: string | number = 0;

        /**
         * A bottom extension of the pane.
         * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
         */
        public extension: string | number = 0;

        /** An array of indicator bands on this pane. */
        public bands: BandData[] = [];

        /** An array of indicator lines on this pane. */
        public lines: LineData[] = [];
  }    
}
