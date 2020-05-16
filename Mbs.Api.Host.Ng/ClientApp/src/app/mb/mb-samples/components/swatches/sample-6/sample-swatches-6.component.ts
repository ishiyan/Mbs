import { Component } from '@angular/core';

import { colorsCoSequential5Palettes } from '../../../../../shared/mbs/colors/colors-co-sequential-5-palettes';
import { colorsCoSequential5PalettesSelection } from '../../../../../shared/mbs/colors/colors-co-sequential-5-palettes-selection';

@Component({
  selector: 'mb-sample-swatches-6',
  templateUrl: './sample-swatches-6.component.html',
  styleUrls: ['./sample-swatches-6.component.scss']
})
export class SampleSwatches6Component {

  private sequence = 13254;
  get paletteSequenceLength(): number {
    return this.sequence;
  }
  set paletteSequenceLength(value: number) {
    this.sequence = value;
    this.palettesSequence = colorsCoSequential5PalettesSelection(this.sequence.toString());
    this.selectedPaletteIndex = this.palettesSequence.indexOf(this.selectedPaletteSequence);
    this.selectedPaletteSequence = this.palettesSequence[this.selectedPaletteIndex];
  }

  palettesSequence: string[][] = colorsCoSequential5PalettesSelection(this.sequence.toString());
  selectedPaletteIndex = 0;
  selectedPaletteSequence: string[] = this.palettesSequence[this.selectedPaletteIndex];

  palettesAll: string[][] = colorsCoSequential5Palettes;
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
