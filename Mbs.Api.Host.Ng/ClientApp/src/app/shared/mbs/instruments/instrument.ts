import { InstrumentType } from './instrument-type.enum';
import { ExchangeMic } from './../markets/exchange-mic.enum';
import { CurrencyCode } from './../currencies/currency-code.enum';

/** Contains information to describe an instrument. */
export class Instrument implements IInstrument {
    /** Gets or sets the optional symbol (ticker) of the security. */
    symbol?: string | undefined;
    /** Gets or sets a name of the instrument. */
    name?: string | undefined;
    /** Gets or sets an optional textual description for the instrument. */
    description?: string | undefined;
    /** Gets or sets the instrument type. */
    type!: InstrumentType;
    /** Gets or sets an exchange representations according to ISO 10383 Market Identifier Code (MIC). */
    mic!: ExchangeMic;
    /** Gets or sets an ISIN. */
    isin?: string | undefined;
    /** Gets or sets an additional information for stocks. */
    stock?: Stock | undefined;
    /** Gets or sets an additional information for Exchange-Traded Vehicles. */
    etv?: Etv | undefined;
    /** Gets or sets an additional information for Exchange-Traded Funds. */
    etf?: Etf | undefined;
    /** Gets or sets an additional information for Indicative Net Asset Values. */
    inav?: Inav | undefined;
    /** Gets or sets an additional information for funds. */
    fund?: Fund | undefined;
    /** Gets or sets an additional information for indices. */
    index?: Index | undefined;

    constructor(data?: IInstrument) {
        if (data) {
            for (var property in data) {
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

    init(data?: any) {
        if (data) {
            this.symbol = data["Symbol"];
            this.name = data["Name"];
            this.description = data["Description"];
            this.type = data["Type"];
            this.mic = data["Mic"];
            this.isin = data["Isin"];
            this.stock = data["Stock"] ? Stock.fromJS(data["Stock"]) : <any>undefined;
            this.etv = data["Etv"] ? Etv.fromJS(data["Etv"]) : <any>undefined;
            this.etf = data["Etf"] ? Etf.fromJS(data["Etf"]) : <any>undefined;
            this.inav = data["Inav"] ? Inav.fromJS(data["Inav"]) : <any>undefined;
            this.fund = data["Fund"] ? Fund.fromJS(data["Fund"]) : <any>undefined;
            this.index = data["Index"] ? Index.fromJS(data["Index"]) : <any>undefined;
        }
    }

    static fromJS(data: any): Instrument {
        data = typeof data === 'object' ? data : {};
        let result = new Instrument();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Symbol"] = this.symbol;
        data["Name"] = this.name;
        data["Description"] = this.description;
        data["Type"] = this.type;
        data["Mic"] = this.mic;
        data["Isin"] = this.isin;
        data["Stock"] = this.stock ? this.stock.toJSON() : <any>undefined;
        data["Etv"] = this.etv ? this.etv.toJSON() : <any>undefined;
        data["Etf"] = this.etf ? this.etf.toJSON() : <any>undefined;
        data["Inav"] = this.inav ? this.inav.toJSON() : <any>undefined;
        data["Fund"] = this.fund ? this.fund.toJSON() : <any>undefined;
        data["Index"] = this.index ? this.index.toJSON() : <any>undefined;
        return data; 
    }
}

/** Contains information to describe an instrument. */
export interface IInstrument {
    /** Gets or sets the optional symbol (ticker) of the security. */
    symbol?: string | undefined;
    /** Gets or sets a name of the instrument. */
    name?: string | undefined;
    /** Gets or sets an optional textual description for the instrument. */
    description?: string | undefined;
    /** Gets or sets the instrument type. */
    type: InstrumentType;
    /** Gets or sets an exchange representations according to ISO 10383 Market Identifier Code (MIC). */
    mic: ExchangeMic;
    /** Gets or sets an ISIN. */
    isin?: string | undefined;
    /** Gets or sets an additional information for stocks. */
    stock?: IStock | undefined;
    /** Gets or sets an additional information for Exchange-Traded Vehicles. */
    etv?: IEtv | undefined;
    /** Gets or sets an additional information for Exchange-Traded Funds. */
    etf?: IEtf | undefined;
    /** Gets or sets an additional information for Indicative Net Asset Values. */
    inav?: IInav | undefined;
    /** Gets or sets an additional information for funds. */
    fund?: IFund | undefined;
    /** Gets or sets an additional information for indices. */
    index?: IIndex | undefined;
}

/** An additional information for stocks. */
export class Stock implements IStock {
    /** Gets or sets a currency code. */
    currency!: CurrencyCode;
    /** Gets or sets a trading mode. */
    tradingMode?: string | undefined;
    /** Gets or sets an ISO 10962 Classification of Financial Instruments code. */
    cfi?: string | undefined;
    /** Gets or sets an Industry Classification Benchmark code. */
    icb?: string | undefined;
    /** Gets or sets a number of shares outstanding. */
    sharesOutstanding!: number;

    constructor(data?: IStock) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.currency = data["Currency"];
            this.tradingMode = data["TradingMode"];
            this.cfi = data["Cfi"];
            this.icb = data["Icb"];
            this.sharesOutstanding = data["SharesOutstanding"];
        }
    }

    static fromJS(data: any): Stock {
        data = typeof data === 'object' ? data : {};
        let result = new Stock();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Currency"] = this.currency;
        data["TradingMode"] = this.tradingMode;
        data["Cfi"] = this.cfi;
        data["Icb"] = this.icb;
        data["SharesOutstanding"] = this.sharesOutstanding;
        return data; 
    }
}

/** An additional information for stocks. */
export interface IStock {
    /** Gets or sets a currency code. */
    currency: CurrencyCode;
    /** Gets or sets a trading mode. */
    tradingMode?: string | undefined;
    /** Gets or sets an ISO 10962 Classification of Financial Instruments code. */
    cfi?: string | undefined;
    /** Gets or sets an Industry Classification Benchmark code. */
    icb?: string | undefined;
    /** Gets or sets a number of shares outstanding. */
    sharesOutstanding: number;
}

/** An additional information for Exchange-Traded Vehicles. */
export class Etv implements IEtv {
    /** Gets or sets a currency code. */
    currency!: CurrencyCode;
    /** Gets or sets a trading mode. */
    tradingMode?: string | undefined;
    /** Gets or sets all-in fees. */
    allInFees?: string | undefined;
    /** Gets or sets an expence ratio percentage. */
    expenseRatio?: string | undefined;
    /** Gets or sets a dividend frequency. */
    dividendFrequency?: string | undefined;
    /** Gets or sets an issuer. */
    issuer?: string | undefined;
    /** Gets or sets a number of shares outstanding. */
    sharesOutstanding!: number;

    constructor(data?: IEtv) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.currency = data["Currency"];
            this.tradingMode = data["TradingMode"];
            this.allInFees = data["AllInFees"];
            this.expenseRatio = data["ExpenseRatio"];
            this.dividendFrequency = data["DividendFrequency"];
            this.issuer = data["Issuer"];
            this.sharesOutstanding = data["SharesOutstanding"];
        }
    }

    static fromJS(data: any): Etv {
        data = typeof data === 'object' ? data : {};
        let result = new Etv();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Currency"] = this.currency;
        data["TradingMode"] = this.tradingMode;
        data["AllInFees"] = this.allInFees;
        data["ExpenseRatio"] = this.expenseRatio;
        data["DividendFrequency"] = this.dividendFrequency;
        data["Issuer"] = this.issuer;
        data["SharesOutstanding"] = this.sharesOutstanding;
        return data; 
    }
}

/** An additional information for Exchange-Traded Vehicles. */
export interface IEtv {
    /** Gets or sets a currency code. */
    currency: CurrencyCode;
    /** Gets or sets a trading mode. */
    tradingMode?: string | undefined;
    /** Gets or sets all-in fees. */
    allInFees?: string | undefined;
    /** Gets or sets an expence ratio percentage. */
    expenseRatio?: string | undefined;
    /** Gets or sets a dividend frequency. */
    dividendFrequency?: string | undefined;
    /** Gets or sets an issuer. */
    issuer?: string | undefined;
    /** Gets or sets a number of shares outstanding. */
    sharesOutstanding: number;
}

/** An additional information for Exchange-Traded Funds. */
export class Etf implements IEtf {
    /** Gets or sets a currency code. */
    currency!: CurrencyCode;
    /** Gets or sets a trading mode. */
    tradingMode?: string | undefined;
    /** Gets or sets an ISO 10962 Classification of Financial Instruments code. */
    cfi?: string | undefined;
    /** Gets or sets a dividend frequency. */
    dividendFrequency?: string | undefined;
    /** Gets or sets an exposition type. */
    expositionType?: string | undefined;
    /** Gets or sets a fraction. */
    fraction?: string | undefined;
    /** Gets or sets a total expence ratio percentage. */
    totalExpenseRatio?: string | undefined;
    /** Gets or sets an index family. */
    indexFamily?: string | undefined;
    /** Gets or sets a launch date. */
    launchDate?: string | undefined;
    /** Gets or sets an issuer. */
    issuer?: string | undefined;
    /** Gets or sets an Indicative Net Asset Value instrument reference. */
    inav?: InstrumentReference | undefined;
    /** Gets or sets an underlying instrument reference. */
    underlying?: InstrumentReference | undefined;

    constructor(data?: IEtf) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
            this.inav = data.inav && !(<any>data.inav).toJSON ? new InstrumentReference(data.inav) : <InstrumentReference>this.inav; 
            this.underlying = data.underlying && !(<any>data.underlying).toJSON ? new InstrumentReference(data.underlying) : <InstrumentReference>this.underlying; 
        }
    }

    init(data?: any) {
        if (data) {
            this.currency = data["Currency"];
            this.tradingMode = data["TradingMode"];
            this.cfi = data["Cfi"];
            this.dividendFrequency = data["DividendFrequency"];
            this.expositionType = data["ExpositionType"];
            this.fraction = data["Fraction"];
            this.totalExpenseRatio = data["TotalExpenseRatio"];
            this.indexFamily = data["IndexFamily"];
            this.launchDate = data["LaunchDate"];
            this.issuer = data["Issuer"];
            this.inav = data["Inav"] ? InstrumentReference.fromJS(data["Inav"]) : <any>undefined;
            this.underlying = data["Underlying"] ? InstrumentReference.fromJS(data["Underlying"]) : <any>undefined;
        }
    }

    static fromJS(data: any): Etf {
        data = typeof data === 'object' ? data : {};
        let result = new Etf();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Currency"] = this.currency;
        data["TradingMode"] = this.tradingMode;
        data["Cfi"] = this.cfi;
        data["DividendFrequency"] = this.dividendFrequency;
        data["ExpositionType"] = this.expositionType;
        data["Fraction"] = this.fraction;
        data["TotalExpenseRatio"] = this.totalExpenseRatio;
        data["IndexFamily"] = this.indexFamily;
        data["LaunchDate"] = this.launchDate;
        data["Issuer"] = this.issuer;
        data["Inav"] = this.inav ? this.inav.toJSON() : <any>undefined;
        data["Underlying"] = this.underlying ? this.underlying.toJSON() : <any>undefined;
        return data; 
    }
}

/** An additional information for Exchange-Traded Funds. */
export interface IEtf {
    /** Gets or sets a currency code. */
    currency: CurrencyCode;
    /** Gets or sets a trading mode. */
    tradingMode?: string | undefined;
    /** Gets or sets an ISO 10962 Classification of Financial Instruments code. */
    cfi?: string | undefined;
    /** Gets or sets a dividend frequency. */
    dividendFrequency?: string | undefined;
    /** Gets or sets an exposition type. */
    expositionType?: string | undefined;
    /** Gets or sets a fraction. */
    fraction?: string | undefined;
    /** Gets or sets a total expence ratio percentage. */
    totalExpenseRatio?: string | undefined;
    /** Gets or sets an index family. */
    indexFamily?: string | undefined;
    /** Gets or sets a launch date. */
    launchDate?: string | undefined;
    /** Gets or sets an issuer. */
    issuer?: string | undefined;
    /** Gets or sets an Indicative Net Asset Value instrument reference. */
    inav?: IInstrumentReference | undefined;
    /** Gets or sets an underlying instrument reference. */
    underlying?: IInstrumentReference | undefined;
}

/** Contains information to reference an instrument. */
export class InstrumentReference implements IInstrumentReference {
    /** Gets or sets an exchange representations according to ISO 10383 Market Identifier Code (MIC). */
    mic?: string | undefined;
    /** Gets or sets an ISIN. */
    isin?: string | undefined;
    /** Gets or sets the optional symbol (ticker) of the security. */
    symbol?: string | undefined;
    /** Gets or sets a name of the instrument. */
    name?: string | undefined;

    constructor(data?: IInstrumentReference) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.mic = data["Mic"];
            this.isin = data["Isin"];
            this.symbol = data["Symbol"];
            this.name = data["Name"];
        }
    }

    static fromJS(data: any): InstrumentReference {
        data = typeof data === 'object' ? data : {};
        let result = new InstrumentReference();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Mic"] = this.mic;
        data["Isin"] = this.isin;
        data["Symbol"] = this.symbol;
        data["Name"] = this.name;
        return data; 
    }
}

/** Contains information to reference an instrument. */
export interface IInstrumentReference {
    /** Gets or sets an exchange representations according to ISO 10383 Market Identifier Code (MIC). */
    mic?: string | undefined;
    /** Gets or sets an ISIN. */
    isin?: string | undefined;
    /** Gets or sets the optional symbol (ticker) of the security. */
    symbol?: string | undefined;
    /** Gets or sets a name of the instrument. */
    name?: string | undefined;
}

/** An additional information for Indicative Net Asset Values. */
export class Inav implements IInav {
    /** Gets or sets a currency code. */
    currency!: CurrencyCode;
    /** Gets or sets a target instrument reference. */
    target?: InstrumentReference | undefined;

    constructor(data?: IInav) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
            this.target = data.target && !(<any>data.target).toJSON ? new InstrumentReference(data.target) : <InstrumentReference>this.target; 
        }
    }

    init(data?: any) {
        if (data) {
            this.currency = data["Currency"];
            this.target = data["Target"] ? InstrumentReference.fromJS(data["Target"]) : <any>undefined;
        }
    }

    static fromJS(data: any): Inav {
        data = typeof data === 'object' ? data : {};
        let result = new Inav();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Currency"] = this.currency;
        data["Target"] = this.target ? this.target.toJSON() : <any>undefined;
        return data; 
    }
}

/** An additional information for Indicative Net Asset Values. */
export interface IInav {
    /** Gets or sets a currency code. */
    currency: CurrencyCode;
    /** Gets or sets a target instrument reference. */
    target?: IInstrumentReference | undefined;
}

/** An additional information for funds. */
export class Fund implements IFund {
    /** Gets or sets a currency code. */
    currency!: CurrencyCode;
    /** Gets or sets a trading mode. */
    tradingMode?: string | undefined;
    /** Gets or sets an ISO 10962 Classification of Financial Instruments code. */
    cfi?: string | undefined;
    /** Gets or sets an issuer. */
    issuer?: string | undefined;
    /** Gets or sets a number of shares outstanding. */
    sharesOutstanding!: number;

    constructor(data?: IFund) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.currency = data["Currency"];
            this.tradingMode = data["TradingMode"];
            this.cfi = data["Cfi"];
            this.issuer = data["Issuer"];
            this.sharesOutstanding = data["SharesOutstanding"];
        }
    }

    static fromJS(data: any): Fund {
        data = typeof data === 'object' ? data : {};
        let result = new Fund();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Currency"] = this.currency;
        data["TradingMode"] = this.tradingMode;
        data["Cfi"] = this.cfi;
        data["Issuer"] = this.issuer;
        data["SharesOutstanding"] = this.sharesOutstanding;
        return data; 
    }
}

/** An additional information for funds. */
export interface IFund {
    /** Gets or sets a currency code. */
    currency: CurrencyCode;
    /** Gets or sets a trading mode. */
    tradingMode?: string | undefined;
    /** Gets or sets an ISO 10962 Classification of Financial Instruments code. */
    cfi?: string | undefined;
    /** Gets or sets an issuer. */
    issuer?: string | undefined;
    /** Gets or sets a number of shares outstanding. */
    sharesOutstanding: number;
}

/** An additional information for indices. */
export class Index implements IIndex {
    /** Gets or sets a currency code. */
    currency!: CurrencyCode;
    /** Gets or sets a kind of an index. */
    kind?: string | undefined;
    /** Gets or sets a family of an index. */
    family?: string | undefined;
    /** Gets or sets a calculation frequency of an index. */
    calculationFrequency?: string | undefined;
    /** Gets or sets a weighting of an index. */
    weighting?: string | undefined;
    /** Gets or sets a capping factor of an index. */
    cappingFactor?: string | undefined;
    /** Gets or sets an Industry Classification Benchmark code. */
    icb?: string | undefined;
    /** Gets or sets a base date of an index. */
    baseDate?: string | undefined;
    /** Gets or sets a base level of an index. */
    baseLevel?: string | undefined;

    constructor(data?: IIndex) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.currency = data["Currency"];
            this.kind = data["Kind"];
            this.family = data["Family"];
            this.calculationFrequency = data["CalculationFrequency"];
            this.weighting = data["Weighting"];
            this.cappingFactor = data["CappingFactor"];
            this.icb = data["Icb"];
            this.baseDate = data["BaseDate"];
            this.baseLevel = data["BaseLevel"];
        }
    }

    static fromJS(data: any): Index {
        data = typeof data === 'object' ? data : {};
        let result = new Index();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Currency"] = this.currency;
        data["Kind"] = this.kind;
        data["Family"] = this.family;
        data["CalculationFrequency"] = this.calculationFrequency;
        data["Weighting"] = this.weighting;
        data["CappingFactor"] = this.cappingFactor;
        data["Icb"] = this.icb;
        data["BaseDate"] = this.baseDate;
        data["BaseLevel"] = this.baseLevel;
        return data; 
    }
}

/** An additional information for indices. */
export interface IIndex {
    /** Gets or sets a currency code. */
    currency: CurrencyCode;
    /** Gets or sets a kind of an index. */
    kind?: string | undefined;
    /** Gets or sets a family of an index. */
    family?: string | undefined;
    /** Gets or sets a calculation frequency of an index. */
    calculationFrequency?: string | undefined;
    /** Gets or sets a weighting of an index. */
    weighting?: string | undefined;
    /** Gets or sets a capping factor of an index. */
    cappingFactor?: string | undefined;
    /** Gets or sets an Industry Classification Benchmark code. */
    icb?: string | undefined;
    /** Gets or sets a base date of an index. */
    baseDate?: string | undefined;
    /** Gets or sets a base level of an index. */
    baseLevel?: string | undefined;
}
