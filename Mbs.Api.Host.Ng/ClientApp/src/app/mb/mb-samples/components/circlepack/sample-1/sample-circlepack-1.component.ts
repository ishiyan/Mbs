import { Component } from '@angular/core';
import * as d3 from 'd3';

// tslint:disable:max-line-length
import { HierarchyTreeNode } from '../../../../../shared/mbs/charts/hierarchy-tree/hierarchy-tree';
import { HierarchyTreeSumFunction, sumNodeValues, sumNumberOfLeafNodes } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/sum-function';
import { HierarchyTreeFillFunction, coolFill, coolFillInverted, rainbowFill, rainbowFillInverted } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/fill-function';
import { HierarchyTreeFillOpacityFunction, transparentFillOpacity, opaqueFillOpacity, linearFillOpacity } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/fill-opacity-function';
import { HierarchyTreeStrokeFunction, noStroke, blackStroke } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/stroke-function';
import { HierarchyTreeStrokeWidthFunction, noStrokeWidth, linearStrokeWidth } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/stroke-width-function';
import { HierarchyTreeTapFunction } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/tap-function';
import { HierarchyTreeLabelFunction, nameLabels, valueLabels, emptyLabels } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/label-function';
import { HierarchyTreeFontSizeFunction, equalFontSize8, equalFontSize10, equalFontSize12, equalFontSize14, equalFontSize16, equalFontSize18, linearFontSize } from '../../../../../shared/mbs/charts/hierarchy-tree/functions/font-size-function';

import { CountryHierarchyTreeNode, countries } from '../../../test-data/hierarchies/countries';
import { flare } from '../../../test-data/hierarchies/flare';
import { jdk } from '../../../test-data/hierarchies/jdk';

@Component({
  selector: 'mb-sample-circlepack-1',
  templateUrl: './sample-circlepack-1.component.html',
  styleUrls: ['./sample-circlepack-1.component.scss']
})
export class SampleCirclepack1Component {

  flareHierarchy = flare;
  sumFunc = sumNodeValues;
  rainbowFillFunc = rainbowFill;
  linearOpacity = linearFillOpacity;
  nameLabel = nameLabels;

  rainbowInterpolate = d3.scaleOrdinal(d3.quantize(d3.interpolateRainbow, this.flareHierarchy.children ? this.flareHierarchy.children.length : 1 + 1));
  /**
   * Provides an implementation of the **HierarchyTreeFillFunction** interface which will uss the
   * **d3.interpolateRainbow** to fill a node acconding to the ordinal of the first-level parent of a node.
   */
  rainbowRootFill = (d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => {
    let first: d3.HierarchyRectangularNode<HierarchyTreeNode> = d;
    while (first.depth > 1 && first.parent !== null) {
      first = first.parent;
    }
    return this.rainbowInterpolate(first.data.name ? first.data.name : '');
  };

  tapFunc = (d: d3.HierarchyNode<HierarchyTreeNode>) => console.log(d);
}
