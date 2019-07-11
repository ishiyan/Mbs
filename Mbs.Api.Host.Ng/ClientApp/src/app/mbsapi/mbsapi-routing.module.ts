import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MbsApiComponent } from './mbsapi.component';
import { SyntheticDataComponent } from './mbsapi-samples/synthetic-data/synthetic-data.component';
import { Sample1Component } from './mbsapi-samples/sample-1/sample-1.component';

const routes: Routes = [
    { path: '', component: MbsApiComponent, children: [
        { path: 'synthetic-data', component: SyntheticDataComponent },
        { path: 'sample-1', component: Sample1Component },
        { path: '', component: SyntheticDataComponent }
    ]},
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MbsApiRoutingModule { }
