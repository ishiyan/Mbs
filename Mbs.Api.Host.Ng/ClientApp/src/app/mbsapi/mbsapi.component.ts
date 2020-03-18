import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { MbsApiSample } from './mbsapi-samples/mbsapi-sample';
import { samples } from './mbsapi-samples/mbsapi-samples';

@Component({
  selector: 'app-mbsapi',
  templateUrl: './mbsapi.component.html',
  styleUrls: ['./mbsapi.component.scss']
})
export class MbsApiComponent {
  public readonly samples: MbsApiSample[] = samples;
  private sample: MbsApiSample = samples[0];

  constructor(router: Router) {
    const routeUrl = router.routerState.snapshot.url;
    for (let i = 0; i < samples.length; ++i) {
      const smp = samples[i];
      const url = '/mbsapi/' + smp.route;
      if (routeUrl === url) {
        this.sample = smp;
        break;
      }
    }
  }

}
