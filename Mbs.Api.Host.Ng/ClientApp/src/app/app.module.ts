import 'hammerjs';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { Directive } from '@angular/core';

import { MaterialModule } from './shared/material/material.module';
import { MathJaxModule } from './shared/math-jax/math-jax.module';
import { ToolbarModule } from './shared/toolbar/toolbar.module';
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
        AppRoutingModule, MaterialModule, ToolbarModule, SnackBarModule, MbsModule, MathJaxModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
