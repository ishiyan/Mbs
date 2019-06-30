import { ConvertableToJSON } from '../convertable-to-json';

/** A _scalar_ (value and time) entity. */
export class Scalar implements ConvertableToJSON {
    /** The value. */
    value: number;

    /** The date and time. */
    time: Date;

    constructor(data?: Scalar) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    toJSON(data?: any): any {
        data = typeof data === 'object' ? data : {};
        data['value'] = this.value;
        data['time'] = this.time ? this.time.toISOString() : <any>undefined;
        return data;
    }
}
