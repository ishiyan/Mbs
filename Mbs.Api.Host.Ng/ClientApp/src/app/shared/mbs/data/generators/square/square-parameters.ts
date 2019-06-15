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
        data = typeof data === 'object' ? data : {};
        const result = new SquareParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.amplitude = data['amplitude'] !== undefined
                ? data['amplitude'] : SquareParameters.defaultAmplitude;
            this.minimalValue = data['minimalValue'] !== undefined
                ? data['minimalValue'] : SquareParameters.defaultMinimalValue;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['amplitude'] = this.amplitude;
        data['minimalValue'] = this.minimalValue;
        return data;
    }
}
