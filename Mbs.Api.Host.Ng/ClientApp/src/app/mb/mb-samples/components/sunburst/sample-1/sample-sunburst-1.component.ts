import { Component } from '@angular/core';
import * as d3 from 'd3';

import { HierarchyTreeNode, linearFillOpacity, nameLabels } from '../../../../../shared/mbs/charts/hierarchy-tree';
import { flare } from '../../../test-data/hierarchies/flare';
import { jdk } from '../../../test-data/hierarchies/jdk';
// import { countries } from '../../../test-data/hierarchies/countries';

@Component({
  selector: 'mb-sample-sunburst-1',
  templateUrl: './sample-sunburst-1.component.html',
  styleUrls: ['./sample-sunburst-1.component.scss']
})
export class SampleSunburst1Component {

  flareHierarchy = jdk;

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
  linearOpacity = linearFillOpacity;
  nameLabel = nameLabels;

  tapFunc = (d: d3.HierarchyNode<HierarchyTreeNode>) => console.log(d);
}
