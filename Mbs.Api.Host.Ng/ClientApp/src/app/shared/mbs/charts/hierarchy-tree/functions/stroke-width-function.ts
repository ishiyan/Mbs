import * as d3 from 'd3';

import { HierarchyTreeNode } from '../hierarchy-tree';

/**
 * Defines a function called when a node is displayed and returning a number representing a stroke width.
 */
export type HierarchyTreeStrokeWidthFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) => number;

/**
 * Provides an implementation of the *'no stroke width'* **HierarchyTreeStrokeWidthFunction** type.
 */
export const noStrokeWidth: HierarchyTreeStrokeWidthFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) => 0;

/**
 * Provides an implementation of the *'linear stroke width'* **HierarchyTreeStrokeWidthFunction** type.
 */
export const linearStrokeWidth: HierarchyTreeStrokeWidthFunction =
  (d: d3.HierarchyRectangularNode<HierarchyTreeNode> | d3.HierarchyCircularNode<HierarchyTreeNode>) =>
  d.depth < 2 ? 1 : (d.depth < 3 ? 0.5 : (d.depth < 4 ? 0.1 : 0));
