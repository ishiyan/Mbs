import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { Category } from './categories/category';
import { categories } from './categories/categories';

@Component({
  selector: 'app-tex',
  templateUrl: './tex.component.html',
  styleUrls: ['./tex.component.scss']
})
export class TexComponent {
  public readonly categories: Category[] = categories;
  public category: Category = categories[0];

  constructor(router: Router) {
    const routeUrl = router.routerState.snapshot.url;
    for (let i = 0; i < categories.length; ++i) {
      const cat = categories[i];
      const url = '/tex/' + cat.route;
      if (routeUrl === url) {
        this.category = cat;
        break;
      }
    }
  }

}
