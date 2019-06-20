import { amplitudeName, periodName, phaseInPiName, objectName } from '../constants';

/** The input parameters for an individual component of the multi-sinusoidal generator. */
export class MultiSinusoidalComponentParameters {
    private static readonly defaultAmplitude: number = 100;
    private static readonly defaultPeriod: number = 16;
    private static readonly defaultPhaseInPi: number = 0;

    /** The amplitude of the sinusoidal component, should be positive. */
    amplitude: number = MultiSinusoidalComponentParameters.defaultAmplitude;

    /** The period of the sinusoidal component in samples, should be ≥ 2. */
    period: number = MultiSinusoidalComponentParameters.defaultPeriod;

    /** The phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π]. */
    phaseInPi: number = MultiSinusoidalComponentParameters.defaultPhaseInPi;

    constructor(data?: MultiSinusoidalComponentParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): MultiSinusoidalComponentParameters {
        data = typeof data === objectName ? data : {};
        const result = new MultiSinusoidalComponentParameters();
        result.init(data);
        return result;
    }

    private init(data?: any): void {
        if (data) {
            this.amplitude = data[amplitudeName] !== undefined ? data[amplitudeName] :
                MultiSinusoidalComponentParameters.defaultAmplitude;
            this.period = data[periodName] !== undefined ? data[periodName] :
                MultiSinusoidalComponentParameters.defaultPeriod;
            this.phaseInPi = data[phaseInPiName] !== undefined ? data[phaseInPiName] :
                MultiSinusoidalComponentParameters.defaultPhaseInPi;
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[amplitudeName] = this.amplitude;
        data[periodName] = this.period;
        data[phaseInPiName] = this.phaseInPi;
        return data;
    }
}
