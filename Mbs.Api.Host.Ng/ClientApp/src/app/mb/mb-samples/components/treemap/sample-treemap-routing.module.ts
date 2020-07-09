import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SampleTreemap1Component } from './sample-1/sample-treemap-1.component';
import { SampleTreemap2Component } from './sample-2/sample-treemap-2.component';
import { SampleTreemap3Component } from './sample-3/sample-treemap-3.component';

const routes: Routes = [
  { path: 's1', component: SampleTreemap1Component },
  { path: 's2', component: SampleTreemap2Component },
  { path: 's3', component: SampleTreemap3Component },
  { path: '**', redirectTo: 's1' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SampleTreemapRoutingModule { }
