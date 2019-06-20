import { amplitudeName, minimalValueName, periodName, phaseInPiName, objectName } from '../constants';

/** The input parameters for the sinusoidal generator. */
export class SinusoidalParameters {
    private static readonly defaultAmplitude: number = 100;
    private static readonly defaultMinimalValue: number = 10;
    private static readonly defaultPeriod: number = 16;
    private static readonly defaultPhaseInPi: number = 0;

    /** The amplitude of the sinusoid, should be positive. */
    amplitude: number = SinusoidalParameters.defaultAmplitude;

    /** The minimum of the sinusoid, should be positive. */
    minimalValue: number = SinusoidalParameters.defaultMinimalValue;

    /** The period of the sinusoid in samples, should be ≥ 2. */
    period: number = SinusoidalParameters.defaultPeriod;

    /** The phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π]. */
    phaseInPi: number = SinusoidalParameters.defaultPhaseInPi;

    constructor(data?: SinusoidalParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): SinusoidalParameters {
        data = typeof data === objectName ? data : {};
        const result = new SinusoidalParameters();
        result.init(data);
        return result;
    }

    private init(data?: any): void {
        if (data) {
            this.amplitude = data[amplitudeName] !== undefined ? data[amplitudeName] :
                SinusoidalParameters.defaultAmplitude;
            this.minimalValue = data[minimalValueName] !== undefined ? data[minimalValueName] :
                SinusoidalParameters.defaultMinimalValue;
            this.period = data[periodName] !== undefined ? data[periodName] :
                SinusoidalParameters.defaultPeriod;
            this.phaseInPi = data[phaseInPiName] !== undefined ? data[phaseInPiName] :
                SinusoidalParameters.defaultPhaseInPi;
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[amplitudeName] = this.amplitude;
        data[minimalValueName] = this.minimalValue;
        data[periodName] = this.period;
        data[phaseInPiName] = this.phaseInPi;
        return data;
    }
}
