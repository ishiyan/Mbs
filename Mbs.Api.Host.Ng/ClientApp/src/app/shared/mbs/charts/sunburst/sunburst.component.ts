import { Component, Input, ElementRef, OnChanges, ChangeDetectionStrategy, ViewEncapsulation, HostListener } from '@angular/core';
import * as d3 from 'd3';

import { computeDimensions } from '../compute-dimensions';
import { HierarchyTreeSumFunction, HierarchyTreeNode } from '../hierarchy-tree';
import { sumNumberOfLeafNodes } from '../hierarchy-tree';
import { HierarchyTreeLabelFunction, emptyLabels } from '../hierarchy-tree';
import { HierarchyTreeTooltipFunction, pathTooltips } from '../hierarchy-tree';
import { HierarchyTreeTapFunction, doNothingTap } from '../hierarchy-tree';
import { HierarchyTreeFillFunction, rainbowMiddleFill } from '../hierarchy-tree';
import { HierarchyTreeFillOpacityFunction, linearFillOpacity } from '../hierarchy-tree';
import { SunburstConfiguration } from './sunburst-configuration.interface';

const twoPi = 2 * Math.PI;
const gradPerRad = 180 / Math.PI;
const defaultDiameter = 300;
const allLevels = 0;
const defaultTransitionMsec = 750;
const defaultFontSize = 9;

@Component({
  selector: 'mb-sunburst',
  templateUrl: './sunburst.component.html',
  styleUrls: ['./sunburst.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None
})
export class SunburstComponent implements OnChanges {
  private currentConfiguration: SunburstConfiguration = {
    fillColor: 'steelblue', strokeColor: undefined, strokeWidth: 1, interpolation: 'linear'
  };

  /**
   * Defines a value function returning a non-negative number which will be called by the **sum**
   * method of the **d3.HierarchyNode<Datum>** interface for all nodes in a hierarchy tree.
   */
  @Input() sumFunction: HierarchyTreeSumFunction = sumNumberOfLeafNodes;

  /** If the chart is zoomable. Tapping on a sector zooms in, tapping in the center zooms out. */
  @Input() zoomable: boolean = false;

  /** Zoomable transition duration in milliseconds. */
  @Input() transitionMsec: number = defaultTransitionMsec;

  /** A number of hierarchy levels to display or **0** to display all levels. */
  @Input() levels: number = allLevels;

  /** A function returning a text string which will be displayed as a label for a node. */
  @Input() labelFunction: HierarchyTreeLabelFunction = emptyLabels;

  /** A font size used to draw the labels. */
  @Input() fontSize: number = defaultFontSize;

  /** A function returning a text string which will be displayed as a tooltip for a node. */
  @Input() tooltipFunction: HierarchyTreeTooltipFunction = pathTooltips;

  /** A function called when a node is tapped or clicked allowing to display a node information. */
  @Input() tapFunction: HierarchyTreeTapFunction = doNothingTap;

  /** A function returning a fill color of a node. */
  @Input() fillFunction: HierarchyTreeFillFunction = rainbowMiddleFill;

  /** A function returning a fill color opacity of a node. */
  @Input() fillOpacityFunction: HierarchyTreeFillOpacityFunction = linearFillOpacity;

  /** A diameter of the sunburst. */
  @Input() diameter: number | string = defaultDiameter;

  /** Specifies fill, stroke and interpolation. */
  @Input() set configuration(cfg: SunburstConfiguration) {
    if (cfg && cfg != null) {
      this.currentConfiguration = { ...this.currentConfiguration, ...cfg };
    }
  }
  get configuration(): SunburstConfiguration {
    return this.currentConfiguration;
  }

  /** The data hierarchy to use. */
  @Input() data: HierarchyTreeNode;

  constructor(private elementRef: ElementRef) { }

  ngOnChanges(changes: any) {
    this.render();
  }

  @HostListener('window:resize', [])
  public render(): void {
    const sel = d3.select(this.elementRef.nativeElement);
    sel.select('svg').remove();
    const dat = this.data;
    if (!dat || !dat.children || dat.children.length < 1) {
      return;
    }
    const computed = computeDimensions(this.elementRef, this.diameter, this.diameter, defaultDiameter, defaultDiameter);
    const s = Math.max(computed[0], computed[1]);
    const s2 = s / 2;
    const format = d3.format(',d');
    const svg: any = sel.append('svg').attr('preserveAspectRatio', 'xMinYMin meet')
      .attr('width', s).attr('height', s).attr('viewBox', `0 0 ${s} ${s}`);
    const g = svg.append('g')
      .attr('transform', `translate(${s2},${s2})`);

    const partition = (d: HierarchyTreeNode) => {
      const root = d3.hierarchy(d)
        .sum(this.sumFunction)
        .sort((a: any, b: any) => ((b && b.value)? b.value : 0) - ((a && a.value)? a.value : 0));
      return d3.partition()
        .size([twoPi, root.height + 1])(root);
    };
    const root = partition(dat);
    root.each((d: any) => d.current = d);

    const n = (this.levels < 1 ? root.height : this.levels) + 1;
    const radius = s / (2 * n);
    const arc = d3.arc<HierarchyTreeNode>()
      .startAngle((d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => d.x0)
      .endAngle((d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => d.x1)
      .padAngle((d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => Math.min((d.x1 - d.x0) / 2, 0.005))
      .padRadius(radius * 1.5)
      .innerRadius((d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => d.y0 * radius)
      .outerRadius((d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => Math.max(d.y0 * radius, d.y1 * radius - 1));

    const arcVisible = (d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => d.y1 <= n && d.y0 >= 1 && d.x1 > d.x0;
    const labelVisible = (d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => d.y1 <= n && d.y0 >= 1 && (d.y1 - d.y0) * (d.x1 - d.x0) > 0.03;
    const labelTransform = (d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => {
      const x = (d.x0 + d.x1) / 2 * gradPerRad;
      const y = (d.y0 + d.y1) / 2 * radius;
      return `rotate(${x - 90}) translate(${y},0) rotate(${x < 180 ? 0 : 180})`;
    };

    const clicked = (p: d3.HierarchyRectangularNode<HierarchyTreeNode>) => {
      this.tapFunction(p);
      if (this.zoomable) {
        parent.datum(p.parent || root);
        root.each((d: any) => d.target = {
          x0: Math.max(0, Math.min(1, (d.x0 - p.x0) / (p.x1 - p.x0))) * twoPi,
          x1: Math.max(0, Math.min(1, (d.x1 - p.x0) / (p.x1 - p.x0))) * twoPi,
          y0: Math.max(0, d.y0 - p.depth),
          y1: Math.max(0, d.y1 - p.depth)
        });
        const t = g.transition().duration(this.transitionMsec);
        // Transition the data on all arcs, even the ones that aren’t visible,
        // so that if this transition is interrupted, entering arcs will start
        // the next transition from the desired position.
        path.transition(t)
          .tween('data', (d: any) => {
              const i = d3.interpolate(d.current, d.target);
              return (t: any) => d.current = i(t);
          })
          .filter((d: any) => arcVisible(d.current) || arcVisible(d.target))
          .attr('fill-opacity', (d: any) => arcVisible(d.target) ?
            this.fillOpacityFunction(d as d3.HierarchyRectangularNode<HierarchyTreeNode>, root.height) : 0)
          .attrTween('d', (d: any) => () => arc(d.current));
  
        label.filter((d: any) => arcVisible(d.current) || labelVisible(d.target)).transition(t)
          .attr('fill-opacity', (d: any) => +labelVisible(d.target))
          .attrTween('transform', (d: any) => () => labelTransform(d.current));
      }
    };

    const path = g.append('g')
      .selectAll('path')
      .data(root.descendants().slice(1))
      .join('path')
        .attr('fill', (d: d3.HierarchyRectangularNode<HierarchyTreeNode>) => this.fillFunction(d))
        .attr('fill-opacity', (d: any) => arcVisible(d.current) ?
          this.fillOpacityFunction(d as d3.HierarchyRectangularNode<HierarchyTreeNode>, root.height) : 0)
        .attr('d', (d: any) => arc(d.current));

    if (this.zoomable) {
      path.filter((d: d3.HierarchyNode<HierarchyTreeNode>) => d.children)
        .style('cursor', 'pointer')
        .on('click', clicked);
    } else {
      path.filter((d: d3.HierarchyNode<HierarchyTreeNode>) => d.parent !== null)
        .on('click', clicked);
    }

    path.append('title')
      .text((d: d3.HierarchyNode<HierarchyTreeNode>) => this.tooltipFunction(d));

    const label = g.append("g")
        .attr('pointer-events', 'none')
        .attr('text-anchor', 'middle')
        .style('user-select', 'none')
        .attr('font-size', this.fontSize)
      .selectAll('text')
      .data(root.descendants().slice(1))
      .join('text')
        .attr('dy', '0.35em')
        .attr('fill-opacity', (d: any) => +labelVisible(d.current))
        .attr('transform', (d: any) => labelTransform(d.current))
        .text((d: d3.HierarchyNode<HierarchyTreeNode>) => this.labelFunction(d));

    const parent = g.append("circle")
      .datum(root)
      .attr("r", radius)
      .attr("fill", "none")
      .attr("pointer-events", "all");
    if (this.zoomable) {
      parent.on("click", clicked);      
    }
  }
}
