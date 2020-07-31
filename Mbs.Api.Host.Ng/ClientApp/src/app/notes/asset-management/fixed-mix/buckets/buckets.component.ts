import { Component } from '@angular/core';

import { nameLabels } from '../../../../shared/mbs/charts/hierarchy-tree/functions/label-function';
import { sumNumberOfLeafNodes } from '../../../../shared/mbs/charts/hierarchy-tree/functions/sum-function';
import { noStroke } from '../../../../shared/mbs/charts/hierarchy-tree/functions/stroke-function';
import { coolFill, coolFillFirstLevel } from '../../../../shared/mbs/charts/hierarchy-tree/functions/fill-function';

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
  fillFunc = coolFill;
  fillFunc2 = coolFillFirstLevel;
  sumFunc = sumNumberOfLeafNodes;
  strokeFunc = noStroke;
  labelFontSizeFunc = () => 12;
  labelFontSizeFuncLarge = () => 16;
}
