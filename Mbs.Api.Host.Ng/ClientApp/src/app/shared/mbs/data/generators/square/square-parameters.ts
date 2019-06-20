import { amplitudeName, minimalValueName, objectName } from '../constants';

/** The input parameters for the square impulse generator. */
export class SquareParameters {
    private static readonly defaultAmplitude: number = 100;
    private static readonly defaultMinimalValue: number = 10;

    /** The amplitude of the square impulse, should be positive. */
    amplitude: number = SquareParameters.defaultAmplitude;

    /** The minimum of square impulse, should be positive. */
    minimalValue: number = SquareParameters.defaultMinimalValue;

    constructor(data?: SquareParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): SquareParameters {
        data = typeof data === objectName ? data : {};
        const result = new SquareParameters();
        result.init(data);
        return result;
    }

    private init(data?: any): void {
        if (data) {
            this.amplitude = data[amplitudeName] !== undefined ? data[amplitudeName] :
                SquareParameters.defaultAmplitude;
            this.minimalValue = data[minimalValueName] !== undefined ? data[minimalValueName] :
                SquareParameters.defaultMinimalValue;
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[amplitudeName] = this.amplitude;
        data[minimalValueName] = this.minimalValue;
        return data;
    }
}
