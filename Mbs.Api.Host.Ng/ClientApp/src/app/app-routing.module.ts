import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: 'tex', loadChildren: () => import('./tex/tex.module').then(m => m.TexModule) },
  { path: 'd3', loadChildren: () => import('./d3/d3.module').then(m => m.D3Module) },
  { path: 'mbsapi', loadChildren: () => import('./mbsapi/mbsapi.module').then(m => m.MbsApiModule) },
  { path: '**', redirectTo: 'tex', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'enabled',
    anchorScrolling: 'enabled',
    relativeLinkResolution: 'corrected'    
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
