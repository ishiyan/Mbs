import { Component, OnInit, ElementRef, ViewChild, Input, HostListener, ViewEncapsulation } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';
import * as d3 from 'd3';
import * as d3ts from '../../../../shared/d3ts';

import { Ohlcv } from './ohlcv';
import { Scalar } from './scalar';
import { Band } from './band';
//import { dataOhlcvDaily } from './data-ohlcv-daily';
import { dataTestOhlcv } from './data-test-ohlcv';
import { dataTestBb } from './data-test-bb';
import { dataTestMa } from './data-test-ma';
import { dataTestLo } from './data-test-lo';
import { dataTestUp } from './data-test-up';
import { dataTestPercentB } from './data-test-percentb';
import { dataTestBw } from './data-test-bw';
import { OhlcvChartConfig } from './ohlcv-chart-config';
import { Downloader } from './downloader';
import { curveStep } from 'd3';

const ohlcvViewCandlesticks = 0;
const ohlcvViewBars = 1;

/** The width of a vertical axis in pixels including annotation. */
const verticalAxisWidth: number = 30;

@Component({
    selector: 'app-ohlcv-chart',
    templateUrl: './ohlcv-chart.component.html',
    styleUrls: ['./ohlcv-chart.component.scss']//,
    //encapsulation: ViewEncapsulation.Emulated
})
export class OhlcvChartComponent implements OnInit {
    @ViewChild('container', { static: true }) container: ElementRef;
    @Input() svgheight: any;
  
    private config: OhlcvChartConfig = {
        width: '100%',
        heightNavigationPane: 30,
        timeFormat: '%Y-%m-%d',
        axisLeft: true,
        axisRight: true,
        margin: {left: 0, top: 0, right: 0, bottom: 0},
        ohlcv: {name: 'BRILL@XAMS', data: dataTestOhlcv, candlesticks: false},
        pricePane: {
            height: '30%', valueFormat: ',.2f', valueMarginPercentageFactor: 0.01,
            bands: [
                {name: 'bb(stdev.p(20,c),2,sma(20,c))', data: dataTestBb, indicator: 0, output: 0, color: '#00ff004f', legendColor: '#00ff00', interpolation: 'natural'},
            ], lineAreas: [], horizontals: [], lines: [
                {name: 'sma(20,c)', data: dataTestMa, indicator: 0, output: 0, color: 'red', width: 1, dash: '', interpolation: 'natural'}
                //{name: 'lo-bb(stdev.p(20,c),2,sma(20,c))', data: dataTestLo, indicator: 0, output: 0, color: 'blue', width: 0.5, dash: '', interpolation: 'cardinal'},
                //{name: 'up-bb(stdev.p(20,c),2,sma(20,c))', data: dataTestUp, indicator: 0, output: 0, color: 'blue', width: 0.5, dash: '', interpolation: 'linear'}
            ]},
        indicatorPanes: [
            {height: '60', valueFormat: ',.2f', valueTicks: 5, valueMarginPercentageFactor: 0.01,
                bands: [], lineAreas: [], horizontals: [
                    {value: 0, color: 'red', width: 0.5, dash: ''},
                    {value: 1, color: 'red', width: 0.5, dash: ''}
                ], lines: [
                    {name: '%b(c)-bb(stdev.p(20,c),2,sma(20,c))', data: dataTestPercentB, indicator: 0, output: 0, color: 'green', width: 1, dash: '', interpolation: 'natural'}
                ]},
            {height: '60', valueFormat: ',.2f', valueTicks: 3, valueMarginPercentageFactor: 0.01,
                bands: [], lineAreas: [
                    {name: 'bw(c)-bb(stdev.p(20,c),2,sma(20,c))', data: dataTestBw, indicator: 0, output: 0, color: '#0000ff4f', legendColor: '#0000ff', value: 0.3, interpolation: 'step'}
                ], horizontals: [
                ], lines: [
                    {name: 'bw(c)-bb(stdev.p(20,c),2,sma(20,c))', data: dataTestBw, indicator: 0, output: 0, color: 'blue', width: 1, dash: '', interpolation: 'natural'}
                ]}            
        ],
        crosshair: true,
        volumeInPricePane: true
    }

    private static minDate = new Date(-8640000000000000);
    private static maxDate = new Date(8640000000000000);

    readonly ohlcvViewCandlesticks = ohlcvViewCandlesticks;
    readonly ohlcvViewBars = ohlcvViewBars;
    private ohlcvView: number = this.config.ohlcv.candlesticks ? this.ohlcvViewCandlesticks : this.ohlcvViewBars;
    get ohlcvViewType(): number {
      return this.ohlcvView;
    }
    set ohlcvViewType(value: number) {
      this.ohlcvView = value;
      this.render();
    }
 
    private renderCrosshair = this.config.crosshair;
    get viewCrosshair() {
        return this.renderCrosshair;
    }
    set viewCrosshair(value: boolean) {
        this.renderCrosshair = value;
        this.render();
    }

    private renderVolume = this.config.volumeInPricePane;
    get viewVolume() {
      return this.renderVolume;
    }
    set viewVolume(value: boolean) {
      this.renderVolume = value;
      this.render();
    }

    get chartTitle(): string {
        return this.config.ohlcv.name;
    }

    constructor(private element: ElementRef, iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
        iconRegistry.addSvgIcon('mb-candlesticks',
          sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-candlesticks.svg'));
        iconRegistry.addSvgIcon('mb-bars',
          sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-bars.svg'));
    }

    private currentSelection: any = null;

    @HostListener('window:resize', [])
    render() {
        const chartId = '#chart';
        //console.log('width=' + this.container.nativeElement.getBoundingClientRect().width);
        //console.log('offsetWidth=' + this.container.nativeElement.offsetWidth);
        //const w = this.container.nativeElement.getBoundingClientRect().width;
        const w = this.container.nativeElement.offsetWidth;
        const cfg = this.config;
        const lh = OhlcvChartComponent.layoutHorizontal(cfg, w);

        d3.select(chartId).select('svg').remove();
        const svg: any = d3.select(chartId).append('svg')
          .attr('preserveAspectRatio', 'xMinYMin meet').attr('width', lh.width);

        const lv = OhlcvChartComponent.layoutVertical(svg, cfg, lh);
        svg.attr('height', lv.height).attr("viewBox", `0 0 ${lh.width} ${lv.height}`);

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
            }
        }

        function brushing(): void {
            if (d3.event.selection) {
                const sel = d3.event.selection;
                if (sel[1] - sel[0] > 10) {
                    setCurrentSelection(sel);
                    const zoomable = timePane.timeScale.zoomable();
                    const zoomableNav = navPane.timeScale.zoomable();
                    zoomable.domain(zoomableNav.domain());
                    zoomable.domain(d3.event.selection.map(zoomable.invert));
                    draw();
                }
            }
        }

        navPane.brush.on('brush', brushing);
        navPane.brush.on('end', brushed);

        // data begin ----------------------------------
        timePane.timeScale.domain(cfg.ohlcv.data.map(pricePane.priceAccessor.t));
        //console.log(OhlcvChartComponent.firstTime(cfg));
        //console.log(OhlcvChartComponent.lastTime(cfg));
        //timePane.timeScale.domain([OhlcvChartComponent.firstTime(cfg), OhlcvChartComponent.lastTime(cfg)]);
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

        navPane.areaSelection.datum(cfg.ohlcv.data).call(navPane.area);

        // Associate the brush with the scale and render the brush only AFTER a domain has been applied
        navPane.paneSelection.call(navPane.brush).selectAll('rect').attr('height', lv.navigationPane.height);

        if (this.currentSelection != null) {
            navPane.brush.move(navPane.paneSelection, this.currentSelection);
            const zoomable = timePane.timeScale.zoomable();
            const zoomableNav = navPane.timeScale.zoomable();
            zoomable.domain(zoomableNav.domain());
            zoomable.domain(this.currentSelection.map(zoomable.invert));
        }
        draw();
    }

    ngOnInit() {
        //console.log(this.configLayout);
        //this.render();
        setTimeout(() => this.render(), 0);
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

    private static layoutHorizontal(cfg: OhlcvChartConfig, referenceWidth: number): OhlcvChartComponent.HorizontalLayout {
        const totalWidth: number = OhlcvChartComponent.valueToPixels(cfg.width, referenceWidth);

        const chartLeft: number = cfg.margin.left;
        const chartWidth: number = totalWidth - chartLeft - cfg.margin.right;

        const contentLeft: number = cfg.axisLeft ? OhlcvChartConfig.verticalAxisWidth : 0;
        const contentWidth: number = chartWidth - contentLeft -  (cfg.axisRight ? OhlcvChartConfig.verticalAxisWidth : 0);

        return {width: totalWidth, chart: {left: chartLeft, width: chartWidth}, content: {left: contentLeft, width: contentWidth}};
    }

    private static layoutVertical(svg: any, cfg: OhlcvChartConfig, lh: OhlcvChartComponent.HorizontalLayout): OhlcvChartComponent.VerticalLayout {
        const t = svg.append('text').text(`w`);
        const lineHeight = t.node().getBoundingClientRect().height;
        t.remove();
        
        const heightPricePaneLegend: number = OhlcvChartComponent.appendLegend(svg, cfg.margin.top, lineHeight, lh.content.left,
            lh.content.width, cfg.pricePane, cfg.ohlcv.name);
        const l = new OhlcvChartComponent.VerticalLayout();
        l.pricePane.top = cfg.margin.top + heightPricePaneLegend;
        l.pricePane.height = OhlcvChartComponent.valueToPixels(cfg.pricePane.height, lh.content.width);

        let top = l.pricePane.top + l.pricePane.height;        
        if (cfg.indicatorPanes && cfg.indicatorPanes.length) {
            for (let i = 0; i < cfg.indicatorPanes.length; ++i) {
                const pane = cfg.indicatorPanes[i];
                const block = new  OhlcvChartComponent.VerticalLayoutBlock();
                const legendHeight: number = OhlcvChartComponent.appendLegend(svg, top, lineHeight, lh.content.left,
                    lh.content.width, pane, undefined);
                block.top = top + legendHeight;
                block.height = OhlcvChartComponent.valueToPixels(pane.height, lh.content.width);
                l.indicatorPanes.push(block);
                top = block.top + block.height;
            }
        }

        l.timeAxis.top = top;
        l.timeAxis.height = 32;
        top += l.timeAxis.height;

        if (cfg.heightNavigationPane) {
            l.navigationPane.top = top;
            l.navigationPane.height = OhlcvChartComponent.valueToPixels(cfg.heightNavigationPane, lh.content.width);
            top += l.navigationPane.height;
        }

        l.height = top + cfg.margin.bottom;
        return l;
    }

    private static appendLegend(g: any, top: number, lineHeight: number, left: number, width: number,
        pane: OhlcvChartConfig.Pane, instrument: string = ''): number {
        g = g.append('g').attr('class', 'legend');
        const whitespaceBetweenAxisAndLegend = 8;

        top += lineHeight / 2;
        left += whitespaceBetweenAxisAndLegend;
        let l = left;
        let height = 0;

        if (instrument && instrument.length > 0) {
            const t = g.append('text')
                .attr('font-size', '10px')
                .attr('font-family', 'sans-serif')
                .attr('x', l)
                .attr('y', top)
                .text(` ${instrument} `);
            const r = t.node().getBoundingClientRect();
            l += r.width + whitespaceBetweenAxisAndLegend;
            height = r.height;
        }

        for (let i = 0; i < pane.bands.length; ++i) {
            const band = pane.bands[i];
            const t = g.append('text')
                .attr('font-size', '10px')
                .attr('font-family', 'sans-serif')
                .style('fill', band.legendColor ? band.legendColor : band.color)
                .attr("x", l)
                .attr("y", top)
                .text(' ◼ ' + band.name);
            const r = t.node().getBoundingClientRect();
            if (height === 0) {
                height = r.height;
            }
            const w = r.width;
            if (l + w > width) {
                l = left;
                top += r.height;
                height += r.height;
                t.attr("x", l).attr("y", top);
            }
            l += w + whitespaceBetweenAxisAndLegend;
        }

        for (let i = 0; i < pane.lineAreas.length; ++i) {
            const lineArea = pane.lineAreas[i];
            const t = g.append('text')
                .attr('font-size', '10px')
                .attr('font-family', 'sans-serif')
                .style('fill', lineArea.legendColor ? lineArea.legendColor : lineArea.color)
                .attr("x", l)
                .attr("y", top)
                .text(' ◼ ' + lineArea.name);
            const r = t.node().getBoundingClientRect();
            if (height === 0) {
                height = r.height;
            }
            const w = r.width;
            if (l + w > width) {
                l = left;
                top += r.height;
                height += r.height;
                t.attr("x", l).attr("y", top);
            }
            l += w + whitespaceBetweenAxisAndLegend;
        }

        for (let i = 0; i < pane.lines.length; ++i) {
            const line = pane.lines[i];
            const t = g.append('text')
                .attr('font-size', '10px')
                .attr('font-family', 'sans-serif')
                .style('fill', line.color)
                .attr("x", l)
                .attr("y", top)
                .text(' ― ' + line.name);
            const r = t.node().getBoundingClientRect();
            if (height === 0) {
                height = r.height;
            }
            const w = r.width;
            if (l + w > width) {
                l = left;
                top += r.height;
                height += r.height;
                t.attr("x", l).attr("y", top);
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
            if (time > t)
                time = t;
        }
        for (let i = 0; i < cfg.pricePane.lines.length; ++i) {
            const data = cfg.pricePane.lines[i].data;
            const t = data[0].time;
            if (time > t)
                time = t;
        }
        for (let j = 0; j < cfg.indicatorPanes.length; ++j) {
            const pane = cfg.indicatorPanes[j];
            for (let i = 0; i < pane.bands.length; ++i) {
                const data = pane.bands[i].data;
                const t = data[0].time;
                if (time > t)
                    time = t;
            }
            for (let i = 0; i < pane.lines.length; ++i) {
                const data = pane.lines[i].data;
                const t = data[0].time;
                if (time > t)
                    time = t;
            }
        }
        return time;
    }

    private static lastTime(cfg: OhlcvChartConfig): Date {
        let time = cfg.ohlcv.data[cfg.ohlcv.data.length - 1].time;
        for (let i = 0; i < cfg.pricePane.bands.length; ++i) {
            const data = cfg.pricePane.bands[i].data;
            const t = data[data.length - 1].time;
            if (time < t)
                time = t;
        }
        for (let i = 0; i < cfg.pricePane.lines.length; ++i) {
            const data = cfg.pricePane.lines[i].data;
            const t = data[data.length - 1].time;
            if (time < t)
                time = t;
        }
        for (let j = 0; j < cfg.indicatorPanes.length; ++j) {
            const pane = cfg.indicatorPanes[j];
            for (let i = 0; i < pane.bands.length; ++i) {
                const data = pane.bands[i].data;
                const t = data[data.length - 1].time;
                if (time < t)
                    time = t;
            }
            for (let i = 0; i < pane.lines.length; ++i) {
                const data = pane.lines[i].data;
                const t = data[data.length - 1].time;
                if (time < t)
                    time = t;
            }
        }
        return time;
    }

    public downloadSvg(): void {
        Downloader.download(Downloader.serializeToSvg(Downloader.getChildElementById(this.container.nativeElement.parentNode, 'chart')), 'ohlcv_chart.svg');
    }

    public downloadPng(): void {
        const last = dataTestOhlcv[dataTestOhlcv.length - 1];
        const next = new Ohlcv();
        next.time = last.time;
        //next.time.setDate(last.time.getDate() + 1);
        next.time = new Date(last.time.getFullYear(), last.time.getMonth(), last.time.getDay()+1)
        next.open = last.open + 1;
        next.high = last.high + 1;
        next.low = last.low - 1;
        next.close = last.close - 1;
        next.volume = last.volume + 100;
        console.log(next);
        dataTestOhlcv.push(next);
        this.render();
        //Downloader.download(Downloader.rasterizeToPng(Downloader.getChildElementById(this.container.nativeElement.parentNode, 'chart')), 'ohlcv_chart.png');
    }

    private static createPricePane(cfg: OhlcvChartConfig, lh: OhlcvChartComponent.HorizontalLayout,
        lv: OhlcvChartComponent.VerticalLayout, timeScale: any, timeAnnotationBottom: any, svg: any,
        isCandlestick: boolean, isVolume: boolean, isCrossHair: boolean): OhlcvChartComponent.PricePane {

        const config = cfg.pricePane;
        const pane = new OhlcvChartComponent.PricePane();
        pane.yPrice = d3.scaleLinear().range([lv.pricePane.height, 0]);
        const factor = config.valueMarginPercentageFactor;
        pane.yMarginFactorTop = 1 + factor;
        pane.yMarginFactorBottom = 1 - factor;
        pane.priceShape = (isCandlestick ? d3ts.plot.candlestick() : d3ts.plot.ohlc()).xScale(timeScale).yScale(pane.yPrice);
        pane.priceAccessor = pane.priceShape.accessor();

        const clip = 'price-clip';
        const clipUrl = `url(#${clip})`;
        pane.group = svg.append('g').attr('class', 'price-pane').attr('transform', `translate(${lh.content.left}, ${lv.pricePane.top})`);
        pane.group.append('clipPath').attr('id', clip).append('rect').attr('x', 0).attr('y', pane.yPrice(1))
            .attr('width', lh.content.width).attr('height', pane.yPrice(0) - pane.yPrice(1));
        pane.groupPrice = pane.group.append('g').attr('class', 'price').attr('clip-path', clipUrl);

        if (isVolume) {
            pane.yVolume = d3.scaleLinear().range([pane.yPrice(0), pane.yPrice(0.3)]);
            pane.volume = d3ts.plot.volume().xScale(timeScale).yScale(pane.yVolume);
            pane.groupVolume = pane.group.append('g').attr('class', 'volume').attr('clip-path', clipUrl);
        }

        for (let i = 0; i < config.bands.length; ++i) {
            const band = config.bands[i];
            const indicatorBand = new OhlcvChartComponent.IndicatorBand();
            indicatorBand.path = pane.group.append('g').attr('class', `band-${i}`).attr('clip-path', clipUrl).append('path')
                .attr('fill', band.color);
            indicatorBand.area = d3.area()
                .curve(OhlcvChartComponent.convertInterpolation(band.interpolation))
                .defined(d => { const w: any = d; return !isNaN(w.lower) && !isNaN(w.upper); })
                .x(d => { const w: any = d; return timeScale(w.time)})
                .y0(d => { const w: any = d; return pane.yPrice(w.lower)})
                .y1(d => { const w: any = d; return pane.yPrice(w.upper)});
            indicatorBand.data = band.data;
            pane.indicatorBands.push(indicatorBand);
        }

        for (let i = 0; i < config.lineAreas.length; ++i) {
            const lineArea = config.lineAreas[i];
            const value = lineArea.value;
            const indicatorLineArea = new OhlcvChartComponent.IndicatorLineArea();
            indicatorLineArea.path = pane.group.append('g').attr('class', `linearea-${i}`).attr('clip-path', clipUrl).append('path')
                .attr('fill', lineArea.color);
                indicatorLineArea.area = d3.area()
                .curve(OhlcvChartComponent.convertInterpolation(lineArea.interpolation))
                .defined(d => { const w: any = d; return !isNaN(w.value); })
                .x(d => { const w: any = d; return timeScale(w.time)})
                .y0(d => { const w: any = d; return pane.yPrice(w.value)})
                .y1(d => { const w: any = d; return pane.yPrice(value)});
            indicatorLineArea.data = lineArea.data;
            indicatorLineArea.value = lineArea.value;
            pane.indicatorLineAreas.push(indicatorLineArea);
        }

        for (let i = 0; i < config.horizontals.length; ++i) {
            const horizontal = config.horizontals[i];
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
            indicatorHorizontal.data = [{time: OhlcvChartComponent.minDate, value: value}, {time: OhlcvChartComponent.maxDate, value: value}];
            indicatorHorizontal.line = d3.line()
                .x(d => { const w: any = d; return timeScale(w.time)})
                .y(d => { return pane.yPrice(value)});
            pane.indicatorHorizontals.push(indicatorHorizontal);
        }

        for (let i = 0; i < config.lines.length; ++i) {
            const line = config.lines[i];
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
                .x(d => { const w: any = d; return timeScale(w.time)})
                .y(d => { const w: any = d; return pane.yPrice(w.value)});
            indicatorLine.data = line.data;
            pane.indicatorLines.push(indicatorLine);
        }

        if (cfg.axisLeft) {
            pane.yAxisLeft = d3.axisLeft(pane.yPrice).tickFormat(d3.format(config.valueFormat));
            if (config.valueTicks) {
                pane.yAxisLeft.ticks(config.valueTicks);
            }
            pane.groupAxisLeft = pane.group.append('g').attr('class', 'y axis left');
        }
        if (cfg.axisRight) {
            pane.yAxisRight = d3.axisRight(pane.yPrice).tickFormat(d3.format(config.valueFormat));
            if (config.valueTicks) {
                pane.yAxisRight.ticks(config.valueTicks);
            }
            pane.groupAxisRight = pane.group.append('g').attr('class', 'y axis right').attr('transform', `translate(${lh.content.width}, 0)`);
        }
    
        if (isCrossHair) {
            let crosshair = d3ts.plot.crosshair().xScale(timeScale).yScale(pane.yPrice).xAnnotation(timeAnnotationBottom)
                .verticalWireRange([0, lv.timeAxis.top]);
            if (cfg.axisLeft && cfg.axisRight) {
                const annotationLeft = d3ts.plot.axisannotation().axis(pane.yAxisLeft).orient('left')
                    .format(d3.format(config.valueFormat));
                const annotationRight = d3ts.plot.axisannotation().axis(pane.yAxisRight).orient('right')
                    .format(d3.format(config.valueFormat)).translate([timeScale(1), 0]);
                crosshair = crosshair.yAnnotation([annotationLeft, annotationRight]);
            }
            else if (cfg.axisLeft) {
                const annotationLeft = d3ts.plot.axisannotation().axis(pane.yAxisLeft).orient('left')
                    .format(d3.format(config.valueFormat));
                crosshair = crosshair.yAnnotation(annotationLeft);
            }
            else if (cfg.axisRight) {
                const annotationRight = d3ts.plot.axisannotation().axis(pane.yAxisRight).orient('right')
                    .format(d3.format(config.valueFormat)).translate([timeScale(1), 0]);
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
        const config = cfg.indicatorPanes[index];
        const pane = new OhlcvChartComponent.IndicatorPane();
        pane.yValue = d3.scaleLinear().range([block.height, 0]);
        const factor = config.valueMarginPercentageFactor;
        pane.yMarginFactorTop = 1 + factor;
        pane.yMarginFactorBottom = 1 - factor;

        const clip = `indicator-clip-${index}`;
        const clipUrl = `url(#${clip})`;
        pane.group = svg.append('g').attr('class', 'indicator-pane').attr('transform', `translate(${lh.content.left}, ${block.top})`);
        pane.group.append('clipPath').attr('id', clip).append('rect').attr('x', 0).attr('y', pane.yValue(1))
            .attr('width', lh.content.width).attr('height', pane.yValue(0) - pane.yValue(1));

        for (let i = 0; i < config.bands.length; ++i) {
            const band = config.bands[i];
            const indicatorBand = new OhlcvChartComponent.IndicatorBand();
            indicatorBand.path = pane.group.append('g').attr('class', `band-${i}`).attr('clip-path', clipUrl).append('path')
                .attr('fill', band.color);
            indicatorBand.area = d3.area()
                .curve(OhlcvChartComponent.convertInterpolation(band.interpolation))
                .defined(d => { const w: any = d; return !isNaN(w.lower) && !isNaN(w.upper); })
                .x(d => { const w: any = d; return timeScale(w.time)})
                .y0(d => { const w: any = d; return pane.yValue(w.lower)})
                .y1(d => { const w: any = d; return pane.yValue(w.upper)});
            indicatorBand.data = band.data;
            pane.indicatorBands.push(indicatorBand);
        }

        for (let i = 0; i < config.lineAreas.length; ++i) {
            const lineArea = config.lineAreas[i];
            const value = lineArea.value;
            const indicatorLineArea = new OhlcvChartComponent.IndicatorLineArea();
            indicatorLineArea.path = pane.group.append('g').attr('class', `linearea-${i}`).attr('clip-path', clipUrl).append('path')
                .attr('fill', lineArea.color);
                indicatorLineArea.area = d3.area()
                .curve(OhlcvChartComponent.convertInterpolation(lineArea.interpolation))
                .defined(d => { const w: any = d; return !isNaN(w.value); })
                .x(d => { const w: any = d; return timeScale(w.time)})
                .y0(d => { const w: any = d; return pane.yValue(w.value)})
                .y1(d => { const w: any = d; return pane.yValue(value)});
            indicatorLineArea.data = lineArea.data;
            indicatorLineArea.value = lineArea.value;
            pane.indicatorLineAreas.push(indicatorLineArea);
        }

        for (let i = 0; i < config.horizontals.length; ++i) {
            const horizontal = config.horizontals[i];
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
            indicatorHorizontal.data = [{time: OhlcvChartComponent.minDate, value: value}, {time: OhlcvChartComponent.maxDate, value: value}];
            indicatorHorizontal.line = d3.line()
                .x(d => { const w: any = d; return timeScale(w.time)})
                .y(d => { return pane.yValue(value)});
            pane.indicatorHorizontals.push(indicatorHorizontal);
        }

        for (let i = 0; i < config.lines.length; ++i) {
            const line = config.lines[i];
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
                .x(d => { const w: any = d; return timeScale(w.time)})
                .y(d => { const w: any = d; return pane.yValue(w.value)});
            indicatorLine.data = line.data;
            pane.indicatorLines.push(indicatorLine);
        }

        if (cfg.axisLeft) {
            pane.yAxisLeft = d3.axisLeft(pane.yValue).tickFormat(d3.format(config.valueFormat));
            if (config.valueTicks) {
                pane.yAxisLeft.ticks(config.valueTicks);
            }
            pane.groupAxisLeft = pane.group.append('g').attr('class', 'y axis left');
        }
        if (cfg.axisRight) {
            pane.yAxisRight = d3.axisRight(pane.yValue).tickFormat(d3.format(config.valueFormat));
            if (config.valueTicks) {
                pane.yAxisRight.ticks(config.valueTicks);
            }
            pane.groupAxisRight = pane.group.append('g').attr('class', 'y axis right').attr('transform', `translate(${lh.content.width}, 0)`);
        }
    
        if (isCrossHair) {
            const delta = lv.pricePane.top + lv.timeAxis.top - block.top;
            const timeAnnotationBottom = d3ts.plot.axisannotation().axis(timeAxisBottom).orient('bottom')
                .format(d3.timeFormat(cfg.timeFormat)).width(65).translate([0, delta]);
            let crosshair = d3ts.plot.crosshair().xScale(timeScale).yScale(pane.yValue).xAnnotation(timeAnnotationBottom)
                .verticalWireRange([lv.pricePane.top - block.top, delta]);
            if (cfg.axisLeft && cfg.axisRight) {
                const annotationLeft = d3ts.plot.axisannotation().axis(pane.yAxisLeft).orient('left')
                    .format(d3.format(config.valueFormat));
                const annotationRight = d3ts.plot.axisannotation().axis(pane.yAxisRight).orient('right')
                    .format(d3.format(config.valueFormat)).translate([timeScale(1), 0]);
                crosshair = crosshair.yAnnotation([annotationLeft, annotationRight]);
            }
            else if (cfg.axisLeft) {
                const annotationLeft = d3ts.plot.axisannotation().axis(pane.yAxisLeft).orient('left')
                    .format(d3.format(config.valueFormat));
                crosshair = crosshair.yAnnotation(annotationLeft);
            }
            else if (cfg.axisRight) {
                const annotationRight = d3ts.plot.axisannotation().axis(pane.yAxisRight).orient('right')
                    .format(d3.format(config.valueFormat)).translate([timeScale(1), 0]);
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
        pane.timeScale = d3ts.scale.financetime().range([0, width]);
        pane.priceScale = d3.scaleLinear().range([height, 0]);
        pane.brush = d3.brushX().extent([[0, 0], [width, height]]);
        pane.area = d3ts.plot.ohlcarea().xScale(pane.timeScale).yScale(pane.priceScale);

        pane.group = svg.append('g').attr('class', 'nav').attr('transform', `translate(${lh.content.left}, ${lv.navigationPane.top})`);
        pane.areaSelection = pane.group.append('g').attr('class', 'area');
        pane.paneSelection = pane.group.append('g').attr('class', 'pane');

        return pane;    
    }

    private static createTimePane(cfg: OhlcvChartConfig, lh: OhlcvChartComponent.HorizontalLayout,
        lv: OhlcvChartComponent.VerticalLayout, svg: any): OhlcvChartComponent.TimePane {

        const pane = new OhlcvChartComponent.TimePane();
        pane.timeScale = d3ts.scale.financetime().range([0, lh.content.width]);
        pane.timeAxis =  d3.axisBottom(pane.timeScale);//.tickFormat(d3.timeFormat(cfg.timeFormat));
        pane.timeAnnotation = d3ts.plot.axisannotation().axis(pane.timeAxis).orient('bottom')
            .format(d3.timeFormat(cfg.timeFormat)).width(65).translate([0, lv.timeAxis.top]);

        pane.group = svg.append('g').attr('class', 'x axis').attr("height", lv.timeAxis.height)
            .attr('transform', `translate(${lh.content.left}, ${lv.pricePane.top + lv.timeAxis.top})`);

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
}

export namespace OhlcvChartComponent {
    export class HorizontalLayoutBlock {
        public left: number = 0;
        public width: number = 0;
    }

    export class HorizontalLayout {
        public width: number = 0;
        public chart: HorizontalLayoutBlock = {left: 0, width: 0};
        public content: HorizontalLayoutBlock = {left: 0, width: 0};
    }

    export class VerticalLayoutBlock {
        public top: number = 0;
        public height: number = 0;
    }

    export class VerticalLayout {
        public height: number = 0;
        public pricePane: VerticalLayoutBlock = {top: 0, height: 0};
        public indicatorPanes: VerticalLayoutBlock[] = [];
        public timeAxis: VerticalLayoutBlock = {top: 0, height: 0};
        public navigationPane: VerticalLayoutBlock = {top: 0, height: 0};
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

        public draw(timePane: TimePane): void {
            const datum = this.groupPrice.datum();
            const datumLastIndex = datum.length - 1;

            const timeDomain: [number, number] = timePane.timeScale.zoomable().domain();
            let min = Math.round(timeDomain[0]);
            let max = Math.round(timeDomain[1]);
            if (min < 0) min = 0;
            if (max > datumLastIndex) max = datumLastIndex;
            const priceDomain: [number, number] = d3ts.scale.plot.ohlc(datum.slice.apply(datum, [min, max]), this.priceAccessor).domain();
            min = +datum[min].time;
            max = +datum[max].time;
            let minPrice = priceDomain[0];
            let maxPrice = priceDomain[1];
            for (let i = 0; i < this.indicatorBands.length; ++i) {
                const data = this.indicatorBands[i].data;
                for (let j = 0; j < data.length; ++j) {
                    const d = data[j];
                    const t = +d.time;
                    if (min <= t && t <= max) {
                        if (minPrice > d.lower) minPrice = d.lower;
                        if (maxPrice < d.upper) maxPrice = d.upper;    
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
                        if (minPrice > d.value) minPrice = d.value;
                        if (maxPrice < d.value) maxPrice = d.value;    
                        if (minPrice > value) minPrice = value;
                        if (maxPrice < value) maxPrice = value;    
                    }
                }
            }
            for (let i = 0; i < this.indicatorHorizontals.length; ++i) {
                const value = this.indicatorHorizontals[i].value;
                if (minPrice > value) minPrice = value;
                if (maxPrice < value) maxPrice = value;    
            }
            for (let i = 0; i < this.indicatorLines.length; ++i) {
                const data = this.indicatorLines[i].data;
                for (let j = 0; j < data.length; ++j) {
                    const d = data[j];
                    const t = +d.time;
                    if (min <= t && t <= max) {
                        const value = d.value;
                        if (minPrice > value) minPrice = value;
                        if (maxPrice < value) maxPrice = value;    
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

            // Using refresh method is more efficient as it does not perform any data joins
            // Use this if underlying data is not changing
            //this.groupPrice.call(this.priceShape.refresh);

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
            if (min < 0) min = 0;
            if (max > datumLastIndex) max = datumLastIndex;
            min = +datum[min].time;
            max = +datum[max].time;
            let minValue = Number.MAX_VALUE;
            let maxValue = Number.MIN_VALUE;
            for (let i = 0; i < this.indicatorBands.length; ++i) {
                const data = this.indicatorBands[i].data;
                for (let j = 0; j < data.length; ++j) {
                    const d = data[j];
                    const t = +d.time;
                    if (min <= t && t <= max) {
                        if (minValue > d.lower) minValue = d.lower;
                        if (maxValue < d.upper) maxValue = d.upper;    
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
                        if (minValue > d.value) minValue = d.value;
                        if (maxValue < d.value) maxValue = d.value;    
                        if (minValue > value) minValue = value;
                        if (maxValue < value) maxValue = value;    
                    }
                }
            }
            for (let i = 0; i < this.indicatorHorizontals.length; ++i) {
                const value = this.indicatorHorizontals[i].value;
                if (minValue > value) minValue = value;
                if (maxValue < value) maxValue = value;    
            }
            for (let i = 0; i < this.indicatorLines.length; ++i) {
                const data = this.indicatorLines[i].data;
                for (let j = 0; j < data.length; ++j) {
                    const d = data[j];
                    const t = +d.time;
                    if (min <= t && t <= max) {
                        const value = d.value;
                        if (minValue > value) minValue = value;
                        if (maxValue < value) maxValue = value;    
                    }
                }
            }
            minValue *= this.yMarginFactorBottom;
            maxValue *= this.yMarginFactorTop;
            this.yValue.domain([minValue, maxValue]).nice();

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
    }

    export class NavPane {
        group: any;
        priceScale: d3.ScaleLinear<number, number>;
        timeScale: any;
        brush: any;
        area: any;
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
}