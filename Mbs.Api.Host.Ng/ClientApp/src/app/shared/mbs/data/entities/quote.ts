/** A price _quote_ (bid/ask price and size pair). */
export class Quote {
  /** The date and time. */
  time: Date;

  /** The bid price. */
  bidPrice: number;

  /** The bid size. */
  bidSize: number;

  /** The ask price. */
  askPrice: number;

  /** The ask size. */
  askSize: number;

  /*constructor(data?: any) {
    if (data) {
      for (const property in data) {
        if (data.hasOwnProperty(property)) {
          (<any>this)[property] = (<any>data)[property];
        }
      }
    }
  }*/

  /*toJSON(data?: any): any {
    data = typeof data === 'object' ? data : {};
    data['time'] = this.time ? this.time.toISOString() : <any>undefined;
    data['bidPrice'] = this.bidPrice;
    data['bidSize'] = this.bidSize;
    data['askPrice'] = this.askPrice;
    data['askSize'] = this.askSize;
    return data;
  }*/
}

export namespace Quote {
  export function fromJson(json?: any): Quote {
    const quote = new Quote();
    if (json) {
      for (const property in json) {
        if (json.hasOwnProperty(property)) {
          (<any>quote)[property] = (<any>json)[property];
        }
      }
    }
    return quote;
  }

  export function toJson(quote: Quote, json?: any): any {
    json = typeof json === 'object' ? json : {};
    json['time'] = quote.time ? quote.time.toISOString() : <any>undefined;
    json['bidPrice'] = quote.bidPrice;
    json['bidSize'] = quote.bidSize;
    json['askPrice'] = quote.askPrice;
    json['askSize'] = quote.askSize;
    return json;
  }
}
