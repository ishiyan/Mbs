import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../../../../shared/material/material.module';
import { KatexModule } from '../../../../shared/katex/katex.module';
import { MbsModule } from '../../../../shared/mbs/mbs.module';

import { SampleSwatches1Component } from './sample-1/sample-swatches-1.component';
import { SampleSwatches2Component } from './sample-2/sample-swatches-2.component';
import { SampleSwatches3Component } from './sample-3/sample-swatches-3.component';
import { SampleSwatches4Component } from './sample-4/sample-swatches-4.component';
import { SampleSwatches5Component } from './sample-5/sample-swatches-5.component';
import { SampleSwatches6Component } from './sample-6/sample-swatches-6.component';
import { SampleSwatches7Component } from './sample-7/sample-swatches-7.component';

import { SampleSwatchesRoutingModule } from './sample-swatches-routing.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, KatexModule, MbsModule, SampleSwatchesRoutingModule
  ],
  declarations: [
    SampleSwatches1Component, SampleSwatches2Component, SampleSwatches3Component, SampleSwatches4Component,
    SampleSwatches5Component, SampleSwatches6Component, SampleSwatches7Component
  ]
})
export class SampleSwatchesModule { }
