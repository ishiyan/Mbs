import { Component } from '@angular/core';
import { MatSelectChange } from '@angular/material/select';
import * as d3 from 'd3';

import { huge5Palettes } from './huge-palette-5';
import { sequential5Palettes } from './sequential-5-palettes';

// -----------------------------------------------------
function randomDate(now: number): any {
  // return a pseudo random dates, should be tweaked
  return new Date(now * Math.random()).getTime();
}
function rgbString(color: any): string {
  return `rgb(${Math.floor(color.r*255)}, ${Math.floor(color.g*255)}, ${Math.floor(color.b*255)})`
}
function SampleFromColorScheme(r1: number, r2: number, [color1, color2, color3]: any[]): any {
  // Return a sample from our generated colorScheme
  return {
    r : (1. - Math.sqrt(r1)) * color1.r + (Math.sqrt(r1)*(1. - r2))* color2.r
    + (r2*Math.sqrt(r1)) * color3.r,
    g : (1. - Math.sqrt(r1)) * color1.g + (Math.sqrt(r1)*(1. - r2))* color2.g
    + (r2*Math.sqrt(r1)) * color3.g,
    b : (1. - Math.sqrt(r1)) * color1.b + (Math.sqrt(r1)*(1. - r2))* color2.b
    + (r2*Math.sqrt(r1)) * color3.b,
  }
}
function generateColor(iTime: number): any {
  // return a random color based on the algorithm
  return {
    r : Math.abs(Math.sin(iTime+12.) + Math.sin(iTime*0.7 + 71.124)*0.5),
    g : Math.abs(Math.sin(iTime) + Math.sin(iTime*0.8 + 41.)*0.5),
    b: Math.abs(Math.sin(iTime+61.) + Math.sin(iTime*0.8 + 831.32)*0.5)
  }
}
function generateColors(n: number, [color1, color2, color3]: any[]){
  // generate n colors from the given scheme
  const colors: any[] = [];
  for (let i = 0; i < n; ++i) {
    colors.push(SampleFromColorScheme(i/n, i/n, [color1, color2, color3]));
  }
  // I like to sort them by luminance, feel free to sort them as you prefer
  const hslColors = colors.map(c => d3.hcl(rgbString(c)));
  colors.sort((a,b) => hslColors[colors.indexOf(a)].l - hslColors[colors.indexOf(b)].l)
  return colors
}
function getRandomScheme(n: number): any {
  // generate a random color scheme from 3 starting colors
  const startingColors = [
    generateColor(randomDate(Date.now())),
    generateColor(randomDate(Date.now())),
    generateColor(randomDate(Date.now()))
  ]
  return generateColors(n, startingColors);
}
function componentToHex(c: any): string {
  c = Math.min(Math.max(0, c), 255);
  const hex = c.toString(16);
  return hex.length === 1 ? '0' + hex : hex;
}
function rgbToHex(r: any, g: any, b: any): string {
  return '#' + componentToHex(r) + componentToHex(g) + componentToHex(b);
}
function randomSwatches(): string[][] {
  // https://medium.com/@shahriyarshahrabi/procedural-color-algorithm-a37739f6dc1
  // https://observablehq.com/@tarte0/color-palettes-generator

  const palette: string[][] = [];
  const startingColors = [
    generateColor(randomDate(Date.now())),
    generateColor(randomDate(Date.now())),
    generateColor(randomDate(Date.now()))
  ];
  for (let k = 2; k < 20; ++k) {
    const swatches = generateColors(k, startingColors)
      .map(c => rgbToHex(Math.floor(c.r * 255), Math.floor(c.g * 255), Math.floor(c.b * 255)));
    palette.push(swatches);
  }
  return palette;
}
// -----------------------------------------------------

@Component({
  selector: 'mb-sample-swatches-1',
  templateUrl: './sample-swatches-1.component.html',
  styleUrls: ['./sample-swatches-1.component.scss']
})
export class SampleSwatches1Component {

  selectionChanged(selection: MatSelectChange) {
    console.log('selection changed', selection);
  }

  readonly colors2: string[] = [ '#5C849F', '#93B2C1', '#6BAEA7', '#73C0DC' ];

  readonly colors1: string[] = [ '#31B9B0', '#5AF0BC', '#32D2BA', '#26BBBE', '#32BEA1' ];

  readonly swatchesArray1: string[][] = [ this.colors1, this.colors2 ];
  swatchesSelected1: string[] = this.swatchesArray1[1];

  readonly huge5SwatchesArray = huge5Palettes;
  huge5SwatchesSelected = this.huge5SwatchesArray[0];

  readonly sequential5SwatchesArray = sequential5Palettes;
  sequential5SwatchesSelected = this.sequential5SwatchesArray[0];

  readonly randomPalettes: string[][] = randomSwatches();
}
