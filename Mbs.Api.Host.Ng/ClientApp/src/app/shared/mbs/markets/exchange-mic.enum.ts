/** Exchange representations according to ISO 10383 Market Identifier Code (MIC). */
export enum ExchangeMic {
  Egsi = 'egsi',
  Xwbo = 'xwbo',
  Exaa = 'exaa',
  Wbah = 'wbah',
  Wbdm = 'wbdm',
  Wbgf = 'wbgf',
  Xvie = 'xvie',
  Beam = 'beam',
  Bmts = 'bmts',
  Mtsd = 'mtsd',
  Mtsf = 'mtsf',
  Blpx = 'blpx',
  Xbru = 'xbru',
  Alxb = 'alxb',
  Enxb = 'enxb',
  Mlxb = 'mlxb',
  Tnlb = 'tnlb',
  Vpxb = 'vpxb',
  Xbrd = 'xbrd',
  Cand = 'cand',
  Canx = 'canx',
  Chic = 'chic',
  Xcx2 = 'xcx2',
  Cotc = 'cotc',
  Ifca = 'ifca',
  Ivzx = 'ivzx',
  Lica = 'lica',
  Matn = 'matn',
  Neoe = 'neoe',
  Ngxc = 'ngxc',
  Omga = 'omga',
  Lynx = 'lynx',
  Tmxs = 'tmxs',
  Xats = 'xats',
  Xbbk = 'xbbk',
  Xcnq = 'xcnq',
  Pure = 'pure',
  Xcxd = 'xcxd',
  Xicx = 'xicx',
  Xmoc = 'xmoc',
  Xmod = 'xmod',
  Xtse = 'xtse',
  Xtsx = 'xtsx',
  Xtnx = 'xtnx',
  Dasi = 'dasi',
  Dktc = 'dktc',
  Gxgr = 'gxgr',
  Gxgf = 'gxgf',
  Gxgm = 'gxgm',
  Jbsi = 'jbsi',
  Npga = 'npga',
  Snsi = 'snsi',
  Xcse = 'xcse',
  Dcse = 'dcse',
  Fndk = 'fndk',
  Dndk = 'dndk',
  Mcse = 'mcse',
  Mndk = 'mndk',
  Fgex = 'fgex',
  Xhel = 'xhel',
  Fnfi = 'fnfi',
  Dhel = 'dhel',
  Dnfi = 'dnfi',
  Mhel = 'mhel',
  Mnfi = 'mnfi',
  Coal = 'coal',
  Epex = 'epex',
  Exse = 'exse',
  Fmts = 'fmts',
  Gmtf = 'gmtf',
  Lchc = 'lchc',
  Natx = 'natx',
  Xafr = 'xafr',
  Xbln = 'xbln',
  Xpar = 'xpar',
  Alxp = 'alxp',
  Mtch = 'mtch',
  Xmat = 'xmat',
  Xmli = 'xmli',
  Xmon = 'xmon',
  Xspm = 'xspm',
  Xpow = 'xpow',
  Xpsf = 'xpsf',
  Xpot = 'xpot',
  X360T = 'x360T',
  Baad = 'baad',
  Cats = 'cats',
  Dapa = 'dapa',
  Dbox = 'dbox',
  Auto = 'auto',
  Ecag = 'ecag',
  Ficx = 'ficx',
  Hsbt = 'hsbt',
  Tgat = 'tgat',
  Xgat = 'xgat',
  Xgrm = 'xgrm',
  Vwdx = 'vwdx',
  Xber = 'xber',
  Bera = 'bera',
  Berb = 'berb',
  Berc = 'berc',
  Eqta = 'eqta',
  Eqtb = 'eqtb',
  Eqtc = 'eqtc',
  Eqtd = 'eqtd',
  Xeqt = 'xeqt',
  Zobx = 'zobx',
  Xdus = 'xdus',
  Dusa = 'dusa',
  Dusb = 'dusb',
  Dusc = 'dusc',
  Dusd = 'dusd',
  Xqtx = 'xqtx',
  Xecb = 'xecb',
  Xecc = 'xecc',
  Xeee = 'xeee',
  Xeeo = 'xeeo',
  Xeer = 'xeer',
  Xetr = 'xetr',
  Xeub = 'xeub',
  Xeta = 'xeta',
  Xetb = 'xetb',
  Xeup = 'xeup',
  Xeum = 'xeum',
  Xere = 'xere',
  Xert = 'xert',
  Xeur = 'xeur',
  Xfra = 'xfra',
  Fraa = 'fraa',
  Frab = 'frab',
  Xdbc = 'xdbc',
  Xdbv = 'xdbv',
  Xdbx = 'xdbx',
  Xham = 'xham',
  Hama = 'hama',
  Hamb = 'hamb',
  Hamm = 'hamm',
  Hamn = 'hamn',
  Haml = 'haml',
  Xhan = 'xhan',
  Hana = 'hana',
  Hanb = 'hanb',
  Xinv = 'xinv',
  Xmun = 'xmun',
  Muna = 'muna',
  Munb = 'munb',
  Mund = 'mund',
  Munc = 'munc',
  Xsco = 'xsco',
  Xsc1 = 'xsc1',
  Xsc2 = 'xsc2',
  Xsc3 = 'xsc3',
  Xstu = 'xstu',
  Euwx = 'euwx',
  Stua = 'stua',
  Stub = 'stub',
  Xstf = 'xstf',
  Stuc = 'stuc',
  Stud = 'stud',
  Xxsc = 'xxsc',
  Xice = 'xice',
  Dice = 'dice',
  Fnis = 'fnis',
  Dnis = 'dnis',
  Mice = 'mice',
  Mnis = 'mnis',
  Cgit = 'cgit',
  Cggd = 'cggd',
  Cgcm = 'cgcm',
  Cgqt = 'cgqt',
  Cgdb = 'cgdb',
  Cgeb = 'cgeb',
  Cgtr = 'cgtr',
  Cgqd = 'cgqd',
  Cgnd = 'cgnd',
  Emid = 'emid',
  Emdr = 'emdr',
  Emir = 'emir',
  Emib = 'emib',
  Etlx = 'etlx',
  Fbsi = 'fbsi',
  Hmtf = 'hmtf',
  Hmod = 'hmod',
  Hrfq = 'hrfq',
  Mtso = 'mtso',
  Bond = 'bond',
  Mtsc = 'mtsc',
  Mtsm = 'mtsm',
  Ssob = 'ssob',
  Xgme = 'xgme',
  Xmil = 'xmil',
  Mtah = 'mtah',
  Etfp = 'etfp',
  Mivx = 'mivx',
  Motx = 'motx',
  Mtaa = 'mtaa',
  Sedx = 'sedx',
  Xaim = 'xaim',
  Xdmi = 'xdmi',
  Xmot = 'xmot',
  Cclx = 'cclx',
  Mibl = 'mibl',
  Rbcb = 'rbcb',
  Rbsi = 'rbsi',
  Xlux = 'xlux',
  Emtf = 'emtf',
  Xves = 'xves',
  Fish = 'fish',
  Fshx = 'fshx',
  Icas = 'icas',
  Nexo = 'nexo',
  Nops = 'nops',
  Norx = 'norx',
  Eleu = 'eleu',
  Else = 'else',
  Elno = 'elno',
  Eluk = 'eluk',
  Frei = 'frei',
  Bulk = 'bulk',
  Stee = 'stee',
  Nosc = 'nosc',
  Notc = 'notc',
  Oslc = 'oslc',
  Xdnb = 'xdnb',
  Xima = 'xima',
  Xosl = 'xosl',
  Xoam = 'xoam',
  Xoas = 'xoas',
  Nibr = 'nibr',
  Merd = 'merd',
  Merk = 'merk',
  Xosc = 'xosc',
  Xoad = 'xoad',
  Xosd = 'xosd',
  Omic = 'omic',
  Opex = 'opex',
  Xlis = 'xlis',
  Alxl = 'alxl',
  Enxl = 'enxl',
  Mfox = 'mfox',
  Omip = 'omip',
  Wqxl = 'wqxl',
  Bmex = 'bmex',
  Mabx = 'mabx',
  Send = 'send',
  Xbar = 'xbar',
  Xbil = 'xbil',
  Xdrf = 'xdrf',
  Xlat = 'xlat',
  Xmad = 'xmad',
  Xmce = 'xmce',
  Xmrv = 'xmrv',
  Xval = 'xval',
  Merf = 'merf',
  Xmpw = 'xmpw',
  Marf = 'marf',
  Bmcl = 'bmcl',
  Sbar = 'sbar',
  Sbil = 'sbil',
  Bmea = 'bmea',
  Ibgh = 'ibgh',
  Mibg = 'mibg',
  Omel = 'omel',
  Pave = 'pave',
  Xdpa = 'xdpa',
  Xnaf = 'xnaf',
  Cryd = 'cryd',
  Cryx = 'cryx',
  Napa = 'napa',
  Sebx = 'sebx',
  Ensx = 'ensx',
  Sebs = 'sebs',
  Xngm = 'xngm',
  Nmtf = 'nmtf',
  Xndx = 'xndx',
  Xnmr = 'xnmr',
  Xsat = 'xsat',
  Xsto = 'xsto',
  Fnse = 'fnse',
  Xopv = 'xopv',
  Csto = 'csto',
  Dsto = 'dsto',
  Dnse = 'dnse',
  Msto = 'msto',
  Mnse = 'mnse',
  Dked = 'dked',
  Fied = 'fied',
  Noed = 'noed',
  Seed = 'seed',
  Pned = 'pned',
  Euwb = 'euwb',
  Uswb = 'uswb',
  Dkfi = 'dkfi',
  Nofi = 'nofi',
  Ebon = 'ebon',
  Onse = 'onse',
  Esto = 'esto',
  Aixe = 'aixe',
  Dots = 'dots',
  Ebss = 'ebss',
  Ebsc = 'ebsc',
  Euch = 'euch',
  Eusp = 'eusp',
  Eurm = 'eurm',
  Eusc = 'eusc',
  S3fm = 's3fm',
  Stox = 'stox',
  Xscu = 'xscu',
  Xstv = 'xstv',
  Xstx = 'xstx',
  Ubsg = 'ubsg',
  Ubsf = 'ubsf',
  Ubsc = 'ubsc',
  Vlex = 'vlex',
  Xbrn = 'xbrn',
  Xswx = 'xswx',
  Xqmh = 'xqmh',
  Xvtx = 'xvtx',
  Xbtr = 'xbtr',
  Xswm = 'xswm',
  Xsls = 'xsls',
  Xicb = 'xicb',
  Zkbx = 'zkbx',
  Kmux = 'kmux',
  Clmx = 'clmx',
  Hchc = 'hchc',
  Ndex = 'ndex',
  Imco = 'imco',
  Imeq = 'imeq',
  Ndxs = 'ndxs',
  Nlpx = 'nlpx',
  Xams = 'xams',
  Tnla = 'tnla',
  Xeuc = 'xeuc',
  Xeue = 'xeue',
  Xeui = 'xeui',
  Xems = 'xems',
  Xnxc = 'xnxc',
  X3579 = 'x3579',
  Afdl = 'afdl',
  Ampx = 'ampx',
  Anzl = 'anzl',
  Aqxe = 'aqxe',
  Arax = 'arax',
  Atlb = 'atlb',
  Autx = 'autx',
  Autp = 'autp',
  Autb = 'autb',
  Balt = 'balt',
  Bltx = 'bltx',
  Bapa = 'bapa',
  Bcrm = 'bcrm',
  Baro = 'baro',
  Bark = 'bark',
  Bart = 'bart',
  Bcxe = 'bcxe',
  Bate = 'bate',
  Chix = 'chix',
  Batd = 'batd',
  Chid = 'chid',
  Batf = 'batf',
  Chio = 'chio',
  Batp = 'batp',
  Botc = 'botc',
  Lisx = 'lisx',
  Bgci = 'bgci',
  Bgcb = 'bgcb',
  Bkln = 'bkln',
  Bklf = 'bklf',
  Blox = 'blox',
  Bmtf = 'bmtf',
  Boat = 'boat',
  Bosc = 'bosc',
  Brnx = 'brnx',
  Btee = 'btee',
  Ebsm = 'ebsm',
  Ebsd = 'ebsd',
  Ebsi = 'ebsi',
  Nexy = 'nexy',
  Ccml = 'ccml',
  Cco2 = 'cco2',
  Cgme = 'cgme',
  Chev = 'chev',
  Blnk = 'blnk',
  Cmee = 'cmee',
  Cmec = 'cmec',
  Cmed = 'cmed',
  Cmmt = 'cmmt',
  Cryp = 'cryp',
  Cseu = 'cseu',
  Cscf = 'cscf',
  Csbx = 'csbx',
  Sics = 'sics',
  Csin = 'csin',
  Cssi = 'cssi',
  Dbes = 'dbes',
  Dbix = 'dbix',
  Dbdc = 'dbdc',
  Dbcx = 'dbcx',
  Dbcr = 'dbcr',
  Dbmo = 'dbmo',
  Dbse = 'dbse',
  Dowg = 'dowg',
  Echo = 'echo',
  Embx = 'embx',
  Encl = 'encl',
  Eqld = 'eqld',
  Exeu = 'exeu',
  Exmp = 'exmp',
  Exor = 'exor',
  Exvp = 'exvp',
  Exbo = 'exbo',
  Exlp = 'exlp',
  Exdc = 'exdc',
  Exsi = 'exsi',
  Excp = 'excp',
  Exot = 'exot',
  Fair = 'fair',
  Fisu = 'fisu',
  Fxgb = 'fxgb',
  Gemx = 'gemx',
  Gfic = 'gfic',
  Gfif = 'gfif',
  Gfin = 'gfin',
  Gfir = 'gfir',
  Gmeg = 'gmeg',
  Xldx = 'xldx',
  Xgdx = 'xgdx',
  Xgsx = 'xgsx',
  Xgcx = 'xgcx',
  Grif = 'grif',
  Grio = 'grio',
  Grse = 'grse',
  Gsib = 'gsib',
  Bisi = 'bisi',
  Gsil = 'gsil',
  Gssi = 'gssi',
  Gsbx = 'gsbx',
  Hpcs = 'hpcs',
  Hsbc = 'hsbc',
  Ibal = 'ibal',
  Icap = 'icap',
  Icah = 'icah',
  Icen = 'icen',
  Icse = 'icse',
  Ictq = 'ictq',
  Wclk = 'wclk',
  Igdl = 'igdl',
  Ifeu = 'ifeu',
  Cxrt = 'cxrt',
  Iflo = 'iflo',
  Ifll = 'ifll',
  Ifut = 'ifut',
  Iflx = 'iflx',
  Ifen = 'ifen',
  Cxot = 'cxot',
  Ifls = 'ifls',
  Inve = 'inve',
  Iswa = 'iswa',
  Jpcb = 'jpcb',
  Jpsi = 'jpsi',
  Jssi = 'jssi',
  Kleu = 'kleu',
  Lcur = 'lcur',
  Liqu = 'liqu',
  Liqh = 'liqh',
  Liqf = 'liqf',
  Lmax = 'lmax',
  Lmad = 'lmad',
  Lmae = 'lmae',
  Lmaf = 'lmaf',
  Lmao = 'lmao',
  Lmec = 'lmec',
  Lotc = 'lotc',
  Pldx = 'pldx',
  Lppm = 'lppm',
  Mael = 'mael',
  Mcur = 'mcur',
  Mcxs = 'mcxs',
  Mcxr = 'mcxr',
  Mfgl = 'mfgl',
  Mfxc = 'mfxc',
  Mfxa = 'mfxa',
  Mfxr = 'mfxr',
  Mhip = 'mhip',
  Mlxn = 'mlxn',
  Mlax = 'mlax',
  Mleu = 'mleu',
  Mlve = 'mlve',
  Msip = 'msip',
  Mssi = 'mssi',
  Mufp = 'mufp',
  Muti = 'muti',
  Mytr = 'mytr',
  N2Ex = 'n2Ex',
  Ndcm = 'ndcm',
  Nexs = 'nexs',
  Nexx = 'nexx',
  Nexf = 'nexf',
  Nexg = 'nexg',
  Next = 'next',
  Nexn = 'nexn',
  Nexd = 'nexd',
  Nexl = 'nexl',
  Noff = 'noff',
  Nosi = 'nosi',
  Nuro = 'nuro',
  Xnlx = 'xnlx',
  Nurd = 'nurd',
  Nxeu = 'nxeu',
  Otce = 'otce',
  Peel = 'peel',
  Xrsp = 'xrsp',
  Xphx = 'xphx',
  Pieu = 'pieu',
  Pirm = 'pirm',
  Ppex = 'ppex',
  Qwix = 'qwix',
  Rbce = 'rbce',
  Rbct = 'rbct',
  Rtsi = 'rtsi',
  Rbsx = 'rbsx',
  Rtsl = 'rtsl',
  Trfw = 'trfw',
  Tral = 'tral',
  Secf = 'secf',
  Sedr = 'sedr',
  Sgmx = 'sgmx',
  Sgmy = 'sgmy',
  Shar = 'shar',
  Spec = 'spec',
  Sprz = 'sprz',
  Ssex = 'ssex',
  Stal = 'stal',
  Stan = 'stan',
  Stsi = 'stsi',
  Swap = 'swap',
  Tcml = 'tcml',
  Tfsv = 'tfsv',
  Fxop = 'fxop',
  Tpie = 'tpie',
  Trax = 'trax',
  Trde = 'trde',
  Nave = 'nave',
  Tcds = 'tcds',
  Trdx = 'trdx',
  Tfsg = 'tfsg',
  Parx = 'parx',
  Elix = 'elix',
  Treu = 'treu',
  Trea = 'trea',
  Treo = 'treo',
  Trqx = 'trqx',
  Trqm = 'trqm',
  Trqs = 'trqs',
  Trqa = 'trqa',
  Trsi = 'trsi',
  Ubsb = 'ubsb',
  Ubsy = 'ubsy',
  Ubsl = 'ubsl',
  Ubse = 'ubse',
  Ubsi = 'ubsi',
  Ukpx = 'ukpx',
  Vcmo = 'vcmo',
  Vega = 'vega',
  Wins = 'wins',
  Winx = 'winx',
  Xalt = 'xalt',
  Xcor = 'xcor',
  Xgcl = 'xgcl',
  Xlbm = 'xlbm',
  Xlch = 'xlch',
  Xldn = 'xldn',
  Xsmp = 'xsmp',
  Ensy = 'ensy',
  Xlme = 'xlme',
  Xlon = 'xlon',
  Aimx = 'aimx',
  Xlod = 'xlod',
  Xlom = 'xlom',
  Xmts = 'xmts',
  Hung = 'hung',
  Ukgd = 'ukgd',
  Amts = 'amts',
  Emts = 'emts',
  Gmts = 'gmts',
  Imts = 'imts',
  Mczk = 'mczk',
  Mtsa = 'mtsa',
  Mtsg = 'mtsg',
  Mtss = 'mtss',
  Rmts = 'rmts',
  Smts = 'smts',
  Vmts = 'vmts',
  Bvuk = 'bvuk',
  Port = 'port',
  Mtsw = 'mtsw',
  Xsga = 'xsga',
  Xswb = 'xswb',
  Xtup = 'xtup',
  Tpeq = 'tpeq',
  Tben = 'tben',
  Tbla = 'tbla',
  Tpcd = 'tpcd',
  Tpfd = 'tpfd',
  Tpre = 'tpre',
  Tpsd = 'tpsd',
  Xtpe = 'xtpe',
  Tpel = 'tpel',
  Tpsl = 'tpsl',
  Xubs = 'xubs',
  Aats = 'aats',
  Advt = 'advt',
  Aqua = 'aqua',
  Atdf = 'atdf',
  Core = 'core',
  Baml = 'baml',
  Mlvx = 'mlvx',
  Mlco = 'mlco',
  Barx = 'barx',
  Bard = 'bard',
  Barl = 'barl',
  Bcdx = 'bcdx',
  Bats = 'bats',
  Bato = 'bato',
  Baty = 'baty',
  Bzxd = 'bzxd',
  Byxd = 'byxd',
  Bbsf = 'bbsf',
  Bgcf = 'bgcf',
  Fncs = 'fncs',
  Bgcd = 'bgcd',
  Bhsf = 'bhsf',
  Bids = 'bids',
  Bltd = 'bltd',
  Bpol = 'bpol',
  Bnyc = 'bnyc',
  Vtex = 'vtex',
  Nyfx = 'nyfx',
  Btec = 'btec',
  Icsu = 'icsu',
  Cded = 'cded',
  Cgmi = 'cgmi',
  Cicx = 'cicx',
  Lqfi = 'lqfi',
  Cblc = 'cblc',
  Cmsf = 'cmsf',
  Cred = 'cred',
  Caes = 'caes',
  Cslp = 'cslp',
  Dbsx = 'dbsx',
  Deal = 'deal',
  Edge = 'edge',
  Eddp = 'eddp',
  Edga = 'edga',
  Edgd = 'edgd',
  Edgx = 'edgx',
  Edgo = 'edgo',
  Egmt = 'egmt',
  Eris = 'eris',
  Fast = 'fast',
  Finr = 'finr',
  Finn = 'finn',
  Fino = 'fino',
  Finy = 'finy',
  Xadf = 'xadf',
  Ootc = 'ootc',
  Fsef = 'fsef',
  Fxal = 'fxal',
  Fxcm = 'fxcm',
  G1xx = 'g1xx',
  Gllc = 'gllc',
  Glps = 'glps',
  Glpx = 'glpx',
  Gotc = 'gotc',
  Govx = 'govx',
  Gree = 'gree',
  Gsco = 'gsco',
  Sgmt = 'sgmt',
  Gsef = 'gsef',
  Gtco = 'gtco',
  Gtsx = 'gtsx',
  Gtxs = 'gtxs',
  Hegx = 'hegx',
  Hppo = 'hppo',
  Hsfx = 'hsfx',
  Icel = 'icel',
  Iexg = 'iexg',
  Iexd = 'iexd',
  Ifus = 'ifus',
  Iepa = 'iepa',
  Imfx = 'imfx',
  Imag = 'imag',
  Imbd = 'imbd',
  Imcr = 'imcr',
  Imen = 'imen',
  Imir = 'imir',
  Ifed = 'ifed',
  Imcg = 'imcg',
  Imcc = 'imcc',
  Ices = 'ices',
  Imcs = 'imcs',
  Isda = 'isda',
  Itgi = 'itgi',
  Jefx = 'jefx',
  Jlqd = 'jlqd',
  Jpbx = 'jpbx',
  Jpmx = 'jpmx',
  Jses = 'jses',
  Jsjx = 'jsjx',
  Knig = 'knig',
  Kncm = 'kncm',
  Knem = 'knem',
  Knli = 'knli',
  Knmx = 'knmx',
  Ackf = 'ackf',
  Lasf = 'lasf',
  Ledg = 'ledg',
  Levl = 'levl',
  Lius = 'lius',
  Lifi = 'lifi',
  Liuh = 'liuh',
  Lqed = 'lqed',
  Ltaa = 'ltaa',
  Lmnx = 'lmnx',
  Mihi = 'mihi',
  Xmio = 'xmio',
  Mprl = 'mprl',
  Msco = 'msco',
  Mspl = 'mspl',
  Msrp = 'msrp',
  Mstx = 'mstx',
  Mslp = 'mslp',
  Mtus = 'mtus',
  Bvus = 'bvus',
  Mtsb = 'mtsb',
  Mtxx = 'mtxx',
  Mtxs = 'mtxs',
  Mtxm = 'mtxm',
  Mtxc = 'mtxc',
  Mtxa = 'mtxa',
  Nblx = 'nblx',
  Nfsc = 'nfsc',
  Nfsa = 'nfsa',
  Nfsd = 'nfsd',
  Xstm = 'xstm',
  Nmra = 'nmra',
  Nodx = 'nodx',
  Nxus = 'nxus',
  Nypc = 'nypc',
  Ollc = 'ollc',
  Opra = 'opra',
  Otcm = 'otcm',
  Otcb = 'otcb',
  Otcq = 'otcq',
  Pinc = 'pinc',
  Pini = 'pini',
  Pinl = 'pinl',
  Pinx = 'pinx',
  Psgm = 'psgm',
  Cave = 'cave',
  Pdqx = 'pdqx',
  Pdqd = 'pdqd',
  Pipe = 'pipe',
  Prse = 'prse',
  Pulx = 'pulx',
  Ricx = 'ricx',
  Ricd = 'ricd',
  Scxs = 'scxs',
  Sgma = 'sgma',
  Shaw = 'shaw',
  Shad = 'shad',
  Soho = 'soho',
  Sstx = 'sstx',
  Tera = 'tera',
  Tfsu = 'tfsu',
  Them = 'them',
  Thre = 'thre',
  Tmid = 'tmid',
  Tpse = 'tpse',
  Trck = 'trck',
  Trux = 'trux',
  Tru1 = 'tru1',
  Tru2 = 'tru2',
  Trwb = 'trwb',
  Bndd = 'bndd',
  Twsf = 'twsf',
  Dwsf = 'dwsf',
  Tsad = 'tsad',
  Tsbx = 'tsbx',
  Tsef = 'tsef',
  Ubsa = 'ubsa',
  Ubsp = 'ubsp',
  Vert = 'vert',
  Vfcm = 'vfcm',
  Virt = 'virt',
  Weed = 'weed',
  Xwee = 'xwee',
  Welx = 'welx',
  Wsag = 'wsag',
  Xaqs = 'xaqs',
  Xbox = 'xbox',
  Xcbo = 'xcbo',
  C2Ox = 'c2Ox',
  Cbsx = 'cbsx',
  Xcbf = 'xcbf',
  Xcbt = 'xcbt',
  Fcbt = 'fcbt',
  Xkbt = 'xkbt',
  Xcff = 'xcff',
  Xchi = 'xchi',
  Xcis = 'xcis',
  Xcme = 'xcme',
  Fcme = 'fcme',
  Glbx = 'glbx',
  Ximm = 'ximm',
  Xiom = 'xiom',
  Nyms = 'nyms',
  Cmes = 'cmes',
  Cbts = 'cbts',
  Cecs = 'cecs',
  Xcur = 'xcur',
  Xelx = 'xelx',
  Xfci = 'xfci',
  Xgmx = 'xgmx',
  Xins = 'xins',
  Iblx = 'iblx',
  Icbx = 'icbx',
  Icro = 'icro',
  Iidx = 'iidx',
  Rcbx = 'rcbx',
  Mocx = 'mocx',
  Xisx = 'xisx',
  Xisa = 'xisa',
  Xise = 'xise',
  Mcry = 'mcry',
  Gmni = 'gmni',
  Xmer = 'xmer',
  Xmge = 'xmge',
  Xnas = 'xnas',
  Xbxo = 'xbxo',
  Bosd = 'bosd',
  Nasd = 'nasd',
  Xbrt = 'xbrt',
  Xncm = 'xncm',
  Xndq = 'xndq',
  Xngs = 'xngs',
  Xnim = 'xnim',
  Xnms = 'xnms',
  Xpbt = 'xpbt',
  Xphl = 'xphl',
  Xpho = 'xpho',
  Xpor = 'xpor',
  Xpsx = 'xpsx',
  Espd = 'espd',
  Xbos = 'xbos',
  Xnym = 'xnym',
  Xcec = 'xcec',
  Xnye = 'xnye',
  Xnyl = 'xnyl',
  Xnys = 'xnys',
  Aldp = 'aldp',
  Amxo = 'amxo',
  Arcd = 'arcd',
  Arco = 'arco',
  Arcx = 'arcx',
  Nysd = 'nysd',
  Xase = 'xase',
  Xnli = 'xnli',
  Xoch = 'xoch',
  Xotc = 'xotc',
  Xsef = 'xsef',
  Bilt = 'bilt',
  Xoff = 'xoff',
  Xxxx = 'xxxx',
}
