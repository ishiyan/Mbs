import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { Instrument } from '../../../../shared/mbs/instruments/instrument';
import { MbsError } from '../../../../shared/mbs/errors/mbs-error';

const apiUrl = '/api/v1/instruments/lists/';
// const apiUrl = 'http://localhost:5000/api/v1/instruments/lists/';
const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json' // , 'Authorization': 'my-auth-token'
    })
};


@Injectable()
export class ListService {
    constructor(private httpClient: HttpClient) { }

    public getInstrumentList = (name: string): Observable<Instrument[]> => {
        return this.httpClient.get<Instrument[]>(apiUrl + name, httpOptions)
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
        return throwError(text);
    }

}
