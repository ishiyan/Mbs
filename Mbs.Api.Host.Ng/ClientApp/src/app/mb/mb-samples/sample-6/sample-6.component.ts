import { Component } from '@angular/core';

import { OhlcvChartConfig } from '../../../shared/mbs/charts/ohlcv-chart/ohlcv-chart-config';
import { TestData } from './test-data/test-data';

@Component({
  selector: 'mb-sample-6',
  templateUrl: './sample-6.component.html',
  styleUrls: ['./sample-6.component.scss']
})
export class Sample6Component {
  public showPortal = false;
  public configPrefilled: OhlcvChartConfig = TestData.configDataPrefilled;

  private testData: TestData = new TestData();
  public configModifiable: OhlcvChartConfig = this.testData.config;

  public clearData(): void {
    this.testData.clear();
    this.configModifiable = { ...this.configModifiable };
  }

  public addData1(): void {
    this.testData.addSingle();
    this.configModifiable = { ...this.configModifiable };
  }

  public addData10(): void {
    this.testData.addTen();
    this.configModifiable = { ...this.configModifiable };
  }

  public addDataAll(): void {
    this.testData.addAll();
    this.configModifiable = { ...this.configModifiable };
  }
}
