import { CurrencyCode } from '../../currencies/currency-code.enum';

/** An additional information for stocks. */
export class Stock {
    /** A currency code. */
    currency!: CurrencyCode;
    /** A trading mode. */
    tradingMode?: string | undefined;
    /** An ISO 10962 Classification of Financial Instruments code. */
    cfi?: string | undefined;
    /** An Industry Classification Benchmark code. */
    icb?: string | undefined;
    /** A number of shares outstanding. */
    sharesOutstanding?: number | undefined;

    constructor(data?: Stock) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['Currency'] = this.currency;
        data['TradingMode'] = this.tradingMode;
        data['Cfi'] = this.cfi;
        data['Icb'] = this.icb;
        data['SharesOutstanding'] = this.sharesOutstanding;
        return data;
    }
}
