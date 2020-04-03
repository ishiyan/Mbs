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
          {name: 'Features', header: 'Multiline features', route: 'comp-multiline/s1'},
        ]
      }
    ]
  }
];
