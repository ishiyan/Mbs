import { Component } from '@angular/core';

import { OhlcvChartConfig } from '../../../shared/mbs/charts/ohlcv-chart/ohlcv-chart-config';

import { dataTestOhlcv } from '../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-ohlcv';
import { dataTestBb } from '../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-bb';
import { dataTestMa } from '../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-ma';
import { dataTestLo } from '../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-lo';
import { dataTestUp } from '../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-up';
import { dataTestPercentB } from '../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-percentb';
import { dataTestBw } from '../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-bw';
import { dataTestGoertzel1 } from '../../../shared/mbs/charts/ohlcv-chart/test-data/data-test-goertzel_1';
import { TestData } from './test-data/test-data';

@Component({
    selector: 'app-sample-6',
    templateUrl: './sample-6.component.html',
    styleUrls: ['./sample-6.component.scss']
})
export class Sample6Component {

    public configPrefilled: OhlcvChartConfig = TestData.configDataPrefilled;

    private testData: TestData = new TestData();
    public configModifiable: OhlcvChartConfig = this.testData.config;

    public clearData(): void {
        this.testData.clear();
        this.configModifiable = {...this.configModifiable};
    }

    public addData1(): void {
        this.testData.addSingle();
        this.configModifiable = {...this.configModifiable};
    }

    public addData10(): void {
        this.testData.addTen();
        this.configModifiable = {...this.configModifiable};
    }

    public addDataAll(): void {
        this.testData.addAll();
        this.configModifiable = {...this.configModifiable};
    }

 }
