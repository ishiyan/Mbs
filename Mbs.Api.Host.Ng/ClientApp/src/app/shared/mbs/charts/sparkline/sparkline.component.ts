import { Component, Input, ElementRef, OnChanges, ViewChild, HostListener } from '@angular/core';
import * as d3 from 'd3';

import { Ohlcv } from '../../data/entities/ohlcv';
import { Quote } from '../../data/entities/quote';
import { Trade } from '../../data/entities/trade';
import { Scalar } from '../../data/entities/scalar';
import { SparklineConfig } from './sparkline-config';

@Component({
  selector: 'mb-sparkline',
  templateUrl: './sparkline.component.html',
  styleUrls: ['./sparkline.component.scss']
})
export class SparklineComponent implements OnChanges {
  @ViewChild('container', { static: true }) container: ElementRef;
  @Input() svgheight: any;
  @Input()
  public set configuration(cfg: SparklineConfig) {
    if (cfg && cfg != null) {
      this.config = cfg;
    } else {
      this.config = new SparklineConfig();
    }
    this.render();
  }

  private config: SparklineConfig;
  private data: any[] = [];

  private static valueToPixels(value: number | string, reference: number): number {
    if (typeof value === 'number') {
      return +value;
    }

    const numeric = value.match(/\d+/);
    if (value.endsWith('%')) {
      // @ts-ignore
      return +numeric / 100 * reference;
    }
    // @ts-ignore
    return +numeric;
  }

  private static calculateWidth(cfg: SparklineConfig, referenceWidth: number): number {
    let totalWidth: number = SparklineComponent.valueToPixels(cfg.width, referenceWidth);
    if (cfg.widthMin && cfg.widthMin > totalWidth) {
      totalWidth = cfg.widthMin;
    }
    if (cfg.widthMax && cfg.widthMax < totalWidth) {
      totalWidth = cfg.widthMax;
    }
    return totalWidth - cfg.marginLeft - cfg.marginRight;
  }

  private static calculateHeight(cfg: SparklineConfig, width: number): number {
    let height = SparklineComponent.valueToPixels(cfg.height, width);
    if (cfg.heightMin && cfg.heightMin > height) {
      height = cfg.heightMin;
    }
    if (cfg.heightMax && cfg.heightMax < height) {
      height = cfg.heightMax;
    }
    return height;
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
      default: return d3.curveLinear;
    }
  }

  ngOnChanges() {
    this.render();
  }

  @HostListener('window:resize', [])
  public render(): void {
    const chartId = '#chart';
    // console.log('width=' + this.container.nativeElement.getBoundingClientRect().width);
    // console.log('offsetWidth=' + this.container.nativeElement.offsetWidth);
    // const w = this.container.nativeElement.getBoundingClientRect().width;
    const w = this.container.nativeElement.offsetWidth;
    const cfg = this.config;
    const width = SparklineComponent.calculateWidth(cfg, w);
    const height = SparklineComponent.calculateHeight(cfg, width);
    d3.select(chartId).select('svg').remove();
    const svg: any = d3.select(chartId).append('svg').attr('preserveAspectRatio', 'xMinYMin meet')
      .attr('width', width).attr('height', height).attr('viewBox', `0 0 ${width} ${height}`);

    const xScale = d3.scaleLinear().domain([0, cfg.data.length - 1]).range([0, width]);
    let yExtent: any[];
    let getY: any;
    if (cfg.data as Ohlcv[]) {
      yExtent = d3.extent(cfg.data as Ohlcv[], d => d.close);
      getY = (d: any) => (d as Ohlcv).close;
    } else if (cfg.data as Trade[]) {
      yExtent = d3.extent(cfg.data as Trade[], d => d.price);
      getY = (d: any) => (d as Trade).price;
    } else if (cfg.data as Quote[]) {
      yExtent = d3.extent(cfg.data as Quote[], d => d.bidPrice);
      getY = (d: any) => (d as Quote).bidPrice;
    } else {
      yExtent = d3.extent(cfg.data as Scalar[], d => d.value);
      getY = (d: any) => (d as Scalar).value;
    }
    const yScale = d3.scaleLinear().domain(yExtent).range([height, 0]);
    const root = d3.select(svg).datum(cfg.data);
    if (cfg.fillColor) {
      const min: number = yExtent[0];
      const area: any = d3.area()
        .curve(SparklineComponent.convertInterpolation(cfg.interpolation))
        .defined((d: any) => !isNaN(getY(d)))
        .x((d: any, i: number) => xScale(i))
        .y0((d: any) => yScale(min))
        .y1((d: any) => yScale(getY(d)));
      root.append('path').attr('class', 'area')
        .attr('fill', cfg.fillColor)
        .attr('d', area);
    }
    if (cfg.strokeColor && cfg.strokeWidth) {
      const line: any = d3.line()
        .curve(SparklineComponent.convertInterpolation(cfg.interpolation))
        .defined((d: any) => !isNaN(getY(d)))
        .x((d: any, i: number) => xScale(i))
        .y((d: any) => yScale(getY(d)));
      root.append('path').attr('class', 'line')
        .style('stroke-width', cfg.strokeColor)
        .style('stroke', cfg.strokeColor)
        .attr('stroke-linejoin', 'round')
        .attr('stroke-linecap', 'round')
        .style('fill', 'none')
        .attr('d', line);
    }
  }
}
