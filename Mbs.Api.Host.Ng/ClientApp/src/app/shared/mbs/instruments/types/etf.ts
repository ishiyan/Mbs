import { CurrencyCode } from '../../currencies/currency-code.enum';
import { InstrumentReference } from './instrument-reference';

/** An additional information for Exchange-Traded Funds. */
export class Etf {
    /** A currency code. */
    currency!: CurrencyCode;

    /** A trading mode. */
    tradingMode?: string | undefined;

    /** An ISO 10962 Classification of Financial Instruments code. */
    cfi?: string | undefined;

    /** A dividend frequency. */
    dividendFrequency?: string | undefined;

    /** An exposition type. */
    expositionType?: string | undefined;

    /** A fraction. */
    fraction?: string | undefined;

    /** A total expence ratio percentage. */
    totalExpenseRatio?: string | undefined;

    /** An index family. */
    indexFamily?: string | undefined;

    /** A launch date. */
    launchDate?: string | undefined;

    /** An issuer. */
    issuer?: string | undefined;

    /** An Indicative Net Asset Value instrument reference. */
    inav?: InstrumentReference | undefined;

    /** Gets or sets an underlying instrument reference. */
    underlying?: InstrumentReference | undefined;

    constructor(data?: Etf) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
            this.inav = data.inav && !(<any>data.inav).toJSON ? new InstrumentReference(data.inav) : <InstrumentReference>this.inav;
            this.underlying = data.underlying && !(<any>data.underlying).toJSON ? new InstrumentReference(data.underlying) :
                <InstrumentReference>this.underlying;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['Currency'] = this.currency;
        data['TradingMode'] = this.tradingMode;
        data['Cfi'] = this.cfi;
        data['DividendFrequency'] = this.dividendFrequency;
        data['ExpositionType'] = this.expositionType;
        data['Fraction'] = this.fraction;
        data['TotalExpenseRatio'] = this.totalExpenseRatio;
        data['IndexFamily'] = this.indexFamily;
        data['LaunchDate'] = this.launchDate;
        data['Issuer'] = this.issuer;
        data['Inav'] = this.inav ? this.inav.toJSON() : <any>undefined;
        data['Underlying'] = this.underlying ? this.underlying.toJSON() : <any>undefined;
        return data;
    }
}
