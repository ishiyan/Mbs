import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MbComponent } from './mb.component';
import { SyntheticDataComponent } from './mb-samples/synthetic-data/synthetic-data.component';
import { Sample1Component } from './mb-samples/sample-1/sample-1.component';
import { Sample6Component } from './mb-samples/sample-6/sample-6.component';
import { SampleSparkline1Component } from './mb-samples/components/sparkline/sample-1/sample-sparkline-1.component';
import { SampleSparkline2Component } from './mb-samples/components/sparkline/sample-2/sample-sparkline-2.component';
import { SampleSparkline3Component } from './mb-samples/components/sparkline/sample-3/sample-sparkline-3.component';

const routes: Routes = [
  {
    path: '', component: MbComponent, children: [
      { path: 'synthetic-data', component: SyntheticDataComponent },
      { path: 'sample-1', component: Sample1Component },
      { path: 'sample-6', component: Sample6Component },
      { path: 'comp-sparkline-1', component: SampleSparkline1Component },
      { path: 'comp-sparkline-2', component: SampleSparkline2Component },
      { path: 'comp-sparkline-3', component: SampleSparkline3Component },
      { path: '', component: Sample6Component }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MbRoutingModule { }
