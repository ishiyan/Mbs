import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { FooterModule } from '../shared/footer/footer.module';
import { MaterialModule } from '../shared/material/material.module';
import { SnackBarModule } from '../shared/snack-bar/snack-bar.module';
import { SvgViewerModule } from '../shared/svg-viewer/svg-viewer.module';
import { MbsModule } from '../shared/mbs/mbs.module';
import { MbsApiRoutingModule } from './mbsapi-routing.module';
import { SyntheticDataComponent } from './mbsapi-samples/synthetic-data/synthetic-data.component';
import { Sample1Component } from './mbsapi-samples/sample-1/sample-1.component';
import { MbsApiComponent } from './mbsapi.component';
import { Table1Component } from './mbsapi-samples/sample-1/table1/table1.component';
import { Table12Component } from './mbsapi-samples/sample-1/table12/table12.component';
import { ListService } from './mbsapi-samples/sample-1/table12/list.service';

@NgModule({
    imports: [
        CommonModule, FormsModule, FlexLayoutModule,
        MbsApiRoutingModule, MaterialModule, SnackBarModule, MbsModule, FooterModule, SvgViewerModule
    ],
    declarations: [
        MbsApiComponent, SyntheticDataComponent, Sample1Component, Table1Component, Table12Component
    ],
    providers: [ListService]
})
export class MbsApiModule { }
