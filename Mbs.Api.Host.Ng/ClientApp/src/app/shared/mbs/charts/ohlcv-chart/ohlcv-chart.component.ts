import { Component, ElementRef, ViewChild, Input, HostListener } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';
import * as d3 from 'd3';
import * as d3ts from '../d3ts';

import { Ohlcv } from '../../data/entities/ohlcv';
import { Scalar } from '../../data/entities/scalar';
import { Band } from '../entities/band';
import { Heatmap } from '../entities/heatmap';
import { OhlcvChartConfig } from './ohlcv-chart-config';
import { Downloader } from '../downloader';

/** *Ohlcv* view type: *candlesticks*. */
const ohlcvViewCandlesticks = 0;
/** *Ohlcv* view type: *bars*. */
const ohlcvViewBars = 1;
/** The width of a vertical axis in pixels including annotation. */
const verticalAxisWidth = 50;
/** The height of a vertical gap between an arrow and a price point in pixels. */
const verticalArrowGap = 3;
/** The minimal width of an arrow in pixels. */
const minArrowWidth = 3;
/** The arrow height / width ratio. */
const arowHeightToWidthRatio = 5 / 4;
/** The minimal navigation selection width in pixels which causes the chart to re-draw. */
const minSelection = 10;
/** The height of a time axis in pixels. */
const timeAxisHeight = 22;
/** The horizontal distance between y axis and legend item in pixels. */
const whitespaceBetweenAxisAndLegend = 8;
/** The horizontal distance between legend items in pixels. */
const whitespaceBetweenLegendItems = 5;
/** The width of a legend heatmap item image in pixels. */
const legendHeatmapImageWidth = 50;
/** The width of a legend heatmap item image in pixels. */
const legendHeatmapImageHeight = 6;
/** The width of a legend area item image in pixels. */
const legendAreaImageWidth = 20;
/** The width of a legend area item image in pixels. */
const legendAreaImageHeight = 6;
/** The width of a legend line item image in pixels. */
const legendLineImageWidth = 20;
/** The width of a legend line item image in pixels. */
const legendLineImageHeight = 6;
/** Default number of pixels between time ticks on horizontal time axis. */
const defaultWhitespaceBetweenTimeTicks = 100;
/** Default number of pixels between value ticks on vertical value axis. */
const defaultWhitespaceBetweenValueTicks = 20;
/** The minimal date. */
const minDate = new Date(-8640000000000000);
/** The maximal date. */
const maxDate = new Date(8640000000000000);
/** The text to place before the SVG line when exporting chart as SVG. */
const textBeforeSvg = `<html><meta charset="utf-8"><style>
  text { fill: black; font-family: Arial, Helvetica, sans-serif; }
  path.candle { stroke: black; }
  path.candle.up { fill: white; }
  path.candle.down { fill: black; }
  path.ohlc.up { fill: none; stroke: black; }
  path.ohlc.down { fill: none; stroke: black; }
  path.volume { fill: lightgrey; }
  path.area { fill: lightgrey; }
  path.line { stroke: black; }
  rect.selection { fill: darkgrey; }
</style><body>
`;
/** The text to place after the SVG line when exporting chart as SVG. */
const textAfterSvg = `
</body></html>
`;
/** If *brushing* (zooming) is real-time. */
const smoothBrushing = false;

@Component({
  selector: 'app-mbs-ohlcv-chart',
  templateUrl: './ohlcv-chart.component.html',
  styleUrls: ['./ohlcv-chart.component.scss']
})
export class OhlcvChartComponent {
  @ViewChild('container', { static: true }) container: ElementRef;
  @Input()
  public set configuration(cfg: OhlcvChartConfig) {
    if (cfg && cfg != null) {
      this.config = cfg;
      this.ohlcvView = cfg.ohlcv.candlesticks ? ohlcvViewCandlesticks : ohlcvViewBars;
      this.renderCrosshair = cfg.crosshair;
      this.renderVolume = cfg.volumeInPricePane;
      this.currentSelection = null;
    } else {
      this.config = new OhlcvChartConfig();
    }
    this.render();
  }

  private config: OhlcvChartConfig;

  /** Gets if menu is visible. */
  public get viewMenu(): boolean {
    return this.config ? this.config.menuVisible : false;
  }

  /** Gets if *download SVG* menu setting is visible. */
  public get viewDownloadSvg(): boolean {
    return this.config ? this.config.downloadSvgVisible : false;
  }

  /** Gets or sets the *ohlcv* view type: *candlesticks* or *bars*. */
  public get ohlcvViewType(): number {
    return this.ohlcvView;
  }
  public set ohlcvViewType(value: number) {
    this.ohlcvView = value;
    this.render();
  }
  private ohlcvView: number = (this.config && !this.config.ohlcv.candlesticks) ? ohlcvViewBars : ohlcvViewCandlesticks;
  public readonly ohlcvViewCandlesticks = ohlcvViewCandlesticks;
  public readonly ohlcvViewBars = ohlcvViewBars;

  /** Gets or sets if *crosshair* is visible */
  public get viewCrosshair(): boolean {
    return this.renderCrosshair;
  }
  public set viewCrosshair(value: boolean) {
    this.renderCrosshair = value;
    this.render();
  }
  private renderCrosshair = this.config ? this.config.crosshair : true;

  /** Gets or sets if volume in price pane is visible */
  public get viewVolume(): boolean {
    return this.renderVolume;
  }
  public set viewVolume(value: boolean) {
    this.renderVolume = value;
    this.render();
  }
  private renderVolume = this.config ? this.config.volumeInPricePane : true;

  /** Gets a title of the chart. */
  public get chartTitle(): string {
    return this.config ? this.config.ohlcv.name : '---';
  }

  private currentSelection: any = null;

  constructor(private element: ElementRef, iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon('mb-candlesticks',
      sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-candlesticks.svg'));
    iconRegistry.addSvgIcon('mb-bars',
      sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-bars.svg'));
  }

  public downloadSvg(): void {
    Downloader.download(Downloader.serializeToSvg(Downloader.getChildElementById(this.container.nativeElement.parentNode, 'chart'),
      textBeforeSvg, textAfterSvg), 'ohlcv_chart.html');
  }

  @HostListener('window:resize', [])
  render() {
    const chartId = '#chart';
    // console.log('width=' + this.container.nativeElement.getBoundingClientRect().width);
    // console.log('offsetWidth=' + this.container.nativeElement.offsetWidth);
    // const w = this.container.nativeElement.getBoundingClientRect().width;
    const w = this.container.nativeElement.offsetWidth;
    const cfg = this.config;
    const lh = OhlcvChartComponent.layoutHorizontal(cfg, w);

    d3.select(chartId).select('svg').remove();
    const svg: any = d3.select(chartId).append('svg')
      .attr('preserveAspectRatio', 'xMinYMin meet').attr('width', lh.width);

    const lv = OhlcvChartComponent.layoutVertical(svg, cfg, lh);
    svg.attr('height', lv.height).attr('viewBox', `0 0 ${lh.width} ${lv.height}`);

    const timePane = OhlcvChartComponent.createTimePane(cfg, lh, lv, svg);

    const pricePane = OhlcvChartComponent.createPricePane(cfg, lh, lv, timePane.timeScale, timePane.timeAnnotation,
      svg, this.ohlcvView === ohlcvViewCandlesticks, this.renderVolume, this.renderCrosshair);

    const indicatorPanes: OhlcvChartComponent.IndicatorPane[] = [];
    for (let i = 0; i < cfg.indicatorPanes.length; ++i) {
      const pane = OhlcvChartComponent.createIndicatorPane(i, cfg, lh, lv, timePane.timeScale, timePane.timeAxis,
        svg, this.renderCrosshair);
      indicatorPanes.push(pane);
    }

    const navPane = OhlcvChartComponent.createNavPane(cfg, lh, lv, svg);

    function draw(): void {
      timePane.draw();
      pricePane.draw(timePane);
      for (let i = 0; i < indicatorPanes.length; ++i) {
        indicatorPanes[i].draw(timePane, pricePane);
      }
    }

    const setCurrentSelection = x => { this.currentSelection = x; };

    function brushed(): void {
      const zoomable = timePane.timeScale.zoomable();
      const zoomableNav = navPane.timeScale.zoomable();
      zoomable.domain(zoomableNav.domain());
      if (!d3.event.selection) {
        setCurrentSelection(null);
        draw();
      } else {
        setCurrentSelection(d3.event.selection);
        zoomable.domain(d3.event.selection.map(zoomable.invert));
        if (!smoothBrushing) {
          draw();
        }
      }
    }

    function brushing(): void {
      if (d3.event.selection) {
        const sel = d3.event.selection;
        if (sel[1] - sel[0] > minSelection) {
          // setCurrentSelection(sel);
          const zoomable = timePane.timeScale.zoomable();
          const zoomableNav = navPane.timeScale.zoomable();
          zoomable.domain(zoomableNav.domain());
          zoomable.domain(d3.event.selection.map(zoomable.invert));
          draw();
        }
      }
    }

    if (smoothBrushing) {
      navPane.brush.on('brush', brushing);
    }
    navPane.brush.on('end', brushed);

    timePane.timeScale.domain(cfg.ohlcv.data.map(pricePane.priceAccessor.t));
    // console.log(OhlcvChartComponent.firstTime(cfg));
    // console.log(OhlcvChartComponent.lastTime(cfg));
    // timePane.timeScale.domain([OhlcvChartComponent.firstTime(cfg), OhlcvChartComponent.lastTime(cfg)]);
    // navPane.timeScale.domain([OhlcvChartComponent.firstTime(cfg), OhlcvChartComponent.lastTime(cfg)]);
    navPane.timeScale.domain(timePane.timeScale.domain());
    pricePane.yPrice.domain(d3ts.scale.plot.ohlc(cfg.ohlcv.data, pricePane.priceAccessor).domain());

    navPane.priceScale.domain(pricePane.yPrice.domain());
    if (pricePane.yVolume) {
      pricePane.yVolume.domain(d3ts.scale.plot.volume(cfg.ohlcv.data).domain());
    }
    pricePane.groupPrice.datum(cfg.ohlcv.data);
    if (this.renderVolume) {
      pricePane.groupVolume.datum(cfg.ohlcv.data);
    }

    pricePane.setIndicatorDatum();

    for (let i = 0; i < indicatorPanes.length; ++i) {
      indicatorPanes[i].setIndicatorDatum();
    }

    if (navPane.area) {
      navPane.areaSelection.datum(cfg.ohlcv.data).call(navPane.area);
    }
    if (navPane.line) {
      navPane.lineSelection.datum(cfg.ohlcv.data).call(navPane.line);
    }
    if (navPane.timeAxis) {
      navPane.timeAxisSelection.call(navPane.timeAxis);
    }

    // associate the brush with the scale and render the brush only AFTER a domain has been applied
    navPane.paneSelection.call(navPane.brush).selectAll('rect').attr('height', lv.navigationPane.height);

    if (this.currentSelection != null && (this.currentSelection[1] - this.currentSelection[0] > minSelection)) {
      navPane.brush.move(navPane.paneSelection, this.currentSelection);
      const zoomable = timePane.timeScale.zoomable();
      const zoomableNav = navPane.timeScale.zoomable();
      zoomable.domain(zoomableNav.domain());
      zoomable.domain(this.currentSelection.map(zoomable.invert));
    }
    draw();
  }

  private static valueToPixels(value: number | string, reference: number): number {
    if (typeof value === 'number') {
      return +value;
    }

    const numeric = value.match(/\d+/);
    if (value.endsWith('%')) {
      return +numeric / 100 * reference;
    }

    return +numeric;
  }

  private static layoutHorizontal(cfg: OhlcvChartConfig, referenceWidth: number): OhlcvChartComponent.HorizontalLayout {
    let totalWidth: number = OhlcvChartComponent.valueToPixels(cfg.width, referenceWidth);
    if (cfg.widthMin && cfg.widthMin > totalWidth) {
      totalWidth = cfg.widthMin;
    }
    if (cfg.widthMax && cfg.widthMax < totalWidth) {
      totalWidth = cfg.widthMax;
    }

    const chartLeft: number = cfg.margin.left;
    const chartWidth: number = totalWidth - chartLeft - cfg.margin.right;

    const contentLeft: number = cfg.axisLeft ? verticalAxisWidth : 0;
    const contentWidth: number = chartWidth - contentLeft - (cfg.axisRight ? verticalAxisWidth : 0);

    return { width: totalWidth, chart: { left: chartLeft, width: chartWidth }, content: { left: contentLeft, width: contentWidth } };
  }

  private static textBoundingClientRect(t: any, remove: boolean = false): any {
    const node = t.node();
    let rect: any;
    if (node && node != null) {
      rect = node.getBoundingClientRect();
      if (remove) {
        t.remove();
      }
    } else {
      // console.log('node is null');
      rect = { width: 100, height: 12 };
    }
    return rect;
  }

  private static layoutVertical(svg: any, cfg: OhlcvChartConfig,
    lh: OhlcvChartComponent.HorizontalLayout): OhlcvChartComponent.VerticalLayout {
    const t = svg.append('text').text(`w`);
    const lineHeight = OhlcvChartComponent.textBoundingClientRect(t, true).height;
    const heightPricePaneLegend: number = OhlcvChartComponent.appendLegend(svg, cfg.margin.top, lineHeight, lh.content.left,
      lh.content.width, cfg.pricePane, cfg.ohlcv.name);
    const l = new OhlcvChartComponent.VerticalLayout();
    l.pricePane.top = cfg.margin.top + heightPricePaneLegend;
    let q = OhlcvChartComponent.valueToPixels(cfg.pricePane.height, lh.content.width);
    if (cfg.pricePane.heightMin && cfg.pricePane.heightMin > q) {
      q = cfg.pricePane.heightMin;
    }
    if (cfg.pricePane.heightMax && cfg.pricePane.heightMax < q) {
      q = cfg.pricePane.heightMax;
    }
    l.pricePane.height = q;

    let top = l.pricePane.top + l.pricePane.height;
    if (cfg.indicatorPanes && cfg.indicatorPanes.length) {
      for (let i = 0; i < cfg.indicatorPanes.length; ++i) {
        const pane = cfg.indicatorPanes[i];
        const block = new OhlcvChartComponent.VerticalLayoutBlock();
        const legendHeight: number = OhlcvChartComponent.appendLegend(svg, top, lineHeight, lh.content.left,
          lh.content.width, pane, undefined);
        block.top = top + legendHeight;
        q = OhlcvChartComponent.valueToPixels(pane.height, lh.content.width);
        if (pane.heightMin && pane.heightMin > q) {
          q = pane.heightMin;
        }
        if (pane.heightMax && pane.heightMax < q) {
          q = pane.heightMax;
        }
        block.height = q;
        l.indicatorPanes.push(block);
        top = block.top + block.height;
      }
    }

    l.timeAxis.top = top;
    l.timeAxis.height = timeAxisHeight;
    top += l.timeAxis.height;

    l.navigationPane.top = top;
    if (cfg.navigationPane) {
      const nav = cfg.navigationPane;
      q = OhlcvChartComponent.valueToPixels(nav.height, lh.content.width);
      if (nav.heightMin && nav.heightMin > q) {
        q = nav.heightMin;
      }
      if (nav.heightMax && nav.heightMax < q) {
        q = nav.heightMax;
      }
      if (nav.hasTimeAxis) {
        if (!nav.timeTicks || nav.timeTicks > 0) {
          q += timeAxisHeight;
        }
      }
      l.navigationPane.height = q;
      top += q;
    }

    l.height = top + cfg.margin.bottom;
    return l;
  }

  private static appendLegend(g: any, top: number, lineHeight: number, left: number, width: number,
    pane: OhlcvChartConfig.Pane, instrument: string = ''): number {
    g = g.append('g').attr('class', 'legend');
    top += lineHeight / 2;
    left += whitespaceBetweenAxisAndLegend;
    let l = left;
    let height = 0;

    if (instrument && instrument.length > 0) {
      const t = OhlcvChartComponent.appendText(g, l, top, ` ${instrument} `);
      const r = OhlcvChartComponent.textBoundingClientRect(t);
      l += r.width + whitespaceBetweenAxisAndLegend;
      height = r.height;
    }

    if (pane.heatmap) {
      const heiz = 6;
      const q = g.append('image').attr('x', l).attr('y', top - legendHeatmapImageHeight)
        .attr('width', legendHeatmapImageWidth).attr('height', legendHeatmapImageHeight).attr('preserveAspectRatio', 'none')
        .attr('xlink:href',
          OhlcvChartComponent.ramp(OhlcvChartComponent.convertGradient(pane.heatmap.gradient), pane.heatmap.invertGradient,
            legendHeatmapImageWidth, legendHeatmapImageHeight).toDataURL());
      const d1 = legendHeatmapImageWidth + whitespaceBetweenLegendItems;
      l += d1;
      const t = OhlcvChartComponent.appendText(g, l, top, pane.heatmap.name);
      const r = OhlcvChartComponent.textBoundingClientRect(t);
      if (height === 0) {
        height = r.height;
      }
      const w = r.width;
      if (l + w > width) {
        top += r.height;
        height += r.height;
        q.attr('x', left).attr('y', top - legendHeatmapImageHeight);
        l = left + d1;
        t.attr('x', l).attr('y', top);
      }
      l += w + whitespaceBetweenAxisAndLegend;
    }

    for (let i = 0; i < pane.bands.length; ++i) {
      const band = pane.bands[i];
      const q = g.append('rect').attr('x', l).attr('y', top - legendAreaImageHeight)
        .attr('width', legendAreaImageWidth).attr('height', legendAreaImageHeight).attr('stroke-width', 0)
        .attr('fill', band.color);
      const d1 = legendAreaImageWidth + whitespaceBetweenLegendItems;
      l += d1;
      const t = OhlcvChartComponent.appendText(g, l, top, band.name);
      const r = OhlcvChartComponent.textBoundingClientRect(t);
      if (height === 0) {
        height = r.height;
      }
      const w = r.width;
      if (l + w > width) {
        top += r.height;
        height += r.height;
        q.attr('x', left).attr('y', top - legendAreaImageHeight);
        l = left + d1;
        t.attr('x', l).attr('y', top);
      }
      l += w + whitespaceBetweenAxisAndLegend;
    }

    for (let i = 0; i < pane.lineAreas.length; ++i) {
      const lineArea = pane.lineAreas[i];
      const q = g.append('rect').attr('x', l).attr('y', top - legendAreaImageHeight)
        .attr('width', legendAreaImageWidth).attr('height', legendAreaImageHeight).attr('stroke-width', 0)
        .attr('fill', lineArea.color);
      const d1 = legendAreaImageWidth + whitespaceBetweenLegendItems;
      l += d1;

      const t = OhlcvChartComponent.appendText(g, l, top, lineArea.name);
      const r = OhlcvChartComponent.textBoundingClientRect(t);
      if (height === 0) {
        height = r.height;
      }
      const w = r.width;
      if (l + w > width) {
        top += r.height;
        height += r.height;
        q.attr('x', left).attr('y', top - legendAreaImageHeight);
        l = left + d1;
        t.attr('x', l).attr('y', top);
      }
      l += w + whitespaceBetweenAxisAndLegend;
    }

    for (let i = 0; i < pane.lines.length; ++i) {
      const line = pane.lines[i];
      const hei = (legendLineImageHeight - line.width) / 2;
      const q = g.append('line').attr('x1', l).attr('y1', top - hei)
        .attr('x2', l + legendLineImageWidth).attr('y2', top - hei).attr('stroke-width', line.width)
        .attr('stroke', line.color).attr('stroke-dasharray', line.dash).attr('fill', 'none');
      const d1 = legendLineImageWidth + whitespaceBetweenLegendItems;
      l += d1;

      const t = OhlcvChartComponent.appendText(g, l, top, line.name);
      const r = OhlcvChartComponent.textBoundingClientRect(t);
      if (height === 0) {
        height = r.height;
      }
      const w = r.width;
      if (l + w > width) {
        top += r.height;
        height += r.height;
        q.attr('x1', left).attr('y1', top - hei).attr('x2', left + legendLineImageWidth).attr('y2', top - hei);
        l = left + d1;
        t.attr('x', l).attr('y', top);
      }
      l += w + whitespaceBetweenAxisAndLegend;
    }
    return height;
  }

  private static firstTime(cfg: OhlcvChartConfig): Date {
    let time = cfg.ohlcv.data[0].time;
    for (let i = 0; i < cfg.pricePane.bands.length; ++i) {
      const data = cfg.pricePane.bands[i].data;
      const t = data[0].time;
      if (time > t) {
        time = t;
      }
    }
    for (let i = 0; i < cfg.pricePane.lines.length; ++i) {
      const data = cfg.pricePane.lines[i].data;
      const t = data[0].time;
      if (time > t) {
        time = t;
      }
    }
    for (let j = 0; j < cfg.indicatorPanes.length; ++j) {
      const pane = cfg.indicatorPanes[j];
      for (let i = 0; i < pane.bands.length; ++i) {
        const data = pane.bands[i].data;
        const t = data[0].time;
        if (time > t) {
          time = t;
        }
      }
      for (let i = 0; i < pane.lines.length; ++i) {
        const data = pane.lines[i].data;
        const t = data[0].time;
        if (time > t) {
          time = t;
        }
      }
    }
    return time;
  }

  private static lastTime(cfg: OhlcvChartConfig): Date {
    let time = cfg.ohlcv.data[cfg.ohlcv.data.length - 1].time;
    for (let i = 0; i < cfg.pricePane.bands.length; ++i) {
      const data = cfg.pricePane.bands[i].data;
      const t = data[data.length - 1].time;
      if (time < t) {
        time = t;
      }
    }
    for (let i = 0; i < cfg.pricePane.lines.length; ++i) {
      const data = cfg.pricePane.lines[i].data;
      const t = data[data.length - 1].time;
      if (time < t) {
        time = t;
      }
    }
    for (let j = 0; j < cfg.indicatorPanes.length; ++j) {
      const pane = cfg.indicatorPanes[j];
      for (let i = 0; i < pane.bands.length; ++i) {
        const data = pane.bands[i].data;
        const t = data[data.length - 1].time;
        if (time < t) {
          time = t;
        }
      }
      for (let i = 0; i < pane.lines.length; ++i) {
        const data = pane.lines[i].data;
        const t = data[data.length - 1].time;
        if (time < t) {
          time = t;
        }
      }
    }
    // return time;
    return new Date(time.getFullYear(), time.getMonth(), time.getDay() + 10);
  }

  private static appendText(group: any, left: number, top: number, text: string): any {
    return group.append('text')
      .attr('font-size', '10px')
      .attr('font-family', 'sans-serif')
      .attr('x', left)
      .attr('y', top)
      .text(text);
  }

  private static estimateNumberOfVerticalTicks(height: number): number {
    return height / defaultWhitespaceBetweenValueTicks;
  }

  private static createPricePane(cfg: OhlcvChartConfig, lh: OhlcvChartComponent.HorizontalLayout,
    lv: OhlcvChartComponent.VerticalLayout, timeScale: any, timeAnnotationBottom: any, svg: any,
    isCandlestick: boolean, isVolume: boolean, isCrossHair: boolean): OhlcvChartComponent.PricePane {

    const cf = cfg.pricePane;
    const pane = new OhlcvChartComponent.PricePane();
    pane.yPrice = d3.scaleLinear().range([lv.pricePane.height, 0]);
    const factor = cf.valueMarginPercentageFactor;
    pane.yMarginFactorTop = 1 + factor;
    pane.yMarginFactorBottom = 1 - factor;
    pane.priceShape = (isCandlestick ? d3ts.plot.candlestick() : d3ts.plot.ohlc()).xScale(timeScale).yScale(pane.yPrice);
    pane.priceAccessor = pane.priceShape.accessor();

    const clip = 'price-clip';
    const clipUrl = `url(#${clip})`;
    pane.group = svg.append('g').attr('class', 'price-pane').attr('transform', `translate(${lh.content.left}, ${lv.pricePane.top})`);
    pane.group.append('clipPath').attr('id', clip).append('rect').attr('x', 0).attr('y', pane.yPrice(1))
      .attr('width', lh.content.width).attr('height', pane.yPrice(0) - pane.yPrice(1));

    for (let i = 0; i < cf.bands.length; ++i) {
      const band = cf.bands[i];
      const indicatorBand = new OhlcvChartComponent.IndicatorBand();
      indicatorBand.path = pane.group.append('g').attr('class', `band-${i}`).attr('clip-path', clipUrl).append('path')
        .attr('fill', band.color);
      indicatorBand.area = d3.area()
        .curve(OhlcvChartComponent.convertInterpolation(band.interpolation))
        .defined(d => { const w: any = d; return !isNaN(w.lower) && !isNaN(w.upper); })
        .x(d => { const w: any = d; return timeScale(w.time); })
        .y0(d => { const w: any = d; return pane.yPrice(w.lower); })
        .y1(d => { const w: any = d; return pane.yPrice(w.upper); });
      indicatorBand.data = band.data;
      pane.indicatorBands.push(indicatorBand);
    }

    for (let i = 0; i < cf.lineAreas.length; ++i) {
      const lineArea = cf.lineAreas[i];
      const value = lineArea.value;
      const indicatorLineArea = new OhlcvChartComponent.IndicatorLineArea();
      indicatorLineArea.path = pane.group.append('g').attr('class', `linearea-${i}`).attr('clip-path', clipUrl).append('path')
        .attr('fill', lineArea.color);
      indicatorLineArea.area = d3.area()
        .curve(OhlcvChartComponent.convertInterpolation(lineArea.interpolation))
        .defined(d => { const w: any = d; return !isNaN(w.value); })
        .x(d => { const w: any = d; return timeScale(w.time); })
        .y0(d => { const w: any = d; return pane.yPrice(w.value); })
        .y1(d => { const w: any = d; return pane.yPrice(value); });
      indicatorLineArea.data = lineArea.data;
      indicatorLineArea.value = lineArea.value;
      pane.indicatorLineAreas.push(indicatorLineArea);
    }

    for (let i = 0; i < cf.horizontals.length; ++i) {
      const horizontal = cf.horizontals[i];
      const indicatorHorizontal = new OhlcvChartComponent.IndicatorHorizontal();
      indicatorHorizontal.path = pane.group.append('g').attr('class', `horizontal-${i}`).attr('clip-path', clipUrl).append('path')
        .attr('stroke', horizontal.color)
        .attr('stroke-width', horizontal.width)
        .attr('stroke-dasharray', horizontal.dash)
        .attr('stroke-linejoin', 'round')
        .attr('stroke-linecap', 'round')
        .attr('fill', 'none');
      const value = horizontal.value;
      indicatorHorizontal.value = value;
      indicatorHorizontal.data =
        [{ time: minDate, value: value }, { time: maxDate, value: value }];
      indicatorHorizontal.line = d3.line()
        .x(d => { const w: any = d; return timeScale(w.time); })
        .y(d => pane.yPrice(value));
      pane.indicatorHorizontals.push(indicatorHorizontal);
    }

    for (let i = 0; i < cf.lines.length; ++i) {
      const line = cf.lines[i];
      const indicatorLine = new OhlcvChartComponent.IndicatorLine();
      indicatorLine.path = pane.group.append('g').attr('class', `line-${i}`).attr('clip-path', clipUrl).append('path')
        .attr('stroke', line.color)
        .attr('stroke-width', line.width)
        .attr('stroke-dasharray', line.dash)
        .attr('stroke-linejoin', 'round')
        .attr('stroke-linecap', 'round')
        .attr('fill', 'none');
      indicatorLine.line = d3.line()
        .curve(OhlcvChartComponent.convertInterpolation(line.interpolation))
        .defined(d => { const w: any = d; return !isNaN(w.value); })
        .x(d => { const w: any = d; return timeScale(w.time); })
        .y(d => { const w: any = d; return pane.yPrice(w.value); });
      indicatorLine.data = line.data;
      pane.indicatorLines.push(indicatorLine);
    }

    pane.groupPrice = pane.group.append('g').attr('class', 'price').attr('clip-path', clipUrl);

    if (isVolume) {
      pane.yVolume = d3.scaleLinear().range([pane.yPrice(0), pane.yPrice(0.3)]);
      pane.volume = d3ts.plot.volume().xScale(timeScale).yScale(pane.yVolume);
      pane.groupVolume = pane.group.append('g').attr('class', 'volume').attr('clip-path', clipUrl);
    }

    let gArrows;
    for (let i = 0; i < cf.arrows.length; ++i) {
      if (!gArrows) {
        gArrows = pane.group.append('g').attr('class', `arrows`).attr('clip-path', clipUrl);
      }
      const arrow = cf.arrows[i];
      const indicatorArrow = new OhlcvChartComponent.IndicatorArrow();
      indicatorArrow.isDown = arrow.down;
      indicatorArrow.path = gArrows.append('path')
        .attr('stroke-width', 0)
        .attr('fill', arrow.color);
      const price = OhlcvChartComponent.getArrowPrice(cfg.ohlcv.data, arrow);
      indicatorArrow.price = price;
      indicatorArrow.arrow = d3ts.svg.arrow()
        .orient(arrow.down ? 'down' : 'up')
        .x(d => timeScale(arrow.time))
        .y(d => {
          const p = pane.yPrice(price);
          return arrow.down ? p - verticalArrowGap : p + verticalArrowGap;
        });
      pane.indicatorArrows.push(indicatorArrow);
    }

    if (cfg.axisLeft) {
      pane.yAxisLeft = d3.axisLeft(pane.yPrice).tickSizeOuter(0).tickFormat(d3.format(cf.valueFormat));
      if (cf.valueTicks) {
        pane.yAxisLeft.ticks(cf.valueTicks);
      } else {
        pane.yAxisLeft.ticks(OhlcvChartComponent.estimateNumberOfVerticalTicks(lv.pricePane.height));
      }
      pane.groupAxisLeft = pane.group.append('g').attr('class', 'y axis left');
    }
    if (cfg.axisRight) {
      pane.yAxisRight = d3.axisRight(pane.yPrice).tickSizeOuter(0).tickFormat(d3.format(cf.valueFormat));
      if (cf.valueTicks) {
        pane.yAxisRight.ticks(cf.valueTicks);
      } else {
        pane.yAxisRight.ticks(OhlcvChartComponent.estimateNumberOfVerticalTicks(lv.pricePane.height));
      }
      pane.groupAxisRight = pane.group.append('g').attr('class', 'y axis right')
        .attr('transform', `translate(${lh.content.width}, 0)`);
    }

    if (isCrossHair) {
      let crosshair = d3ts.plot.crosshair().xScale(timeScale).yScale(pane.yPrice).xAnnotation(timeAnnotationBottom)
        .verticalWireRange([0, lv.timeAxis.top]);
      if (cfg.axisLeft && cfg.axisRight) {
        const annotationLeft = d3ts.plot.axisannotation().axis(pane.yAxisLeft).orient('left')
          .format(d3.format(cf.valueFormat));
        const annotationRight = d3ts.plot.axisannotation().axis(pane.yAxisRight).orient('right')
          .format(d3.format(cf.valueFormat)).translate([timeScale(1), 0]);
        crosshair = crosshair.yAnnotation([annotationLeft, annotationRight]);
      } else
        if (cfg.axisLeft) {
          const annotationLeft = d3ts.plot.axisannotation().axis(pane.yAxisLeft).orient('left')
            .format(d3.format(cf.valueFormat));
          crosshair = crosshair.yAnnotation(annotationLeft);
        } else
          if (cfg.axisRight) {
            const annotationRight = d3ts.plot.axisannotation().axis(pane.yAxisRight).orient('right')
              .format(d3.format(cf.valueFormat)).translate([timeScale(1), 0]);
            crosshair = crosshair.yAnnotation(annotationRight);
          }
      pane.group.append('g').attr('class', 'crosshair').call(crosshair);
    }

    return pane;
  }

  private static createIndicatorPane(index: number, cfg: OhlcvChartConfig, lh: OhlcvChartComponent.HorizontalLayout,
    lv: OhlcvChartComponent.VerticalLayout, timeScale: any, timeAxisBottom: any, svg: any,
    isCrossHair: boolean): OhlcvChartComponent.IndicatorPane {

    const block = lv.indicatorPanes[index];
    const cf = cfg.indicatorPanes[index];
    const pane = new OhlcvChartComponent.IndicatorPane();
    pane.yValue = d3.scaleLinear().range([block.height, 0]);
    const factor = cf.valueMarginPercentageFactor;
    pane.yMarginFactorTop = 1 + factor;
    pane.yMarginFactorBottom = 1 - factor;

    const clip = `indicator-clip-${index}`;
    const clipUrl = `url(#${clip})`;
    pane.group = svg.append('g').attr('class', 'indicator-pane').attr('transform', `translate(${lh.content.left}, ${block.top})`);
    pane.group.append('clipPath').attr('id', clip).append('rect').attr('x', 0).attr('y', pane.yValue(1))
      .attr('width', lh.content.width).attr('height', pane.yValue(0) - pane.yValue(1));

    if (cf.heatmap) {
      const heatmap = cf.heatmap;
      const indicatorHeatmap = new OhlcvChartComponent.IndicatorHeatmap();
      indicatorHeatmap.path = pane.group.append('g').attr('class', `heatmap`).attr('clip-path', clipUrl);
      indicatorHeatmap.invertGradient = heatmap.invertGradient;
      indicatorHeatmap.gradient = OhlcvChartComponent.convertGradient(heatmap.gradient);
      indicatorHeatmap.data = heatmap.data;
      indicatorHeatmap.height = block.height;
      pane.indicatorHeatmap = indicatorHeatmap;
    }

    for (let i = 0; i < cf.bands.length; ++i) {
      const band = cf.bands[i];
      const indicatorBand = new OhlcvChartComponent.IndicatorBand();
      indicatorBand.path = pane.group.append('g').attr('class', `band-${i}`).attr('clip-path', clipUrl).append('path')
        .attr('fill', band.color);
      indicatorBand.area = d3.area()
        .curve(OhlcvChartComponent.convertInterpolation(band.interpolation))
        .defined(d => { const w: any = d; return !isNaN(w.lower) && !isNaN(w.upper); })
        .x(d => { const w: any = d; return timeScale(w.time); })
        .y0(d => { const w: any = d; return pane.yValue(w.lower); })
        .y1(d => { const w: any = d; return pane.yValue(w.upper); });
      indicatorBand.data = band.data;
      pane.indicatorBands.push(indicatorBand);
    }

    for (let i = 0; i < cf.lineAreas.length; ++i) {
      const lineArea = cf.lineAreas[i];
      const value = lineArea.value;
      const indicatorLineArea = new OhlcvChartComponent.IndicatorLineArea();
      indicatorLineArea.path = pane.group.append('g').attr('class', `linearea-${i}`).attr('clip-path', clipUrl).append('path')
        .attr('fill', lineArea.color);
      indicatorLineArea.area = d3.area()
        .curve(OhlcvChartComponent.convertInterpolation(lineArea.interpolation))
        .defined(d => { const w: any = d; return !isNaN(w.value); })
        .x(d => { const w: any = d; return timeScale(w.time); })
        .y0(d => { const w: any = d; return pane.yValue(w.value); })
        .y1(d => { const w: any = d; return pane.yValue(value); });
      indicatorLineArea.data = lineArea.data;
      indicatorLineArea.value = lineArea.value;
      pane.indicatorLineAreas.push(indicatorLineArea);
    }

    for (let i = 0; i < cf.horizontals.length; ++i) {
      const horizontal = cf.horizontals[i];
      const indicatorHorizontal = new OhlcvChartComponent.IndicatorHorizontal();
      indicatorHorizontal.path = pane.group.append('g').attr('class', `horizontal-${i}`).attr('clip-path', clipUrl).append('path')
        .attr('stroke', horizontal.color)
        .attr('stroke-width', horizontal.width)
        .attr('stroke-dasharray', horizontal.dash)
        .attr('stroke-linejoin', 'round')
        .attr('stroke-linecap', 'round')
        .attr('fill', 'none');
      const value = horizontal.value;
      indicatorHorizontal.value = value;
      indicatorHorizontal.data =
        [{ time: minDate, value: value }, { time: maxDate, value: value }];
      indicatorHorizontal.line = d3.line()
        .x(d => { const w: any = d; return timeScale(w.time); })
        .y(d => pane.yValue(value));
      pane.indicatorHorizontals.push(indicatorHorizontal);
    }

    for (let i = 0; i < cf.lines.length; ++i) {
      const line = cf.lines[i];
      const indicatorLine = new OhlcvChartComponent.IndicatorLine();
      indicatorLine.path = pane.group.append('g').attr('class', `line-${i}`).attr('clip-path', clipUrl).append('path')
        .attr('stroke', line.color)
        .attr('stroke-width', line.width)
        .attr('stroke-dasharray', line.dash)
        .attr('stroke-linejoin', 'round')
        .attr('stroke-linecap', 'round')
        .attr('fill', 'none');
      indicatorLine.line = d3.line()
        .curve(OhlcvChartComponent.convertInterpolation(line.interpolation))
        .defined(d => { const w: any = d; return !isNaN(w.value); })
        .x(d => { const w: any = d; return timeScale(w.time); })
        .y(d => { const w: any = d; return pane.yValue(w.value); });
      indicatorLine.data = line.data;
      pane.indicatorLines.push(indicatorLine);
    }

    if (cfg.axisLeft) {
      pane.yAxisLeft = d3.axisLeft(pane.yValue).tickSizeOuter(0).tickFormat(d3.format(cf.valueFormat));
      if (cf.valueTicks) {
        pane.yAxisLeft.ticks(cf.valueTicks);
      } else {
        pane.yAxisLeft.ticks(OhlcvChartComponent.estimateNumberOfVerticalTicks(block.height));
      }
      pane.groupAxisLeft = pane.group.append('g').attr('class', 'y axis left');
    }
    if (cfg.axisRight) {
      pane.yAxisRight = d3.axisRight(pane.yValue).tickSizeOuter(0).tickFormat(d3.format(cf.valueFormat));
      if (cf.valueTicks) {
        pane.yAxisRight.ticks(cf.valueTicks);
      } else {
        pane.yAxisRight.ticks(OhlcvChartComponent.estimateNumberOfVerticalTicks(block.height));
      }
      pane.groupAxisRight = pane.group.append('g').attr('class', 'y axis right')
        .attr('transform', `translate(${lh.content.width}, 0)`);
    }

    if (isCrossHair) {
      const delta = lv.timeAxis.top - block.top;
      const timeAnnotationBottom = d3ts.plot.axisannotation().axis(timeAxisBottom).orient('bottom')
        .width(65).translate([0, delta]);
      if (cfg.timeAnnotationFormat) {
        timeAnnotationBottom.format(d3.timeFormat(cfg.timeAnnotationFormat));
      }
      let crosshair = d3ts.plot.crosshair().xScale(timeScale).yScale(pane.yValue).xAnnotation(timeAnnotationBottom)
        .verticalWireRange([lv.pricePane.top - block.top, delta]);
      if (cfg.axisLeft && cfg.axisRight) {
        const annotationLeft = d3ts.plot.axisannotation().axis(pane.yAxisLeft).orient('left')
          .format(d3.format(cf.valueFormat));
        const annotationRight = d3ts.plot.axisannotation().axis(pane.yAxisRight).orient('right')
          .format(d3.format(cf.valueFormat)).translate([timeScale(1), 0]);
        crosshair = crosshair.yAnnotation([annotationLeft, annotationRight]);
      } else
        if (cfg.axisLeft) {
          const annotationLeft = d3ts.plot.axisannotation().axis(pane.yAxisLeft).orient('left')
            .format(d3.format(cf.valueFormat));
          crosshair = crosshair.yAnnotation(annotationLeft);
        } else
          if (cfg.axisRight) {
            const annotationRight = d3ts.plot.axisannotation().axis(pane.yAxisRight).orient('right')
              .format(d3.format(cf.valueFormat)).translate([timeScale(1), 0]);
            crosshair = crosshair.yAnnotation(annotationRight);
          }
      pane.group.append('g').attr('class', 'crosshair').call(crosshair);
    }

    return pane;
  }

  private static createNavPane(cfg: OhlcvChartConfig, lh: OhlcvChartComponent.HorizontalLayout,
    lv: OhlcvChartComponent.VerticalLayout, svg: any): OhlcvChartComponent.NavPane {

    const width = lh.content.width;
    const height = lv.navigationPane.height;
    const pane = new OhlcvChartComponent.NavPane();
    if (cfg.navigationPane !== undefined) {
      const nav = cfg.navigationPane;
      const heightWithoutTimeAxis = nav.hasTimeAxis ? height - timeAxisHeight : height;
      pane.timeScale = d3ts.scale.financetime().range([0, width]);
      pane.priceScale = d3.scaleLinear().range([heightWithoutTimeAxis, 0]);
      pane.brush = d3.brushX().extent([[0, 0], [width, heightWithoutTimeAxis]]);
      if (nav.hasArea) {
        pane.area = d3ts.plot.ohlcarea().xScale(pane.timeScale).yScale(pane.priceScale);
      }
      if (nav.hasLine) {
        pane.line = d3ts.plot.closeline().xScale(pane.timeScale).yScale(pane.priceScale);
      }
      if (nav.hasTimeAxis) {
        pane.timeAxis = d3.axisBottom(pane.timeScale).tickSizeOuter(0);
        if (nav.timeTicksFormat !== undefined) {
          pane.timeAxis.tickFormat(d3.timeFormat(nav.timeTicksFormat));
        }
        if (nav.timeTicks !== undefined) {
          pane.timeAxis.ticks(nav.timeTicks);
        } else {
          pane.timeAxis.ticks(lh.content.width / defaultWhitespaceBetweenTimeTicks);
        }
      }
      pane.group = svg.append('g').attr('class', 'nav').attr('transform', `translate(${lh.content.left}, ${lv.navigationPane.top})`)
        .attr('height', height);
      if (nav.hasArea) {
        pane.areaSelection = pane.group.append('g').attr('class', 'area').attr('height', heightWithoutTimeAxis);
      }
      if (nav.hasLine) {
        pane.lineSelection = pane.group.append('g').attr('class', 'line').attr('fill', 'none').attr('stroke-width', 0.5)
          .attr('height', heightWithoutTimeAxis);
      }
      pane.paneSelection = pane.group.append('g').attr('class', 'pane').attr('height', heightWithoutTimeAxis);
      if (nav.hasTimeAxis) {
        pane.timeAxisSelection = pane.group.append('g').attr('class', 'x axis').attr('height', timeAxisHeight)
          .attr('transform', `translate(0, ${heightWithoutTimeAxis})`);
      }
    }
    return pane;
  }

  private static createTimePane(cfg: OhlcvChartConfig, lh: OhlcvChartComponent.HorizontalLayout,
    lv: OhlcvChartComponent.VerticalLayout, svg: any): OhlcvChartComponent.TimePane {

    const pane = new OhlcvChartComponent.TimePane();
    pane.timeScale = d3ts.scale.financetime().range([0, lh.content.width]);
    pane.timeAxis = d3.axisBottom(pane.timeScale).tickSizeOuter(0);
    if (cfg.timeTicksFormat !== undefined) {
      pane.timeAxis.tickFormat(d3.timeFormat(cfg.timeTicksFormat));
    }
    if (cfg.timeTicks !== undefined) {
      pane.timeAxis.ticks(cfg.timeTicks);
    } else {
      pane.timeAxis.ticks(lh.content.width / defaultWhitespaceBetweenTimeTicks);
    }
    pane.timeAnnotation = d3ts.plot.axisannotation().axis(pane.timeAxis).orient('bottom')
      .width(65).translate([0, lv.timeAxis.top - lv.pricePane.top]);
    if (cfg.timeAnnotationFormat !== undefined) {
      pane.timeAnnotation.format(d3.timeFormat(cfg.timeAnnotationFormat));
    }

    pane.group = svg.append('g').attr('class', 'x axis').attr('height', lv.timeAxis.height)
      .attr('transform', `translate(${lh.content.left}, ${lv.timeAxis.top})`);

    return pane;
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

  private static findOhlcv(data: Ohlcv[], time: Date): Ohlcv | null {
    for (let i = 0; i < data.length; ++i) {
      const d = data[i];
      if (+d.time >= +time) {
        return d;
      }
    }
    return null;
  }

  private static getArrowPrice(data: Ohlcv[], arrow: OhlcvChartConfig.ArrowData): number {
    if (arrow.value) {
      return +arrow.value;
    }
    const ohlcv = OhlcvChartComponent.findOhlcv(data, arrow.time);
    if (ohlcv != null) {
      return arrow.down ? ohlcv.high : ohlcv.low;
    }
    return 0;
  }

  private static ramp(color: any, invertGradient: boolean, width: number, height: number): HTMLCanvasElement {
    const canvas = document.createElement('canvas');
    canvas.width = width;
    canvas.height = height;
    canvas.style.width = width + 'px';
    canvas.style.height = height + 'px';
    canvas.style.imageRendering = 'pixelated';
    const context = canvas.getContext('2d');
    const k = 1 / (width - 1);
    for (let i = 0; i < width; ++i) {
      context.fillStyle = color(invertGradient ? (1 - i * k) : (i * k));
      context.fillRect(i, 0, 1, height);
    }
    return canvas;
  }

  private static convertGradient(gradient: string): any {
    switch (gradient.toLowerCase()) {
      case 'viridis': return d3.interpolateViridis;
      case 'inferno': return d3.interpolateInferno;
      case 'magma': return d3.interpolateMagma;
      case 'plasma': return d3.interpolatePlasma;
      case 'warm': return d3.interpolateWarm;
      case 'cool': return d3.interpolateCool;
      case 'rainbow': return d3.interpolateRainbow;
      case 'cubehelixdefault': return d3.interpolateCubehelixDefault;
      case 'bugn': return d3.interpolateBuGn;
      case 'bupu': return d3.interpolateBuPu;
      case 'gnbu': return d3.interpolateGnBu;
      case 'orrd': return d3.interpolateOrRd;
      case 'pubugn': return d3.interpolatePuBuGn;
      case 'pubu': return d3.interpolatePuBu;
      case 'purd': return d3.interpolatePuRd;
      case 'rdpu': return d3.interpolateRdPu;
      case 'ylgnbu': return d3.interpolateYlGnBu;
      case 'ylgn': return d3.interpolateYlGn;
      case 'ylorbr': return d3.interpolateYlOrBr;
      case 'ylorrd': return d3.interpolateYlOrRd;
      case 'blues': return d3.interpolateBlues;
      case 'greens': return d3.interpolateGreens;
      case 'greys': return d3.interpolateGreys;
      case 'oranges': return d3.interpolateOranges;
      case 'purples': return d3.interpolatePurples;
      case 'reds': return d3.interpolateReds;
      default: return d3.interpolateGreys;
    }
  }
}

export namespace OhlcvChartComponent {
  export class HorizontalLayoutBlock {
    public left = 0;
    public width = 0;
  }

  export class HorizontalLayout {
    public width = 0;
    public chart: HorizontalLayoutBlock = { left: 0, width: 0 };
    public content: HorizontalLayoutBlock = { left: 0, width: 0 };
  }

  export class VerticalLayoutBlock {
    public top = 0;
    public height = 0;
  }

  export class VerticalLayout {
    public height = 0;
    public pricePane: VerticalLayoutBlock = { top: 0, height: 0 };
    public indicatorPanes: VerticalLayoutBlock[] = [];
    public timeAxis: VerticalLayoutBlock = { top: 0, height: 0 };
    public navigationPane: VerticalLayoutBlock = { top: 0, height: 0 };
  }

  export class PricePane {
    group: any;
    groupPrice: any;
    groupVolume: any;
    groupAxisLeft: any;
    groupAxisRight: any;
    yPrice: d3.ScaleLinear<number, number>;
    yVolume: d3.ScaleLinear<number, number>;
    yMarginFactorTop: number;
    yMarginFactorBottom: number;
    yAxisLeft: any;
    yAxisRight: any;
    priceShape: any;
    priceAccessor: any;
    volume: any;
    indicatorBands: OhlcvChartComponent.IndicatorBand[] = [];
    indicatorLineAreas: OhlcvChartComponent.IndicatorLineArea[] = [];
    indicatorHorizontals: OhlcvChartComponent.IndicatorHorizontal[] = [];
    indicatorLines: OhlcvChartComponent.IndicatorLine[] = [];
    indicatorArrows: OhlcvChartComponent.IndicatorArrow[] = [];

    public draw(timePane: TimePane): void {
      const datum = this.groupPrice.datum();
      const datumLastIndex = datum.length - 1;
      const timeDomain: [number, number] = timePane.timeScale.zoomable().domain();
      let min = Math.round(timeDomain[0]);
      let max = Math.round(timeDomain[1]);
      if (min < 0) {
        min = 0;
      }
      if (max > datumLastIndex) {
        max = datumLastIndex;
      }
      const priceDomain: [number, number] = d3ts.scale.plot.ohlc(datum.slice.apply(datum, [min, max]), this.priceAccessor).domain();
      if (datum[min] != undefined) {
        min = +datum[min].time;
      }
      if (datum[max] != undefined) {
        max = +datum[max].time;
      }
      let minPrice = priceDomain[0];
      let maxPrice = priceDomain[1];
      for (let i = 0; i < this.indicatorBands.length; ++i) {
        const data = this.indicatorBands[i].data;
        for (let j = 0; j < data.length; ++j) {
          const d = data[j];
          const t = +d.time;
          if (min <= t && t <= max) {
            if (minPrice > d.lower) {
              minPrice = d.lower;
            }
            if (maxPrice < d.upper) {
              maxPrice = d.upper;
            }
          }
        }
      }
      for (let i = 0; i < this.indicatorLineAreas.length; ++i) {
        const data = this.indicatorLineAreas[i].data;
        const value = this.indicatorLineAreas[i].value;
        for (let j = 0; j < data.length; ++j) {
          const d = data[j];
          const t = +d.time;
          if (min <= t && t <= max) {
            if (minPrice > d.value) {
              minPrice = d.value;
            }
            if (maxPrice < d.value) {
              maxPrice = d.value;
            }
            if (minPrice > value) {
              minPrice = value;
            }
            if (maxPrice < value) {
              maxPrice = value;
            }
          }
        }
      }
      for (let i = 0; i < this.indicatorHorizontals.length; ++i) {
        const value = this.indicatorHorizontals[i].value;
        if (minPrice > value) {
          minPrice = value;
        }
        if (maxPrice < value) {
          maxPrice = value;
        }
      }
      for (let i = 0; i < this.indicatorLines.length; ++i) {
        const data = this.indicatorLines[i].data;
        for (let j = 0; j < data.length; ++j) {
          const d = data[j];
          const t = +d.time;
          if (min <= t && t <= max) {
            const value = d.value;
            if (minPrice > value) {
              minPrice = value;
            }
            if (maxPrice < value) {
              maxPrice = value;
            }
          }
        }
      }
      let arrowWidth;
      let arrowHeight;
      if (this.indicatorArrows.length > 0) {
        const slotWidth = this.priceShape.width()(timePane.timeScale);
        arrowWidth = slotWidth < minArrowWidth ? minArrowWidth : slotWidth;
        arrowHeight = arrowWidth * arowHeightToWidthRatio;
        const arrowDelta = arrowHeight + verticalArrowGap;
        const h = this.yPrice.range()[0];
        if (arrowDelta < h) {
          const delta = h - arrowDelta;
          for (let i = 0; i < this.indicatorArrows.length; ++i) {
            const indicatorArrow = this.indicatorArrows[i];
            const p = indicatorArrow.price;
            const ph = p * h;
            if (maxPrice < p) {
              maxPrice = p;
            }
            if (minPrice > p) {
              minPrice = p;
            }
            if (indicatorArrow.isDown) {
              const maxNew = (ph - minPrice * arrowDelta) / delta;
              if (maxPrice < maxNew) {
                maxPrice = maxNew;
              }
            } else {
              const minNew = (ph - maxPrice * arrowDelta) / delta;
              if (minPrice > minNew) {
                minPrice = minNew;
              }
              if (minPrice < 0) {
                minPrice = 0;
              }
            }
          }
        }
      }
      minPrice *= this.yMarginFactorBottom;
      maxPrice *= this.yMarginFactorTop;
      this.yPrice.domain([minPrice, maxPrice]).nice();

      // Draw bands and areas below price and lines.
      for (let i = 0; i < this.indicatorBands.length; ++i) {
        const indicatorBand = this.indicatorBands[i];
        indicatorBand.path.attr('d', indicatorBand.area);
      }
      for (let i = 0; i < this.indicatorLineAreas.length; ++i) {
        const indicatorLineArea = this.indicatorLineAreas[i];
        indicatorLineArea.path.attr('d', indicatorLineArea.area);
      }

      // Draw horizontals above bands and areas but below price and lines.
      this.groupPrice.call(this.priceShape);
      if (this.volume) {
        this.groupVolume.call(this.volume);
      }

      for (let i = 0; i < this.indicatorHorizontals.length; ++i) {
        const indicatorHorizontal = this.indicatorHorizontals[i];
        indicatorHorizontal.path.attr('d', indicatorHorizontal.line);
      }
      for (let i = 0; i < this.indicatorLines.length; ++i) {
        const indicatorLine = this.indicatorLines[i];
        indicatorLine.path.attr('d', indicatorLine.line);
      }
      for (let i = 0; i < this.indicatorArrows.length; ++i) {
        const indicatorArrow = this.indicatorArrows[i];
        indicatorArrow.arrow.width(arrowWidth).height(arrowHeight);
        indicatorArrow.path.attr('d', indicatorArrow.arrow);
      }

      if (this.yAxisLeft) {
        this.groupAxisLeft.call(this.yAxisLeft);
      }
      if (this.yAxisRight) {
        this.groupAxisRight.call(this.yAxisRight);
      }
    }

    public setIndicatorDatum(): void {
      for (let i = 0; i < this.indicatorBands.length; ++i) {
        const indicatorBand = this.indicatorBands[i];
        indicatorBand.path.datum(indicatorBand.data);
      }
      for (let i = 0; i < this.indicatorLineAreas.length; ++i) {
        const indicatorLineArea = this.indicatorLineAreas[i];
        indicatorLineArea.path.datum(indicatorLineArea.data);
      }
      for (let i = 0; i < this.indicatorHorizontals.length; ++i) {
        const indicatorHorizontal = this.indicatorHorizontals[i];
        indicatorHorizontal.path.datum(indicatorHorizontal.data);
      }
      for (let i = 0; i < this.indicatorLines.length; ++i) {
        const indicatorLine = this.indicatorLines[i];
        indicatorLine.path.datum(indicatorLine.data);
      }
    }
  }

  export class IndicatorPane {
    group: any;
    groupAxisLeft: any;
    groupAxisRight: any;
    yValue: d3.ScaleLinear<number, number>;
    yMarginFactorTop: number;
    yMarginFactorBottom: number;
    yAxisLeft: any;
    yAxisRight: any;
    indicatorHeatmap?: OhlcvChartComponent.IndicatorHeatmap;
    indicatorBands: OhlcvChartComponent.IndicatorBand[] = [];
    indicatorLineAreas: OhlcvChartComponent.IndicatorLineArea[] = [];
    indicatorHorizontals: OhlcvChartComponent.IndicatorHorizontal[] = [];
    indicatorLines: OhlcvChartComponent.IndicatorLine[] = [];

    public draw(timePane: TimePane, pricePane: PricePane): void {
      const datum = pricePane.groupPrice.datum();
      const datumLastIndex = datum.length - 1;
      const timeDomain: [number, number] = timePane.timeScale.zoomable().domain();
      let min = Math.round(timeDomain[0]);
      let max = Math.round(timeDomain[1]);
      if (min < 0) {
        min = 0;
      }
      if (max > datumLastIndex) {
        max = datumLastIndex;
      }
      if (datum[min] != undefined) {
        min = +datum[min].time;
      }
      if (datum[max] != undefined) {
        max = +datum[max].time;
      }
      let minValue = Number.MAX_VALUE;
      let maxValue = Number.MIN_VALUE;
      let minIntensity = Number.MAX_VALUE;
      let maxIntensity = Number.MIN_VALUE;
      if (this.indicatorHeatmap) {
        const data = this.indicatorHeatmap.data;
        if (data.length > 0) {
          const paramFirst = data[0].parameterFirst;
          const paramLast = data[0].parameterLast;
          minValue = Math.min(paramFirst, paramLast);
          maxValue = Math.max(paramFirst, paramLast);
          for (let j = 0; j < data.length; ++j) {
            const d = data[j];
            const t = +d.time;
            if (min <= t && t <= max && d.values.length > 0) {
              if (minIntensity > d.valueMin) {
                minIntensity = d.valueMin;
              }
              if (maxIntensity < d.valueMax) {
                maxIntensity = d.valueMax;
              }
            }
          }
          if (maxIntensity === Number.MIN_VALUE || minIntensity === Number.MAX_VALUE) {
            minIntensity = 0;
            maxIntensity = 0;
          }
        }
      }
      for (let i = 0; i < this.indicatorBands.length; ++i) {
        const data = this.indicatorBands[i].data;
        for (let j = 0; j < data.length; ++j) {
          const d = data[j];
          const t = +d.time;
          if (min <= t && t <= max) {
            if (minValue > d.lower) {
              minValue = d.lower;
            }
            if (maxValue < d.upper) {
              maxValue = d.upper;
            }
          }
        }
      }
      for (let i = 0; i < this.indicatorLineAreas.length; ++i) {
        const data = this.indicatorLineAreas[i].data;
        const value = this.indicatorLineAreas[i].value;
        for (let j = 0; j < data.length; ++j) {
          const d = data[j];
          const t = +d.time;
          if (min <= t && t <= max) {
            if (minValue > d.value) {
              minValue = d.value;
            }
            if (maxValue < d.value) {
              maxValue = d.value;
            }
            if (minValue > value) {
              minValue = value;
            }
            if (maxValue < value) {
              maxValue = value;
            }
          }
        }
      }
      for (let i = 0; i < this.indicatorHorizontals.length; ++i) {
        const value = this.indicatorHorizontals[i].value;
        if (minValue > value) {
          minValue = value;
        }
        if (maxValue < value) {
          maxValue = value;
        }
      }
      for (let i = 0; i < this.indicatorLines.length; ++i) {
        const data = this.indicatorLines[i].data;
        for (let j = 0; j < data.length; ++j) {
          const d = data[j];
          const t = +d.time;
          if (min <= t && t <= max) {
            const value = d.value;
            if (minValue > value) {
              minValue = value;
            }
            if (maxValue < value) {
              maxValue = value;
            }
          }
        }
      }
      if (this.indicatorHeatmap) {
        this.yValue.domain([minValue, maxValue]).nice();
      } else {
        minValue *= this.yMarginFactorBottom;
        maxValue *= this.yMarginFactorTop;
        this.yValue.domain([minValue, maxValue]).nice();
      }

      if (this.indicatorHeatmap) {
        this.indicatorHeatmap.path.selectAll('image').remove();
        const data = this.indicatorHeatmap.data;
        if (data.length > 0) {
          const slotWidth = 1 + (data.length > 1 ?
            timePane.timeScale(data[1].time) - timePane.timeScale(data[0].time) :
            pricePane.priceShape.width()(timePane.timeScale));
          const h = this.indicatorHeatmap.height;
          const gradient = this.indicatorHeatmap.gradient;
          const invertGradient = this.indicatorHeatmap.invertGradient;
          const periodFirst = data[0].parameterFirst;
          const periodLast = data[0].parameterLast;
          const periodRes = data[0].parameterResolution;
          const periodInverted = periodFirst > periodLast;
          const periodMin = Math.min(periodFirst, periodLast);
          for (let j = 0; j < data.length; ++j) {
            const d = data[j];
            const t = +d.time;
            if (min <= t && t <= max && d.values.length > 0) {
              const xMid = timePane.timeScale(t);
              const xMin = xMid - slotWidth / 2;
              const img = this.heatColumn(d, periodMin, periodRes, periodInverted, d.valueMin, d.valueMax,
                gradient, invertGradient, slotWidth, h);
              this.indicatorHeatmap.path.append('image').attr('x', xMin).attr('width', slotWidth)
                .attr('y', 0).attr('height', h).attr('preserveAspectRatio', 'none')
                .attr('xlink:href', img.toDataURL());
            }
          }
        }
      }

      // Draw bands and areas below lines.
      for (let i = 0; i < this.indicatorBands.length; ++i) {
        const indicatorBand = this.indicatorBands[i];
        indicatorBand.path.attr('d', indicatorBand.area);
      }
      for (let i = 0; i < this.indicatorLineAreas.length; ++i) {
        const indicatorLineArea = this.indicatorLineAreas[i];
        indicatorLineArea.path.attr('d', indicatorLineArea.area);
      }

      // Draw horizontals above bands and areas but below lines.
      for (let i = 0; i < this.indicatorHorizontals.length; ++i) {
        const indicatorHorizontal = this.indicatorHorizontals[i];
        indicatorHorizontal.path.attr('d', indicatorHorizontal.line);
      }
      for (let i = 0; i < this.indicatorLines.length; ++i) {
        const indicatorLine = this.indicatorLines[i];
        indicatorLine.path.attr('d', indicatorLine.line);
      }

      if (this.yAxisLeft) {
        this.groupAxisLeft.call(this.yAxisLeft);
      }
      if (this.yAxisRight) {
        this.groupAxisRight.call(this.yAxisRight);
      }
    }

    public setIndicatorDatum(): void {
      for (let i = 0; i < this.indicatorBands.length; ++i) {
        const indicatorBand = this.indicatorBands[i];
        indicatorBand.path.datum(indicatorBand.data);
      }
      for (let i = 0; i < this.indicatorLineAreas.length; ++i) {
        const indicatorLineArea = this.indicatorLineAreas[i];
        indicatorLineArea.path.datum(indicatorLineArea.data);
      }
      for (let i = 0; i < this.indicatorHorizontals.length; ++i) {
        const indicatorHorizontal = this.indicatorHorizontals[i];
        indicatorHorizontal.path.datum(indicatorHorizontal.data);
      }
      for (let i = 0; i < this.indicatorLines.length; ++i) {
        const indicatorLine = this.indicatorLines[i];
        indicatorLine.path.datum(indicatorLine.data);
      }
    }

    private heatColumn(heatmap: Heatmap, periodMin: number, periodRes: number, periodInverted: boolean, min: number, max: number,
      color: any, invertColor: boolean, width: number, height: number): HTMLCanvasElement {
      const canvas = document.createElement('canvas');
      canvas.width = width;
      canvas.height = height;
      canvas.style.width = width + 'px';
      canvas.style.height = height + 'px';
      canvas.style.imageRendering = 'pixelated';
      const context = canvas.getContext('2d');
      const y = this.yValue;
      const heat = heatmap.values;
      if (min !== 0 && max !== 1) {
        const delta = max - min;
        for (let i = 0; i < height; ++i) {
          const index = Math.round((y.invert(i) - periodMin) * periodRes);
          const value = (heat[index] - min) / delta;
          context.fillStyle = color(invertColor ? 1 - value : value);
          context.fillRect(0, periodInverted ? height - i : i, width, 1);
        }
      } else {
        for (let i = 0; i < height; ++i) {
          const index = Math.round((y.invert(i) - periodMin) * periodRes);
          const value = heat[index];
          context.fillStyle = color(invertColor ? 1 - value : value);
          context.fillRect(0, periodInverted ? height - i : i, width, 1);
        }
      }
      return canvas;
    }
  }

  export class NavPane {
    group: any;
    priceScale: d3.ScaleLinear<number, number>;
    timeScale: any;
    brush: any;
    area: any;
    line: any;
    timeAxis: any;
    lineSelection: any;
    timeAxisSelection: any;
    areaSelection: any;
    paneSelection: any;
  }

  export class TimePane {
    group: any;
    timeScale: any;
    timeAxis: any;
    timeAnnotation: any;

    public draw(): void {
      this.group.call(this.timeAxis);
    }
  }

  export class IndicatorLine {
    data: Scalar[];
    line: any;
    path: any;
  }

  export class IndicatorLineArea {
    data: Scalar[];
    value: number;
    area: any;
    path: any;
  }

  export class IndicatorBand {
    data: Band[];
    area: any;
    path: any;
  }

  export class IndicatorHorizontal {
    data: Scalar[];
    value: number;
    line: any;
    path: any;
  }

  export class IndicatorArrow {
    arrow: any;
    path: any;
    isDown: boolean;
    price: number;
  }

  export class IndicatorHeatmap {
    data: Heatmap[];
    gradient: any;
    invertGradient: boolean;
    path: any;
    height: number;
  }
}
