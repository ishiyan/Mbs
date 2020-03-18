import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  // {[route-id], text} pairs
  public toolbarItems: any = {
    ['mbsapi']: 'Data', // Mbs, D3, TeX
    ['d3']: 'Indicators',
    ['tex']: 'Strategies',
    ['notes']: 'Notes'
  };
}
