﻿using System.Linq;
using Mbs.Numerics.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

namespace Mbs.UnitTests.Numerics.RandomGenerators
{
    [TestClass]
    public class MersenneTwisterSfmt216091UniformRandomTests
    {
        private const int Seed = 12345;
        private const int Count = 5000;

        private static readonly ulong[] ExpectedULong =
        {
            1905350899,  752275649, 2172726721, 1382267163, 3279518050,
            3268632728, 3717363760, 4072571273, 1344655483,  219752927,
             511254305, 3596362759, 4226641704, 4129438728,  558485682,
            1317634157, 4010378104, 3580397823, 1818301359, 3313744323,
            1311807646, 3802672028, 2808584435, 3266301975, 2020171045,
             620506054,   53161800, 1537320991, 2134803893, 2715479037,
             120112962, 2441794334, 1814983540, 1206486557, 4161448945,
            2366049554, 3777104347,  739449873,   25707446, 4065003655,
            2161161550, 1741136020,  657768502, 2433923856, 2717307829,
            1525759538,  830194516,  459936436, 1162980610,  334755644,
             384422724, 2793270342,  828636896, 2867499727, 3028851983,
            1389550954, 1307927144,  893471662,  900883406, 2683038639,
            3752961866,  843578343,   22805862, 1267242205,  706648080,
            1235221497,  401566969,  414340440,  183806636, 4048355600,
             144171325, 1757826623, 1187359184,  862386598, 3200993912,
            3519551609, 3664131000, 3927930108, 2280155993, 3567109092,
            3775477558,  935849832, 2314171361, 4112947106,  241167946,
            2009679537, 4068434237, 3401169166, 2404496165, 2540046975,
            1411369610, 2565200500, 1970906296,  189184781, 3991085631,
            2289307749,  772916776, 2177298567, 2622244404,  841323653,
            3002297462, 1110997129, 3903838163, 3330917352, 3830682741,
            1185829981, 4069158510, 3563499067, 1355228158,  279174398,
            2087329476, 2423096782, 1016630296, 3806781368,  441143051,
             298391628, 3621582017, 2030648235, 2778568577,   20008614,
             771286796,  775965775, 2896149172, 1792081455, 3106136512,
              94759167, 1472517982, 3534989573, 3357084062, 3958304829,
            1332393462, 2190063587, 1952654355, 1539833518, 3822333182,
            4274945979, 2721966223, 3575010048,  695018425, 2862589877,
            3587652205, 3552094794, 2676233649, 3263717172, 1637396305,
            4027583255, 1958060351, 4264331398,  593683175,  408980239,
             988052840,  876803380, 3140314974, 3922813482, 1390448671,
            3730494261, 1145457955,  273168740, 3292616328, 2978721561,
            1345183856, 1045127570, 1051033980, 2925181721, 2033022892,
            1316366587,  680747006, 3961605821, 1441180756,  279426804,
             470644301, 1741858073,  593697194, 1767225360,  183859904,
            3974384281,  845903783, 1310882131,  669406753, 1054340021,
            4014997838, 2938642801, 1538049494, 1530891450,  882749482,
            1201044069,  679219334, 2135595151, 2032090146, 4158670169,
             256935726, 3432106977, 3958491628, 1223520607,  170446626,
            2773504093,  991316677, 3361854848, 4169796350, 3422898364,
             758069043, 4131191096, 1007403602, 2578499118, 3975011761,
             343998493,  672215119, 2728119719,  845695282, 3683074078,
            4036121292, 2386799079, 1937349744, 4168522699, 3721568296,
            3257994462, 2235973040, 4138798405, 3416508495,  415554641,
            1830850018, 2272294274, 1431434993, 3171369806, 1613253939,
            2403758089, 3556453402, 2856071833, 1227218253,  715707246,
            2880808631, 1417631643, 3990886286,  609649821,  114165630,
            2087942882, 1081333306,  400518308, 3793455546, 3167928542,
               8941665, 4201007837, 1876696559, 2404116336, 1113953214,
             430106456, 2335684449,    1222027, 2592528910,  745815402,
            3359205119, 1397314198,  985063645, 1435856156,  364592888,
            2164242233, 2889344733, 1373414961, 3381926854, 2333409774,
             421351976, 4015789532, 3395346051, 3782874312,  689098557,
            1429425300, 2629692405, 2215730551, 2109553092, 3672589282,
             405112387,  206204079, 3265885168, 1652102882,  199867821,
             662089205, 1749535295, 1348785678, 2585946875, 1622353301,
            3145896841, 3871573397, 3871396933, 1710241364, 4006086513,
             595925263, 1495163900,  501657747,  954738840,  559217420,
             937485239, 2096175720, 2664016451, 1650924959, 1703340294,
            3900873930, 1041550706, 1511453442,  238912931, 2273696131,
            1914688989,  689121986,  521960682, 1222383853,  741801167,
            2179811491, 3527208543, 3035827211, 2742762783, 2341044996,
            2891690164, 3609731649, 2202071854, 1665659873,  608668030,
             740015651, 3394484943,  889328397, 1892219396, 1035630635,
             303150783,  242866685, 1156459843, 3238094121, 1005137614,
            1693171753, 2017910270,   82778904, 1512210142, 1455571619,
            2290401807, 2463148413,  293243398,   69455180, 4026701615,
            1337514959, 3717351941,    4361229, 3842407776, 2080215983,
            3669593060, 3263059767, 2081889249, 3045763888, 2175848985,
            3357888652, 4055120041, 1513353044, 4217354995, 2039234489,
            3490626025, 3057548944, 1197440319, 1438020449, 1959938614,
            3366455920, 2787368744, 1798301159, 1030434720,  489988194,
            2318866924, 1899252011, 2455865224, 4048325487, 3774544612,
             440534978,  181215195,  159896639, 2768529360, 2004867199,
            1011946060, 2993917926, 3201239590, 3581873124,  779356519,
            2358406154,  666324630,  381242630, 3137262257, 3221474981,
            2322873782, 2760921896,  298928658, 1353803174,  445500537,
            3189831613, 2199433368, 3578130406,   67308495, 1097363257,
            2345086433, 2850283446,  879892602, 3130882565,  431969044,
            2251884252, 1918563667, 3814629197, 2932239181,  915247832,
            3944290064, 2366036830,  385869551,  437584686, 2828872448,
            3671644609, 4037169112, 4215110938,  208323649, 2965252484,
            2799986380, 3082494494, 2724601563, 1311749244, 3231851010,
            4142144591,  849002738,   26518122,  299631092,  766137831,
            2933250906, 2358659658, 2119050184, 2953832477, 3488231542,
            1951847275,  122554435, 1466629227, 3728408993, 2207185674,
            1998805080, 3344258774, 3416013689, 2442481343,   71788421,
            1568331105, 2277980228, 1900004962, 3501955663, 1378743000,
             223061576,  240469200, 2734068860, 3141850757, 1971215373,
            3507256797,  484451521, 2910208224,  645921375, 1881701019,
            1242948664, 1415794074,  642481995, 3341106988, 1233064995,
            2118719351, 2684563659, 3942699681, 1661876636,   49294522,
            3815910708,  862726175,   60187426, 3079050903,  340365930,
            2175487462, 1222347486, 1663546068,  624493697, 3167995909,
            3416972739, 3548936506, 2988463021, 4205768043, 3373602650,
            1874001854, 2681314917, 2566928537, 3694039453, 3491697154,
             722699593, 3386012388, 3699708928, 3203712520, 1884692942,
            3172314623, 2886343397, 4015110850, 3481661006, 2333523771,
             394579788, 3244486965, 3851634617,  575958442,  661475677,
            4027014616, 1032733116, 1742476501, 2699106464, 2427387422,
            3278968563, 1401654580, 2434892939, 3310709615, 1030082652,
              71575511, 3286064376, 4266478707, 4067520610, 2035259649,
            3956312742, 3747564205, 1269107995,  971482350, 3615656023,
            2165985934, 1347872943,  769909410, 2141768520, 2210728437,
            4272113076, 3397210555, 3842658603, 3928093005, 1181043821,
            3650581809, 3837331899, 4251287777,  857437418, 3712160358,
             158112186, 2017456377, 2091290325,  530034986, 3958485552,
            1130768218, 1458679438,  711757957, 2226951129, 1400435199,
            2205573989,  681132584, 1606485119,  605061683, 1634137208,
             771521733, 1883230857,  819731249,  460566561, 2247917649,
            3180372503, 3956499079, 2857265355, 2822023457,  633551835,
             730663949, 4133772982, 1541007581,  655291723,  491833565,
            4238613467, 4238395265, 4148332602, 2846709712, 3666739336,
            1599217927, 1992539186,  542117821, 3381540942, 1083521146,
            2929587423, 1286034197,  694687584, 2114688830,  755884057,
            1225480683, 2303112066, 2216851515, 2679964190, 2012417031,
            2202755208, 1055662452, 1441196894, 1960640136,  134891511,
            1792286378,   96095693, 2441587205, 2571697140,  572348094,
            3038698416, 1515336879, 4119890414, 1477774860, 2621261274,
             775524487, 2370850430, 3573588215, 2432666246, 2805137457,
            1705084958, 1912673196, 1733419047,  565540065, 3658176306,
            1186139561, 1540614361, 1765415508, 2901119453, 2677938763,
             608410512, 3147563433, 2744572721,  630281268, 4281547057,
            3163422031, 4042483214,  879433734, 1474743579, 3244105636,
             546449004, 3901977495, 4271494524,  490303826, 2830346598,
            3538065040, 1759889319, 2632553428, 2497137691, 3382364696,
            3771178811, 4182558868,  661217915, 2094009163, 3473927964,
            1933445160,   60056494, 4286653490, 2282498290, 1816865056,
            1744504080, 3877997209, 2411794398, 1427135069,  475576541,
            2596725670, 2851942858, 2688636717, 2062076802, 2652736260,
            1840536028, 1796158845, 1419491939, 2392569010, 3543309817,
            1857463887, 1407222956, 1350400882,  853369886,  357026409,
            1916226738,  369757202, 3595771756, 2069824978, 1798820858,
             265441812, 3933300586,  631886096, 3719724467,  440994221,
            4033156456, 3539051907, 3528500224, 2905184043,  869863780,
            1119918947,  349851603, 2022008554, 3657649802, 2502263757,
            2887847332, 3623549117, 3139775195,  189879460, 3594415232,
            1476443725, 2027146420, 3478737667, 2022372715, 3635771295,
            1247288801, 2490343781, 2917445650, 3486234070, 3147298103,
             702157996, 1733675174,  148961960, 3637546432,  119043037,
            2641607842, 1851728830, 1493188937, 1850960804,  776561474,
             486908782,  676982621, 1360437101, 3921558300,  524059269,
            3616251342, 1313444427, 2575500259, 3008388353, 2184228934,
            3132104180, 2791516052, 3933368817, 3092737999, 1033355673,
             652434873, 1795221881,  165990977, 1591663583, 3778618815,
             979411763, 1757157297, 2978295606, 3104401780, 4003790533,
            3600737390, 1902620436, 1808442196,  148638181, 3518506772,
             222273385, 3840762809,  871803367, 2443412330, 3369059660,
            2184132100, 3734420911,  654812739, 3204847244, 3529461580,
             312855498, 4052839218, 2606765561,  404908990, 3058325420,
             800669907, 2181256043, 2913644147, 3191177949,  469487888,
            1163628636, 3229479050, 4249743748, 3343878471, 1832882027,
            3896395064,  290407245, 3030125745, 4259975835, 2395810376,
             586762202, 3485368721, 2271812809, 1646089564, 1400554960,
            1607918564, 3906227975, 2551611230, 2620472891, 3390950610,
             569235675,  469225893, 2475911543, 2493840062, 2916795345,
             653901202, 4226148040,  252276449, 2506184468, 2456244826,
            3350464974, 1047531605, 1574699344, 3231005863,  236573044,
            1976806281, 3474400269,  273166164, 2376991635, 1468294738,
            4220744536, 3544242931,  113085795, 3735139209, 3015931663,
            1183259596, 1513537201, 3635055725, 2815567198, 3769886419,
            1859125153, 1567197264, 4039237572, 1214682641, 3467042661,
             313911929, 1071494973, 3239591657, 1731606646, 2380790737,
            3117457529, 2032056436,   82741758, 3087189901, 1815548596,
            2099257988, 3135424492, 2141920934, 2706228204, 3227386575,
            2408246373, 2510580362, 2876752042, 3275017751, 2461160041,
            1089028823,  132296717, 3721613910, 4061462406, 3116575260,
            3637105965, 4102598244, 4270491831, 2219471231,   28269712,
             229752821, 4012012616, 2087217767, 1825557711, 2913412551,
            3827272668,  996553880, 3048694357,  656145720, 1400890929,
             855309158, 3361316084, 2281796537, 3621003468, 3376712896,
             373269204, 2150554304, 2644032731, 2608297774, 3247061906,
            2378486044, 2197636344, 1110248316,  510488438, 4222508121,
            1932487903, 1939451788, 2726692525,  507249057, 1189478681,
            1968056288, 1912686872, 1523204488, 4135978045, 4276393978,
             892470374, 2116871245, 2873988508,  267782302, 2600333103,
            1118661926,  522071866,  986506443, 3504149281, 2288468281,
              85506479, 1944847983, 3601012475, 3242243666, 1704027784,
            2910918199, 2639618413, 2842511672, 3090899876, 3344265804,
            3491372023, 1284553783,  823292036, 1042834900, 1217014154,
            3041156196, 3923654801,  449145181, 4185621008,  776101533,
            4244012815, 4183393238, 1630551432, 3541095693,  518007229,
            3786790801, 2087362168, 4043303764, 2541597901,  877636548,
            1543232672, 3223248673, 2946294413, 2951302921, 1762981213,
             646559268, 4024991007,  875051543, 3790758686,  662911583,
            2393433589, 3746877236, 2276633172, 1958215371, 1621859081,
            1090430683, 4286867349,  307201797,  137436826,  231157450,
            1180153035, 1217334245, 1008255547,  894051524,   36561812,
            1431589635, 4046684945, 2296659457,  188808300, 3741772165,
            1387380472, 3845864591,  347066786, 1408362619,  739470604,
            1136979532,  130583991, 3797954283, 1150742889,  439975670,
            2022875617, 2701471236, 2597216437, 2438216351, 3209302349,
            3779981080,  621383002, 3593639869, 1112530494, 1820064407,
            2950023860, 1547469744,  287878313, 3274997972, 2298389862,
            2236035319, 3396268332,  206954102, 3599143175,  451092095,
            2173514121,  254771403, 3560241300, 4283726585,  664976717,
            1385804231,  852193920, 2060007671,  493637957, 1056257230,
            4007629170, 1507468191, 2027451508, 3051295786,    2507318,
            4171954654, 2938491210, 1356393891, 3558249995, 3711769979,
            3434953144, 1601628304, 2187495640, 1762169715, 2141213778
        };

        private static readonly ulong[] ExpectedULongArray =
        {
            2175197313, 3416852690, 2735085457, 1320269992, 2016635691,
             810525983, 4006675942, 1301941202, 4042575088, 1187019939,
            2736388103, 3781722487, 4243529074,  246767902, 1304070346,
             976271690, 2556889879, 3073406364, 1698928375, 2296993776,
            1444184832, 2595126707, 2156126708, 1688098107, 2179521664,
            2803293687, 2385281238, 4227802979, 3134797180, 1147971141,
             884537117, 2319774828, 2957815251, 3878881667, 3638220066,
             187628187,  580180104, 3626674378, 2659137357, 3178256863,
            2208485580, 2118491266,  700978461, 3933624247, 1702805675,
            3421323227, 3994113341, 3233452765,  902746261, 3659636479,
            3040090851,  399690766, 3615970251, 3284661145, 3359446250,
            3392410964,  678870364,  125302232, 2971839201, 3280031982,
            1692954894, 1359525016, 1205709969, 2267560271, 2795714772,
            3332428943,  523833143,  814904241, 2717831408,  750976024,
             987287690, 2330824167, 4201373885, 4108491402, 3391537672,
            1215583381, 4207951069,  184863212,  519418259,  418551471,
            2065950398, 4203837364, 3382375333,  901435475, 3498831803,
            3791603773,  920521390,  595471020, 1684346142, 1325404090,
             899206118, 2667187708, 2524634641, 4223857302,  420191959,
            3600153965, 3418779320, 2005628553,  519485311, 1802332047,
            1775431541, 2974754701, 1302953822,  718291602, 2792216152,
             272273433, 2000679835, 1115490757, 4189014810, 4221662093,
            3279382464, 2167310573, 3572981844,  986564611, 1016187445,
            3923385874, 1690925277,  747521598,  896966884, 2953660581,
            2223572744, 2915157846, 1695795322, 2732766708, 2454770935,
            1365459672, 3542922404,  845615953, 2952906265, 2551210050,
            2378603943,  714719251,  681529710, 1450504234, 2697092333,
            4229290409, 4178094760, 3804824237,  329105432, 1148771138,
            1816355435, 2601460377, 2926855389,  559774675,  204601994,
             619613294, 1366647394,  631389794, 1632373745, 1702819439,
            1351351499, 3926253776, 2212069724, 2257522855, 3182894196,
            2144664872, 2978782920, 4174177214, 2264637001, 2033550555,
            2721089530, 3852902799, 2358982732, 4282409512,  645949970,
            4158048561, 2365295236, 2538335946, 1422410582, 1515666957,
            1328540612, 3416608278, 1907992560, 3215610555, 3605052737,
            2369473152, 3046303461, 3541223054, 4070694226, 2911823714,
            3015417186,   86976646, 1894631564, 3730827512, 2042286503,
            2442702782, 4065863279, 2575067876, 2518755498, 1231162998,
            1708707552,  755484493, 1196472843,  238532071, 2794049399,
            4068323913,  788142385, 3439317810, 4254806892, 2730299359,
             456712940, 3105911153,  635599068, 1919504978, 1602271422,
            3073159126, 1937899444, 3250494505, 3433658738, 2905249014,
             828004134, 1274724379,   11204149, 3065771975,  463281632,
            1311245967, 3607137686,  927226106, 2125254305, 4032264774,
            2717798701, 1180979891, 4140632357, 1391872498,  489622893,
            2237053816, 3409142052, 1348562203, 1181199099, 1021856681,
            1913287390, 4149402416, 2044125802, 1065033545, 3898981831,
             601484044, 3713889560, 1898297229, 1566140406,  112283335,
             144370687, 2559705952,  256226270,  749397708, 3927764607,
            1441819412,  260830114, 2010196388, 2065871892, 2078573014,
            3372978833, 1939689043, 3048082384, 2868393234, 3841503922,
            1161036831, 3303568233, 1635879973, 3952891055, 1842874301,
            2539835055, 1982813921, 4132924078,  449904843, 3000078014,
            3548435079, 3331798910, 2230308451, 4163397456, 1621961694,
            2607164414, 2196949496, 2866571435, 1549332011, 2614412233,
             446988690, 1420449928,  285578824, 1775783972, 2965689197,
            3924454932, 2082465460,  124817675,  438267808,  528385620,
            2636262341, 1080131492, 1558519152, 1116581600, 2929512475,
            2736868748, 3915621037,  764731211, 1586427655, 4090831503,
            2859278247, 3568367657, 1461667032, 3073042373, 1636409165,
             788064321, 4080437576,  918221278,  114909641, 3304636569,
            2297221668,   17015928, 1021721761, 3066504010,  197231261,
            3679321520,   50703873, 2361931142,  862683610,  953124365,
            3790316208,  473924483, 3119112329, 3641147116, 4293169628,
            1537054880, 3964372923, 4010396075, 3654613378, 3917125842,
            2262022437,  659622162, 2050227208, 3312299650, 2678098735,
             213107655, 3777826433,  921076906, 2086158981,  248548184,
            1641066954, 2114150584, 2838573398, 1755756364,  338185631,
            1525923083, 2995417952, 2468265825,  490566396, 3969536020,
            2631745927,  366431955,  925816415, 2177840594, 2117418995,
            3666734578, 1192843802,  602615620, 2264921997, 1281680980,
            3807690723, 4108778961, 1507019153, 2194700039, 1648735668,
            1662123733, 2545156448, 1365100506, 1454975775, 2939480794,
            3868914122, 3403436081, 2933163888, 1226203229, 3097935437,
             412741039, 2266117260, 3946237294, 1968556064, 4053706233,
            4188522920,  938480963, 1544826626,  187079461, 3575534833,
            3153119555, 3003083646,  623217156, 2682800552, 1391523050,
            1320972777, 3679801797, 3731157853, 1927511125, 2226279215,
            4279974680, 3294471840, 2438551136,  965362540, 1354235226,
            2826329648, 1945569688,  744342424,  517834045, 4171419655,
            1586512882, 3919656292, 2856570140, 1299221590, 1362052002,
            4198374247, 1942398157, 1628264064, 3340090278, 2696001177,
            3371008304, 2355576789, 4213879562, 1577788029, 2891482437,
             152253326, 3059746846,  308725122,  883477698, 1606888552,
             475456301, 3495085382, 1708884771, 1151106563, 2151585755,
             568104092, 1171123750, 1320333513, 2915951392, 3101194384,
             833485376, 2832675893, 2749470956, 1850954040, 3061763321,
            2164320062, 1437402049,  745474594,  476522513, 1910684623,
            1233503194, 1720653281, 2563149383, 1369917579, 2885433700,
            4017259377, 3527631012, 1495165834, 1488814059, 3042374123,
             253776860,  209442710,  294595258, 2290681583, 1453774141,
            4225773404, 2484925463, 4021847441,  561499559, 1613866912,
            2398351554,  587600213,  592498160,  285795251, 2163635292,
             140357620,  198819979, 1495667887, 4171446004, 2803832664,
              52871663, 1271649032,  260785107,  638082092, 3420258153,
            4059999016, 1173714699, 1003989385, 3804549249, 1700003691,
            3325490599, 4220913045, 3497459584, 3754110578, 1073652440,
            1012934728, 1908630564, 1548408009, 1144290124, 3230097511,
             493155866, 2098201937, 2011702478, 1052311189,  348816736,
            2307344475, 4255316055, 3972139683,  758529954, 2479320791,
            3436940926, 1620978392, 1668816453, 2442705447, 1021687635,
            3498850853,  780712235, 3205066929, 2581613651, 2197066671,
            2866767384,  400835161, 3733231902, 2654797756, 1350390285,
            2960276823, 3019375647, 3888260190, 1764552268, 3153913479,
            1984530194, 1512481464,  379627922, 3520063658, 2475600509,
            3984183971, 4275487577, 3646980350, 1279585319, 2000548989,
            2218157333,  707399500, 2204164482, 4205919981, 1441582841,
            3182181210, 3877058632,  794852786,  428188274, 1629674176,
            1499341650,  859137871, 1301416764, 4168290397, 2173539596,
             793979628, 2405965440, 4137305943,  637929855,  669254412,
             801661779, 3634743833, 3628206635,  365398844, 1808502337,
            2723237444, 1072681779, 2033628304, 4245264435, 3865184947,
            1090467638, 3872064476,  627298422,  396726946,  481924798,
            3651780346, 1585710222, 1748228465,  348556929,  871711709,
            1477462651, 1604552285,  673172061, 4182455192, 2841924606,
            3750792751, 2724776922, 1934316109, 3740695097, 1632193719,
            3915585028, 3809234032,  972078571,  430124387, 3118515724,
            3305723824, 3966767131, 2839025077, 4018983439, 1801038881,
            3186187083, 1586513064, 3124051036, 3206908706, 1700234174,
            2009338397,  368951161, 3496940778, 1309267904, 1454879460,
            1534730588, 2540462591, 3495121659, 1286436580, 2121123522,
            3803296217, 3424613127, 3263080971, 2305527327, 2153789609,
             307760308, 1162791703,  526925408, 2708783455,  591869996,
            2259056771, 2968651294, 3079688598, 1275609000, 3768382937,
            3998068236, 3714609865,  811228301,   22966662, 2046913183,
            3589828856,  450332931,  794632673, 2020057902, 3299760487,
            3470251729,  188445207, 1705528856,  817468666, 1027149306,
             544116305, 3663174674, 1578709146, 1762940491, 2447389952,
            1229001993,  203108885, 2877754547, 3030918381, 3848995443,
            1633825983, 1519190803, 4267991805, 2607622564, 3340941626,
            1854830846, 1050246808,  607338338, 3331524841,  833234909,
            2428311142, 2188549171, 3852799987, 1063052652, 2716278187,
             260504929, 1991191301, 2203905243, 3211900247, 1076062295,
             911074432, 2768884400, 3245312982,  677284819, 2531089803,
            2451416863, 1248110914, 1424518050, 3981487522, 2159851928,
            3077857143, 3217033469, 1788924090,  113479711, 4048163260,
             978959509,  801049735, 2284260051, 2448611748, 1331883434,
            3530821282, 2056990087, 3267156943, 1289431743, 3041408991,
            3371378966,  897374472, 3752213871,  167182617, 2216574346,
            3886361619, 1262723901, 2889845049,  661329052, 1992684057,
            3297093677,  602295724, 2433775366,  399145111, 3851953479,
            2345657061, 3117292882, 1408599700, 2446398117, 1416064500,
            3822296248,  499265514, 1707784044, 2282903559, 1741480693,
            2790411558,  720223207, 1894367642, 2575177054, 4280417343,
            1500041234,  945158823, 1581005662, 2441117924, 1027956854,
            4259581671, 4077432599,  689285855, 2493021881, 2131028256,
             456332468,  389749108, 2366746419, 2171146063, 1062337531,
            3755141464,   32116264, 1833502968, 2147328776, 2995179629,
            1582154861,   60770962, 3829819981, 1435614783, 2636025435,
            1131130948, 2662124059, 2476403763, 2221250819, 2034433547,
            2145091724, 3943455844, 3148913966, 3775270579, 2721124289,
            1266153044,  996285842, 1377759111,   28822593,  608877661,
            2349716629, 3572118512, 4030405590, 3040902659, 4161211660,
            1998327724, 3244322393, 1035715623, 1910619407, 2476072332,
            4243268837, 1263015503,  396760440, 3254107919, 1205143202,
            2108779348, 4114612012, 3673092820, 3304246765,  882827530,
            3069979836, 2001186664, 2493984943, 2496763897, 2482648785,
            1703577363, 1201091511, 1444732859,  361152817, 1472553314,
            3881615325, 3962413370,   25022874, 3651576874,  371022715,
            2070053952, 2368435695, 3740025884, 1129856622, 3501990541,
            3117390023, 1192726459, 1307545885, 1643032670, 2092168071,
            1896762363,  364612316, 3661401513,  326494149,  835912096,
            3072879933, 1288425070, 2386859294, 2748042775, 1182172695,
            3555382231, 3811296812, 1475947344, 2813400362, 3534982989,
            3902519615, 1509970742, 1761305367, 3337739647,  626726617,
             180983034, 3242908874,  291356705, 3436703419, 1573230175,
            1316827742, 3201634875,  534578844, 1524586559,  282553387,
            4214854791, 1739428771, 3737883509, 4124531253, 2809843337,
             641530665, 3966599242, 2378521235, 3847808362,  609156757,
              13915429,  282900100, 2878769474, 1827756868, 1988253358,
            2025607405,  509055324, 3094349309,  489684985, 3489973653,
            1970056080, 1190267878, 2444440525, 3213087554, 3978291266,
            2525337971, 3105143204,  563053816, 3378964804,  519789089,
            3178344863, 3224239129,  334890639,  789361440, 4155831518,
            2369475996, 1256759340, 2959262064, 3613020045,  754053548,
            1648290221, 3943556578, 1051201026, 1243647573, 4088893620,
            2448193223, 3443657995,  909908501, 2603993110, 2485011443,
            3739739227, 1107006222, 1096007957, 3858382409, 3443387000,
            1126627190, 3858982888, 3288399110, 3087251655, 1361918679,
            4270072291, 2241977389, 2055272974, 2336149023,  427604815,
            2416945588,  477730490, 1849166819, 2482618186, 1197010095,
            2175113125, 1974984801,  402106393, 2845848922,  413118646,
            1044459142, 2782431535, 1611038346, 3784911649, 2304516243,
            3275169024, 2985645336, 3776650801,  668172808, 2645512274,
            3387318012,  257208369, 3271560144, 4153868782, 2662593401,
            2776930762,  667310664, 2282021200, 2559188473, 1246615918,
            1417978504, 4094431725,  992705932, 1790002730, 2271317089,
            2183574056, 2975890001, 4136750819, 2317346481, 3062774483,
            3449787835,  847276118, 2310548514, 2543431891, 3159223530,
            2938181125, 2258677207, 1560997372, 1640034798, 4209071202,
            2206879854, 1422756023,  498967929, 1570994471, 2713274094,
            2641382769, 1189132077, 2955208070,  812147130, 3561410021,
            2339313903,   99232733, 3652328368,  163270809, 3787710791,
            1478717943, 1458910024, 1281535813, 2473293563, 3910849288,
            1880913121, 2264643059,  157005894, 2723897975, 1269072463,
            1127039487, 2279447155, 1599885486, 2684810945, 1871485781,
            1981006755, 1327140886, 1625152520, 3132595501, 3793357925,
            1390503937, 2333800871,  602768086, 4203128997,  338005653,
            2636137017, 4034474558,  592484300, 1157795121,  314769650,
             981204844, 1288025446, 1687965736, 2016140313, 1172298096
        };

        /// <summary>
        /// Taken from http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html,
        /// <para />
        /// SFMT-src-1.3.3.zip, SFMT.216091.out.txt.
        /// </summary>
        [TestMethod]
        public void MersenneTwisterSfmt216091UniformRandom_NextUInt_ReferenceImplementationTest()
        {
            // int ulongLength = ExpectedULong.Length;
            const int ulongLength = 100;
            var gen = new MersenneTwisterSfmt216091UniformRandom(1234);
            for (int i = 0; i < ulongLength; ++i)
            {
                ulong expected = ExpectedULong[i];
                ulong actual = gen.NextUInt();
                Assert.AreEqual(expected, actual,
                    Invariant($"Reference implementation uint: index={i}, actual={actual} expected={expected}"));
            }

            // int ulongArrayLength = ExpectedULongArray.Length;
            const int ulongArrayLength = 100;
            gen = new MersenneTwisterSfmt216091UniformRandom(new[] { 0x1234, 0x5678, 0x9abc, 0xdef0 });
            for (int i = 0; i < ulongArrayLength; ++i)
            {
                ulong expected = ExpectedULongArray[i];
                ulong actual = gen.NextUInt();
                Assert.AreEqual(expected, actual,
                    Invariant($"Reference implementation array: index={i}, actual={actual} expected={expected}"));
            }
        }

        [TestMethod]
        public void MersenneTwisterSfmt216091UniformRandom_NextDouble_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt216091UniformRandom(Seed);
            double expected = 0.5;

            double actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextDouble();
            }

            actual /= Count;
            Doubles.AreEqual(expected / actual, 1, 1e-1,
                Invariant($"Mean value 1: actual={actual}, expected={expected}"));

            expected = 4;
            actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextDouble(8);
            }

            actual /= Count;
            Doubles.AreEqual(expected / actual, 1, 1e-1,
                Invariant($"Mean value 2: actual={actual}, expected={expected}"));

            expected = 6;
            actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextDouble(4, 8);
            }

            actual /= Count;
            Doubles.AreEqual(expected / actual, 1, 1e-2,
                Invariant($"Mean value 3: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void MersenneTwisterSfmt216091UniformRandom_NextInt_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt216091UniformRandom(Seed);

            double expected = int.MaxValue / 2d;
            double actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextInt();
            }

            actual /= Count;
            Doubles.AreEqual(expected / actual, 1, 1e-1,
                Invariant($"Mean value 1: actual={actual}, expected={expected}"));

            expected = 400d;
            actual = 0d;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextInt(800);
            }

            actual /= Count;
            Doubles.AreEqual(expected / actual, 1, 1e-1,
                Invariant($"Mean value 2: actual={actual}, expected={expected}"));

            expected = 600d;
            actual = 0d;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextInt(400, 800);
            }

            actual /= Count;
            Doubles.AreEqual(expected / actual, 1, 1e-2,
                Invariant($"Mean value 3: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void MersenneTwisterSfmt216091UniformRandom_NextBoolean_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt216091UniformRandom(Seed);

            double expected = Count / 2d;
            double actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextBoolean() ? 1 : 0;
            }

            Doubles.AreEqual(expected / actual, 1, 1e-1,
                Invariant($"Mean value: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void MersenneTwisterSfmt216091UniformRandom_NextBytes_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt216091UniformRandom(Seed);

            double expected = 128;
            double actual = 0;
            var array = new byte[32];

            for (int i = 0; i < Count; ++i)
            {
                gen.NextBytes(array);
                actual = array.Aggregate(actual, (current, b) => current + b);
            }

            actual /= Count * 32;
            Doubles.AreEqual(expected / actual, 1, 1e-2,
                Invariant($"Mean value: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void MersenneTwisterSfmt216091UniformRandom_Seed_SameSeed_SameValue()
        {
            var gen1 = new MersenneTwisterSfmt216091UniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new MersenneTwisterSfmt216091UniformRandom(Seed);
            int actual = gen2.NextInt();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MersenneTwisterSfmt216091UniformRandom_Seed_DifferentSeed_DifferentValue()
        {
            var gen1 = new MersenneTwisterSfmt216091UniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new MersenneTwisterSfmt216091UniformRandom(Seed + 1);
            int actual = gen2.NextInt();

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void MersenneTwisterSfmt216091UniformRandom_CanReset_ReturnsTrue()
        {
            var gen = new MersenneTwisterSfmt216091UniformRandom();

            Assert.IsTrue(gen.CanReset);
        }

        [TestMethod]
        public void MersenneTwisterSfmt216091UniformRandom_Reset_ValueGeneratedThenReset_SameValueGenerated()
        {
            var gen = new MersenneTwisterSfmt216091UniformRandom();
            int expected = gen.NextInt();

            gen.Reset();
            int actual = gen.NextInt();

            Assert.AreEqual(expected, actual);
        }
    }
}
