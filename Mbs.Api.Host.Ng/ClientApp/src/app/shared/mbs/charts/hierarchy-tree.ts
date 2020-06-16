import * as d3 from 'd3';

export interface HierarchyTreeNode {
  value?: number;
  name?: string;
  children?: HierarchyTreeNode[];
};

/**
 * Defines a value function returning a non-negative number which will be called by the
 * **sum** method of the **d3.HierarchyNode<Datum>** interface for all nodes in a hierarchy tree.
 *
 * The relative sizes of all nodes will be defined based on values this method returns.
 * Several simple implementations of this method are provided.
 * - **sumNumberOfLeafNodes** will return **0** or **1** per node depending on the presense of child nodes.
 * This will result in node sizes being proportional to the number of leaf sub-nodes.
 * - **sumNumberOfNodes** will return **1** for all hierarchy tree nodes.
 * This will result in node sizes being proportional to the number of sub-nodes.
 * - **sumNodeValues** will return the numeric value of the **value** property or **0** if it is undefined.
 * This will result in node sizes being proportional to the cumulative value of sub-nodes.
 */
export interface HierarchyTreeSumFunction
{
    (d: HierarchyTreeNode): number;
};

/**
 * Provides an implementation of the **HierarchyTreeSumFunction** interface.
 *
 * This function returns **0** or **1** per hierarchy tree node depending on the presense of child nodes.
 *
 * This will result in node sizes being proportional to the number of leaf nodes.
 */
export const sumNumberOfLeafNodes: HierarchyTreeSumFunction = (d: HierarchyTreeNode) => d.children ? 0 : 1;

/**
 * Provides an implementation of the **HierarchyTreeSumFunction** interface.
 *
 * This function returns **1** for all hierarchy tree nodes.
 *
 * This will result in node sizes being proportional to the number of sub-nodes.
 */
export const sumNumberOfNodes: HierarchyTreeSumFunction = (d: HierarchyTreeNode) => 1;

/**
 * Provides an implementation of the **HierarchyTreeSumFunction** interface.
 *
 * For each node, this function returns the numeric value of the **value** property or **0** if it is undefined.
 *
 * This will result in node sizes being proportional to the cumulative value of sub-nodes.
 */
export const sumNodeValues: HierarchyTreeSumFunction = (d: HierarchyTreeNode) => d.value ? d.value : 0;

/**
 * Defines a function returning a text string which will be displayed as a label for a node. 
 *
 * Several simple implementations of this method are provided.
 * - **nameLabels** will return node name or an empty string if the name is undefined.
 * - **valueLabels** will return node name or an empty string if the name is undefined.
 * - **emptyLabels** will return an empty string for all nodes.
 */
export interface HierarchyTreeLabelFunction
{
    (d: d3.HierarchyNode<HierarchyTreeNode>): string;
};

/**
 * Provides an implementation of the **HierarchyTreeLabelFunction** interface.
 *
 * For each node, this function returns its name or an empty string if the name is undefined.
 */
export const nameLabels: HierarchyTreeLabelFunction = (d: d3.HierarchyNode<HierarchyTreeNode>) => d.data.name ? d.data.name : '';

/**
 * Provides an implementation of the **HierarchyTreeLabelFunction** interface.
 *
 * For each node, this function returns its value or an empty string if the value is undefined.
 */
export const valueLabels: HierarchyTreeLabelFunction = (d: d3.HierarchyNode<HierarchyTreeNode>) => d.data.value ? d.data.value.toString() : '';

/**
 * Provides an implementation of the **HierarchyTreeLabelFunction** interface.
 *
 * For each node, this function returns an empty string.
 */
export const emptyLabels: HierarchyTreeLabelFunction = (d: d3.HierarchyNode<HierarchyTreeNode>) => '';

/**
 * Defines a function returning a text string which will be displayed as a tooltip for a node. 
 *
 * Several simple implementations of this method are provided.
 * - **emptyTooltips** will return an empty string for all nodes.
 * - **pathTooltips** will return node path separated by '/' characters.
 * - **pathValueTooltips** will return node path separated by '/' characters and an optional value on a separate line.
 */
export interface HierarchyTreeTooltipFunction
{
    (d: d3.HierarchyNode<HierarchyTreeNode>): string;
};

/**
 * Provides an implementation of the **HierarchyTreeLabelFunction** interface.
 *
 * For each node, this function returns an empty string.
 */
export const emptyTooltips: HierarchyTreeLabelFunction = (d: d3.HierarchyNode<HierarchyTreeNode>) => '';

const format = d3.format(',d');

/**
 * Provides an implementation of the **HierarchyTreeLabelFunction** interface.
 *
 * For each node, this function returns its path separated by new lines.
 */
export const pathTooltips: HierarchyTreeLabelFunction = (d: d3.HierarchyNode<HierarchyTreeNode>) =>
  `${d.ancestors().map((d: d3.HierarchyNode<HierarchyTreeNode>) => d.data.name).reverse().join('\n')}`;

/**
 * Provides an implementation of the **HierarchyTreeLabelFunction** interface.
 *
 * For each node, this function returns its path separated by new lines and an optional value.
 */
export const pathValueTooltips: HierarchyTreeLabelFunction = (d: d3.HierarchyNode<HierarchyTreeNode>) =>
  `${d.ancestors().map((d: d3.HierarchyNode<HierarchyTreeNode>) => d.data.name).reverse().join('\n')}${d.data.value ? '\n' + format(d.data.value) : ''}`;

/**
 * Defines a function called when a node is tapped or clicked allowing to display a node information.
 * A zooming functionality is performed independently from this function.
 *
 * A default implementation does nothing when a node is tapped or clicked.
 */
export interface HierarchyTreeTapFunction
{
    (d: d3.HierarchyNode<HierarchyTreeNode>): void;
};

/**
 * Provides a *'do nothing'* implementation of the **HierarchyTreeTapFunction** interface.
 */
export const doNothingTap: HierarchyTreeTapFunction = (d: d3.HierarchyNode<HierarchyTreeNode>) => {};

/**
 * Defines a function called when a node is displayed and returning a string representing a fill color.
 */
export interface HierarchyTreeFillFunction
{
    (d: d3.HierarchyRectangularNode<HierarchyTreeNode>): string;
};

const fourPi = 4 * Math.PI;

/**
 * Provides an implementation of the **HierarchyTreeFillFunction** interface which will uss the
 * **d3.interpolateRainbow** to fill a node acconding to the middle angle of the angular sector it occupies.
 */
export const rainbowMiddleFill: HierarchyTreeFillFunction = (d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => {
  const t = (d.x1 + d.x0)/fourPi;
  return d3.interpolateRainbow(t);
  // return d3.interpolateViridis(t);
  // return d3.interpolateHslLong("yellow", "blue")(t);
};

/**
 * Defines a function called when a node is displayed and returning a fill color opacity number in the range of [0, 1].
 */
export interface HierarchyTreeFillOpacityFunction
{
    (d: d3.HierarchyRectangularNode<HierarchyTreeNode>, maxHeight: number): number;
};

/**
 * Provides an *'opaque'* implementation of the **HierarchyTreeFillOpacityFunction** interface.
 */
export const opaqueFillOpacity: HierarchyTreeFillOpacityFunction = (d: d3.HierarchyNode<HierarchyTreeNode>, maxHeight: number) => 1;

/**
 * Provides an *'linear'* implementation of the **HierarchyTreeFillOpacityFunction** interface.
 */
export const linearFillOpacity: HierarchyTreeFillOpacityFunction = (d: d3.HierarchyNode<HierarchyTreeNode>, maxHeight: number) => 1 - (d.depth - 1) / (maxHeight * 1.9);
