import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MathJaxDirective } from './math-jax.directive';
import { MathJaxService } from './math-jax.service';
import { ModuleConfiguration } from './module-configuration';

/** Module to load and configure the *MathJax* library. */
@NgModule({
  declarations: [MathJaxDirective],
  imports: [
    CommonModule
  ],
  exports: [MathJaxDirective]
})
export class MathJaxModule {
  constructor(service: MathJaxService, moduleConfig?: ModuleConfiguration) {
    service.init();

    /** Define the **function string** to be inserted into the mathjax configuration script block. */
    const mathJaxHubConfig = (() => {
      MathJax.Hub.Config({
        skipStartupTypeset: true,
        messageStyle: 'none',
        tex2jax: {
          inlineMath: [['$', '$'], ['\\(', '\\)']],
          displayMath: [['$$', '$$'], ['\\[', '\\]']],
          processEscapes: true,
          ignoreClass: "tex2jax_ignore|dno",
          preview: 'none'
        }
      });
      // @ts-ignore
      MathJax.Hub.Register.StartupHook('End', () => {
        window.mathJaxHub$.next();
        window.mathJaxHub$.complete();
      });
    }).toString();

    if (moduleConfig) {
      // Insert the MathJax configuration script into the Head element.
      (function () {
        const script = document.createElement('script') as HTMLScriptElement;
        script.type = 'text/x-mathjax-config';
        script.text = `(${mathJaxHubConfig})();`;
        document.getElementsByTagName('head')[0].appendChild(script);
      })();

      // Insert the script block to load the MathJax library.
      (function (version: string, config: string, online: boolean) {
        const script = document.createElement('script') as HTMLScriptElement;
        script.type = 'text/javascript';
        if (online) {
          script.src = `https://cdnjs.cloudflare.com/ajax/libs/mathjax/${version}/MathJax.js?config=${config}`;
        } else {
          script.src = `assets/mathjax/MathJax.js?config=${config}`;
        }
        script.async = true;
        document.getElementsByTagName('head')[0].appendChild(script);
      })(moduleConfig.version, moduleConfig.config, moduleConfig.online);
    }
  }

  /** Configure the module for the root module. */
  public static forRoot(moduleConfiguration: ModuleConfiguration = {
    version: '2.7.7',
    config: 'TeX-AMS_HTML',
    online: false
  }): ModuleWithProviders<MathJaxModule> {
    return {
      ngModule: MathJaxModule,
      providers: [
        { provide: ModuleConfiguration, useValue: moduleConfiguration },
        { provide: MathJaxService, useClass: MathJaxService },
      ]
    };
  }

  /** Configure the module for a child module. */
  public static forChild() {
    return {
      ngModule: MathJaxModule
    };
  }
}
