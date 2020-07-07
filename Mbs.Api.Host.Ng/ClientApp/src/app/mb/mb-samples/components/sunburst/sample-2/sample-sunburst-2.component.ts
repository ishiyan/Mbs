import { Component } from '@angular/core';

// tslint:disable:max-line-length
import { HierarchyTreeNode } from '../../../../../shared/mbs/charts/hierarchy-tree/hierarchy-tree';
import { HierarchyTreeSumFunction, sumNumberOfLeafNodes } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/sum-function';
import { HierarchyTreeFillFunction, coolFill, coolFillInverted, rainbowFill, rainbowFillInverted } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/fill-function';
import { HierarchyTreeFillOpacityFunction, transparentFillOpacity, opaqueFillOpacity, linearFillOpacity } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/fill-opacity-function';
import { HierarchyTreeTapFunction } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/tap-function';
import { HierarchyTreeLabelFunction, nameLabels, valueLabels, emptyLabels } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/label-function';
import { HierarchyTreeFontSizeFunction, equalFontSize8, equalFontSize10, equalFontSize12, equalFontSize14, equalFontSize16, equalFontSize18, linearFontSize } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/font-size-function';

import { CountryHierarchyTreeNode, countries } from '../../../test-data/hierarchies/countries';

interface DiameterItam {
  value: number | string;
  key: string;
}

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

interface FillFunc {
  value: HierarchyTreeFillFunction;
  key: string;
}

interface FillOpacityFunc {
  value: HierarchyTreeFillOpacityFunction;
  key: string;
}

interface LabelFunc {
  value: HierarchyTreeLabelFunction;
  key: string;
}

interface LabelFontSizeFunc {
  value: HierarchyTreeFontSizeFunction;
  key: string;
}

@Component({
  selector: 'mb-sample-sunburst-2',
  templateUrl: './sample-sunburst-2.component.html',
  styleUrls: ['./sample-sunburst-2.component.scss']
})
export class SampleSunburst2Component {

  readonly contriesHierarchy: CountryHierarchyTreeNode = countries;
  zoom = true;

  readonly diameterArray: DiameterItam[] = [
    { key: '100%', value: '100%' },
    { key: '1000', value: 1000 },
    { key: '200', value: 200 }
  ];
  diameterSelected: number | string = this.diameterArray[0].value;

  readonly levelsArray: number[] = [ 0, 1, 2, 3, 4 ];
  levelsSelected: number = this.levelsArray[0];

  readonly sumFuncArray: SumFunc[] = [
    { key: 'median wealth per adult', value: sumFuncWpaMedian },
    { key: 'mean wealth per adult', value: sumFuncWpaMean },
    { key: 'share of income top 1%', value: sumFuncSirop },
    { key: 'Wealth inequality', value: sumFuncWgini },
    { key: 'income inequality', value: sumFuncIgini },
    { key: 'GDP per capita', value: sumFuncGdpPerCapita },
    { key: 'inequality-adjusted HDI', value: sumFuncIhdi },
    { key: 'human development index', value: sumFuncHdi },
    { key: 'population density', value: sumFuncDensity },
    { key: 'population', value: sumFuncPopulation },
    { key: 'area', value: sumFuncArea },
    { key: 'conuntries', value: sumNumberOfLeafNodes }
  ];
  sumFuncSelected: HierarchyTreeSumFunction = this.sumFuncArray[0].value;

  readonly sortArray: string[] = [ 'asc', 'desc', 'none' ];
  sortSelected: string = this.sortArray[0];

  readonly fillFuncArray: FillFunc[] = [
    { key: 'cool', value: coolFill },
    { key: 'cool inverted', value: coolFillInverted },
    { key: 'rainbow', value: rainbowFill },
    { key: 'rainbow inverted', value: rainbowFillInverted }
  ];
  fillFuncSelected: HierarchyTreeFillFunction = this.fillFuncArray[0].value;

  readonly fillOpacityFuncArray: FillOpacityFunc[] = [
    { key: 'opaque', value: opaqueFillOpacity },
    { key: 'linear', value: linearFillOpacity },
    { key: 'transparent', value: transparentFillOpacity }
  ];
  fillOpacityFuncSelected: HierarchyTreeFillOpacityFunction = this.fillOpacityFuncArray[0].value;

  readonly labelFuncArray: LabelFunc[] = [
    { key: 'none', value: emptyLabels },
    { key: 'name', value: nameLabels },
    { key: 'value', value: valueLabels }
  ];
  labelFuncSelected: HierarchyTreeLabelFunction = this.labelFuncArray[0].value;

  readonly labelFillArray: string[] = [ 'white', 'black', 'transparent' ];
  labelFillSelected: string = this.labelFillArray[0];

  readonly labelFontSizeFuncArray: LabelFontSizeFunc[] = [
    { key: 'linear', value: linearFontSize },
    { key: '18', value: equalFontSize18 },
    { key: '16', value: equalFontSize16 },
    { key: '14', value: equalFontSize14 },
    { key: '12', value: equalFontSize12 },
    { key: '10', value: equalFontSize10 },
    { key: '8', value: equalFontSize8 }
  ];
  labelFontSizeFuncSelected: HierarchyTreeFontSizeFunction = this.labelFontSizeFuncArray[0].value;

  selectedNodeInfo: string;
  tapFunc: HierarchyTreeTapFunction = (d: d3.HierarchyNode<HierarchyTreeNode>) => {
    const n = d.data as CountryHierarchyTreeNode;
    let text = `${n.name ? n.name : 'root'}: value: ${d.value}`;
    if (!n.children) {
      text += ` area: ${n.area}, population: ${n.population}`;
    }
    if (n.density) {
      text += `, density: ${n.density}`;
    }
    if (n.hdi) {
      text += `, HDI: ${n.hdi}`;
    }
    if (n.ihdi) {
      text += `, inequality-adjusted HDI: ${n.ihdi}`;
    }
    if (n.gdpPerCapita) {
      text += `, GDP per capita: ${n.gdpPerCapita}`;
    }
    if (n.igini) {
      text += `, income inequality: ${n.igini}`;
    }
    if (n.wgini) {
      text += `, wealth inequality: ${n.wgini}`;
    }
    if (n.sirop) {
      text += `, share of income top 1%: ${n.sirop}`;
    }
    if (n.wpaMean) {
      text += `, mean wealth per adult: ${n.wpaMean}`;
    }
    if (n.wpaMedian) {
      text += `, median wealth per adult: ${n.wpaMedian}`;
    }
    this.selectedNodeInfo = text;
  }
}
