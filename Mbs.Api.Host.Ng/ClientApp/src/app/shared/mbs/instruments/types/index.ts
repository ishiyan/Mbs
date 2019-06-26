import { CurrencyCode } from '../../currencies/currency-code.enum';

/** An additional information for indices. */
export class Index {
    /** A currency code. */
    currency!: CurrencyCode;

    /** A kind of an index. */
    kind?: string | undefined;

    /** A family of an index. */
    family?: string | undefined;

    /** A calculation frequency of an index. */
    calculationFrequency?: string | undefined;

    /** A weighting of an index. */
    weighting?: string | undefined;

    /** A capping factor of an index. */
    cappingFactor?: string | undefined;

    /** An Industry Classification Benchmark code. */
    icb?: string | undefined;

    /** A base date of an index. */
    baseDate?: string | undefined;

    /** A base level of an index. */
    baseLevel?: string | undefined;

    constructor(data?: Index) {
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
        data['Kind'] = this.kind;
        data['Family'] = this.family;
        data['CalculationFrequency'] = this.calculationFrequency;
        data['Weighting'] = this.weighting;
        data['CappingFactor'] = this.cappingFactor;
        data['Icb'] = this.icb;
        data['BaseDate'] = this.baseDate;
        data['BaseLevel'] = this.baseLevel;
        return data;
    }
}
