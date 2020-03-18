import { CurrencyCode } from '../../currencies/currency-code.enum';
import { InstrumentReference } from './instrument-reference';

/** An additional information for Indicative Net Asset Values. */
export class Inav {
  /** A currency code. */
  currency!: CurrencyCode;

  /** A target instrument reference. */
  target?: InstrumentReference | undefined;

  constructor(data?: Inav) {
    if (data) {
      for (const property in data) {
        if (data.hasOwnProperty(property)) {
          (<any>this)[property] = (<any>data)[property];
        }
      }
      this.target = data.target && !(<any>data.target).toJSON ? new InstrumentReference(data.target) :
        <InstrumentReference>this.target;
    }
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['Currency'] = this.currency;
    data['Target'] = this.target ? this.target.toJSON() : <any>undefined;
    return data;
  }
}
