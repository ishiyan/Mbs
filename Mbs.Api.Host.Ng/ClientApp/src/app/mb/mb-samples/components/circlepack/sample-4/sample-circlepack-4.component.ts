import { Component } from '@angular/core';

/* eslint-disable max-len */
import { HierarchyTreeNode } from '../../../../../shared/mbs/charts/hierarchy-tree/hierarchy-tree';
import { HierarchyTreeSumFunction, sumNumberOfNodes, sumNumberOfLeafNodes } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/sum-function';
import { HierarchyTreeFillFunction, coolFill, coolFillInverted, warmFill, warmFillInverted, viridisFill, viridisFillInverted, bluesFill, bluesFillInverted, rainbowFill, rainbowFillInverted } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/fill-function';
import { HierarchyTreeFillOpacityFunction, transparentFillOpacity, opaqueFillOpacity, linearFillOpacity } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/fill-opacity-function';
import { HierarchyTreeStrokeFunction, noStroke, blackStroke, whiteStroke, transparentStroke } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/stroke-function';
import { HierarchyTreeStrokeWidthFunction, noStrokeWidth, linearStrokeWidthThin, linearStrokeWidth, linearStrokeWidthThick, linearStrokeWidthExtraThick } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/stroke-width-function';
import { HierarchyTreeTapFunction } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/tap-function';
import { pathParentTooltips } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/tooltip-function';
import { HierarchyTreeLabelFunction, nameLabels, valueLabels, emptyLabels } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/label-function';
import { HierarchyTreeFontSizeFunction, equalFontSize8, equalFontSize10, equalFontSize12, equalFontSize14, equalFontSize16, equalFontSize18, linearFontSize } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/font-size-function';

import { Omxn40HierarchyTreeNode, omxn40Tickers, omxn40Currencies, omxn40Icb, omxn40Ms, omxn40MsStyle, omxn40MsStyleCapValueGrowth, omxn40MsStyleValueGrowthCap } from '../../../test-data/hierarchies/omxn40';

interface Dataset {
  value: Omxn40HierarchyTreeNode;
  key: string;
}

interface DiameterItam {
  value: number | string;
  key: string;
}

interface SumFunc {
  value: HierarchyTreeSumFunction;
  key: string;
}

const sumFuncValueEur: HierarchyTreeSumFunction = (d: Omxn40HierarchyTreeNode) => d.constituent ? d.constituent.ratio * d.constituent.close : 0.0001;
const sumFuncTransactions: HierarchyTreeSumFunction = (d: Omxn40HierarchyTreeNode) => d.constituent ? d.constituent.transactions : 1;
const sumFuncVolume: HierarchyTreeSumFunction = (d: Omxn40HierarchyTreeNode) => d.constituent ? d.constituent.volume : 1;
const sumFuncTurnoverEur: HierarchyTreeSumFunction = (d: Omxn40HierarchyTreeNode) => d.constituent ? d.constituent.ratio * d.constituent.turnover : 1;
const sumFuncMarketCapBnEur: HierarchyTreeSumFunction = (d: Omxn40HierarchyTreeNode) => d.constituent ? d.constituent.ratio * d.constituent.marketCap : 1;

interface FillFunc {
  value: HierarchyTreeFillFunction;
  key: string;
}

interface FillOpacityFunc {
  value: HierarchyTreeFillOpacityFunction;
  key: string;
}

interface StrokeFunc {
  value: HierarchyTreeStrokeFunction;
  key: string;
}

interface StrokeWidthFunc {
  value: HierarchyTreeStrokeWidthFunction;
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
  selector: 'mb-sample-circlepack-4',
  templateUrl: './sample-circlepack-4.component.html',
  styleUrls: ['./sample-circlepack-4.component.scss']
})
export class SampleCirclepack4Component {

  readonly datasetArray: Dataset[] = [
    { key: 'currencies', value: omxn40Currencies },
    { key: 'tickers', value: omxn40Tickers },
    { key: 'icb', value: omxn40Icb },
    { key: 'ms', value: omxn40Ms },
    { key: 'ms style', value: omxn40MsStyle },
    { key: 'ms cap val', value: omxn40MsStyleCapValueGrowth },
    { key: 'ms val cap', value: omxn40MsStyleValueGrowthCap }
  ];
  datasetSelected: Omxn40HierarchyTreeNode = this.datasetArray[0].value;

  zoom = true;
  flat = false;
  rootCircle = false;

  readonly diameterArray: DiameterItam[] = [
    { key: '100%', value: '100%' },
    { key: '75%', value: '75%' },
    { key: '50%', value: '50%' },
    { key: '25%', value: '25%' },
    { key: '900', value: 900 },
    { key: '800', value: 800 },
    { key: '700', value: 700 },
    { key: '600', value: 600 },
    { key: '500', value: 500 },
    { key: '400', value: 400 },
    { key: '300', value: 300 },
    { key: '200', value: 200 }
  ];
  diameterSelected: number | string = this.diameterArray[0].value;

  readonly paddingArray: number[] = [ 1, 0, 2, 3, 4, 5, 10, 20 ];
  paddingSelected: number = this.paddingArray[0];

  readonly sumFuncArray: SumFunc[] = [
    { key: 'value eur', value: sumFuncValueEur },
    { key: 'transactions', value: sumFuncTransactions },
    { key: 'volume', value: sumFuncVolume },
    { key: 'turnover eur', value: sumFuncTurnoverEur },
    { key: 'market cap, bn eur', value: sumFuncMarketCapBnEur },
    { key: '# tickers', value: sumNumberOfLeafNodes }
  ];
  sumFuncSelected: HierarchyTreeSumFunction = this.sumFuncArray[0].value;

  readonly sortArray: string[] = [ 'asc', 'desc', 'none' ];
  sortSelected: string = this.sortArray[0];

  readonly fillFuncArray: FillFunc[] = [
    { key: 'cool', value: coolFill },
    { key: 'warm', value: warmFill },
    { key: 'viridis', value: viridisFill },
    { key: 'blues', value: bluesFill },
    { key: 'rainbow', value: rainbowFill },

    { key: 'cool inv', value: coolFillInverted },
    { key: 'warm inv', value: warmFillInverted },
    { key: 'viridis inv', value: viridisFillInverted },
    { key: 'blues inv', value: bluesFillInverted },
    { key: 'rainbow inv', value: rainbowFillInverted },
  ];
  fillFuncSelected: HierarchyTreeFillFunction = this.fillFuncArray[0].value;

  readonly fillOpacityFuncArray: FillOpacityFunc[] = [
    { key: 'opaque', value: opaqueFillOpacity },
    { key: 'linear', value: linearFillOpacity },
    { key: '90%', value: (d: any) => 0.9 },
    { key: '80%', value: (d: any) => 0.8 },
    { key: '70%', value: (d: any) => 0.7 },
    { key: '60%', value: (d: any) => 0.6 },
    { key: '50%', value: (d: any) => 0.5 },
    { key: '60%', value: (d: any) => 0.4 },
    { key: '70%', value: (d: any) => 0.3 },
    { key: '80%', value: (d: any) => 0.2 },
    { key: '10%', value: (d: any) => 0.1 },
    { key: 'transparent', value: transparentFillOpacity }
  ];
  fillOpacityFuncSelected: HierarchyTreeFillOpacityFunction = this.fillOpacityFuncArray[0].value;

  readonly strokeFuncArray: StrokeFunc[] = [
    { key: 'none', value: noStroke },
    { key: 'white', value: whiteStroke },
    { key: 'black', value: blackStroke },
    { key: 'transparent', value: transparentStroke },
  ];
  strokeFuncSelected: HierarchyTreeStrokeFunction = this.strokeFuncArray[0].value;

  readonly strokeWidthFuncArray: StrokeWidthFunc[] = [
    { key: 'thin', value: linearStrokeWidthThin },
    { key: 'normal', value: linearStrokeWidth },
    { key: 'thick', value: linearStrokeWidthThick },
    { key: 'extra thick', value: linearStrokeWidthExtraThick },
    { key: 'none', value: noStrokeWidth }
  ];
  strokeWidthFuncSelected: HierarchyTreeStrokeWidthFunction = this.strokeWidthFuncArray[0].value;

  readonly labelFuncArray: LabelFunc[] = [
    { key: 'name', value: nameLabels },
    { key: 'value', value: valueLabels },
    { key: 'none', value: emptyLabels }
  ];
  labelFuncSelected: HierarchyTreeLabelFunction = this.labelFuncArray[0].value;

  readonly labelMinRadiusArray: number[] = [ 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 16, 17, 18, 19, 20, 25, 30, 35, 40 ];
  labelMinRadiusSelected: number = this.labelMinRadiusArray[0];

  readonly labelFillArray: string[] = [ 'white', 'black', 'transparent' ];
  labelFillSelected: string = this.labelFillArray[0];

  readonly labelShadowArray: string[] = [ '0px 0px 8px #000000', '0px 0px 8px #ffffff', 'none' ];
  labelShadowSelected: string = this.labelShadowArray[0];

  readonly labelFontSizeFuncArray: LabelFontSizeFunc[] = [
    { key: 'linear', value: linearFontSize },
    { key: '18', value: equalFontSize18 },
    { key: '16', value: equalFontSize16 },
    { key: '14', value: equalFontSize14 },
    { key: '12', value: equalFontSize12 },
    { key: '10', value: equalFontSize10 },
    { key: '9', value: (t: any) => 9 },
    { key: '8', value: equalFontSize8 },
    { key: '7', value: (t: any) => 7 },
    { key: '6', value: (t: any) => 6 }
  ];
  labelFontSizeFuncSelected: HierarchyTreeFontSizeFunction = this.labelFontSizeFuncArray[0].value;

  selectedNodeInfo: string;
  tapFunc: HierarchyTreeTapFunction = (d: d3.HierarchyNode<HierarchyTreeNode>) => {
    const t = pathParentTooltips(d);
    const n = d.data as Omxn40HierarchyTreeNode;
    let text = `${d.data.name ? t : 'root'}: value: ${d.value}`;
    if (n.constituent) {
      const c = n.constituent;
      text += ` ticker: ${c.ticker}, isin: ${c.isin}, mic: ${c.mic}, name: ${c.name}, country: ${c.country},`;
      text += ` icb: ${c.icb}, icb industry: ${c.icbIndustry}, icb supersector: ${c.icbSupersector},`;
      text += ` morningstar sector: ${c.msSector}, morningstar industry: ${c.msIndustry}, morningstar stock style: ${c.msStockStyle},`;
      text += ` price: EUR ${(c.close * c.ratio).toFixed(2)} (${c.currency} ${c.close}), turnover: EUR ${(c.turnover * c.ratio).toFixed(2)} (${c.currency} ${c.turnover}),`;
      text += ` transactions: ${c.transactions}, volume: ${c.volume},`;
      text += ` market cap: EUR ${(c.marketCap * c.ratio).toFixed(2)} Bn (${c.currency} ${c.marketCap} Bn),`;
      text += ` description: ${c.description}`;
    }
    this.selectedNodeInfo = text;
  };
}
