import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../../../../shared/material/material.module';
import { MbsModule } from '../../../../shared/mbs/mbs.module';

import { SampleStackline1Component } from './sample-1/sample-stackline-1.component';

import { SampleStacklineRoutingModule } from './sample-stackline-routing.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, MbsModule, SampleStacklineRoutingModule
  ],
  declarations: [
    SampleStackline1Component
  ]
})
export class SampleStacklineModule { }
