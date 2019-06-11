import { Component, OnInit, ElementRef, ViewChild, Input } from '@angular/core';
import * as d3 from 'd3';
import * as d3tc from '../../../../shared/d3tc';

import { D3Ohlcv } from '../../data/d3-ohlcv';
import { dataOhlcvDaily } from '../../data/data-ohlcv-daily-big';

@Component({
    selector: 'app-d3tc-candlestick-chart',
    templateUrl: './d3tc-candlestick-chart.component.html',
    styleUrls: ['./d3tc-candlestick-chart.component.scss']
})
export class D3tcCandlestickChartComponent implements OnInit {
    @ViewChild('container', { static: true }) container: ElementRef;
    @Input() svgheight: any;

    constructor(private element: ElementRef) {
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
        lines.enter().append('line').style('stroke-width', 5).style('stroke-opacity', 0.4).style('stroke', 'black')
            .merge(lines).attr('x1', function (d) { return d.x1; }).attr('y1', function (d) { return d.y1; })
            .attr('x2', function (d) { return d.x2; }).attr('y2', function (d) { return d.y2; });

        // Add a text label.
        const text = gEnter
            .append('text').style('font-size', '1em').style('font-family', 'sans-serif').style('text-anchor', 'middle')
                .style('alignment-baseline', 'middle').attr('fill', 'white')
            .merge(g.select('text')).attr('x', width / 2).attr('y', height / 2).text(name);
      }

    ngOnInit() {
        const configLayout = {
            width: '77%',
            heightChart: '12%',
            heightNavigation: 80,
            heightIndicatorPanes: [
                '5%', 10, '50px'
            ],
            margin: {left: 10, top: 10, right: 10, bottom: 10 }
        };
        console.log(configLayout);
        const w = this.container.nativeElement.getBoundingClientRect().width;
        const layout = d3tc.csch.convertLayout(configLayout, w);
        console.log(layout);
        const svg: any = d3.select(this.element.nativeElement).select('svg')
            .attr('width', layout.totalWidth).attr('height', layout.totalHeight);

        this.drawBox(svg, 'a', 'red', {left: 0, top: 0, width: layout.totalWidth, height: layout.totalHeight});
        this.drawBox(svg, 'chart', 'green', {left: layout.left, top: layout.chart.top, width: layout.width, height: layout.chart.height});
        if (layout.indicatorPanes) {
            for (let i = 0; i < layout.indicatorPanes.length; ++i) {
                this.drawBox(svg, 'pane-' + (i + 1), 'blue',
                    {left: layout.left, top: layout.indicatorPanes[i].top, width: layout.width, height: layout.indicatorPanes[i].height});
            }
        }
        if (layout.navigation) {
            this.drawBox(svg, 'navigation', 'orange',
                {left: layout.left, top: layout.navigation.top, width: layout.width, height: layout.navigation.height});
        }

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
}
