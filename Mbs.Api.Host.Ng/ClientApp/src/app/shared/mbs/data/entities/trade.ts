/** A _trade_ (price and volume) entity. */
export class Trade {
  /** The date and time. */
  time: Date;

  /** The price. */
  price: number;

  /** The volume (quantity). */
  volume: number;

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
    data['price'] = this.price;
    data['volume'] = this.volume;
    return data;
  }*/
}

export namespace Trade {
  export function fromJson(json?: any): Trade {
    const trade = new Trade();
    if (json) {
      for (const property in json) {
        if (json.hasOwnProperty(property)) {
          (<any>trade)[property] = (<any>json)[property];
        }
      }
    }
    return trade;
  }

  export function toJson(trade: Trade, json?: any): any {
    json = typeof json === 'object' ? json : {};
    json['time'] = trade.time ? trade.time.toISOString() : <any>undefined;
    json['price'] = trade.price;
    json['volume'] = trade.volume;
    return json;
  }
}
