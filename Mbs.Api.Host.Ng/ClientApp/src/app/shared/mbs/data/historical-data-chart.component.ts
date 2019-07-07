import { Component, OnInit, ElementRef, ViewChild, Input, ViewEncapsulation, HostListener } from '@angular/core';
import * as d3 from 'd3';
import * as d3ts from '../../../shared/d3ts';
import { Ohlcv } from './entities/ohlcv';
import { HistoricalData } from './historical-data';
import { TemporalEntityKind } from './entities/temporal-entity-kind.enum';

@Component({
  selector: 'app-historical-data-chart',
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

  private temporalEntityKind: TemporalEntityKind;
  private currentHistoricalData: HistoricalData;
  private data: any[] = [];
  private renderVolume = false;
  private renderNavAxis = false;
  private renderCandlestick = true;
  private renderCrosshair = true;
  private renderScalarAsPoint = false;
  private renderTradeAsPoint = true;

  get viewCandlestick() {
    return this.renderCandlestick;
  }
  set viewCandlestick(value: boolean) {
    this.renderCandlestick = value;
    this.render();
  }

  get viewCrosshair() {
    return this.renderCrosshair;
  }
  set viewCrosshair(value: boolean) {
    this.renderCrosshair = value;
    this.render();
  }

  get viewVolume() {
    return this.renderVolume;
  }
  set viewVolume(value: boolean) {
    this.renderVolume = value;
    this.render();
  }

  get viewScalarAsPoint() {
    return this.renderScalarAsPoint;
  }
  set viewScalarAsPoint(value: boolean) {
    this.renderScalarAsPoint = value;
    this.render();
  }

  get viewTradeAsPoint() {
    return this.renderTradeAsPoint;
  }
  set viewTradeAsPoint(value: boolean) {
    this.renderTradeAsPoint = value;
    this.render();
  }

  /*constructor(private element: ElementRef) {
  }*/

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

    let yVolume;
    let volume;
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

    function draw(renderVolume: boolean, temporalEntityKind: TemporalEntityKind) {
      const priceSelection = focus.select('g.price');
      const datum = priceSelection.datum();
      switch (temporalEntityKind) {
        case TemporalEntityKind.Ohlcv:
            y.domain(d3ts.scale.plot.ohlc(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
            break;
        case TemporalEntityKind.Quote:
            break;
        case TemporalEntityKind.Trade:
            y.domain(d3ts.scale.plot.tradepoint(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
            /*if (this.renderTradeAsPoint) {
              y.domain(d3ts.scale.plot.tradepoint(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
            } else {
              y.domain(d3ts.scale.plot.tradeline(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
            }*/
            break;
        case TemporalEntityKind.Scalar:
            y.domain(d3ts.scale.plot.valuepoint(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
            /*if (this.renderScalarAsPoint) {
              y.domain(d3ts.scale.plot.valuepoint(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
            } else {
              y.domain(d3ts.scale.plot.valueline(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
            }*/
            break;
      }
      // y.domain(d3ts.scale.plot.ohlc(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
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

    function brushed() {
      const zoomable = x.zoomable();
      const zoomableNav = xNav.zoomable();
      zoomable.domain(zoomableNav.domain());
      if (d3.event.selection !== null) {
        zoomable.domain(d3.event.selection.map(zoomable.invert));
      }
      draw(this.renderVolume, this.temporalEntityKind);
    }

    brushNav.on('end', brushed);

    // data begin ----------------------------------
    x.domain(this.data.map(accessor.t));
    xNav.domain(x.domain());
    switch (this.temporalEntityKind) {
      case TemporalEntityKind.Ohlcv:
          y.domain(d3ts.scale.plot.ohlc(this.data, accessor).domain());
          break;
      case TemporalEntityKind.Quote:
          break;
      case TemporalEntityKind.Trade:
          y.domain(d3ts.scale.plot.tradepoint(this.data, accessor).domain());
          /*if (this.renderTradeAsPoint) {
            y.domain(d3ts.scale.plot.tradepoint(this.data, accessor).domain());
          } else {
            y.domain(d3ts.scale.plot.tradeline(this.data, accessor).domain());
          }*/
          break;
      case TemporalEntityKind.Scalar:
          y.domain(d3ts.scale.plot.valuepoint(this.data, accessor).domain());
          /*if (this.renderScalarAsPoint) {
            y.domain(d3ts.scale.plot.valuepoint(this.data, accessor).domain());
          } else {
            y.domain(d3ts.scale.plot.valueline(this.data, accessor).domain());
          }*/
          break;
    }
    yNav.domain(y.domain());
    if (this.renderVolume) {
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
    draw(this.renderVolume, this.temporalEntityKind);
    // data end ----------------------------------
  }

  private getPriceShape(): any {
    switch (this.temporalEntityKind) {
      case TemporalEntityKind.Ohlcv:
        return this.renderCandlestick ? d3ts.plot.candlestick() : d3ts.plot.ohlc();
      case TemporalEntityKind.Quote:
        return d3ts.plot.candlestick();
      case TemporalEntityKind.Trade:
          return this.renderTradeAsPoint ? d3ts.plot.tradepoint() : d3ts.plot.tradeline();
      case TemporalEntityKind.Scalar:
        return this.renderScalarAsPoint ? d3ts.plot.valuepoint() : d3ts.plot.valueline();
    }
    return d3ts.plot.valueline();
  }

  private getNavArea(): any {
    switch (this.temporalEntityKind) {
      case TemporalEntityKind.Ohlcv:
        return d3ts.plot.ohlcarea();
      case TemporalEntityKind.Quote:
        return d3ts.plot.closeline();
      case TemporalEntityKind.Trade:
        return d3ts.plot.tradearea();
      case TemporalEntityKind.Scalar:
        return d3ts.plot.valuearea();
    }
    return d3ts.plot.valuearea();
  }
}
