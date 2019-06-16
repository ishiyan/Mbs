import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { MathJaxModule } from '../math-jax/math-jax.module';
import { MaterialModule } from '../material/material.module';

import { BusinessDayCalendarExplanationComponent } from './components/data/generators/business-day-calendar-explanation.component';
import { TimeParametersComponent } from './components/data/generators/time-parameters.component';
import { WaveformParametersComponent } from './components/data/generators/waveform-parameters.component';
import { OhlcvParametersComponent } from './components/data/generators/ohlcv-parameters.component';
import { QuoteParametersComponent } from './components/data/generators/quote-parameters.component';
import { TradeParametersComponent } from './components/data/generators/trade-parameters.component';
// tslint:disable-next-line: max-line-length
import { FractionalBrownianMotionParametersComponent } from './components/data/generators/fractional-brownian-motion/fractional-brownian-motion-parameters.component';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        MaterialModule,
        FlexLayoutModule,
        FormsModule,
        MathJaxModule
    ],
    exports: [
        BusinessDayCalendarExplanationComponent, TimeParametersComponent, WaveformParametersComponent,
        OhlcvParametersComponent, QuoteParametersComponent, TradeParametersComponent,
        FractionalBrownianMotionParametersComponent
    ],
    declarations: [
        BusinessDayCalendarExplanationComponent, TimeParametersComponent, WaveformParametersComponent,
        OhlcvParametersComponent, QuoteParametersComponent, TradeParametersComponent,
        FractionalBrownianMotionParametersComponent
    ]
})
export class MbsModule { }
