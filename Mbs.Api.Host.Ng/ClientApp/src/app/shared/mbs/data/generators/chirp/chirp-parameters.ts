import { ChirpSweep } from './chirp-sweep.enum';

/** The input parameters for the chirp generator. */
export class ChirpParameters {
    private static readonly defaultAmplitude: number = 100;
    private static readonly defaultMinimalValue: number = 10;
    private static readonly defaultInitialPeriod: number = 128;
    private static readonly defaultFinalPeriod: number = 16;
    private static readonly defaultPhaseInPi: number = 0;
    private static readonly defaultIsBiDirectional: boolean = false;
    private static readonly defaultChirpSweep: ChirpSweep = ChirpSweep.LinearPeriod;

    /** The amplitude of the chirp, should be positive. */
    amplitude: number = ChirpParameters.defaultAmplitude;
    /** The minimum of the chirp, should be positive. */
    minimalValue: number = ChirpParameters.defaultMinimalValue;
    /** The instantaneous initial period of the chirp in samples, should be ≥ 2. */
    initialPeriod: number = ChirpParameters.defaultInitialPeriod;
    /** The instantaneous final period of the chirp in samples, should be ≥ 2. */
    finalPeriod: number = ChirpParameters.defaultFinalPeriod;
    /** The initial phase, φ, of the chirp in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π]. */
    phaseInPi: number = ChirpParameters.defaultPhaseInPi;
    /** If the period of even chirps descends from the final period to the initial one, to form a symmetrical shape with odd chirps. */
    isBiDirectional: boolean = ChirpParameters.defaultIsBiDirectional;
    /** The chirp sweep. */
    chirpSweep: ChirpSweep = ChirpParameters.defaultChirpSweep;

    constructor(data?: ChirpParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): ChirpParameters {
        data = typeof data === 'object' ? data : {};
        const result = new ChirpParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.amplitude = data['amplitude'] !== undefined
                ? data['amplitude'] : ChirpParameters.defaultAmplitude;
            this.minimalValue = data['minimalValue'] !== undefined
                ? data['minimalValue'] : ChirpParameters.defaultMinimalValue;
            this.initialPeriod = data['initialPeriod'] !== undefined
                ? data['initialPeriod'] : ChirpParameters.defaultInitialPeriod;
            this.finalPeriod = data['finalPeriod'] !== undefined
                ? data['finalPeriod'] : ChirpParameters.defaultFinalPeriod;
            this.phaseInPi = data['phaseInPi'] !== undefined
                ? data['phaseInPi'] : ChirpParameters.defaultPhaseInPi;
            this.isBiDirectional = data['isBiDirectional'] !== undefined
                ? data['isBiDirectional']
                : ChirpParameters.defaultIsBiDirectional;
            this.chirpSweep = data['chirpSweep'] !== undefined
                ? data['chirpSweep'] : ChirpParameters.defaultChirpSweep;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['amplitude'] = this.amplitude;
        data['minimalValue'] = this.minimalValue;
        data['initialPeriod'] = this.initialPeriod;
        data['finalPeriod'] = this.finalPeriod;
        data['phaseInPi'] = this.phaseInPi;
        data['isBiDirectional'] = this.isBiDirectional;
        data['chirpSweep'] = this.chirpSweep;
        return data;
    }
}
