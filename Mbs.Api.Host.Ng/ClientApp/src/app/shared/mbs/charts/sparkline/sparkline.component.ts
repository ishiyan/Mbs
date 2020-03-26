import { Component, Input, ElementRef, OnChanges, ViewChild, HostListener, AfterViewInit } from '@angular/core';
import * as d3 from 'd3';

import { Ohlcv } from '../../data/entities/ohlcv';
import { Quote } from '../../data/entities/quote';
import { Trade } from '../../data/entities/trade';
import { Scalar } from '../../data/entities/scalar';
import { SparklineConfiguration } from './sparkline-configuration.interface';

@Component({
  selector: 'mb-sparkline',
  templateUrl: './sparkline.component.html',
  styleUrls: ['./sparkline.component.scss']
})
export class SparklineComponent implements OnChanges, AfterViewInit {
  private currentConfiguration: SparklineConfiguration = {
    fillColor: 'steelblue', strokeColor: undefined, strokeWidth: 0.1, interpolation: 'linear'
  };
  private currentData: Ohlcv[] | Quote[] | Trade[] | Scalar[];

  /** A width of the sparkline. */
  @Input() width = 160;

  /** A height of the sparkline. */
  @Input() height = 24;

  /** Specifies fill, stroke and interpolation. */
  @Input() set configuration(cfg: SparklineConfiguration) {
    if (cfg && cfg != null) {
      this.currentConfiguration = { ...this.currentConfiguration, ...cfg };
    }
  }
  get configuration(): SparklineConfiguration {
    return this.currentConfiguration;
  }

  /** The data array to use. */
  @Input() set data(dat: Ohlcv[] | Quote[] | Trade[] | Scalar[]) {
    this.currentData = dat;
  }
  get data(): Ohlcv[] | Quote[] | Trade[] | Scalar[] {
    return this.currentData;
  }

  private static convertInterpolation(interpolation: string): d3.CurveFactory {
    switch (interpolation.toLowerCase()) {
      case 'step': return d3.curveStep;
      case 'stepbefore': return d3.curveStepBefore;
      case 'stepafter': return d3.curveStepAfter;
      case 'natural': return d3.curveNatural;
      case 'basis': return d3.curveBasis;
      case 'catmullrom': return d3.curveCatmullRom;
      case 'cardinal': return d3.curveCardinal;
      case 'monotonex': return d3.curveMonotoneX;
      case 'monotoney': return d3.curveMonotoneY;
      default: return d3.curveLinear;
    }
  }

  constructor(private ref: ElementRef) { }

  ngOnChanges(changes: any) {
    this.render();
    // console.log('onChanges', changes);
  }
  ngAfterViewInit() {
    // this.render();
  }

  public render(): void {
    const sel = d3.select(this.ref.nativeElement);
    sel.select('svg').remove();
    const dat = this.currentData;
    if (!dat) {
      return;
    }
    const cfg = this.currentConfiguration;
    const w = this.width;
    const h = this.height;
    const svg: any = sel.append('svg').attr('preserveAspectRatio', 'xMinYMin meet')
      .attr('width', w).attr('height', h).attr('viewBox', `0 0 ${w} ${h}`);

    const xScale = d3.scaleLinear().domain([0, dat.length - 1]).range([0, w]);

    let yExtent: any[];
    let getY: any;
    if ((dat as Ohlcv[])[0].close !== undefined) {
      yExtent = d3.extent(dat as Ohlcv[], d => d.close);
      getY = (d: any) => (d as Ohlcv).close;
    } else if ((dat as Trade[])[0].price !== undefined) {
      yExtent = d3.extent(dat as Trade[], d => d.price);
      getY = (d: any) => (d as Trade).price;
    } else if ((dat as Quote[])[0].bidPrice !== undefined) {
      yExtent = d3.extent(dat as Quote[], d => d.bidPrice);
      getY = (d: any) => (d as Quote).bidPrice;
    } else {
      yExtent = d3.extent(dat as Scalar[], d => d.value);
      getY = (d: any) => (d as Scalar).value;
    }

    const interp = cfg.interpolation ? cfg.interpolation : 'linear;'
    const yScale = d3.scaleLinear().domain(yExtent).range([h, 0]);
    svg.datum(dat);
    if (cfg.fillColor && cfg.fillColor !== 'none') {
      const min: number = yExtent[0];
      const area: any = d3.area()
        .curve(SparklineComponent.convertInterpolation(interp))
        .defined((d: any) => !isNaN(getY(d)))
        .x((d: any, i: number) => xScale(i))
        .y0((d: any) => yScale(min))
        .y1((d: any) => yScale(getY(d)));
      svg.append('path') // .attr('class', 'area')
        .attr('fill', cfg.fillColor)
        .attr('d', area);
    }
    if (cfg.strokeColor && cfg.strokeWidth && cfg.strokeWidth > 0 && cfg.strokeColor !== 'none') {
      const line: any = d3.line()
        .curve(SparklineComponent.convertInterpolation(interp))
        .defined((d: any) => !isNaN(getY(d)))
        .x((d: any, i: number) => xScale(i))
        .y((d: any) => yScale(getY(d)));
      svg.append('path') // .attr('class', 'line')
        .style('stroke-width', cfg.strokeColor)
        .style('stroke', cfg.strokeColor)
        .attr('stroke-linejoin', 'round')
        .attr('stroke-linecap', 'round')
        .style('fill', 'none')
        .attr('d', line);
    }
  }
}
