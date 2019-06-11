import { Component, OnInit, ElementRef, Input } from '@angular/core';
import * as d3 from 'd3';
import * as d3tc from '../../../../shared/d3tc';

import { D3Ohlcv } from '../../data/d3-ohlcv';
import { dataOhlcvDaily } from '../../data/data-ohlcv-daily';

@Component({
    selector: 'app-d3tc-horizon-chart-single',
    templateUrl: './d3tc-horizon-chart-single.component.html',
    styleUrls: ['./d3tc-horizon-chart-single.component.scss']
})
export class D3tcHorizonChartSingleComponent implements OnInit {
    private _width = 700;
    private _height = 40;
    private _bands = 3;
    private _mode = 'mirror';
    private _interpolation = d3.curveCatmullRom;
    private _colors = ['#08519c', '#bdd7e7', '#bae4b3', '#006d2c'];
    private horizonData: (number|Date)[][];
    private svg: any;
    private g: any;
    private chart: any;
    @Input() set width(value: number) {
        this._width = value;
        if (this.svg) {
            this.svg.attr('width', value);
            this.g.call(this.chart.duration(0).width(value));
        }
    }
    @Input() set height(value: number) {
        this._height = value;
        if (this.svg) {
            this.svg.attr('height', value);
            this.g.call(this.chart.duration(0).height(value));
        }
    }
    @Input() set bands(value: number) {
        this._bands = value;
        if (this.g) {
            this.g.call(this.chart.duration(0).bands(value));
        }
    }
    @Input() set mode(value: string) {
        this._mode = value;
        if (this.g) {
            this.g.call(this.chart.duration(500).mode(value));
        }
    }
    @Input() set colors(value: string[]) {
        this._colors = value;
        if (this.g) {
            this.g.call(this.chart.duration(500).colors(value));
        }
    }
    @Input() set interpolation(value: any) {
        this._interpolation = value;
        if (this.g) {
            this.g.call(this.chart.duration(500).interpolate(value));
        }
    }
    @Input() set data(value: D3Ohlcv[]) {
        const mean = value.map(c => c.close).reduce(function (p, v) { return p + v; }, 0) / value.length;
        this.horizonData = value.map(function (c) { return [c.date, c.close ? (c.close - mean) : null]; });
        if (this.g) {
            this.g.data([this.horizonData]).call(this.chart);
        }
    }

    constructor(private element: ElementRef) {
    }

    ngOnInit() {
        this.svg = d3.select(this.element.nativeElement).select('svg').attr('width', this._width).attr('height', this._height);
        this.g = this.svg.append('g');
        this.chart = d3tc.horizonChart().width(this._width).height(this._height).bands(this._bands)
            .mode(this._mode).colors(this._colors).interpolate(this._interpolation).defined(function(d) { return d[1]; });
        this.g.data([this.horizonData]).call(this.chart);
    }
}
