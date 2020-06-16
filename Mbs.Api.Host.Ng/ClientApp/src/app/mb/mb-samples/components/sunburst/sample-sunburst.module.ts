import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../../../../shared/material/material.module';
import { MbsModule } from '../../../../shared/mbs/mbs.module';

import { SampleSunburst1Component } from './sample-1/sample-sunburst-1.component';
import { SampleSunburst2Component } from './sample-2/sample-sunburst-2.component';
import { SampleSunburst3Component } from './sample-3/sample-sunburst-3.component';

import { SampleSunburstRoutingModule } from './sample-sunburst-routing.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, MbsModule, SampleSunburstRoutingModule
  ],
  declarations: [
    SampleSunburst1Component, SampleSunburst2Component, SampleSunburst3Component
  ]
})
export class SampleSunburstModule { }
