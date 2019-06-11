import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnDestroy } from '@angular/core';

import { D3Sample } from './d3-samples/d3-sample';
import { samples } from './d3-samples/d3-samples';

@Component({
    selector: 'app-d3',
    templateUrl: './d3.component.html',
    styleUrls: ['./d3.component.scss']
})
export class D3Component implements OnDestroy {
    public readonly samples: D3Sample[] = samples;
    private sample: D3Sample = samples[0];
    mobileQuery: MediaQueryList;
    private mobileQueryListener: () => void;

    constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher) {
        this.mobileQuery = media.matchMedia('(max-width: 720px)');
        this.mobileQueryListener = () => changeDetectorRef.detectChanges();
        this.mobileQuery.addListener(this.mobileQueryListener);
    }

    ngOnDestroy(): void {
        this.mobileQuery.removeListener(this.mobileQueryListener);
    }

    public get currentSample(): D3Sample {
        return this.sample;
    }

    public set selectedSample(sample: D3Sample) {
        this.sample = sample;
    }
}
