import { Component, Input, OnInit } from '@angular/core';
import { SyntheticDataKind } from './synthetic-data-kind.enum';
import { ChirpGeneratorParameters } from './chirp/chirp-generator-parameters';
import { ChirpOhlcvGeneratorParameters } from './chirp/chirp-ohlcv-generator-parameters';
import { ChirpQuoteGeneratorParameters } from './chirp/chirp-quote-generator-parameters';
import { ChirpTradeGeneratorParameters } from './chirp/chirp-trade-generator-parameters';
import { ChirpScalarGeneratorParameters } from './chirp/chirp-scalar-generator-parameters';
import { TemporalEntityKind } from '../entities/temporal-entity-kind.enum';
import { FractionalBrownianMotionGeneratorParameters } from './fractional-brownian-motion/fractional-brownian-motion-generator-parameters';
import { GeometricBrownianMotionGeneratorParameters } from './geometric-brownian-motion/geometric-brownian-motion-generator-parameters';
import { SawtoothGeneratorParameters } from './sawtooth/sawtooth-generator-parameters';
import { SquareGeneratorParameters } from './square/square-generator-parameters';
import { SinusoidalGeneratorParameters } from './sinusoidal/sinusoidal-generator-parameters';

@Component({
    selector: 'app-mbs-data-generators-synthetic-data-parameters',
    templateUrl: './synthetic-data-parameters.component.html',
    styleUrls: ['./synthetic-data-parameters.component.scss']
})
export class SyntheticDataParametersComponent implements OnInit {
    @Input() temporalEntityKind: TemporalEntityKind;
    readonly dataKinds: string[] = Object.values(SyntheticDataKind);

    readonly chirpGeneratorParameters: ChirpGeneratorParameters = new ChirpGeneratorParameters();
    readonly fbmGeneratorParameters: FractionalBrownianMotionGeneratorParameters = new FractionalBrownianMotionGeneratorParameters();
    readonly gbmGeneratorParameters: GeometricBrownianMotionGeneratorParameters = new GeometricBrownianMotionGeneratorParameters();
    readonly sawtoothGeneratorParameters: SawtoothGeneratorParameters = new SawtoothGeneratorParameters();
    readonly squareGeneratorParameters: SquareGeneratorParameters = new SquareGeneratorParameters();
    readonly sinusoidGeneratorParameters: SinusoidalGeneratorParameters = new SinusoidalGeneratorParameters();

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

    public get isOhlcv(): boolean {
        return this.temporalEntityKind === TemporalEntityKind.Ohlcv;
    }

    public get isQuote(): boolean {
        return this.temporalEntityKind === TemporalEntityKind.Quote;
    }

    public get isTrade(): boolean {
        return this.temporalEntityKind === TemporalEntityKind.Trade;
    }

    public get isScalar(): boolean {
        return this.temporalEntityKind === TemporalEntityKind.Scalar;
    }
}

