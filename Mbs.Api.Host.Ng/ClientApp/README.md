# Notes

## To do

- Migrate Nbs to `.Net 6`.
- In `tsconfig.json` set `"strictTemplates": true` in `angularCompilerOptions` and fix all errors in html. 
- Lint errors one-by-one.
- Latest Katex 0.15.1 does non work.
- MathJax does not redraw when changing decks.
- Update price histories from Binck.
- Price chart does not work for Quotes (in synthetics).
- Restructure `Notes` to a blog style with categories, searcheable text, etc.
- Finish Industry Classifications presentation. More text, more classifications. Academic style, footnotes.
- Make dummy up-to-date spec tests.

## Updating

Install latest tools, run `ng update`, run `ncu`.

```bash
npm install -g @angular/cli@latest
npm install -g npm-check-updates@latest
npm list -g

ng update
ng update --force @angular/cli @angular/core @angular-eslint/schematics @angular/material @angular/cdk

ncu
# edit package.json
npm install
```

## Naming and styling

Follow Angular [naming conventions](https://github.com/angular/angular/blob/master/docs/NAMING.md),
TypeScript [coding guidelines](https://github.com/Microsoft/TypeScript/wiki/Coding-guidelines) and
Angular [coding style guide](https://angular.io/guide/styleguide).

## Azure

- https://mbrane.visualstudio.com/IvanShiyan/_build?definitionId=10&_a=summary
- https://portal.azure.com/#@ivanshiyanalex.onmicrosoft.com/dashboard/private/5441bbe8-b3a7-4a41-a3b3-2932b0798271

## Material colors

- https://material.io/tools/color/#!/?view.left=0&view.right=0&primary.color=3F51B5&secondary.color=9E9E9E

## Material icons

- https://material.io/tools/icons/?search=fill&icon=remove_red_eye&style=baseline
- https://materialdesignicons.com/

## D3

- https://github.com/glouwa/d3-hypertree/tree/master/src
- https://bl.ocks.org/mbostock/3035090

## hexagons

- https://www.redblobgames.com/grids/hexagons/
- https://github.com/d3/d3-plugins/blob/master/hexbin/hexbin.js
- https://www.visualcinnamon.com/2013/07/self-organizing-maps-creating-hexagonal.html
- https://www.superdatascience.com/blogs/the-ultimate-guide-to-self-organizing-maps-soms

## Save to PNG

- https://stackoverflow.com/questions/36303964/save-d3-chart-as-image
- http://bl.ocks.org/Rokotyan/0556f8facbaf344507cdc45dc3622177

## Misc

- https://github.com/manfredsteyer/angular-oauth2-oidc
- https://manfredsteyer.github.io/angular-oauth2-oidc/docs/additional-documentation/callback-after-login.html
- https://connect2id.com/learn/openid-connect
- http://docs.iatec.com/projects/auth/en/latest/misc/2_credentials.html
- https://damienbod.com/2017/06/16/angular-oidc-oauth2-client-with-google-identity-platform/
- https://openid.net/specs/openid-connect-implicit-1_0.html
- https://openid.net/specs/openid-connect-basic-1_0.html
- https://developers.google.com/identity/protocols/OpenIDConnect
- https://developer.humanbrainproject.eu/docs/projects/HBP%20Identity%20Management/1.2/developer-manual/introduction.html
- https://github.com/JasonGT/CodingFlowBuildingSpaSeries/releases/tag/v1.0
- http://www.codingflow.net/building-single-page-applications-on-asp-net-core-2-1-with-angular-6-part-1-getting-started/
- https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/angular?view=aspnetcore-2.1&tabs=visual-studio
- https://docs.microsoft.com/en-us/aspnet/core/tutorials/dotnet-watch?view=aspnetcore-2.1

## Updates

1. https://github.com/angular/angular-cli/releases
2. https://github.com/angular/angular/blob/master/CHANGELOG.md, https://github.com/angular/angular/blob/master/docs/RELEASE_SCHEDULE.md
3. https://github.com/angular/material2/blob/master/CHANGELOG.md
4. https://github.com/d3/d3/releases
5. https://www.mathjax.org/news/#new-in-release
6. https://github.com/ReactiveX/rxjs/blob/master/CHANGELOG.md
7. https://github.com/angular/flex-layout/blob/master/CHANGELOG.md
8. http://hammerjs.github.io/changelog/
9. https://github.com/google/material-design-icons/releases
10. https://github.com/angular/zone.js/blob/master/CHANGELOG.md
11. https://github.com/Microsoft/tslib/releases
12. https://github.com/zloirock/core-js/blob/master/CHANGELOG.md
13. https://github.com/Kcnarf/d3-voronoi-treemap/releases
14. https://github.com/Kcnarf/d3-voronoi-map/releases
15. https://github.com/KaTeX/KaTeX/releases
16. https://github.com/jossef/material-design-icons-iconfont/releases, icons themselves https://jossef.github.io/material-design-icons-iconfont/

## Opt-out ng usage analytics

```bash
ng config --global cli.analyticsSharing undefined
ng analytics off off
```

## KaTeX 0.15.1 fix

Or delete `"@types/katex": "0.11.1",` from `package.json` untill newer version.

New versions of `katex` package produce `Error: export 'render' (imported as 'render') was not found in 'katex' (possible exports: default)` when compiling.

The workaround is to delete the following `exports` block in the `node_modules\katex\package.json`:

```json
{
  "name": "katex",
  "version": "0.15.1",
  "description": "Fast math typesetting for the web.",
  "main": "dist/katex.js",
  /* ------- DELETE FROM HERE ------------ */
  "exports": {
    ".": {
      "require": "./dist/katex.js",
      "import": "./dist/katex.mjs"
    },
    "./contrib/auto-render": {
      "require": "./dist/contrib/auto-render.js",
      "import": "./dist/contrib/auto-render.mjs"
    },
    "./contrib/mhchem": {
      "require": "./dist/contrib/mhchem.js",
      "import": "./dist/contrib/mhchem.mjs"
    },
    "./contrib/copy-tex": {
      "require": "./dist/contrib/copy-tex.js",
      "import": "./dist/contrib/copy-tex.mjs"
    },
    "./contrib/mathtex-script-type": {
      "require": "./dist/contrib/mathtex-script-type.js",
      "import": "./dist/contrib/mathtex-script-type.mjs"
    },
    "./contrib/render-a11y-string": {
      "require": "./dist/contrib/render-a11y-string.js",
      "import": "./dist/contrib/render-a11y-string.mjs"
    },
    "./*": "./*"
  },
  /* ------- DELETE TO HERE ------------ */
  /* ... */
}
```

## MathJax in CLI project

Version 3 support: add the following to the `node_modules\@types\mathjax\index.d.ts`:

```ts
declare namespace MathJax {
  export const Hub: Hub;
  export const Ajax: Ajax;
  export const Message: Message;
  export const HTML: HTML;
  export const Callback: Callback;
  export const Localization: Localization;
  export const InputJax: InputJax;
  export const OutputJax: OutputJax;
  //
  // Mathjax@3 surrpot -----------
  //
  export const typeset: () => any;
  export const typesetPromise: () => any;
  export const startup: Startup;

  export interface Startup {
    promise: any;
  }
  //
  // -----------------------------------
  //
```

- 1. Create a new project
- 2. Install MathJax and types

```shell
npm install mathjax@latest --save
npm -i @types/mathjax --only=dev
```

- 3. Add script to the `angular-cli.json`

```json
"scripts": ["../node_modules/mathjax/MathJax.js"],
```

- 4. Add to the `index/html`

```html
<head>
...
<script type="text/x-mathjax-config">
    MathJax.Hub.Config({
      tex2jax: { inlineMath: [ ["$", "$"], ["\\(","\\)"] ], displayMath: [ ["$$","$$"], ["\\[", "\\]"] ], processEscapes: true, ignoreClass: "tex2jax_ignore|dno" },
      TeX: { noUndefined: { attributes: { mathcolor: "red", mathbackground: "#FFEEEE", mathsize: "90%" } } },
      messageStyle: "none"
</script>
<script type="text/javascript" async
  src="https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.1/MathJax.js?config=TeX-AMS_CHTML">
</script>
<!--
Or the following if you want to serve it offline:
<script type="text/javascript" async src="assets/mathjax/MathJax.js?config=TeX-AMS_CHTML"></script>
-->
</head>
```

- 5. Add to `tsconfig.app.json` and `tsconfig.spec.json`

```json
"types": ["MathJax"]
```

- 6. Add a new file `mathjax.directive.ts` to the `app` folder

```typescript
import {Directive, ElementRef, Input, OnChanges, OnInit} from '@angular/core';
@Directive({
  selector: '[appMathJax]'
})
export class MathJaxDirective implements OnInit, OnChanges {
  @Input('MathJax') private value = '';
  constructor(private element: ElementRef) {
  }
  ngOnInit() {
    this.element.nativeElement.innerHTML = this.value;
    MathJax.Hub.Queue(['Typeset', MathJax.Hub, this.element.nativeElement]);
  }
  ngOnChanges() {
    this.element.nativeElement.innerHTML = this.value;
    MathJax.Hub.Queue(['Typeset', MathJax.Hub, this.element.nativeElement]);
  }
}
```

- 7. Add to `app.module.ts`

```typescript
...
import { MathJaxDirective } from './mathjax.directive';
...
declarations: [
 ...,
 MathJaxDirective
 ],
```

- 8. Update `app.component.ts` to

```typescript
import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { MathJaxDirective } from './mathjax.directive';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnChanges {
  @Input() texString: String = '$M = \\begin{bmatrix}x_1 & \\cdots & n_1 \\\\x_2 & \\cdots & n_2 \\\\x_3 & \\cdots & n_3 \\\\x_4 & \\cdots & n_4 \\\\x_5 & \\cdots & n_5 \\end{bmatrix}$';
  formulae: string = '$$\\gamma=\\lim_{n\\to\\infty}\\left(\\sum_{k=1}^n\\frac1k\\right)-\\frac1n$$';
  fractionString: string = 'Inside Angular one half = $\\frac 12$';
  index: number = 3;
  ngOnInit(): void {}
  ngOnChanges(): void {}
  update () {
    this.fractionString = 'Inside Angular one third = $\\frac 1' + this.index + '$';
    this.index++;
  }
  updateArea () {
    MathJax.Hub.Queue(['Typeset', MathJax.Hub, 'MathJax']);
  }
}
```

- 9. Update `app.component.html` to

```html
<h1 appMathJax [MathJax]="formulae">{{formulae}}</h1>
<h1 appMathJax [MathJax]="fractionString">{{fractionString}}</h1>
<button (click)="update()" style="background-color:red;color:white"><b>Increment</b></button>
<br/>
<br/>
<br/>
<div class="container">
  <div class="row">
    <div class="col" layout-align="right centre">
      <div appMathJax [MathJax]="texString" style="padding-top: 10px"></div>
    </div>
  </div>
</div>
<br/>
<br/>
<br/>
<textarea #txt>$\sum_{i=1}^n i^3 = (\frac{n(n+1)}{2})^2$</textarea>
<br/>
<button (click)="updateArea()" style="background-color:red;color:white"><b>Update</b></button>
<br/>
<span appMathJax [MathJax]="txt.value"></span>
```

- 10. (Optional) Create batch files to copy mathjax to the assets folder

copy_mathjax_to_assets.bat

```shell
@echo off
set source_folder=node_modules\mathjax
set target_folder=src\assets\mathjax
rmdir /S /Q "%target_folder%"
mkdir "%target_folder%"
copy "%source_folder%\MathJax.js" "%target_folder%\MathJax.js"
xcopy /S /Q /G /R /Y /I "%source_folder%\config" "%target_folder%\config"
xcopy /S /Q /G /R /Y /I "%source_folder%\extensions" "%target_folder%\extensions"
xcopy /S /Q /G /R /Y /I "%source_folder%\jax" "%target_folder%\jax"
xcopy /S /Q /G /R /Y /I "%source_folder%\localization" "%target_folder%\localization"
xcopy /S /Q /G /R /Y /I "%source_folder%\fonts" "%target_folder%\fonts"
```

link_mathjax_to_assets.cmd

```shell
@echo off
set source_folder=node_modules\mathjax
set target_folder=src\assets\mathjax
rmdir /S /Q "%target_folder%"
mkdir "%target_folder%"
copy "%source_folder%\MathJax.js" "%target_folder%\MathJax.js"
mklink /J "%target_folder%\config" "%source_folder%\config"
mklink /J "%target_folder%\extensions" "%source_folder%\extensions"
mklink /J "%target_folder%\jax" "%source_folder%\jax"
mklink /J "%target_folder%\localization" "%source_folder%\localization"
mklink /J "%target_folder%\fonts" "%source_folder%\fonts"
```

delete_mathjax_from_assets.cmd

```shell
@echo off
set target_folder=src\assets\mathjax
rmdir /S /Q "%target_folder%"
```

## Using D3 in NgCli

```shell
npm install -prod d3@latest @types/d3@latest
```

add to `src/typings.d.ts`

```typescript
declare module 'd3' {
  export * from 'd3-array';
  export * from 'd3-axis';
  export * from 'd3-brush';
  export * from 'd3-chord';
  export * from 'd3-collection';
  export * from 'd3-color';
  export * from 'd3-dispatch';
  export * from 'd3-drag';
  export * from 'd3-dsv';
  export * from 'd3-ease';
  export * from 'd3-force';
  export * from 'd3-format';
  export * from 'd3-geo';
  export * from 'd3-hierarchy';
  export * from 'd3-interpolate';
  export * from 'd3-path';
  export * from 'd3-polygon';
  export * from 'd3-quadtree';
  export * from 'd3-queue';
  export * from 'd3-random';
  export * from 'd3-request';
  export * from 'd3-scale';
  export * from 'd3-selection';
  export * from 'd3-shape';
  export * from 'd3-time';
  export * from 'd3-time-format';
  export * from 'd3-timer';
  export * from 'd3-transition';
  export * from 'd3-voronoi';
  export * from 'd3-zoom';
}

```

## Using Material Design icons

```shell
npm install -prod material-design-icons@latest
```

You can use it in two ways.

First one. Add to the `head` section of the `index.html`:

```html
<link href="https://fonts.googleapis.com/icon?family=Material+Icons&display=outlined" rel="stylesheet">
```

Second one. Add to the `styles.scss`:

```scss
@import '~material-design-icons/iconfont/material-icons.css';
```

## Using up-to-date Material Design icons font

The original Google package described above is not up-to-date and misses many icons.
You can use a font-replacement package which has up-to-date fonts.

This font has only regular, not outline and rounded flavours. 

```shell
npm install -prod material-design-icons-iconfont@latest
```

Add to the `styles.scss`:

```scss
$material-design-icons-font-directory-path: '~material-design-icons-iconfont/dist/fonts/';
@import '~material-design-icons-iconfont/src/material-design-icons';
```

## Typescript warnings

```shell
ng set warnings.typescriptMismatch=false
```

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `-prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
