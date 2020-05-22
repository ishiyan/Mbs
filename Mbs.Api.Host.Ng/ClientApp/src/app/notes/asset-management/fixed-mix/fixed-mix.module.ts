import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../../../shared/material/material.module';
import { KatexModule } from '../../../shared/katex/katex.module';
import { MbsModule } from '../../../shared/mbs/mbs.module';

import { BucketsComponent } from './buckets/buckets.component';
import { BucketsInteractiveComponent } from './buckets-interactive/buckets-interactive.component';
import { SingleComponent } from './single/single.component';
import { SingleInteractiveComponent } from './single-interactive/single-interactive.component';

import { FixedMixRoutingModule } from './fixed-mix-routing.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, KatexModule, MbsModule, FixedMixRoutingModule
  ],
  declarations: [
    BucketsComponent, BucketsInteractiveComponent, SingleComponent, SingleInteractiveComponent
  ]
})
export class FixedMixModule { }
