import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { D3Sample } from './d3-samples/d3-sample';
import { samples } from './d3-samples/d3-samples';

@Component({
  selector: 'app-d3',
  templateUrl: './d3.component.html',
  styleUrls: ['./d3.component.scss']
})
export class D3Component {
  public readonly samples: D3Sample[] = samples;
  public sample: D3Sample = samples[0];

  constructor(router: Router) {
    const routeUrl = router.routerState.snapshot.url;
    for (let i = 0; i < samples.length; ++i) {
      const smp = samples[i];
      const url = '/d3/' + smp.route;
      if (routeUrl === url) {
        this.sample = smp;
        break;
      }
    }
  }

}
