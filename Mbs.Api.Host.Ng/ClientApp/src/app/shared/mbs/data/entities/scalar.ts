/** A _scalar_ (value and time) entity. */
export class Scalar {
  /** The date and time. */
  time: Date;

  /** The value. */
  value: number;

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
    data['value'] = this.value;
    return data;
  }*/
}

export namespace Scalar {
  export function fromJson(json?: any): Scalar {
    const scalar = new Scalar();
    if (json) {
      for (const property in json) {
        if (json.hasOwnProperty(property)) {
          (scalar as any)[property] = json[property];
        }
      }
    }
    return scalar;
  }

  export function toJson(scalar: Scalar, json?: any): any {
    json = typeof json === 'object' ? json : {};
    // tslint:disable:no-string-literal
    json['time'] = scalar.time ? scalar.time.toISOString() : (undefined as any);
    json['value'] = scalar.value;
    // tslint:enable:no-string-literal
    return json;
  }
}
