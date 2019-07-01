import { Ohlcv } from './entities/ohlcv';
import { Trade } from './entities/trade';
import { Quote } from '@angular/compiler';
import { Scalar } from './entities/scalar';

export interface HistoricalData {
    isDataAdjusted?: boolean | undefined;
    name?: string | undefined;
    moniker?: string | undefined;
    data: Ohlcv[] | Quote[] | Trade[] | Scalar[];
}
