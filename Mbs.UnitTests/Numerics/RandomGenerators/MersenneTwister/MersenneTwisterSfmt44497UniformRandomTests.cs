﻿using System.Linq;
using Mbs.Numerics.RandomGenerators.MersenneTwister;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

namespace Mbs.UnitTests.Numerics.RandomGenerators.MersenneTwister
{
    [TestClass]
    public class MersenneTwisterSfmt44497UniformRandomTests
    {
        private const int Seed = 12345;
        private const int Count = 5000;

        private static readonly ulong[] ExpectedULong =
        {
            3668471065UL, 3938124162UL, 4226228648UL, 1183164762UL, 959305109UL,
            2236634761UL, 1737928337UL, 1068590968UL, 3291811912UL, 438989883UL,
            3781999933UL, 3563472944UL, 2602217062UL, 3696065256UL, 4239318581UL,
            600000730UL, 307966818UL, 9789691UL, 3596262746UL, 4049798863UL,
            214000891UL, 2824657360UL, 3401599604UL, 4084185957UL, 241354882UL,
            2606104362UL, 3350316152UL, 1210189242UL, 956851300UL, 428314233UL,
            3496239949UL, 2553393773UL, 715503161UL, 1503533865UL, 2409071411UL,
            3720288303UL, 3163234373UL, 1966474779UL, 2310392540UL, 2097191938UL,
            2197028828UL, 1059701116UL, 4266659183UL, 1774483317UL, 1571386112UL,
            2168327624UL, 3672944477UL, 3063369424UL, 4223758748UL, 3685829169UL,
            1090539779UL, 620035341UL, 3365385515UL, 4222182628UL, 4183383137UL,
            594933048UL, 3450871960UL, 1352101656UL, 3902914885UL, 1731004782UL,
            2760236537UL, 2081803457UL, 2570222764UL, 4048523979UL, 248156801UL,
            2600643123UL, 1817685073UL, 1968293488UL, 3576078775UL, 981562410UL,
            1805583156UL, 1343375494UL, 3164177767UL, 2780605897UL, 2076516353UL,
            3788566596UL, 4117756860UL, 3367883848UL, 2289329031UL, 2921339930UL,
            1743986361UL, 1727106344UL, 1833850427UL, 3750676455UL, 4176756476UL,
            3138275147UL, 3834722234UL, 253290855UL, 4159875912UL, 333560160UL,
            289945216UL, 3065004471UL, 3119224239UL, 3950217856UL, 750566052UL,
            1879854821UL, 1079185965UL, 418199396UL, 3605518556UL, 2475870057UL,
            1215532958UL, 3953209850UL, 1069578365UL, 2561700523UL, 461233929UL,
            571306934UL, 1665480644UL, 2352745674UL, 2333284139UL, 816689027UL,
            521695839UL, 577677178UL, 925115487UL, 3268605454UL, 2925280507UL,
            818376724UL, 2765037170UL, 3387701710UL, 2493131447UL, 1373426262UL,
            957043528UL, 231214681UL, 1479541201UL, 3677505186UL, 418322763UL,
            2780533861UL, 3595572878UL, 856130714UL, 1300256770UL, 4157344003UL,
            3191677717UL, 2138486720UL, 1215914067UL, 3687237744UL, 1931891023UL,
            1561352010UL, 1172418912UL, 1506554008UL, 2031661455UL, 2077744726UL,
            4000638947UL, 2157398374UL, 2116801448UL, 1167153875UL, 3658311631UL,
            2264923400UL, 4086619644UL, 238096042UL, 2052176473UL, 3721012419UL,
            1083371007UL, 2528909843UL, 245849814UL, 4293498808UL, 3557294589UL,
            519744913UL, 1054550756UL, 2626726854UL, 3385911002UL, 3094744326UL,
            2052448271UL, 4198985573UL, 3252308784UL, 4251495372UL, 2820713376UL,
            89026744UL, 1343444563UL, 2491264117UL, 4015221172UL, 2925816909UL,
            4111900412UL, 3806669100UL, 1230222486UL, 1968470262UL, 52328799UL,
            634244867UL, 2431822557UL, 316017391UL, 333253817UL, 2768339044UL,
            4003653076UL, 940576000UL, 1181928687UL, 1147879332UL, 3857662527UL,
            4176040643UL, 881145720UL, 242444761UL, 1224124983UL, 4251960466UL,
            2921064958UL, 1447744715UL, 2157496962UL, 3832193337UL, 2972646800UL,
            3797899393UL, 2430635309UL, 463281550UL, 820073350UL, 96282779UL,
            3054747094UL, 2277108126UL, 1914931552UL, 79961237UL, 1244810248UL,
            468360831UL, 864476736UL, 3168112817UL, 2604375886UL, 3752023769UL,
            3822478969UL, 3392767416UL, 2707517232UL, 4293931739UL, 1194024122UL,
            2563256986UL, 3634780466UL, 2800677793UL, 1703243209UL, 2301298236UL,
            1934593057UL, 3510926096UL, 3984334374UL, 3188915212UL, 2799848510UL,
            4007731498UL, 2301115727UL, 1383343593UL, 286166726UL, 1217889422UL,
            191404597UL, 4219647274UL, 178357379UL, 1116506706UL, 725250649UL,
            4131865415UL, 3983608816UL, 770535705UL, 3966813426UL, 4025075453UL,
            557662654UL, 4175024917UL, 2563492750UL, 3832556290UL, 1870625229UL,
            4196007234UL, 1168212900UL, 3516487257UL, 1566560411UL, 1548852392UL,
            3253707787UL, 4118892983UL, 3863430210UL, 3200390603UL, 4054130806UL,
            2053917664UL, 3904403307UL, 845564479UL, 861714700UL, 996396981UL,
            1865180888UL, 468309921UL, 1413915976UL, 632944325UL, 1908083025UL,
            1380877218UL, 2677708929UL, 3815178898UL, 3910439272UL, 3423348286UL,
            1966664493UL, 3937230547UL, 3541458131UL, 3056762916UL, 2424045303UL,
            557061908UL, 2943212109UL, 632696247UL, 3670035387UL, 4077524513UL,
            3094691621UL, 2684463965UL, 1824263502UL, 2648204785UL, 1288166405UL,
            78047510UL, 1471683082UL, 661249222UL, 1951403822UL, 1686323466UL,
            3210289579UL, 2759787000UL, 4238366569UL, 300775971UL, 1932659812UL,
            4024710497UL, 1751327592UL, 2974591301UL, 1211502932UL, 1872848709UL,
            2867646014UL, 2202022915UL, 3686335429UL, 2460518686UL, 3248965230UL,
            3856557812UL, 1689347388UL, 30556487UL, 2723355731UL, 153373155UL,
            2560775636UL, 1966083226UL, 1838282549UL, 3843502231UL, 874259385UL,
            3419152363UL, 1597881641UL, 3196758215UL, 3198743754UL, 2574241613UL,
            2665414716UL, 2918606421UL, 957032739UL, 1063315021UL, 3938418125UL,
            3089328447UL, 1826591805UL, 3821204021UL, 1334787263UL, 1150738420UL,
            1814731952UL, 2459596315UL, 4178131701UL, 2768857647UL, 2315022757UL,
            2578240716UL, 491666359UL, 2597859143UL, 702138144UL, 2286627628UL,
            2754943374UL, 1584816573UL, 399610346UL, 1347175572UL, 518494131UL,
            1304124989UL, 1469415010UL, 110390133UL, 3771042229UL, 3557573144UL,
            4284386549UL, 4211194656UL, 2510057277UL, 2306536991UL, 711443859UL,
            1533321010UL, 1541982713UL, 1073817423UL, 1921756124UL, 827772977UL,
            4065010113UL, 1603869810UL, 2392272617UL, 2114562047UL, 4155255484UL,
            1627005278UL, 3771108335UL, 2895984492UL, 904975369UL, 1733328370UL,
            709652621UL, 530614145UL, 460062668UL, 3691684513UL, 1299478976UL,
            3836789414UL, 486842819UL, 1872267498UL, 4070567976UL, 3319570519UL,
            2128277197UL, 2388268478UL, 240108571UL, 4125625515UL, 4270141283UL,
            3644055815UL, 3115455195UL, 3822144200UL, 3360812198UL, 1037290037UL,
            909010578UL, 4239505225UL, 2829222507UL, 1762355225UL, 3539505380UL,
            238983232UL, 474391724UL, 3874036674UL, 205757016UL, 968202594UL,
            3124476026UL, 1222960157UL, 1329830214UL, 1090395383UL, 2204442551UL,
            4277701969UL, 2328748412UL, 1284334994UL, 867221060UL, 92017439UL,
            1024793394UL, 3450371731UL, 3139741656UL, 99876861UL, 3601757810UL,
            2586165476UL, 2664361471UL, 469681386UL, 1677699172UL, 1229428870UL,
            774289262UL, 4202235456UL, 2343626992UL, 3402393540UL, 830439819UL,
            2191812394UL, 2699126692UL, 1908336401UL, 2481705494UL, 434428810UL,
            3393783746UL, 2815077229UL, 2703098120UL, 2025880350UL, 2185716097UL,
            250306694UL, 2879624656UL, 3537556270UL, 3215644148UL, 1499821968UL,
            1061450939UL, 1776261558UL, 16810303UL, 4163970943UL, 2024401413UL,
            2772604644UL, 901847589UL, 2380032337UL, 3456929058UL, 3341693939UL,
            1201666757UL, 1563664807UL, 2280779497UL, 1641443561UL, 700851793UL,
            3264062932UL, 2962816317UL, 1603877007UL, 177453634UL, 467029654UL,
            332285038UL, 587207529UL, 3671911212UL, 1277709384UL, 3288458872UL,
            2411528263UL, 3426823463UL, 3532671990UL, 4134452986UL, 4025767448UL,
            547335263UL, 3164869181UL, 3970808498UL, 97375206UL, 3356405786UL,
            3098223729UL, 4190773327UL, 200935111UL, 3954015694UL, 1383984572UL,
            956182239UL, 3809831195UL, 3362013645UL, 163750531UL, 2424243341UL,
            2093128576UL, 4146298994UL, 1906609787UL, 2154154611UL, 3668124640UL,
            1766927907UL, 2025241589UL, 4222689844UL, 100428254UL, 1294821014UL,
            4057102939UL, 1232150499UL, 1720414681UL, 3965983610UL, 2431630630UL,
            3383370832UL, 304453348UL, 520914697UL, 2951903556UL, 3736806694UL,
            3639052774UL, 3746640371UL, 2883128319UL, 444821350UL, 3276341492UL,
            1992708679UL, 3472126898UL, 2697357850UL, 2232331553UL, 568502379UL,
            2774444761UL, 671721204UL, 3508505965UL, 237966378UL, 4195028471UL,
            2825154392UL, 1125119371UL, 3069427307UL, 2209537752UL, 3983536457UL,
            903433738UL, 2520145936UL, 2220561995UL, 12907436UL, 94760256UL,
            4177472245UL, 3752630479UL, 1469918497UL, 1041799071UL, 704849474UL,
            1235395033UL, 1388272685UL, 288911159UL, 486367835UL, 1443018700UL,
            1800251476UL, 1479959892UL, 3700628490UL, 599987231UL, 111397546UL,
            3309445586UL, 394194964UL, 3434451911UL, 3859141113UL, 576940132UL,
            1552446671UL, 2646402026UL, 1879677356UL, 4154520027UL, 3039676590UL,
            120886928UL, 3055291763UL, 2506115127UL, 2688481669UL, 2474502957UL,
            2632911205UL, 1773984050UL, 3199155193UL, 1079287375UL, 3662861927UL,
            460686384UL, 396728453UL, 1073792880UL, 3471944918UL, 719136261UL,
            3283394898UL, 3425628413UL, 4104238366UL, 463876074UL, 162022272UL,
            4130133370UL, 4200448482UL, 3351069702UL, 4274221219UL, 3903841308UL,
            1701983564UL, 1804655700UL, 2726177665UL, 4183879629UL, 2078583656UL,
            2755132613UL, 3578527073UL, 1515470958UL, 1404769395UL, 604115077UL,
            648361734UL, 765091477UL, 1486420245UL, 3026361116UL, 4010433049UL,
            202251369UL, 2798176874UL, 3827295270UL, 2923565899UL, 3934195075UL,
            1353795733UL, 619331940UL, 3673734204UL, 4129009467UL, 671492969UL,
            396116589UL, 3312560796UL, 24100167UL, 2963949118UL, 731654588UL,
            4023163045UL, 1185021024UL, 3755508734UL, 238340499UL, 562366552UL,
            2294446540UL, 1788417929UL, 609470783UL, 4135169103UL, 3057117800UL,
            3904842512UL, 2384470391UL, 2322551780UL, 869908773UL, 2071054519UL,
            4025608343UL, 2017359745UL, 747884988UL, 3506255313UL, 814709863UL,
            1109473400UL, 670903697UL, 3818189624UL, 586014739UL, 4031605358UL,
            3932641706UL, 2912737692UL, 949208089UL, 2535979868UL, 3492127502UL,
            3519759236UL, 2907912894UL, 1171839275UL, 149869870UL, 1551897769UL,
            3388140819UL, 530103856UL, 1061165230UL, 3843932369UL, 2440047826UL,
            3295199596UL, 2308733431UL, 3852368188UL, 517851268UL, 2437724713UL,
            609166024UL, 1235745525UL, 3792552259UL, 1856620882UL, 701654446UL,
            763346753UL, 157367246UL, 3902279574UL, 2133026351UL, 17361257UL,
            3289613377UL, 3673527696UL, 972720319UL, 825113425UL, 4270860334UL,
            3563897014UL, 2736989862UL, 1136644116UL, 2834015816UL, 2091856750UL,
            2882295327UL, 3688068604UL, 764250835UL, 666382379UL, 3904626950UL,
            3292192469UL, 2261829517UL, 4178989490UL, 811424867UL, 1055674996UL,
            1739590022UL, 91235693UL, 1317700293UL, 991872191UL, 3872846511UL,
            2212748309UL, 2415463196UL, 1679761841UL, 2059549608UL, 2396500149UL,
            2872048880UL, 2290246831UL, 253301488UL, 567254774UL, 3000195235UL,
            934780318UL, 1792446648UL, 1741686869UL, 4152856444UL, 4099647021UL,
            2977466397UL, 3737476792UL, 985753970UL, 1099626078UL, 2220150905UL,
            3820039986UL, 2727104245UL, 1341851333UL, 1166622599UL, 76524896UL,
            2816878482UL, 2420005686UL, 2796966347UL, 1807652744UL, 1106141956UL,
            2968784517UL, 3881748460UL, 3620841976UL, 3723856769UL, 2730519008UL,
            2812775155UL, 3287045090UL, 4190833598UL, 264893300UL, 2409752701UL,
            1631108493UL, 1631100009UL, 1307937757UL, 2253064226UL, 4243107387UL,
            400043454UL, 1795477000UL, 1285395115UL, 3789707549UL, 3478508813UL,
            670359955UL, 82110046UL, 3062186815UL, 736049302UL, 791902270UL,
            3013859461UL, 89725297UL, 3501974273UL, 1327213906UL, 720937231UL,
            1558736874UL, 2501022772UL, 885956349UL, 3634031972UL, 247951155UL,
            4246010942UL, 1482542578UL, 2045299900UL, 3098535940UL, 328300839UL,
            1266315543UL, 2485122502UL, 2515642934UL, 1941597329UL, 2141425621UL,
            858009460UL, 1800572110UL, 2694121840UL, 2907168710UL, 3177698792UL,
            3764081722UL, 2989883712UL, 620540482UL, 625518192UL, 716543246UL,
            3317582704UL, 2155036046UL, 209487585UL, 489137596UL, 1948890051UL,
            3843819351UL, 2285115188UL, 3919128084UL, 3325426356UL, 4154907130UL,
            1997316562UL, 3188326029UL, 2436085004UL, 4072050003UL, 924611992UL,
            361545429UL, 164330605UL, 757417108UL, 4237276716UL, 415808197UL,
            2078811012UL, 1583856346UL, 3808319704UL, 578996186UL, 1441176245UL,
            2417150484UL, 1938835031UL, 891301201UL, 7234411UL, 1201514993UL,
            2731743807UL, 1146735750UL, 1820386225UL, 97698457UL, 2790714152UL,
            3848237689UL, 2715879657UL, 1024441545UL, 2458068994UL, 3453072562UL,
            1755444989UL, 716147771UL, 3285652666UL, 3788760884UL, 4250706482UL,
            687645990UL, 3028317522UL, 2344486950UL, 2997970741UL, 4155340582UL,
            1465732681UL, 1386659782UL, 2471813121UL, 2861504206UL, 919935469UL,
            1785892173UL, 3554336178UL, 315273598UL, 2517441760UL, 1137546813UL,
            3061951994UL, 2342366659UL, 1039911139UL, 859018891UL, 2675508108UL,
            2966918334UL, 3581057550UL, 3850447785UL, 4186090558UL, 1991360108UL,
            362343044UL, 563103072UL, 4226070610UL, 4074153608UL, 4209763434UL,
            2092329502UL, 260176025UL, 1226404159UL, 2098258390UL, 970009559UL,
            707151395UL, 1915000160UL, 1317604528UL, 4042264273UL, 2026976800UL,
            1669450950UL, 2280675081UL, 355032296UL, 1435313411UL, 4288027938UL,
            3112975881UL, 4249994558UL, 2023866087UL, 2533310216UL, 1508811118UL,
            3916687231UL, 1446316669UL, 916164280UL, 650128889UL, 2908097559UL,
            4184733170UL, 147848713UL, 3505797198UL, 2898326314UL, 657351178UL,
            3239217236UL, 3250003594UL, 429788236UL, 3659309536UL, 2870420702UL,
            2632220419UL, 645284771UL, 2098842906UL, 348489627UL, 1777817617UL,
            1053425496UL, 644535629UL, 2139719735UL, 347947737UL, 4101346312UL,
            3067206190UL, 2822292569UL, 2199011214UL, 476949484UL, 1119224820UL,
            775333821UL, 554748861UL, 4064902759UL, 4266570770UL, 1546548501UL,
            2526237667UL, 2803082123UL, 1962577111UL, 3305312830UL, 1807838801UL,
            2436907200UL, 1651927029UL, 1244830606UL, 727876668UL, 3232450407UL,
            1104953743UL, 92449386UL, 4281939925UL, 3380390821UL, 2356276908UL,
            3477029072UL, 2150703187UL, 763377102UL, 3357720372UL, 960125101UL,
            3195485160UL, 49716104UL, 277963460UL, 1681312873UL, 2123240963UL,
            2227465138UL, 2016614367UL, 2842184996UL, 1192258763UL, 2784706689UL,
            1672627768UL, 2660365442UL, 1240803123UL, 353830762UL, 3372517974UL,
            1965407360UL, 954894947UL, 40133935UL, 1152324029UL, 392954500UL,
            15026379UL, 417100912UL, 2261871275UL, 1926518514UL, 3638741165UL,
            1110641020UL, 2742698958UL, 710920372UL, 2424938611UL, 145119866UL,
            1406562238UL, 90257725UL, 1129334365UL, 2205447699UL, 1352826484UL,
            2219220808UL, 1814531525UL, 772385308UL, 117336543UL, 1848169948UL,
            2357308811UL, 3863089713UL, 2967687149UL, 2764225167UL, 34438731UL,
            1771178970UL, 2467406279UL, 96500709UL, 2427662038UL, 1527860408UL,
            2488803444UL, 1920376085UL, 2470587471UL, 3097984093UL, 3319609258UL,
            3560925349UL, 2481438710UL, 485377602UL, 3012935809UL, 2728787840UL,
            2537282231UL, 487321294UL, 1759605900UL, 789200824UL, 46017448UL,
            1483092082UL, 1895679637UL, 9122740UL, 635864575UL, 320732971UL,
            4253159584UL, 30097521UL, 839233316UL, 1431693534UL, 645981752UL,
        };

        private static readonly ulong[] ExpectedULongArray =
        {
            684975361UL, 2487942892UL, 4151500063UL, 54722954UL, 342503900UL,
            2056596948UL, 2988817246UL, 4115463554UL, 3909146713UL, 2954953855UL,
            2392358767UL, 2805985593UL, 337683357UL, 3744843600UL, 4062780564UL,
            2849031419UL, 3311297738UL, 2073551555UL, 2645431070UL, 3521908773UL,
            2525510316UL, 563603035UL, 1358376030UL, 2464853050UL, 2212361572UL,
            1897669050UL, 1341168415UL, 2663496237UL, 3730590905UL, 2177714665UL,
            3708408UL, 1017773362UL, 3153611644UL, 255331818UL, 2371411126UL,
            3779915620UL, 1698275394UL, 1470906491UL, 1266552795UL, 2841612460UL,
            2099156677UL, 1657011499UL, 3832220803UL, 1719456599UL, 684450823UL,
            1586753818UL, 1074981324UL, 1415783495UL, 3674439465UL, 2677731497UL,
            3922465187UL, 996394686UL, 3895252050UL, 665404286UL, 563289119UL,
            1619890258UL, 1449533762UL, 3120934807UL, 2067107528UL, 1055630497UL,
            1032879739UL, 3151004457UL, 3542785958UL, 4168316829UL, 2519834186UL,
            3306413181UL, 3832405685UL, 2257061362UL, 2360361651UL, 2322816283UL,
            936713972UL, 3366713329UL, 329082198UL, 1237646659UL, 461119731UL,
            1855601012UL, 2600763382UL, 697883013UL, 1302517116UL, 932253293UL,
            1600171040UL, 3193927591UL, 4197726818UL, 2490578457UL, 4130874991UL,
            4242899165UL, 3725337733UL, 363161476UL, 3084505702UL, 3247499688UL,
            975533818UL, 3710151092UL, 2765291830UL, 1421623829UL, 914407309UL,
            1845701572UL, 4112553229UL, 2049444703UL, 991296597UL, 3615367192UL,
            2220595495UL, 3878745150UL, 857488219UL, 902083463UL, 114172732UL,
            1942374497UL, 2345473985UL, 1959808724UL, 2693959659UL, 1873555581UL,
            2480343508UL, 287169754UL, 2737441125UL, 2258749576UL, 2497218716UL,
            1663030569UL, 787017291UL, 4076291104UL, 710825601UL, 1192142920UL,
            3819099754UL, 2796134222UL, 1353840633UL, 1375260846UL, 2280539058UL,
            3521884290UL, 2790934703UL, 2788957726UL, 2565659212UL, 4095721521UL,
            3320649137UL, 2699360917UL, 1562966751UL, 1130447114UL, 2903910056UL,
            1171391383UL, 3497813995UL, 1607965401UL, 1935680607UL, 1197803692UL,
            128258597UL, 765135286UL, 2169079268UL, 2151663592UL, 4184115523UL,
            775299448UL, 2824961602UL, 1399440855UL, 1273632402UL, 1276253509UL,
            3495617889UL, 1217642205UL, 56477934UL, 1590819929UL, 2270347856UL,
            745605971UL, 4048203448UL, 2756166044UL, 3004303000UL, 3115093450UL,
            1127867080UL, 2869792922UL, 2408572067UL, 495574931UL, 3655945042UL,
            2123108497UL, 51567216UL, 803469706UL, 690135403UL, 1732397580UL,
            3087105235UL, 1841102639UL, 1527639759UL, 3899711070UL, 2542743308UL,
            1698985801UL, 9350005UL, 922919897UL, 3978083185UL, 3686269289UL,
            2753241014UL, 3700697546UL, 2176984517UL, 2592770926UL, 3324127759UL,
            3140135044UL, 398927337UL, 1995747171UL, 2439522503UL, 3235962603UL,
            3314578837UL, 948434804UL, 3887275779UL, 2903321385UL, 879793513UL,
            3344634253UL, 3505357122UL, 2888691656UL, 492079120UL, 693932803UL,
            999907939UL, 3356399207UL, 4022936501UL, 3376323365UL, 787569271UL,
            890637583UL, 3301988790UL, 2968470994UL, 3218607885UL, 1166122209UL,
            926291418UL, 4099445588UL, 778467247UL, 3743982117UL, 2022062016UL,
            2903184875UL, 4187830769UL, 799823061UL, 3221414271UL, 3623859198UL,
            2519869985UL, 667057891UL, 874655225UL, 2518696247UL, 4040370784UL,
            2241464701UL, 426790262UL, 1763346894UL, 488019312UL, 3222126248UL,
            4207331182UL, 335346595UL, 4226852268UL, 276331012UL, 3577109997UL,
            172016932UL, 3817060878UL, 779137378UL, 1759784420UL, 3768126519UL,
            1060129497UL, 1177151560UL, 1681823301UL, 1138043988UL, 385137952UL,
            3079606477UL, 4043088157UL, 3912726260UL, 1705078374UL, 1686645242UL,
            3954418529UL, 1550739145UL, 82508189UL, 1233872303UL, 1731274661UL,
            2614638744UL, 3680529200UL, 3822612437UL, 1648886489UL, 3761017431UL,
            636291140UL, 536886849UL, 1267695503UL, 165271750UL, 1218857469UL,
            3796110535UL, 3749066613UL, 1421452984UL, 485039738UL, 3912706166UL,
            24258852UL, 1646289630UL, 2758488579UL, 631040720UL, 3446088674UL,
            1690145059UL, 2114593518UL, 1102292481UL, 20334757UL, 1421842045UL,
            3394074090UL, 1354124663UL, 987615861UL, 2799990871UL, 2174635609UL,
            34662850UL, 2487615774UL, 4182921436UL, 405423782UL, 3671831829UL,
            1259386906UL, 2221952747UL, 434917264UL, 746327640UL, 3169535910UL,
            2871122554UL, 3292584106UL, 2234401265UL, 1320639855UL, 19293933UL,
            2970905356UL, 715449597UL, 562725345UL, 3180272122UL, 3052942630UL,
            3720180016UL, 3976109710UL, 3374348864UL, 1083429223UL, 1386112314UL,
            4196366891UL, 1531536373UL, 1762082785UL, 846692171UL, 4261231491UL,
            2270559091UL, 4186110629UL, 4014655319UL, 4096629750UL, 897171642UL,
            2974939145UL, 4173036245UL, 833494220UL, 857259553UL, 664876368UL,
            312038353UL, 2414814982UL, 3128523230UL, 2940054013UL, 1943209572UL,
            63223506UL, 3351103993UL, 966139776UL, 1787218600UL, 3519333609UL,
            3366381182UL, 2261216540UL, 403616014UL, 3631074671UL, 1801703243UL,
            287742111UL, 1047211510UL, 3236430376UL, 857724104UL, 3913815258UL,
            2173081363UL, 92984431UL, 2670026167UL, 4211010100UL, 2024406728UL,
            501007146UL, 189488668UL, 2141434335UL, 324334037UL, 1635101308UL,
            2456311806UL, 3374778413UL, 2621277156UL, 1609449907UL, 1628993583UL,
            236437798UL, 1292255819UL, 605962801UL, 1937844131UL, 221923282UL,
            2948575326UL, 3454587280UL, 971479933UL, 3542163502UL, 1697837850UL,
            32422817UL, 3223284882UL, 845152623UL, 1584490692UL, 3086530488UL,
            1923024297UL, 1455829718UL, 4100655553UL, 2542626342UL, 1391459697UL,
            2416406726UL, 770691897UL, 1162088077UL, 446047331UL, 2988521380UL,
            2496235159UL, 3237970291UL, 3354753208UL, 4208541968UL, 4109013479UL,
            753355965UL, 3076153711UL, 724837335UL, 497842836UL, 792686325UL,
            2799292337UL, 58448095UL, 72367036UL, 93387345UL, 3801061135UL,
            351535252UL, 3480371512UL, 4157060233UL, 2986630890UL, 1389831856UL,
            1536904354UL, 759093557UL, 2168801837UL, 2112892969UL, 589384258UL,
            4266026414UL, 1591084529UL, 2865362337UL, 2568148776UL, 3803893019UL,
            4099023039UL, 1523326035UL, 3298237325UL, 3957084972UL, 60920028UL,
            1098333253UL, 319031688UL, 2926386528UL, 3824665159UL, 364742618UL,
            3272865002UL, 300767042UL, 2293701793UL, 1604130009UL, 774792687UL,
            2620876029UL, 3383211711UL, 179271372UL, 906536187UL, 1393265969UL,
            4059077578UL, 3145239434UL, 3249711767UL, 2618421236UL, 1630455841UL,
            485435478UL, 2560017961UL, 2605151120UL, 1482891609UL, 380322578UL,
            1359433822UL, 4079826681UL, 3833759552UL, 413985578UL, 1920228571UL,
            1773134197UL, 2179571509UL, 4078859619UL, 2330769072UL, 1733337870UL,
            3328658980UL, 832747128UL, 1563770656UL, 969223214UL, 1840793897UL,
            3659929592UL, 3278579848UL, 3275871712UL, 310479483UL, 400262102UL,
            1770939224UL, 437274814UL, 2950215871UL, 347749928UL, 4019277934UL,
            2320073534UL, 2283017209UL, 1023601518UL, 3482767365UL, 3902131696UL,
            3422852160UL, 2464812985UL, 2976221138UL, 1651023701UL, 1050460931UL,
            199848361UL, 3296851303UL, 1459955097UL, 771011185UL, 4220052530UL,
            1813544374UL, 1370255200UL, 4190864819UL, 3727442680UL, 3545208544UL,
            1831809223UL, 1395491272UL, 2027579389UL, 1669401598UL, 2292078490UL,
            554108218UL, 3274751565UL, 1768284236UL, 2079430058UL, 1495165627UL,
            1433502343UL, 3054057761UL, 1413762911UL, 3405340896UL, 2955704881UL,
            3717955534UL, 966755105UL, 4005384894UL, 1748340606UL, 1037087026UL,
            1614468879UL, 1179006662UL, 3923511915UL, 4124386352UL, 3443912930UL,
            3500765253UL, 12337586UL, 666110904UL, 969847404UL, 3878185809UL,
            2070954448UL, 1161710505UL, 4241205957UL, 1337222355UL, 1652663555UL,
            406710859UL, 3136093625UL, 803137086UL, 4294390171UL, 1011217971UL,
            3531173898UL, 3944724422UL, 13297832UL, 3728052176UL, 3613185933UL,
            988598314UL, 709061770UL, 3060669882UL, 1459547049UL, 3504836811UL,
            1060864165UL, 4067037856UL, 2138554249UL, 3073017993UL, 1041861495UL,
            1447529993UL, 1785141820UL, 4098979843UL, 1648224543UL, 2490097646UL,
            65209449UL, 3680651012UL, 2693284107UL, 984759948UL, 3251406596UL,
            370590747UL, 3243196406UL, 1785731763UL, 590283726UL, 1963439114UL,
            176186720UL, 175331635UL, 575814080UL, 2284386170UL, 3442614813UL,
            603858074UL, 4276303569UL, 619129767UL, 1394525629UL, 3843731906UL,
            2831203904UL, 1314320458UL, 2557431506UL, 2568469869UL, 1135134494UL,
            2377656107UL, 1071106524UL, 3698650747UL, 3291589750UL, 519683978UL,
            1914121510UL, 2979993925UL, 3994217106UL, 1574831031UL, 247704662UL,
            2416291830UL, 1016776283UL, 3974839830UL, 3180073839UL, 803275325UL,
            2543778236UL, 1734477206UL, 2553683793UL, 3178076968UL, 3367398574UL,
            667329297UL, 3737662587UL, 3854573846UL, 561559009UL, 2489817655UL,
            2334454110UL, 679269539UL, 2349179569UL, 3735225237UL, 653957767UL,
            3555137137UL, 3894601347UL, 3152974963UL, 4139069232UL, 2240781730UL,
            523688285UL, 3166167015UL, 3471800877UL, 955125274UL, 107478146UL,
            3013140726UL, 1640264073UL, 3971825508UL, 3490189286UL, 2285435046UL,
            3573578481UL, 3250872938UL, 744654755UL, 3436231084UL, 921129838UL,
            811891477UL, 3607582846UL, 2918438798UL, 3246286893UL, 3912591629UL,
            1274726348UL, 3514828575UL, 475164486UL, 3268749252UL, 839610326UL,
            957142402UL, 1667457940UL, 3786171593UL, 1270963994UL, 580648699UL,
            2872993498UL, 2991141990UL, 3425309289UL, 3936025456UL, 2158743955UL,
            384630373UL, 517652940UL, 3513584012UL, 810054900UL, 2745664097UL,
            1570635852UL, 2074484253UL, 677606390UL, 4009443607UL, 1385781894UL,
            2393669976UL, 165686249UL, 1496450589UL, 4013865530UL, 2290621688UL,
            3385357180UL, 2210629116UL, 4148597911UL, 1202809305UL, 455672086UL,
            4048940332UL, 726424906UL, 2269285382UL, 3316666291UL, 250231004UL,
            2462114401UL, 2308635961UL, 3943593113UL, 1266704610UL, 1163967864UL,
            828848921UL, 287923784UL, 1938913292UL, 1929880277UL, 660026720UL,
            1694073064UL, 2327608261UL, 686131676UL, 3571825177UL, 1310090872UL,
            843906573UL, 2319937167UL, 528434635UL, 2049406819UL, 3373034099UL,
            2288595656UL, 4123538177UL, 820252657UL, 271521983UL, 28292127UL,
            1239301430UL, 58272310UL, 3455007578UL, 3839850828UL, 3883402667UL,
            4060701226UL, 597088921UL, 4078456301UL, 2519171459UL, 3626900665UL,
            52212125UL, 3120331907UL, 159131871UL, 985166730UL, 2236824863UL,
            4060195572UL, 217208747UL, 3749149192UL, 4269940571UL, 1490968864UL,
            290092356UL, 212271720UL, 1078395790UL, 306551567UL, 3569154778UL,
            3918766124UL, 1715019761UL, 2022281943UL, 3894237181UL, 1685061613UL,
            3613570500UL, 3884854785UL, 1746685699UL, 1974934664UL, 3109086778UL,
            191094725UL, 2026320531UL, 858695557UL, 1227189640UL, 4209480691UL,
            4119128751UL, 469840833UL, 3952391967UL, 3525709291UL, 531652111UL,
            917040323UL, 590539629UL, 3280239642UL, 2289215332UL, 569706427UL,
            1889832844UL, 3310878393UL, 2396092275UL, 1172707390UL, 312670280UL,
            3015965505UL, 2063451345UL, 3138590088UL, 3591167004UL, 4145089791UL,
            4168674656UL, 166403028UL, 856460074UL, 859310504UL, 822163878UL,
            1867811711UL, 841721884UL, 227849240UL, 3851247325UL, 59916961UL,
            2590964017UL, 122077343UL, 1907758232UL, 610616835UL, 96783291UL,
            2650533543UL, 837605791UL, 1082097965UL, 2010518271UL, 2620266369UL,
            1066209021UL, 2534182964UL, 779292692UL, 1298963853UL, 3611682883UL,
            362586592UL, 1849409613UL, 2833380477UL, 4018736478UL, 1881193151UL,
            4000743062UL, 1789015738UL, 325371520UL, 1626243809UL, 3545161922UL,
            3098959584UL, 293926097UL, 3748930635UL, 2379584738UL, 310424004UL,
            8219353UL, 191285632UL, 72013098UL, 508867080UL, 2524106895UL,
            1173604872UL, 1746821946UL, 1299664703UL, 1489155837UL, 2736533793UL,
            1878394623UL, 235447201UL, 876569140UL, 777848722UL, 3710031465UL,
            445308600UL, 1306046325UL, 3169511728UL, 3002405582UL, 2906319429UL,
            1017065573UL, 2391738845UL, 1632204455UL, 1915589132UL, 1293139389UL,
            3955649589UL, 3718614309UL, 900446372UL, 1404116789UL, 4000130860UL,
            2484929064UL, 912006813UL, 3525225278UL, 1613317968UL, 537734584UL,
            3958185848UL, 2349727129UL, 82304607UL, 3017584172UL, 2177195469UL,
            2911722307UL, 425625638UL, 334461051UL, 3251097319UL, 934896212UL,
            800472108UL, 341276622UL, 2700905245UL, 3990616448UL, 641387636UL,
            1209957780UL, 236027388UL, 3337048604UL, 2312820416UL, 450262488UL,
            4246816041UL, 1990087069UL, 621901267UL, 3761510927UL, 308535352UL,
            2423363391UL, 799259935UL, 3536255815UL, 1517219757UL, 3251042517UL,
            1609771552UL, 670418175UL, 3004508582UL, 3801331787UL, 674369786UL,
            2459102211UL, 3472827872UL, 1524857071UL, 2981241625UL, 1209482347UL,
            3507570951UL, 1022898042UL, 4238119858UL, 2990929867UL, 3737869670UL,
            1783005784UL, 3956102596UL, 893145358UL, 4248333549UL, 4126981715UL,
            970294753UL, 3782823697UL, 1793671802UL, 3583687220UL, 3562326438UL,
            444934810UL, 3248763868UL, 1065283618UL, 1661369025UL, 3056949600UL,
            706764867UL, 575458674UL, 3036992317UL, 2163501640UL, 3381714343UL,
            595721710UL, 77458567UL, 1149744974UL, 4225463217UL, 971933200UL,
            2874514061UL, 4247146911UL, 2846911276UL, 3472989449UL, 3618689153UL,
            3136621753UL, 2220597035UL, 2453395120UL, 3166114260UL, 1419856729UL,
            2073563868UL, 2232629242UL, 2665380307UL, 1831624028UL, 4065571792UL,
            3228887488UL, 4052810701UL, 3673096883UL, 2782398940UL, 3464368053UL,
            3976902066UL, 214378622UL, 3144291184UL, 3831753728UL, 3426318004UL,
            99629543UL, 2579257929UL, 175451450UL, 3145770440UL, 3980286744UL,
            1987265694UL, 2631682253UL, 3240699503UL, 2911487256UL, 4215555091UL,
            1907916097UL, 3068515022UL, 1214348574UL, 4087495565UL, 3841307810UL,
            3133855061UL, 2896148255UL, 1452464920UL, 3295295973UL, 4014205741UL,
            3983387873UL, 4278530705UL, 1285590983UL, 2486012196UL, 2645424275UL,
            4088015497UL, 1011201408UL, 956627565UL, 104107756UL, 249913506UL,
            879980540UL, 2505016230UL, 1144832662UL, 3262987074UL, 853625579UL,
            2545491673UL, 1607844245UL, 1657493894UL, 4258031523UL, 3494981418UL,
            3245738256UL, 2872824830UL, 3102640800UL, 3934790280UL, 1215485083UL,
            2165576981UL, 691171919UL, 890142672UL, 320933457UL, 3933525051UL,
            2492443562UL, 2246064783UL, 2661424963UL, 1581891487UL, 1271384172UL,
            1461803060UL, 3471828900UL, 1332453373UL, 288105585UL, 2299919311UL,
            167904102UL, 1820959195UL, 2866722432UL, 2052650573UL, 3406613036UL,
            2679795393UL, 3278648081UL, 3378363089UL, 4164467575UL, 726954514UL,
            1866835396UL, 1199611926UL, 2185729502UL, 274315749UL, 453317054UL,
        };

        /// <summary>
        /// Taken from http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html,
        /// SFMT-src-1.3.3.zip, SFMT.44497.out.txt.
        /// </summary>
        [TestMethod]
        public void MersenneTwisterSfmt44497UniformRandom_NextUInt_ReferenceImplementationTest()
        {
            const int ulongLength = 100;
            var gen = new MersenneTwisterSfmt44497UniformRandom(1234);
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
            gen = new MersenneTwisterSfmt44497UniformRandom(new[] { 0x1234, 0x5678, 0x9abc, 0xdef0 });
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
        public void MersenneTwisterSfmt44497UniformRandom_NextDouble_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt44497UniformRandom(Seed);
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
        public void MersenneTwisterSfmt44497UniformRandom_NextInt_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt44497UniformRandom(Seed);

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
        public void MersenneTwisterSfmt44497UniformRandom_NextBoolean_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt44497UniformRandom(Seed);

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
        public void MersenneTwisterSfmt44497UniformRandom_NextBytes_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new MersenneTwisterSfmt44497UniformRandom(Seed);

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
        public void MersenneTwisterSfmt44497UniformRandom_Seed_SameSeed_SameValue()
        {
            var gen1 = new MersenneTwisterSfmt44497UniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new MersenneTwisterSfmt44497UniformRandom(Seed);
            int actual = gen2.NextInt();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MersenneTwisterSfmt44497UniformRandom_Seed_DifferentSeed_DifferentValue()
        {
            var gen1 = new MersenneTwisterSfmt44497UniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new MersenneTwisterSfmt44497UniformRandom(Seed + 1);
            int actual = gen2.NextInt();

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void MersenneTwisterSfmt44497UniformRandom_CanReset_ReturnsTrue()
        {
            var gen = new MersenneTwisterSfmt44497UniformRandom();

            Assert.IsTrue(gen.CanReset);
        }

        [TestMethod]
        public void MersenneTwisterSfmt44497UniformRandom_Reset_ValueGeneratedThenReset_SameValueGenerated()
        {
            var gen = new MersenneTwisterSfmt44497UniformRandom();
            int expected = gen.NextInt();

            gen.Reset();
            int actual = gen.NextInt();

            Assert.AreEqual(expected, actual);
        }
    }
}
