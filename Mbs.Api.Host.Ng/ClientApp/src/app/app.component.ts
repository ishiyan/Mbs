import { Component } from '@angular/core';

@Component({
  // eslint-disable-next-line @angular-eslint/component-selector
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  // {[route-id], text} pairs
  public toolbarItems: any = {
    // Data, Indicators, Strategies, Notes, Panopticum
    // Mb, D3, TeX
    ['mb']: 'Mb',
    ['d3']: 'D3',
    ['tex']: 'TeX',
    ['notes']: 'Notes'
  };
}
