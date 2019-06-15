import { SawtoothShape } from './sawtooth-shape.enum';

/** The input parameters for the sawtooth impulse generator. */
export class SawtoothParameters {
    private static readonly defaultAmplitude: number = 100;
    private static readonly defaultMinimalValue: number = 10;
    private static readonly defaultIsBiDirectional: boolean = false;
    private static readonly defaultSawtoothShape: SawtoothShape = SawtoothShape.Linear;

    /** The amplitude of the sawtooth impulse, should be positive. */
    amplitude: number = SawtoothParameters.defaultAmplitude;
    /** The minimum of the sawtooth impulse, should be positive. */
    minimalValue: number = SawtoothParameters.defaultMinimalValue;
    /** whether the sawtooth impulse is reflected horizontally to form a triangle shape. */
    isBiDirectional: boolean = SawtoothParameters.defaultIsBiDirectional;
    /** The sawtooth shape. */
    sawtoothShape: SawtoothShape = SawtoothParameters.defaultSawtoothShape;

    constructor(data?: SawtoothParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): SawtoothParameters {
        data = typeof data === 'object' ? data : {};
        const result = new SawtoothParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.amplitude = data['amplitude'] !== undefined
                ? data['amplitude'] : SawtoothParameters.defaultAmplitude;
            this.minimalValue = data['minimalValue'] !== undefined
                ? data['minimalValue'] : SawtoothParameters.defaultMinimalValue;
            this.isBiDirectional = data['isBiDirectional'] !== undefined
                ? data['isBiDirectional']
                : SawtoothParameters.defaultIsBiDirectional;
            this.sawtoothShape = data['sawtoothShape'] !== undefined
                ? data['sawtoothShape'] : SawtoothParameters.defaultSawtoothShape;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['amplitude'] = this.amplitude;
        data['minimalValue'] = this.minimalValue;
        data['isBiDirectional'] = this.isBiDirectional;
        data['sawtoothShape'] = this.sawtoothShape;
        return data;
    }
}
