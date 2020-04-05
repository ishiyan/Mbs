import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { FooterModule } from '../shared/footer/footer.module';
import { MaterialModule } from '../shared/material/material.module';
import { SnackBarModule } from '../shared/snack-bar/snack-bar.module';
import { SvgViewerModule } from '../shared/svg-viewer/svg-viewer.module';
import { MbsModule } from '../shared/mbs/mbs.module';

import { NotesRoutingModule } from './notes-routing.module';
import { NotesComponent } from './notes.component';
import { AssetManagementModule } from './asset-management/asset-management.module';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    MaterialModule, SnackBarModule, MbsModule, FooterModule, SvgViewerModule,
    NotesRoutingModule, AssetManagementModule
  ],
  declarations: [
    NotesComponent
  ],
  providers: []
})
export class NotesModule { }
