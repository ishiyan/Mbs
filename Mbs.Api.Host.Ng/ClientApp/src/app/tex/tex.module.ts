import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { TexRoutingModule } from './tex-routing.module';
import { MathJaxModule } from '../shared/math-jax/math-jax.module';
import { MaterialModule } from '../shared/material/material.module';
import { FooterModule } from '../shared/footer/footer.module';
import { TexComponent } from './tex.component';
import { TexListComponent } from './tex-list/tex-list.component';
import { TexCardComponent } from './tex-card/tex-card.component';

@NgModule({
  imports: [
    CommonModule, FormsModule, FlexLayoutModule,
    TexRoutingModule, MaterialModule, FooterModule, MathJaxModule
  ],
  declarations: [TexComponent, TexListComponent, TexCardComponent]
})
export class TexModule { }
