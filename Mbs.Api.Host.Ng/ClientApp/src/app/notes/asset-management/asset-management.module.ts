import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../../shared/material/material.module';
import { MbsModule } from '../../shared/mbs/mbs.module';

import { AssetManagementRoutingModule } from './asset-management-routing.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, MbsModule, AssetManagementRoutingModule
  ],
  declarations: []
})
export class AssetManagementModule { }
