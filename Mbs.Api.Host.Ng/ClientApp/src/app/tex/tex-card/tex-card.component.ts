import { Component, Input, ChangeDetectorRef, AfterViewChecked } from '@angular/core';

import { MathJaxDirective } from '../../shared/math-jax/math-jax.directive';
import { Sample } from '../samples/sample';

@Component({
    selector: 'app-tex-card',
    templateUrl: './tex-card.component.html',
    styleUrls: ['./tex-card.component.scss']
})
export class TexCardComponent implements AfterViewChecked {
    @Input() sample: Sample;

    constructor(private changeDetectionRef: ChangeDetectorRef) { }

    ngAfterViewChecked() {
        this.changeDetectionRef.detectChanges();
    }

    updateMathJax() {
        MathJax.Hub.Queue(['Typeset', MathJax.Hub, 'MathJax']);
    }
}
