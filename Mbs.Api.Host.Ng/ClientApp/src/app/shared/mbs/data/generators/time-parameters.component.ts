import { Component, Input } from '@angular/core';
import { TimeGranularity } from './../../time/time-granularity.enum';
import { BusinessDayCalendar } from './../../time/business-day-calendar.enum';
import { BusinessDayCalendarDescriptionComponent } from '../../time/business-day-calendar-description.component';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { TimeParameters } from './time-parameters';
import { Enums } from '../../utils/enums';

@Component({
  selector: 'mb-data-generators-time-parameters',
  templateUrl: './time-parameters.component.html',
  styleUrls: ['./time-parameters.component.scss']
})
export class TimeParametersComponent {
  @Input() timeParameters!: TimeParameters;

  timeGranularities = Object.keys(TimeGranularity);
  businessDayCalendars = Object.keys(BusinessDayCalendar);

  compare = Enums.compare;

  constructor(public dialog: MatDialog) {
  }

  openBusinessDayCalendarDescription() {
    this.dialog.open(BusinessDayCalendarDescriptionComponent, undefined);
  }
}
