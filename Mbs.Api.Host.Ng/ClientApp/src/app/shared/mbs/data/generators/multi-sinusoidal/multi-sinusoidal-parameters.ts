import { MultiSinusoidalComponentParameters } from './multi-sinusoidal-component-parameters';
import { minimalValueName, multiSinusoidalComponentsName, objectName } from '../constants';

/** The input parameters for the multi-sinusoidal generator. */
export class MultiSinusoidalParameters {
    private static readonly defaultMinimalValue: number = 10;

    /** The minimum of the combined sinusoids, should be positive. */
    minimalValue: number = MultiSinusoidalParameters.defaultMinimalValue;

    /** An array of the parameters for individual sinusoids. */
    multiSinusoidalComponents: MultiSinusoidalComponentParameters[] = [new MultiSinusoidalComponentParameters()];

    constructor(data?: MultiSinusoidalParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): MultiSinusoidalParameters {
        data = typeof data === objectName ? data : {};
        const result = new MultiSinusoidalParameters();
        result.init(data);
        return result;
    }

    private init(data?: any): void {
        if (data) {
            this.minimalValue = data[minimalValueName] !== undefined ? data[minimalValueName] :
                MultiSinusoidalParameters.defaultMinimalValue;
            if (data[multiSinusoidalComponentsName] && data[multiSinusoidalComponentsName].constructor === Array) {
                this.multiSinusoidalComponents = [] as any;
                for (const item of data[multiSinusoidalComponentsName]) {
                    this.multiSinusoidalComponents.push(MultiSinusoidalComponentParameters.fromJS(item));
                }
            }
        }
    }

    toJSON(data?: any) {
        data = typeof data === objectName ? data : {};
        data[minimalValueName] = this.minimalValue;
        if (this.multiSinusoidalComponents && this.multiSinusoidalComponents.constructor === Array) {
            data[multiSinusoidalComponentsName] = [];
            for (const item of this.multiSinusoidalComponents) {
                data[multiSinusoidalComponentsName].push(item.toJSON());
            }
        }
        return data;
    }
}
