export interface TemporalEntity {
    time: Date;
    open?: number | undefined;
    high?: number | undefined;
    low?: number | undefined;
    close?: number | undefined;
    bidPrice?: number | undefined;
    bidSize?: number | undefined;
    askPrice?: number | undefined;
    askSize?: number | undefined;
    price?: number | undefined;
    volume?: number | undefined;
    value?: number | undefined;
}

export interface HistoricalData {
    isDataAdjusted?: boolean | undefined;
    name?: string | undefined;
    moniker?: string | undefined;
    data: TemporalEntity[];
}
