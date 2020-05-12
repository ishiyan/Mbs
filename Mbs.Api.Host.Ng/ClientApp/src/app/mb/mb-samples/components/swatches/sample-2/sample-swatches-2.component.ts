import { Component } from '@angular/core';

import { materialPalettesA } from '../../../../../shared/mbs/colors/material-palettes-a';
import { materialPalettes } from '../../../../../shared/mbs/colors/material-palettes';

@Component({
  selector: 'mb-sample-swatches-2',
  templateUrl: './sample-swatches-2.component.html',
  styleUrls: ['./sample-swatches-2.component.scss']
})
export class SampleSwatches2Component {

  private sequenceA = 7241;
  get paletteSequenceA(): number {
    return this.sequenceA;
  }
  set paletteSequenceA(value: number) {
    this.sequenceA = value;
    this.palettesA = materialPalettesA(this.sequenceA.toString());
  }

  private sequence = 9785634120;
  get paletteSequence(): number {
    return this.sequence;
  }
  set paletteSequence(value: number) {
    this.sequence = value;
    this.palettes = materialPalettes(this.sequence.toString());
  }

  palettesAOrdered: string[][] = materialPalettesA('7421');
  palettesA: string[][] = materialPalettesA(this.sequenceA.toString());
  selectedPaletteA: string[] = this.palettesA[0];

  palettesOrdered: string[][] = materialPalettes('9876543210');
  palettes: string[][] = materialPalettes(this.sequence.toString());
  selectedPalette: string[] = this.palettes[0];

  selectionChangedA(selection: string[]) {
    console.log('selection changed', selection);
  }

  selectionChanged(selection: string[]) {
    console.log('selection changed', selection);
  }
}
