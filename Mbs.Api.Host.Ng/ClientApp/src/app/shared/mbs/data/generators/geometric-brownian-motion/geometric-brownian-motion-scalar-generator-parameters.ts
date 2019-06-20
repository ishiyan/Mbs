import { GeometricBrownianMotionParameters } from './geometric-brownian-motion-parameters';
import { TimeParameters } from '../time-parameters';
import { WaveformParameters } from '../waveform-parameters';
import { GeometricBrownianMotionGeneratorParameters } from './geometric-brownian-motion-generator-parameters';
import { sampleCountName, timeParametersName, waveformParametersName, gbmParametersName, objectName } from '../constants';

/** The input parameters for the geometric Brownian motion scalar generator. */
export class GeometricBrownianMotionScalarGeneratorParameters {
    /** The number of samples to generate. */
    sampleCount: number = GeometricBrownianMotionGeneratorParameters.defaultSampleCount;

    /** The time related input parameters. */
    timeParameters: TimeParameters = new TimeParameters();

    /** The waveform related input parameters. */
    waveformParameters: WaveformParameters = new WaveformParameters();

    /** The geometric Brownian motion related input parameters. */
    gbmParameters: GeometricBrownianMotionParameters = new GeometricBrownianMotionParameters();

    constructor(data?: GeometricBrownianMotionScalarGeneratorParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): GeometricBrownianMotionScalarGeneratorParameters {
        data = typeof data === objectName ? data : {};
        const result = new GeometricBrownianMotionScalarGeneratorParameters();
        result.init(data);
        return result;
    }

    private init(data?: any): void {
        if (data) {
            this.sampleCount = data[sampleCountName];
            this.timeParameters = data[timeParametersName] ? TimeParameters.fromJS(data[timeParametersName]) :
                new TimeParameters();
            this.waveformParameters = data[waveformParametersName] ? WaveformParameters.fromJS(data[waveformParametersName]) :
                new WaveformParameters();
            this.gbmParameters = data[gbmParametersName] ? GeometricBrownianMotionParameters.fromJS(data[gbmParametersName]) :
                new GeometricBrownianMotionParameters();
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[sampleCountName] = this.sampleCount;
        data[timeParametersName] = this.timeParameters ? this.timeParameters.toJSON() : <any>undefined;
        data[waveformParametersName] = this.waveformParameters ? this.waveformParameters.toJSON() : <any>undefined;
        data[gbmParametersName] = this.gbmParameters ? this.gbmParameters.toJSON() : <any>undefined;
        return data;
    }
}
