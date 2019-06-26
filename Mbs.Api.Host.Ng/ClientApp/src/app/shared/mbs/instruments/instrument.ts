import { InstrumentType } from './types/instrument-type.enum';
import { ExchangeMic } from './../markets/exchange-mic.enum';
import { Stock } from './types/stock';
import { Etf } from './types/etf';
import { Etv } from './types/etv';
import { Inav } from './types/inav';
import { Fund } from './types/fund';
import { Index } from './types';

/** Contains information to describe an instrument. */
export class Instrument {
    /** An optional symbol (ticker) of the security. */
    symbol?: string | undefined;

    /** A name of the instrument. */
    name?: string | undefined;

    /** An optional textual description for the instrument. */
    description?: string | undefined;

    /** The instrument type. */
    type!: InstrumentType;

    /** An exchange representations according to ISO 10383 Market Identifier Code (MIC). */
    mic!: ExchangeMic;

    /** An ISIN. */
    isin?: string | undefined;

    /** An additional information for stocks. */
    stock?: Stock | undefined;

    /** An additional information for Exchange-Traded Vehicles. */
    etv?: Etv | undefined;

    /** An additional information for Exchange-Traded Funds. */
    etf?: Etf | undefined;

    /** An additional information for Indicative Net Asset Values. */
    inav?: Inav | undefined;

    /** An additional information for funds. */
    fund?: Fund | undefined;

    /** An additional information for indices. */
    index?: Index | undefined;

    constructor(data?: Instrument) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
            this.stock = data.stock && !(<any>data.stock).toJSON ? new Stock(data.stock) : <Stock>this.stock;
            this.etv = data.etv && !(<any>data.etv).toJSON ? new Etv(data.etv) : <Etv>this.etv;
            this.etf = data.etf && !(<any>data.etf).toJSON ? new Etf(data.etf) : <Etf>this.etf;
            this.inav = data.inav && !(<any>data.inav).toJSON ? new Inav(data.inav) : <Inav>this.inav;
            this.fund = data.fund && !(<any>data.fund).toJSON ? new Fund(data.fund) : <Fund>this.fund;
            this.index = data.index && !(<any>data.index).toJSON ? new Index(data.index) : <Index>this.index;
        }
        if (!data) {
            this.type = InstrumentType.Undefined;
            this.mic = ExchangeMic.Xxxx;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['Symbol'] = this.symbol;
        data['Name'] = this.name;
        data['Description'] = this.description;
        data['Type'] = this.type;
        data['Mic'] = this.mic;
        data['Isin'] = this.isin;
        data['Stock'] = this.stock ? this.stock.toJSON() : <any>undefined;
        data['Etv'] = this.etv ? this.etv.toJSON() : <any>undefined;
        data['Etf'] = this.etf ? this.etf.toJSON() : <any>undefined;
        data['Inav'] = this.inav ? this.inav.toJSON() : <any>undefined;
        data['Fund'] = this.fund ? this.fund.toJSON() : <any>undefined;
        data['Index'] = this.index ? this.index.toJSON() : <any>undefined;
        return data;
    }
}
