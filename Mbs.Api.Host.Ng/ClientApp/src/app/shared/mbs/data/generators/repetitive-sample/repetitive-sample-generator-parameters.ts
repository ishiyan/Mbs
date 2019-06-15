import { TimeParameters } from '../time-parameters';

/** The input parameters for the repetitive sample generator. */
export class RepetitiveSampleGeneratorParameters {
    private static readonly defaultSampleCount: number = 256;
    private static readonly defaultOffsetSamples: number = 0;
    private static readonly defaultRepetitionsCount: number = 0;

    /** The number of samples to generate. */
    sampleCount: number = RepetitiveSampleGeneratorParameters.defaultSampleCount;
    /** The number of samples before the very first waveform sample.
     *
     * The value of zero means the waveform starts immediately. */
    offsetSamples: number = RepetitiveSampleGeneratorParameters.defaultOffsetSamples;
    /** The number of repetitions of the waveform.
     *
     * The value of zero means infinite. */
    repetitionsCount: number = RepetitiveSampleGeneratorParameters.defaultRepetitionsCount;
    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    constructor(data?: RepetitiveSampleGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): RepetitiveSampleGeneratorParameters {
        data = typeof data === 'object' ? data : {};
        const result = new RepetitiveSampleGeneratorParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.sampleCount = data['sampleCount'] !== undefined
                ? data['sampleCount'] : RepetitiveSampleGeneratorParameters.defaultSampleCount;
            this.offsetSamples = data['offsetSamples'] !== undefined
                ? data['offsetSamples'] : RepetitiveSampleGeneratorParameters.defaultOffsetSamples;
            this.repetitionsCount = data['repetitionsCount'] !== undefined
                ? data['repetitionsCount'] : RepetitiveSampleGeneratorParameters.defaultRepetitionsCount;
            this.timeParameters = data['timeParameters']
                ? TimeParameters.fromJS(data['timeParameters']) : new TimeParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['sampleCount'] = this.sampleCount;
        data['offsetSamples'] = this.offsetSamples;
        data['repetitionsCount'] = this.repetitionsCount;
        data['timeParameters'] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        return data;
    }
}
