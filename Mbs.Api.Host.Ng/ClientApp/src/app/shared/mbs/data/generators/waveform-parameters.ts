import { UniformRandomGeneratorKind } from './uniform-random-generator-kind.enum';

/** The input parameters for the waveform generators. A waveform generator produces samples with an optional noise. */
export class WaveformParameters {
    private static readonly defaultWaveformSamples: number = 128;
    private static readonly defaultOffsetSamples: number = 0;
    private static readonly defaultRepetitionsCount: number = 0;
    private static readonly defaultNoiseAmplitudeFraction: number = 0.01;
    private static readonly defaultNoiseUniformRandomGeneratorKind: UniformRandomGeneratorKind = UniformRandomGeneratorKind.Well44497A;
    private static readonly defaultNoiseUniformRandomGeneratorSeed: number = 12345678;

    /** The number of samples in the waveform. */
    waveformSamples: number = WaveformParameters.defaultWaveformSamples;
    /** The number of samples before the very first waveform sample. The value of zero means the waveform starts immediately. */
    offsetSamples: number = WaveformParameters.defaultOffsetSamples;
    /** The number of repetitions of the waveform. The value of zero means infinite. */
    repetitionsCount: number = WaveformParameters.defaultRepetitionsCount;
    /** The amplitude of the noise as a fraction of the sample value. If zero, noise will be not produced. */
    noiseAmplitudeFraction: number = WaveformParameters.defaultNoiseAmplitudeFraction;
    /** The kind of an uniform random generator to produce the noise. */
    noiseUniformRandomGeneratorKind: UniformRandomGeneratorKind = WaveformParameters.defaultNoiseUniformRandomGeneratorKind;
    /** The seed of a random generator to produce the noise. */
    noiseUniformRandomGeneratorSeed: number = WaveformParameters.defaultNoiseUniformRandomGeneratorSeed;

    constructor(data?: WaveformParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): WaveformParameters {
        data = typeof data === 'object' ? data : {};
        const result = new WaveformParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.waveformSamples = data['waveformSamples'] !== undefined
                ? data['waveformSamples'] : WaveformParameters.defaultOffsetSamples;
            this.offsetSamples = data['offsetSamples'] !== undefined
                ? data['offsetSamples'] : WaveformParameters.defaultOffsetSamples;
            this.repetitionsCount = data['repetitionsCount'] !== undefined
                ? data['repetitionsCount'] : WaveformParameters.defaultRepetitionsCount;
            this.noiseAmplitudeFraction = data['noiseAmplitudeFraction'] !== undefined
                ? data['noiseAmplitudeFraction'] : WaveformParameters.defaultNoiseAmplitudeFraction;
            this.noiseUniformRandomGeneratorKind = data['noiseUniformRandomGeneratorKind'] !== undefined
                ? data['noiseUniformRandomGeneratorKind'] : WaveformParameters.defaultNoiseUniformRandomGeneratorKind;
            this.noiseUniformRandomGeneratorSeed = data['noiseUniformRandomGeneratorSeed'] !== undefined
                ? data['noiseUniformRandomGeneratorSeed'] : WaveformParameters.defaultNoiseUniformRandomGeneratorSeed;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['waveformSamples'] = this.waveformSamples;
        data['offsetSamples'] = this.offsetSamples;
        data['repetitionsCount'] = this.repetitionsCount;
        data['noiseAmplitudeFraction'] = this.noiseAmplitudeFraction;
        data['noiseUniformRandomGeneratorKind'] = this.noiseUniformRandomGeneratorKind;
        data['noiseUniformRandomGeneratorSeed'] = this.noiseUniformRandomGeneratorSeed;
        return data;
    }
}
