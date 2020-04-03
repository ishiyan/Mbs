import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../../../../shared/material/material.module';
import { MbsModule } from '../../../../shared/mbs/mbs.module';

import { SampleSparkline1Component } from './sample-1/sample-sparkline-1.component';
import { SampleSparkline2Component } from './sample-2/sample-sparkline-2.component';
import { SampleSparkline3Component } from './sample-3/sample-sparkline-3.component';

import { SampleSparklineRoutingModule } from './sample-sparkline-routing.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, MbsModule, SampleSparklineRoutingModule
  ],
  declarations: [
    SampleSparkline1Component, SampleSparkline2Component, SampleSparkline3Component
  ]
})
export class SampleSparklineModule { }
