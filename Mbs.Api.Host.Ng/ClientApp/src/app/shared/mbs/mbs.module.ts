import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { MathJaxModule } from '../math-jax/math-jax.module';
import { KatexModule } from '../katex/katex.module';
import { MaterialModule } from '../material/material.module';
import { SvgViewerModule } from '../svg-viewer/svg-viewer.module';

import { BusinessDayCalendarDescriptionComponent } from './time/business-day-calendar-description.component';
import { TimeParametersComponent } from './data/generators/time-parameters.component';
import { WaveformParametersComponent } from './data/generators/waveform-parameters.component';
import { OhlcvParametersComponent } from './data/generators/ohlcv-parameters.component';
import { QuoteParametersComponent } from './data/generators/quote-parameters.component';
import { TradeParametersComponent } from './data/generators/trade-parameters.component';
// eslint-disable-next-line max-len
import { FractionalBrownianMotionParametersComponent } from './data/generators/fractional-brownian-motion/fractional-brownian-motion-parameters.component';
// eslint-disable-next-line max-len
import { GeometricBrownianMotionParametersComponent } from './data/generators/geometric-brownian-motion/geometric-brownian-motion-parameters.component';
import { ChirpParametersComponent } from './data/generators/chirp/chirp-parameters.component';
import { SawtoothParametersComponent } from './data/generators/sawtooth/sawtooth-parameters.component';
import { SquareParametersComponent } from './data/generators/square/square-parameters.component';
import { SinusoidalParametersComponent } from './data/generators/sinusoidal/sinusoidal-parameters.component';
import { SyntheticDataParametersComponent } from './data/generators/synthetic-data-parameters.component';
import { SyntheticDataService } from './data/generators/synthetic-data.service';

import { HistoricalDataTableComponent } from './charts/historical-data-chart/historical-data-table.component';
import { HistoricalDataDownloadComponent } from './charts/historical-data-chart/historical-data-download.component';
import { HistoricalDataChartComponent } from './charts/historical-data-chart/historical-data-chart.component';

import { OhlcvChartComponent } from './charts/ohlcv-chart/ohlcv-chart.component';
import { SparklineComponent } from './charts/sparkline/sparkline.component';
import { MultilineComponent } from './charts/multiline/multiline.component';
import { StacklineComponent } from './charts/stackline/stackline.component';
import { SunburstComponent } from './charts/hierarchy-tree/sunburst/sunburst.component';
import { CirclepackComponent } from './charts/hierarchy-tree/circlepack/circlepack.component';
import { IcicleComponent } from './charts/hierarchy-tree/icicle/icicle.component';
import { TreemapComponent } from './charts/hierarchy-tree/treemap/treemap.component';
import { VoronoiComponent } from './charts/hierarchy-tree/voronoi/voronoi.component';
import { SwatchesComponent } from './colors/swatches/swatches.component';
import { SwatchesSelectComponent } from './colors/swatches/swatches-select.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule,
    FlexLayoutModule,
    FormsModule,
    MathJaxModule.forChild(),
    KatexModule,
    SvgViewerModule
  ],
  exports: [
    BusinessDayCalendarDescriptionComponent, TimeParametersComponent, WaveformParametersComponent,
    OhlcvParametersComponent, QuoteParametersComponent, TradeParametersComponent,
    FractionalBrownianMotionParametersComponent, GeometricBrownianMotionParametersComponent, ChirpParametersComponent,
    SawtoothParametersComponent, SquareParametersComponent, SinusoidalParametersComponent, SyntheticDataParametersComponent,
    HistoricalDataTableComponent, HistoricalDataDownloadComponent, HistoricalDataChartComponent,
    OhlcvChartComponent, SparklineComponent, MultilineComponent, StacklineComponent, SunburstComponent, CirclepackComponent,
    IcicleComponent, TreemapComponent, VoronoiComponent, SwatchesComponent, SwatchesSelectComponent
  ],
  declarations: [
    BusinessDayCalendarDescriptionComponent, TimeParametersComponent, WaveformParametersComponent,
    OhlcvParametersComponent, QuoteParametersComponent, TradeParametersComponent,
    FractionalBrownianMotionParametersComponent, GeometricBrownianMotionParametersComponent, ChirpParametersComponent,
    SawtoothParametersComponent, SquareParametersComponent, SinusoidalParametersComponent, SyntheticDataParametersComponent,
    HistoricalDataTableComponent, HistoricalDataDownloadComponent, HistoricalDataChartComponent,
    OhlcvChartComponent, SparklineComponent, MultilineComponent, StacklineComponent, SunburstComponent, CirclepackComponent,
    IcicleComponent, TreemapComponent, VoronoiComponent, SwatchesComponent, SwatchesSelectComponent
  ],
  providers: [
    SyntheticDataService
  ],
  entryComponents: [
    BusinessDayCalendarDescriptionComponent
  ]
})
export class MbsModule { }
