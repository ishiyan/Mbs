import { Component, Input } from '@angular/core';

import { Sample } from '../samples/sample';

@Component({
    selector: 'app-tex-list',
    templateUrl: './tex-list.component.html',
    styleUrls: ['./tex-list.component.scss']
})
export class TexListComponent {
    @Input() samples: Sample[];
}
