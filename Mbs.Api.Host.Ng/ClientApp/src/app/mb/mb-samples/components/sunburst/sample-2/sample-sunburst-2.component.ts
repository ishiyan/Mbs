import { Component } from '@angular/core';

import { HierarchyTreeNode, linearFillOpacity, nameLabels } from '../../../../../shared/mbs/charts/hierarchy-tree';
import { HierarchyTreeSumFunction, sumNumberOfNodes, sumNumberOfLeafNodes, sumNodeValues } from '../../../../../shared/mbs/charts/hierarchy-tree';
import { CountryHierarchyTreeNode, countries } from '../../../test-data/hierarchies/countries';

import { SparklineConfiguration } from '../../../../../shared/mbs/charts/sparkline/sparkline-configuration.interface';
import { Ohlcv } from '../../../../../shared/mbs/data/entities/ohlcv';
import { Quote } from '../../../../../shared/mbs/data/entities/quote';
import { Trade } from '../../../../../shared/mbs/data/entities/trade';
import { Scalar } from '../../../../../shared/mbs/data/entities/scalar';
import { testDataOhlcv } from '../../../test-data/indicators/test-data-ohlcv';
import { testDataBbBw } from '../../../test-data/indicators/test-data-bb-bw';
import { testDataBbMa } from '../../../test-data/indicators/test-data-bb-ma';
interface DataItem {
  data: Ohlcv[] | Quote[] | Trade[] | Scalar[];
  name: string;
}
interface DataGroup {
  disabled?: boolean;
  name: string;
  items: DataItem[];
}
///////////////////////////////////////////////////////
interface SumFunc {
  value: HierarchyTreeSumFunction;
  key: string;
}

const sumFuncArea: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.area ? d.area : 0;
const sumFuncPopulation: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.population ? d.population : 0;
const sumFuncDensity: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.density ? d.density : 0;
const sumFuncHdi: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.hdi ? d.hdi : 0;
const sumFuncIhdi: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.ihdi ? d.ihdi : 0;
const sumFuncGdpPerCapita: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.gdpPerCapita ? d.gdpPerCapita : 0;
const sumFuncIgini: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.igini ? d.igini : 0;
const sumFuncWgini: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.wgini ? d.wgini : 0;
const sumFuncSirop: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.sirop ? d.sirop : 0;
const sumFuncWpaMean: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.wpaMean ? d.wpaMean : 0;
const sumFuncWpaMedian: HierarchyTreeSumFunction = (d: CountryHierarchyTreeNode) => d.wpaMedian ? d.wpaMedian : 0;

@Component({
  selector: 'mb-sample-sunburst-2',
  templateUrl: './sample-sunburst-2.component.html',
  styleUrls: ['./sample-sunburst-2.component.scss']
})
export class SampleSunburst2Component {

  readonly contriesHierarchy: CountryHierarchyTreeNode = countries;
  readonly sumFuncArray: SumFunc[] = [
    { key: 'conuntries', value: sumNumberOfLeafNodes },
    { key: 'area', value: sumFuncArea },
    { key: 'population', value: sumFuncPopulation },
    { key: 'population density', value: sumFuncDensity },
    { key: 'human development index', value: sumFuncHdi },
    { key: 'inequality-adjusted HDI', value: sumFuncIhdi },
    { key: 'GDP per capita', value: sumFuncGdpPerCapita },
    { key: 'income inequality', value: sumFuncIgini },
    { key: 'Wealth inequality', value: sumFuncWgini },
    { key: 'share of income top 1%', value: sumFuncSirop },
    { key: 'mean wealth per adult', value: sumFuncWpaMean },
    { key: 'median wealth per adult', value: sumFuncWpaMedian },
  ];
  sumFuncSelected: HierarchyTreeSumFunction = this.sumFuncArray[0].value;

  //////////////////////////////////////////////////
  dataOhlcv = testDataOhlcv;
  dataScalar = testDataBbBw;
  dataScalarWithNaN = testDataBbMa;

  readonly dataArray: DataItem[] = [
    { data: this.dataOhlcv, name: 'ohlcv data' },
    { data: this.dataScalar, name: 'scalar data' },
    { data: this.dataScalarWithNaN, name: 'scalar data with NaN' }
  ];
  arrayItemFixed: DataItem = this.dataArray[0];
  arrayItem: DataItem = this.dataArray[0];

  readonly dataGroups: DataGroup[] = [
    {
      name: 'Ohlcv group',
      items: [this.dataArray[0]]
    },
    {
      name: 'Scalar group',
      items: [this.dataArray[1], this.dataArray[2]]
    },
    {
      disabled: true,
      name: 'Quote group',
      items: [{data: [], name: 'some name'}]
    },
  ];
  groupItemFixed: DataItem = this.dataGroups[0].items[0];
  groupItem: DataItem = this.dataGroups[0].items[0];

  readonly configLine: SparklineConfiguration = { fillColor: undefined, strokeColor: 'steelblue', strokeWidth: 1 };
  readonly configFill: SparklineConfiguration = { fillColor: 'steelblue', strokeColor: undefined, strokeWidth: 1 };
  arrayItemIsLineFixed = false;
  groupItemIsLineFixed = false;
  arrayItemIsLine = false;
  groupItemIsLine = false;

  itemIsLineChangedFixed(isGroup: boolean, event: any) {
    const value = event.value === 'true';
    if (isGroup) {
      this.groupItemIsLineFixed = value;
    } else {
      this.arrayItemIsLineFixed = value;
    }
  }

  itemIsLineChanged(isGroup: boolean, event: any) {
    const value = event.value === 'true';
    if (isGroup) {
      this.groupItemIsLine = value;
    } else {
      this.arrayItemIsLine = value;
    }
  }
}
