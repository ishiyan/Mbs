import { Component, OnInit, ElementRef, ViewChild, Input, HostListener, ViewEncapsulation } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';
import * as d3 from 'd3';
import * as d3ts from '../../../../shared/d3ts';

import { OhlcvChartLayoutConfig } from './ohlcv-chart-layout-config';
import { Ohlcv } from './ohlcv';
import { Scalar } from './scalar';
//import { dataOhlcvDaily } from './data-ohlcv-daily';
import { dataTestOhlcv } from './data-test-ohlcv';
import { dataTestPercentB } from './data-test-percentb';
import { OhlcvChartConfig } from './ohlcv-chart-config';

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
  
    private config = {
        width: '100%',
        heightPricePane: '30%',
        heightNavigationPane: '30',
        heightIndicatorPanes: [
            100 //'8%', '9%', '10%'
        ],
        axisLeft: true,
        axisRight: false,
        marginLeft: 0,
        marginTop: 0,
        marginRight: 100,
        marginBottom: 0
    };

    private config2: OhlcvChartConfig = {
        width: '100%',
        heightNavigationPane: 30,
        axisLeft: true,
        axisRight: true,
        axisNav: true,
        margin: {left: 0, top: 0, right: 0, bottom: 0},
        ohlcv: {name: 'Test data set', data: dataTestOhlcv},
        pricePane: {height: '30%', extension: 0, bands: [
            {name: 'bb(stdev.p(20,c),2,sma(20,c))', data: [], indicator: 0, output: 0, stroke: 'red', color: 'green'},
        ], lines: [
            {name: 'ma-bb(stdev.p(20,c),2,sma(20,c))', data: [], indicator: 0, output: 0, stroke: 'red', color: 'red'},
            {name: 'up-bb(stdev.p(20,c),2,sma(20,c))', data: [], indicator: 0, output: 0, stroke: 'red', color: 'blue'}
        ]},
        indicatorPanes: []
    }

    readonly ohlcvViewCandlesticks = ohlcvViewCandlesticks;
    readonly ohlcvViewBars = ohlcvViewBars;
    private ohlcvView: number = this.ohlcvViewCandlesticks;
    get ohlcvViewType(): number {
      return this.ohlcvView;
    }
    set ohlcvViewType(value: number) {
      this.ohlcvView = value;
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

    get chartTitle(): string {
        return '----- title -----';
    }

    private renderNavAxis = false;
    private data: Ohlcv[] = dataTestOhlcv;//dataOhlcvDaily;
    private dataProcB: Scalar[] = dataTestPercentB;

    constructor(private element: ElementRef, iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
        iconRegistry.addSvgIcon('mb-candlesticks',
          sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-candlesticks.svg'));
        iconRegistry.addSvgIcon('mb-bars',
          sanitizer.bypassSecurityTrustResourceUrl('assets/img/mb-bars.svg'));
    }
    
    private drawBox(svg: any, name: string, color: string, box ) {
        const x = box.left;
        const y = box.top;
        const width = box.width;
        const height = box.height;

        // Setup a group for this box.
        const g = svg.selectAll('.' + name).data([null]);
        const gEnter = g.enter().append('g').attr('class', name);
        gEnter.merge(g).attr('transform', 'translate(' + x + ',' + y + ')');

        // Draw a box.
        gEnter.append('rect').attr('fill', color).attr('stroke', 'white')
            .merge(g.select('rect')).attr('width', width).attr('height', height);

        // Draw an X to show the size of the box.
        const lines = gEnter.merge(g).selectAll('line').data([
            {x1: 0, y1: 0, x2: width, y2: height}, {x1: 0, y1: height, x2: width, y2: 0}
        ]);
        lines.enter().append('line').style('stroke-width', 1).style('stroke-opacity', 0.4).style('stroke', 'black')
            .merge(lines).attr('x1', function (d) { return d.x1; }).attr('y1', function (d) { return d.y1; })
            .attr('x2', function (d) { return d.x2; }).attr('y2', function (d) { return d.y2; });

        // Add a text label.
        const text = gEnter
            .append('text').style('font-size', '1em').style('font-family', 'sans-serif').style('text-anchor', 'middle')
                .style('alignment-baseline', 'middle').attr('fill', 'white')
            .merge(g.select('text')).attr('x', width / 2).attr('y', height / 2).text(name);
    }

    @HostListener('window:resize', [])
    render() {
        const chartId = '#chart';
        //console.log('width=' + this.container.nativeElement.getBoundingClientRect().width);
        //console.log('offsetWidth=' + this.container.nativeElement.offsetWidth);
        //const w = this.container.nativeElement.getBoundingClientRect().width;
        const w = this.container.nativeElement.offsetWidth;        
        const layout = OhlcvChartLayoutConfig.configToLayout(this.config, w);
        //
        const marginLeft: number = this.config2.margin.left + (this.config2.axisLeft ? OhlcvChartConfig.verticalAxisWidth : 0);
        const marginRight: number = this.config2.margin.right +  (this.config2.axisRight ? OhlcvChartConfig.verticalAxisWidth : 0);
        let marginTop: number = this.config2.margin.top + 10;
        const marginBottom: number = this.config2.margin.bottom + 10;
        const left: number = marginLeft;
        const totalWidth: number = OhlcvChartComponent.valueToPixels(this.config2.width, w);
        const width: number = totalWidth - left - marginRight;
        const heightPricePane: number = OhlcvChartComponent.valueToPixels(this.config2.pricePane.height/*0*config.heightPricePane*/, width);
        const heightNavigationPane: number = OhlcvChartComponent.valueToPixels(this.config2.heightNavigationPane, width);


        d3.select(chartId).select('svg').remove();
        const svg: any = d3.select(chartId).append('svg')
          .attr("viewBox", `0 0 ${/*layout.totalWidth*/totalWidth} ${layout.totalHeight}`)
          .attr('preserveAspectRatio', 'xMinYMin meet')
          .attr('width', /*layout.totalWidth*/totalWidth)
          .attr('height', layout.totalHeight)
          //.append('g')
          .attr('transform', 'translate(' + /*layout.left*/left + ',' + /*layout.pricePane.top*/marginTop + ')');

        //const means = new Float64Array(64).fill(NaN);
        const nh: number = this.appendIndicatorLegend(svg, marginTop, left, width, this.config2.pricePane, 'BRILL@XAMS (NL0000442523)');
        console.log(`marginTop ${marginTop} -> ${marginTop + nh} `);
        if (nh > 0) {

        }
        marginTop += nh;

        /*let text = '';
        this.config2.pricePane.bands.forEach(element => {
            if (element.name && element.name.length > 0) text += ' ◼ ' + element.name;
        });
        this.config2.pricePane.lines.forEach(element => {
            if (element.name && element.name.length > 0) text += ' ― ' + element.name;
        });*/
        //svg./*append('g').*/append('text').attr("font-size", 10).attr("x", left + 10).attr("y", 12).text(text);

        const x = d3ts.scale.financetime().range([0, /*layout.width*/width]);
        const y = d3.scaleLinear/*Linear*/().range([/*layout.pricePane.height*/heightPricePane + nh, 0+ nh]);
        const xNav = d3ts.scale.financetime().range([0, /*layout.width*/width]);
        const yNav = d3.scaleLinear().range([/*layout.navigationPane.height*/heightNavigationPane, 0]);
        const brushNav = d3.brushX().extent([[0, 0], [/*layout.width*/width, /*layout.navigationPane.height*/heightNavigationPane + 14]]);
        const priceShape = this.getPriceShape().xScale(x).yScale(y);
        const accessor = priceShape.accessor();
        const areaNav = d3ts.plot.ohlcarea().xScale(xNav).yScale(yNav);

        const xAxisBottom = d3.axisBottom(x);
        const yAxisLeft = d3.axisLeft(y);
        let xAxisNavBottom;
        if (this.renderNavAxis) {
          xAxisNavBottom = d3.axisBottom(xNav);
        }

        const priceAnnotationLeft = d3ts.plot.axisannotation().axis(yAxisLeft).orient('left')
            .format(d3.format(',.2f'));//.translate([x(1), 0]);
        const timeAnnotationBottom = d3ts.plot.axisannotation().axis(xAxisBottom).orient('bottom')
            .format(d3.timeFormat('%Y-%m-%d')).width(65).translate([0+ nh, /*layout.pricePane.height*/heightPricePane+ nh/*layout.navigationPane.top - layout.pricePane.top*/]);////dim.plot.height
  
        let crosshair;
        if (this.renderCrosshair) {
            crosshair = d3ts.plot.crosshair().xScale(x).yScale(y)
                .xAnnotation(timeAnnotationBottom).yAnnotation(priceAnnotationLeft);
        }

        const focus = svg.append('g').attr('class', 'focus')
            .attr('transform', 'translate(' + /*layout.left*/left + ',' + layout.pricePane.top + ')');//?????????????????????
        focus.append('clipPath').attr('id', 'clip')
             .append('rect').attr('x', 0).attr('y', y(1)).attr('width', /*layout.width*/width).attr('height', y(0) - y(1));

        let yVolume;
        let volume;
        if (this.renderVolume) {
            yVolume = d3.scaleLinear/*Linear*/().range([y(0), y(0.3)]);
            volume = d3ts.plot.volume().xScale(x).yScale(yVolume);
            focus.append('g').attr('class', 'volume').attr('clip-path', 'url(#clip)');
        }

        focus.append('g').attr('class', 'price').attr('clip-path', 'url(#clip)');
        focus.append('g').attr('class', 'x axis')
            .attr('transform', 'translate(0,' + /*layout.pricePane.height*/heightPricePane + ')');
        focus.append('g').attr('class', 'y axis');
            //.append('text').attr('transform', 'rotate(-90)').attr('y', 6).attr('dy', '.71em').style('text-anchor', 'end').text('Price');
        if (this.renderCrosshair) {
            focus.append('g').attr('class', 'crosshair').call(crosshair);
        }

        const nav = svg.append('g').attr('class', 'nav')
            .attr('transform', 'translate(' + /*layout.left*/left + ',' + layout.navigationPane.top+ ')');//????????????????????????
        nav.append('g').attr('class', 'area');
        nav.append('g').attr('class', 'pane');
        if (this.renderNavAxis) {
            nav.append('g').attr('class', 'x axis').attr('transform', 'translate(0,' + /*layout.navigationPane.height*/heightNavigationPane + ')');
        }
        
        function draw(renderVolume: boolean) {
            const priceSelection = focus.select('g.price');
            const datum = priceSelection.datum();
            y.domain(d3ts.scale.plot.ohlc(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
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
            draw(this.renderVolume);
        }

        brushNav.on('end', brushed);

        // data begin ----------------------------------
        x.domain(this.data.map(accessor.t));
        xNav.domain(x.domain());
        //console.log('d3ts.scale.plot', d3ts.scale.plot); ///////////////////////////////////////////////////////////
        y.domain(d3ts.scale.plot.ohlc(this.data, accessor).domain());
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
        nav.select('g.pane').call(brushNav).selectAll('rect').attr('height', /*layout.navigationPane.height*/heightNavigationPane);

        x.zoomable().domain(xNav.zoomable().domain());
        draw(this.renderVolume);
        // data end ----------------------------------      
    }

    appendIndicatorLegend(svg: any, top: number, left: number, width: number, pane: OhlcvChartConfig.Pane, instrument: string = ''): number {
        const fontSize = 10;
        const whitespaceBetweenAxisAndLegend = 8;

        left += whitespaceBetweenAxisAndLegend;
        let l = left;
        let height = 0;

        if (instrument && instrument.length > 0) {
            const t = svg.append('text')
                .attr('font-size', fontSize)
                .attr('x', l)
                .attr('y', top)
                .text(` ${instrument} `);
            const r = t.node().getBoundingClientRect();
            l += r.width;
            height = r.height;
        }

        for (let i = 0; i < pane.bands.length; ++i) {
            const t = svg.append('text')
                .attr("font-size", fontSize)
                .style('fill', pane.bands[i].color)
                .attr("x", l)
                .attr("y", top)
                .text(' ◼ ' + pane.bands[i].name);
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
            l += w;
        }

        for (let i = 0; i < pane.lines.length; ++i) {
            const t = svg.append('text')
                .attr("font-size", fontSize)
                .style('fill', pane.lines[i].color)
                .attr("x", l)
                .attr("y", top)
                .text(' ― ' + pane.lines[i].name);
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
            l += w;
        }

        return height;
    }

    render1() {
        const chartId = '#chart';
        //console.log('width=' + this.container.nativeElement.getBoundingClientRect().width);
        //console.log('offsetWidth=' + this.container.nativeElement.offsetWidth);
        //const w = this.container.nativeElement.getBoundingClientRect().width;
        const w = this.container.nativeElement.offsetWidth;
        const layout = OhlcvChartLayoutConfig.configToLayout(this.config, w);
        d3.select(chartId).select('svg').remove();
        const svg: any = d3.select(chartId).append('svg')
          .attr('width', layout.totalWidth)
          .attr('height', layout.totalHeight);
          //.append('g')
          //.attr('transform', 'translate(' + layout.left + ',' + 0 + ')');

          this.drawBox(svg, 'price', 'green', {left: layout.left, top: layout.pricePane.top, width: layout.width, height: layout.pricePane.height});
          if (layout.indicatorPanes) {
              for (let i = 0; i < layout.indicatorPanes.length; ++i) {
                  this.drawBox(svg, 'pane-' + (i + 1), 'blue',
                      {left: layout.left, top: layout.indicatorPanes[i].top, width: layout.width, height: layout.indicatorPanes[i].height});
              }
          }
          if (layout.navigationPane) {
              this.drawBox(svg, 'navigation', 'orange',
                  {left: layout.left, top: layout.navigationPane.top, width: layout.width, height: layout.navigationPane.height});
          }
      
    }

    ngOnInit() {
        //console.log(this.configLayout);
        //this.render();
        setTimeout(() => this.render(), 0);

/*
        const data: D3Ohlcv[] = dataOhlcvDaily;

        const margin = { top: 10, bottom: 20, right: 80, left: 35 };
        const marginNav = { top: this.svgheight - 30 - 40, bottom: 40, right: margin.right, left: margin.left };

        // console.log(this.container.nativeElement.getBoundingClientRect());
        const w = this.container.nativeElement.getBoundingClientRect().width;
        const svg: any = d3.select(this.element.nativeElement).select('svg')
            .attr('width', w)
            .attr('height', this.svgheight)
            .append('g')
            .attr('transform', 'translate(' + margin.left + ',' + margin.top + ')');
        const width = w - margin.left - margin.right;
        const height = marginNav.top - margin.top - margin.bottom;
        const heightNav = this.svgheight - marginNav.top - marginNav.bottom;

        //const x = d3.scaleTime().range([0, width]);
        const x = d3tc.scale.financetime().range([0, width]);
        const y = d3.scaleLinear().range([height, 0]);
        const yVolume = d3.scaleLinear().range([y(0), y(0.3)]);
        const xNav = d3tc.scale.financetime().range([0, width]);
        const yNav = d3.scaleLinear().range([heightNav, 0]);
        const brush = d3.brushX().extent([[0, 0], [width, heightNav]]);
        const candlestick = d3tc.plot.candlestick().xScale(x).yScale(y);
        const volume = d3tc.plot.volume().xScale(x).yScale(yVolume);
        const close = d3tc.plot.close().xScale(xNav).yScale(yNav);
        // const area = d3.area().curve(d3.curveMonotoneX)
        //     .x(function(d) { return xNav(d['date']); }).y0(heightNav).y1(function(d) { return yNav(d['close']); });

        const accessor = candlestick.accessor();

        const xAxisBottom = d3.axisBottom(x);
        const xAxisNavBottom = d3.axisBottom(xNav);
        const yAxisLeft = d3.axisLeft(y);
        // const yAxisNavLeft = d3.axisLeft(yNav).ticks(0);

        const ohlcAnnotationLeft = d3tc.plot.axisannotation().axis(yAxisLeft).orient('left')
            .format(d3.format(',.2f'));
        const timeAnnotationBottom = d3tc.plot.axisannotation().axis(xAxisBottom).orient('bottom')
            .format(d3.timeFormat('%Y-%m-%d')).width(65).translate([0, height]);

        const crosshair = d3tc.plot.crosshair().xScale(x).yScale(y)
            .xAnnotation(timeAnnotationBottom).yAnnotation(ohlcAnnotationLeft);

        const focus = svg.append('g').attr('class', 'focus').attr('transform', 'translate(' + margin.left + ',' + margin.top + ')');
        focus.append('clipPath').attr('id', 'clip')
            .append('rect').attr('x', 0).attr('y', y(1)).attr('width', width).attr('height', y(0) - y(1));
        focus.append('g').attr('class', 'volume').attr('clip-path', 'url(#clip)');
        focus.append('g').attr('class', 'candlestick').attr('clip-path', 'url(#clip)');
        focus.append('g').attr('class', 'x axis').attr('transform', 'translate(0,' + height + ')');
        focus.append('g').attr('class', 'y axis')
            .append('text').attr('transform', 'rotate(-90)').attr('y', 6).attr('dy', '.71em').style('text-anchor', 'end').text('Price');
        focus.append('g').attr('class', 'crosshair').call(crosshair);

        const nav = svg.append('g').attr('class', 'context')
            .attr('transform', 'translate(' + marginNav.left + ',' + marginNav.top + ')');
        nav.append('g').attr('class', 'close');
        // nav.append('g').attr('class', 'area');
        nav.append('g').attr('class', 'pane');
        nav.append('g').attr('class', 'x axis').attr('transform', 'translate(0,' + heightNav + ')');
        // nav.append('g').attr('class', 'y axis').call(yAxisNavLeft);

        function draw() {
            const candlestickSelection = focus.select('g.candlestick');
            const datum = candlestickSelection.datum();
            y.domain(d3tc.scale.plot.ohlc(datum.slice.apply(datum, x.zoomable().domain()), accessor).domain());
            candlestickSelection.call(candlestick);
            focus.select('g.volume').call(volume);

            // Using refresh method is more efficient as it does not perform any data joins
            // Use this if underlying data is not changing
            svg.select('g.candlestick').call(candlestick.refresh);

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
            draw();
        }

        brush.on('end', brushed);

        // data begin ----------------------------------
        x.domain(data.map(accessor.d));
        xNav.domain(x.domain());
        y.domain(d3tc.scale.plot.ohlc(data, accessor).domain());
        yNav.domain(y.domain());
        yVolume.domain(d3tc.scale.plot.volume(data).domain());

        focus.select('g.candlestick').datum(data);
        focus.select('g.volume').datum(data);

        nav.select('g.close').datum(data).call(close);
        // nav.select('g.area').datum(data).call(area);
        nav.select('g.x.axis').call(xAxisNavBottom);

        // Associate the brush with the scale and render the brush only AFTER a domain has been applied
        nav.select('g.pane').call(brush).selectAll('rect').attr('height', heightNav);

        x.zoomable().domain(xNav.zoomable().domain());
        draw();
        // data end ----------------------------------
*/
    }

    private getPriceShape(): any {
        return this.ohlcvView == ohlcvViewCandlesticks ? d3ts.plot.candlestick() : d3ts.plot.ohlc();
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
