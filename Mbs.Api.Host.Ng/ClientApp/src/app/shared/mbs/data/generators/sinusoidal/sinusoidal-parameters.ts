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
        data = typeof data === 'object' ? data : {};
        const result = new SinusoidalParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.amplitude = data['amplitude'] !== undefined
                ? data['amplitude'] : SinusoidalParameters.defaultAmplitude;
            this.minimalValue = data['minimalValue'] !== undefined
                ? data['minimalValue'] : SinusoidalParameters.defaultMinimalValue;
            this.period = data['period'] !== undefined
                ? data['period'] : SinusoidalParameters.defaultPeriod;
            this.phaseInPi = data['phaseInPi'] !== undefined
                ? data['phaseInPi'] : SinusoidalParameters.defaultPhaseInPi;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['amplitude'] = this.amplitude;
        data['minimalValue'] = this.minimalValue;
        data['period'] = this.period;
        data['phaseInPi'] = this.phaseInPi;
        return data;
    }
}
