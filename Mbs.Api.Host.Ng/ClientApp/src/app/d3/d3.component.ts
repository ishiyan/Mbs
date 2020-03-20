import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {NestedTreeControl} from '@angular/cdk/tree';
import {MatTreeNestedDataSource} from '@angular/material/tree';

import { D3Sample } from './d3-samples/d3-sample';
import { treeNodes } from './d3-samples/d3-samples';

@Component({
  selector: 'app-d3',
  templateUrl: './d3.component.html',
  styleUrls: ['./d3.component.scss']
})
export class D3Component {
  public sample: D3Sample= treeNodes[0];
  public treeControl = new NestedTreeControl<D3Sample>(node => node.children);
  public dataSource = new MatTreeNestedDataSource<D3Sample>();
  public hasChild = (_: number, node: D3Sample) => !!node.children && node.children.length > 0;

  constructor(router: Router) {
    const routeUrl = router.routerState.snapshot.url;
    for (let i = 0; i < treeNodes.length; ++i) {
      const n = D3Component.findEqual(treeNodes[i], routeUrl);
      if (n) {
        this.sample = n;
        break;
      }
    }
    if (!this.sample) {
      for (let i = 0; i < treeNodes.length; ++i) {
        const n = D3Component.findFirst(treeNodes[i]);
        if (n) {
          this.sample = n;
          break;
        }
      }
    }
    this.dataSource.data = treeNodes;
  }

  private static findEqual(node: D3Sample, routeUrl: string): D3Sample | undefined {
    if (node.route) {
      const url = '/d3/' + node.route;
      if (routeUrl === url) {
        return node;
      }
    }
    if (node.children) {
      for (let i = 0; i < node.children.length; ++i) {
        const n = D3Component.findEqual(node.children[i], routeUrl);
        if (n) {
          return n;
        }
      }
    }
    return undefined;
  }

  private static findFirst(node: D3Sample): D3Sample | undefined {
    if (node.route) {
      return node;
    }
    if (node.children) {
      for (let i = 0; i < node.children.length; ++i) {
        const n = D3Component.findFirst(node.children[i]);
        if (n) {
          return n;
        }
      }
    }
    return undefined;
  }
}
