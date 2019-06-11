import { FractionalBrownianMotionAlgorithm } from './fractional-brownian-motion-algorithm.enum';
import { NormalRandomGeneratorKind } from '../normal-random-generator-kind.enum';
import { UniformRandomGeneratorKind } from '../uniform-random-generator-kind.enum';

/** The input parameters for the waveform generators. A waveform generator produces samples with an optional noise. */
export class FractionalBrownianMotionParameters {
    private static readonly defaultAmplitude: number = 100;
    private static readonly defaultMinimalValue: number = 10;
    private static readonly defaultHurstExponent: number = 0.63;
    private static readonly defaultAlgorithm: FractionalBrownianMotionAlgorithm = FractionalBrownianMotionAlgorithm.Hosking;
    private static readonly defaultNormalRandomGeneratorKind: NormalRandomGeneratorKind = NormalRandomGeneratorKind.ZigguratColinGreen;
    private static readonly defaultAssociatedUniformRandomGeneratorKind: UniformRandomGeneratorKind = UniformRandomGeneratorKind.Well44497A;
    private static readonly defaultSeed: number = 123456789;

    /** The amplitude of the fractional Brownian motion, should be positive. */
    amplitude: number = FractionalBrownianMotionParameters.defaultAmplitude;
    /** The minimum of the fractional Brownian motion, should be positive. */
    minimalValue: number = FractionalBrownianMotionParameters.defaultMinimalValue;
    /** The Hurst exponent of the fractal Brownian motion, H∈[0, 1]. */
    hurstExponent: number = FractionalBrownianMotionParameters.defaultHurstExponent;
    /** The fractional Brownian motion algorithm. */
    algorithm: FractionalBrownianMotionAlgorithm = FractionalBrownianMotionParameters.defaultAlgorithm;
    /** The kind of a Gaussian distribution random generator. */
    normalRandomGeneratorKind: NormalRandomGeneratorKind = FractionalBrownianMotionParameters.defaultNormalRandomGeneratorKind;
    /** The kind of a uniform random generator optionally associated with the Gaussian random generator.
     *
     * Used only by ZigguratColinGreen and BoxMuller Gaussian generators. */
    associatedUniformRandomGeneratorKind: UniformRandomGeneratorKind =
        FractionalBrownianMotionParameters.defaultAssociatedUniformRandomGeneratorKind;
    /** The seed of a random generator.
     *
     * If ZigguratColinGreen or BoxMuller Gaussian generator is used, the seed will be applied to the associated uniform generator.
     * Otherwise, it will be applied to the Gaussian generator. */
    seed: number = FractionalBrownianMotionParameters.defaultSeed;

    constructor(data?: FractionalBrownianMotionParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): FractionalBrownianMotionParameters {
        data = typeof data === 'object' ? data : {};
        const result = new FractionalBrownianMotionParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.amplitude = data['amplitude'] !== undefined
                ? data['amplitude'] : FractionalBrownianMotionParameters.defaultAmplitude;
            this.minimalValue = data['minimalValue'] !== undefined
                ? data['minimalValue'] : FractionalBrownianMotionParameters.defaultMinimalValue;
            this.hurstExponent = data['hurstExponent'] !== undefined
                ? data['hurstExponent'] : FractionalBrownianMotionParameters.defaultHurstExponent;
            this.algorithm = data['algorithm'] !== undefined
                ? data['algorithm'] : FractionalBrownianMotionParameters.defaultAlgorithm;
            this.normalRandomGeneratorKind = data['normalRandomGeneratorKind'] !== undefined
                ? data['normalRandomGeneratorKind'] : FractionalBrownianMotionParameters.defaultNormalRandomGeneratorKind;
            this.associatedUniformRandomGeneratorKind = data['associatedUniformRandomGeneratorKind'] !== undefined
                ? data['associatedUniformRandomGeneratorKind']
                : FractionalBrownianMotionParameters.defaultAssociatedUniformRandomGeneratorKind;
            this.seed = data['seed'] !== undefined
                ? data['seed'] : FractionalBrownianMotionParameters.defaultSeed;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['amplitude'] = this.amplitude;
        data['minimalValue'] = this.minimalValue;
        data['hurstExponent'] = this.hurstExponent;
        data['algorithm'] = this.algorithm;
        data['normalRandomGeneratorKind'] = this.normalRandomGeneratorKind;
        data['associatedUniformRandomGeneratorKind'] = this.associatedUniformRandomGeneratorKind;
        data['seed'] = this.seed;
        return data;
    }
}
