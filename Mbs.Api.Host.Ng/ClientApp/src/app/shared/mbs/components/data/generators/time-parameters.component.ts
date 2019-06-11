import { Component, ElementRef, Input, NgModule } from '@angular/core';
import { TimeGranularity } from '../../../time/time-granularity.enum';
import { BusinessDayCalendar } from '../../../time/business-day-calendar.enum';
import { BusinessDayCalendarExplanationComponent } from './business-day-calendar-explanation.component';
import { MatDialog } from '@angular/material';
import { TimeParameters } from '../../../data/generators/time-parameters';
import { Enums } from '../../enums';

@Component({
    selector: 'app-mbs-data-generators-time-parameters',
    templateUrl: './time-parameters.component.html',
    styleUrls: ['./time-parameters.component.scss']
})
export class TimeParametersComponent {
    @Input() timeParameters: TimeParameters;

    timeGranularities = Object.keys(TimeGranularity);
    businessDayCalendars = Object.keys(BusinessDayCalendar);

    compare = Enums.compare;

    constructor(public dialog: MatDialog) {
    }

    openBusinessDayCalendarExplanation() {
        this.dialog.open(BusinessDayCalendarExplanationComponent, null);
    }
}
