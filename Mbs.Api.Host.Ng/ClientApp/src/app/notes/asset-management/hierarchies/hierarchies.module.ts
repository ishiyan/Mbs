import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../../../shared/material/material.module';
import { KatexModule } from '../../../shared/katex/katex.module';
import { MbsModule } from '../../../shared/mbs/mbs.module';

import { DemoComponent } from './demo/demo.component';
import { IndustryClassificationsComponent } from './industry-classifications/industry-classifications.component';

import { HierarchiesRoutingModule } from './hierarchies-routing.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, KatexModule, MbsModule, HierarchiesRoutingModule
  ],
  declarations: [
    DemoComponent, IndustryClassificationsComponent
  ]
})
export class HierarchiesModule { }
