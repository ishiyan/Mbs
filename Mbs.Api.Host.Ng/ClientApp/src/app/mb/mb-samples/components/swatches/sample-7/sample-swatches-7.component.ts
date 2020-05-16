import { Component } from '@angular/core';

import { colorsCoAll5Palettes } from '../../../../../shared/mbs/colors/colors-co-all-5-palettes';
import { colorsCoAll5PalettesSelection } from '../../../../../shared/mbs/colors/colors-co-all-5-palettes-selection';

@Component({
  selector: 'mb-sample-swatches-7',
  templateUrl: './sample-swatches-7.component.html',
  styleUrls: ['./sample-swatches-7.component.scss']
})
export class SampleSwatches7Component {

  private sequence = 13254;
  get paletteSequenceLength(): number {
    return this.sequence;
  }
  set paletteSequenceLength(value: number) {
    this.sequence = value;
    this.palettesSequence = colorsCoAll5PalettesSelection(this.sequence.toString());
    this.selectedPaletteSequence = this.palettesSequence[this.selectedPaletteIndex];
  }

  palettesSequence: string[][] = colorsCoAll5PalettesSelection(this.sequence.toString());
  selectedPaletteIndex = 0;
  selectedPaletteSequence: string[] = this.palettesSequence[this.selectedPaletteIndex];

  palettesAll: string[][] = colorsCoAll5Palettes;
  selectedPalettesAllIndex = 0;
  selectedPalettesAll: string[] = this.palettesAll[this.selectedPalettesAllIndex];

  selectionChanged(selection: string[]) {
    this.selectedPaletteIndex = this.palettesSequence.indexOf(selection);
    this.selectedPaletteSequence = selection;
  }

  selectionAllChanged(selection: string[]) {
    this.selectedPalettesAllIndex = this.palettesAll.indexOf(selection);
    this.selectedPalettesAll = selection;
  }
}
