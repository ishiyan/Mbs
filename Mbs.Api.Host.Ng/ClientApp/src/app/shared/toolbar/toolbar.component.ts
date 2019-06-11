import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import {DomSanitizer} from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';

@Component({
    selector: 'app-toolbar',
    templateUrl: './toolbar.component.html',
    styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent {
    private map: any = {};
    private mapKeys: any = [];

    constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
        iconRegistry.addSvgIcon(
            'mbrane',
            sanitizer.bypassSecurityTrustResourceUrl('assets/img/mbrane-top.svg'));
      }
    
    @Input() public set toolbarItems(input: any) {
        this.map = input;
        this.mapKeys = Object.keys(input);
    }

    public get items() {
        return this.map;
    }

    public get itemKeys() {
        return this.mapKeys;
    }
}
