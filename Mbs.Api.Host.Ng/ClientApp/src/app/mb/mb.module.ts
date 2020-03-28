import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { FooterModule } from '../shared/footer/footer.module';
import { MaterialModule } from '../shared/material/material.module';
import { SnackBarModule } from '../shared/snack-bar/snack-bar.module';
import { SvgViewerModule } from '../shared/svg-viewer/svg-viewer.module';
import { MbsModule } from '../shared/mbs/mbs.module';
import { MbRoutingModule } from './mb-routing.module';
import { SyntheticDataComponent } from './mb-samples/synthetic-data/synthetic-data.component';
import { Sample1Component } from './mb-samples/sample-1/sample-1.component';
import { MbComponent } from './mb.component';
import { Table1Component } from './mb-samples/sample-1/table1/table1.component';
import { Table12Component } from './mb-samples/sample-1/table12/table12.component';
import { ListService } from './mb-samples/sample-1/table12/list.service';
import { Sample6Component } from './mb-samples/sample-6/sample-6.component';

import { SampleSparkline1Component } from './mb-samples/components/sparkline/sample-1/sample-sparkline-1.component';
import { SampleSparkline2Component } from './mb-samples/components/sparkline/sample-2/sample-sparkline-2.component';
import { SampleSparkline3Component } from './mb-samples/components/sparkline/sample-3/sample-sparkline-3.component';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MbRoutingModule, MaterialModule, SnackBarModule, MbsModule, FooterModule, SvgViewerModule
  ],
  declarations: [
    MbComponent, SyntheticDataComponent, Sample1Component, Table1Component, Table12Component, Sample6Component,
    SampleSparkline1Component, SampleSparkline2Component, SampleSparkline3Component
  ],
  providers: [ListService]
})
export class MbModule { }
