import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { ThemePickerModule } from './shared/theme-picker/theme-picker.module';
import { MaterialModule } from './shared/material/material.module';
import { MathJaxModule } from './shared/math-jax/math-jax.module';
import { KatexModule } from './shared/katex/katex.module';
import { ToolbarModule } from './shared/toolbar/toolbar.module';
import { FooterModule } from './shared/footer/footer.module';
import { SnackBarModule } from './shared/snack-bar/snack-bar.module';
import { MbsModule } from './shared/mbs/mbs.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, BrowserAnimationsModule, HttpClientModule, FormsModule, FlexLayoutModule,
    AppRoutingModule, MaterialModule, ToolbarModule, FooterModule, SnackBarModule, MbsModule,
    MathJaxModule.forRoot(), KatexModule, ThemePickerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
