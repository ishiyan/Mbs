import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../../../../shared/material/material.module';
import { MbsModule } from '../../../../shared/mbs/mbs.module';

import { SampleVoronoi1Component } from './sample-1/sample-voronoi-1.component';
import { SampleVoronoi2Component } from './sample-2/sample-voronoi-2.component';
import { SampleVoronoi3Component } from './sample-3/sample-voronoi-3.component';

import { SampleVoronoiRoutingModule } from './sample-voronoi-routing.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, MbsModule, SampleVoronoiRoutingModule
  ],
  declarations: [
    SampleVoronoi1Component, SampleVoronoi2Component, SampleVoronoi3Component
  ]
})
export class SampleVoronoiModule { }
