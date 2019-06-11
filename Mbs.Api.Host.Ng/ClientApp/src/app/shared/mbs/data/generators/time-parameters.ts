import { TimeGranularity } from '../../time/time-granularity.enum';
import { BusinessDayCalendar } from '../../time/business-day-calendar.enum';

/** The time-related input parameters for temporal data generators. */
export class TimeParameters {
    private static readonly defaultSessionStartTime: string = '09:00:00';
    private static readonly defaultSessionEndTime: string = '18:00:00';
    private static readonly dafaultStartDate: Date = new Date(2000, 0, 1);

    /** The start time of the trading session. */
    sessionStartTime: string = TimeParameters.defaultSessionStartTime;
    /** The end time of the trading session. */
    sessionEndTime: string = TimeParameters.defaultSessionEndTime;
    /** The date of the first data sample. */
    startDate: Date = TimeParameters.dafaultStartDate;
    /** The time granularity of data samples. */
    timeGranularity: TimeGranularity = TimeGranularity.Day1;
    /** An exchange holiday schedule or a general country holiday schedule. */
    businessDayCalendar: BusinessDayCalendar = BusinessDayCalendar.WeekendsOnly;

    constructor(data?: TimeParameters) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): TimeParameters {
        data = typeof data === 'object' ? data : {};
        const result = new TimeParameters();
        result.init(data);
        return result;
    }

    private static formatDate(d: Date): string {
        return d.getFullYear() + '-' +
            (d.getMonth() < 9 ? ('0' + (d.getMonth() + 1)) : (d.getMonth() + 1)) + '-' +
            (d.getDate() < 10 ? ('0' + d.getDate()) : d.getDate());
    }

    init(data?: any) {
        if (data) {
            this.sessionStartTime = data['sessionStartTime'] !== undefined ? data['sessionStartTime'] : '09:00:00';
            this.sessionEndTime = data['sessionEndTime'] !== undefined ? data['sessionEndTime'] : '18:00:00';
            this.startDate = data['startDate'] ? new Date(data['startDate'].toString()) : <any>undefined;
            this.timeGranularity = data['timeGranularity'] !== undefined ? data['timeGranularity'] : TimeGranularity.Day1;
            this.businessDayCalendar = data['businessDayCalendar'] !== undefined
                ? data['businessDayCalendar'] : BusinessDayCalendar.WeekendsOnly;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['sessionStartTime'] = this.sessionStartTime;
        data['sessionEndTime'] = this.sessionEndTime;
        data['startDate'] = this.startDate ? TimeParameters.formatDate(this.startDate) : <any>undefined;
        data['timeGranularity'] = this.timeGranularity;
        data['businessDayCalendar'] = this.businessDayCalendar;
        return data;
    }
}
