/** Contains information to reference an instrument. */
export class InstrumentReference {
    /** An exchange representations according to ISO 10383 Market Identifier Code (MIC). */
    mic?: string | undefined;

    /** An ISIN. */
    isin?: string | undefined;

    /** An optional symbol (ticker) of the security. */
    symbol?: string | undefined;

    /** A name of the instrument. */
    name?: string | undefined;

    constructor(data?: InstrumentReference) {
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
        data['Mic'] = this.mic;
        data['Isin'] = this.isin;
        data['Symbol'] = this.symbol;
        data['Name'] = this.name;
        return data;
    }
}
