import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule, MatIconModule, MatMenuModule, MatGridListModule, MatTooltipModule } from '@angular/material';
import { ThemePickerComponent } from './theme-picker.component';
import { ThemeManagerService } from './theme-manager.service';
import { ThemeStorageService } from './theme-storage.service';

@NgModule({
  imports: [
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatGridListModule,
    MatTooltipModule,
    CommonModule
  ],
  exports: [ThemePickerComponent],
  declarations: [ThemePickerComponent],
  providers: [ThemeManagerService, ThemeStorageService],
})
export class ThemePickerModule { }
