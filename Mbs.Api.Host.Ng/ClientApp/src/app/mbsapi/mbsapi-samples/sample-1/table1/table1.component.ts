import { Component, OnInit, ElementRef, ViewChild, Input } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { InstrumentType } from '../../../../shared/mbs/instruments/types/instrument-type.enum';
import { ExchangeMic } from '../../../../shared/mbs/markets/exchange-mic.enum';
import { CurrencyCode } from '../../../../shared/mbs/currencies/currency-code.enum';
import { Instrument } from '../../../../shared/mbs/instruments/instrument';
// import { euronextListShort } from '../../../../shared/mbs/euronext-list-short';

@Component({
  selector: 'app-table1',
  templateUrl: './table1.component.html',
  styleUrls: ['./table1.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', display: 'none' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class Table1Component implements OnInit {
  @ViewChild('container') container: ElementRef;

  public InstrumentType = InstrumentType;
  public ExchangeMic = ExchangeMic;
  public CurrencyCode = CurrencyCode; // added
  public expandedInstrument: Instrument; // added
  displayedColumns: string[] = ['type', 'symbol', 'name', 'isin', 'mic'];
  dataSource = []; // euronextListShort;

  constructor(private element: ElementRef) {
  }

  ngOnInit() {
    console.log('ngOnInit');
  }
}
