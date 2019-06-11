import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TexComponent } from './tex.component';

const routes: Routes = [
    { path: '', component: TexComponent },
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TexRoutingModule { }
