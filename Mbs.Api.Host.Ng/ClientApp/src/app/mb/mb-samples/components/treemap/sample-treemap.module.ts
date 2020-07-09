import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../../../../shared/material/material.module';
import { MbsModule } from '../../../../shared/mbs/mbs.module';

import { SampleTreemap1Component } from './sample-1/sample-treemap-1.component';
import { SampleTreemap2Component } from './sample-2/sample-treemap-2.component';
import { SampleTreemap3Component } from './sample-3/sample-treemap-3.component';

import { SampleTreemapRoutingModule } from './sample-treemap-routing.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, MbsModule, SampleTreemapRoutingModule
  ],
  declarations: [
    SampleTreemap1Component, SampleTreemap2Component, SampleTreemap3Component
  ]
})
export class SampleTreemapModule { }
