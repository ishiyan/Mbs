import { Component, OnInit, OnChanges, ViewChild, ElementRef, Input, ViewEncapsulation } from '@angular/core';
import * as d3 from 'd3';

import { D3DatePrice } from '../../data/d3-date-price';
import { d3Sp500 } from '../../data/d3-sp500';

@Component({
    selector: 'app-brush-and-zoom-area-chart',
    templateUrl: './brush-and-zoom-area-chart.component.html',
    styleUrls: ['./brush-and-zoom-area-chart.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class BrushAndZoomAreaChartComponent implements OnInit {
    @ViewChild('container', { static: true }) container: ElementRef;
    // @Input() svgwidth: any;
    @Input() svgheight: any;

    constructor(private element: ElementRef) {
    }

    ngOnInit(): void {
        const data: D3DatePrice[] = d3Sp500;

        const margin: any = { top: 20, right: 20, bottom: 110, left: 40 };
        const margin2: any = { top: 430, right: 20, bottom: 30, left: 40 };

        const w = this.container.nativeElement.getBoundingClientRect().width;
        const svg: any = d3.select(this.element.nativeElement).select('svg')
            .attr('width', w)
            .attr('height', this.svgheight);
        const width: number = +w - margin.left - margin.right;
        const height: number = +this.svgheight - margin.top - margin.bottom;
        const height2: number = +this.svgheight - margin2.top - margin2.bottom;

        const x = d3.scaleTime().range([0, width]);
        const x2 = d3.scaleTime().range([0, width]);
        const y = d3.scaleLinear().range([height, 0]);
        const y2 = d3.scaleLinear().range([height2, 0]);

        const xAxis = d3.axisBottom(x);
        const xAxis2 = d3.axisBottom(x2);
        const yAxis = d3.axisLeft(y);

        const brush = d3.brushX().extent([[0, 0], [width, height2]]);

        const zoom = d3.zoom()
            .scaleExtent([1, Infinity])
            .translateExtent([[0, 0], [width, height]])
            .extent([[0, 0], [width, height]]);

        const area = d3.area()
            .curve(d3.curveMonotoneX)
            .x(function(d) { return x(d['date']); })
            .y0(height)
            .y1(function(d) { return y(d['price']); });

        const area2 = d3.area()
            .curve(d3.curveMonotoneX)
            .x(function(d) { return x2(d['date']); })
            .y0(height2)
            .y1(function(d) { return y2(d['price']); });

        svg.append('defs').append('clipPath')
            .attr('id', 'clip')
            .append('rect')
            .attr('width', width)
            .attr('height', height);

        const focus = svg.append('g')
            .attr('class', 'focus')
            .attr('transform', 'translate(' + margin.left + ',' + margin.top + ')');

        const context = svg.append('g')
            .attr('class', 'context')
            .attr('transform', 'translate(' + margin2.left + ',' + margin2.top + ')');

        // data begin ----------------------------------
        x.domain(d3.extent(data, function(d) { return d.date; }));
        y.domain([0, d3.max(data, function(d) { return d.price; })]);
        x2.domain(x.domain());
        y2.domain(y.domain());

        focus.append('path').datum(data).attr('class', 'area').attr('d', area);

        focus.append('g').attr('class', 'axis axis--x').attr('transform', 'translate(0,' + height + ')').call(xAxis);

        focus.append('g').attr('class', 'axis axis--y').call(yAxis);

        context.append('path').datum(data).attr('class', 'area').attr('d', area2);

        context.append('g').attr('class', 'axis axis--x').attr('transform', 'translate(0,' + height2 + ')').call(xAxis2);

        context.append('g').attr('class', 'brush').call(brush).call(brush.move, x.range());

        svg.append('rect').attr('class', 'zoom').attr('width', width).attr('height', height)
            .attr('transform', 'translate(' + margin.left + ',' + margin.top + ')').call(zoom);
        // data end ----------------------------------

        brush.on('brush end', () => {
            if (d3.event.sourceEvent && d3.event.sourceEvent.type === 'zoom') {
                return; // ignore brush-by-zoom
            }
            const s = d3.event.selection || x2.range();
            x.domain(s.map(x2.invert, x2));
            focus.select('.area').attr('d', area);
            focus.select('.axis--x').call(xAxis);
            svg.select('.zoom').call(zoom.transform, d3.zoomIdentity
                .scale(width / (s[1] - s[0]))
                .translate(-s[0], 0));
        });

        zoom.on('zoom', () => {
            if (d3.event.sourceEvent && d3.event.sourceEvent.type === 'brush') {
                return; // ignore zoom-by-brush
            }
            const t = d3.event.transform;
            x.domain(t.rescaleX(x2).domain());
            focus.select('.area').attr('d', area);
            focus.select('.axis--x').call(xAxis);
            context.select('.brush').call(brush.move, x.range().map(t.invertX, t));
        });
    }
}
