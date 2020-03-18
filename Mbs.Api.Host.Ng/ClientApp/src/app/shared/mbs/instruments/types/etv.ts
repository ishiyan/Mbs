import { CurrencyCode } from '../../currencies/currency-code.enum';

/** An additional information for Exchange-Traded Vehicles. */
export class Etv {
  /** A currency code. */
  currency!: CurrencyCode;

  /** A trading mode. */
  tradingMode?: string | undefined;

  /** All-in fees. */
  allInFees?: string | undefined;

  /** An expence ratio percentage. */
  expenseRatio?: string | undefined;

  /** A dividend frequency. */
  dividendFrequency?: string | undefined;

  /** An issuer. */
  issuer?: string | undefined;

  /** A number of shares outstanding. */
  sharesOutstanding?: number | undefined;

  constructor(data?: Etv) {
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
    data['AllInFees'] = this.allInFees;
    data['ExpenseRatio'] = this.expenseRatio;
    data['DividendFrequency'] = this.dividendFrequency;
    data['Issuer'] = this.issuer;
    data['SharesOutstanding'] = this.sharesOutstanding;
    return data;
  }
}
