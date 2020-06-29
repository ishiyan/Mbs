import { Component } from '@angular/core';

import { nameLabels } from '../../../../shared/mbs/charts/hierarchy-tree/functions/label-function';

import { icbTaxonomy } from '../../../../shared/mbs/instruments/industry-classification/icb-taxonomy';
import { gicsTaxonomy } from '../../../../shared/mbs/instruments/industry-classification/gics-taxonomy';

@Component({
  selector: 'mb-fixed-buckets',
  templateUrl: './buckets.component.html',
  styleUrls: ['./buckets.component.scss']
})
export class BucketsComponent {

  icb = icbTaxonomy;
  gics = gicsTaxonomy;

  namLabFunc = nameLabels;
}
