import { Component, OnInit, ElementRef, ViewChild, Input } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { TimeParameters } from 'src/app/shared/mbs/data/generators/time-parameters';
import { WaveformParameters } from 'src/app/shared/mbs/data/generators/waveform-parameters';
import { OhlcvParameters } from 'src/app/shared/mbs/data/generators/ohlcv-parameters';
import { QuoteParameters } from 'src/app/shared/mbs/data/generators/quote-parameters';
import { TradeParameters } from 'src/app/shared/mbs/data/generators/trade-parameters';
// tslint:disable-next-line: max-line-length
import { FractionalBrownianMotionParameters } from 'src/app/shared/mbs/data/generators/fractional-brownian-motion/fractional-brownian-motion-parameters';

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

    constructor(private element: ElementRef) {
    }
    @ViewChild('container', { static: true }) container: ElementRef;

    ngOnInit() {
    }
}
