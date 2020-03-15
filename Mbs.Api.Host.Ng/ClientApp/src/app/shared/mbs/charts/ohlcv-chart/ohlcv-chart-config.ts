import { Ohlcv } from '../../data/entities/ohlcv';
import { Scalar } from '../../data/entities/scalar';
import { Band } from '../entities/band';
import { Heatmap } from '../entities/heatmap';

/** Describes an ohlcv chart layout configuration. */
export class OhlcvChartConfig {
    /**
     * Total width of a chart including margins.
     * Defines the width of all chart panes.
     * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
    */
    public width: string | number;

    /** An optional minimal width in pixels including margins. */
    public widthMin?: number | undefined = undefined;

    /** An optional maximal width in pixels including margins. */
    public widthMax?: number | undefined = undefined;

    /** An optional navigation pane. */
    public navigationPane?: OhlcvChartConfig.NavigationPane | undefined = new OhlcvChartConfig.NavigationPane();

    /**
     * An optional height of the navigation pane.
     * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
     * If undefined, the navigation pane will not be created.
     */
    public heightNavigationPane?: string | number | undefined = undefined;

    /** An optional d3.timeFormat specifier for time axis annotations, e.g. '%Y-%m-%d'. */
    public timeAnnotationFormat?: string | undefined = undefined;

    /** An optional d3.timeFormat specifier for time axis ticks, e.g. '%Y-%m-%d'. */
    public timeTicksFormat?: string | undefined = undefined;

    /** An optional number of ticks in the time axis. */
    public timeTicks?: number | undefined = undefined;

    /** If left axis should be visible on the price and indicator panes. */
    public axisLeft = true;

    /** If right axis should be visible on the price and indicator panes. */
    public axisRight = false;

    /** The margins of the chart, exclusive space needed for annotation. */
    public margin: OhlcvChartConfig.Margin = new OhlcvChartConfig.Margin();

    public ohlcv: OhlcvChartConfig.OhlcvData = new OhlcvChartConfig.OhlcvData();

    /** The price pane. */
    public pricePane: OhlcvChartConfig.Pane = new OhlcvChartConfig.Pane();

    /** An optional array of indicator panes. */
    public indicatorPanes: OhlcvChartConfig.Pane[] = [];

    /** If *crosshair* should be visible */
    public crosshair = false;

    /** If volume in price pane should be visible */
    public volumeInPricePane = false;

    /** If menu should be visible. */
    public menuVisible = true;

    /** If *download SVG* menu setting should be visible. */
    public downloadSvgVisible = true;
}

export namespace OhlcvChartConfig {

    /** The margins of the chart, exclusive space needed for annotation. */
    export class Margin {
        public left = 0;
        public top = 0;
        public right = 0;
        public bottom = 0;
    }

    /** Describes a horizontal line in a pane. */
    export class HorizontalData {
        /** Data value. */
        public value = 0;

        /** A color of the line stroke. */
        public color = 'black';

        /** A width of the line stroke in pixels. */
        public width = 1;

        /** A dash array of the line stroke, e.g. '5,5' or '20,10,5,5,5,10' or empty if no dashes. */
        public dash = '';
    }

    /** Describes a line in a pane. */
    export class LineData {
        /** A name of the data. */
        public name = '';

        /** Data array. */
        public data: Scalar[] = [];

        /** An index of an indicator in the output data. */
        public indicator = 0;

        /** An index of an output within an indicator in the output data. */
        public output = 0;

        /** A color of the line stroke. */
        public color = 'black';

        /** A width of the line stroke in pixels. */
        public width = 1;

        /** A dash array of the line stroke, e.g. '5,5' or '20,10,5,5,5,10' or empty if no dashes. */
        public dash = '';

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
    }

    /** Describes an area in a pane between a line and a constant value. */
    export class LineAreaData {
        /** A name of the data. */
        public name = '';

        /** Data array. */
        public data: Scalar[] = [];

        /** An index of an indicator in the output data. */
        public indicator = 0;

        /** An index of an output within an indicator in the output data. */
        public output = 0;

        /** A constant limiting value. */
        public value = 0;

        /** A fill color of an area. */
        public color = '#00000011';

        /**
         *  The fill color of an area may be very transprent and its visibility on a legend may be poor.
         *  This allows to specify a legend color different from the fill color.
         */
        public legendColor?: string;

        /** An erea edge interpoltion method:
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
    }

    /** Describes a band in a pane. */
    export class BandData {
        /** A name of the data. */
        public name = '';

        /** Data array. */
        public data: Band[] = [];

        /** An index of an indicator in the output data. */
        public indicator = 0;

        /** An index of an output within an indicator in the output data. */
        public output = 0;

        /** A fill color of the band. */
        public color = '#00000011';

        /**
         *  The fill color of the band may be very transprent and its visibility on a legend may be poor.
         *  This allows to specify a legend color different from the fill color.
         */
        public legendColor?: string;

        /** A band edge interpoltion method:
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
    }

    /** Describes a band in a pane. */
    export class HeatmapData {
        /** A name of the data. */
        public name = '';

        /** Data array. */
        public data: Heatmap[] = [];

        /** An index of an indicator in the output data. */
        public indicator = 0;

        /** An index of an output within an indicator in the output data. */
        public output = 0;

        /** An intensity gradient:
         * - Viridis
         * - Inferno
         * - Magma
         * - Plasma
         * - Cividis
         * - Warm
         * - Cool
         * - Rainbow
         * - CubehelixDefault
         * - BuGn
         * - BuPu
         * - GnBu
         * - OrRd
         * - PuBuGn
         * - PuBu
         * - PuRd
         * - RdPu
         * - YlGnBu
         * - YlGn
         * - YlOrBr
         * - YlOrRd
         * - Blues
         * - Greens
         * - Greys
         * - Oranges
         * - Purples
         * - Reds
         */
       public gradient = 'viridis';

       /** If to invert the gradient. */
       public invertGradient = false;
    }

    /** Describes an vertical arrow in a pane. */
    export class ArrowData {
        /** A name of the arrow. */
        public name = '';

        /** Is an arrow points up or down. */
        public down: boolean;

        /** Arrow time. */
        public time: Date;

        /** An optional value to which the arrow points. */
        public value?: number;

        /** An index of an indicator in the output data. */
        public indicator = 0;

        /** An index of an output within an indicator in the output data. */
        public output = 0;

        /** A color of the arrow. */
        public color: string;
    }

    /** Describes an ohlcv data. */
    export class OhlcvData {
        /** A name of the data. */
        public name = '';

        /** Data array. */
        public data: Ohlcv[] = [];

        /** If data is displayed as candlesticks or as bars */
        public candlesticks = true;
    }

    /** Describes a pane. */
    export class Pane {
        /**
         * A height of the pane.
         * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
         */
        public height: string | number = 0;

        /** An optional minimal height of the pane in pixels. */
        public heightMin?: number | undefined = undefined;

        /** An optional maximal height of the pane in pixels. */
        public heightMax?: number | undefined = undefined;

        /** A d3.format specifier for value ticks and annotations on the pane. */
        public valueFormat = ',.2f';

        /** An optional number of ticks in the value axis. */
        public valueTicks?: number;

        /**
         * A percentage factor (e.g., 0.05) to add to a lower and upper parts of a value axis.
         * This allows to add space between the top / bottom of the pane and the max / min values.
         */
        public valueMarginPercentageFactor = 0;

        /** An optional heatmap on this pane. */
        public heatmap?: HeatmapData;

        /** An array of indicator bands on this pane. */
        public bands: BandData[] = [];

        /** An array of indicator line areas on this pane. */
        public lineAreas: LineAreaData[] = [];

        /** An array of indicator bands on this pane. */
        public horizontals: HorizontalData[] = [];

        /** An array of indicator lines on this pane. */
        public lines: LineData[] = [];

        /** An array of arrows on this pane. */
        public arrows: ArrowData[] = [];
  }

    /** Describes a navigation pane. */
    export class NavigationPane {
        /**
         * A height of the pane.
         * Can be either a positive number of pixels or a percentage string (e.g. '45%') of a reference width.
         */
        public height: string | number = 30;

        /** An optional minimal height of the pane in pixels. */
        public heightMin?: number | undefined = undefined;

        /** An optional maximal height of the pane in pixels. */
        public heightMax?: number | undefined = undefined;

        /** If navigation pane has closing price line. */
        public hasLine = true;

        /** If navigation pane has closing price area. */
        public hasArea = false;

        /** If navigation pane has time axis. */
        public hasTimeAxis = true;

        /** An optional d3.timeFormat specifier for time axis ticks, e.g. '%Y-%m-%d'. */
        public timeTicksFormat?: string | undefined = undefined;

        /** An optional number of ticks in the time axis. */
        public timeTicks?: number | undefined = undefined;
  }
}
