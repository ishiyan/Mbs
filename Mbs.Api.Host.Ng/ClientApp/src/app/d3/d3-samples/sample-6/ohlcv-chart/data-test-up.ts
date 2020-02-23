import { Scalar } from './scalar';

// kind: 3
// name: up-bb(stdev.p(20,c),2,sma(20,c))
// description: Upper Band of Bollinger Bands bb(stdev.p(20,c),2,sma(20,c))
export const dataTestUp: Scalar[] = [
    /*{ time: new Date(2011, 0, 3), value: NaN },
    { time: new Date(2011, 0, 4), value: NaN },
    { time: new Date(2011, 0, 5), value: NaN },
    { time: new Date(2011, 0, 6), value: NaN },
    { time: new Date(2011, 0, 7), value: NaN },
    { time: new Date(2011, 0, 10), value: NaN },
    { time: new Date(2011, 0, 11), value: NaN },
    { time: new Date(2011, 0, 12), value: NaN },
    { time: new Date(2011, 0, 13), value: NaN },
    { time: new Date(2011, 0, 14), value: NaN },
    { time: new Date(2011, 0, 17), value: NaN },
    { time: new Date(2011, 0, 18), value: NaN },
    { time: new Date(2011, 0, 19), value: NaN },
    { time: new Date(2011, 0, 20), value: NaN },
    { time: new Date(2011, 0, 21), value: NaN },
    { time: new Date(2011, 0, 24), value: NaN },
    { time: new Date(2011, 0, 25), value: NaN },
    { time: new Date(2011, 0, 26), value: NaN },
    { time: new Date(2011, 0, 27), value: NaN },*/
    { time: new Date(2011, 0, 28), value: 98.07339481321102 },
    { time: new Date(2011, 0, 31), value: 98.25303186576227 },
    { time: new Date(2011, 1, 1), value: 98.23109278733673 },
    { time: new Date(2011, 1, 2), value: 98.5368868280653 },
    { time: new Date(2011, 1, 3), value: 98.8023245852656 },
    { time: new Date(2011, 1, 4), value: 98.91719548842377 },
    { time: new Date(2011, 1, 7), value: 99.01526133298898 },
    { time: new Date(2011, 1, 8), value: 98.86648384728417 },
    { time: new Date(2011, 1, 9), value: 98.58554691242708 },
    { time: new Date(2011, 1, 10), value: 98.48306217345815 },
    { time: new Date(2011, 1, 11), value: 98.14785150968665 },
    { time: new Date(2011, 1, 14), value: 97.1309809204235 },
    { time: new Date(2011, 1, 15), value: 95.64220450381146 },
    { time: new Date(2011, 1, 16), value: 93.33724561595407 },
    { time: new Date(2011, 1, 17), value: 93.22817107096422 },
    { time: new Date(2011, 1, 18), value: 92.88431392718215 },
    { time: new Date(2011, 1, 21), value: 91.9666822349697 },
    { time: new Date(2011, 1, 22), value: 91.7410325712435 },
    { time: new Date(2011, 1, 23), value: 91.43341864505734 },
    { time: new Date(2011, 1, 24), value: 90.56284536249998 },
    { time: new Date(2011, 1, 25), value: 90.02075966970075 },
    { time: new Date(2011, 1, 28), value: 89.7373260990979 },
    { time: new Date(2011, 2, 1), value: 89.52631139595003 },
    { time: new Date(2011, 2, 2), value: 90.03675136279239 },
    { time: new Date(2011, 2, 3), value: 90.45719268020328 },
    { time: new Date(2011, 2, 4), value: 91.18807612059643 },
    { time: new Date(2011, 2, 7), value: 91.4145935847659 },
    { time: new Date(2011, 2, 8), value: 92.02620070581253 },
    { time: new Date(2011, 2, 9), value: 91.99394209171125 },
    { time: new Date(2011, 2, 10), value: 92.47847783479686 },
    { time: new Date(2011, 2, 11), value: 92.82544473552284 },
    { time: new Date(2011, 2, 14), value: 92.92078046313236 },
    { time: new Date(2011, 2, 15), value: 93.00937483783025 },
    { time: new Date(2011, 2, 16), value: 93.10362307427948 },
    { time: new Date(2011, 2, 17), value: 93.13553208694782 },
    { time: new Date(2011, 2, 18), value: 93.22543774539244 },
    { time: new Date(2011, 2, 21), value: 93.22221912257919 },
    { time: new Date(2011, 2, 22), value: 93.20041376115665 },
    { time: new Date(2011, 2, 23), value: 93.19413477986359 },
    { time: new Date(2011, 2, 24), value: 93.30853821314001 },
    { time: new Date(2011, 2, 25), value: 93.3896485161806 },
    { time: new Date(2011, 2, 28), value: 93.297994425625 },
    { time: new Date(2011, 2, 29), value: 93.3271163701221 },
    { time: new Date(2011, 2, 30), value: 93.71463882586548 },
    { time: new Date(2011, 2, 31), value: 93.97774767392492 },
    { time: new Date(2011, 3, 1), value: 94.36642539684102 },
    { time: new Date(2011, 3, 4), value: 94.82897460531153 },
    { time: new Date(2011, 3, 5), value: 95.1123481873987 },
    { time: new Date(2011, 3, 6), value: 95.3816065903525 },
    { time: new Date(2011, 3, 7), value: 95.27994287065431 },
    { time: new Date(2011, 3, 8), value: 95.20632754049555 },
    { time: new Date(2011, 3, 11), value: 95.19759585699548 },
    { time: new Date(2011, 3, 12), value: 95.19924924185287 },
    { time: new Date(2011, 3, 13), value: 95.28131893584948 },
    { time: new Date(2011, 3, 14), value: 95.17021635172641 },
    { time: new Date(2011, 3, 15), value: 94.88795316808947 },
    { time: new Date(2011, 3, 18), value: 96.25725838538646 },
    { time: new Date(2011, 3, 19), value: 98.12522201185921 },
    { time: new Date(2011, 3, 20), value: 101.13336272283368 },
    { time: new Date(2011, 3, 21), value: 103.84700153489479 },
    { time: new Date(2011, 3, 22), value: 105.29572572102212 },
    { time: new Date(2011, 3, 25), value: 106.54152226642769 },
    { time: new Date(2011, 3, 26), value: 108.03428494532893 },
    { time: new Date(2011, 3, 27), value: 109.6550045250374 },
    { time: new Date(2011, 3, 28), value: 111.07338513298093 },
    { time: new Date(2011, 3, 29), value: 112.33775408864022 },
    { time: new Date(2011, 4, 2), value: 113.25733924458953 },
    { time: new Date(2011, 4, 3), value: 114.73245086414165 },
    { time: new Date(2011, 4, 4), value: 116.14196427641313 },
    { time: new Date(2011, 4, 5), value: 117.49153903394571 },
    { time: new Date(2011, 4, 6), value: 118.97317437897037 },
    { time: new Date(2011, 4, 9), value: 122.30415772822455 },
    { time: new Date(2011, 4, 10), value: 123.8017179743232 },
    { time: new Date(2011, 4, 11), value: 124.19139466152023 },
    { time: new Date(2011, 4, 12), value: 124.16111011162397 },
    { time: new Date(2011, 4, 13), value: 123.16634684277696 },
    { time: new Date(2011, 4, 16), value: 123.39078085772358 },
    { time: new Date(2011, 4, 17), value: 123.49771126102944 },
    { time: new Date(2011, 4, 18), value: 123.57011816965954 },
    { time: new Date(2011, 4, 19), value: 123.58494903870756 },
    { time: new Date(2011, 4, 20), value: 124.00800468556471 },
    { time: new Date(2011, 4, 23), value: 123.9272636200093 },
    { time: new Date(2011, 4, 24), value: 123.91886549941601 },
    { time: new Date(2011, 4, 25), value: 123.70926255178722 },
    { time: new Date(2011, 4, 26), value: 123.46922819368787 },
    { time: new Date(2011, 4, 27), value: 123.10377081185287 },
    { time: new Date(2011, 4, 30), value: 122.45937808830257 },
    { time: new Date(2011, 4, 31), value: 122.83932458591562 },
    { time: new Date(2011, 5, 1), value: 122.63439995476062 },
    { time: new Date(2011, 5, 2), value: 122.49923662294108 },
    { time: new Date(2011, 5, 3), value: 122.44005811780364 },
    { time: new Date(2011, 5, 6), value: 121.2359268012998 },
    { time: new Date(2011, 5, 7), value: 120.75018765436539 },
    { time: new Date(2011, 5, 8), value: 120.41069495641798 },
    { time: new Date(2011, 5, 9), value: 120.73580471474953 },
    { time: new Date(2011, 5, 10), value: 121.13788663991731 },
    { time: new Date(2011, 5, 13), value: 121.77541918533636 },
    { time: new Date(2011, 5, 14), value: 123.38371248910526 },
    { time: new Date(2011, 5, 15), value: 124.24994120771893 },
    { time: new Date(2011, 5, 16), value: 124.67655023479305 },
    { time: new Date(2011, 5, 17), value: 125.21868306191517 },
    { time: new Date(2011, 5, 20), value: 125.86509652984537 },
    { time: new Date(2011, 5, 21), value: 126.3474984003089 },
    { time: new Date(2011, 5, 22), value: 126.80241195288554 },
    { time: new Date(2011, 5, 23), value: 128.3308824248464 },
    { time: new Date(2011, 5, 24), value: 129.88805111643475 },
    { time: new Date(2011, 5, 27), value: 131.6808475950931 },
    { time: new Date(2011, 5, 28), value: 132.97880178538463 },
    { time: new Date(2011, 5, 29), value: 134.40920310472328 },
    { time: new Date(2011, 5, 30), value: 135.86410057548835 },
    { time: new Date(2011, 6, 1), value: 137.6584653785181 },
    { time: new Date(2011, 6, 4), value: 139.02665286996464 },
    { time: new Date(2011, 6, 5), value: 140.14894728473314 },
    { time: new Date(2011, 6, 6), value: 140.80918063869478 },
    { time: new Date(2011, 6, 7), value: 141.540222487973 },
    { time: new Date(2011, 6, 8), value: 142.0107524318674 },
    { time: new Date(2011, 6, 11), value: 142.10578135915523 },
    { time: new Date(2011, 6, 12), value: 142.0242388537986 },
    { time: new Date(2011, 6, 13), value: 141.82374355012502 },
    { time: new Date(2011, 6, 14), value: 141.73722874742666 },
    { time: new Date(2011, 6, 15), value: 141.5295690420867 },
    { time: new Date(2011, 6, 18), value: 141.5424678619271 },
    { time: new Date(2011, 6, 19), value: 141.22369454976675 },
    { time: new Date(2011, 6, 20), value: 141.02395827595234 },
    { time: new Date(2011, 6, 21), value: 141.16559038056513 },
    { time: new Date(2011, 6, 22), value: 141.20947123758395 },
    { time: new Date(2011, 6, 25), value: 141.40058052977167 },
    { time: new Date(2011, 6, 26), value: 141.8740805332405 },
    { time: new Date(2011, 6, 27), value: 142.09189415621944 },
    { time: new Date(2011, 6, 28), value: 141.6268365995072 },
    { time: new Date(2011, 6, 29), value: 140.4900725102626 },
    { time: new Date(2011, 7, 1), value: 139.1127117432687 },
    { time: new Date(2011, 7, 2), value: 137.61665767137077 },
    { time: new Date(2011, 7, 3), value: 135.80892033261048 },
    { time: new Date(2011, 7, 4), value: 133.9655972961046 },
    { time: new Date(2011, 7, 5), value: 131.689198285994 },
    { time: new Date(2011, 7, 8), value: 129.82085834854573 },
    { time: new Date(2011, 7, 9), value: 129.87096579733668 },
    { time: new Date(2011, 7, 10), value: 129.1395651406236 },
    { time: new Date(2011, 7, 11), value: 129.10096142791926 },
    { time: new Date(2011, 7, 12), value: 128.9766655460237 },
    { time: new Date(2011, 7, 15), value: 129.06027220446435 },
    { time: new Date(2011, 7, 16), value: 128.7393979182077 },
    { time: new Date(2011, 7, 17), value: 127.92560132413666 },
    { time: new Date(2011, 7, 18), value: 127.68421013123188 },
    { time: new Date(2011, 7, 19), value: 127.45714784363781 },
    { time: new Date(2011, 7, 22), value: 127.49936707442542 },
    { time: new Date(2011, 7, 23), value: 127.52319445277405 },
    { time: new Date(2011, 7, 24), value: 127.77189423743346 },
    { time: new Date(2011, 7, 25), value: 128.01942904957343 },
    { time: new Date(2011, 7, 26), value: 128.8357660440261 },
    { time: new Date(2011, 7, 29), value: 130.3760009282724 },
    { time: new Date(2011, 7, 30), value: 131.06008572077874 },
    { time: new Date(2011, 7, 31), value: 132.90757136717522 },
    { time: new Date(2011, 8, 1), value: 134.36568471589823 },
    { time: new Date(2011, 8, 2), value: 135.11428232553038 },
    { time: new Date(2011, 8, 5), value: 135.88742825640327 },
    { time: new Date(2011, 8, 6), value: 136.29701423501092 },
    { time: new Date(2011, 8, 7), value: 136.54576408727291 },
    { time: new Date(2011, 8, 8), value: 136.48125726066004 },
    { time: new Date(2011, 8, 9), value: 136.53139635377812 },
    { time: new Date(2011, 8, 12), value: 136.51594335836018 },
    { time: new Date(2011, 8, 13), value: 136.3092334136638 },
    { time: new Date(2011, 8, 14), value: 136.3477451004074 },
    { time: new Date(2011, 8, 15), value: 136.2207158696155 },
    { time: new Date(2011, 8, 16), value: 136.29577723225114 },
    { time: new Date(2011, 8, 19), value: 136.27163589911314 },
    { time: new Date(2011, 8, 20), value: 136.70254594875308 },
    { time: new Date(2011, 8, 21), value: 136.9426877527115 },
    { time: new Date(2011, 8, 22), value: 137.52842901791772 },
    { time: new Date(2011, 8, 23), value: 137.57809231893108 },
    { time: new Date(2011, 8, 26), value: 137.03183184361248 },
    { time: new Date(2011, 8, 27), value: 136.72426157519362 },
    { time: new Date(2011, 8, 28), value: 135.77406182053863 },
    { time: new Date(2011, 8, 29), value: 134.78717723065486 },
    { time: new Date(2011, 8, 30), value: 133.935913037652 },
    { time: new Date(2011, 9, 3), value: 133.102867582176 },
    { time: new Date(2011, 9, 4), value: 132.97913895276056 },
    { time: new Date(2011, 9, 5), value: 132.35089582489263 },
    { time: new Date(2011, 9, 6), value: 132.0845077897933 },
    { time: new Date(2011, 9, 7), value: 130.70485707931311 },
    { time: new Date(2011, 9, 10), value: 129.61975905039938 },
    { time: new Date(2011, 9, 11), value: 128.65172547919244 },
    { time: new Date(2011, 9, 12), value: 130.4895905264651 },
    { time: new Date(2011, 9, 13), value: 130.24280286179865 },
    { time: new Date(2011, 9, 14), value: 129.78607698136958 },
    { time: new Date(2011, 9, 17), value: 128.58263941879602 },
    { time: new Date(2011, 9, 18), value: 127.85941375012835 },
    { time: new Date(2011, 9, 19), value: 126.4221259716512 },
    { time: new Date(2011, 9, 20), value: 125.11093516682152 },
    { time: new Date(2011, 9, 21), value: 123.20841350908138 },
    { time: new Date(2011, 9, 24), value: 120.50770870178462 },
    { time: new Date(2011, 9, 25), value: 118.0450142499157 },
    { time: new Date(2011, 9, 26), value: 116.00589637488106 },
    { time: new Date(2011, 9, 27), value: 114.3393260091979 },
    { time: new Date(2011, 9, 28), value: 111.88900962955637 },
    { time: new Date(2011, 9, 31), value: 110.15732825702426 },
    { time: new Date(2011, 10, 1), value: 109.30241949732448 },
    { time: new Date(2011, 10, 2), value: 107.89384118344489 },
    { time: new Date(2011, 10, 3), value: 106.06864335026192 },
    { time: new Date(2011, 10, 4), value: 104.12068826011719 },
    { time: new Date(2011, 10, 7), value: 101.65928563613656 },
    { time: new Date(2011, 10, 8), value: 98.09506860096225 },
    { time: new Date(2011, 10, 9), value: 98.45244914383379 },
    { time: new Date(2011, 10, 10), value: 100.68316606118188 },
    { time: new Date(2011, 10, 11), value: 103.63989691646572 },
    { time: new Date(2011, 10, 14), value: 105.34361850543807 },
    { time: new Date(2011, 10, 15), value: 106.43736622354038 },
    { time: new Date(2011, 10, 16), value: 107.50921556297453 },
    { time: new Date(2011, 10, 17), value: 108.22438352058634 },
    { time: new Date(2011, 10, 18), value: 108.77705319508435 },
    { time: new Date(2011, 10, 21), value: 109.3396659232368 },
    { time: new Date(2011, 10, 22), value: 110.09260973595103 },
    { time: new Date(2011, 10, 23), value: 111.8752975598597 },
    { time: new Date(2011, 10, 24), value: 114.17336747776893 },
    { time: new Date(2011, 10, 25), value: 116.44588739209613 },
    { time: new Date(2011, 10, 28), value: 118.65105263853565 },
    { time: new Date(2011, 10, 29), value: 119.69948519323904 },
    { time: new Date(2011, 10, 30), value: 119.85652168409344 },
    { time: new Date(2011, 11, 1), value: 120.00862627344905 },
    { time: new Date(2011, 11, 2), value: 119.7253589391771 },
    { time: new Date(2011, 11, 5), value: 119.11075018797786 },
    { time: new Date(2011, 11, 6), value: 118.1234224907404 },
    { time: new Date(2011, 11, 7), value: 117.61587392906017 },
    { time: new Date(2011, 11, 8), value: 117.61104745617811 },
    { time: new Date(2011, 11, 9), value: 117.72335278469933 },
    { time: new Date(2011, 11, 12), value: 117.72703959174594 },
    { time: new Date(2011, 11, 13), value: 117.67003609231897 },
    { time: new Date(2011, 11, 14), value: 117.66374757924606 },
    { time: new Date(2011, 11, 15), value: 117.5541357689087 },
    { time: new Date(2011, 11, 16), value: 117.20585174035142 },
    { time: new Date(2011, 11, 19), value: 116.800283012573 },
    { time: new Date(2011, 11, 20), value: 116.59999170812226 }
];
