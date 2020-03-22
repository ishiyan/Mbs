import { AfterViewInit, Directive, ElementRef, Input, OnChanges, OnDestroy, SimpleChanges } from '@angular/core';
import { combineLatest, Observable, ReplaySubject, Subject, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { MathJaxService } from './math-jax.service';

interface UpdateValue<T> {
  value: T;
  order: number;
}

/** Typeset the content or expressions using MathJax library. */
@Directive({
  selector: 'mathjax, [mathjax]'
})
export class MathJaxDirective implements AfterViewInit, OnChanges, OnDestroy {
  /** An array of input MathJax expressions. */
  @Input('mathjax')
  public MathJaxExpressions: string[];

  /** The associated native element. */
  private readonly element: HTMLElement;
  /** Observes the change of the input expression. */
  private expressionChangeSubject = new ReplaySubject<UpdateValue<string>[]>();
  /** Observes the completion of the initial MathJax typesetting. */
  private readonly mathJaxTypesetSubject = new Subject<any>();
  /** Observes the readiness of all the Jax instances in the element. */
  private readonly allJax$: Observable<any[]>;
  private readonly mathJaxHub$: Observable<any>;
  private readonly expressionChangeSubscription: Subscription;
  private readonly typesetSubscription: Subscription;
  private hubSubscription: Subscription;
  private isDestroying: boolean;

  constructor(el: ElementRef, service: MathJaxService) {
    this.mathJaxHub$ = service.MathJaxHub$;
    this.element = el.nativeElement;

    this.typesetSubscription = combineLatest([this.mathJaxHub$, this.mathJaxTypesetSubject])
      .subscribe(() => {
        MathJax.Hub.Queue(['Typeset', MathJax.Hub, this.element]);
      });

    this.allJax$ = combineLatest([this.mathJaxHub$, this.mathJaxTypesetSubject]).pipe(
      map(() => MathJax.Hub.getAllJax(this.element))
    );

    this.expressionChangeSubscription = combineLatest([this.allJax$, this.expressionChangeSubject])
      .subscribe(([jax, updateValue]) =>
        updateValue.forEach(v => MathJax.Hub.Queue(() => {
          // Stop pushing messages to the queue when the component is being destroyed.
          if (!this.isDestroying) {
            return jax[v.order].Text(v.value);
          }
        })));
  }

  ngAfterViewInit(): void {
    this.hubSubscription = this.mathJaxHub$
      .subscribe(() => {
        if (this.element) {
          MathJax.Hub.Queue(['Typeset', MathJax.Hub, this.element]);
        }
        try {
          MathJax.Hub.Queue(['MathJaxTypeset', this]);  
        } catch {
        }
      });
  }

  /** Explicitly trigger the *MathJax* typeset process. This is useful if the content is loaded dynamically. */
  public typeset(): void {
    this.mathJaxTypesetSubject.next();
  }

  ngOnChanges(changes: SimpleChanges): void {
    const expressions = changes.MathJaxExpressions;

    // Shortcut if there's nothing to update.
    if (!(expressions.currentValue instanceof Array)) {
      return;
    }

    // Update only the changed expressions.
    // Note that *expressions.currentValue* is defined, we checked it above.
    // Hence, gnore the TypeScript assignability error.
    // @ts-ignore
    const updateValues: UpdateValue<string>[] = expressions.currentValue
      .map((cv, i) =>
        (expressions.previousValue ? expressions.previousValue[i] !== cv : true) ?
          {
            value: expressions.currentValue[i],
            order: i
          }
          : undefined)
      .filter(v => v);
    this.expressionChangeSubject.next(updateValues);
  }

  ngOnDestroy(): void {
    this.isDestroying = true;

    this.expressionChangeSubscription.unsubscribe();
    this.hubSubscription.unsubscribe();
    this.typesetSubscription.unsubscribe();

    this.mathJaxTypesetSubject.complete();
    this.expressionChangeSubject.complete();
  }
}
