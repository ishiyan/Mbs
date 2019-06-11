// import { MediaMatcher } from '@angular/cdk/layout';
import { /*ChangeDetectorRef,*/ Component, OnDestroy } from '@angular/core';

import { MbsApiSample } from './mbsapi-samples/mbsapi-sample';
import { samples } from './mbsapi-samples/mbsapi-samples';

@Component({
    selector: 'app-mbsapi',
    templateUrl: './mbsapi.component.html',
    styleUrls: ['./mbsapi.component.scss']
})
export class MbsApiComponent implements OnDestroy {
    public readonly samples: MbsApiSample[] = samples;
    private sample: MbsApiSample = samples[0];

    /*mobileQuery: MediaQueryList;
    private mobileQueryListener: () => void;

    constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher) {
        this.mobileQuery = media.matchMedia('(max-width: 7200px)');
        this.mobileQueryListener = () => changeDetectorRef.detectChanges();
        this.mobileQuery.addListener(this.mobileQueryListener);
    }*/

    ngOnDestroy(): void {
        // this.mobileQuery.removeListener(this.mobileQueryListener);
    }

    public get currentSample(): MbsApiSample {
        return this.sample;
    }

    public set selectedSample(sample: MbsApiSample) {
        this.sample = sample;
    }
}
