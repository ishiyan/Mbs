import * as d3 from 'd3';

import { HierarchyTreeNode } from '../hierarchy-tree';

/**
 * Defines a function called when a node is displayed and returning a string representing a fill color.
 */
export type HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) => string;

const fourPi = 4 * Math.PI;

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will uss the
 * **d3.interpolateRainbow** to fill a node acconding to the middle angle of the angular sector it occupies.
 */
export const rainbowMiddleFill: HierarchyTreeFillFunction = (d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => {
  const t = (d.x1 + d.x0) / fourPi;
  return d3.interpolateRainbow(t);
  // return d3.interpolateViridis(t);
  // return d3.interpolateHslLong("yellow", "blue")(t);
};

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will uss the
 * **d3.interpolateRainbow** to fill a node acconding to the middle angle of the angular sector it occupies.
 */
export const rainbowFill2: HierarchyTreeFillFunction = (d: d3.HierarchyCircularNode<HierarchyTreeNode>) => {
  let firstLevel = d;
  while (firstLevel.depth > 1 && firstLevel.parent != null) {
    firstLevel = firstLevel.parent;
  }
  let root = d;
  while (root.parent != null) {
    root = root.parent;
  }
  const rootChildren = root.children ? root.children : [];
  // const rainbowInterpolate = d3.scaleOrdinal(d3.quantize(d3.interpolateRainbow, rootChildren.length + 1));
  const index = rootChildren.indexOf(firstLevel);
  const t = index ? index / (rootChildren.length + 1) : 0;
  // const t = index ? index / rootChildren.length : 0;
  // let theColor = d3.hsl(d3.interpolateGnBu(t)).darker();
  // d3.hsl(d3.interpolateBlues(t)).darker()
  const theColor = d3.hsl(d3.interpolateBuGn(t))/*.brighter()*/; // rainbowInterpolate((index ? index : 0).toString()));
  // let theColor = d3.hsl(d3.interpolateViridis(1 - t)); // rainbowInterpolate((index ? index : 0).toString()));
  // let theColor = d3.hsl(d3.interpolateHslLong("lightgreen", "darkblue")(t));
  const depth = d.depth;
  // console.log(theColor.hex(), depth, '==>');
  theColor.l += depth === 1 ? 0 : depth * .1;
  // console.log(theColor.hex());
  return theColor.hex();
  // return d3.interpolateViridis(t);
  // return d3.interpolateHslLong("yellow", "blue")(t);
};

const brighter = 1;
const darker = 2;

const interpolationFill = (d: d3.HierarchyNode<HierarchyTreeNode>, func: any, invert: boolean = false, brighterOrDarker: number = 0) => {
  let firstLevel = d;
  while (firstLevel.depth > 1 && firstLevel.parent != null) {
    firstLevel = firstLevel.parent;
  }
  let root = d;
  while (root.parent != null) {
    root = root.parent;
  }
  const rootChildren = root.children ? root.children : [];
  const index = rootChildren.indexOf(firstLevel);
  const t = index ? index / (rootChildren.length + 1) : 0;
  // const t = index ? index / rootChildren.length : 0;
  let theColor = invert ? d3.hsl(func(1 - t)) : d3.hsl(func(t));
  if (brighterOrDarker === brighter) {
    theColor = theColor.brighter();
  } else if (brighterOrDarker === darker) {
    theColor = theColor.darker();
  }
  const depth = d.depth;
  theColor.l += depth === 1 ? 0 : depth * .1;
  return theColor.hex();
};

const interpolationFillInverted = (d: d3.HierarchyNode<HierarchyTreeNode>, func: any) => {
  let firstLevel = d;
  while (firstLevel.depth > 1 && firstLevel.parent != null) {
    firstLevel = firstLevel.parent;
  }
  let root = d;
  while (root.parent != null) {
    root = root.parent;
  }
  const rootChildren = root.children ? root.children : [];
  const index = rootChildren.indexOf(firstLevel);
  const t = index ? index / (rootChildren.length + 1) : 0;
  // const t = index ? index / rootChildren.length : 0;
  const theColor = d3.hsl(func(1 - t));
  const depth = d.depth;
  theColor.l += depth === 1 ? 0 : depth * .1;
  return theColor.hex();
};

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will use the
 * **d3.interpolateCool** to fill a node acconding to the first-level node number and then makes
 * color lighter depending on a node depth.
 */
export const coolFill: HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  interpolationFill(d, d3.interpolateCool);

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will use the
 * inverted **d3.interpolateCool** to fill a node acconding to the first-level node number and
 * then makes color lighter depending on a node depth.
 */
export const coolFillInverted: HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  interpolationFillInverted(d, d3.interpolateCool);

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will use the
 * **d3.interpolateWarm** to fill a node acconding to the first-level node number and then makes
 * color lighter depending on a node depth.
 */
export const warmFill: HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  interpolationFill(d, d3.interpolateWarm);

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will use the
 * inverted **d3.interpolateWarm** to fill a node acconding to the first-level node number and
 * then makes color lighter depending on a node depth.
 */
export const warmFillInverted: HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  interpolationFill(d, d3.interpolateWarm, true, darker);

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will use the
 * **d3.interpolateViridis** to fill a node acconding to the first-level node number and then makes
 * color lighter depending on a node depth.
 */
export const viridisFill: HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  interpolationFill(d, d3.interpolateViridis);

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will use the
 * inverted **d3.interpolateViridis** to fill a node acconding to the first-level node number and
 * then makes color lighter depending on a node depth.
 */
export const viridisFillInverted: HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  interpolationFill(d, d3.interpolateViridis, true, darker);

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will use the
 * **d3.interpolateBlues** to fill a node acconding to the first-level node number and then makes
 * color lighter depending on a node depth.
 */
export const bluesFill: HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  interpolationFill(d, d3.interpolateBlues, false, darker);

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will use the
 * inverted **d3.interpolateBlues** to fill a node acconding to the first-level node number and
 * then makes color lighter depending on a node depth.
 */
export const bluesFillInverted: HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  interpolationFill(d, d3.interpolateBlues, true, darker);

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will use the
 * **d3.interpolateRainbow** to fill a node acconding to the first-level node number and then makes
 * color lighter depending on a node depth.
 */
export const rainbowFill: HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  interpolationFill(d, d3.interpolateRainbow);

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** type which will use the
 * inverted **d3.interpolateRainbow** to fill a node acconding to the first-level node number and
 * then makes color lighter depending on a node depth.
 */
export const rainbowFillInverted: HierarchyTreeFillFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  interpolationFill(d, d3.interpolateRainbow, true);
