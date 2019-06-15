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
        data = typeof data === 'object' ? data : {};
        const result = new MultiSinusoidalComponentParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.amplitude = data['amplitude'] !== undefined
                ? data['amplitude'] : MultiSinusoidalComponentParameters.defaultAmplitude;
            this.period = data['period'] !== undefined
                ? data['period'] : MultiSinusoidalComponentParameters.defaultPeriod;
            this.phaseInPi = data['phaseInPi'] !== undefined
                ? data['phaseInPi'] : MultiSinusoidalComponentParameters.defaultPhaseInPi;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['amplitude'] = this.amplitude;
        data['period'] = this.period;
        data['phaseInPi'] = this.phaseInPi;
        return data;
    }
}
