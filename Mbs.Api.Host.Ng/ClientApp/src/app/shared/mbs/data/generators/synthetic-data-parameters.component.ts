import { Component, Input, OnInit } from '@angular/core';
import { SyntheticDataKind, SyntheticDataKindValues } from './synthetic-data-kind.enum';
import { ChirpGeneratorParameters } from './chirp/chirp-generator-parameters';
import { ChirpOhlcvGeneratorParameters } from './chirp/chirp-ohlcv-generator-parameters';
import { ChirpQuoteGeneratorParameters } from './chirp/chirp-quote-generator-parameters';
import { ChirpTradeGeneratorParameters } from './chirp/chirp-trade-generator-parameters';
import { ChirpScalarGeneratorParameters } from './chirp/chirp-scalar-generator-parameters';

@Component({
    selector: 'app-mbs-data-generators-synthetic-data-parameters',
    templateUrl: './synthetic-data-parameters.component.html',
    styleUrls: ['./synthetic-data-parameters.component.scss']
})
export class SyntheticDataParametersComponent implements OnInit {
    readonly dataKinds: string[] = SyntheticDataKindValues;
    readonly chirpGeneratorParameters: ChirpGeneratorParameters = new ChirpGeneratorParameters();
    syntheticDataKind: SyntheticDataKind = SyntheticDataKind.FractionalBrownianMotion;

    ngOnInit() {
    }

    public get ohlcvGen(): ChirpOhlcvGeneratorParameters {
        return ChirpOhlcvGeneratorParameters.fromJS(this.chirpGeneratorParameters);
    }

    public get quoteGen(): ChirpQuoteGeneratorParameters {
        return ChirpQuoteGeneratorParameters.fromJS(this.chirpGeneratorParameters);
    }

    public get tradeGen(): ChirpTradeGeneratorParameters {
        return ChirpTradeGeneratorParameters.fromJS(this.chirpGeneratorParameters);
    }

    public get scalarGen(): ChirpScalarGeneratorParameters {
        return ChirpScalarGeneratorParameters.fromJS(this.chirpGeneratorParameters);
    }

    public dataKindChanged(value: SyntheticDataKind): void {
        this.syntheticDataKind = value;
    }

    public get isFbm(): boolean {
        return this.syntheticDataKind === SyntheticDataKind.FractionalBrownianMotion;
    }

    public get isGbm(): boolean {
        return this.syntheticDataKind === SyntheticDataKind.GeometricBrownianMotion;
    }

    public get isChirp(): boolean {
        return this.syntheticDataKind === SyntheticDataKind.Chirp;
    }

    public get isSawtooth(): boolean {
        return this.syntheticDataKind === SyntheticDataKind.Sawtooth;
    }

    public get isSquare(): boolean {
        return this.syntheticDataKind === SyntheticDataKind.Square;
    }

    public get isSinusoid(): boolean {
        return this.syntheticDataKind === SyntheticDataKind.Sinusoid;
    }

    public get isMultiSinusoid(): boolean {
        return this.syntheticDataKind === SyntheticDataKind.MultiSinusoid;
    }
}

