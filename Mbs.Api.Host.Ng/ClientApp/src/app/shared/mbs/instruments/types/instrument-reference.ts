/** Contains information to reference an instrument. */
export class InstrumentReference {
  /** An exchange representations according to ISO 10383 *Market Identifier Code* (MIC). */
  mic?: string;

  /** An ISIN. */
  isin?: string;

  /** An optional symbol (ticker) of the security. */
  symbol?: string;

  /** A name of the instrument. */
  name?: string;

  constructor(data?: InstrumentReference) {
    if (data) {
      for (const property in data) {
        if (data.hasOwnProperty(property)) {
          (this as any)[property] = (data as any)[property];
        }
      }
    }
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    // tslint:disable:no-string-literal
    data['Mic'] = this.mic;
    data['Isin'] = this.isin;
    data['Symbol'] = this.symbol;
    data['Name'] = this.name;
    // tslint:enable:no-string-literal
    return data;
  }
}
