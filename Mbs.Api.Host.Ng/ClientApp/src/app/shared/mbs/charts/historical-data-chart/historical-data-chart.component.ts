import { Component, OnInit, ElementRef, ViewChild, Input, ViewEncapsulation, HostListener } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';
import * as d3 from 'd3';
// @ts-ignore
import * as d3ts from '../d3ts';

import { Downloader } from '../downloader';
import { HistoricalData } from '../../data/historical-data';
import { TemporalEntityKind } from '../../data/entities/temporal-entity-kind.enum';

const ohlcvViewCandlesticks = 0;
const ohlcvViewBars = 1;
const ohlcvViewLine = 2;
const ohlcvViewArea = 3;
const scalarViewLine = 0;
const scalarViewDots = 1;
const scalarViewArea = 2;
const tradeViewLine = 0;
const tradeViewDots = 1;
const tradeViewArea = 2;
const quoteViewBars = 0;
const quoteViewDots = 1;

/** The text to place before the SVG line when exporting chart as SVG. */
const textBeforeSvg = `<html><meta charset="utf-8"><style>
  text { fill: black; font-family: Arial, Helvetica, sans-serif; }
  path.candle { stroke: black; }
  path.candle.up { fill: white; }
  path.candle.down { fill: black; }
  path.ohlc.up { fill: none; stroke: black; }
  path.ohlc.down { fill: none; stroke: black; }
  path.volume { fill: lightgrey; }
  path.point { fill: none; stroke: black; }
  path.area { fill: lightgrey; }
  path.line { stroke: black; }
  rect.selection { fill: darkgrey; }
</style><body>
`;
/** The text to place after the SVG line when exporting chart as SVG. */
const textAfterSvg = `
</body></html>
`;

@Component({
  selector: 'mb-historical-data-chart',
  templateUrl: './historical-data-chart.component.html',
  styleUrls: ['./historical-data-chart.component.scss'],
  encapsulation: ViewEncapsulation.Emulated
})
export class HistoricalDataChartComponent implements OnInit {
  @ViewChild('container', { static: true }) container: ElementRef;
  @Input() svgheight: any;
  @Input()
  set historicalData(historicalData: HistoricalData) {
    this.currentHistoricalData = historicalData;
    if (historicalData && historicalData.data) {
      this.temporalEntityKind = historicalData.temporalEntityKind;
      this.data = historicalData.data;
    } else {
      this.temporalEntityKind = undefined;
      this.data = [];
    }
    this.render();
  }

  private temporalEntityKind: TemporalEntityKind | undefined;
  get isOhlcv(): boolean {
    return this.temporalEntityKind === TemporalEntityKind.Ohlcv;
  }
  get isQuote(): boolean {
    return this.temporalEntityKind === TemporalEntityKind.Quote;
  }
  get isTrade(): boolean {
    return this.temporalEntityKind === TemporalEntityKind.Trade;
  }
  get isScalar(): boolean {
    return this.temporalEntityKind === TemporalEntityKind.Scalar;
  }

  readonly ohlcvViewCandlesticks = ohlcvViewCandlesticks;
  readonly ohlcvViewBars = ohlcvViewBars;
  readonly ohlcvViewLine = ohlcvViewLine;
  readonly ohlcvViewArea = ohlcvViewArea;
  private ohlcvView: number = this.ohlcvViewCandlesticks;
  get ohlcvViewType(): number {
    return this.ohlcvView;
  }
  set ohlcvViewType(value: number) {
    this.ohlcvView = value;
    this.render();
  }

  readonly scalarViewLine = scalarViewLine;
  readonly scalarViewDots = scalarViewDots;
  readonly scalarViewArea = scalarViewArea;
  private scalarView: number = this.scalarViewLine;
  get scalarViewType(): number {
    return this.scalarView;
  }
  set scalarViewType(value: number) {
    this.scalarView = value;
    this.render();
  }

  readonly tradeViewLine = tradeViewLine;
  readonly tradeViewDots = tradeViewDots;
  readonly tradeViewArea = tradeViewArea;
  private tradeView: number = this.tradeViewLine;
  get tradeViewType(): number {
    return this.tradeView;
  }
  set tradeViewType(value: number) {
    this.tradeView = value;
    this.render();
  }

  readonly quoteViewBars = quoteViewBars;
  readonly quoteViewDots = quoteViewDots;
  private quoteView: number = this.quoteViewBars;
  get quoteViewType(): number {
    return this.quoteView;
  }
  set quoteViewType(value: number) {
    this.quoteView = value;
    this.render();
  }

  private renderCrosshair = false;
  get viewCrosshair() {
    return this.renderCrosshair;
  }
  set viewCrosshair(value: boolean) {
    this.renderCrosshair = value;
    this.render();
  }

  private renderVolume = false;
  get viewVolume() {
    return this.renderVolume;
  }
  set viewVolume(value: boolean) {
    this.renderVolume = value;
    this.render();
  }

  private currentHistoricalData: HistoricalData;
  get chartTitle(): string {
    if (this.currentHistoricalData && this.currentHistoricalData.moniker) {
      return this.currentHistoricalData.moniker;
    }
    return '-----';
  }

  private data: any[] = [];
  private renderNavAxis = false;

  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon('mb-candlesticks',
      sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-candlesticks.svg'));
    iconRegistry.addSvgIcon('mb-bars',
      sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-bars.svg'));
    iconRegistry.addSvgIcon('mb-line',
      sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-line.svg'));
    iconRegistry.addSvgIcon('mb-area',
      sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-area.svg'));
    iconRegistry.addSvgIcon('mb-dots',
      sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-dots.svg'));
  }

  ngOnInit() {
  }

  @HostListener('window:resize', [])
  render() {
    const chartId = '#chart';
    const margin = { top: 10, bottom: 20, right: 80, left: 35 };
    const marginNav = { top: this.svgheight - 30 - 40, bottom: 40, right: margin.right, left: margin.left };

    const w = this.container.nativeElement.getBoundingClientRect().width;
    d3.select(chartId).select('svg').remove();
    const svg: any = d3.select(chartId).append('svg')
      .attr('width', w)
      .attr('height', this.svgheight)
      .append('g')
      .attr('transform', 'translate(' + margin.left + ',' + margin.top + ')');
    const width = w - margin.left - margin.right;
    const height = marginNav.top - margin.top - margin.bottom;
    const heightNav = this.svgheight - marginNav.top - marginNav.bottom;

    const x = d3ts.scale.financetime().range([0, width]);
    const y = d3.scaleLinear().range([height, 0]);
    const xNav = d3ts.scale.financetime().range([0, width]);
    const yNav = d3.scaleLinear().range([heightNav, 0]);
    const brushNav = d3.brushX().extent([[0, 0], [width, heightNav + 4]]);
    const priceShape = this.getPriceShape().xScale(x).yScale(y);
    const accessor = priceShape.accessor();
    const areaNav = this.getNavArea().xScale(xNav).yScale(yNav);

    const xAxisBottom = d3.axisBottom(x);
    const yAxisLeft = d3.axisLeft(y);
    let xAxisNavBottom;
    if (this.renderNavAxis) {
      xAxisNavBottom = d3.axisBottom(xNav);
    }

    const priceAnnotationLeft = d3ts.plot.axisannotation().axis(yAxisLeft).orient('left')
      .format(d3.format(',.2f'));
    const timeAnnotationBottom = d3ts.plot.axisannotation().axis(xAxisBottom).orient('bottom')
      .format(d3.timeFormat('%Y-%m-%d')).width(65).translate([0, height]);

    let crosshair;
    if (this.renderCrosshair) {
      crosshair = d3ts.plot.crosshair().xScale(x).yScale(y)
        .xAnnotation(timeAnnotationBottom).yAnnotation(priceAnnotationLeft);
    }

    const focus = svg.append('g').attr('class', 'focus').attr('transform', 'translate(' + margin.left + ',' + margin.top + ')');
    focus.append('clipPath').attr('id', 'clip')
      .append('rect').attr('x', 0).attr('y', y(1)).attr('width', width).attr('height', y(0) - y(1));

    let yVolume: d3.ScaleLinear<number, number>;
    let volume: any;
    if (this.renderVolume) {
      yVolume = d3.scaleLinear().range([y(0), y(0.3)]);
      volume = d3ts.plot.volume().xScale(x).yScale(yVolume);
      focus.append('g').attr('class', 'volume').attr('clip-path', 'url(#clip)');
    }
    focus.append('g').attr('class', 'price').attr('clip-path', 'url(#clip)');
    focus.append('g').attr('class', 'x axis').attr('transform', 'translate(0,' + height + ')');
    focus.append('g').attr('class', 'y axis')
      .append('text').attr('transform', 'rotate(-90)').attr('y', 6).attr('dy', '.71em').style('text-anchor', 'end').text('Price');
    if (this.renderCrosshair) {
      focus.append('g').attr('class', 'crosshair').call(crosshair);
    }

    const nav = svg.append('g').attr('class', 'nav')
      .attr('transform', 'translate(' + marginNav.left + ',' + marginNav.top + ')');
    nav.append('g').attr('class', 'area');
    nav.append('g').attr('class', 'pane');
    if (this.renderNavAxis) {
      nav.append('g').attr('class', 'x axis').attr('transform', 'translate(0,' + heightNav + ')');
    }

    function draw(scalarView: number, tradeView: number, quoteView: number,
                  renderVolume: boolean, temporalEntityKind: TemporalEntityKind | undefined) {
      const priceSelection = focus.select('g.price');
      const datum = priceSelection.datum();
      switch (temporalEntityKind) {
        case TemporalEntityKind.Ohlcv:
          y.domain(d3ts.scale.plot.ohlc(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
          break;
        case TemporalEntityKind.Quote:
          switch (quoteView) {
            case quoteViewDots:
              y.domain(d3ts.scale.plot.quotepoint(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
              break;
            case quoteViewBars:
              y.domain(d3ts.scale.plot.quotebar(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
              break;
          }
          y.domain(d3ts.scale.plot.tick(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
          break;
        case TemporalEntityKind.Trade:
          switch (tradeView) {
            case tradeViewDots:
              y.domain(d3ts.scale.plot.tradepoint(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
              break;
            case tradeViewLine:
              y.domain(d3ts.scale.plot.tradeline(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
              break;
            case tradeViewArea:
              y.domain(d3ts.scale.plot.tradeline(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
              break;
          }
          break;
        case TemporalEntityKind.Scalar:
          switch (scalarView) {
            case scalarViewDots:
              y.domain(d3ts.scale.plot.valuepoint(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
              break;
            case scalarViewLine:
              y.domain(d3ts.scale.plot.valueline(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
              break;
            case scalarViewArea:
              y.domain(d3ts.scale.plot.valueline(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
              break;
          }
          break;
      }
      priceSelection.call(priceShape);
      if (renderVolume) {
        focus.select('g.volume').call(volume);
      }

      // Using refresh method is more efficient as it does not perform any data joins
      // Use this if underlying data is not changing
      svg.select('g.price').call(priceShape.refresh);

      focus.select('g.x.axis').call(xAxisBottom);
      focus.select('g.y.axis').call(yAxisLeft);
    }

    const sv = this.scalarView;
    const tv = this.tradeView;
    const qv = this.quoteView;
    const rv = this.renderVolume;
    const tek = this.temporalEntityKind;
    function brushed(event: any) {
      const zoomable = x.zoomable();
      const zoomableNav = xNav.zoomable();
      zoomable.domain(zoomableNav.domain());
      if (event.selection !== null) {
        zoomable.domain(event.selection.map(zoomable.invert));
      }
      draw(sv, tv, qv, rv, tek);
    }

    brushNav.on('end', brushed);

    // data begin ----------------------------------
    x.domain(this.data.map(accessor.t));
    xNav.domain(x.domain());
    // console.log('d3ts.scale.plot', d3ts.scale.plot); ///////////////////////////////////////////////////////////
    switch (this.temporalEntityKind) {
      case TemporalEntityKind.Ohlcv:
        y.domain(d3ts.scale.plot.ohlc(this.data, accessor).domain());
        break;
      case TemporalEntityKind.Quote:
        switch (this.quoteView) {
          case quoteViewDots:
            y.domain(d3ts.scale.plot.quotepoint(this.data, accessor).domain());
            break;
          case quoteViewBars:
            // console.log('d3ts.scale.plot', d3ts.scale.plot);
            y.domain(d3ts.scale.plot.quotebar(this.data, accessor).domain());
            break;
        }
        y.domain(d3ts.scale.plot.tick(this.data, accessor).domain());
        break;
      case TemporalEntityKind.Trade:
        switch (this.tradeView) {
          case tradeViewDots:
            y.domain(d3ts.scale.plot.tradepoint(this.data, accessor).domain());
            break;
          case tradeViewLine:
            y.domain(d3ts.scale.plot.tradeline(this.data, accessor).domain());
            break;
          case tradeViewArea:
            y.domain(d3ts.scale.plot.tradeline(this.data, accessor).domain());
            break;
        }
        break;
      case TemporalEntityKind.Scalar:
        switch (this.scalarView) {
          case scalarViewDots:
            y.domain(d3ts.scale.plot.valuepoint(this.data, accessor).domain());
            break;
          case scalarViewLine:
            y.domain(d3ts.scale.plot.valueline(this.data, accessor).domain());
            break;
          case scalarViewArea:
            y.domain(d3ts.scale.plot.valueline(this.data, accessor).domain());
            break;
        }
        break;
    }
    yNav.domain(y.domain());
    if (this.renderVolume) {
      // @ts-ignore
      yVolume.domain(d3ts.scale.plot.volume(this.data).domain());
    }
    focus.select('g.price').datum(this.data);
    if (this.renderVolume) {
      focus.select('g.volume').datum(this.data);
    }

    nav.select('g.area').datum(this.data).call(areaNav);
    if (this.renderNavAxis) {
      nav.select('g.x.axis').call(xAxisNavBottom);
    }

    // Associate the brush with the scale and render the brush only AFTER a domain has been applied
    nav.select('g.pane').call(brushNav).selectAll('rect').attr('height', heightNav);

    x.zoomable().domain(xNav.zoomable().domain());
    draw(this.scalarView, this.tradeView, this.quoteView, this.renderVolume, this.temporalEntityKind);
    // data end ----------------------------------
  }

  private getPriceShape(): any {
    switch (this.temporalEntityKind) {
      case TemporalEntityKind.Ohlcv:
        switch (this.ohlcvView) {
          case ohlcvViewCandlesticks: return d3ts.plot.candlestick();
          case ohlcvViewBars: return d3ts.plot.ohlc();
          case ohlcvViewLine: return d3ts.plot.closeline();
          case ohlcvViewArea: return d3ts.plot.ohlcarea();
          default: return d3ts.plot.candlestick();
        }
      case TemporalEntityKind.Quote:
        switch (this.quoteView) {
          case quoteViewDots: return d3ts.plot.quotepoint(); // quotepoint(); tick();
          case quoteViewBars: return d3ts.plot.quotebar(); // quotebar(); tick();
          default: return d3ts.plot.quotebar();
        }
      case TemporalEntityKind.Trade:
        switch (this.tradeView) {
          case tradeViewDots: return d3ts.plot.tradepoint();
          case tradeViewLine: return d3ts.plot.tradeline();
          case tradeViewArea: return d3ts.plot.tradearea();
          default: return d3ts.plot.tradeline();
        }
      case TemporalEntityKind.Scalar:
        switch (this.scalarView) {
          case scalarViewDots: return d3ts.plot.valuepoint();
          case scalarViewLine: return d3ts.plot.valueline();
          case scalarViewArea: return d3ts.plot.valuearea();
          default: return d3ts.plot.valueline();
        }
    }
    return d3ts.plot.valueline();
  }

  private getNavArea(): any {
    switch (this.temporalEntityKind) {
      case TemporalEntityKind.Ohlcv:
        return d3ts.plot.ohlcarea();
      case TemporalEntityKind.Quote:
        return d3ts.plot.quotearea();
      case TemporalEntityKind.Trade:
        return d3ts.plot.tradearea();
      case TemporalEntityKind.Scalar:
        return d3ts.plot.valuearea();
    }
    return d3ts.plot.valuearea();
  }

  public saveToSvg(): void {
    Downloader.download(Downloader.serializeToSvg(Downloader.getChildElementById(this.container.nativeElement.parentNode, 'chart'),
      textBeforeSvg, textAfterSvg), 'historical_data_chart.html');
  }
}
