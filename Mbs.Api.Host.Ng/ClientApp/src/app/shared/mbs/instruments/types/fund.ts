import { CurrencyCode } from '../../currencies/currency-code.enum';

/** An additional information for funds. */
export class Fund {
  /** A currency code. */
  currency!: CurrencyCode;

  /** A trading mode. */
  tradingMode?: string | undefined;

  /** An ISO 10962 Classification of Financial Instruments code. */
  cfi?: string | undefined;

  /** An issuer. */
  issuer?: string | undefined;

  /** A number of shares outstanding. */
  sharesOutstanding?: number | undefined;

  constructor(data?: Fund) {
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
    data['Issuer'] = this.issuer;
    data['SharesOutstanding'] = this.sharesOutstanding;
    return data;
  }
}
