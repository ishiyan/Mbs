import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SampleIcicle1Component } from './sample-1/sample-icicle-1.component';
import { SampleIcicle2Component } from './sample-2/sample-icicle-2.component';
import { SampleIcicle3Component } from './sample-3/sample-icicle-3.component';

const routes: Routes = [
  { path: 's1', component: SampleIcicle1Component },
  { path: 's2', component: SampleIcicle2Component },
  { path: 's3', component: SampleIcicle3Component },
  { path: '**', redirectTo: 's1' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SampleIcicleRoutingModule { }
