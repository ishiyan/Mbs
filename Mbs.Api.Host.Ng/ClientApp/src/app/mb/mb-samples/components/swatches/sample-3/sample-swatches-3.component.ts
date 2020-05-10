import { Component } from '@angular/core';

import { materialPalettesA } from '../../../../../shared/mbs/colors/material-palettes-a';

@Component({
  selector: 'mb-sample-swatches-3',
  templateUrl: './sample-swatches-3.component.html',
  styleUrls: ['./sample-swatches-3.component.scss']
})
export class SampleSwatches3Component {

  materialA7421Palettes(sequence: string): string[][]{
    return materialPalettesA(sequence);
  }

  palettes: string[][] =  materialPalettesA('7241');
  selectedPalette: string[] = this.palettes[0];

  selectionChanged(selection: string[]) {
    console.log('selection changed', selection);
  }
}
