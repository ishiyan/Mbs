import { MbSample } from './mb-sample';

export const treeNodes: MbSample[] = [
  {
    name: 'Ohlcv chart', header: 'Ohlcv chart', route: 'sample-6'
  }, {
    name: 'Synthetic data', header: 'Synthetic data', route: 'synthetic-data'
  }, {
    name: 'Data',
    children: [
      {name: 'Synthetic data', header: 'Synthetic data', route: 'synthetic-data'}
    ]
  }, {
    name: 'Instruments',
    children: [
      {name: 'Instruments table', header: 'Instruments table', route: 'sample-1'}
    ]
  }, {
    name: 'Components',
    children: [
      {
        name: 'Ohlcv chart',
        children: [
          {name: 'Candlestick chart', header: 'Candlestick chart', route: 'sample-6'}
        ]
      },
      {
        name: 'Sparkline',
        children: [
          {name: 'Features', header: 'Sparkline features', route: 'comp-sparkline/s1'},
          {name: 'Mat-select', header: 'Sparkline mat-select', route: 'comp-sparkline/s2'},
          {name: 'Mat-list', header: 'Sparkline mat-list', route: 'comp-sparkline/s3'}
        ]
      },
      {
        name: 'Multiline',
        children: [
          {name: 'Features', header: 'Multiline features', route: 'comp-multiline/s1'}
        ]
      },
      {
        name: 'Stackline',
        children: [
          {name: 'Features', header: 'Stackline features', route: 'comp-stackline/s1'}
        ]
      },
      {
        name: 'Sunbrst',
        children: [
          {name: 'Features', header: 'Sunburst features', route: 'comp-sunburst/s1'},
          {name: 'Countries', header: 'Sunburst countries', route: 'comp-sunburst/s2'},
          {name: 'JDK', header: 'Sunburst JDK', route: 'comp-sunburst/s3'}
        ]
      },
      {
        name: 'Circlepack',
        children: [
          {name: 'Features', header: 'Circlepack features', route: 'comp-circlepack/s1'},
          {name: 'Countries', header: 'Circlepack countries', route: 'comp-circlepack/s2'},
          {name: 'JDK', header: 'Circlepack JDK', route: 'comp-circlepack/s3'}
        ]
      },
      {
        name: 'Swatches',
        children: [
          {name: 'Features', header: 'Swatches features', route: 'comp-swatches/s1'},
          {name: 'Material palettes', header: 'Material palettes', route: 'comp-swatches/s2'},
          {name: 'Linear interpolated palettes', header: 'Linear interpolated palettes', route: 'comp-swatches/s3'},
          {name: 'Random procedural palettes', header: 'Random procedural', route: 'comp-swatches/s4'},
          {name: 'Parametric procedural palettes', header: 'Parametric procedural', route: 'comp-swatches/s5'},
          {name: 'Coolors.co palettes', header: 'Coolors.co palettes', route: 'comp-swatches/s6'},
          {name: 'Palettes from web', header: 'Palettes from web', route: 'comp-swatches/s7'}
        ]
      }
    ]
  }
];
