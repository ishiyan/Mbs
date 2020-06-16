import { Component } from '@angular/core';
import { icbTaxonomy } from '../../../../shared/mbs/instruments/industry-classification/icb-taxonomy';
import { gicsTaxonomy } from '../../../../shared/mbs/instruments/industry-classification/gics-taxonomy';
import { HierarchyTreeLabelFunction, emptyLabels, nameLabels, valueLabels } from '../../../../shared/mbs/charts/hierarchy-tree';
import { flare } from './flare';
import { countries } from './countries';

@Component({
  selector: 'mb-fixed-buckets',
  templateUrl: './buckets.component.html',
  styleUrls: ['./buckets.component.scss']
})
export class BucketsComponent {

  icb = icbTaxonomy;
  gics = gicsTaxonomy;
  dat = flare;
  countr = countries;

  namLabFunc = nameLabels;
  /*private constructor() {
    console.log(icbTaxonomy);
    console.log(gicsTaxonomy);
  }*/
}
