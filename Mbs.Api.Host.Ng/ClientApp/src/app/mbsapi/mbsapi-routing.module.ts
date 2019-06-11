import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MbsApiComponent } from './mbsapi.component';
import { Sample1Component } from './mbsapi-samples/sample-1/sample-1.component';
import { Sample2Component } from './mbsapi-samples/sample-2/sample-2.component';

const routes: Routes = [
    { path: '', component: MbsApiComponent, children: [
        { path: '', component: Sample1Component },
        { path: 'sample-1', component: Sample1Component },
        { path: 'sample-2', component: Sample2Component }
    ]},
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MbsApiRoutingModule { }
