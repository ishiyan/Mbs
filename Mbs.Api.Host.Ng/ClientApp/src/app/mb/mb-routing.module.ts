import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MbComponent } from './mb.component';
import { SyntheticDataComponent } from './mb-samples/synthetic-data/synthetic-data.component';
import { Sample1Component } from './mb-samples/sample-1/sample-1.component';
import { Sample6Component } from './mb-samples/sample-6/sample-6.component';

const routes: Routes = [
  {
    path: '', component: MbComponent, children: [
      { path: 'synthetic-data', component: SyntheticDataComponent },
      { path: 'sample-1', component: Sample1Component },
      { path: 'sample-6', component: Sample6Component },
      {
        path: 'comp-sparkline', loadChildren: () =>
          import('./mb-samples/components/sparkline/sample-sparkline.module').then(m => m.SampleSparklineModule)
      },
      {
        path: 'comp-multiline', loadChildren: () =>
          import('./mb-samples/components/multiline/sample-multiline.module').then(m => m.SampleMultilineModule)
      },
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
