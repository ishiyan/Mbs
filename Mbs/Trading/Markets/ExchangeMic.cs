// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
namespace Mbs.Trading.Markets
{
    /// <summary>
    /// Exchange representations according to ISO 10383 Market Identifier Code (MIC).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Generated automatically from the list of ISO 10383 Exchange/Market Identifier Codes, version SEP2017.1 (2017-09-29).
    /// </para>
    /// <para>
    /// See http://www.iso15022.org/MIC/homepageMIC.htm.
    /// </para>
    /// <para>
    /// Only the following countries are included:</para><para>Austria, Belgium, Canada, Denmark, Finland, France, Germany, Iceland, Italy, Luxembourg, Norway, Portugal, Spain, Sweden, Switzerland, Netherlands, UnitedKingdom, UnitedStates, NoCountry.
    /// </para>
    /// </remarks>
    public enum ExchangeMic
    {
        /// <summary>ERSTE GROUP BANK AG<para>http://www.erstegroup.com</para></summary>
        Egsi,

        /// <summary>WIENER BOERSE AG<para>http://www.wienerboerse.at</para></summary>
        Xwbo,

        /// <summary>WIENER BOERSE AG, AUSTRIAN ENERGY EXCHANGE<para>http://www.exaa.at</para></summary>
        Exaa,

        /// <summary>WIENER BOERSE AG AMTLICHER HANDEL (OFFICIAL MARKET)<para>http://www.wienerboerse.at</para></summary>
        Wbah,

        /// <summary>WIENER BOERSE AG DRITTER MARKT (THIRD MARKET)<para>http://www.wienerboerse.at</para></summary>
        Wbdm,

        /// <summary>WIENER BOERSE AG GEREGELTER FREIVERKEHR (SECOND REGULATED MARKET)<para>http://www.wienerboerse.at</para></summary>
        Wbgf,

        /// <summary>WIENER BOERSE AG, WERTPAPIERBOERSE (SECURITIES EXCHANGE)<para>http://www.wienerborse.at</para></summary>
        Xvie,

        /// <summary>MTS ASSOCIATED MARKETS<para>http://www.mtsbelgium.com</para></summary>
        Beam,

        /// <summary>MTS BELGIUM<para>http://www.mtsbelgium.com</para></summary>
        Bmts,

        /// <summary>MTS DENMARK<para>http://www.mtsdenmark.com</para></summary>
        Mtsd,

        /// <summary>MTS FINLAND<para>http://www.mtsfinland.com</para></summary>
        Mtsf,

        /// <summary>BELGIAN POWER EXCHANGE<para>http://www.belpex.be</para></summary>
        Blpx,

        /// <summary>EURONEXT - EURONEXT BRUSSELS<para>http://www.euronext.com</para></summary>
        Xbru,

        /// <summary>EURONEXT GROWTH BRUSSELS<para>http://www.euronext.com</para></summary>
        Alxb,

        /// <summary>EURONEXT - EASY NEXT<para>http://www.euronext.com</para></summary>
        Enxb,

        /// <summary>EURONEXT ACCESS BRUSSELS<para>http://www.euronext.com</para></summary>
        Mlxb,

        /// <summary>EURONEXT - TRADING FACILITY BRUSSELS<para>http://www.euronext.com</para></summary>
        Tnlb,

        /// <summary>EURONEXT - VENTES PUBLIQUES BRUSSELS<para>http://www.euronext.com</para></summary>
        Vpxb,

        /// <summary>EURONEXT - EURONEXT BRUSSELS - DERIVATIVES<para>http://www.euronext.com</para></summary>
        Xbrd,

        /// <summary>CANDEALCA INC<para>http://www.candeal.com</para></summary>
        Cand,

        /// <summary>CANNEX FINANCIAL EXCHANGE LTS<para>http://www.cannex.com</para></summary>
        Canx,

        /// <summary>CHI-X CANADA ATS<para>http://www.chi-xcanada.com</para></summary>
        Chic,

        /// <summary>CX2<para>http://www.chi-x.com/ca</para></summary>
        Xcx2,

        /// <summary>BMO CAPITAL MARKETS - CAD OTC TRADES<para>http://www.bmocm.com</para></summary>
        Cotc,

        /// <summary>ICE FUTURES CANADA<para>http://www.theice.com</para></summary>
        Ifca,

        /// <summary>INVESCO CANADA PTF TRADES<para>http://www.invesco.ca</para></summary>
        Ivzx,

        /// <summary>LIQUIDNET CANADA ATS<para>http://www.liquidnet.com</para></summary>
        Lica,

        /// <summary>MATCH NOW<para>http://www.triactcanada.com</para></summary>
        Matn,

        /// <summary>AEQUITAS NEO EXCHANGE<para>http://www.aequitasinnovations.com</para></summary>
        Neoe,

        /// <summary>NATURAL GAS EXCHANGE<para>http://www.ngx.com</para></summary>
        Ngxc,

        /// <summary>OMEGA ATS<para>http://www.omegaats.com</para></summary>
        Omga,

        /// <summary>LYNX ATS<para>http://www.omegaats.com</para></summary>
        Lynx,

        /// <summary>TMX SELECT<para>http://www.tmx.com</para></summary>
        Tmxs,

        /// <summary>ALPHA EXCHANGE<para>http://www.alphatradingsystems.ca</para></summary>
        Xats,

        /// <summary>PERIMETER FINANCIAL CORP - BLOCKBOOK ATS<para>http://www.pfin.ca</para></summary>
        Xbbk,

        /// <summary>CANADIAN NATIONAL STOCK EXCHANGE<para>http://www.cnsx.ca</para></summary>
        Xcnq,

        /// <summary>PURE TRADING<para>http://www.puretrading.ca</para></summary>
        Pure,

        /// <summary>NASDAQ CXD<para>http://business.nasdaq.com/trade/canadian-equities/overview</para></summary>
        Xcxd,

        /// <summary>INSTINET CANADA CROSS<para>http://www.instinet.com</para></summary>
        Xicx,

        /// <summary>MONTREAL CLIMATE EXCHANGE<para>http://www.m-x.ca</para></summary>
        Xmoc,

        /// <summary>THE MONTREAL EXCHANGE / BOURSE DE MONTREAL<para>http://www.m-x.ca</para></summary>
        Xmod,

        /// <summary>TORONTO STOCK EXCHANGE<para>http://www.tse.com</para></summary>
        Xtse,

        /// <summary>TSX VENTURE EXCHANGE<para>http://www.tsx.com</para></summary>
        Xtsx,

        /// <summary>TSX VENTURE EXCHANGE - NEX<para>http://www.tsx.com/en/nex</para></summary>
        Xtnx,

        /// <summary>DANSKE BANK A/S - SYSTEMATIC INTERNALISER<para>http://www.danskebank.dk</para></summary>
        Dasi,

        /// <summary>DANSK OTC<para>http://www.danskotc.dk</para></summary>
        Dktc,

        /// <summary>GXG MARKETS A/S<para>http://www.gxgmarkets.com</para></summary>
        Gxgr,

        /// <summary>GXG MTF FIRST QUOTE<para>http://www.gxgmarkets.com</para></summary>
        Gxgf,

        /// <summary>GXG MTF<para>http://www.gxgmarkets.com</para></summary>
        Gxgm,

        /// <summary>JYSKE BANK - SYSTEMATIC INTERNALISER<para>http://www.jyskebank.dk</para></summary>
        Jbsi,

        /// <summary>GASPOINT NORDIC A/S<para>http://www.gaspointnordic.com</para></summary>
        Npga,

        /// <summary>SPAR NORD BANK - SYSTEMATIC INTERNALISER<para>http://www.sparnord.dk</para></summary>
        Snsi,

        /// <summary>NASDAQ COPENHAGEN A/S<para>http://www.nasdaqomxnordic.com</para></summary>
        Xcse,

        /// <summary>NASDAQ COPENHAGEN A/S - NORDIC@MID<para>http://www.nasdaqomxnordic.com</para></summary>
        Dcse,

        /// <summary>FIRST NORTH DENMARK<para>http://www.nasdaqomxnordic.com</para></summary>
        Fndk,

        /// <summary>FIRST NORTH DENMARK - NORDIC@MID<para>http://www.nasdaqomxnordic.com</para></summary>
        Dndk,

        /// <summary>NASDAQ COPENHAGEN A/S � AUCTION ON DEMAND<para>http://www.nasdaqomxnordic.com</para></summary>
        Mcse,

        /// <summary>FIRST NORTH DENMARK � AUCTION ON DEMAND<para>http://www.nasdaqomxnordic.com</para></summary>
        Mndk,

        /// <summary>KAASUPORSSI - FINNISH GAS EXCHANGE<para>http://www.kaasuporssi.com</para></summary>
        Fgex,

        /// <summary>NASDAQ HELSINKI LTD<para>http://www.nasdaqomxnordic.com</para></summary>
        Xhel,

        /// <summary>FIRST NORTH FINLAND<para>http://www.nasdaqomxnordic.com</para></summary>
        Fnfi,

        /// <summary>NASDAQ HELSINKI LTD - NORDIC@MID<para>http://www.nasdaqomxnordic.com</para></summary>
        Dhel,

        /// <summary>FIRST NORTH FINLAND - NORDIC@MID<para>http://www.nasdaqomxnordic.com</para></summary>
        Dnfi,

        /// <summary>NASDAQ HELSINKI LTD �  AUCTION ON DEMAND<para>http://www.nasdaqomxnordic.com</para></summary>
        Mhel,

        /// <summary>FIRST NORTH FINLAND � AUCTION ON DEMAND<para>http://www.nasdaqomxnordic.com</para></summary>
        Mnfi,

        /// <summary>LA COTE ALPHA<para>http://www.cote-alpha.fr</para></summary>
        Coal,

        /// <summary>EPEX SPOT SE<para>http://www.epexspot.com</para></summary>
        Epex,

        /// <summary>EXANE BNP PARIBAS - SYSTEMATIC INTERNALISER<para>http://www.exane.com</para></summary>
        Exse,

        /// <summary>MTS FRANCE SAS<para>http://www.mtsfrance.com</para></summary>
        Fmts,

        /// <summary>GALAXY<para>http://www.tradingscreen.com</para></summary>
        Gmtf,

        /// <summary>LCHCLEARNET<para>http://www.lchclearnet.com</para></summary>
        Lchc,

        /// <summary>NATIXIS - SYSTEMATIC INTERNALISER<para>http://www.natixis.com</para></summary>
        Natx,

        /// <summary>ALTERNATIVA FRANCE<para>http://www.alternativa.fr</para></summary>
        Xafr,

        /// <summary>BLUENEXT<para>http://www.bluenext.eu</para></summary>
        Xbln,

        /// <summary>EURONEXT - EURONEXT PARIS<para>http://www.euronext.com</para></summary>
        Xpar,

        /// <summary>EURONEXT GROWTH PARIS<para>http://www.euronext.com</para></summary>
        Alxp,

        /// <summary>BONDMATCH<para>http://www.euronext.com</para></summary>
        Mtch,

        /// <summary>EURONEXT PARIS MATIF<para>http://www.euronext.com</para></summary>
        Xmat,

        /// <summary>EURONEXT ACCESS PARIS<para>http://www.euronext.com</para></summary>
        Xmli,

        /// <summary>EURONEXT PARIS MONEP<para>http://www.euronext.com</para></summary>
        Xmon,

        /// <summary>EURONEXT STRUCTURED PRODUCTS MTF<para>http://www.euronext.com</para></summary>
        Xspm,

        /// <summary>POWERNEXT<para>http://www.powernext.fr</para></summary>
        Xpow,

        /// <summary>POWERNEXT - GAS SPOT AND FUTURES<para>http://www.powernext.com</para></summary>
        Xpsf,

        /// <summary>POWERNEXT - OTF<para>http://www.powernext.com</para></summary>
        Xpot,

        /// <summary>360T<para>http://www.360t.com</para></summary>
        X360T,

        /// <summary>BAADER BANK - SYSTEMATIC INTERNALISER<para>http://www.baaderbank.de</para></summary>
        Baad,

        /// <summary>CATS<para>http://www.bs-cats.com</para></summary>
        Cats,

        /// <summary>DEUTSCHE BOERSE AG APA SERVICE<para>http://www.deutsche-boerse.com</para></summary>
        Dapa,

        /// <summary>DEUTSCHE BANK OFF EXCHANGE TRADING<para>http://www.deutsche-bank.de</para></summary>
        Dbox,

        /// <summary>AUTOBAHN FX<para>http://www.autobahnfx.db.com</para></summary>
        Auto,

        /// <summary>EUREX CLEARING AG<para>http://www.eurexclearing.com</para></summary>
        Ecag,

        /// <summary>FINANCIAL INFORMATION CONTRIBUTORS EXCHANGE<para>http://www.ficonex.com</para></summary>
        Ficx,

        /// <summary>HSBC TRINKAUS AND BURKHARDT AG - SYSTEMATIC INTERNALISER<para>http://www.hsbc.de</para></summary>
        Hsbt,

        /// <summary>TRADEGATE EXCHANGE<para>http://www.tradegate.de</para></summary>
        Tgat,

        /// <summary>TRADEGATE EXCHANGE - FREIVERKEHR<para>http://www.tradegate.de</para></summary>
        Xgat,

        /// <summary>TRADEGATE EXCHANGE - REGULIERTER MARKT<para>http://www.tradegate.de</para></summary>
        Xgrm,

        /// <summary>TRADEPLUS<para>http://www.transactionsolutions.de</para></summary>
        Vwdx,

        /// <summary>BOERSE BERLIN<para>http://www.boerse-berlin.de</para></summary>
        Xber,

        /// <summary>BOERSE BERLIN - REGULIERTER MARKT<para>http://www.berlin-boerse.de</para></summary>
        Bera,

        /// <summary>BOERSE BERLIN - FREIVERKEHR<para>http://www.berlin-boerse.de</para></summary>
        Berb,

        /// <summary>BOERSE BERLIN - BERLIN SECOND REGULATED MARKET<para>http://www.berlin-boerse.de</para></summary>
        Berc,

        /// <summary>BOERSE BERLIN EQUIDUCT TRADING - REGULIERTER MARKT<para>http://www.equiduct-trading.de</para></summary>
        Eqta,

        /// <summary>BOERSE BERLIN EQUIDUCT TRADING - BERLIN SECOND REGULATED MARKET<para>http://www.equiduct-trading.de</para></summary>
        Eqtb,

        /// <summary>BOERSE BERLIN EQUIDUCT TRADING - FREIVERKEHR<para>http://www.equiduct-trading.de</para></summary>
        Eqtc,

        /// <summary>BOERSE BERLIN EQUIDUCT TRADING - OTC<para>http://www.equiduct-trading.de</para></summary>
        Eqtd,

        /// <summary>BOERSE BERLIN EQUIDUCT TRADING<para>http://www.equiduct-trading.de</para></summary>
        Xeqt,

        /// <summary>ZOBEX<para>http://www.boerse-berlin.de</para></summary>
        Zobx,

        /// <summary>BOERSE DUESSELDORF<para>http://www.boerse-duesseldorf.de</para></summary>
        Xdus,

        /// <summary>BOERSE DUESSELDORF - REGULIERTER MARKT</summary>
        Dusa,

        /// <summary>BOERSE DUESSELDORF - FREIVERKEHR</summary>
        Dusb,

        /// <summary>BOERSE DUESSELDORF - QUOTRIX - REGULIERTER MARKT</summary>
        Dusc,

        /// <summary>BOERSE DUESSELDORF - QUOTRIX MTF</summary>
        Dusd,

        /// <summary>BOERSE DUESSELDORF - QUOTRIX<para>http://www.boerse-duesseldorf.de</para></summary>
        Xqtx,

        /// <summary>ECB EXCHANGE RATES<para>http://www.ecb.europa.eu</para></summary>
        Xecb,

        /// <summary>EUROPEAN COMMODITY CLEARING AG<para>http://www.ecc.de</para></summary>
        Xecc,

        /// <summary>EUROPEAN ENERGY EXCHANGE<para>http://www.eex.com</para></summary>
        Xeee,

        /// <summary>EUROPEAN ENERGY EXCHANGE - NON-MTF MARKET<para>http://www.eex.com</para></summary>
        Xeeo,

        /// <summary>EUROPEAN ENERGY EXCHANGE - REGULATED MARKET<para>http://www.eex.com</para></summary>
        Xeer,

        /// <summary>XETRA<para>http://www.deutsche-boerse.com</para></summary>
        Xetr,

        /// <summary>EUREX BONDS<para>http://www.eurex-bonds.com</para></summary>
        Xeub,

        /// <summary>XETRA - REGULIERTER MARKT<para>http://www.deutsche-boerse.com</para></summary>
        Xeta,

        /// <summary>XETRA - FREIVERKEHR<para>http://www.deutsche-boerse.com</para></summary>
        Xetb,

        /// <summary>EUREX REPO GMBH<para>http://www.eurexrepo.com</para></summary>
        Xeup,

        /// <summary>EUREX REPO SECLEND MARKET<para>http://www.eurexrepo.com</para></summary>
        Xeum,

        /// <summary>EUREX REPO - FUNDING AND FINANCING PRODUCTS<para>http://www.eurexrepo.com</para></summary>
        Xere,

        /// <summary>EUREX REPO - TRIPARTY<para>http://www.eurexrepo.com</para></summary>
        Xert,

        /// <summary>EUREX DEUTSCHLAND<para>http://www.eurexchange.com</para></summary>
        Xeur,

        /// <summary>DEUTSCHE BOERSE AG<para>http://www.deutsche-boerse.com</para></summary>
        Xfra,

        /// <summary>BOERSE FRANKFURT - REGULIERTER MARKT<para>http://www.deutsche-boerse.com</para></summary>
        Fraa,

        /// <summary>BOERSE FRANKFURT - FREIVERKEHR<para>http://www.deutsche-boerse.com</para></summary>
        Frab,

        /// <summary>DEUTSCHE BOERSE AG - CUSTOMIZED INDICES<para>http://www.deutsche-boerse.com</para></summary>
        Xdbc,

        /// <summary>DEUTSCHE BOERSE AG - VOLATILITY INDICES<para>http://www.deutsche-boerse.com</para></summary>
        Xdbv,

        /// <summary>DEUTSCHE BOERSE AG - INDICES<para>http://www.deutsche-boerse.com</para></summary>
        Xdbx,

        /// <summary>HANSEATISCHE WERTPAPIERBOERSE HAMBURG<para>http://www.boersenag.de</para></summary>
        Xham,

        /// <summary>BOERSE HAMBURG - REGULIERTER MARKT</summary>
        Hama,

        /// <summary>BOERSE HAMBURG - FREIVERKEHR</summary>
        Hamb,

        /// <summary>BOERSE HAMBURG - LANG AND SCHWARZ EXCHANGE - REGULIERTER MARKT<para>http://www.boersenag.de</para></summary>
        Hamm,

        /// <summary>BOERSE HAMBURG - LANG AND SCHWARZ EXCHANGE - FREIVERKEHR<para>http://www.boersenag.de</para></summary>
        Hamn,

        /// <summary>BOERSE HAMBURG - LANG AND SCHWARZ EXCHANGE<para>http://www.boersenag.de</para></summary>
        Haml,

        /// <summary>NIEDERSAECHSISCHE BOERSE ZU HANNOVER<para>http://www.boersenag.de</para></summary>
        Xhan,

        /// <summary>BOERSE HANNOVER - REGULIERTER MARKT</summary>
        Hana,

        /// <summary>BOERSE HANNOVER - FREIVERKEHR</summary>
        Hanb,

        /// <summary>INVESTRO<para>http://www.investro.de</para></summary>
        Xinv,

        /// <summary>BOERSE MUENCHEN<para>http://www.boerse-muenchen.de</para></summary>
        Xmun,

        /// <summary>BOERSE MUENCHEN - REGULIERTER MARKT<para>http://www.boerse-muenchen.de</para></summary>
        Muna,

        /// <summary>BOERSE MUENCHEN - FREIVERKEHR<para>http://www.boerse-muenchen.de</para></summary>
        Munb,

        /// <summary>BOERSE MUENCHEN � GETTEX � FREIVERKEHR<para>http://www.gettex.de</para></summary>
        Mund,

        /// <summary>BOERSE MUENCHEN - GETTEX - REGULIERTER MARKT<para>http://www.gettex.de</para></summary>
        Munc,

        /// <summary>BOERSE FRANKFURT WARRANTS TECHNICAL<para>http://www.zertifikateboerse.de</para></summary>
        Xsco,

        /// <summary>BOERSE FRANKFURT WARRANTS TECHNICAL 1<para>http://www.zertifikateboerse.de</para></summary>
        Xsc1,

        /// <summary>BOERSE FRANKFURT WARRANTS TECHNICAL 2<para>http://www.zertifikateboerse.de</para></summary>
        Xsc2,

        /// <summary>BOERSE FRANKFURT WARRANTS TECHNICAL 3<para>http://www.zertifikateboerse.de</para></summary>
        Xsc3,

        /// <summary>BOERSE STUTTGART<para>http://www.boerse-stuttgart.de</para></summary>
        Xstu,

        /// <summary>EUWAX<para>http://www.euwax.de</para></summary>
        Euwx,

        /// <summary>BOERSE STUTTGART - REGULIERTER MARKT<para>http://www.boerse-stuttgart.de</para></summary>
        Stua,

        /// <summary>BOERSE STUTTGART - FREIVERKEHR<para>http://www.boerse-stuttgart.de</para></summary>
        Stub,

        /// <summary>BOERSE STUTTGART - TECHNICAL PLATFORM 2<para>http://www.boerse-stuttgart.de</para></summary>
        Xstf,

        /// <summary>BOERSE STUTTGART - REGULIERTER MARKT - TECHNICAL PLATFORM 2<para>http://www.boerse-stuttgart.de</para></summary>
        Stuc,

        /// <summary>BOERSE STUTTGART - FREIVERKEHR - TECHNICAL PLATFORM 2<para>http://www.boerse-stuttgart.de</para></summary>
        Stud,

        /// <summary>FRANKFURT CEF SC<para>http://www.deutsche-boerse.com</para></summary>
        Xxsc,

        /// <summary>NASDAQ ICELAND HF<para>http://www.nasdaqomxnordic.com</para></summary>
        Xice,

        /// <summary>NASDAQ ICELAND HF - NORDIC@MID<para>http://www.nasdaqomxnordic.com</para></summary>
        Dice,

        /// <summary>FIRST NORTH ICELAND<para>http://www.nasdaqomxnordic.com</para></summary>
        Fnis,

        /// <summary>FIRST NORTH ICELAND - NORDIC@MID<para>http://www.nasdaqomxnordic.com</para></summary>
        Dnis,

        /// <summary>NASDAQ ICELAND HF � AUCTION ON DEMAND<para>http://www.nasdaqomxnordic.com</para></summary>
        Mice,

        /// <summary>FIRST NORTH ICELAND � AUCTION ON DEMAND<para>http://www.nasdaqomxnordic.com</para></summary>
        Mnis,

        /// <summary>CASSA DI COMPENSAZIONE E GARANZIA SPA<para>http://www.ccg.it</para></summary>
        Cgit,

        /// <summary>CASSA DI COMPENSAZIONE E GARANZIA SPA - CCP AGRICULTURAL COMMODITY DERIVATIVES<para>http://www.ccg.it</para></summary>
        Cggd,

        /// <summary>CASSA DI COMPENSAZIONE E GARANZIA SPA - COLLATERALIZED MONEY MARKET GUARANTEE SERVICE<para>http://www.ccg.it</para></summary>
        Cgcm,

        /// <summary>CASSA DI COMPENSAZIONE E GARANZIA SPA - EQUITY CCP SERVICE<para>http://www.ccg.it</para></summary>
        Cgqt,

        /// <summary>CASSA DI COMPENSAZIONE E GARANZIA SPA - BONDS CCP SERVICE<para>http://www.ccg.it</para></summary>
        Cgdb,

        /// <summary>CASSA DI COMPENSAZIONE E GARANZIA SPA - EURO BONDS CCP SERVICE<para>http://www.ccg.it</para></summary>
        Cgeb,

        /// <summary>CASSA DI COMPENSAZIONE E GARANZIA SPA - TRIPARTY REPO CCP SERVICE<para>http://www.ccg.it</para></summary>
        Cgtr,

        /// <summary>CASSA DI COMPENSAZIONE E GARANZIA SPA - CCP EQUITY DERIVATIVES<para>http://www.ccg.it</para></summary>
        Cgqd,

        /// <summary>CASSA DI COMPENSAZIONE E GARANZIA SPA - CCP ENERGY DERIVATIVES<para>http://www.ccg.it</para></summary>
        Cgnd,

        /// <summary>E-MID<para>http://www.e-mid.it</para></summary>
        Emid,

        /// <summary>E-MID - E-MIDER MARKET<para>http://www.e-mid.it</para></summary>
        Emdr,

        /// <summary>E-MID REPO<para>http://www.e-mid.it</para></summary>
        Emir,

        /// <summary>E-MID - BANCA DITALIA SHARES TRADING MARKET<para>http://www.e-mid.it</para></summary>
        Emib,

        /// <summary>EUROTLX<para>http://www.eurotlx.com</para></summary>
        Etlx,

        /// <summary>FINECO BANK - SYSTEMATIC INTERNALISER<para>http://www.finecobank.com</para></summary>
        Fbsi,

        /// <summary>HI-MTF<para>http://www.himtf.com</para></summary>
        Hmtf,

        /// <summary>HI-MTF ORDER DRIVEN<para>http://www.himtf.com</para></summary>
        Hmod,

        /// <summary>HI-MTF RFQ<para>http://www.himtf.com</para></summary>
        Hrfq,

        /// <summary>MTS SPA<para>http://www.mtsmarkets.com</para></summary>
        Mtso,

        /// <summary>BONDVISION ITALIA<para>http://www.mtsmarkets.com</para></summary>
        Bond,

        /// <summary>MTS ITALIA<para>http://www.mtsmarkets.com</para></summary>
        Mtsc,

        /// <summary>MTS CORPORATE MARKET<para>http://www.mtsmarkets.com</para></summary>
        Mtsm,

        /// <summary>BONDVISION ITALIA MTF<para>http://www.mtsmarkets.com</para></summary>
        Ssob,

        /// <summary>GESTORE MERCATO ELETTRICO - ITALIAN POWER EXCHANGE<para>http://www.mercatoelettrico.org</para></summary>
        Xgme,

        /// <summary>BORSA ITALIANA SPA<para>http://www.borsaitaliana.it</para></summary>
        Xmil,

        /// <summary>BORSA ITALIANA EQUITY MTF<para>http://www.borsaitaliana.it</para></summary>
        Mtah,

        /// <summary>ELECTRONIC ETF, ETC/ETN AND OPEN-END FUNDS MARKET<para>http://www.borsaitaliana.it</para></summary>
        Etfp,

        /// <summary>MARKET FOR INVESTMENT VEHICLES<para>http://www.borsaitaliana.it</para></summary>
        Mivx,

        /// <summary>ELECTRONIC BOND MARKET<para>http://www.borsaitaliana.it</para></summary>
        Motx,

        /// <summary>ELECTRONIC SHARE MARKET<para>http://www.borsaitaliana.it</para></summary>
        Mtaa,

        /// <summary>SECURITISED DERIVATIVES MARKET<para>http://www.borsaitaliana.it</para></summary>
        Sedx,

        /// <summary>AIM ITALIA - MERCATO ALTERNATIVO DEL CAPITALE<para>http://www.borsaitaliana.it</para></summary>
        Xaim,

        /// <summary>ITALIAN DERIVATIVES MARKET<para>http://www.borsaitaliana.it</para></summary>
        Xdmi,

        /// <summary>EXTRAMOT<para>http://www.borsaitaliana.it</para></summary>
        Xmot,

        /// <summary>FINESTI SA<para>http://www.cclux.lu</para></summary>
        Cclx,

        /// <summary>MITSUBISHI UFJ INVESTOR SERVICES AND BANKING - SYSTEMATIC INTERNALISER<para>http://www.lu.tr.mufg.jp</para></summary>
        Mibl,

        /// <summary>RBC INVESTOR SERVICES BANK SA<para>http://www.rbcits.com</para></summary>
        Rbcb,

        /// <summary>RBC INVESTOR SERVICES BANK SA - SYSTEMATIC INTERNALISER<para>http://www.rbcits.com</para></summary>
        Rbsi,

        /// <summary>LUXEMBOURG STOCK EXCHANGE<para>http://www.bourse.lu</para></summary>
        Xlux,

        /// <summary>EURO MTF<para>http://www.bourse.lu</para></summary>
        Emtf,

        /// <summary>VESTIMA<para>http://www.clearstream.com</para></summary>
        Xves,

        /// <summary>FISH POOL ASA<para>http://www.fishpool.eu</para></summary>
        Fish,

        /// <summary>FISHEX<para>http://www.fishex.no</para></summary>
        Fshx,

        /// <summary>ICAP ENERGY AS<para>http://www.icapenergy.com/eu</para></summary>
        Icas,

        /// <summary>NOREXECO ASA<para>http://www.norexeco.com</para></summary>
        Nexo,

        /// <summary>NORD POOL SPOT AS<para>http://www.nordpoolspot.com</para></summary>
        Nops,

        /// <summary>NASDAQ OMX COMMODITIES<para>http://www.nasdaqomxcommodities.com</para></summary>
        Norx,

        /// <summary>NASDAQ COMMODITIES - EUR POWER/ENERGY<para>http://www.nasdaqomx.com/commodities/</para></summary>
        Eleu,

        /// <summary>NASDAQ COMMODITIES - SEK POWER/ENERGY<para>http://www.nasdaqomx.com/commodities/</para></summary>
        Else,

        /// <summary>NASDAQ COMMODITIES - NOK POWER/ENERGY<para>http://www.nasdaqomx.com/commodities/</para></summary>
        Elno,

        /// <summary>NASDAQ COMMODITIES - GBP POWER/ENERGY<para>http://www.nasdaqomx.com/commodities/</para></summary>
        Eluk,

        /// <summary>NASDAQ COMMODITIES - FREIGHT COMMODITY<para>http://www.nasdaqomx.com/commodities/</para></summary>
        Frei,

        /// <summary>NASDAQ COMMODITIES - BULK COMMODITY<para>http://www.nasdaqomx.com/commodities/</para></summary>
        Bulk,

        /// <summary>NASDAQ COMMODITIES - STEEL COMMODITY<para>http://www.nasdaqomx.com/commodities/</para></summary>
        Stee,

        /// <summary>NOS CLEARING ASA<para>http://www.nosclearing.com</para></summary>
        Nosc,

        /// <summary>NORWEGIAN OVER THE COUNTER MARKET<para>http://www.nfmf.no</para></summary>
        Notc,

        /// <summary>SIX X-CLEAR AG<para>http://www.six-securities-services.com</para></summary>
        Oslc,

        /// <summary>DNB BANK ASA - SYSTEMATIC INTERNALISER<para>http://www.dnb.no</para></summary>
        Xdnb,

        /// <summary>INTERNATIONAL MARTIME EXCHANGE<para>http://www.imarex.com</para></summary>
        Xima,

        /// <summary>OSLO BORS ASA<para>http://www.oslobors.no</para></summary>
        Xosl,

        /// <summary>NORDIC ALTERNATIVE BOND MARKET<para>http://www.oslobors.no</para></summary>
        Xoam,

        /// <summary>OSLO AXESS<para>http://www.oslobors.no</para></summary>
        Xoas,

        /// <summary>NORWEGIAN INTER BANK OFFERED RATE<para>http://www.oslobors.no</para></summary>
        Nibr,

        /// <summary>MERKUR MARKET - DARK POOL<para>http://www.oslobors.no</para></summary>
        Merd,

        /// <summary>MERKUR MARKET<para>http://www.oslobors.no</para></summary>
        Merk,

        /// <summary>OSLO CONNECT<para>http://www.oslobors.no</para></summary>
        Xosc,

        /// <summary>OSLO AXESS NORTH SEA - DARK POOL<para>http://www.oslobors.no</para></summary>
        Xoad,

        /// <summary>OSLO BORS NORTH SEA - DARK POOL<para>http://www.oslobors.no</para></summary>
        Xosd,

        /// <summary>THE IBERIAN ENERGY CLEARING HOUSE<para>http://www.omiclear.pt</para></summary>
        Omic,

        /// <summary>PEX-PRIVATE EXCHANGE<para>http://www.opex.pt</para></summary>
        Opex,

        /// <summary>EURONEXT - EURONEXT LISBON<para>http://www.euronext.com</para></summary>
        Xlis,

        /// <summary>EURONEXT GROWTH LISBON<para>http://www.euronext.com</para></summary>
        Alxl,

        /// <summary>EURONEXT ACCESS LISBON<para>http://www.euronext.com</para></summary>
        Enxl,

        /// <summary>EURONEXT - MERCADO DE FUTUROS E OPÇÕES<para>http://www.euronext.com</para></summary>
        Mfox,

        /// <summary>OPERADOR DE MERCADO IBERICO DE ENERGIA - PORTUGAL<para>http://www.omip.pt</para></summary>
        Omip,

        /// <summary>EURONEXT - MARKET WITHOUT QUOTATIONS LISBON<para>http://www.euronext.com</para></summary>
        Wqxl,

        /// <summary>BME - BOLSAS Y MERCADOS ESPANOLES<para>http://www.bolsasymercados.es</para></summary>
        Bmex,

        /// <summary>MERCADO ALTERNATIVO BURSATIL<para>http://www.bolsasymercados.es</para></summary>
        Mabx,

        /// <summary>SEND - SISTEMA ELECTRONICO DE NEGOCIACION DE DEUDA<para>http://www.aiaf.es</para></summary>
        Send,

        /// <summary>BOLSA DE BARCELONA<para>http://www.borsabcn.es</para></summary>
        Xbar,

        /// <summary>BOLSA DE VALORES DE BILBAO<para>http://www.bolsabilbao.es</para></summary>
        Xbil,

        /// <summary>AIAF - MERCADO DE RENTA FIJA<para>http://www.aiaf.es</para></summary>
        Xdrf,

        /// <summary>LATIBEX<para>http://www.latibex.com</para></summary>
        Xlat,

        /// <summary>BOLSA DE MADRID<para>http://www.bolsamadrid.es</para></summary>
        Xmad,

        /// <summary>MERCADO CONTINUO ESPANOL - CONTINUOUS MARKET<para>http://www.bolsasymercados.es</para></summary>
        Xmce,

        /// <summary>MEFF FINANCIAL DERIVATIVES<para>http://www.meff.com</para></summary>
        Xmrv,

        /// <summary>BOLSA DE VALENCIA<para>http://www.bolsavalencia.es</para></summary>
        Xval,

        /// <summary>MERCADO ELECTRONICO DE RENTA FIJA<para>http://www.bmerf.es</para></summary>
        Merf,

        /// <summary>MEFF POWER DERIVATIVES<para>http://www.meff.com</para></summary>
        Xmpw,

        /// <summary>MERCADO ALTERNATIVO DE RENTA FIJA<para>http://www.aiaf.es</para></summary>
        Marf,

        /// <summary>BME CLEARING SA<para>http://www.aiaf.es</para></summary>
        Bmcl,

        /// <summary>BOLSA DE BARCELONA RENTA FIJA<para>http://www.bolsasymercados.es</para></summary>
        Sbar,

        /// <summary>BOLSA DE BILBAO RENTA FIJA<para>http://www.bolsasymercados.es</para></summary>
        Sbil,

        /// <summary>BME - APA<para>http://www.bolsasymercados.es/regulatory-services/ing/home</para></summary>
        Bmea,

        /// <summary>IBERIAN GAS HUB<para>http://www.iberiangashub.com</para></summary>
        Ibgh,

        /// <summary>MERCADO ORGANIZADO DEL GAS<para>http://www.mercadosgas.omie.es</para></summary>
        Mibg,

        /// <summary>OMI POLO ESPANOL SA (OMIE)<para>http://www.omie.es</para></summary>
        Omel,

        /// <summary>ALTERNATIVE PLATFORM FOR SPANISH SECURITIES<para>http://www.paveplatform.com</para></summary>
        Pave,

        /// <summary>CADE - MERCADO DE DEUDA PUBLICA ANOTADA</summary>
        Xdpa,

        /// <summary>SISTEMA ESPANOL DE NEGOCIACION DE ACTIVOS FINANCIEROS<para>http://www.senaf.net</para></summary>
        Xnaf,

        /// <summary>CRYEX - FX AND DIGITAL CURRENCIES<para>http://www.cryex.com</para></summary>
        Cryd,

        /// <summary>CRYEX<para>http://www.cryex.com</para></summary>
        Cryx,

        /// <summary>NASDAQ STOCKHOLM AB - APA SERVICE<para>http://www.nasdaqomxnordic.com</para></summary>
        Napa,

        /// <summary>SEB - LIQUIDITY POOL<para>http://www.seb.se</para></summary>
        Sebx,

        /// <summary>SEB ENSKILDA<para>http://www.seb.se</para></summary>
        Ensx,

        /// <summary>SEB - SYSTEMATIC INTERNALISER<para>http://www.seb.se</para></summary>
        Sebs,

        /// <summary>NORDIC GROWTH MARKET<para>http://www.ngm.se</para></summary>
        Xngm,

        /// <summary>NORDIC MTF<para>http://www.nordicmtf.se</para></summary>
        Nmtf,

        /// <summary>NORDIC DERIVATIVES EXCHANGE<para>http://www.ndx.se</para></summary>
        Xndx,

        /// <summary>NORDIC MTF REPORTING<para>http://www.nordicmtf.se</para></summary>
        Xnmr,

        /// <summary>AKTIETORGET<para>http://www.aktietorget.se</para></summary>
        Xsat,

        /// <summary>NASDAQ STOCKHOLM AB<para>http://www.nasdaqomxnordic.com</para></summary>
        Xsto,

        /// <summary>FIRST NORTH SWEDEN<para>http://nasdaqomxnordic.com/firstnorth</para></summary>
        Fnse,

        /// <summary>OTC PUBLICATION VENUE<para>http://www.nasdaqomxnordic.com</para></summary>
        Xopv,

        /// <summary>NASDAQ CLEARING AB<para>http://www.nasdaqomx.com/europeanclearing</para></summary>
        Csto,

        /// <summary>NASDAQ STOCKHOLM AB - NORDIC@MID<para>http://www.nasdaqomxnordic.com</para></summary>
        Dsto,

        /// <summary>FIRST NORTH SWEDEN - NORDIC@MID<para>http://nasdaqomxnordic.com/firstnorth</para></summary>
        Dnse,

        /// <summary>NASDAQ STOCKHOLM AB � AUCTION ON DEMAND<para>http://www.nasdaqomxnordic.com</para></summary>
        Msto,

        /// <summary>FIRST NORTH SWEDEN � AUCTION ON DEMAND<para>http://www.nasdaqomxnordic.com</para></summary>
        Mnse,

        /// <summary>NASDAQ STOCKHOLM AB - DANISH EQ DERIVATIVES<para>http://www.nasdaqomxnordic.com</para></summary>
        Dked,

        /// <summary>NASDAQ STOCKHOLM AB - FINNISH EQ DERIVATIVES<para>http://www.nasdaqomxnordic.com</para></summary>
        Fied,

        /// <summary>NASDAQ STOCKHOLM AB - NORWEGIAN EQ DERIVATIVES<para>http://www.nasdaqomxnordic.com</para></summary>
        Noed,

        /// <summary>NASDAQ STOCKHOLM AB - SWEDISH EQ DERIVATIVES<para>http://www.nasdaqomxnordic.com</para></summary>
        Seed,

        /// <summary>NASDAQ STOCKHOLM AB - PAN-NORDIC EQ DERIVATIVES<para>http://www.nasdaqomxnordic.com</para></summary>
        Pned,

        /// <summary>NASDAQ STOCKHOLM AB - EUR WB EQ DERIVATIVES<para>http://www.nasdaqomxnordic.com</para></summary>
        Euwb,

        /// <summary>NASDAQ STOCKHOLM AB - USD WB EQ DERIVATIVES<para>http://www.nasdaqomxnordic.com</para></summary>
        Uswb,

        /// <summary>NASDAQ STOCKHOLM AB - DANISH FI DERIVATIVES<para>http://www.nasdaqomxnordic.com</para></summary>
        Dkfi,

        /// <summary>NASDAQ STOCKHOLM AB - NORWEGIAN FI DERIVATIVES<para>http://www.nasdaqomxnordic.com</para></summary>
        Nofi,

        /// <summary>NASDAQ STOCKHOLM AB - EUR FI DERIVATIVES<para>http://www.nasdaqomxnordic.com</para></summary>
        Ebon,

        /// <summary>FIRST NORTH SWEDEN - NORWAY<para>http://nasdaqomxnordic.com/firstnorth</para></summary>
        Onse,

        /// <summary>NASDAQ STOCKHOLM AB � NORWAY ETF<para>http://nasdaqomxnordic.com</para></summary>
        Esto,

        /// <summary>AIXECUTE<para>http://www.bekb.ch</para></summary>
        Aixe,

        /// <summary>SWISS DOTS BY CATS<para>http://www.bs-cats.com</para></summary>
        Dots,

        /// <summary>EBS SERVICE COMPANY LIMITED - ALL MARKETS<para>http://www.ebs.com</para></summary>
        Ebss,

        /// <summary>EBS MARKET- CLOB - FOR THE TRADING OF SPOT FX, PRECIOUS METALS AND OTHER FX PRODUCTS<para>http://www.ebs.com</para></summary>
        Ebsc,

        /// <summary>EUREX ZURICH<para>http://www.eurexrepo.com</para></summary>
        Euch,

        /// <summary>EUREX OTC SPOT MARKET<para>http://www.eurexrepo.com</para></summary>
        Eusp,

        /// <summary>EUREX REPO MARKET<para>http://www.eurexrepo.com</para></summary>
        Eurm,

        /// <summary>EUREX CH SECLEND MARKET<para>http://www.eurexrepo.com</para></summary>
        Eusc,

        /// <summary>SOCIETY3 FUNDERSMART<para>http://society3.com</para></summary>
        // ReSharper disable once InconsistentNaming
        S3fm,

        /// <summary>STOXX LIMITED<para>http://www.stoxx.com</para></summary>
        Stox,

        /// <summary>STOXX LIMITED - CUSTOMIZED INDICES<para>http://www.stoxx.com</para></summary>
        Xscu,

        /// <summary>STOXX LIMITED - VOLATILITY INDICES<para>http://www.stoxx.com</para></summary>
        Xstv,

        /// <summary>STOXX LIMITED - INDICES<para>http://www.stoxx.com</para></summary>
        Xstx,

        /// <summary>UBS TRADING<para>http://www.ubs.com</para></summary>
        Ubsg,

        /// <summary>UBS FX<para>http://www.ubs.com</para></summary>
        Ubsf,

        /// <summary>UBS PIN-FX<para>http://www.ubs.com</para></summary>
        Ubsc,

        /// <summary>VONTOBEL LIQUIDITY EXTENDER<para>http://www.vontobel.com</para></summary>
        Vlex,

        /// <summary>BX SWISS AG<para>http://"www.bxswiss.com</para></summary>
        Xbrn,

        /// <summary>SIX SWISS EXCHANGE<para>http://www.six-swiss-exchange.com</para></summary>
        Xswx,

        /// <summary>SIX SWISS EXCHANGE � STRUCTURED PRODUCTS<para>http://www.six-structured-products.com</para></summary>
        Xqmh,

        /// <summary>SIX SWISS EXCHANGE - BLUE CHIPS SEGMENT<para>http://www.six-swiss-exchange.com</para></summary>
        Xvtx,

        /// <summary>SIX SWISS BILATERAL TRADING PLATFORM FOR STRUCTURED OTC PRODUCTS<para>http://www.six-swiss-exchange.com</para></summary>
        Xbtr,

        /// <summary>SIX SWISS EXCHANGE - SIX SWISS EXCHANGE AT MIDPOINT<para>http://www.six-swiss-exchange.com</para></summary>
        Xswm,

        /// <summary>SIX SWISS EXCHANGE - SLS<para>http://www.six-swiss-exchange.com</para></summary>
        Xsls,

        /// <summary>SIX CORPORATE BONDS AG<para>http://www.six-swiss-exchange.com</para></summary>
        Xicb,

        /// <summary>ZURCHER KANTONALBANK SECURITIES EXCHANGE<para>http://www.zkb.ch</para></summary>
        Zkbx,

        /// <summary>ZURCHER KANTONALBANK - EKMU-X<para>http://www.zkb.ch/ekmux</para></summary>
        Kmux,

        /// <summary>CLIMEX<para>http://www.climex.com</para></summary>
        Clmx,

        /// <summary>ICE CLEAR NETHERLANDS BV<para>http://www.theice.com</para></summary>
        Hchc,

        /// <summary>ICE ENDEX FUTURES<para>http://www.theice.com/endex</para></summary>
        Ndex,

        /// <summary>ICE ENDEX PHYSICAL FORWARDS<para>http://www.theice.com/endex</para></summary>
        Imco,

        /// <summary>ICE MARKETS EQUITY<para>http://www.theice.com/endex</para></summary>
        Imeq,

        /// <summary>ICE ENDEX EUROPEAN GAS SPOT<para>http://www.theice.com/endex</para></summary>
        Ndxs,

        /// <summary>APX POWER NL<para>http://www.apxgroup.com</para></summary>
        Nlpx,

        /// <summary>EURONEXT - EURONEXT AMSTERDAM<para>http://www.euronext.com</para></summary>
        Xams,

        /// <summary>EURONEXT - TRADED BUT NOT LISTED AMSTERDAM<para>http://www.euronext.com</para></summary>
        Tnla,

        /// <summary>EURONEXT COM, COMMODITIES FUTURES AND OPTIONS<para>http://www.euronext.com</para></summary>
        Xeuc,

        /// <summary>EURONEXT EQF, EQUITIES AND INDICES DERIVATIVES<para>http://www.euronext.com</para></summary>
        Xeue,

        /// <summary>EURONEXT IRF, INTEREST RATE FUTURE AND OPTIONS<para>http://www.euronext.com</para></summary>
        Xeui,

        /// <summary>EMS EXCHANGE<para>http://www.vanlanschot.nl</para></summary>
        Xems,

        /// <summary>NXCHANGE<para>http://www.nxchange.com</para></summary>
        Xnxc,

        /// <summary>SSY FUTURES LTD -  FREIGHT SCREEN<para>http://www.ssyonline.com</para></summary>
        X3579,

        /// <summary>ABIDE FINANCIAL DRSP LIMITED APA<para>http://www.abide-financial.com</para></summary>
        Afdl,

        /// <summary>ASSET MATCH PRIVATE EXCHANGE<para>http://www.assetmatch.com</para></summary>
        Ampx,

        /// <summary>AUSTRALIA AND NEW ZEALAND BANKING GROUP LIMITED SYSTEMATIC INTERNALISER<para>http://www.anz.com.au</para></summary>
        Anzl,

        /// <summary>AQUIS EXCHANGE<para>http://www.aquis.eu</para></summary>
        Aqxe,

        /// <summary>ARAX COMMODITIES LTD<para>http://www.araxcommodities.com</para></summary>
        Arax,

        /// <summary>ATLANTIC BROKERS LTD<para>http://www.atlanticbrokers.co.uk</para></summary>
        Atlb,

        /// <summary>AUTILLA<para>http://www.autilla.com</para></summary>
        Autx,

        /// <summary>AUTILLA - PRECIOUS METALS<para>http://www.autilla.com</para></summary>
        Autp,

        /// <summary>AUTILLA - BASE METALS<para>http://www.autilla.com</para></summary>
        Autb,

        /// <summary>THE BALTIC EXCHANGE<para>http://www.balticexchange.com</para></summary>
        Balt,

        /// <summary>BALTEX - FREIGHT DERIVATIVES MARKET<para>http://www.balticexchange.com</para></summary>
        Bltx,

        /// <summary>BLOOMBERG - APA<para>http://www.bloomberg.com</para></summary>
        Bapa,

        /// <summary>BATS EUROPE REGULATED MARKETS<para>http://www.batstrading.co.uk</para></summary>
        Bcrm,

        /// <summary>BATS EUROPE - REGULATED MARKET OFF BOOK<para>http://www.batstrading.co.uk</para></summary>
        Baro,

        /// <summary>BATS EUROPE - REGULATED MARKET DARK BOOK<para>http://www.batstrading.co.uk</para></summary>
        Bark,

        /// <summary>BATS EUROPE - REGULATED MARKET INTEGRATED BOOK<para>http://www.batstrading.co.uk</para></summary>
        Bart,

        /// <summary>BATS  EUROPE<para>http://www.batstrading.co.uk</para></summary>
        Bcxe,

        /// <summary>BATS EUROPE -BXE ORDER BOOKS<para>http://www.batstrading.co.uk</para></summary>
        Bate,

        /// <summary>BATS EUROPE - CXE ORDER BOOKS<para>http://www.batstrading.co.uk</para></summary>
        Chix,

        /// <summary>BATS EUROPE -BXE DARK ORDER BOOK<para>http://www.batstrading.co.uk</para></summary>
        Batd,

        /// <summary>BATS EUROPE - CXE DARK ORDER BOOK<para>http://www.batstrading.co.uk</para></summary>
        Chid,

        /// <summary>BATS EUROPE � BATS OFF-BOOK<para>http://www.batstrading.co.uk</para></summary>
        Batf,

        /// <summary>BATS EUROPE - CXE - OFF-BOOK<para>http://www.batstrading.co.uk</para></summary>
        Chio,

        /// <summary>BATS EUROPE - BXE PERIODIC<para>http://www.batstrading.co.uk</para></summary>
        Batp,

        /// <summary>OFF EXCHANGE IDENTIFIER FOR OTC TRADES REPORTED TO BATS EUROPE<para>http://www.batstrading.co.uk</para></summary>
        Botc,

        /// <summary>BATS  EUROPE - LIS SERVICE<para>http://www.batstrading.co.uk</para></summary>
        Lisx,

        /// <summary>BGC BROKERS LP<para>http://www.bgcpartners.com</para></summary>
        Bgci,

        /// <summary>BGC BROKERS LP - TRAYPORT<para>http://www.bgcpartners.com</para></summary>
        Bgcb,

        /// <summary>BNY MELLON INTERNATIONAL - LONDON BRANCH<para>http://www.bnymellon.com</para></summary>
        Bkln,

        /// <summary>BNY MELLON - SYSTEMATIC INTERNALISER<para>http://www.bnymellon.com</para></summary>
        Bklf,

        /// <summary>BLOCKMATCH<para>http://www.instinet.com</para></summary>
        Blox,

        /// <summary>BLOOMBERG TRADING FACILITY LIMITED<para>http://www.bloomberg.com</para></summary>
        Bmtf,

        /// <summary>CINNOBER BOAT<para>http://www.cinnober.com/boat-trade-reporting</para></summary>
        Boat,

        /// <summary>BONDSCAPE<para>http://www.bondscape.net</para></summary>
        Bosc,

        /// <summary>BERNSTEIN CROSS (BERN-X)<para>http://www.alliancebernstein.com</para></summary>
        Brnx,

        /// <summary>BROKERTEC EUROPE LIMITED - ALL MARKETS<para>http://www.brokertec.com/</para></summary>
        Btee,

        /// <summary>EBS MTF - CLOB - FOR THE TRADING OF FX PRODUCTS<para>http://www.ebs.com</para></summary>
        Ebsm,

        /// <summary>EBS MTF - RFQ - FOR THE TRADING OF FX PRODUCTS<para>http://www.ebs.com</para></summary>
        Ebsd,

        /// <summary>EBS MTF - RFQ - FOR ASSET MANAGERS TRADING FX PRODUCTS<para>http://www.ebs.com</para></summary>
        Ebsi,

        /// <summary>EBS MTF - RFQ - FOR CORPORATES TRADING FX PRODUCTS<para>http://www.ebs.com</para></summary>
        Nexy,

        /// <summary>CONTINENTAL CAPITAL MARKETS LIMITED - OTF<para>http://www.conticap.com</para></summary>
        Ccml,

        /// <summary>CANTORCO2ECOM LIMITED<para>http://www.cantorco2e.com</para></summary>
        Cco2,

        /// <summary>CITI MATCH<para>http://www.citigroup.com</para></summary>
        Cgme,

        /// <summary>CA CHEUVREUX<para>http://www.cheuvreux.com</para></summary>
        Chev,

        /// <summary>BLINK MTF<para>http://www.cheuvreux.com</para></summary>
        Blnk,

        /// <summary>CME  EUROPE<para>http://www.cmegroup.com</para></summary>
        Cmee,

        /// <summary>CME CLEARING EUROPE<para>http://www.cmeclearingeurope.com</para></summary>
        Cmec,

        /// <summary>CME EUROPE - DERIVATIVES<para>http://www.cmeeurope.com</para></summary>
        Cmed,

        /// <summary>CLEAR MARKETS EUROPE LIMITED<para>http://www.clear-markets.com</para></summary>
        Cmmt,

        /// <summary>CRYPTO FACILITIES<para>http://www.cryptofacilities.co.uk</para></summary>
        Cryp,

        /// <summary>CREDIT SUISSE (EUROPE)<para>http://www.credit-suisse.com</para></summary>
        Cseu,

        /// <summary>CREDIT SUISSE AES CROSSFINDER EUROPE<para>http://www.credit-suisse.com</para></summary>
        Cscf,

        /// <summary>CREDIT SUISSE AES EUROPE BENCHMARK CROSS<para>http://www.credit-suisse.com</para></summary>
        Csbx,

        /// <summary>CREDIT SUISSE SECURITIES (EUROPE) LIMITED - SYSTEMATIC INTERNALISER<para>http://www.credit-suisse.com</para></summary>
        Sics,

        /// <summary>CREDIT SUISSE INTERNATIONAL<para>http://www.credit-suisse.com</para></summary>
        Csin,

        /// <summary>CREDIT SUISSE INTERNATIONAL - SYSTEMATIC INTERNALISER<para>http://www.credit-suisse.com</para></summary>
        Cssi,

        /// <summary>DEUTSCHE BANK AG OPERATING MARKET CODE FOR EU SYSTEMATIC INTERNALISER<para>https://autobahn.db.com</para></summary>
        Dbes,

        /// <summary>DEUTSCHE BANK INTERNALISATION<para>https://autobahn.db.com/microsite/html/equity.html</para></summary>
        Dbix,

        /// <summary>DEUTSCHE BANK - DIRECT CAPITAL ACCESS<para>https://autobahn.db.com/microsite/html/equity.html</para></summary>
        Dbdc,

        /// <summary>DEUTSCHE BANK - CLOSE CROSS<para>https://autobahn.db.com/microsite/html/equity.html</para></summary>
        Dbcx,

        /// <summary>DEUTSCHE BANK - CENTRAL RISK BOOK<para>https://autobahn.db.com/microsite/html/equity.html</para></summary>
        Dbcr,

        /// <summary>DEUTSCHE BANK - MANUAL OTC<para>https://autobahn.db.com/microsite/html/equity.html</para></summary>
        Dbmo,

        /// <summary>DEUTSCHE BANK - SUPERX EU<para>https://autobahn.db.com/microsite/html/equity.html</para></summary>
        Dbse,

        /// <summary>DOWGATE<para>http://www.dowgate.com</para></summary>
        Dowg,

        /// <summary>LONDON STOCK EXCHANGE - APA<para>http://www.londonstockexchange.com</para></summary>
        Echo,

        /// <summary>EMERGING MARKETS BOND EXCHANGE LIMITED<para>http://www.embonds.com</para></summary>
        Embx,

        /// <summary>ENCLEAR<para>http://www.lchclearnet.com</para></summary>
        Encl,

        /// <summary>EQUILEND EUROPE LIMITED<para>http://www.equilend.com</para></summary>
        Eqld,

        /// <summary>EXANE BNP PARIBAS<para>http://www.exane.com</para></summary>
        Exeu,

        /// <summary>EXANE BNP PARIBAS - MID POINT<para>http://www.exane.com</para></summary>
        Exmp,

        /// <summary>EXANE BNP PARIBAS - CHILD ORDER CROSSING<para>http://www.exane.com</para></summary>
        Exor,

        /// <summary>EXANE BNP PARIBAS - VOLUME PROFILE CROSSING<para>http://www.exane.com</para></summary>
        Exvp,

        /// <summary>EXANE BNP PARIBAS - BID-OFFER CROSSING<para>http://www.exane.com</para></summary>
        Exbo,

        /// <summary>EXANE BNP PARIBAS - LIQUIDITY PROVISION<para>http://www.exane.com</para></summary>
        Exlp,

        /// <summary>EXANE BNP PARIBAS - DIRECT CAPITAL ACCESS<para>http://www.exane.com</para></summary>
        Exdc,

        /// <summary>EXANE BNP PARIBAS - SYSTEMATIC INTERNALISER<para>http://www.exane.com</para></summary>
        Exsi,

        /// <summary>EXANE BNP PARIBAS - CLOSING PRICE<para>http://www.exane.com</para></summary>
        Excp,

        /// <summary>EXOTIX CAPITAL  - OTF<para>http://www.exotix.com</para></summary>
        Exot,

        /// <summary>CANTOR SPREADFAIR<para>http://www.spreadfair.com</para></summary>
        Fair,

        /// <summary>FREIGHT INVESTOR SERVICES LIMITED<para>http://www.freightinvestorservices.com</para></summary>
        Fisu,

        /// <summary>FXCM - MTF<para>http://www.fxcm.com</para></summary>
        Fxgb,

        /// <summary>GEMMA (GILT EDGED MARKET MAKERS ASSOCIATION)</summary>
        Gemx,

        /// <summary>GFI CREDITMATCH<para>http://www.gfigroup.com</para></summary>
        Gfic,

        /// <summary>GFI FOREXMATCH<para>http://www.gfigroup.com</para></summary>
        Gfif,

        /// <summary>GFI ENERGYMATCH<para>http://www.gfigroup.com</para></summary>
        Gfin,

        /// <summary>GFI RATESMATCH<para>http://www.gfigroup.com</para></summary>
        Gfir,

        /// <summary>GMEX EXCHANGE<para>http://www.gmex-group.com</para></summary>
        Gmeg,

        /// <summary>LONDON DERIVATIVES EXCHANGE<para>http://www.gmex-group.com</para></summary>
        Xldx,

        /// <summary>GLOBAL DERIVATIVES EXCHANGE<para>http://www.gmex-group.com</para></summary>
        Xgdx,

        /// <summary>GLOBAL SECURITIES EXCHANGE<para>http://www.gmex-group.com</para></summary>
        Xgsx,

        /// <summary>GLOBAL COMMODITIES EXCHANGE<para>http://www.gmex-group.com</para></summary>
        Xgcx,

        /// <summary>GRIFFIN MARKETS LIMITED<para>http://www.griffinmarkets.com</para></summary>
        Grif,

        /// <summary>GRIFFIN MARKETS LIMITED - OTF<para>http://www.griffinmarkets.com</para></summary>
        Grio,

        /// <summary>THE GREEN STOCK EXCHANGE - ACB IMPACT MARKETS<para>http://www.acbimpactmarkets.com</para></summary>
        Grse,

        /// <summary>GOLDMAN SACHS INTERNATIONAL BANK<para>http://www.goldmansachs.com</para></summary>
        Gsib,

        /// <summary>GOLDMAN SACHS INTERNATIONAL BANK - SYSTEMATIC INTERNALISER<para>http://www.goldmansachs.com</para></summary>
        Bisi,

        /// <summary>GOLDMAN SACHS INTERNATIONAL<para>http://www.goldmansachs.com/gset</para></summary>
        Gsil,

        /// <summary>GOLDMAN SACHS INTERNATIONAL - SYSTEMATIC INTERNALISER<para>http://www.goldmansachs.com/gset</para></summary>
        Gssi,

        /// <summary>GOLDMAN SACHS INTERNATIONAL - SIGMA BCN<para>http://www.goldmansachs.com/gset</para></summary>
        Gsbx,

        /// <summary>HPC SA<para>http://www.otcexgroup.com</para></summary>
        Hpcs,

        /// <summary>HSBC - SYSTEMATIC INTERNALISER<para>http://www.hsbcnet.com</para></summary>
        Hsbc,

        /// <summary>ICE BENCHMARK ADMINISTRATION<para>http://www.theice.com/iba</para></summary>
        Ibal,

        /// <summary>ICAP EUROPE<para>http://www.i-swap.com</para></summary>
        Icap,

        /// <summary>TRAYPORT<para>http://www.icap.com</para></summary>
        Icah,

        /// <summary>ICAP ENERGY<para>http://www.icapenergy.com</para></summary>
        Icen,

        /// <summary>ICAP SECURITIES<para>http://www.icap.com</para></summary>
        Icse,

        /// <summary>ICAP TRUEQUOTE<para>http://www.icapenergy.com/eu</para></summary>
        Ictq,

        /// <summary>ICAP WCLK<para>http://www.icap.com</para></summary>
        Wclk,

        /// <summary>ICAP GLOBAL DERIVATIVES LIMITED<para>http://www.icap.com/what-we-do/global-broking/sef.aspx</para></summary>
        Igdl,

        /// <summary>ICE FUTURES EUROPE<para>http://www.theice.com</para></summary>
        Ifeu,

        /// <summary>CREDITEX BROKERAGE LLP - MTF<para>http://www.creditex.com</para></summary>
        Cxrt,

        /// <summary>ICE FUTURES EUROPE - EQUITY PRODUCTS DIVISION<para>http://www.theice.com</para></summary>
        Iflo,

        /// <summary>ICE FUTURES EUROPE - FINANCIAL PRODUCTS DIVISION<para>http://www.theice.com</para></summary>
        Ifll,

        /// <summary>ICE FUTURES EUROPE - EUROPEAN UTILITIES DIVISION<para>http://www.theice.com</para></summary>
        Ifut,

        /// <summary>ICE FUTURES EUROPE - AGRICULTURAL PRODUCTS DIVISION<para>http://www.theice.com</para></summary>
        Iflx,

        /// <summary>ICE FUTURES EUROPE - OIL AND REFINED PRODUCTS DIVISION<para>http://www.theice.com</para></summary>
        Ifen,

        /// <summary>CREDITEX BROKERAGE LLP - OTF<para>http://www.theice.com/service/creditex</para></summary>
        Cxot,

        /// <summary>SWAPXECUTE<para>http://www.intlfcstone.com</para></summary>
        Ifls,

        /// <summary>INVESTEC BANK PLC - SYSTEMATIC INTERNALISER<para>http://www.investec.com</para></summary>
        Inve,

        /// <summary>I-SWAP<para>http://www.i-swap.com</para></summary>
        Iswa,

        /// <summary>JPMORGAN CHASE BANK NA<para>http://www.jpmorgan.com</para></summary>
        Jpcb,

        /// <summary>JP MORGAN SECURITIES PLC<para>http://www.jpmorgan.com</para></summary>
        Jpsi,

        /// <summary>JANE STREET FINANCIAL LTD - SYSTEMATIC INTERNALISER<para>http://www.janestreet.com</para></summary>
        Jssi,

        /// <summary>KNIGHT LINK EUROPE<para>http://www.knight.com</para></summary>
        Kleu,

        /// <summary>CURRENEX LDFX<para>http://www.currenex.com</para></summary>
        Lcur,

        /// <summary>LIQUIDNET SYSTEMS<para>http://www.liquidnet.com</para></summary>
        Liqu,

        /// <summary>LIQUIDNET H20<para>http://www.liquidnet.com</para></summary>
        Liqh,

        /// <summary>LIQUIDNET EUROPE LIMITED<para>http://www.liquidnet.com</para></summary>
        Liqf,

        /// <summary>LMAX<para>http://www.mtf.lmax.com/</para></summary>
        Lmax,

        /// <summary>LMAX - DERIVATIVES<para>http://www.lmax.com</para></summary>
        Lmad,

        /// <summary>LMAX - EQUITIES<para>http://www.lmax.com</para></summary>
        Lmae,

        /// <summary>LMAX - FX<para>http://www.lmax.com</para></summary>
        Lmaf,

        /// <summary>LMAX - INDICES/RATES/COMMODITIES<para>http://www.lmax.com</para></summary>
        Lmao,

        /// <summary>LME CLEAR<para>http://www.lme.com</para></summary>
        Lmec,

        /// <summary>OTC MARKET<para>http://www.lotce.com</para></summary>
        Lotc,

        /// <summary>PLUS DERIVATIVES EXCHANGE<para>http://www.plus-dx.com</para></summary>
        Pldx,

        /// <summary>LONDON PLATINUM AND PALLADIUM MARKET<para>http://www.lppm.org.uk</para></summary>
        Lppm,

        /// <summary>MARKETAXESS EUROPE LIMITED<para>http://www.marketaxess.com</para></summary>
        Mael,

        /// <summary>CURRENEX MTF<para>http://www.currenex.com</para></summary>
        Mcur,

        /// <summary>CURRENEX MTF - STREAMING<para>http://www.currenex.com</para></summary>
        Mcxs,

        /// <summary>CURRENEX MTF - RFQ<para>http://www.currenex.com</para></summary>
        Mcxr,

        /// <summary>MF GLOBAL ENERGY MTF<para>http://www.mfglobal.com</para></summary>
        Mfgl,

        /// <summary>FX CONNECT - MTF<para>http://www.fxconnect.com</para></summary>
        Mfxc,

        /// <summary>FX CONNECT - MTF - ALLOCATIONS<para>http://www.fxconnect.com</para></summary>
        Mfxa,

        /// <summary>FX CONNECT - MTF - RFQ<para>http://www.fxconnect.com</para></summary>
        Mfxr,

        /// <summary>MIZUHO INTERNATIONAL - SYSTEMATIC INTERNALISER<para>http://www.onemizuho.eu</para></summary>
        Mhip,

        /// <summary>BANK OF AMERICA - MERRILL LYNCH INSTINCT X - EUROPE<para>http://corp.bankofamerica.com/business/ci/trader-instinct</para></summary>
        Mlxn,

        /// <summary>BANK OF AMERICA - MERRILL LYNCH AUCTION CROSS<para>http://corp.bankofamerica.com/business/ci/trader-instinct</para></summary>
        Mlax,

        /// <summary>BANK OF AMERICA - MERRILL LYNCH OTC - EUROPE<para>http://corp.bankofamerica.com/business/ci/trader-instinct</para></summary>
        Mleu,

        /// <summary>BANK OF AMERICA - MERRILL LYNCH VWAP CROSS - EUROPE<para>http://corp.bankofamerica.com/business/ci/trader-instinct</para></summary>
        Mlve,

        /// <summary>MORGAN STANLEY AND CO INTERNATIONAL PLC<para>http://www.morganstanley.com</para></summary>
        Msip,

        /// <summary>MORGAN STANLEY AND CO INTERNATIONAL PLC - SYSTEMATIC INTERNALISER<para>http://www.morganstanley.com</para></summary>
        Mssi,

        /// <summary>MARIANA UFP LLP<para>http://www.marianainvestments.com</para></summary>
        Mufp,

        /// <summary>MITSUBISHI UFJ TRUST INTERNATIONAL LIMITED - SYSTEMATIC INTERNALISER<para>http://www.tr.mufg.jp/english/ourservices/administration/muti.html</para></summary>
        Muti,

        /// <summary>MYTREASURY<para>http://www.mytreasury.com</para></summary>
        Mytr,

        /// <summary>N2EX<para>http://www.n2ex.com</para></summary>
        N2Ex,

        /// <summary>ICE ENDEX UK OCM GAS SPOT<para>http://www.theice.com/endex</para></summary>
        Ndcm,

        /// <summary>NEX SEF<para>http://www.nex.com</para></summary>
        Nexs,

        /// <summary>NEX EXCHANGE<para>http://www.nexexchange.com</para></summary>
        Nexx,

        /// <summary>NEX EXCHANGE GROWTH (NON-EQUITY)<para>http://www.nexexchange.com</para></summary>
        Nexf,

        /// <summary>NEX EXCHANGE GROWTH (EQUITY)<para>http://www.nexexchange.com</para></summary>
        Nexg,

        /// <summary>NEX EXCHANGE TRADING (EQUITY)<para>http://www.nexexchange.com</para></summary>
        Next,

        /// <summary>NEX EXCHANGE TRADING (NON-EQUITY)<para>http://www.nexexchange.com</para></summary>
        Nexn,

        /// <summary>NEX EXCHANGE MAIN BOARD (NON-EQUITY)<para>http://www.nexexchange.com</para></summary>
        Nexd,

        /// <summary>NEX EXCHANGE MAIN BOARD (EQUITY)<para>http://www.nexexchange.com</para></summary>
        Nexl,

        /// <summary>NOMURA OTC TRADES<para>http://www.nomura.com</para></summary>
        Noff,

        /// <summary>NOMURA SYSTEMATIC INTERNALISER<para>http://www.nomura.com</para></summary>
        Nosi,

        /// <summary>NASDAQ OMX EUROPE<para>http://www.nasdaqomxeurope.com</para></summary>
        Nuro,

        /// <summary>NASDAQ OMX NLX<para>http://www.nlx.co.uk</para></summary>
        Xnlx,

        /// <summary>NASDAQ EUROPE (NURO) DARK<para>http://www.nasdaqomxeurope.com</para></summary>
        Nurd,

        /// <summary>NX<para>http://www.nomura.com</para></summary>
        Nxeu,

        /// <summary>OTCEX<para>http://www.otcexgroup.com</para></summary>
        Otce,

        /// <summary>PEEL HUNT LLP UK<para>http://www.peelhunt.com</para></summary>
        Peel,

        /// <summary>PEEL HUNT RETAIL<para>http://www.peelhunt.com</para></summary>
        Xrsp,

        /// <summary>PEEL HUNT CROSSING<para>http://www.peelhunt.com</para></summary>
        Xphx,

        /// <summary>ARITAS FINANCIAL LTD<para>http://www.pipelinetrading.com</para></summary>
        Pieu,

        /// <summary>PIRUM<para>http://www.pirum.com</para></summary>
        Pirm,

        /// <summary>PROPERTY PARTNER EXCHANGE<para>http://www.propertypartner.co</para></summary>
        Ppex,

        /// <summary>Q-WIXX PLATFORM<para>http://www.qwixx.com</para></summary>
        Qwix,

        /// <summary>RBC EUROPE LIMITED<para>http://www.rbc.com</para></summary>
        Rbce,

        /// <summary>RBC INVESTOR SERVICES TRUST<para>http://www.rbcits.com</para></summary>
        Rbct,

        /// <summary>RBC INVESTOR SERVICES TRUST - SYSTEMATIC INTERNALISER<para>http://www.rbcits.com</para></summary>
        Rtsi,

        /// <summary>RBS CROSS<para>http://www.rbs.com</para></summary>
        Rbsx,

        /// <summary>REUTERS TRANSACTION SERVICES LIMITED<para>http://www.reuters.com</para></summary>
        Rtsl,

        /// <summary>REUTERS TRANSACTION SERVICES LIMITED - FORWARDS MATCHING<para>http://www.reuters.com</para></summary>
        Trfw,

        /// <summary>REUTERS TRANSACTION SERVICES LIMITED - FXALL RFQ<para>http://www.reuters.com</para></summary>
        Tral,

        /// <summary>SECFINEX<para>http://www.secfinex.com</para></summary>
        Secf,

        /// <summary>SEEDRS - SECONDARY MARKET<para>http://www.seedrs.com/secondary-market</para></summary>
        Sedr,

        /// <summary>SIGMA X MTF<para>http://gset.gs.com/sigmaxmtf/</para></summary>
        Sgmx,

        /// <summary>SIGMA X MTF - AUCTION BOOK<para>http://gset.gs.com/sigmaxmtf/</para></summary>
        Sgmy,

        /// <summary>ASSET MATCH<para>http://www.sharemark.com</para></summary>
        Shar,

        /// <summary>MAREX SPECTRON INTERNATIONAL LIMITED OTF<para>http://www.marexspectron.com</para></summary>
        Spec,

        /// <summary>SPREADZERO<para>http://www.spreadzero.com</para></summary>
        Sprz,

        /// <summary>SOCIAL STOCK EXCHANGE<para>http://www.socialstockexchange.com</para></summary>
        Ssex,

        /// <summary>SCHNEIDER OTF<para>http://www.schneidertrading.com</para></summary>
        Stal,

        /// <summary>STANDARD CHARTERED - SYSTEMATIC INTERNALISER<para>http://www.sc.com</para></summary>
        Stan,

        /// <summary>SUN TRADING INTERNATIONAL - SYSTEMATIC INTERNALISER<para>http://www.suntradinginternational.co.uk</para></summary>
        Stsi,

        /// <summary>SWAPSTREAM<para>http://www.swapstream.com</para></summary>
        Swap,

        /// <summary>TIDE CM<para>http://www.tidecm.com</para></summary>
        Tcml,

        /// <summary>VOLBROKER<para>http://www.tfsicap.com</para></summary>
        Tfsv,

        /// <summary>TRADITION-NEX OTF<para>http://www.tradition.com</para></summary>
        Fxop,

        /// <summary>THE PROPERTY INVESTMENT EXCHANGE<para>http://www.propex.co.uk</para></summary>
        Tpie,

        /// <summary>TRAX APA<para>http://www.traxmarkets.com</para></summary>
        Trax,

        /// <summary>TRADITION ELECTRONIC TRADING PLATFORM<para>http://www.tradition.com</para></summary>
        Trde,

        /// <summary>NAVESIS-MTF<para>http://www.tradition.com</para></summary>
        Nave,

        /// <summary>TRADITION CDS<para>http://www.tradition.co.uk</para></summary>
        Tcds,

        /// <summary>TRAD-X<para>http://www.tradition.com</para></summary>
        Trdx,

        /// <summary>TRADITION ENERGY<para>http://www.tfsgreen.com</para></summary>
        Tfsg,

        /// <summary>PARFX<para>http://www.parfx.com</para></summary>
        Parx,

        /// <summary>ELIXIUM<para>http://www.tradition.com</para></summary>
        Elix,

        /// <summary>TRADEWEB EUROPE LIMITED<para>http://www.tradeweb.com</para></summary>
        Treu,

        /// <summary>TRADEWEB EUROPE LIMITED - APA<para>http://www.tradeweb.com</para></summary>
        Trea,

        /// <summary>TRADEWEB EUROPE LIMITED - OTF<para>http://www.tradeweb.com</para></summary>
        Treo,

        /// <summary>TURQUOISE<para>http://www.tradeturquoise.com</para></summary>
        Trqx,

        /// <summary>TURQUOISE DARK<para>http://www.tradeturquoise.com</para></summary>
        Trqm,

        /// <summary>TURQUOISE SWAPMATCH<para>http://www.tradeturquoise.com</para></summary>
        Trqs,

        /// <summary>TURQUOISE LIT AUCTIONS<para>http://www.tradeturquoise.com</para></summary>
        Trqa,

        /// <summary>TOWER RESEARCH CAPITAL EUROPE LTD<para>http://www.tower-research.com</para></summary>
        Trsi,

        /// <summary>UBS AG LONDON BRANCH EMEA TRADING<para>http://www.ubs.com</para></summary>
        Ubsb,

        /// <summary>UBS AG LONDON BRANCH - SYSTEMATIC INTERNALISER<para>http://www.ubs.com</para></summary>
        Ubsy,

        /// <summary>UBS LIMITED EMEA TRADING<para>http://www.ubs.com</para></summary>
        Ubsl,

        /// <summary>UBS PIN (EMEA)<para>http://www.ubs.com</para></summary>
        Ubse,

        /// <summary>UBS LIMITED EMEA TRADING - SYSTEMATIC INTERNALISER<para>http://www.ubs.com</para></summary>
        Ubsi,

        /// <summary>APX POWER UK<para>http://www.apxgroup.com</para></summary>
        Ukpx,

        /// <summary>VANTAGE CAPITAL MARKETS LLP - OTF<para>http://www.vcmllp.com</para></summary>
        Vcmo,

        /// <summary>VEGA-CHI<para>http://www.vega-chi.com</para></summary>
        Vega,

        /// <summary>WINTERFLOOD SECURITIES LIMITED - ELECTRONIC PLATFORM<para>http://www.winterflood.com</para></summary>
        Wins,

        /// <summary>WINTERFLOOD SECURITIES LIMITED - MANUAL TRADING<para>http://www.winterflood.com</para></summary>
        Winx,

        /// <summary>ALTEX-ATS<para>http://www.altex-ats.co.uk</para></summary>
        Xalt,

        /// <summary>ICMA<para>http://www.icma-group.org</para></summary>
        Xcor,

        /// <summary>GLOBAL COAL LIMITED<para>http://www.globalcoal.com</para></summary>
        Xgcl,

        /// <summary>LONDON BULLION MARKET<para>http://www.lbma.org.uk</para></summary>
        Xlbm,

        /// <summary>LCHCLEARNET LTD<para>http://www.lchclearnet.com</para></summary>
        Xlch,

        /// <summary>EURONEXT - EURONEXT LONDON<para>http://www.euronext.com</para></summary>
        Xldn,

        /// <summary>EURONEXT BLOCK<para>http://www.euronext.com/blockmtf</para></summary>
        Xsmp,

        /// <summary>EURONEXT SYNAPSE<para>http://www.euronext.com</para></summary>
        Ensy,

        /// <summary>LONDON METAL EXCHANGE<para>http://www.lme.co.uk</para></summary>
        Xlme,

        /// <summary>LONDON STOCK EXCHANGE<para>http://www.londonstockexchange.com</para></summary>
        Xlon,

        /// <summary>LONDON STOCK EXCHANGE - AIM MTF<para>http://www.londonstockexchange.com</para></summary>
        Aimx,

        /// <summary>LONDON STOCK EXCHANGE - DERIVATIVES MARKET<para>http://www.londonstockexchange.com</para></summary>
        Xlod,

        /// <summary>LONDON STOCK EXCHANGE - MTF<para>http://www.londonstockexchange.com</para></summary>
        Xlom,

        /// <summary>EUROMTS LTD<para>http://www.mtsmarkets.com</para></summary>
        Xmts,

        /// <summary>MTS HUNGARY</summary>
        Hung,

        /// <summary>UK GILTS MARKET</summary>
        Ukgd,

        /// <summary>MTS NETHERLANDS<para>http://www.mtsmarkets.com</para></summary>
        Amts,

        /// <summary>EUROMTS<para>http://www.euromts-ltd.com</para></summary>
        Emts,

        /// <summary>MTS GERMANY<para>http://www.mtsgermany.com</para></summary>
        Gmts,

        /// <summary>MTS IRELAND<para>http://www.mtsireland.com</para></summary>
        Imts,

        /// <summary>MTS CZECH REPUBLIC</summary>
        Mczk,

        /// <summary>MTS AUSTRIA<para>http://www.mtsaustria.com</para></summary>
        Mtsa,

        /// <summary>MTS GREECE<para>http://www.mtsgreece.com</para></summary>
        Mtsg,

        /// <summary>MTS INTERDEALER SWAPS MARKET</summary>
        Mtss,

        /// <summary>MTS ISRAEL<para>http://www.mtsisrael.com</para></summary>
        Rmts,

        /// <summary>MTS SPAIN<para>http://www.mtsspain.com</para></summary>
        Smts,

        /// <summary>MTS SLOVENIA<para>http://www.mtsslovenia.com</para></summary>
        Vmts,

        /// <summary>BONDVISION UK<para>http://www.mtsmarkets.com</para></summary>
        Bvuk,

        /// <summary>MTS PORTUGAL<para>http://www.mtsmarkets.com</para></summary>
        Port,

        /// <summary>MTS SWAP MARKET<para>http://www.mtsmarkets.com</para></summary>
        Mtsw,

        /// <summary>ALPHA Y<para>http://www.execution.socgen.com</para></summary>
        Xsga,

        /// <summary>SWX SWISS BLOCK<para>http://www.swxeurope.com</para></summary>
        Xswb,

        /// <summary>TULLETT PREBON PLC<para>http://www.tullettprebon.com</para></summary>
        Xtup,

        /// <summary>TULLETT PREBON PLC - TP EQUITYTRADE<para>http://www.tullettprebon.com</para></summary>
        Tpeq,

        /// <summary>TULLETT PREBON PLC - TP ENERGY<para>http://www.tullettprebon.com</para></summary>
        Tben,

        /// <summary>TULLETT PREBON PLC - TP TRADEBLADE<para>http://www.tullettprebon.com</para></summary>
        Tbla,

        /// <summary>TULLETT PREBON PLC - TP CREDITDEAL<para>http://www.tullettprebon.com</para></summary>
        Tpcd,

        /// <summary>TULLETT PREBON PLC - TP FORWARDDEAL<para>http://www.tullettprebon.com</para></summary>
        Tpfd,

        /// <summary>TULLETT PREBON PLC - TP REPO<para>http://www.tullettprebon.com</para></summary>
        Tpre,

        /// <summary>TULLETT PREBON PLC - TP SWAPDEAL<para>http://www.tullettprebon.com</para></summary>
        Tpsd,

        /// <summary>TULLETT PREBON PLC - TP ENERGYTRADE<para>http://www.tullettprebon.com</para></summary>
        Xtpe,

        /// <summary>TULLETT PREBON PLC � TULLETT PREBON (EUROPE) LIMITED<para>http://www.tullettprebon.com</para></summary>
        Tpel,

        /// <summary>TULLETT PREBON PLC � TULLETT PREBON (SECURITIES) LIMITED<para>http://www.tullettprebon.com</para></summary>
        Tpsl,

        /// <summary>UBS MTF<para>http://www.ubs.com/mtf</para></summary>
        Xubs,

        /// <summary>ASSENT ATS<para>http://www.sungard.com</para></summary>
        Aats,

        /// <summary>ADVISE TECHNOLOGIES - APA TRANSPARENCY REPORTING<para>http://www.advisetechnologies.com</para></summary>
        Advt,

        /// <summary>AQUA EQUITIES LP<para>http://www.aquaequities.com</para></summary>
        Aqua,

        /// <summary>AUTOMATED TRADING DESK FINANCIAL SERVICES, LLC<para>http://www.atdesk.com</para></summary>
        Atdf,

        /// <summary>ATD - CITIGROUP AGENCY OPTION AND EQUITIES ROUTING ENGINE<para>http://www.atdesk.com</para></summary>
        Core,

        /// <summary>BANK OF AMERICA - MERRILL LYNCH INSTINCT X ATS<para>http://corp.bankofamerica.com/business/ci/trader-instinct</para></summary>
        Baml,

        /// <summary>BANK OF AMERICA - MERRILL LYNCH VWAP CROSS<para>http://corp.bankofamerica.com/business/ci/trader-instinct</para></summary>
        Mlvx,

        /// <summary>BANK OF AMERICA - MERRILL LYNCH OTC<para>http://corp.bankofamerica.com/business/ci/trader-instinct</para></summary>
        Mlco,

        /// <summary>BARCLAYS ATS<para>http://www.barx.com/equities/liquiditycrossing.html</para></summary>
        Barx,

        /// <summary>BARCLAYS FX � TRADING<para>http://www.barx.com/fx/index.html</para></summary>
        Bard,

        /// <summary>BARCLAYS LIQUID MARKETS<para>http://www.barcap.com</para></summary>
        Barl,

        /// <summary>BARCLAYS DIRECT EX ATS<para>http://realizations.barclays.com/global-markets/equities</para></summary>
        Bcdx,

        /// <summary>BATS Z-EXCHANGE<para>http://www.bats.com</para></summary>
        Bats,

        /// <summary>BZX OPTIONS MARKET<para>http://www.bats.com</para></summary>
        Bato,

        /// <summary>BATS Y-EXCHANGE, INC<para>http://www.bats.com</para></summary>
        Baty,

        /// <summary>BATS Z-EXCHANGE DARK<para>http://www.bats.com</para></summary>
        Bzxd,

        /// <summary>BATS Y-EXCHANGE DARK<para>http://www.bats.com</para></summary>
        Byxd,

        /// <summary>BLOOMBERG SEF LLC<para>http://www.bloombergsef.com</para></summary>
        Bbsf,

        /// <summary>BGC FINANCIAL INC<para>http://www.bgcpartners.com</para></summary>
        Bgcf,

        /// <summary>FENICS - US TREASURIES<para>http://www.fenicsust.com</para></summary>
        Fncs,

        /// <summary>BGC DERIVATIVE MARKETS LP<para>http://www.bgcsef.com</para></summary>
        Bgcd,

        /// <summary>BATS HOTSPOT SEF LLC<para>http://www.hotspotfx.com</para></summary>
        Bhsf,

        /// <summary>BIDS TRADING LP<para>http://www.bidstrading.com</para></summary>
        Bids,

        /// <summary>BLOOMBERG TRADEBOOK LLC<para>http://www.bloombergtradebook.com</para></summary>
        Bltd,

        /// <summary>BLOOMBERG BPOOL<para>http://www.bloomberg.com</para></summary>
        Bpol,

        /// <summary>CONVERGEX<para>http://www.convergex.com</para></summary>
        Bnyc,

        /// <summary>VORTEX<para>http://www.bnyconvergex.com</para></summary>
        Vtex,

        /// <summary>MILLENNIUM<para>http://www.convergex.com</para></summary>
        Nyfx,

        /// <summary>ICAP ELECTRONIC BROKING (US)<para>http://www.icap.com</para></summary>
        Btec,

        /// <summary>ICAP SEF (US)  LLC<para>http://www.icap.com</para></summary>
        Icsu,

        /// <summary>CITADEL SECURITIES<para>http://www.citadelsecurities.com</para></summary>
        Cded,

        /// <summary>CITIGROUP GLOBAL MARKETS<para>http://www.citigroup.com</para></summary>
        Cgmi,

        /// <summary>CITI CROSS<para>http://www.citigroup.com</para></summary>
        Cicx,

        /// <summary>CITI LIQUIFI<para>http://www.citivelocity.com</para></summary>
        Lqfi,

        /// <summary>CITIBLOC<para>http://www.citivelocity.com</para></summary>
        Cblc,

        /// <summary>CLEAR MARKETS NORTH AMERICA, INC<para>http://www.clear-markets.com</para></summary>
        Cmsf,

        /// <summary>CREDIT SUISSE (US)<para>http://www.credit-suisse.com</para></summary>
        Cred,

        /// <summary>CREDIT SUISSE AES CROSSFINDER<para>http://www.credit-suisse.com</para></summary>
        Caes,

        /// <summary>CREDIT SUISSE LIGHT POOL<para>http://www.credit-suisse.com</para></summary>
        Cslp,

        /// <summary>DEUTSCHE BANK SUPER X<para>http://www.db.com</para></summary>
        Dbsx,

        /// <summary>DCX (DERIVATIVES CURRENCY EXCHANGE)<para>http://www.fxcmpro.com/dcx</para></summary>
        Deal,

        /// <summary>BATS DIRECT EDGE<para>http://www.bats.com</para></summary>
        Edge,

        /// <summary>EDGX EXCHANGE DARK<para>http://www.bats.com</para></summary>
        Eddp,

        /// <summary>EDGA EXCHANGE<para>http://www.bats.com</para></summary>
        Edga,

        /// <summary>EDGA EXCHANGE DARK<para>http://www.bats.com</para></summary>
        Edgd,

        /// <summary>EDGX EXCHANGE<para>http://www.bats.com</para></summary>
        Edgx,

        /// <summary>EDGX OPTIONS MARKET<para>http://www.bats.com</para></summary>
        Edgo,

        /// <summary>EG MARKET TECHNOLOGIES<para>http://www.eglp.com</para></summary>
        Egmt,

        /// <summary>ERIS EXCHANGE<para>http://www.erisfutures.com</para></summary>
        Eris,

        /// <summary>FASTMATCH<para>http://www.fastmatch.com</para></summary>
        Fast,

        /// <summary>FINRA<para>http://www.finra.org/industry/compliance/markettransparency/adf/</para></summary>
        Finr,

        /// <summary>FINRA/NASDAQ TRF(TRADE REPORTING FACILITY)<para>http://www.finra.org</para></summary>
        Finn,

        /// <summary>FINRA ORF (TRADE REPORTING FACILITY)<para>http://www.finra.org/</para></summary>
        Fino,

        /// <summary>FINRA/NYSE TRF (TRADE REPORTING FACILITY)<para>http://www.finra.org/industry/compliance/markettransparency/trf/participants/</para></summary>
        Finy,

        /// <summary>FINRA ALTERNATIVE DISPLAY FACILITY (ADF)<para>http://www.finra.org</para></summary>
        Xadf,

        /// <summary>OTHER OTC<para>http://www.otcbb.com</para></summary>
        Ootc,

        /// <summary>FTSEF LLC<para>http://www.ftsef.com</para></summary>
        Fsef,

        /// <summary>FXALL<para>http://www.fxall.com</para></summary>
        Fxal,

        /// <summary>FXCM<para>http://www.fxcm.com</para></summary>
        Fxcm,

        /// <summary>G1 EXECUTION SERVICES<para>http://www.g1x.com</para></summary>
        // ReSharper disable once InconsistentNaming
        G1xx,

        /// <summary>GATE US LLC<para>http://www.gateus.com</para></summary>
        Gllc,

        /// <summary>ESSEX RADEZ, LLC<para>http://www.essexradez.com</para></summary>
        Glps,

        /// <summary>ACS EXECUTION SERVICES, LLC</summary>
        Glpx,

        /// <summary>GLOBAL OTC<para>http://www.globalotc.com</para></summary>
        Gotc,

        /// <summary>GOVEX<para>http://www.govex.com</para></summary>
        Govx,

        /// <summary>THE GREEN EXCHANGE<para>http://www.nymex.greenfutures.com/</para></summary>
        Gree,

        /// <summary>GOLDMAN SACHS AND CO<para>http://www.goldmansachs.com</para></summary>
        Gsco,

        /// <summary>SIGMA X2<para>http://www.goldmansachs.com</para></summary>
        Sgmt,

        /// <summary>GFI SWAPS EXCHANGE, LLC<para>http://www.gfigroup.com/markets/swaps-exchange/overview.aspx</para></summary>
        Gsef,

        /// <summary>KCG AMERICAS LLC<para>http://www.kcg.com</para></summary>
        Gtco,

        /// <summary>GTSX<para>http://www.gtsx.com</para></summary>
        Gtsx,

        /// <summary>GTX SEF, LLC<para>http://www.gaingtx.com/sef.shtml</para></summary>
        Gtxs,

        /// <summary>NADEX<para>http://www.nadex.com</para></summary>
        Hegx,

        /// <summary>POTAMUS TRADING LLC<para>http://www.potamustrading.com</para></summary>
        Hppo,

        /// <summary>HOTSPOT FX<para>http://www.hotspotfx.com</para></summary>
        Hsfx,

        /// <summary>ISLAND ECN LTD, THE<para>http://www.island.com</para></summary>
        Icel,

        /// <summary>INVESTORS EXCHANGE<para>http://www.iextrading.com</para></summary>
        Iexg,

        /// <summary>INVESTORS EXCHANGE - DARK<para>http://www.iextrading.com</para></summary>
        Iexd,

        /// <summary>ICE FUTURES US<para>http://www.theice.com</para></summary>
        Ifus,

        /// <summary>INTERCONTINENTAL EXCHANGE<para>http://www.theice.com</para></summary>
        Iepa,

        /// <summary>ICE MARKETS FOREIGN EXCHANGE<para>http://www.theice.com</para></summary>
        Imfx,

        /// <summary>ICE MARKETS AGRICULTURE<para>http://www.theice.com</para></summary>
        Imag,

        /// <summary>ICE MARKETS BONDS<para>http://www.theice.com</para></summary>
        Imbd,

        /// <summary>ICE MARKETS CREDIT<para>http://www.theice.com</para></summary>
        Imcr,

        /// <summary>ICE MARKETS ENERGY<para>http://www.theice.com</para></summary>
        Imen,

        /// <summary>ICE MARKETS RATES<para>http://www.theice.com</para></summary>
        Imir,

        /// <summary>ICE FUTURES US ENERGY DIVISION<para>http://www.theice.com</para></summary>
        Ifed,

        /// <summary>CREDITEX LLC<para>http://www.theice.com</para></summary>
        Imcg,

        /// <summary>CREDITEX SECURITIES CORPORATION<para>http://www.theice.com</para></summary>
        Imcc,

        /// <summary>ICE SWAP TRADE LLC<para>http://www.theice.com</para></summary>
        Ices,

        /// <summary>IMC FINANCIAL MARKETS<para>http://www.imc.com</para></summary>
        Imcs,

        /// <summary>ISDAFIX<para>http://www2.isda.org</para></summary>
        Isda,

        /// <summary>ITG - POSIT<para>http://www.itg.com</para></summary>
        Itgi,

        /// <summary>JETX<para>http://www.jefferies.com</para></summary>
        Jefx,

        /// <summary>JUMP LIQUIDITY<para>http://www.jumptrading.com</para></summary>
        Jlqd,

        /// <summary>JPBX<para>http://www.jpmorgan.com</para></summary>
        Jpbx,

        /// <summary>JPMX<para>http://www.jpmorgan.com</para></summary>
        Jpmx,

        /// <summary>JANE STREET EXECUTION SERVICES LLC<para>http://www.janestreet.com</para></summary>
        Jses,

        /// <summary>JANE STREET JX<para>http://www.janestreet.com</para></summary>
        Jsjx,

        /// <summary>KNIGHT<para>http://www.knight.com</para></summary>
        Knig,

        /// <summary>KNIGHT CAPITAL MARKETS LLC<para>http://www.knight.com</para></summary>
        Kncm,

        /// <summary>KNIGHT EQUITY MARKETS LP<para>http://www.knight.com</para></summary>
        Knem,

        /// <summary>KNIGHT LINK<para>http://www.knight.com</para></summary>
        Knli,

        /// <summary>KNIGHT MATCH ATS<para>http://www.knight.com</para></summary>
        Knmx,

        /// <summary>KCG ACKNOWLEDGE FI<para>http://www.kcg.com</para></summary>
        Ackf,

        /// <summary>LATAM SEF<para>http://www.latamsef.com</para></summary>
        Lasf,

        /// <summary>LEDGERX<para>http://www.ledgerx.com</para></summary>
        Ledg,

        /// <summary>LEVEL ATS<para>http://www.levelats.com</para></summary>
        Levl,

        /// <summary>LIQUIDNET, INC<para>http://www.liquidnet.com</para></summary>
        Lius,

        /// <summary>LIQUIDNET, INC FIXED INCOME ATS<para>http://www.liquidnet.com</para></summary>
        Lifi,

        /// <summary>LIQUIDNET, INC H2O ATS<para>http://www.liquidnet.com</para></summary>
        Liuh,

        /// <summary>LIQUIDITYEDGE<para>http://www.liquidityedge.trade</para></summary>
        Lqed,

        /// <summary>LUMINEX TRADING &amp; ANALYTICS LLC<para>http://www.luminextrading.com</para></summary>
        Ltaa,

        /// <summary>LUMINEX TRADING &amp; ANALYTICS LLC - ATS<para>http://www.luminextrading.com</para></summary>
        Lmnx,

        /// <summary>MIAMI INTERNATIONAL HOLDINGS, INC<para>http://www.miaxoptions.com</para></summary>
        Mihi,

        /// <summary>MIAMI INTERNATIONAL SECURITIES EXCHANGE<para>http://www.miaxoptions.com</para></summary>
        Xmio,

        /// <summary>MIAX PEARL, LLC<para>http://www.miaxoptions.com</para></summary>
        Mprl,

        /// <summary>MORGAN STANLEY AND CO LLC<para>http://www.morganstanley.com</para></summary>
        Msco,

        /// <summary>MS POOL<para>http://www.morganstanley.com</para></summary>
        Mspl,

        /// <summary>MS RETAIL POOL<para>http://www.morganstanley.com</para></summary>
        Msrp,

        /// <summary>MS TRAJECTORY CROSS<para>http://www.morganstanley.com</para></summary>
        Mstx,

        /// <summary>MORGAN STANLEY AUTOMATED LIQUIDITY PROVISION<para>http://www.morganstanley.com</para></summary>
        Mslp,

        /// <summary>MTS MARKETS INTERNATIONAL INC<para>http://www.mtsmarkets.com</para></summary>
        Mtus,

        /// <summary>BONDVISION US<para>http://www.mtsmarkets.com</para></summary>
        Bvus,

        /// <summary>MTS BONDSCOM<para>http://www.mtsmarkets.com/products/mts-bonds-com</para></summary>
        Mtsb,

        /// <summary>MARKETAXESS CORPORATION<para>http://www.marketaxess.com</para></summary>
        Mtxx,

        /// <summary>MARKETAXESS SEF CORPORATION<para>http://www.marketaxess.com</para></summary>
        Mtxs,

        /// <summary>MARKETAXESS CORPORATION MID-X TRADING SYSTEM<para>http://www.marketaxess.com</para></summary>
        Mtxm,

        /// <summary>MARKETAXESS CORPORATION SINGLE-NAME CDS CENTRAL LIMIT ORDER<para>http://www.marketaxess.com</para></summary>
        Mtxc,

        /// <summary>MARKETAXESS CANADA COMPANY<para>http://www.marketaxess.com</para></summary>
        Mtxa,

        /// <summary>NOBLE EXCHANGE<para>http://www.noblex.io</para></summary>
        Nblx,

        /// <summary>NATIONAL FINANCIAL SERVICES, LLC<para>http://www.fidelitycapitalmarkets.com</para></summary>
        Nfsc,

        /// <summary>FIDELITY CROSSSTREAM<para>http://www.capitalmarkets.fidelity.com</para></summary>
        Nfsa,

        /// <summary>FIDELITY DARK<para>http://www.capitalmarkets.fidelity.com</para></summary>
        Nfsd,

        /// <summary>FIDELITY CROSSSTREAM ATS<para>http://www.capitalmarkets.fidelity.com</para></summary>
        Xstm,

        /// <summary>NOMURA SECURITIES INTERNATIONAL<para>http://www.nomura.com</para></summary>
        Nmra,

        /// <summary>NODAL EXCHANGE<para>http://www.nodalexchange.com</para></summary>
        Nodx,

        /// <summary>NX ATS - CROSSING PLATFORM<para>http://www.nomura.com</para></summary>
        Nxus,

        /// <summary>NEW YORK PORTFOLIO CLEARING<para>http://www.nypclear.com</para></summary>
        Nypc,

        /// <summary>OTCEX LLC<para>http://www.otcexgroup.com</para></summary>
        Ollc,

        /// <summary>OPTIONS PRICE REPORTING AUTHORITY<para>http://www.opradata.com</para></summary>
        Opra,

        /// <summary>OTC MARKETS<para>http://www.otcmarkets.com</para></summary>
        Otcm,

        /// <summary>OTCQB MARKETPLACE<para>http://www.otcmarkets.com</para></summary>
        Otcb,

        /// <summary>OTCQX MARKETPLACE<para>http://www.otcmarkets.com</para></summary>
        Otcq,

        /// <summary>OTC PINK CURRENT<para>http://www.otcmarkets.com</para></summary>
        Pinc,

        /// <summary>OTC PINK NO INFORMATION<para>http://www.otcmarkets.com</para></summary>
        Pini,

        /// <summary>OTC PINK LIMITED<para>http://www.otcmarkets.com</para></summary>
        Pinl,

        /// <summary>OTC PINK MARKETPLACE<para>http://www.otcmarkets.com</para></summary>
        Pinx,

        /// <summary>OTC GREY MARKET<para>http://www.otcmarkets.com</para></summary>
        Psgm,

        /// <summary>CAVEAT EMPTOR<para>http://www.otcmarkets.com</para></summary>
        Cave,

        /// <summary>CODA MARKETS<para>http://www.pdqenterprises.com</para></summary>
        Pdqx,

        /// <summary>CODA MARKETS ATS DARK<para>http://www.pdqenterprises.com</para></summary>
        Pdqd,

        /// <summary>ARITAS SECURITIES LLC<para>http://www.pipelinetrading.com</para></summary>
        Pipe,

        /// <summary>PRAGMA ATS<para>http://www.pragmatrading.com</para></summary>
        Prse,

        /// <summary>BLOCKCROSS ATS<para>http://www.blockcross.com</para></summary>
        Pulx,

        /// <summary>RIVERCROSS<para>http://www.rxats.com</para></summary>
        Ricx,

        /// <summary>RIVERCROSS DARK<para>http://www.rxats.com</para></summary>
        Ricd,

        /// <summary>SEED SEF LLC<para>http://www.seedcx.com</para></summary>
        Scxs,

        /// <summary>GOLDMAN SACH MTF<para>http://www2.goldmansachs.com</para></summary>
        Sgma,

        /// <summary>DE SHAW<para>http://www.deshaw.com</para></summary>
        Shaw,

        /// <summary>DE SHAW DARK<para>http://www.deshaw.com</para></summary>
        Shad,

        /// <summary>TWO SIGMA SECURITIES, LLC<para>http://www.twosigmasecurities.com</para></summary>
        Soho,

        /// <summary>E-EXCHANGE</summary>
        Sstx,

        /// <summary>TERAEXCHANGE<para>http://www.teraexchange.com</para></summary>
        Tera,

        /// <summary>TFS GREEN UNITED STATES GREEN MARKETS<para>http://www.tradition.com</para></summary>
        Tfsu,

        /// <summary>THEMIS TRADING LLC<para>http://www.themistrading.com</para></summary>
        Them,

        /// <summary>THOMSON REUTERS (SEF) LLC<para>http://www.fxall.com/solutions--capabilities/regulatory-solutions/thomson-reuters-sef</para></summary>
        Thre,

        /// <summary>TRUMID ATS<para>http://www.trumid.com</para></summary>
        Tmid,

        /// <summary>TP SEF, INC<para>http://www.tullettprebon.com</para></summary>
        Tpse,

        /// <summary>TRACK ECN<para>http://www.trackecn.com</para></summary>
        Trck,

        /// <summary>TRUEEX LLC<para>http://www.trueex.com</para></summary>
        Trux,

        /// <summary>TRUEEX LLC - DESIGNATED CONTRACT MARKET (DMC)<para>http://www.trueex.com</para></summary>
        Tru1,

        /// <summary>TRUEEX LLC - SEF (SWAP EXECUTION FACILITY)<para>http://www.trueex.com</para></summary>
        Tru2,

        /// <summary>TRADEWEB LLC<para>http://www.tradeweb.com</para></summary>
        Trwb,

        /// <summary>TRADEWEB DIRECT LLC<para>http://www.tradeweb.com</para></summary>
        Bndd,

        /// <summary>TW SEF LLC<para>http://www.tradeweb.com</para></summary>
        Twsf,

        /// <summary>DW SEF LLC<para>http://www.tradeweb.com</para></summary>
        Dwsf,

        /// <summary>TRADITION SECURITIES AND DERIVATIVES INC<para>http://www.tradition.com</para></summary>
        Tsad,

        /// <summary>TRIPLESHOT<para>http://www.tripleshot.com</para></summary>
        Tsbx,

        /// <summary>TRADITION SEF<para>http://www.traditionsef.com</para></summary>
        Tsef,

        /// <summary>UBS ATS<para>http://www.ubs.com/ats</para></summary>
        Ubsa,

        /// <summary>UBS PIN (UBS PRICE IMPROVEMENT NETWORK)<para>http://www.ubs.com/ats</para></summary>
        Ubsp,

        /// <summary>VERTICAL<para>http://www.vertgrp.com</para></summary>
        Vert,

        /// <summary>VIRTU FINANCIAL CAPITAL MARKETS LLC<para>http://www.virtu.com</para></summary>
        Vfcm,

        /// <summary>VIRTU FINANCIAL BD<para>http://www.virtu.com</para></summary>
        Virt,

        /// <summary>WEEDEN AND CO MARKETS<para>http://www.weedenco.com</para></summary>
        Weed,

        /// <summary>WEEDEN ATS<para>http://www.weedenco.com</para></summary>
        Xwee,

        /// <summary>WELLS FARGO LIQUIDITY CROSS ATS<para>http://www.wellsfargo.com</para></summary>
        Welx,

        /// <summary>WALL STREET ACCESS<para>http://www.wsaccess.com</para></summary>
        Wsag,

        /// <summary>AUTOMATED EQUITY FINANCE MARKETS<para>http://www.quadriserv.com</para></summary>
        Xaqs,

        /// <summary>BOSTON OPTIONS EXCHANGE<para>http://www.bostonoptions.com</para></summary>
        Xbox,

        /// <summary>CHICAGO BOARD OPTIONS EXCHANGE<para>http://www.cboe.com</para></summary>
        Xcbo,

        /// <summary>C2 OPTIONS EXCHANGE INC</summary>
        C2Ox,

        /// <summary>CBOE STOCK EXCHANGE<para>http://www.cbsx.com</para></summary>
        Cbsx,

        /// <summary>CBOE FUTURES EXCHANGE<para>http://www.cfe.cboe.com</para></summary>
        Xcbf,

        /// <summary>CHICAGO BOARD OF TRADE<para>http://www.cbot.com</para></summary>
        Xcbt,

        /// <summary>CHICAGO BOARD OF TRADE (FLOOR)<para>http://www.cbot.com</para></summary>
        Fcbt,

        /// <summary>KANSAS CITY BOARD OF TRADE<para>http://www.kcbt.com</para></summary>
        Xkbt,

        /// <summary>CANTOR FINANCIAL FUTURES EXCHANGE<para>http://www.cantor.com</para></summary>
        Xcff,

        /// <summary>CHICAGO STOCK EXCHANGE, INC<para>http://www.chx.com</para></summary>
        Xchi,

        /// <summary>NATIONAL STOCK EXCHANGE, INC<para>http://www.nsx.com</para></summary>
        Xcis,

        /// <summary>CHICAGO MERCANTILE EXCHANGE<para>http://www.cme.com</para></summary>
        Xcme,

        /// <summary>CHICAGO MERCANTILE EXCHANGE (FLOOR)<para>http://www.cme.com</para></summary>
        Fcme,

        /// <summary>CME GLOBEX<para>http://www.cme.com</para></summary>
        Glbx,

        /// <summary>INTERNATIONAL MONETARY MARKET<para>http://www.cme.com</para></summary>
        Ximm,

        /// <summary>INDEX AND OPTIONS MARKET<para>http://www.cme.com</para></summary>
        Xiom,

        /// <summary>CME SWAPS MARKETS (NYMEX)<para>http://www.cmegroup.com</para></summary>
        Nyms,

        /// <summary>CME SWAPS MARKETS (CME)<para>http://www.cmegroup.com</para></summary>
        Cmes,

        /// <summary>CME SWAPS MARKETS (CBOT)<para>http://www.cmegroup.com</para></summary>
        Cbts,

        /// <summary>CME SWAPS MARKETS (COMEX)<para>http://www.cmegroup.com</para></summary>
        Cecs,

        /// <summary>CURRENEX<para>http://www.currenex.com</para></summary>
        Xcur,

        /// <summary>ELX<para>http://www.elxfutures.com</para></summary>
        Xelx,

        /// <summary>FINANCIALCONTENT INDEXES<para>http://www.financialcontent.com</para></summary>
        Xfci,

        /// <summary>GLOBALCLEAR MERCANTILE EXCHANGE<para>http://www.gmegroup.us</para></summary>
        Xgmx,

        /// <summary>INSTINET<para>http://www.instinet.com</para></summary>
        Xins,

        /// <summary>INSTINET BLX<para>http://www.instinet.com</para></summary>
        Iblx,

        /// <summary>INSTINET CBX (US)<para>http://www.instinet.com</para></summary>
        Icbx,

        /// <summary>INSTINET VWAP CROSS<para>http://www.instinet.com</para></summary>
        Icro,

        /// <summary>INSTINET IDX<para>http://www.instinet.com</para></summary>
        Iidx,

        /// <summary>INSTINET RETAIL CBX<para>http://www.instinet.com</para></summary>
        Rcbx,

        /// <summary>MOC CROSS<para>http://www.instinet.com</para></summary>
        Mocx,

        /// <summary>INTERNATIONAL SECURITIES EXCHANGE, LLC<para>http://www.ise.com</para></summary>
        Xisx,

        /// <summary>INTERNATIONAL SECURITIES EXCHANGE, LLC -  ALTERNATIVE MARKETS<para>http://www.ise.com</para></summary>
        Xisa,

        /// <summary>INTERNATIONAL SECURITIES EXCHANGE, LLC - EQUITIES<para>http://www.ise.com</para></summary>
        Xise,

        /// <summary>ISE MERCURY, LLC<para>http://www.ise.com</para></summary>
        Mcry,

        /// <summary>ISE GEMINI EXCHANGE<para>http://www.ise.com</para></summary>
        Gmni,

        /// <summary>MERCHANTS EXCHANGE</summary>
        Xmer,

        /// <summary>MINNEAPOLIS GRAIN EXCHANGE<para>http://www.mgex.com</para></summary>
        Xmge,

        /// <summary>NASDAQ - ALL MARKETS<para>http://www.nasdaq.com</para></summary>
        Xnas,

        /// <summary>NASDAQ OMX BX OPTIONS<para>http://www.nasdaqomxtrader.com</para></summary>
        Xbxo,

        /// <summary>NASDAQ OMX BX DARK<para>http://www.nasdaqomxtrader.com</para></summary>
        Bosd,

        /// <summary>NSDQ DARK<para>http://www.nasdaq.com</para></summary>
        Nasd,

        /// <summary>BRUT ECN<para>http://www.nasdaqtrader.com</para></summary>
        Xbrt,

        /// <summary>NASDAQ CAPITAL MARKET<para>http://www.nasdaq.com</para></summary>
        Xncm,

        /// <summary>NASDAQ OPTIONS MARKET<para>http://www.nasdaq.com</para></summary>
        Xndq,

        /// <summary>NASDAQ/NGS (GLOBAL SELECT MARKET)<para>http://www.nasdaq.com</para></summary>
        Xngs,

        /// <summary>NASDAQ INTERMARKET<para>http://www.nasdaq.com</para></summary>
        Xnim,

        /// <summary>NASDAQ/NMS (GLOBAL MARKET)<para>http://www.nasdaq.com</para></summary>
        Xnms,

        /// <summary>NASDAQ OMX FUTURES EXCHANGE<para>http://www.nasdaqtrader.com</para></summary>
        Xpbt,

        /// <summary>NASDAQ OMX PHLX<para>http://www.phlx.com</para></summary>
        Xphl,

        /// <summary>PHILADELPHIA OPTIONS EXCHANGE<para>http://www.phlx.com</para></summary>
        Xpho,

        /// <summary>PORTAL<para>http://www.nasdaqportalmarket.com</para></summary>
        Xpor,

        /// <summary>NASDAQ OMX PSX<para>http://www.nasdaqtrader.com</para></summary>
        Xpsx,

        /// <summary>NASDAQ OMX ESPEED<para>http://www.nasdaqomx.com</para></summary>
        Espd,

        /// <summary>NASDAQ OMX BX<para>http://www.nasdaqomxtrader.com</para></summary>
        Xbos,

        /// <summary>NEW YORK MERCANTILE EXCHANGE<para>http://www.nymex.com</para></summary>
        Xnym,

        /// <summary>COMMODITIES EXCHANGE CENTER<para>http://www.nymex.com</para></summary>
        Xcec,

        /// <summary>NEW YORK MERCANTILE EXCHANGE - OTC MARKETS<para>http://www.nymex.com</para></summary>
        Xnye,

        /// <summary>NEW YORK MERCANTILE EXCHANGE - ENERGY MARKETS<para>http://www.nymexonlchclearnet.com</para></summary>
        Xnyl,

        /// <summary>NEW YORK STOCK EXCHANGE, INC<para>http://www.nyse.com</para></summary>
        Xnys,

        /// <summary>NYSE ALTERNEXT DARK<para>http://www.euronext.com/landing/equitiesop-21363-en.html</para></summary>
        Aldp,

        /// <summary>NYSE AMEX OPTIONS<para>http://www.nyse.com</para></summary>
        Amxo,

        /// <summary>ARCA DARK<para>http://www.nyse.com</para></summary>
        Arcd,

        /// <summary>NYSE ARCA OPTIONS<para>http://www.nyse.com</para></summary>
        Arco,

        /// <summary>NYSE ARCA<para>http://www.nyse.com</para></summary>
        Arcx,

        /// <summary>NYSE DARK<para>http://www.nyse.com</para></summary>
        Nysd,

        /// <summary>NYSE MKT LLC<para>http://www.nyse.com</para></summary>
        Xase,

        /// <summary>NYSE LIFFE<para>http://www.nyse.com</para></summary>
        Xnli,

        /// <summary>ONECHICAGO, LLC<para>http://www.onechicago.com</para></summary>
        Xoch,

        /// <summary>OTCBB<para>http://www.otcbb.com</para></summary>
        Xotc,

        /// <summary>SWAPEX, LLC<para>http://www.swapex.com</para></summary>
        Xsef,

        /// <summary>OFF-EXCHANGE TRANSACTIONS - LISTED AND UNLISTED INSTRUMENTS</summary>
        Bilt,

        /// <summary>MIC TO USE FOR OFF-EXCHANGE TRANSACTIONS IN LISTED INSTRUMENTS</summary>
        Xoff,

        /// <summary>NO MARKET (EG UNLISTED)</summary>
        Xxxx,
    }
}
