import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { FooterModule } from '../shared/footer/footer.module';
import { MaterialModule } from '../shared/material/material.module';
import { SnackBarModule } from '../shared/snack-bar/snack-bar.module';
import { MbsModule } from '../shared/mbs/mbs.module';
import { MbsApiRoutingModule } from './mbsapi-routing.module';
import { Sample1Component } from './mbsapi-samples/sample-1/sample-1.component';
import { Sample2Component } from './mbsapi-samples/sample-2/sample-2.component';
import { MbsApiComponent } from './mbsapi.component';
import { Table1Component } from './mbsapi-samples/sample-1/table1/table1.component';
import { Table12Component } from './mbsapi-samples/sample-1/table12/table12.component';
import { Table2Component } from './mbsapi-samples/sample-2/table2/table2.component';
import { ListService } from './mbsapi-samples/sample-1/table12/list.service';

@NgModule({
    imports: [
        CommonModule, FormsModule, FlexLayoutModule,
        MbsApiRoutingModule, MaterialModule, SnackBarModule, MbsModule, FooterModule
    ],
    declarations: [
        MbsApiComponent, Sample1Component, Sample2Component, Table1Component, Table12Component, Table2Component
    ],
    providers: [ListService]
})
export class MbsApiModule { }
