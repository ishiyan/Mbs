import { Component } from '@angular/core';
import { MatSelectChange } from '@angular/material/select';

import { huge5Palettes } from './huge-palette-5';
import { sequential5Palettes } from './sequential-5-palettes';

@Component({
  selector: 'mb-sample-swatches-1',
  templateUrl: './sample-swatches-1.component.html',
  styleUrls: ['./sample-swatches-1.component.scss']
})
export class SampleSwatches1Component {

  readonly colors2: string[] = [ '#5C849F', '#93B2C1', '#6BAEA7', '#73C0DC' ];

  readonly colors1: string[] = [ '#31B9B0', '#5AF0BC', '#32D2BA', '#26BBBE', '#32BEA1' ];

  readonly swatchesArray1: string[][] = [ this.colors1, this.colors2 ];
  swatchesSelected1: string[] = this.swatchesArray1[1];

  readonly huge5SwatchesArray = huge5Palettes;
  huge5SwatchesSelected = this.huge5SwatchesArray[0];

  readonly sequential5SwatchesArray = sequential5Palettes;
  sequential5SwatchesSelected = this.sequential5SwatchesArray[0];

  selectionChanged(selection: MatSelectChange) {
    console.log('selection changed', selection);
  }
}
