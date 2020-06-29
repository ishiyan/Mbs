import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../../../../shared/material/material.module';
import { MbsModule } from '../../../../shared/mbs/mbs.module';

import { SampleCirclepack1Component } from './sample-1/sample-circlepack-1.component';
import { SampleCirclepack2Component } from './sample-2/sample-circlepack-2.component';
import { SampleCirclepack3Component } from './sample-3/sample-circlepack-3.component';

import { SampleCirclepackRoutingModule } from './sample-circlepack-routing.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, MbsModule, SampleCirclepackRoutingModule
  ],
  declarations: [
    SampleCirclepack1Component, SampleCirclepack2Component, SampleCirclepack3Component
  ]
})
export class SampleCirclepackModule { }
