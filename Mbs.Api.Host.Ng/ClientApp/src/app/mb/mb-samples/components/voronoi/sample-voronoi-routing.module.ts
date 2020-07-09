import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SampleVoronoi1Component } from './sample-1/sample-voronoi-1.component';
import { SampleVoronoi2Component } from './sample-2/sample-voronoi-2.component';
import { SampleVoronoi3Component } from './sample-3/sample-voronoi-3.component';

const routes: Routes = [
  { path: 's1', component: SampleVoronoi1Component },
  { path: 's2', component: SampleVoronoi2Component },
  { path: 's3', component: SampleVoronoi3Component },
  { path: '**', redirectTo: 's1' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SampleVoronoiRoutingModule { }
