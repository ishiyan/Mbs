import { Component, OnInit, ElementRef, ViewChild, Input } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { InstrumentType } from '../../../../shared/mbs/instruments/types/instrument-type.enum';
import { ExchangeMic } from '../../../../shared/mbs/markets/exchange-mic.enum';
import { CurrencyCode } from '../../../../shared/mbs/currencies/currency-code.enum';
import { Instrument } from '../../../../shared/mbs/instruments/instrument';
import { ListService } from './list.service';
import { SnackBarService } from '../../../../shared/snack-bar/snack-bar.service';

@Component({
  selector: 'app-table12',
  templateUrl: './table12.component.html',
  styleUrls: ['./table12.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', display: 'none' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class Table12Component implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  public InstrumentType = InstrumentType;
  public ExchangeMic = ExchangeMic;
  public CurrencyCode = CurrencyCode;
  public expandedInstrument: Instrument | null;
  displayedColumns: string[] = ['type', 'symbol', 'name', 'isin', 'mic'];
  dataSource: MatTableDataSource<Instrument>;

  constructor(private listService: ListService, private snackBarService: SnackBarService) {
    this.dataSource = new MatTableDataSource<Instrument>();
    // @ts-ignore
    this.dataSource.filterPredicate = (data: Instrument, filter: string) => {
      // return (data.description && data.description.toLowerCase().indexOf(filter) !== -1) ||
      return (data.name && data.name.toLowerCase().indexOf(filter) !== -1) ||
        (data.symbol && data.symbol.toLowerCase().indexOf(filter) !== -1) ||
        (data.isin && data.isin.toLowerCase().indexOf(filter) !== -1) ||
        (data.type && data.type.toLowerCase().indexOf(filter) !== -1) ||
        (data.mic && data.mic.toLowerCase().indexOf(filter) !== -1) ||
        (data.stock &&
          ((data.stock.currency && data.stock.currency.toLowerCase().indexOf(filter) !== -1) ||
            (data.stock.cfi && data.stock.cfi.toLowerCase().indexOf(filter) !== -1) ||
            (data.stock.icb && data.stock.icb.toLowerCase().indexOf(filter) !== -1) ||
            (data.stock.tradingMode && data.stock.tradingMode.toLowerCase().indexOf(filter) !== -1))) ||
        (data.index &&
          ((data.index.currency && data.index.currency.toLowerCase().indexOf(filter) !== -1) ||
            (data.index.kind && data.index.kind.toLowerCase().indexOf(filter) !== -1) ||
            (data.index.weighting && data.index.weighting.toLowerCase().indexOf(filter) !== -1) ||
            (data.index.family && data.index.family.toLowerCase().indexOf(filter) !== -1))) ||
        (data.etf &&
          ((data.etf.currency && data.etf.currency.toLowerCase().indexOf(filter) !== -1) ||
            (data.etf.expositionType && data.etf.expositionType.toLowerCase().indexOf(filter) !== -1) ||
            (data.etf.cfi && data.etf.cfi.toLowerCase().indexOf(filter) !== -1) ||
            (data.etf.tradingMode && data.etf.tradingMode.toLowerCase().indexOf(filter) !== -1) ||
            (data.etf.issuer && data.etf.issuer.toLowerCase().indexOf(filter) !== -1) ||
            (data.etf.indexFamily && data.etf.indexFamily.toLowerCase().indexOf(filter) !== -1) ||
            (data.etf.underlying &&
              ((data.etf.underlying.mic && data.etf.underlying.mic.toLowerCase().indexOf(filter) !== -1) ||
                (data.etf.underlying.name && data.etf.underlying.name.toLowerCase().indexOf(filter) !== -1) ||
                (data.etf.underlying.symbol && data.etf.underlying.symbol.toLowerCase().indexOf(filter) !== -1
                ) ||
                (data.etf.underlying.isin && data.etf.underlying.isin.toLowerCase().indexOf(filter) !== -1))) ||
            (data.etf.inav &&
              ((data.etf.inav.mic && data.etf.inav.mic.toLowerCase().indexOf(filter) !== -1) ||
                (data.etf.inav.name && data.etf.inav.name.toLowerCase().indexOf(filter) !== -1) ||
                (data.etf.inav.symbol && data.etf.inav.symbol.toLowerCase().indexOf(filter) !== -1) ||
                (data.etf.inav.isin && data.etf.inav.isin.toLowerCase().indexOf(filter) !== -1))))) ||
        (data.etv &&
          ((data.etv.currency && data.etv.currency.toLowerCase().indexOf(filter) !== -1) ||
            (data.etv.tradingMode && data.etv.tradingMode.toLowerCase().indexOf(filter) !== -1) ||
            (data.etv.issuer && data.etv.issuer.toLowerCase().indexOf(filter) !== -1))) ||
        (data.inav &&
          ((data.inav.currency && data.inav.currency.toLowerCase().indexOf(filter) !== -1) ||
            (data.inav.target &&
              ((data.inav.target.mic && data.inav.target.mic.toLowerCase().indexOf(filter) !== -1) ||
                (data.inav.target.name && data.inav.target.name.toLowerCase().indexOf(filter) !== -1) ||
                (data.inav.target.symbol && data.inav.target.symbol.toLowerCase().indexOf(filter) !== -1) ||
                (data.inav.target.isin && data.inav.target.isin.toLowerCase().indexOf(filter) !== -1))))) ||
        (data.fund &&
          ((data.fund.currency.toLowerCase().indexOf(filter) !== -1) ||
            (data.fund.tradingMode && data.fund.tradingMode.toLowerCase().indexOf(filter) !== -1) ||
            (data.fund.cfi && data.fund.cfi.toLowerCase().indexOf(filter) !== -1) ||
            (data.fund.issuer && data.fund.issuer.toLowerCase().indexOf(filter) !== -1)));
    };
  }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.listService.getInstrumentList('euronext')
      .subscribe({
        next: list => {
          this.expandedInstrument = null;
          this.dataSource.data = list;
        },
        error: error => {
          this.expandedInstrument = null;
          this.dataSource.data = [];
          this.snackBarService.add(error as string);
          console.error(error as string);
        }
      });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getMic(instrument: Instrument): ExchangeMic {
    // @ts-ignore
    return ExchangeMic[instrument.mic];
  }

  getType(instrument: Instrument): InstrumentType {
    // @ts-ignore
    return InstrumentType[instrument.type];
  }

  helpWindow(event: any) {
    window.open(document.URL, '_blank', 'location=no,height=570,width=520,scrollbars=yes,status=yes');
  }
}
