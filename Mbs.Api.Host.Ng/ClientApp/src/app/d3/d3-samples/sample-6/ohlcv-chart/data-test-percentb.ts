import { Scalar } from './scalar';

// kind: 4
// name: %b(c)-bb(stdev.p(20,c),2,sma(20,c))
// description: %B(c) of Bollinger Bands bb(stdev.p(20,c),2,sma(20,c))
export const dataTestPercentB: Scalar[] = [
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
    { time: new Date(2011, 0, 28), value: 0.20901483689436764 },
    { time: new Date(2011, 0, 31), value: 0.10497215382837728 },
    { time: new Date(2011, 1, 1), value: 0.09440082559316576 },
    { time: new Date(2011, 1, 2), value: -0.03569383806490329 },
    { time: new Date(2011, 1, 3), value: -0.051864985555943266 },
    { time: new Date(2011, 1, 4), value: 0.05304964511168433 },
    { time: new Date(2011, 1, 7), value: 0.00908668279016042 },
    { time: new Date(2011, 1, 8), value: 0.2122472877398869 },
    { time: new Date(2011, 1, 9), value: 0.48426934098979557 },
    { time: new Date(2011, 1, 10), value: 0.3380181045870365 },
    { time: new Date(2011, 1, 11), value: 0.34778428367171055 },
    { time: new Date(2011, 1, 14), value: 0.3130777559670157 },
    { time: new Date(2011, 1, 15), value: 0.44444230282910924 },
    { time: new Date(2011, 1, 16), value: 0.3725967624101678 },
    { time: new Date(2011, 1, 17), value: 0.641019411655065 },
    { time: new Date(2011, 1, 18), value: 0.6136987365993096 },
    { time: new Date(2011, 1, 21), value: 0.5001227190881724 },
    { time: new Date(2011, 1, 22), value: 0.5058433685457633 },
    { time: new Date(2011, 1, 23), value: 0.3307966398592606 },
    { time: new Date(2011, 1, 24), value: 0.2760343897575688 },
    { time: new Date(2011, 1, 25), value: 0.2611954821586153 },
    { time: new Date(2011, 1, 28), value: 0.22896337045216048 },
    { time: new Date(2011, 2, 1), value: 0.49934718728675825 },
    { time: new Date(2011, 2, 2), value: 0.9017918006126633 },
    { time: new Date(2011, 2, 3), value: 0.8846574551507279 },
    { time: new Date(2011, 2, 4), value: 0.9902242797679732 },
    { time: new Date(2011, 2, 7), value: 0.926456059361372 },
    { time: new Date(2011, 2, 8), value: 0.938681302183755 },
    { time: new Date(2011, 2, 9), value: 0.6855229753307118 },
    { time: new Date(2011, 2, 10), value: 0.8525496787344659 },
    { time: new Date(2011, 2, 11), value: 0.774138529549826 },
    { time: new Date(2011, 2, 14), value: 0.6151004587583697 },
    { time: new Date(2011, 2, 15), value: 0.5854029236364972 },
    { time: new Date(2011, 2, 16), value: 0.1563402582923164 },
    { time: new Date(2011, 2, 17), value: 0.12954729422225672 },
    { time: new Date(2011, 2, 18), value: 0.10923283414811896 },
    { time: new Date(2011, 2, 21), value: 0.2959436073070833 },
    { time: new Date(2011, 2, 22), value: 0.3767122695867712 },
    { time: new Date(2011, 2, 23), value: 0.41445663662966076 },
    { time: new Date(2011, 2, 24), value: 0.6271170914526543 },
    { time: new Date(2011, 2, 25), value: 0.6375107307808895 },
    { time: new Date(2011, 2, 28), value: 0.5603538124818478 },
    { time: new Date(2011, 2, 29), value: 0.5352836996306088 },
    { time: new Date(2011, 2, 30), value: 0.8397101873280468 },
    { time: new Date(2011, 2, 31), value: 0.7788612876348414 },
    { time: new Date(2011, 3, 1), value: 0.9051171075538145 },
    { time: new Date(2011, 3, 4), value: 0.8929666136616169 },
    { time: new Date(2011, 3, 5), value: 0.8472217784329403 },
    { time: new Date(2011, 3, 6), value: 0.7200224113371487 },
    { time: new Date(2011, 3, 7), value: 0.5930644033462003 },
    { time: new Date(2011, 3, 8), value: 0.5726644250787422 },
    { time: new Date(2011, 3, 11), value: 0.5101304038941499 },
    { time: new Date(2011, 3, 12), value: 0.24586548236257547 },
    { time: new Date(2011, 3, 13), value: 0.11981979863164523 },
    { time: new Date(2011, 3, 14), value: 0.21813032323098516 },
    { time: new Date(2011, 3, 15), value: 0.27121506524912964 },
    { time: new Date(2011, 3, 18), value: 1.0721127668780515 },
    { time: new Date(2011, 3, 19), value: 1.108814034336359 },
    { time: new Date(2011, 3, 20), value: 1.1882449293159558 },
    { time: new Date(2011, 3, 21), value: 1.0899218222054103 },
    { time: new Date(2011, 3, 22), value: 0.890442971142995 },
    { time: new Date(2011, 3, 25), value: 0.8446787567764634 },
    { time: new Date(2011, 3, 26), value: 0.8772037859532722 },
    { time: new Date(2011, 3, 27), value: 0.8816785059254868 },
    { time: new Date(2011, 3, 28), value: 0.837499680829075 },
    { time: new Date(2011, 3, 29), value: 0.8068020047799052 },
    { time: new Date(2011, 4, 2), value: 0.7399480989881093 },
    { time: new Date(2011, 4, 3), value: 0.8234733738448392 },
    { time: new Date(2011, 4, 4), value: 0.808540196152983 },
    { time: new Date(2011, 4, 5), value: 0.8074305151845514 },
    { time: new Date(2011, 4, 6), value: 0.8316416015084608 },
    { time: new Date(2011, 4, 9), value: 1.0173037777133558 },
    { time: new Date(2011, 4, 10), value: 0.8949648006545985 },
    { time: new Date(2011, 4, 11), value: 0.8529622601404002 },
    { time: new Date(2011, 4, 12), value: 0.8534378490463296 },
    { time: new Date(2011, 4, 13), value: 0.8154485999563253 },
    { time: new Date(2011, 4, 16), value: 0.741116433364054 },
    { time: new Date(2011, 4, 17), value: 0.6746083672442585 },
    { time: new Date(2011, 4, 18), value: 0.5318720091020137 },
    { time: new Date(2011, 4, 19), value: 0.47094029398279785 },
    { time: new Date(2011, 4, 20), value: 0.7531935320743665 },
    { time: new Date(2011, 4, 23), value: 0.644764672833126 },
    { time: new Date(2011, 4, 24), value: 0.6257312107116537 },
    { time: new Date(2011, 4, 25), value: 0.4189391377772072 },
    { time: new Date(2011, 4, 26), value: 0.48565868676938556 },
    { time: new Date(2011, 4, 27), value: 0.4184834301992053 },
    { time: new Date(2011, 4, 30), value: 0.5708555192820388 },
    { time: new Date(2011, 4, 31), value: 0.8400368760993231 },
    { time: new Date(2011, 5, 1), value: 0.5539536336108266 },
    { time: new Date(2011, 5, 2), value: 0.5622837319494013 },
    { time: new Date(2011, 5, 3), value: 0.4104149580132643 },
    { time: new Date(2011, 5, 6), value: 0.3500354772507259 },
    { time: new Date(2011, 5, 7), value: 0.48001857071310605 },
    { time: new Date(2011, 5, 8), value: 0.5321711870631327 },
    { time: new Date(2011, 5, 9), value: 0.9955146666743285 },
    { time: new Date(2011, 5, 10), value: 0.9121617426102439 },
    { time: new Date(2011, 5, 13), value: 0.9118702737583495 },
    { time: new Date(2011, 5, 14), value: 1.098322890562315 },
    { time: new Date(2011, 5, 15), value: 0.9392258889553033 },
    { time: new Date(2011, 5, 16), value: 0.8768194460795333 },
    { time: new Date(2011, 5, 17), value: 0.8196080189909006 },
    { time: new Date(2011, 5, 20), value: 0.8208067148740033 },
    { time: new Date(2011, 5, 21), value: 0.7576465846115895 },
    { time: new Date(2011, 5, 22), value: 0.8571329463898166 },
    { time: new Date(2011, 5, 23), value: 1.054765563563885 },
    { time: new Date(2011, 5, 24), value: 1.0614638549521298 },
    { time: new Date(2011, 5, 27), value: 1.0283842537754841 },
    { time: new Date(2011, 5, 28), value: 0.9083786412274563 },
    { time: new Date(2011, 5, 29), value: 0.9299805305769366 },
    { time: new Date(2011, 5, 30), value: 0.9224880459111368 },
    { time: new Date(2011, 6, 1), value: 0.9890475462380157 },
    { time: new Date(2011, 6, 4), value: 0.9528635675303126 },
    { time: new Date(2011, 6, 5), value: 0.9121172612031453 },
    { time: new Date(2011, 6, 6), value: 0.8576348317848626 },
    { time: new Date(2011, 6, 7), value: 0.7899546577169435 },
    { time: new Date(2011, 6, 8), value: 0.7623011171313548 },
    { time: new Date(2011, 6, 11), value: 0.6754959953635463 },
    { time: new Date(2011, 6, 12), value: 0.38847688134616765 },
    { time: new Date(2011, 6, 13), value: 0.4052201376296243 },
    { time: new Date(2011, 6, 14), value: 0.1609446377422404 },
    { time: new Date(2011, 6, 15), value: 0.19049785694527274 },
    { time: new Date(2011, 6, 18), value: 0.10387949637061726 },
    { time: new Date(2011, 6, 19), value: 0.23934983077771363 },
    { time: new Date(2011, 6, 20), value: 0.3313583250752768 },
    { time: new Date(2011, 6, 21), value: 0.19332799317597016 },
    { time: new Date(2011, 6, 22), value: 0.23170715675738993 },
    { time: new Date(2011, 6, 25), value: 0.11266622110099252 },
    { time: new Date(2011, 6, 26), value: 0.05014656661782644 },
    { time: new Date(2011, 6, 27), value: 0.07695568867254562 },
    { time: new Date(2011, 6, 28), value: 0.28288327678105474 },
    { time: new Date(2011, 6, 29), value: 0.315582772489658 },
    { time: new Date(2011, 7, 1), value: 0.2838894469091405 },
    { time: new Date(2011, 7, 2), value: 0.18623750634446162 },
    { time: new Date(2011, 7, 3), value: 0.38352559514557344 },
    { time: new Date(2011, 7, 4), value: 0.2905804800026028 },
    { time: new Date(2011, 7, 5), value: 0.4394976413920428 },
    { time: new Date(2011, 7, 8), value: 0.7928569106841291 },
    { time: new Date(2011, 7, 9), value: 0.8848598577697402 },
    { time: new Date(2011, 7, 10), value: 0.5190743374072847 },
    { time: new Date(2011, 7, 11), value: 0.4385305074261916 },
    { time: new Date(2011, 7, 12), value: 0.34483693320558084 },
    { time: new Date(2011, 7, 15), value: 0.5820098935204722 },
    { time: new Date(2011, 7, 16), value: 0.3781004994519772 },
    { time: new Date(2011, 7, 17), value: 0.43369148188652124 },
    { time: new Date(2011, 7, 18), value: 0.5042840333941077 },
    { time: new Date(2011, 7, 19), value: 0.6276749863361508 },
    { time: new Date(2011, 7, 22), value: 0.5354149865367521 },
    { time: new Date(2011, 7, 23), value: 0.6634412127221284 },
    { time: new Date(2011, 7, 24), value: 0.938067243649225 },
    { time: new Date(2011, 7, 25), value: 0.7516218748970118 },
    { time: new Date(2011, 7, 26), value: 1.0024853983100699 },
    { time: new Date(2011, 7, 29), value: 1.1370463136957787 },
    { time: new Date(2011, 7, 30), value: 0.974318260902651 },
    { time: new Date(2011, 7, 31), value: 1.125976796691181 },
    { time: new Date(2011, 8, 1), value: 1.039268295495872 },
    { time: new Date(2011, 8, 2), value: 0.8367550212502267 },
    { time: new Date(2011, 8, 5), value: 0.8543978562622178 },
    { time: new Date(2011, 8, 6), value: 0.7603010721913851 },
    { time: new Date(2011, 8, 7), value: 0.6375852572376541 },
    { time: new Date(2011, 8, 8), value: 0.37187325026538937 },
    { time: new Date(2011, 8, 9), value: 0.6223764374534453 },
    { time: new Date(2011, 8, 12), value: 0.43577690053424084 },
    { time: new Date(2011, 8, 13), value: 0.3015761702051664 },
    { time: new Date(2011, 8, 14), value: 0.10519480689954955 },
    { time: new Date(2011, 8, 15), value: 0.27954251830895616 },
    { time: new Date(2011, 8, 16), value: 0.15980084860674879 },
    { time: new Date(2011, 8, 19), value: 0.18885897539401073 },
    { time: new Date(2011, 8, 20), value: 0.02430343643829339 },
    { time: new Date(2011, 8, 21), value: 0.1222135150795015 },
    { time: new Date(2011, 8, 22), value: 0.018238439745986608 },
    { time: new Date(2011, 8, 23), value: 0.16250767659199677 },
    { time: new Date(2011, 8, 26), value: 0.2859551593191012 },
    { time: new Date(2011, 8, 27), value: 0.18794753845712156 },
    { time: new Date(2011, 8, 28), value: 0.09874824018578415 },
    { time: new Date(2011, 8, 29), value: 0.01931126810824622 },
    { time: new Date(2011, 8, 30), value: 0.09767291677978034 },
    { time: new Date(2011, 9, 3), value: -0.027628313085404036 },
    { time: new Date(2011, 9, 4), value: -0.12024827981616404 },
    { time: new Date(2011, 9, 5), value: 0.023680996643052007 },
    { time: new Date(2011, 9, 6), value: 0.1092045060674815 },
    { time: new Date(2011, 9, 7), value: 0.11339677199395275 },
    { time: new Date(2011, 9, 10), value: 0.15307836107725503 },
    { time: new Date(2011, 9, 11), value: 0.18139225221503474 },
    { time: new Date(2011, 9, 12), value: -0.18854402820424232 },
    { time: new Date(2011, 9, 13), value: -0.012980698035161407 },
    { time: new Date(2011, 9, 14), value: 0.050772237871314144 },
    { time: new Date(2011, 9, 17), value: 0.13464823107003968 },
    { time: new Date(2011, 9, 18), value: 0.1172558616544569 },
    { time: new Date(2011, 9, 19), value: 0.19731970776554633 },
    { time: new Date(2011, 9, 20), value: 0.3031978645719514 },
    { time: new Date(2011, 9, 21), value: 0.2859850760822904 },
    { time: new Date(2011, 9, 24), value: 0.25239587323906437 },
    { time: new Date(2011, 9, 25), value: 0.2585345092930117 },
    { time: new Date(2011, 9, 26), value: 0.19407696559451554 },
    { time: new Date(2011, 9, 27), value: 0.17852985965234333 },
    { time: new Date(2011, 9, 28), value: 0.32152725145442657 },
    { time: new Date(2011, 9, 31), value: 0.3285603375105354 },
    { time: new Date(2011, 10, 1), value: 0.48147589216553843 },
    { time: new Date(2011, 10, 2), value: 0.4167973934640805 },
    { time: new Date(2011, 10, 3), value: 0.4812038553700564 },
    { time: new Date(2011, 10, 4), value: 0.41015090466712606 },
    { time: new Date(2011, 10, 7), value: 0.47403533085971933 },
    { time: new Date(2011, 10, 8), value: 0.4267823279163027 },
    { time: new Date(2011, 10, 9), value: 0.9404272280290495 },
    { time: new Date(2011, 10, 10), value: 1.2945673034034908 },
    { time: new Date(2011, 10, 11), value: 1.2716865187120996 },
    { time: new Date(2011, 10, 14), value: 1.0399738865195056 },
    { time: new Date(2011, 10, 15), value: 0.8978114054329259 },
    { time: new Date(2011, 10, 16), value: 0.8751412354139562 },
    { time: new Date(2011, 10, 17), value: 0.807269458803459 },
    { time: new Date(2011, 10, 18), value: 0.7329366664095296 },
    { time: new Date(2011, 10, 21), value: 0.7268434646668928 },
    { time: new Date(2011, 10, 22), value: 0.7816560956400136 },
    { time: new Date(2011, 10, 23), value: 0.9997757323401384 },
    { time: new Date(2011, 10, 24), value: 1.071233919681277 },
    { time: new Date(2011, 10, 25), value: 1.0062361752288367 },
    { time: new Date(2011, 10, 28), value: 0.9875752969964071 },
    { time: new Date(2011, 10, 29), value: 0.791271224956036 },
    { time: new Date(2011, 10, 30), value: 0.6286850817142549 },
    { time: new Date(2011, 11, 1), value: 0.6339169068397538 },
    { time: new Date(2011, 11, 2), value: 0.5982406886486469 },
    { time: new Date(2011, 11, 5), value: 0.4869739268801938 },
    { time: new Date(2011, 11, 6), value: 0.5552821868659298 },
    { time: new Date(2011, 11, 7), value: 0.5738834347400082 },
    { time: new Date(2011, 11, 8), value: 0.5148525460177603 },
    { time: new Date(2011, 11, 9), value: 0.5614303562886769 },
    { time: new Date(2011, 11, 12), value: 0.43282829847754684 },
    { time: new Date(2011, 11, 13), value: 0.45560654502680076 },
    { time: new Date(2011, 11, 14), value: 0.5095745698481957 },
    { time: new Date(2011, 11, 15), value: 0.495669239724211 },
    { time: new Date(2011, 11, 16), value: 0.41656418483261926 },
    { time: new Date(2011, 11, 19), value: 0.3671442767044223 },
    { time: new Date(2011, 11, 20), value: 0.27611909512553184 }
];
