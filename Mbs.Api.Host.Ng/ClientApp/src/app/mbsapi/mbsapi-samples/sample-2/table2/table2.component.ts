import { Component, OnInit, ElementRef, ViewChild, Input } from '@angular/core';
import { TimeParameters } from '../../../../shared/mbs/data/generators/time-parameters';
import { WaveformParameters } from '../../../../shared/mbs/data/generators/waveform-parameters';
import { OhlcvParameters } from '../../../../shared/mbs/data/generators/ohlcv-parameters';
import { QuoteParameters } from '../../../../shared/mbs/data/generators/quote-parameters';
import { TradeParameters } from '../../../../shared/mbs/data/generators/trade-parameters';
// tslint:disable-next-line: max-line-length
import { FractionalBrownianMotionParameters } from '../../../../shared/mbs/data/generators/fractional-brownian-motion/fractional-brownian-motion-parameters';
import { ChirpParameters } from '../../../../shared/mbs/data/generators/chirp/chirp-parameters';
import { SawtoothParameters } from '../../../../shared/mbs/data/generators/sawtooth/sawtooth-parameters';
import { SquareParameters } from '../../../../shared/mbs/data/generators/square/square-parameters';
import { SinusoidalParameters } from '../../../../shared/mbs/data/generators/sinusoidal/sinusoidal-parameters';

@Component({
    selector: 'app-table2',
    templateUrl: './table2.component.html',
    styleUrls: ['./table2.component.scss']
})
export class Table2Component implements OnInit {
    timeParameters: TimeParameters = new TimeParameters();
    waveformParameters: WaveformParameters = new WaveformParameters();
    ohlcvParameters: OhlcvParameters = new OhlcvParameters();
    quoteParameters: QuoteParameters = new QuoteParameters();
    tradeParameters: TradeParameters = new TradeParameters();
    fractionalBrownianMotionParameters: FractionalBrownianMotionParameters = new FractionalBrownianMotionParameters();
    chirpParameters: ChirpParameters = new ChirpParameters();
    sawtoothParameters: SawtoothParameters = new SawtoothParameters();
    squareParameters: SquareParameters = new SquareParameters();
    sinusoidalParameters: SinusoidalParameters = new SinusoidalParameters();

    constructor(private element: ElementRef) {
    }
    @ViewChild('container', { static: true }) container: ElementRef;

    ngOnInit() {
    }
}
