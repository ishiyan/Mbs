import { MultiSinusoidalComponentParameters } from './multi-sinusoidal-component-parameters';

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
        data = typeof data === 'object' ? data : {};
        const result = new MultiSinusoidalParameters();
        result.init(data);
        return result;
    }

    init(data?: any) {
        if (data) {
            this.minimalValue = data['minimalValue'] !== undefined
                ? data['minimalValue'] : MultiSinusoidalParameters.defaultMinimalValue;
            if (data['multiSinusoidalComponents'] && data['multiSinusoidalComponents'].constructor === Array) {
                this.multiSinusoidalComponents = [] as any;
                for (const item of data['multiSinusoidalComponents']) {
                    this.multiSinusoidalComponents.push(MultiSinusoidalComponentParameters.fromJS(item));
                }
            }
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['minimalValue'] = this.minimalValue;
        if (this.multiSinusoidalComponents && this.multiSinusoidalComponents.constructor === Array) {
            data['multiSinusoidalComponents'] = [];
            for (const item of this.multiSinusoidalComponents) {
                data['multiSinusoidalComponents'].push(item.toJSON());
            }
        }
        return data;
    }
}
