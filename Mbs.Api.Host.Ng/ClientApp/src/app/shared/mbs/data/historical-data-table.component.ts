import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { TemporalEntityKind } from './entities/temporal-entity-kind.enum';
import { HistoricalData } from './historical-data';
import { Ohlcv } from './entities/ohlcv';
import { Quote } from '@angular/compiler';
import { Trade } from './entities/trade';
import { Scalar } from './entities/scalar';

@Component({
    selector: 'app-mbs-data-historical-data-table',
    templateUrl: './historical-data-table.component.html',
    styleUrls: ['./historical-data-table.component.scss']
})
export class HistoricalDataTableComponent implements OnInit {
    @Input() temporalEntityKind: TemporalEntityKind;
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

    name: string;
    moniker: string;
    dataSource: MatTableDataSource<Ohlcv | Quote | Trade | Scalar> = new MatTableDataSource<Ohlcv | Quote | Trade | Scalar>();

    ngOnInit() {
        this.dataSource.paginator = this.paginator;
    }

    displayedColumns(temporalEntityKind: TemporalEntityKind): string[] {
        switch (temporalEntityKind) {
            case TemporalEntityKind.Ohlcv:
                return ['time', 'open', 'high', 'low', 'close', 'volume'];
            case TemporalEntityKind.Quote:
                return ['time', 'bidPrice', 'bidSize', 'askPrice', 'askSize'];
            case TemporalEntityKind.Trade:
                return ['time', 'price', 'volume'];
            case TemporalEntityKind.Scalar:
                return ['time', 'value'];
        }
        return [];
    }

    @Input()
    set historicalData(historicalData: HistoricalData) {
        this.dataSource.data = (historicalData && historicalData.data) ? historicalData.data : [];
        this.name = (historicalData && historicalData.name) ? historicalData.name : '';
        this.moniker = (historicalData && historicalData.moniker) ? historicalData.moniker : '';
    }

    get isOhlcv(): boolean {
        return this.temporalEntityKind === TemporalEntityKind.Ohlcv;
    }

    get isQuote(): boolean {
        return this.temporalEntityKind === TemporalEntityKind.Quote;
    }

    get isTrade(): boolean {
        return this.temporalEntityKind === TemporalEntityKind.Trade;
    }

    get isScalar(): boolean {
        return this.temporalEntityKind === TemporalEntityKind.Scalar;
    }
}
