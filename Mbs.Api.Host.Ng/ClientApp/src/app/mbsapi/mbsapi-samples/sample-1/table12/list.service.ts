import { Component, OnInit, ElementRef, ViewChild, Injectable, Injector } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { InstrumentType } from '../../../../shared/mbs/instruments/instrument-type.enum';
import { ExchangeMic } from '../../../../shared/mbs/markets/exchange-mic.enum';
import { CurrencyCode } from '../../../../shared/mbs/currencies/currency-code.enum';
import { IInstrument } from '../../../../shared/mbs/instruments/instrument';
import { euronextListShort } from '../../../../shared/mbs/euronext-list-short';
import { MbsError } from '../../../../shared/mbs/errors/mbs-error';
import { SnackBarService } from '../../../../shared/snack-bar/snack-bar.service';

const apiUrl = "/api/v1/instruments/lists/";
//const apiUrl = "http://localhost:5000/api/v1/instruments/lists/";
const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'//,
        //'Authorization': 'my-auth-token'
    })
};


@Injectable()
export class ListService {
    // private snackBarService: SnackBarService;
    constructor(private httpClient: HttpClient/*, private injector: Injector, private snackBarService: SnackBarService*/) { }

    public getInstrumentList = (name: string): Observable<IInstrument[]> => {
        return this.httpClient.get<IInstrument[]>(apiUrl + name, httpOptions)
            .pipe(
                retry(3), // retry a failed request up to 3 times
                catchError(this.handleError) // then handle the error
            );
    }

    private handleError(error: any) {
        let text: string;
        if (error.error.message) {
            const internalError: MbsError = error.error as MbsError;
            text = MbsError.toMessage(internalError);
        } else {
            const errorResponse: HttpErrorResponse = error as HttpErrorResponse;
            text = errorResponse.message;
        }
        // console.error(text);
        /*if (!this.snackBarService) {
            this.snackBarService = this.injector.get(SnackBarService);
        }*/
        // this.snackBarService.add(text, 'Ok', {duration: 3000, verticalPosition: 'top'});
        return throwError(text);
    }

}
