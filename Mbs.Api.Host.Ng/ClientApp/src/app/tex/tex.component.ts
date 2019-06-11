import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnDestroy } from '@angular/core';

import { Category } from './categories/category';
import { categories } from './categories/categories';

@Component({
    selector: 'app-tex',
    templateUrl: './tex.component.html',
    styleUrls: ['./tex.component.scss']
})
export class TexComponent implements OnDestroy {
    private category: Category = categories[0];
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

    public get categories(): Category[] {
        return categories;
    }

    public get currentCategory(): Category {
        return this.category;
    }

    public set selectedCategory(category: Category) {
        this.category = category;
    }
}
