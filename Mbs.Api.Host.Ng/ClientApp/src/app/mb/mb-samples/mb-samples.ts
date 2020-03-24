import { MbSample } from './mb-sample';

export const treeNodes: MbSample[] = [
  {
    name: 'Ohlcv chart', route: 'sample-6'
  }, {
    name: 'Synthetic data', route: 'synthetic-data'
  }, {
    name: 'Data',
    children: [
      {name: 'Synthetic data', route: 'synthetic-data'}
    ]
  }, {
    name: 'Instruments',
    children: [
      {name: 'Instruments table', route: 'sample-1'}
    ]
  }, {
    name: 'Ohlcv chart',
    children: [
      {name: 'Candlestick chart', route: 'sample-6'}
    ]
  }
];
