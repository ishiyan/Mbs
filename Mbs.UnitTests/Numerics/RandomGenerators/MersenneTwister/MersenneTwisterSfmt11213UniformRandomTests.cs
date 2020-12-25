﻿using System.Linq;
using Mbs.Numerics.RandomGenerators.MersenneTwister;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

namespace Mbs.UnitTests.Numerics.RandomGenerators.MersenneTwister
{
    [TestClass]
    public class MersenneTwisterSfmt11213UniformRandomTests
    {
        private const int Seed = 12345;
        private const int Count = 5000;

        private static readonly ulong[] ExpectedULong =
        {
            553293926UL, 698755237UL, 2442073441UL, 4209880924UL, 1764362329UL,
            2195309918UL, 256824928UL, 2214706803UL, 2136320427UL, 2843087932UL,
            3486529899UL, 1306350756UL, 4270642900UL, 3297631664UL, 1850831330UL,
            4213109783UL, 427081536UL, 1131131307UL, 2775965886UL, 877546999UL,
            2836645597UL, 2850507086UL, 2755759911UL, 1306876265UL, 2575673187UL,
            1824065691UL, 3511080041UL, 1928485268UL, 649052443UL, 2971742730UL,
            403698108UL, 169875003UL, 380295356UL, 3528011866UL, 2952256661UL,
            3733745051UL, 4164222377UL, 1628603045UL, 2638256790UL, 3519195868UL,
            4195872803UL, 1476523139UL, 3699602437UL, 1672363774UL, 3684807997UL,
            3968862050UL, 245048207UL, 33360175UL, 169770165UL, 775405269UL,
            3191865739UL, 3052672634UL, 2175042373UL, 363825885UL, 2991060127UL,
            3107352189UL, 2040975233UL, 2280766171UL, 1049451347UL, 2464675372UL,
            2786253384UL, 517425947UL, 2576418495UL, 3915167352UL, 1319262871UL,
            3860956986UL, 3254547342UL, 1881673000UL, 1502819804UL, 2581419104UL,
            3993517707UL, 4179793000UL, 4134726181UL, 193902288UL, 2129660509UL,
            1201683060UL, 2169424339UL, 1171199755UL, 3270482785UL, 2589967476UL,
            1245732811UL, 2180825597UL, 4108548794UL, 3943541881UL, 1664725995UL,
            3482571249UL, 3960328275UL, 2496083819UL, 2285170479UL, 707491711UL,
            1973753445UL, 2559837769UL, 3759732527UL, 3621894707UL, 2667897962UL,
            2786661035UL, 2316413096UL, 4122623937UL, 1564737819UL, 3606925948UL,
            3141917404UL, 3237355011UL, 4148255857UL, 3198176564UL, 899012754UL,
            1921496599UL, 2722540422UL, 2314650322UL, 3316686314UL, 486198287UL,
            3446984721UL, 359923326UL, 4170566747UL, 1715634878UL, 3002640485UL,
            2198169011UL, 1709206686UL, 1602063393UL, 1768555127UL, 296016403UL,
            707947167UL, 4149178993UL, 2286171885UL, 3606834898UL, 860116794UL,
            837512047UL, 2780968890UL, 2934517872UL, 3265475992UL, 3995475331UL,
            823251686UL, 1388379685UL, 1689938384UL, 924010855UL, 801623899UL,
            2370143426UL, 978918237UL, 3737022385UL, 1478921360UL, 3119535156UL,
            3224513071UL, 3275923638UL, 2867594136UL, 731709579UL, 3968236195UL,
            64525079UL, 272371898UL, 4238912616UL, 1886765573UL, 3761754562UL,
            1000710391UL, 2048957979UL, 312497819UL, 439741686UL, 924925889UL,
            3994889236UL, 52289190UL, 759549430UL, 2584954556UL, 756493366UL,
            168627065UL, 857399839UL, 3425284973UL, 959170692UL, 1975409876UL,
            925869263UL, 1735262701UL, 3394788651UL, 550383511UL, 490309361UL,
            881075537UL, 2070029863UL, 1223376354UL, 2762562712UL, 2355745036UL,
            4058079425UL, 688888168UL, 2084555594UL, 2213905937UL, 1164042303UL,
            3938114127UL, 3862067658UL, 1899582417UL, 2637725755UL, 1855007909UL,
            1289092282UL, 3474658897UL, 2719086468UL, 2644140387UL, 3572064696UL,
            2835757420UL, 3854845607UL, 995529688UL, 2400664527UL, 2684303099UL,
            1002902183UL, 2708291817UL, 1162219077UL, 1458303374UL, 2328212094UL,
            2278558260UL, 2811513813UL, 187682146UL, 3731777450UL, 1369014755UL,
            4107243587UL, 4249288801UL, 3167574229UL, 2433688524UL, 1263841578UL,
            4178639597UL, 103168337UL, 726791243UL, 3865077042UL, 468986147UL,
            1352009278UL, 2120693945UL, 2257059684UL, 2511574244UL, 282332575UL,
            943738722UL, 765376122UL, 3886666299UL, 2550905672UL, 3021593314UL,
            1563487207UL, 2902762078UL, 2131594494UL, 1640852828UL, 2051546052UL,
            533501181UL, 3599888918UL, 2717252360UL, 2048541279UL, 449906823UL,
            4214508583UL, 407826952UL, 1220354008UL, 3680787680UL, 874143422UL,
            2771749303UL, 3918211583UL, 1933979094UL, 2397328810UL, 2048627355UL,
            1066449138UL, 620406396UL, 1265779869UL, 82128565UL, 3162944753UL,
            1216223916UL, 1462377073UL, 2410881086UL, 3283960481UL, 2698235827UL,
            1293536964UL, 1062717153UL, 737566608UL, 1581406402UL, 97798499UL,
            4147949360UL, 3084486205UL, 3702594522UL, 1519104056UL, 796070846UL,
            1353803554UL, 3887612590UL, 3110269517UL, 733537104UL, 705633439UL,
            2781674078UL, 991272693UL, 916020596UL, 3545700981UL, 220687238UL,
            324363525UL, 2561026636UL, 3521913746UL, 3070945031UL, 2373998044UL,
            3146062523UL, 1137989108UL, 3961642762UL, 3895747052UL, 1580424738UL,
            3667204475UL, 1010913545UL, 2234226736UL, 4019446758UL, 727087410UL,
            4059848248UL, 2936062142UL, 3183958929UL, 3882269258UL, 3455933746UL,
            1356303281UL, 4279894400UL, 2251669780UL, 133524669UL, 1908619621UL,
            2907753894UL, 364805371UL, 3022035474UL, 2844816175UL, 3612488522UL,
            3506546721UL, 1443147198UL, 1217370657UL, 3834938319UL, 1100686954UL,
            489874032UL, 2821199686UL, 3650884881UL, 3171161774UL, 3305651455UL,
            1771732079UL, 1462745239UL, 2313021630UL, 733759255UL, 2146937574UL,
            2232389715UL, 326010464UL, 2874477323UL, 4182050638UL, 2190268358UL,
            3013967555UL, 2540452868UL, 1319520735UL, 3037507174UL, 1074278118UL,
            3719401211UL, 2013303214UL, 2756190478UL, 2387141578UL, 180439427UL,
            3993230439UL, 2862452196UL, 2557110861UL, 3728952268UL, 1253230935UL,
            781742369UL, 2744212073UL, 1353835579UL, 3955007660UL, 914260429UL,
            1339735569UL, 747434431UL, 643113688UL, 2071932020UL, 2899349882UL,
            3223077155UL, 1630920461UL, 1463073770UL, 630145878UL, 3050743218UL,
            3516525223UL, 3480270450UL, 294098109UL, 4063336363UL, 2361143405UL,
            2013101517UL, 2785286939UL, 1503734644UL, 3839462541UL, 4074196387UL,
            1117196373UL, 2048922479UL, 2388244186UL, 2413315060UL, 2367315280UL,
            3349315306UL, 2936256882UL, 4068808092UL, 1372059822UL, 1149208750UL,
            3401323389UL, 2755773423UL, 2860092467UL, 1505179007UL, 3793739167UL,
            2348158578UL, 2812841843UL, 3568987390UL, 3339508196UL, 1360318330UL,
            487859816UL, 3192619943UL, 1942716817UL, 3417059105UL, 2006941363UL,
            3876292868UL, 3966499501UL, 3343575128UL, 1320062019UL, 4052874294UL,
            1661660417UL, 3278328199UL, 667015521UL, 2547492577UL, 2504792501UL,
            3304712274UL, 481758187UL, 3614301650UL, 3369442324UL, 2121036589UL,
            3692381588UL, 389150458UL, 2736546189UL, 1376548125UL, 1622203973UL,
            3300072739UL, 2789882526UL, 3881344628UL, 34239194UL, 4256374384UL,
            515810405UL, 1337208030UL, 4076098244UL, 3273673306UL, 4049989741UL,
            2223726553UL, 1600951795UL, 3374458346UL, 3959457033UL, 4038761321UL,
            3738299300UL, 3475534027UL, 315621578UL, 2566360079UL, 400233151UL,
            3198002493UL, 4000178367UL, 3003537992UL, 2418185466UL, 4133921279UL,
            3950353604UL, 3290950939UL, 105405052UL, 2073632531UL, 2567135797UL,
            2114529127UL, 617722918UL, 574897074UL, 145060013UL, 3481202032UL,
            278011874UL, 4169773197UL, 1353827771UL, 1239826005UL, 4142475495UL,
            2389529339UL, 3666924805UL, 2066381368UL, 6789416UL, 1668443782UL,
            3196261404UL, 3514547288UL, 4026411825UL, 802392020UL, 2677514010UL,
            2787660169UL, 3939691420UL, 3204448871UL, 2381714337UL, 1233034256UL,
            2984307665UL, 1103407055UL, 3031977605UL, 2409144418UL, 1140946250UL,
            3222997334UL, 280241001UL, 2085596678UL, 520966818UL, 510353206UL,
            208665480UL, 3795213078UL, 288783070UL, 2098371164UL, 562326118UL,
            3233670196UL, 2553636526UL, 522053470UL, 1828316355UL, 4214319056UL,
            452855188UL, 2302876034UL, 924621805UL, 191866231UL, 4089907614UL,
            4143273946UL, 2854199969UL, 4188327916UL, 4109927523UL, 2056850061UL,
            1218639351UL, 3152774385UL, 3248416571UL, 4139264331UL, 673684274UL,
            1841624903UL, 37564639UL, 1701880855UL, 2249177851UL, 729274386UL,
            428106918UL, 4181452858UL, 347163906UL, 1863870163UL, 3001348367UL,
            3537322846UL, 1225876535UL, 4022036962UL, 714821668UL, 3243025973UL,
            3845974554UL, 2663355489UL, 4212970629UL, 2993122213UL, 3666588071UL,
            3380238642UL, 2513936028UL, 2212417121UL, 2955814918UL, 569232806UL,
            3154916977UL, 3107294339UL, 3134048835UL, 3849011673UL, 3836294919UL,
            3734889980UL, 164091041UL, 2030878622UL, 1419071374UL, 346851020UL,
            1377631226UL, 1289968696UL, 962784564UL, 2020590972UL, 421488551UL,
            3180838841UL, 2053804618UL, 690498077UL, 2594130153UL, 1858540204UL,
            2316849314UL, 1897091063UL, 1326185749UL, 3880467220UL, 1013841045UL,
            2510006459UL, 200757585UL, 1706901250UL, 507983600UL, 2286361318UL,
            2687095773UL, 2439114934UL, 3785940507UL, 2910927696UL, 495212298UL,
            1737826401UL, 275378906UL, 1933723674UL, 1120172738UL, 2170670353UL,
            703797292UL, 793855448UL, 1884515135UL, 3720836500UL, 1734151247UL,
            3858004935UL, 1423598860UL, 1976443512UL, 1142497044UL, 2446033335UL,
            1228821251UL, 2197570102UL, 146344084UL, 3365694978UL, 355468092UL,
            881096899UL, 931732364UL, 3350199871UL, 3243058622UL, 245057594UL,
            4278566245UL, 1408621021UL, 2506645410UL, 1267060469UL, 1365764095UL,
            33272254UL, 2646325483UL, 1709890154UL, 442316585UL, 538779104UL,
            3412560582UL, 828087032UL, 2053596312UL, 2772521746UL, 894578119UL,
            201257826UL, 2291277544UL, 3599460260UL, 2893282544UL, 1783425940UL,
            2778987415UL, 2575736397UL, 2012251818UL, 2003236969UL, 3481419440UL,
            1234904448UL, 1194524734UL, 3660540193UL, 2209734072UL, 1072263624UL,
            2734463137UL, 3382453617UL, 2661765428UL, 642404874UL, 3533639813UL,
            3326229745UL, 2297215451UL, 2477704233UL, 195299388UL, 1781116828UL,
            3276896535UL, 2771181349UL, 348737860UL, 1379297272UL, 3012409305UL,
            571556070UL, 91329936UL, 3368648619UL, 4135205696UL, 2115649999UL,
            1627132802UL, 2941027473UL, 1066966330UL, 3576075844UL, 2982957512UL,
            1380172142UL, 613840578UL, 2425556655UL, 84325493UL, 1157833380UL,
            380526350UL, 4039021014UL, 1290253449UL, 545146169UL, 2529775161UL,
            427361307UL, 4182711492UL, 883562403UL, 59017230UL, 25492125UL,
            1099369309UL, 3505470186UL, 2881491845UL, 2566852819UL, 2871303793UL,
            3788442890UL, 2869657037UL, 801373099UL, 3358326700UL, 3419416145UL,
            843059834UL, 3762692802UL, 4171146309UL, 3260102383UL, 4031587170UL,
            2675507635UL, 838206744UL, 3681419048UL, 2099605932UL, 2127485526UL,
            926849448UL, 1980170805UL, 1438224173UL, 3523576643UL, 926000523UL,
            921155423UL, 180162584UL, 2033580132UL, 3202819054UL, 4274281899UL,
            69381977UL, 3353380750UL, 1092443156UL, 3825831970UL, 1248974759UL,
            3565713812UL, 1256704162UL, 1217134093UL, 1591142327UL, 1752494917UL,
            3028371512UL, 1958303331UL, 1850071672UL, 1217813873UL, 2916267074UL,
            2374819653UL, 3587666539UL, 4104385618UL, 3253865448UL, 3488338117UL,
            949576801UL, 689565959UL, 4110396943UL, 2660759860UL, 1472458161UL,
            3610122826UL, 180814354UL, 3695546105UL, 1577211192UL, 1424463045UL,
            2848290167UL, 4040830260UL, 2890841189UL, 920378244UL, 136171106UL,
            4066704554UL, 2505125024UL, 532297214UL, 2924909261UL, 530183725UL,
            1877714361UL, 2069794883UL, 332588948UL, 1738987415UL, 3417039337UL,
            3566072804UL, 3310481469UL, 2816418630UL, 1926132498UL, 2214615551UL,
            1599351183UL, 1233470572UL, 1696978507UL, 1606997108UL, 3913303019UL,
            1946659740UL, 2966027013UL, 2182553097UL, 1357275782UL, 1374421849UL,
            3138941674UL, 1105100192UL, 4222346580UL, 2362357402UL, 1279700422UL,
            249844665UL, 3656189515UL, 580604409UL, 2189793371UL, 3131564844UL,
            2650695683UL, 3243504545UL, 3397723045UL, 4188316599UL, 3026572267UL,
            907127125UL, 3405628562UL, 830752183UL, 1292469083UL, 2111044366UL,
            224549056UL, 4169187060UL, 454544759UL, 2474323400UL, 609923725UL,
            3194353098UL, 1869536251UL, 4096821340UL, 2152204142UL, 1225922401UL,
            1381918119UL, 3791021138UL, 517181631UL, 4016322084UL, 1076718013UL,
            1207923521UL, 1607526999UL, 3517963918UL, 171965789UL, 4124668424UL,
            876823670UL, 3547237734UL, 3510460793UL, 1383243182UL, 81851397UL,
            4122318508UL, 4096995620UL, 4238234251UL, 1369415583UL, 3357744380UL,
            1755168431UL, 3402210424UL, 2708770915UL, 2178593232UL, 2230490979UL,
            3850592336UL, 395202753UL, 1145970800UL, 2073751834UL, 4019268122UL,
            1715664056UL, 2670712766UL, 511797051UL, 1298315286UL, 750155609UL,
            2511075420UL, 3724283654UL, 3264667428UL, 3297812813UL, 2794646432UL,
            792075244UL, 1481885653UL, 3821585101UL, 3356348441UL, 3067087482UL,
            2351213299UL, 2914492104UL, 3578170041UL, 2068541578UL, 2559734620UL,
            1449473034UL, 1515443287UL, 1503937449UL, 3731228403UL, 3301242979UL,
            2675666516UL, 1453384331UL, 275781647UL, 1513899836UL, 1057387684UL,
            2524963245UL, 3897058285UL, 3845220976UL, 927032671UL, 597054215UL,
            1488976437UL, 816921064UL, 183760231UL, 2679276644UL, 3669514486UL,
            2550447080UL, 2645963843UL, 519530310UL, 3972602610UL, 660791018UL,
            2209454157UL, 3286137393UL, 1764490893UL, 3722356670UL, 3818175380UL,
            3812758152UL, 2781451830UL, 2816833912UL, 845403569UL, 2456431864UL,
            129871448UL, 2010242160UL, 983068913UL, 4125747311UL, 2193597351UL,
            450223202UL, 2213550157UL, 2219064558UL, 3324952925UL, 2447629198UL,
            4188828518UL, 1944720473UL, 2668121873UL, 256251632UL, 3141431621UL,
            862629169UL, 1986498648UL, 3852489741UL, 1305422895UL, 1395752592UL,
            2678110086UL, 3356575208UL, 2203525424UL, 4148844695UL, 1801851064UL,
            1148643645UL, 550804577UL, 1041274339UL, 3776082391UL, 3759739470UL,
            1746757602UL, 2594443401UL, 4019087208UL, 2706490867UL, 2087847544UL,
            2009753800UL, 1119878086UL, 1168778453UL, 2726806895UL, 3240455892UL,
            1567033770UL, 1407377442UL, 337526830UL, 18962543UL, 247615138UL,
            3510873826UL, 3685265114UL, 722017411UL, 1469421282UL, 239638616UL,
            4136684102UL, 1352947701UL, 547594847UL, 1141032357UL, 1384197850UL,
            2489754464UL, 834585989UL, 622533931UL, 416760316UL, 3890426510UL,
            4208125764UL, 1854255901UL, 2488983923UL, 2510508267UL, 2931259896UL,
            3940181108UL, 2096823000UL, 2661847721UL, 4008234493UL, 1602830869UL,
            3598000046UL, 215303987UL, 508369812UL, 276413009UL, 371619631UL,
            796969723UL, 4202155772UL, 2161050256UL, 3334645789UL, 77707822UL,
            881847475UL, 1209069193UL, 2503105966UL, 433721509UL, 985791955UL,
            3389467611UL, 4157689377UL, 2288410534UL, 2806875726UL, 467572461UL,
            1064356101UL, 1710832805UL, 242792642UL, 343019004UL, 659238831UL,
            334161479UL, 235290899UL, 3233084111UL, 3866085073UL, 1683915436UL,
            4245718284UL, 167318999UL, 3539996379UL, 3628010425UL, 1850237454UL,
            290721491UL, 2854158795UL, 3243323549UL, 1489524335UL, 2819941436UL,
            3548231239UL, 447435310UL, 1997028181UL, 1952879485UL, 2254426422UL,
            3813914344UL, 3969754167UL, 232325977UL, 3736416005UL, 3159635804UL,
            2769427612UL, 1971270154UL, 3705493847UL, 2558969475UL, 1039180510UL,
            3468554367UL, 2988029559UL, 1400167238UL, 1768303901UL, 2758662123UL,
            2072997009UL, 1332330347UL, 179681555UL, 2315290438UL, 2429393974UL,
            509881964UL, 3807607878UL, 3055319970UL, 671840881UL, 3477325874UL,
        };

        private static readonly ulong[] ExpectedULongArray =
        {
            3887633895UL, 132867192UL, 106293177UL, 4163623294UL, 520921026UL,
            3480846587UL, 1033927788UL, 2644791256UL, 1158495701UL, 4036591710UL,
            252973991UL, 2443176698UL, 3209199166UL, 2767568251UL, 3335727067UL,
            3450783102UL, 2330247266UL, 877440332UL, 2981784736UL, 2091001863UL,
            570833091UL, 1993903953UL, 1066837936UL, 2983397772UL, 1890020964UL,
            3659870890UL, 1968948872UL, 772580052UL, 3800380467UL, 1422555585UL,
            2944453937UL, 1292309301UL, 2119960118UL, 3918837620UL, 2229273615UL,
            3206843710UL, 2518549580UL, 1969574981UL, 1918743079UL, 1365730335UL,
            96674676UL, 2559009893UL, 1146915490UL, 3827753486UL, 201033585UL,
            1240888451UL, 3405784912UL, 2320320349UL, 1576953416UL, 444654152UL,
            457239379UL, 3897923418UL, 1150569866UL, 2126633905UL, 1642092058UL,
            3865529044UL, 2667271541UL, 1566857384UL, 3331818117UL, 2105615477UL,
            299873184UL, 3481263718UL, 89261028UL, 492117525UL, 2269606589UL,
            1596044179UL, 4178644349UL, 2883946084UL, 1831919056UL, 3754846887UL,
            3631318004UL, 1175860737UL, 2656020511UL, 1973196633UL, 957532278UL,
            2107352166UL, 1725040098UL, 3748709209UL, 1024534390UL, 1376344082UL,
            1414557850UL, 1599221135UL, 920575952UL, 3627473443UL, 716780548UL,
            1774740877UL, 4207436595UL, 1787746266UL, 4111134609UL, 2814714798UL,
            353837738UL, 362263951UL, 2457731489UL, 537957686UL, 1430128615UL,
            2334776743UL, 410082359UL, 520100537UL, 1377827014UL, 3133959422UL,
            240777748UL, 102054259UL, 223902919UL, 3558704817UL, 1886848317UL,
            3968411809UL, 346050304UL, 690373883UL, 1529004695UL, 1012695864UL,
            648718794UL, 3374864976UL, 390743123UL, 1664587503UL, 3067624019UL,
            2939614331UL, 2418392802UL, 823145107UL, 58678774UL, 4006777959UL,
            2503932349UL, 261895625UL, 1276400192UL, 1899704290UL, 2149566376UL,
            2896001244UL, 1549043442UL, 2259189175UL, 710989142UL, 2126327065UL,
            3957061522UL, 740772297UL, 2199579302UL, 1661208750UL, 4280245545UL,
            2783096125UL, 384981324UL, 3550432847UL, 1214743024UL, 576859762UL,
            1185416093UL, 1305995558UL, 604128082UL, 2941143558UL, 4103649873UL,
            3470726146UL, 1363198232UL, 1968480451UL, 956690710UL, 2601671462UL,
            2976516856UL, 2516776863UL, 1757313032UL, 901713855UL, 3021479132UL,
            1885115628UL, 3420394477UL, 3476334235UL, 316450606UL, 1899654312UL,
            4190824197UL, 3113090752UL, 1203432183UL, 3092986416UL, 40090931UL,
            2929930371UL, 1561898159UL, 1575088639UL, 2455218408UL, 3955583686UL,
            356940740UL, 1600070686UL, 2143245843UL, 3972513402UL, 2937231318UL,
            2549841969UL, 2698177606UL, 1767248507UL, 3251785250UL, 3579946228UL,
            1026789819UL, 1393417885UL, 538442918UL, 3139359258UL, 2379373080UL,
            2650452716UL, 2020205837UL, 422943063UL, 2766821422UL, 3786108580UL,
            3161865900UL, 2159091494UL, 4136199923UL, 2727464683UL, 2807704188UL,
            129011411UL, 3867741716UL, 4181569467UL, 1952455242UL, 1777735519UL,
            1436983454UL, 169937665UL, 697390109UL, 3417147133UL, 2519299599UL,
            1147345058UL, 1172961223UL, 3349862952UL, 4160853198UL, 593660971UL,
            1476358427UL, 2831865184UL, 3209918819UL, 1612715386UL, 2687737067UL,
            863857523UL, 3550334021UL, 2988540682UL, 848921819UL, 3072935813UL,
            104157974UL, 1555226881UL, 3046616244UL, 444864819UL, 1243255051UL,
            1223374695UL, 2160953721UL, 936577588UL, 3118578137UL, 2191637173UL,
            2007439UL, 2454461625UL, 2927164341UL, 2344512859UL, 1955688507UL,
            2266422085UL, 630712233UL, 3137148040UL, 3853588967UL, 1413733619UL,
            871142227UL, 1452624290UL, 3425668023UL, 1501717547UL, 89173518UL,
            1508053358UL, 3026945336UL, 2843896188UL, 1920372862UL, 3490151UL,
            2419196033UL, 3110174427UL, 2530033027UL, 433852987UL, 1166089771UL,
            2789605139UL, 3267914853UL, 1664169363UL, 2735636915UL, 4224772209UL,
            3822860476UL, 1619716599UL, 292629314UL, 1296952429UL, 40904289UL,
            2571641834UL, 2002892555UL, 2690557756UL, 2909787283UL, 885197089UL,
            1063056595UL, 2805802394UL, 3287708727UL, 1419250412UL, 685340682UL,
            4264696217UL, 559577074UL, 1906496889UL, 3079363054UL, 3105336173UL,
            463227757UL, 169322608UL, 3620552821UL, 756163383UL, 1131026224UL,
            949518377UL, 3442949176UL, 3730589733UL, 1503101960UL, 3374097148UL,
            1118766890UL, 3709787611UL, 3272295510UL, 102193920UL, 584368613UL,
            2567257147UL, 625386233UL, 702564753UL, 1866840665UL, 821311072UL,
            3046881611UL, 2660397199UL, 1361344468UL, 359470919UL, 3529052621UL,
            1124597392UL, 1938093732UL, 3423368586UL, 1840907605UL, 2001282917UL,
            1416348295UL, 1706814199UL, 3477490169UL, 2808643161UL, 4013734778UL,
            750992795UL, 3074398168UL, 2509789448UL, 914476561UL, 2410439001UL,
            3143539096UL, 3084846845UL, 4017523402UL, 750961725UL, 1687709099UL,
            2356842235UL, 3092286896UL, 2705860216UL, 3507707132UL, 1890692052UL,
            353796214UL, 4026349944UL, 2101619722UL, 3286412891UL, 1280330655UL,
            192904766UL, 1805413072UL, 698516761UL, 1446721297UL, 881395940UL,
            4242289663UL, 1319787538UL, 1810940044UL, 1098789900UL, 1657343682UL,
            419070290UL, 3180012080UL, 3479194631UL, 243557403UL, 570208958UL,
            2507967377UL, 4072867721UL, 3803767661UL, 3626523888UL, 4255906192UL,
            1177783399UL, 422040871UL, 1597135087UL, 1833147082UL, 2817664002UL,
            1294941605UL, 2466968315UL, 2086008666UL, 3804921579UL, 2097087757UL,
            538955354UL, 2468314739UL, 762203758UL, 602299431UL, 3744836186UL,
            82384255UL, 3912370992UL, 1749529655UL, 1377719535UL, 2763290671UL,
            3188888958UL, 3427430662UL, 204008088UL, 3512703178UL, 1598534039UL,
            1392208937UL, 449574726UL, 1794240771UL, 2498201962UL, 3122162905UL,
            1059434131UL, 2956971652UL, 1650652556UL, 168064532UL, 2856852245UL,
            1099442844UL, 4089277164UL, 1215706205UL, 3318199189UL, 2403907584UL,
            2961194143UL, 3197740368UL, 2937676556UL, 141442809UL, 3653164873UL,
            3127854643UL, 1137287910UL, 1174804779UL, 96802441UL, 112374271UL,
            3357197150UL, 1113309138UL, 1994816023UL, 2407814518UL, 1138060281UL,
            1763885723UL, 2535303554UL, 3199987198UL, 2846381649UL, 3084454895UL,
            1646663324UL, 344210403UL, 4221790888UL, 3683670217UL, 1255310831UL,
            443022349UL, 2074075515UL, 475869352UL, 3862388616UL, 3722695186UL,
            1144333320UL, 3499339134UL, 3453908604UL, 4020960816UL, 2456240194UL,
            2529486526UL, 3696151518UL, 1161628037UL, 2450248466UL, 8611550UL,
            1232848681UL, 195719489UL, 3565061770UL, 1577546485UL, 1089576749UL,
            4078169007UL, 718430774UL, 403842407UL, 4237062681UL, 3085614496UL,
            2302517615UL, 3420374224UL, 815823740UL, 1379708540UL, 3721963403UL,
            2840556526UL, 1536373742UL, 213324491UL, 2191084713UL, 3080175045UL,
            2828492757UL, 1358617229UL, 1371054268UL, 1770837936UL, 91828110UL,
            3815300419UL, 3147887510UL, 1324385052UL, 3552885569UL, 3402500314UL,
            1013753783UL, 3172187185UL, 4204047789UL, 1358211305UL, 2059033520UL,
            3732561119UL, 2740842812UL, 1848896031UL, 1121105259UL, 245314232UL,
            3998825267UL, 1774093490UL, 2789558951UL, 1069005609UL, 2123083822UL,
            244181168UL, 2861278923UL, 3470275971UL, 164403453UL, 443623596UL,
            2451492452UL, 1344798817UL, 3201252074UL, 2590020328UL, 4154627451UL,
            1961273077UL, 2680083115UL, 3768651868UL, 3885805595UL, 294096124UL,
            486850774UL, 1127465400UL, 125632394UL, 368350665UL, 2259018132UL,
            1139748110UL, 95559154UL, 1750807763UL, 1880807221UL, 3602494955UL,
            4189491848UL, 24760476UL, 683562138UL, 4167529558UL, 2857198605UL,
            1408118573UL, 1303816682UL, 2546441664UL, 1485045624UL, 2241706949UL,
            586470270UL, 2606818345UL, 1039696793UL, 712157993UL, 2531437090UL,
            3242506156UL, 2422152434UL, 2409931873UL, 3929799331UL, 3099073267UL,
            574477139UL, 3017510859UL, 1486688964UL, 3218517983UL, 3275608136UL,
            987882840UL, 1705407541UL, 269378594UL, 1621628071UL, 1666128560UL,
            1029869315UL, 833316630UL, 2501413172UL, 369046314UL, 3985105546UL,
            4210929284UL, 690976123UL, 3192895399UL, 1594384465UL, 1999947475UL,
            1802662999UL, 2963920607UL, 3455491405UL, 3974714559UL, 2425107771UL,
            1121059554UL, 2425859322UL, 3961606201UL, 2942900128UL, 1118013123UL,
            3697673488UL, 1103198442UL, 1884510657UL, 4109271531UL, 1851138095UL,
            20031475UL, 2084116829UL, 2336125886UL, 1082487804UL, 3871513203UL,
            3010318591UL, 2166096650UL, 492815092UL, 137138277UL, 4082181520UL,
            1817400478UL, 3281618916UL, 1480437456UL, 152889217UL, 2335008205UL,
            1223853827UL, 2064475776UL, 1159400170UL, 964325129UL, 2745060543UL,
            622980827UL, 3637277230UL, 3859412439UL, 3095647228UL, 2053743269UL,
            3070751168UL, 2887844558UL, 919343679UL, 700206948UL, 3322531455UL,
            3253470283UL, 2746409987UL, 1978389522UL, 2794105960UL, 3077757477UL,
            3406512690UL, 4138504431UL, 1887697665UL, 2282455230UL, 77021966UL,
            2741221728UL, 3934121080UL, 4215311935UL, 3018235877UL, 2032094276UL,
            1385175760UL, 3675503534UL, 306717740UL, 139112861UL, 3075455413UL,
            407656119UL, 2209590955UL, 1669446669UL, 1786678196UL, 241095493UL,
            3414989896UL, 1135183267UL, 2631652729UL, 2072816214UL, 3135294925UL,
            544822185UL, 3199286645UL, 1262313659UL, 248457573UL, 2066164989UL,
            2071596780UL, 2603613910UL, 2899855715UL, 2827063419UL, 3078171866UL,
            1776532085UL, 3352947555UL, 1767122358UL, 530231407UL, 2492887883UL,
            1638262483UL, 61394793UL, 1587214634UL, 2986318546UL, 2197370782UL,
            4177512490UL, 495225925UL, 706901243UL, 1262155864UL, 4231359333UL,
            3677952443UL, 2732175369UL, 470512637UL, 33814049UL, 3711538789UL,
            1615783059UL, 2380472476UL, 985281006UL, 2904333090UL, 922469069UL,
            2592797216UL, 2856510982UL, 3144909401UL, 3288766948UL, 4222675304UL,
            2649416886UL, 146901033UL, 4267423869UL, 2848227850UL, 4229776146UL,
            2606404424UL, 3392534981UL, 2546145791UL, 4263530207UL, 1674119195UL,
            2623833278UL, 295854582UL, 1787938508UL, 1020676279UL, 3334049714UL,
            2294685860UL, 1993224052UL, 2169925403UL, 3359741603UL, 1372617824UL,
            1908419812UL, 2281517436UL, 3241726983UL, 2308433485UL, 719323620UL,
            2515757523UL, 930623118UL, 3431267948UL, 1187283906UL, 3540468119UL,
            3678912760UL, 3997396999UL, 3498206125UL, 4106826675UL, 2472721217UL,
            2424511800UL, 1071942287UL, 1083033381UL, 1625589310UL, 249955922UL,
            4266976616UL, 2382189892UL, 2790913393UL, 4208943554UL, 1003716619UL,
            2582326878UL, 4148157521UL, 4175902464UL, 572609273UL, 589790462UL,
            2411375936UL, 1775301814UL, 1466794965UL, 2291874111UL, 1806225849UL,
            878239785UL, 913435317UL, 238847134UL, 98969607UL, 2062730004UL,
            3833844960UL, 215710854UL, 2843987287UL, 1768272865UL, 2533574867UL,
            2244601462UL, 2335506718UL, 2893443993UL, 3271962634UL, 1863189705UL,
            1046957120UL, 1433103399UL, 1047195357UL, 3924847497UL, 3435468026UL,
            615623123UL, 3623837906UL, 3494334868UL, 467288359UL, 2236697543UL,
            929082083UL, 2921490058UL, 4246652475UL, 1281272806UL, 2036814344UL,
            307943255UL, 1859933019UL, 2228844179UL, 427913214UL, 141792007UL,
            3131345006UL, 3442553338UL, 2291315395UL, 2644048435UL, 2544234744UL,
            3253475413UL, 645718716UL, 219837225UL, 3691830406UL, 1729290653UL,
            1496508071UL, 2188356947UL, 685437903UL, 2885730381UL, 3811886299UL,
            515198322UL, 4130098744UL, 3474687500UL, 2844928441UL, 3686707090UL,
            11745546UL, 1163302907UL, 702925357UL, 2261167358UL, 1625597641UL,
            3405037647UL, 62439850UL, 1416582799UL, 3509824086UL, 2164321688UL,
            680534483UL, 3767933328UL, 927989108UL, 944477491UL, 2808668550UL,
            884174339UL, 544197220UL, 2227588770UL, 3835044808UL, 3394164466UL,
            331352606UL, 4275345806UL, 1730231051UL, 3343295371UL, 3139880070UL,
            3282006392UL, 395472021UL, 2390682641UL, 2128902962UL, 2428527810UL,
            4067648178UL, 3411274507UL, 3346814056UL, 2899851067UL, 1624868173UL,
            875010809UL, 2793262325UL, 1907802141UL, 4005860639UL, 2010934948UL,
            1921751631UL, 1002254637UL, 2731094522UL, 1257606840UL, 58815584UL,
            3733473506UL, 2171722485UL, 1647487083UL, 1813072230UL, 1142668479UL,
            984191258UL, 3772271519UL, 3670784267UL, 1882510059UL, 657442622UL,
            708220237UL, 1140729799UL, 3907319464UL, 4060715347UL, 1876318437UL,
            2858933417UL, 1820607876UL, 4015866102UL, 144743917UL, 1604410759UL,
            3839924240UL, 811121823UL, 3065602757UL, 3808838588UL, 1918178119UL,
            4251507406UL, 1959849387UL, 4046954968UL, 1900279145UL, 850801893UL,
            3244820279UL, 1372147507UL, 936449060UL, 2813595158UL, 3135547760UL,
            3435882165UL, 1463000157UL, 641620396UL, 368004183UL, 3005639634UL,
            2750416310UL, 1333572815UL, 1773895722UL, 896179350UL, 3000020164UL,
            1816773427UL, 1612021071UL, 690670336UL, 2359612494UL, 2646976076UL,
            363989056UL, 585720731UL, 1751860970UL, 3477573382UL, 221019824UL,
            4286209431UL, 1687146804UL, 1011037507UL, 2514439703UL, 323083478UL,
            3162391695UL, 2864271077UL, 3644965260UL, 2160102929UL, 204245478UL,
            3989118498UL, 3148576692UL, 1047310131UL, 1941734656UL, 881253250UL,
            3669181503UL, 3576049597UL, 530745749UL, 1004655313UL, 476473637UL,
            147160233UL, 3313502122UL, 3091743699UL, 3995508226UL, 3837939695UL,
            249675624UL, 3551248473UL, 1581059277UL, 2095508529UL, 4011075367UL,
            2232698149UL, 735953670UL, 4217647425UL, 1677522771UL, 4180314720UL,
            2583489156UL, 2583442663UL, 4028739476UL, 3581832541UL, 4015879475UL,
            4010069744UL, 715048516UL, 2488455563UL, 2152812104UL, 667868293UL,
            206998607UL, 2092292319UL, 448434255UL, 2744018610UL, 1244239097UL,
            1269811520UL, 159378763UL, 1011017183UL, 1109989204UL, 3231221189UL,
            4243748923UL, 3594834455UL, 247670788UL, 2766015955UL, 1929302471UL,
            741635826UL, 3590878634UL, 2405523880UL, 107043508UL, 3993219492UL,
            1481153038UL, 2035061864UL, 359996688UL, 2260744746UL, 780546738UL,
            153380969UL, 4246076636UL, 3429406461UL, 768000503UL, 165087011UL,
            1910049193UL, 823931080UL, 1225859659UL, 3422948348UL, 1536406145UL,
            4250288178UL, 1111790339UL, 2565707267UL, 1667610940UL, 1166195547UL,
            1940244395UL, 3396894481UL, 3142890564UL, 2651154773UL, 1331801796UL,
            4008464189UL, 2895838856UL, 1345678101UL, 1140702086UL, 697548148UL,
            1701001531UL, 1909794063UL, 3982230469UL, 2508050156UL, 512058770UL,
            2583280938UL, 1994457385UL, 2559309827UL, 1175225967UL, 3254980712UL,
            735287188UL, 4242876904UL, 1335605719UL, 1754669209UL, 729312935UL,
            2490883945UL, 2162409098UL, 1177788409UL, 3219001630UL, 2999314266UL,
            138544320UL, 1091221242UL, 932623318UL, 4050268455UL, 3353875087UL,
            298883056UL, 2968015159UL, 496659771UL, 1466559241UL, 1648343951UL,
            122000131UL, 3935323941UL, 3399870775UL, 2901893836UL, 2247965140UL,
        };

        /// <summary>
        /// Taken from http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html,
        /// SFMT-src-1.3.3.zip, SFMT.11213.out.txt.
        /// </summary>
        [TestMethod]
        public void MersenneTwisterSfmt11213UniformRandom_NextUInt_ReferenceImplementationTest()
        {
            const int ulongLength = 100;
            var gen = new MersenneTwisterSfmt11213UniformRandom(1234);
            for (int i = 0; i < ulongLength; ++i)
            {
                ulong expected = ExpectedULong[i];
                ulong actual = gen.NextUInt();
                Assert.AreEqual(
                    expected,
                    actual,
                    Invariant($"Reference implementation uint: index={i}, actual={actual} expected={expected}"));
            }

            const int ulongArrayLength = 100;
            gen = new MersenneTwisterSfmt11213UniformRandom(new[] { 0x1234, 0x5678, 0x9abc, 0xdef0 });
            for (int i = 0; i < ulongArrayLength; ++i)
            {
                ulong expected = ExpectedULongArray[i];
                ulong actual = gen.NextUInt();
                Assert.AreEqual(
                    expected,
                    actual,
                    Invariant($"Reference implementation array: index={i}, actual={actual} expected={expected}"));
            }
        }

#pragma warning disable S2699 // Tests should include assertions
        [TestMethod]
        public void MersenneTwisterSfmt11213UniformRandom_NextDouble_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt11213UniformRandom(Seed);
            double expected = 0.5;

            double actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextDouble();
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-1,
                Invariant($"Mean value 1: actual={actual}, expected={expected}"));

            expected = 4;
            actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextDouble(8);
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-1,
                Invariant($"Mean value 2: actual={actual}, expected={expected}"));

            expected = 6;
            actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextDouble(4, 8);
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-2,
                Invariant($"Mean value 3: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void MersenneTwisterSfmt11213UniformRandom_NextInt_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt11213UniformRandom(Seed);

            double expected = int.MaxValue / 2d;
            double actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextInt();
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-1,
                Invariant($"Mean value 1: actual={actual}, expected={expected}"));

            expected = 400d;
            actual = 0d;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextInt(800);
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-1,
                Invariant($"Mean value 2: actual={actual}, expected={expected}"));

            expected = 600d;
            actual = 0d;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextInt(400, 800);
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-2,
                Invariant($"Mean value 3: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void MersenneTwisterSfmt11213UniformRandom_NextBoolean_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt11213UniformRandom(Seed);

            const double expected = Count / 2d;
            double actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextBoolean() ? 1 : 0;
            }

            Doubles.AreEqual(
                1,
                expected / actual,
                1e-1,
                Invariant($"Mean value: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void MersenneTwisterSfmt11213UniformRandom_NextBytes_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt11213UniformRandom(Seed);

            const double expected = 128;
            double actual = 0;
            var array = new byte[32];

            for (int i = 0; i < Count; ++i)
            {
                gen.NextBytes(array);
                actual = array.Aggregate(actual, (current, b) => current + b);
            }

            actual /= Count * 32;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-2,
                Invariant($"Mean value: actual={actual}, expected={expected}"));
        }
#pragma warning restore S2699 // Tests should include assertions

        [TestMethod]
        public void MersenneTwisterSfmt11213UniformRandom_Seed_SameSeed_SameValue()
        {
            var gen1 = new MersenneTwisterSfmt11213UniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new MersenneTwisterSfmt11213UniformRandom(Seed);
            int actual = gen2.NextInt();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MersenneTwisterSfmt11213UniformRandom_Seed_DifferentSeed_DifferentValue()
        {
            var gen1 = new MersenneTwisterSfmt11213UniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new MersenneTwisterSfmt11213UniformRandom(Seed + 1);
            int actual = gen2.NextInt();

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void MersenneTwisterSfmt11213UniformRandom_CanReset_ReturnsTrue()
        {
            var gen = new MersenneTwisterSfmt11213UniformRandom();

            Assert.IsTrue(gen.CanReset);
        }

        [TestMethod]
        public void MersenneTwisterSfmt11213UniformRandom_Reset_ValueGeneratedThenReset_SameValueGenerated()
        {
            var gen = new MersenneTwisterSfmt11213UniformRandom();
            int expected = gen.NextInt();

            gen.Reset();
            int actual = gen.NextInt();

            Assert.AreEqual(expected, actual);
        }
    }
}
