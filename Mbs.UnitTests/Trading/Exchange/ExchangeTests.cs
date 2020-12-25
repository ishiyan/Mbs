using System;
using Mbs.Trading.Markets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Markets
{
    /// <summary>
    /// Exchange unit tests.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Generated automatically from the list of ISO 10383 Exchange/Market Identifier Codes, version SEP2017.1 (2017-09-29).
    /// </para>
    /// <para>
    /// See http://www.iso15022.org/MIC/homepageMIC.htm.
    /// </para>
    /// <para>
    /// Only the following countries are included:
    /// </para>
    /// <para>
    /// Austria, Belgium, Canada, Denmark, Finland, France, Germany, Iceland, Italy, Luxembourg, Norway, Portugal, Spain, Sweden, Switzerland, Netherlands, UnitedKingdom, UnitedStates, NoCountry.
    /// </para>
    /// </remarks>
    [TestClass]
    public class ExchangeTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void Exchange_Constructor_ExchangeMic_CorrectResult()
        {
            var target = new Exchange(ExchangeMic.Xpar);
            Assert.IsTrue(target.Mic == ExchangeMic.Xpar);
        }

        [TestMethod]
        public void Exchange_Constructor_EuronextMic_CorrectResult()
        {
            var target = new Exchange(EuronextMic.Xams);
            Assert.IsTrue(target.Mic == ExchangeMic.Xams);
        }

        [TestMethod]
        public void Exchange_Constructor_NoParameters_CorrectResult()
        {
            var target = new Exchange();
            Assert.IsTrue(target.Mic == ExchangeMic.Xams);
        }

        [TestMethod]
        public void Exchange_Compare_CorrectResult()
        {
            var target = new Exchange(ExchangeMic.Xpar);
            Assert.IsFalse(target == null);

            var other = new Exchange(ExchangeMic.Xpar);
            Assert.IsTrue(target == other);

            other = new Exchange(ExchangeMic.Xams);
            Assert.IsFalse(target == other);
        }

        [TestMethod]
        public void Exchange_Country_CorrectResult()
        {
            var target = new Exchange(ExchangeMic.Egsi);
            Assert.AreEqual(ExchangeCountry.Austria, target.Country, "Egsi");

            target = new Exchange(ExchangeMic.Xwbo);
            Assert.AreEqual(ExchangeCountry.Austria, target.Country, "Xwbo");

            target = new Exchange(ExchangeMic.Exaa);
            Assert.AreEqual(ExchangeCountry.Austria, target.Country, "Exaa");

            target = new Exchange(ExchangeMic.Wbah);
            Assert.AreEqual(ExchangeCountry.Austria, target.Country, "Wbah");

            target = new Exchange(ExchangeMic.Wbdm);
            Assert.AreEqual(ExchangeCountry.Austria, target.Country, "Wbdm");

            target = new Exchange(ExchangeMic.Wbgf);
            Assert.AreEqual(ExchangeCountry.Austria, target.Country, "Wbgf");

            target = new Exchange(ExchangeMic.Xvie);
            Assert.AreEqual(ExchangeCountry.Austria, target.Country, "Xvie");

            target = new Exchange(ExchangeMic.Beam);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Beam");

            target = new Exchange(ExchangeMic.Bmts);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Bmts");

            target = new Exchange(ExchangeMic.Mtsd);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Mtsd");

            target = new Exchange(ExchangeMic.Mtsf);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Mtsf");

            target = new Exchange(ExchangeMic.Blpx);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Blpx");

            target = new Exchange(ExchangeMic.Xbru);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Xbru");

            target = new Exchange(ExchangeMic.Alxb);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Alxb");

            target = new Exchange(ExchangeMic.Enxb);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Enxb");

            target = new Exchange(ExchangeMic.Mlxb);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Mlxb");

            target = new Exchange(ExchangeMic.Tnlb);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Tnlb");

            target = new Exchange(ExchangeMic.Vpxb);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Vpxb");

            target = new Exchange(ExchangeMic.Xbrd);
            Assert.AreEqual(ExchangeCountry.Belgium, target.Country, "Xbrd");

            target = new Exchange(ExchangeMic.Cand);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Cand");

            target = new Exchange(ExchangeMic.Canx);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Canx");

            target = new Exchange(ExchangeMic.Chic);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Chic");

            target = new Exchange(ExchangeMic.Xcx2);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xcx2");

            target = new Exchange(ExchangeMic.Cotc);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Cotc");

            target = new Exchange(ExchangeMic.Ifca);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Ifca");

            target = new Exchange(ExchangeMic.Ivzx);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Ivzx");

            target = new Exchange(ExchangeMic.Lica);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Lica");

            target = new Exchange(ExchangeMic.Matn);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Matn");

            target = new Exchange(ExchangeMic.Neoe);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Neoe");

            target = new Exchange(ExchangeMic.Ngxc);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Ngxc");

            target = new Exchange(ExchangeMic.Omga);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Omga");

            target = new Exchange(ExchangeMic.Lynx);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Lynx");

            target = new Exchange(ExchangeMic.Tmxs);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Tmxs");

            target = new Exchange(ExchangeMic.Xats);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xats");

            target = new Exchange(ExchangeMic.Xbbk);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xbbk");

            target = new Exchange(ExchangeMic.Xcnq);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xcnq");

            target = new Exchange(ExchangeMic.Pure);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Pure");

            target = new Exchange(ExchangeMic.Xcxd);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xcxd");

            target = new Exchange(ExchangeMic.Xicx);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xicx");

            target = new Exchange(ExchangeMic.Xmoc);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xmoc");

            target = new Exchange(ExchangeMic.Xmod);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xmod");

            target = new Exchange(ExchangeMic.Xtse);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xtse");

            target = new Exchange(ExchangeMic.Xtsx);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xtsx");

            target = new Exchange(ExchangeMic.Xtnx);
            Assert.AreEqual(ExchangeCountry.Canada, target.Country, "Xtnx");

            target = new Exchange(ExchangeMic.Dasi);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Dasi");

            target = new Exchange(ExchangeMic.Dktc);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Dktc");

            target = new Exchange(ExchangeMic.Gxgr);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Gxgr");

            target = new Exchange(ExchangeMic.Gxgf);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Gxgf");

            target = new Exchange(ExchangeMic.Gxgm);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Gxgm");

            target = new Exchange(ExchangeMic.Jbsi);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Jbsi");

            target = new Exchange(ExchangeMic.Npga);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Npga");

            target = new Exchange(ExchangeMic.Snsi);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Snsi");

            target = new Exchange(ExchangeMic.Xcse);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Xcse");

            target = new Exchange(ExchangeMic.Dcse);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Dcse");

            target = new Exchange(ExchangeMic.Fndk);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Fndk");

            target = new Exchange(ExchangeMic.Dndk);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Dndk");

            target = new Exchange(ExchangeMic.Mcse);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Mcse");

            target = new Exchange(ExchangeMic.Mndk);
            Assert.AreEqual(ExchangeCountry.Denmark, target.Country, "Mndk");

            target = new Exchange(ExchangeMic.Fgex);
            Assert.AreEqual(ExchangeCountry.Finland, target.Country, "Fgex");

            target = new Exchange(ExchangeMic.Xhel);
            Assert.AreEqual(ExchangeCountry.Finland, target.Country, "Xhel");

            target = new Exchange(ExchangeMic.Fnfi);
            Assert.AreEqual(ExchangeCountry.Finland, target.Country, "Fnfi");

            target = new Exchange(ExchangeMic.Dhel);
            Assert.AreEqual(ExchangeCountry.Finland, target.Country, "Dhel");

            target = new Exchange(ExchangeMic.Dnfi);
            Assert.AreEqual(ExchangeCountry.Finland, target.Country, "Dnfi");

            target = new Exchange(ExchangeMic.Mhel);
            Assert.AreEqual(ExchangeCountry.Finland, target.Country, "Mhel");

            target = new Exchange(ExchangeMic.Mnfi);
            Assert.AreEqual(ExchangeCountry.Finland, target.Country, "Mnfi");

            target = new Exchange(ExchangeMic.Coal);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Coal");

            target = new Exchange(ExchangeMic.Epex);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Epex");

            target = new Exchange(ExchangeMic.Exse);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Exse");

            target = new Exchange(ExchangeMic.Fmts);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Fmts");

            target = new Exchange(ExchangeMic.Gmtf);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Gmtf");

            target = new Exchange(ExchangeMic.Lchc);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Lchc");

            target = new Exchange(ExchangeMic.Natx);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Natx");

            target = new Exchange(ExchangeMic.Xafr);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Xafr");

            target = new Exchange(ExchangeMic.Xbln);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Xbln");

            target = new Exchange(ExchangeMic.Xpar);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Xpar");

            target = new Exchange(ExchangeMic.Alxp);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Alxp");

            target = new Exchange(ExchangeMic.Mtch);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Mtch");

            target = new Exchange(ExchangeMic.Xmat);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Xmat");

            target = new Exchange(ExchangeMic.Xmli);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Xmli");

            target = new Exchange(ExchangeMic.Xmon);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Xmon");

            target = new Exchange(ExchangeMic.Xspm);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Xspm");

            target = new Exchange(ExchangeMic.Xpow);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Xpow");

            target = new Exchange(ExchangeMic.Xpsf);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Xpsf");

            target = new Exchange(ExchangeMic.Xpot);
            Assert.AreEqual(ExchangeCountry.France, target.Country, "Xpot");

            target = new Exchange(ExchangeMic.X360T);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "X360T");

            target = new Exchange(ExchangeMic.Baad);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Baad");

            target = new Exchange(ExchangeMic.Cats);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Cats");

            target = new Exchange(ExchangeMic.Dapa);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Dapa");

            target = new Exchange(ExchangeMic.Dbox);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Dbox");

            target = new Exchange(ExchangeMic.Auto);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Auto");

            target = new Exchange(ExchangeMic.Ecag);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Ecag");

            target = new Exchange(ExchangeMic.Ficx);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Ficx");

            target = new Exchange(ExchangeMic.Hsbt);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Hsbt");

            target = new Exchange(ExchangeMic.Tgat);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Tgat");

            target = new Exchange(ExchangeMic.Xgat);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xgat");

            target = new Exchange(ExchangeMic.Xgrm);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xgrm");

            target = new Exchange(ExchangeMic.Vwdx);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Vwdx");

            target = new Exchange(ExchangeMic.Xber);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xber");

            target = new Exchange(ExchangeMic.Bera);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Bera");

            target = new Exchange(ExchangeMic.Berb);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Berb");

            target = new Exchange(ExchangeMic.Berc);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Berc");

            target = new Exchange(ExchangeMic.Eqta);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Eqta");

            target = new Exchange(ExchangeMic.Eqtb);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Eqtb");

            target = new Exchange(ExchangeMic.Eqtc);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Eqtc");

            target = new Exchange(ExchangeMic.Eqtd);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Eqtd");

            target = new Exchange(ExchangeMic.Xeqt);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xeqt");

            target = new Exchange(ExchangeMic.Zobx);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Zobx");

            target = new Exchange(ExchangeMic.Xdus);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xdus");

            target = new Exchange(ExchangeMic.Dusa);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Dusa");

            target = new Exchange(ExchangeMic.Dusb);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Dusb");

            target = new Exchange(ExchangeMic.Dusc);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Dusc");

            target = new Exchange(ExchangeMic.Dusd);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Dusd");

            target = new Exchange(ExchangeMic.Xqtx);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xqtx");

            target = new Exchange(ExchangeMic.Xecb);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xecb");

            target = new Exchange(ExchangeMic.Xecc);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xecc");

            target = new Exchange(ExchangeMic.Xeee);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xeee");

            target = new Exchange(ExchangeMic.Xeeo);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xeeo");

            target = new Exchange(ExchangeMic.Xeer);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xeer");

            target = new Exchange(ExchangeMic.Xetr);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xetr");

            target = new Exchange(ExchangeMic.Xeub);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xeub");

            target = new Exchange(ExchangeMic.Xeta);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xeta");

            target = new Exchange(ExchangeMic.Xetb);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xetb");

            target = new Exchange(ExchangeMic.Xeup);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xeup");

            target = new Exchange(ExchangeMic.Xeum);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xeum");

            target = new Exchange(ExchangeMic.Xere);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xere");

            target = new Exchange(ExchangeMic.Xert);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xert");

            target = new Exchange(ExchangeMic.Xeur);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xeur");

            target = new Exchange(ExchangeMic.Xfra);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xfra");

            target = new Exchange(ExchangeMic.Fraa);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Fraa");

            target = new Exchange(ExchangeMic.Frab);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Frab");

            target = new Exchange(ExchangeMic.Xdbc);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xdbc");

            target = new Exchange(ExchangeMic.Xdbv);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xdbv");

            target = new Exchange(ExchangeMic.Xdbx);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xdbx");

            target = new Exchange(ExchangeMic.Xham);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xham");

            target = new Exchange(ExchangeMic.Hama);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Hama");

            target = new Exchange(ExchangeMic.Hamb);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Hamb");

            target = new Exchange(ExchangeMic.Hamm);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Hamm");

            target = new Exchange(ExchangeMic.Hamn);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Hamn");

            target = new Exchange(ExchangeMic.Haml);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Haml");

            target = new Exchange(ExchangeMic.Xhan);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xhan");

            target = new Exchange(ExchangeMic.Hana);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Hana");

            target = new Exchange(ExchangeMic.Hanb);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Hanb");

            target = new Exchange(ExchangeMic.Xinv);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xinv");

            target = new Exchange(ExchangeMic.Xmun);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xmun");

            target = new Exchange(ExchangeMic.Muna);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Muna");

            target = new Exchange(ExchangeMic.Munb);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Munb");

            target = new Exchange(ExchangeMic.Mund);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Mund");

            target = new Exchange(ExchangeMic.Munc);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Munc");

            target = new Exchange(ExchangeMic.Xsco);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xsco");

            target = new Exchange(ExchangeMic.Xsc1);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xsc1");

            target = new Exchange(ExchangeMic.Xsc2);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xsc2");

            target = new Exchange(ExchangeMic.Xsc3);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xsc3");

            target = new Exchange(ExchangeMic.Xstu);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xstu");

            target = new Exchange(ExchangeMic.Euwx);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Euwx");

            target = new Exchange(ExchangeMic.Stua);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Stua");

            target = new Exchange(ExchangeMic.Stub);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Stub");

            target = new Exchange(ExchangeMic.Xstf);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xstf");

            target = new Exchange(ExchangeMic.Stuc);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Stuc");

            target = new Exchange(ExchangeMic.Stud);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Stud");

            target = new Exchange(ExchangeMic.Xxsc);
            Assert.AreEqual(ExchangeCountry.Germany, target.Country, "Xxsc");

            target = new Exchange(ExchangeMic.Xice);
            Assert.AreEqual(ExchangeCountry.Iceland, target.Country, "Xice");

            target = new Exchange(ExchangeMic.Dice);
            Assert.AreEqual(ExchangeCountry.Iceland, target.Country, "Dice");

            target = new Exchange(ExchangeMic.Fnis);
            Assert.AreEqual(ExchangeCountry.Iceland, target.Country, "Fnis");

            target = new Exchange(ExchangeMic.Dnis);
            Assert.AreEqual(ExchangeCountry.Iceland, target.Country, "Dnis");

            target = new Exchange(ExchangeMic.Mice);
            Assert.AreEqual(ExchangeCountry.Iceland, target.Country, "Mice");

            target = new Exchange(ExchangeMic.Mnis);
            Assert.AreEqual(ExchangeCountry.Iceland, target.Country, "Mnis");

            target = new Exchange(ExchangeMic.Cgit);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Cgit");

            target = new Exchange(ExchangeMic.Cggd);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Cggd");

            target = new Exchange(ExchangeMic.Cgcm);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Cgcm");

            target = new Exchange(ExchangeMic.Cgqt);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Cgqt");

            target = new Exchange(ExchangeMic.Cgdb);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Cgdb");

            target = new Exchange(ExchangeMic.Cgeb);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Cgeb");

            target = new Exchange(ExchangeMic.Cgtr);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Cgtr");

            target = new Exchange(ExchangeMic.Cgqd);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Cgqd");

            target = new Exchange(ExchangeMic.Cgnd);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Cgnd");

            target = new Exchange(ExchangeMic.Emid);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Emid");

            target = new Exchange(ExchangeMic.Emdr);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Emdr");

            target = new Exchange(ExchangeMic.Emir);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Emir");

            target = new Exchange(ExchangeMic.Emib);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Emib");

            target = new Exchange(ExchangeMic.Etlx);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Etlx");

            target = new Exchange(ExchangeMic.Fbsi);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Fbsi");

            target = new Exchange(ExchangeMic.Hmtf);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Hmtf");

            target = new Exchange(ExchangeMic.Hmod);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Hmod");

            target = new Exchange(ExchangeMic.Hrfq);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Hrfq");

            target = new Exchange(ExchangeMic.Mtso);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Mtso");

            target = new Exchange(ExchangeMic.Bond);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Bond");

            target = new Exchange(ExchangeMic.Mtsc);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Mtsc");

            target = new Exchange(ExchangeMic.Mtsm);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Mtsm");

            target = new Exchange(ExchangeMic.Ssob);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Ssob");

            target = new Exchange(ExchangeMic.Xgme);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Xgme");

            target = new Exchange(ExchangeMic.Xmil);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Xmil");

            target = new Exchange(ExchangeMic.Mtah);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Mtah");

            target = new Exchange(ExchangeMic.Etfp);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Etfp");

            target = new Exchange(ExchangeMic.Mivx);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Mivx");

            target = new Exchange(ExchangeMic.Motx);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Motx");

            target = new Exchange(ExchangeMic.Mtaa);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Mtaa");

            target = new Exchange(ExchangeMic.Sedx);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Sedx");

            target = new Exchange(ExchangeMic.Xaim);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Xaim");

            target = new Exchange(ExchangeMic.Xdmi);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Xdmi");

            target = new Exchange(ExchangeMic.Xmot);
            Assert.AreEqual(ExchangeCountry.Italy, target.Country, "Xmot");

            target = new Exchange(ExchangeMic.Cclx);
            Assert.AreEqual(ExchangeCountry.Luxembourg, target.Country, "Cclx");

            target = new Exchange(ExchangeMic.Mibl);
            Assert.AreEqual(ExchangeCountry.Luxembourg, target.Country, "Mibl");

            target = new Exchange(ExchangeMic.Rbcb);
            Assert.AreEqual(ExchangeCountry.Luxembourg, target.Country, "Rbcb");

            target = new Exchange(ExchangeMic.Rbsi);
            Assert.AreEqual(ExchangeCountry.Luxembourg, target.Country, "Rbsi");

            target = new Exchange(ExchangeMic.Xlux);
            Assert.AreEqual(ExchangeCountry.Luxembourg, target.Country, "Xlux");

            target = new Exchange(ExchangeMic.Emtf);
            Assert.AreEqual(ExchangeCountry.Luxembourg, target.Country, "Emtf");

            target = new Exchange(ExchangeMic.Xves);
            Assert.AreEqual(ExchangeCountry.Luxembourg, target.Country, "Xves");

            target = new Exchange(ExchangeMic.Fish);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Fish");

            target = new Exchange(ExchangeMic.Fshx);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Fshx");

            target = new Exchange(ExchangeMic.Icas);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Icas");

            target = new Exchange(ExchangeMic.Nexo);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Nexo");

            target = new Exchange(ExchangeMic.Nops);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Nops");

            target = new Exchange(ExchangeMic.Norx);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Norx");

            target = new Exchange(ExchangeMic.Eleu);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Eleu");

            target = new Exchange(ExchangeMic.Else);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Else");

            target = new Exchange(ExchangeMic.Elno);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Elno");

            target = new Exchange(ExchangeMic.Eluk);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Eluk");

            target = new Exchange(ExchangeMic.Frei);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Frei");

            target = new Exchange(ExchangeMic.Bulk);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Bulk");

            target = new Exchange(ExchangeMic.Stee);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Stee");

            target = new Exchange(ExchangeMic.Nosc);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Nosc");

            target = new Exchange(ExchangeMic.Notc);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Notc");

            target = new Exchange(ExchangeMic.Oslc);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Oslc");

            target = new Exchange(ExchangeMic.Xdnb);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Xdnb");

            target = new Exchange(ExchangeMic.Xima);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Xima");

            target = new Exchange(ExchangeMic.Xosl);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Xosl");

            target = new Exchange(ExchangeMic.Xoam);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Xoam");

            target = new Exchange(ExchangeMic.Xoas);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Xoas");

            target = new Exchange(ExchangeMic.Nibr);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Nibr");

            target = new Exchange(ExchangeMic.Merd);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Merd");

            target = new Exchange(ExchangeMic.Merk);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Merk");

            target = new Exchange(ExchangeMic.Xosc);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Xosc");

            target = new Exchange(ExchangeMic.Xoad);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Xoad");

            target = new Exchange(ExchangeMic.Xosd);
            Assert.AreEqual(ExchangeCountry.Norway, target.Country, "Xosd");

            target = new Exchange(ExchangeMic.Omic);
            Assert.AreEqual(ExchangeCountry.Portugal, target.Country, "Omic");

            target = new Exchange(ExchangeMic.Opex);
            Assert.AreEqual(ExchangeCountry.Portugal, target.Country, "Opex");

            target = new Exchange(ExchangeMic.Xlis);
            Assert.AreEqual(ExchangeCountry.Portugal, target.Country, "Xlis");

            target = new Exchange(ExchangeMic.Alxl);
            Assert.AreEqual(ExchangeCountry.Portugal, target.Country, "Alxl");

            target = new Exchange(ExchangeMic.Enxl);
            Assert.AreEqual(ExchangeCountry.Portugal, target.Country, "Enxl");

            target = new Exchange(ExchangeMic.Mfox);
            Assert.AreEqual(ExchangeCountry.Portugal, target.Country, "Mfox");

            target = new Exchange(ExchangeMic.Omip);
            Assert.AreEqual(ExchangeCountry.Portugal, target.Country, "Omip");

            target = new Exchange(ExchangeMic.Wqxl);
            Assert.AreEqual(ExchangeCountry.Portugal, target.Country, "Wqxl");

            target = new Exchange(ExchangeMic.Bmex);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Bmex");

            target = new Exchange(ExchangeMic.Mabx);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Mabx");

            target = new Exchange(ExchangeMic.Send);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Send");

            target = new Exchange(ExchangeMic.Xbar);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xbar");

            target = new Exchange(ExchangeMic.Xbil);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xbil");

            target = new Exchange(ExchangeMic.Xdrf);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xdrf");

            target = new Exchange(ExchangeMic.Xlat);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xlat");

            target = new Exchange(ExchangeMic.Xmad);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xmad");

            target = new Exchange(ExchangeMic.Xmce);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xmce");

            target = new Exchange(ExchangeMic.Xmrv);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xmrv");

            target = new Exchange(ExchangeMic.Xval);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xval");

            target = new Exchange(ExchangeMic.Merf);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Merf");

            target = new Exchange(ExchangeMic.Xmpw);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xmpw");

            target = new Exchange(ExchangeMic.Marf);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Marf");

            target = new Exchange(ExchangeMic.Bmcl);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Bmcl");

            target = new Exchange(ExchangeMic.Sbar);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Sbar");

            target = new Exchange(ExchangeMic.Sbil);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Sbil");

            target = new Exchange(ExchangeMic.Bmea);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Bmea");

            target = new Exchange(ExchangeMic.Ibgh);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Ibgh");

            target = new Exchange(ExchangeMic.Mibg);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Mibg");

            target = new Exchange(ExchangeMic.Omel);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Omel");

            target = new Exchange(ExchangeMic.Pave);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Pave");

            target = new Exchange(ExchangeMic.Xdpa);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xdpa");

            target = new Exchange(ExchangeMic.Xnaf);
            Assert.AreEqual(ExchangeCountry.Spain, target.Country, "Xnaf");

            target = new Exchange(ExchangeMic.Cryd);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Cryd");

            target = new Exchange(ExchangeMic.Cryx);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Cryx");

            target = new Exchange(ExchangeMic.Napa);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Napa");

            target = new Exchange(ExchangeMic.Sebx);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Sebx");

            target = new Exchange(ExchangeMic.Ensx);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Ensx");

            target = new Exchange(ExchangeMic.Sebs);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Sebs");

            target = new Exchange(ExchangeMic.Xngm);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Xngm");

            target = new Exchange(ExchangeMic.Nmtf);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Nmtf");

            target = new Exchange(ExchangeMic.Xndx);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Xndx");

            target = new Exchange(ExchangeMic.Xnmr);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Xnmr");

            target = new Exchange(ExchangeMic.Xsat);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Xsat");

            target = new Exchange(ExchangeMic.Xsto);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Xsto");

            target = new Exchange(ExchangeMic.Fnse);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Fnse");

            target = new Exchange(ExchangeMic.Xopv);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Xopv");

            target = new Exchange(ExchangeMic.Csto);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Csto");

            target = new Exchange(ExchangeMic.Dsto);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Dsto");

            target = new Exchange(ExchangeMic.Dnse);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Dnse");

            target = new Exchange(ExchangeMic.Msto);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Msto");

            target = new Exchange(ExchangeMic.Mnse);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Mnse");

            target = new Exchange(ExchangeMic.Dked);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Dked");

            target = new Exchange(ExchangeMic.Fied);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Fied");

            target = new Exchange(ExchangeMic.Noed);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Noed");

            target = new Exchange(ExchangeMic.Seed);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Seed");

            target = new Exchange(ExchangeMic.Pned);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Pned");

            target = new Exchange(ExchangeMic.Euwb);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Euwb");

            target = new Exchange(ExchangeMic.Uswb);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Uswb");

            target = new Exchange(ExchangeMic.Dkfi);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Dkfi");

            target = new Exchange(ExchangeMic.Nofi);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Nofi");

            target = new Exchange(ExchangeMic.Ebon);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Ebon");

            target = new Exchange(ExchangeMic.Onse);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Onse");

            target = new Exchange(ExchangeMic.Esto);
            Assert.AreEqual(ExchangeCountry.Sweden, target.Country, "Esto");

            target = new Exchange(ExchangeMic.Aixe);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Aixe");

            target = new Exchange(ExchangeMic.Dots);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Dots");

            target = new Exchange(ExchangeMic.Ebss);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Ebss");

            target = new Exchange(ExchangeMic.Ebsc);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Ebsc");

            target = new Exchange(ExchangeMic.Euch);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Euch");

            target = new Exchange(ExchangeMic.Eusp);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Eusp");

            target = new Exchange(ExchangeMic.Eurm);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Eurm");

            target = new Exchange(ExchangeMic.Eusc);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Eusc");

            target = new Exchange(ExchangeMic.S3fm);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "S3fm");

            target = new Exchange(ExchangeMic.Stox);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Stox");

            target = new Exchange(ExchangeMic.Xscu);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xscu");

            target = new Exchange(ExchangeMic.Xstv);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xstv");

            target = new Exchange(ExchangeMic.Xstx);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xstx");

            target = new Exchange(ExchangeMic.Ubsg);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Ubsg");

            target = new Exchange(ExchangeMic.Ubsf);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Ubsf");

            target = new Exchange(ExchangeMic.Ubsc);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Ubsc");

            target = new Exchange(ExchangeMic.Vlex);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Vlex");

            target = new Exchange(ExchangeMic.Xbrn);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xbrn");

            target = new Exchange(ExchangeMic.Xswx);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xswx");

            target = new Exchange(ExchangeMic.Xqmh);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xqmh");

            target = new Exchange(ExchangeMic.Xvtx);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xvtx");

            target = new Exchange(ExchangeMic.Xbtr);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xbtr");

            target = new Exchange(ExchangeMic.Xswm);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xswm");

            target = new Exchange(ExchangeMic.Xsls);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xsls");

            target = new Exchange(ExchangeMic.Xicb);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Xicb");

            target = new Exchange(ExchangeMic.Zkbx);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Zkbx");

            target = new Exchange(ExchangeMic.Kmux);
            Assert.AreEqual(ExchangeCountry.Switzerland, target.Country, "Kmux");

            target = new Exchange(ExchangeMic.Clmx);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Clmx");

            target = new Exchange(ExchangeMic.Hchc);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Hchc");

            target = new Exchange(ExchangeMic.Ndex);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Ndex");

            target = new Exchange(ExchangeMic.Imco);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Imco");

            target = new Exchange(ExchangeMic.Imeq);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Imeq");

            target = new Exchange(ExchangeMic.Ndxs);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Ndxs");

            target = new Exchange(ExchangeMic.Nlpx);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Nlpx");

            target = new Exchange(ExchangeMic.Xams);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Xams");

            target = new Exchange(ExchangeMic.Tnla);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Tnla");

            target = new Exchange(ExchangeMic.Xeuc);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Xeuc");

            target = new Exchange(ExchangeMic.Xeue);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Xeue");

            target = new Exchange(ExchangeMic.Xeui);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Xeui");

            target = new Exchange(ExchangeMic.Xems);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Xems");

            target = new Exchange(ExchangeMic.Xnxc);
            Assert.AreEqual(ExchangeCountry.Netherlands, target.Country, "Xnxc");

            target = new Exchange(ExchangeMic.X3579);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "X3579");

            target = new Exchange(ExchangeMic.Afdl);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Afdl");

            target = new Exchange(ExchangeMic.Ampx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ampx");

            target = new Exchange(ExchangeMic.Anzl);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Anzl");

            target = new Exchange(ExchangeMic.Aqxe);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Aqxe");

            target = new Exchange(ExchangeMic.Arax);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Arax");

            target = new Exchange(ExchangeMic.Atlb);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Atlb");

            target = new Exchange(ExchangeMic.Autx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Autx");

            target = new Exchange(ExchangeMic.Autp);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Autp");

            target = new Exchange(ExchangeMic.Autb);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Autb");

            target = new Exchange(ExchangeMic.Balt);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Balt");

            target = new Exchange(ExchangeMic.Bltx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bltx");

            target = new Exchange(ExchangeMic.Bapa);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bapa");

            target = new Exchange(ExchangeMic.Bcrm);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bcrm");

            target = new Exchange(ExchangeMic.Baro);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Baro");

            target = new Exchange(ExchangeMic.Bark);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bark");

            target = new Exchange(ExchangeMic.Bart);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bart");

            target = new Exchange(ExchangeMic.Bcxe);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bcxe");

            target = new Exchange(ExchangeMic.Bate);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bate");

            target = new Exchange(ExchangeMic.Chix);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Chix");

            target = new Exchange(ExchangeMic.Batd);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Batd");

            target = new Exchange(ExchangeMic.Chid);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Chid");

            target = new Exchange(ExchangeMic.Batf);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Batf");

            target = new Exchange(ExchangeMic.Chio);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Chio");

            target = new Exchange(ExchangeMic.Batp);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Batp");

            target = new Exchange(ExchangeMic.Botc);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Botc");

            target = new Exchange(ExchangeMic.Lisx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Lisx");

            target = new Exchange(ExchangeMic.Bgci);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bgci");

            target = new Exchange(ExchangeMic.Bgcb);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bgcb");

            target = new Exchange(ExchangeMic.Bkln);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bkln");

            target = new Exchange(ExchangeMic.Bklf);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bklf");

            target = new Exchange(ExchangeMic.Blox);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Blox");

            target = new Exchange(ExchangeMic.Bmtf);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bmtf");

            target = new Exchange(ExchangeMic.Boat);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Boat");

            target = new Exchange(ExchangeMic.Bosc);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bosc");

            target = new Exchange(ExchangeMic.Brnx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Brnx");

            target = new Exchange(ExchangeMic.Btee);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Btee");

            target = new Exchange(ExchangeMic.Ebsm);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ebsm");

            target = new Exchange(ExchangeMic.Ebsd);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ebsd");

            target = new Exchange(ExchangeMic.Ebsi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ebsi");

            target = new Exchange(ExchangeMic.Nexy);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nexy");

            target = new Exchange(ExchangeMic.Ccml);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ccml");

            target = new Exchange(ExchangeMic.Cco2);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cco2");

            target = new Exchange(ExchangeMic.Cgme);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cgme");

            target = new Exchange(ExchangeMic.Chev);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Chev");

            target = new Exchange(ExchangeMic.Blnk);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Blnk");

            target = new Exchange(ExchangeMic.Cmee);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cmee");

            target = new Exchange(ExchangeMic.Cmec);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cmec");

            target = new Exchange(ExchangeMic.Cmed);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cmed");

            target = new Exchange(ExchangeMic.Cmmt);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cmmt");

            target = new Exchange(ExchangeMic.Cryp);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cryp");

            target = new Exchange(ExchangeMic.Cseu);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cseu");

            target = new Exchange(ExchangeMic.Cscf);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cscf");

            target = new Exchange(ExchangeMic.Csbx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Csbx");

            target = new Exchange(ExchangeMic.Sics);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Sics");

            target = new Exchange(ExchangeMic.Csin);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Csin");

            target = new Exchange(ExchangeMic.Cssi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cssi");

            target = new Exchange(ExchangeMic.Dbes);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Dbes");

            target = new Exchange(ExchangeMic.Dbix);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Dbix");

            target = new Exchange(ExchangeMic.Dbdc);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Dbdc");

            target = new Exchange(ExchangeMic.Dbcx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Dbcx");

            target = new Exchange(ExchangeMic.Dbcr);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Dbcr");

            target = new Exchange(ExchangeMic.Dbmo);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Dbmo");

            target = new Exchange(ExchangeMic.Dbse);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Dbse");

            target = new Exchange(ExchangeMic.Dowg);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Dowg");

            target = new Exchange(ExchangeMic.Echo);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Echo");

            target = new Exchange(ExchangeMic.Embx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Embx");

            target = new Exchange(ExchangeMic.Encl);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Encl");

            target = new Exchange(ExchangeMic.Eqld);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Eqld");

            target = new Exchange(ExchangeMic.Exeu);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Exeu");

            target = new Exchange(ExchangeMic.Exmp);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Exmp");

            target = new Exchange(ExchangeMic.Exor);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Exor");

            target = new Exchange(ExchangeMic.Exvp);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Exvp");

            target = new Exchange(ExchangeMic.Exbo);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Exbo");

            target = new Exchange(ExchangeMic.Exlp);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Exlp");

            target = new Exchange(ExchangeMic.Exdc);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Exdc");

            target = new Exchange(ExchangeMic.Exsi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Exsi");

            target = new Exchange(ExchangeMic.Excp);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Excp");

            target = new Exchange(ExchangeMic.Exot);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Exot");

            target = new Exchange(ExchangeMic.Fair);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Fair");

            target = new Exchange(ExchangeMic.Fisu);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Fisu");

            target = new Exchange(ExchangeMic.Fxgb);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Fxgb");

            target = new Exchange(ExchangeMic.Gemx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gemx");

            target = new Exchange(ExchangeMic.Gfic);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gfic");

            target = new Exchange(ExchangeMic.Gfif);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gfif");

            target = new Exchange(ExchangeMic.Gfin);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gfin");

            target = new Exchange(ExchangeMic.Gfir);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gfir");

            target = new Exchange(ExchangeMic.Gmeg);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gmeg");

            target = new Exchange(ExchangeMic.Xldx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xldx");

            target = new Exchange(ExchangeMic.Xgdx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xgdx");

            target = new Exchange(ExchangeMic.Xgsx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xgsx");

            target = new Exchange(ExchangeMic.Xgcx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xgcx");

            target = new Exchange(ExchangeMic.Grif);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Grif");

            target = new Exchange(ExchangeMic.Grio);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Grio");

            target = new Exchange(ExchangeMic.Grse);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Grse");

            target = new Exchange(ExchangeMic.Gsib);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gsib");

            target = new Exchange(ExchangeMic.Bisi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bisi");

            target = new Exchange(ExchangeMic.Gsil);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gsil");

            target = new Exchange(ExchangeMic.Gssi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gssi");

            target = new Exchange(ExchangeMic.Gsbx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gsbx");

            target = new Exchange(ExchangeMic.Hpcs);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Hpcs");

            target = new Exchange(ExchangeMic.Hsbc);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Hsbc");

            target = new Exchange(ExchangeMic.Ibal);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ibal");

            target = new Exchange(ExchangeMic.Icap);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Icap");

            target = new Exchange(ExchangeMic.Icah);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Icah");

            target = new Exchange(ExchangeMic.Icen);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Icen");

            target = new Exchange(ExchangeMic.Icse);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Icse");

            target = new Exchange(ExchangeMic.Ictq);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ictq");

            target = new Exchange(ExchangeMic.Wclk);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Wclk");

            target = new Exchange(ExchangeMic.Igdl);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Igdl");

            target = new Exchange(ExchangeMic.Ifeu);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ifeu");

            target = new Exchange(ExchangeMic.Cxrt);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cxrt");

            target = new Exchange(ExchangeMic.Iflo);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Iflo");

            target = new Exchange(ExchangeMic.Ifll);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ifll");

            target = new Exchange(ExchangeMic.Ifut);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ifut");

            target = new Exchange(ExchangeMic.Iflx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Iflx");

            target = new Exchange(ExchangeMic.Ifen);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ifen");

            target = new Exchange(ExchangeMic.Cxot);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Cxot");

            target = new Exchange(ExchangeMic.Ifls);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ifls");

            target = new Exchange(ExchangeMic.Inve);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Inve");

            target = new Exchange(ExchangeMic.Iswa);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Iswa");

            target = new Exchange(ExchangeMic.Jpcb);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Jpcb");

            target = new Exchange(ExchangeMic.Jpsi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Jpsi");

            target = new Exchange(ExchangeMic.Jssi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Jssi");

            target = new Exchange(ExchangeMic.Kleu);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Kleu");

            target = new Exchange(ExchangeMic.Lcur);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Lcur");

            target = new Exchange(ExchangeMic.Liqu);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Liqu");

            target = new Exchange(ExchangeMic.Liqh);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Liqh");

            target = new Exchange(ExchangeMic.Liqf);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Liqf");

            target = new Exchange(ExchangeMic.Lmax);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Lmax");

            target = new Exchange(ExchangeMic.Lmad);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Lmad");

            target = new Exchange(ExchangeMic.Lmae);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Lmae");

            target = new Exchange(ExchangeMic.Lmaf);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Lmaf");

            target = new Exchange(ExchangeMic.Lmao);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Lmao");

            target = new Exchange(ExchangeMic.Lmec);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Lmec");

            target = new Exchange(ExchangeMic.Lotc);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Lotc");

            target = new Exchange(ExchangeMic.Pldx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Pldx");

            target = new Exchange(ExchangeMic.Lppm);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Lppm");

            target = new Exchange(ExchangeMic.Mael);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mael");

            target = new Exchange(ExchangeMic.Mcur);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mcur");

            target = new Exchange(ExchangeMic.Mcxs);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mcxs");

            target = new Exchange(ExchangeMic.Mcxr);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mcxr");

            target = new Exchange(ExchangeMic.Mfgl);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mfgl");

            target = new Exchange(ExchangeMic.Mfxc);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mfxc");

            target = new Exchange(ExchangeMic.Mfxa);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mfxa");

            target = new Exchange(ExchangeMic.Mfxr);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mfxr");

            target = new Exchange(ExchangeMic.Mhip);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mhip");

            target = new Exchange(ExchangeMic.Mlxn);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mlxn");

            target = new Exchange(ExchangeMic.Mlax);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mlax");

            target = new Exchange(ExchangeMic.Mleu);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mleu");

            target = new Exchange(ExchangeMic.Mlve);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mlve");

            target = new Exchange(ExchangeMic.Msip);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Msip");

            target = new Exchange(ExchangeMic.Mssi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mssi");

            target = new Exchange(ExchangeMic.Mufp);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mufp");

            target = new Exchange(ExchangeMic.Muti);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Muti");

            target = new Exchange(ExchangeMic.Mytr);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mytr");

            target = new Exchange(ExchangeMic.N2Ex);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "N2Ex");

            target = new Exchange(ExchangeMic.Ndcm);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ndcm");

            target = new Exchange(ExchangeMic.Nexs);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nexs");

            target = new Exchange(ExchangeMic.Nexx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nexx");

            target = new Exchange(ExchangeMic.Nexf);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nexf");

            target = new Exchange(ExchangeMic.Nexg);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nexg");

            target = new Exchange(ExchangeMic.Next);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Next");

            target = new Exchange(ExchangeMic.Nexn);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nexn");

            target = new Exchange(ExchangeMic.Nexd);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nexd");

            target = new Exchange(ExchangeMic.Nexl);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nexl");

            target = new Exchange(ExchangeMic.Noff);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Noff");

            target = new Exchange(ExchangeMic.Nosi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nosi");

            target = new Exchange(ExchangeMic.Nuro);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nuro");

            target = new Exchange(ExchangeMic.Xnlx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xnlx");

            target = new Exchange(ExchangeMic.Nurd);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nurd");

            target = new Exchange(ExchangeMic.Nxeu);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nxeu");

            target = new Exchange(ExchangeMic.Otce);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Otce");

            target = new Exchange(ExchangeMic.Peel);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Peel");

            target = new Exchange(ExchangeMic.Xrsp);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xrsp");

            target = new Exchange(ExchangeMic.Xphx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xphx");

            target = new Exchange(ExchangeMic.Pieu);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Pieu");

            target = new Exchange(ExchangeMic.Pirm);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Pirm");

            target = new Exchange(ExchangeMic.Ppex);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ppex");

            target = new Exchange(ExchangeMic.Qwix);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Qwix");

            target = new Exchange(ExchangeMic.Rbce);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Rbce");

            target = new Exchange(ExchangeMic.Rbct);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Rbct");

            target = new Exchange(ExchangeMic.Rtsi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Rtsi");

            target = new Exchange(ExchangeMic.Rbsx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Rbsx");

            target = new Exchange(ExchangeMic.Rtsl);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Rtsl");

            target = new Exchange(ExchangeMic.Trfw);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Trfw");

            target = new Exchange(ExchangeMic.Tral);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tral");

            target = new Exchange(ExchangeMic.Secf);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Secf");

            target = new Exchange(ExchangeMic.Sedr);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Sedr");

            target = new Exchange(ExchangeMic.Sgmx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Sgmx");

            target = new Exchange(ExchangeMic.Sgmy);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Sgmy");

            target = new Exchange(ExchangeMic.Shar);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Shar");

            target = new Exchange(ExchangeMic.Spec);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Spec");

            target = new Exchange(ExchangeMic.Sprz);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Sprz");

            target = new Exchange(ExchangeMic.Ssex);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ssex");

            target = new Exchange(ExchangeMic.Stal);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Stal");

            target = new Exchange(ExchangeMic.Stan);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Stan");

            target = new Exchange(ExchangeMic.Stsi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Stsi");

            target = new Exchange(ExchangeMic.Swap);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Swap");

            target = new Exchange(ExchangeMic.Tcml);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tcml");

            target = new Exchange(ExchangeMic.Tfsv);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tfsv");

            target = new Exchange(ExchangeMic.Fxop);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Fxop");

            target = new Exchange(ExchangeMic.Tpie);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tpie");

            target = new Exchange(ExchangeMic.Trax);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Trax");

            target = new Exchange(ExchangeMic.Trde);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Trde");

            target = new Exchange(ExchangeMic.Nave);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Nave");

            target = new Exchange(ExchangeMic.Tcds);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tcds");

            target = new Exchange(ExchangeMic.Trdx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Trdx");

            target = new Exchange(ExchangeMic.Tfsg);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tfsg");

            target = new Exchange(ExchangeMic.Parx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Parx");

            target = new Exchange(ExchangeMic.Elix);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Elix");

            target = new Exchange(ExchangeMic.Treu);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Treu");

            target = new Exchange(ExchangeMic.Trea);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Trea");

            target = new Exchange(ExchangeMic.Treo);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Treo");

            target = new Exchange(ExchangeMic.Trqx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Trqx");

            target = new Exchange(ExchangeMic.Trqm);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Trqm");

            target = new Exchange(ExchangeMic.Trqs);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Trqs");

            target = new Exchange(ExchangeMic.Trqa);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Trqa");

            target = new Exchange(ExchangeMic.Trsi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Trsi");

            target = new Exchange(ExchangeMic.Ubsb);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ubsb");

            target = new Exchange(ExchangeMic.Ubsy);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ubsy");

            target = new Exchange(ExchangeMic.Ubsl);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ubsl");

            target = new Exchange(ExchangeMic.Ubse);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ubse");

            target = new Exchange(ExchangeMic.Ubsi);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ubsi");

            target = new Exchange(ExchangeMic.Ukpx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ukpx");

            target = new Exchange(ExchangeMic.Vcmo);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Vcmo");

            target = new Exchange(ExchangeMic.Vega);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Vega");

            target = new Exchange(ExchangeMic.Wins);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Wins");

            target = new Exchange(ExchangeMic.Winx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Winx");

            target = new Exchange(ExchangeMic.Xalt);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xalt");

            target = new Exchange(ExchangeMic.Xcor);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xcor");

            target = new Exchange(ExchangeMic.Xgcl);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xgcl");

            target = new Exchange(ExchangeMic.Xlbm);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xlbm");

            target = new Exchange(ExchangeMic.Xlch);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xlch");

            target = new Exchange(ExchangeMic.Xldn);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xldn");

            target = new Exchange(ExchangeMic.Xsmp);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xsmp");

            target = new Exchange(ExchangeMic.Ensy);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ensy");

            target = new Exchange(ExchangeMic.Xlme);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xlme");

            target = new Exchange(ExchangeMic.Xlon);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xlon");

            target = new Exchange(ExchangeMic.Aimx);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Aimx");

            target = new Exchange(ExchangeMic.Xlod);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xlod");

            target = new Exchange(ExchangeMic.Xlom);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xlom");

            target = new Exchange(ExchangeMic.Xmts);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xmts");

            target = new Exchange(ExchangeMic.Hung);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Hung");

            target = new Exchange(ExchangeMic.Ukgd);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Ukgd");

            target = new Exchange(ExchangeMic.Amts);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Amts");

            target = new Exchange(ExchangeMic.Emts);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Emts");

            target = new Exchange(ExchangeMic.Gmts);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Gmts");

            target = new Exchange(ExchangeMic.Imts);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Imts");

            target = new Exchange(ExchangeMic.Mczk);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mczk");

            target = new Exchange(ExchangeMic.Mtsa);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mtsa");

            target = new Exchange(ExchangeMic.Mtsg);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mtsg");

            target = new Exchange(ExchangeMic.Mtss);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mtss");

            target = new Exchange(ExchangeMic.Rmts);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Rmts");

            target = new Exchange(ExchangeMic.Smts);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Smts");

            target = new Exchange(ExchangeMic.Vmts);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Vmts");

            target = new Exchange(ExchangeMic.Bvuk);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Bvuk");

            target = new Exchange(ExchangeMic.Port);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Port");

            target = new Exchange(ExchangeMic.Mtsw);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Mtsw");

            target = new Exchange(ExchangeMic.Xsga);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xsga");

            target = new Exchange(ExchangeMic.Xswb);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xswb");

            target = new Exchange(ExchangeMic.Xtup);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xtup");

            target = new Exchange(ExchangeMic.Tpeq);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tpeq");

            target = new Exchange(ExchangeMic.Tben);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tben");

            target = new Exchange(ExchangeMic.Tbla);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tbla");

            target = new Exchange(ExchangeMic.Tpcd);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tpcd");

            target = new Exchange(ExchangeMic.Tpfd);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tpfd");

            target = new Exchange(ExchangeMic.Tpre);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tpre");

            target = new Exchange(ExchangeMic.Tpsd);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tpsd");

            target = new Exchange(ExchangeMic.Xtpe);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xtpe");

            target = new Exchange(ExchangeMic.Tpel);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tpel");

            target = new Exchange(ExchangeMic.Tpsl);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Tpsl");

            target = new Exchange(ExchangeMic.Xubs);
            Assert.AreEqual(ExchangeCountry.UnitedKingdom, target.Country, "Xubs");

            target = new Exchange(ExchangeMic.Aats);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Aats");

            target = new Exchange(ExchangeMic.Advt);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Advt");

            target = new Exchange(ExchangeMic.Aqua);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Aqua");

            target = new Exchange(ExchangeMic.Atdf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Atdf");

            target = new Exchange(ExchangeMic.Core);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Core");

            target = new Exchange(ExchangeMic.Baml);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Baml");

            target = new Exchange(ExchangeMic.Mlvx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mlvx");

            target = new Exchange(ExchangeMic.Mlco);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mlco");

            target = new Exchange(ExchangeMic.Barx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Barx");

            target = new Exchange(ExchangeMic.Bard);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bard");

            target = new Exchange(ExchangeMic.Barl);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Barl");

            target = new Exchange(ExchangeMic.Bcdx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bcdx");

            target = new Exchange(ExchangeMic.Bats);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bats");

            target = new Exchange(ExchangeMic.Bato);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bato");

            target = new Exchange(ExchangeMic.Baty);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Baty");

            target = new Exchange(ExchangeMic.Bzxd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bzxd");

            target = new Exchange(ExchangeMic.Byxd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Byxd");

            target = new Exchange(ExchangeMic.Bbsf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bbsf");

            target = new Exchange(ExchangeMic.Bgcf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bgcf");

            target = new Exchange(ExchangeMic.Fncs);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Fncs");

            target = new Exchange(ExchangeMic.Bgcd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bgcd");

            target = new Exchange(ExchangeMic.Bhsf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bhsf");

            target = new Exchange(ExchangeMic.Bids);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bids");

            target = new Exchange(ExchangeMic.Bltd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bltd");

            target = new Exchange(ExchangeMic.Bpol);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bpol");

            target = new Exchange(ExchangeMic.Bnyc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bnyc");

            target = new Exchange(ExchangeMic.Vtex);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Vtex");

            target = new Exchange(ExchangeMic.Nyfx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nyfx");

            target = new Exchange(ExchangeMic.Btec);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Btec");

            target = new Exchange(ExchangeMic.Icsu);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Icsu");

            target = new Exchange(ExchangeMic.Cded);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cded");

            target = new Exchange(ExchangeMic.Cgmi);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cgmi");

            target = new Exchange(ExchangeMic.Cicx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cicx");

            target = new Exchange(ExchangeMic.Lqfi);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Lqfi");

            target = new Exchange(ExchangeMic.Cblc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cblc");

            target = new Exchange(ExchangeMic.Cmsf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cmsf");

            target = new Exchange(ExchangeMic.Cred);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cred");

            target = new Exchange(ExchangeMic.Caes);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Caes");

            target = new Exchange(ExchangeMic.Cslp);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cslp");

            target = new Exchange(ExchangeMic.Dbsx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Dbsx");

            target = new Exchange(ExchangeMic.Deal);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Deal");

            target = new Exchange(ExchangeMic.Edge);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Edge");

            target = new Exchange(ExchangeMic.Eddp);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Eddp");

            target = new Exchange(ExchangeMic.Edga);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Edga");

            target = new Exchange(ExchangeMic.Edgd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Edgd");

            target = new Exchange(ExchangeMic.Edgx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Edgx");

            target = new Exchange(ExchangeMic.Edgo);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Edgo");

            target = new Exchange(ExchangeMic.Egmt);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Egmt");

            target = new Exchange(ExchangeMic.Eris);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Eris");

            target = new Exchange(ExchangeMic.Fast);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Fast");

            target = new Exchange(ExchangeMic.Finr);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Finr");

            target = new Exchange(ExchangeMic.Finn);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Finn");

            target = new Exchange(ExchangeMic.Fino);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Fino");

            target = new Exchange(ExchangeMic.Finy);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Finy");

            target = new Exchange(ExchangeMic.Xadf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xadf");

            target = new Exchange(ExchangeMic.Ootc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ootc");

            target = new Exchange(ExchangeMic.Fsef);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Fsef");

            target = new Exchange(ExchangeMic.Fxal);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Fxal");

            target = new Exchange(ExchangeMic.Fxcm);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Fxcm");

            target = new Exchange(ExchangeMic.G1xx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "G1xx");

            target = new Exchange(ExchangeMic.Gllc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Gllc");

            target = new Exchange(ExchangeMic.Glps);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Glps");

            target = new Exchange(ExchangeMic.Glpx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Glpx");

            target = new Exchange(ExchangeMic.Gotc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Gotc");

            target = new Exchange(ExchangeMic.Govx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Govx");

            target = new Exchange(ExchangeMic.Gree);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Gree");

            target = new Exchange(ExchangeMic.Gsco);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Gsco");

            target = new Exchange(ExchangeMic.Sgmt);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Sgmt");

            target = new Exchange(ExchangeMic.Gsef);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Gsef");

            target = new Exchange(ExchangeMic.Gtco);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Gtco");

            target = new Exchange(ExchangeMic.Gtsx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Gtsx");

            target = new Exchange(ExchangeMic.Gtxs);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Gtxs");

            target = new Exchange(ExchangeMic.Hegx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Hegx");

            target = new Exchange(ExchangeMic.Hppo);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Hppo");

            target = new Exchange(ExchangeMic.Hsfx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Hsfx");

            target = new Exchange(ExchangeMic.Icel);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Icel");

            target = new Exchange(ExchangeMic.Iexg);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Iexg");

            target = new Exchange(ExchangeMic.Iexd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Iexd");

            target = new Exchange(ExchangeMic.Ifus);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ifus");

            target = new Exchange(ExchangeMic.Iepa);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Iepa");

            target = new Exchange(ExchangeMic.Imfx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Imfx");

            target = new Exchange(ExchangeMic.Imag);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Imag");

            target = new Exchange(ExchangeMic.Imbd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Imbd");

            target = new Exchange(ExchangeMic.Imcr);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Imcr");

            target = new Exchange(ExchangeMic.Imen);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Imen");

            target = new Exchange(ExchangeMic.Imir);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Imir");

            target = new Exchange(ExchangeMic.Ifed);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ifed");

            target = new Exchange(ExchangeMic.Imcg);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Imcg");

            target = new Exchange(ExchangeMic.Imcc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Imcc");

            target = new Exchange(ExchangeMic.Ices);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ices");

            target = new Exchange(ExchangeMic.Imcs);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Imcs");

            target = new Exchange(ExchangeMic.Isda);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Isda");

            target = new Exchange(ExchangeMic.Itgi);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Itgi");

            target = new Exchange(ExchangeMic.Jefx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Jefx");

            target = new Exchange(ExchangeMic.Jlqd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Jlqd");

            target = new Exchange(ExchangeMic.Jpbx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Jpbx");

            target = new Exchange(ExchangeMic.Jpmx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Jpmx");

            target = new Exchange(ExchangeMic.Jses);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Jses");

            target = new Exchange(ExchangeMic.Jsjx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Jsjx");

            target = new Exchange(ExchangeMic.Knig);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Knig");

            target = new Exchange(ExchangeMic.Kncm);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Kncm");

            target = new Exchange(ExchangeMic.Knem);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Knem");

            target = new Exchange(ExchangeMic.Knli);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Knli");

            target = new Exchange(ExchangeMic.Knmx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Knmx");

            target = new Exchange(ExchangeMic.Ackf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ackf");

            target = new Exchange(ExchangeMic.Lasf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Lasf");

            target = new Exchange(ExchangeMic.Ledg);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ledg");

            target = new Exchange(ExchangeMic.Levl);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Levl");

            target = new Exchange(ExchangeMic.Lius);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Lius");

            target = new Exchange(ExchangeMic.Lifi);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Lifi");

            target = new Exchange(ExchangeMic.Liuh);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Liuh");

            target = new Exchange(ExchangeMic.Lqed);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Lqed");

            target = new Exchange(ExchangeMic.Ltaa);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ltaa");

            target = new Exchange(ExchangeMic.Lmnx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Lmnx");

            target = new Exchange(ExchangeMic.Mihi);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mihi");

            target = new Exchange(ExchangeMic.Xmio);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xmio");

            target = new Exchange(ExchangeMic.Mprl);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mprl");

            target = new Exchange(ExchangeMic.Msco);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Msco");

            target = new Exchange(ExchangeMic.Mspl);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mspl");

            target = new Exchange(ExchangeMic.Msrp);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Msrp");

            target = new Exchange(ExchangeMic.Mstx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mstx");

            target = new Exchange(ExchangeMic.Mslp);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mslp");

            target = new Exchange(ExchangeMic.Mtus);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mtus");

            target = new Exchange(ExchangeMic.Bvus);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bvus");

            target = new Exchange(ExchangeMic.Mtsb);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mtsb");

            target = new Exchange(ExchangeMic.Mtxx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mtxx");

            target = new Exchange(ExchangeMic.Mtxs);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mtxs");

            target = new Exchange(ExchangeMic.Mtxm);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mtxm");

            target = new Exchange(ExchangeMic.Mtxc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mtxc");

            target = new Exchange(ExchangeMic.Mtxa);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mtxa");

            target = new Exchange(ExchangeMic.Nblx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nblx");

            target = new Exchange(ExchangeMic.Nfsc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nfsc");

            target = new Exchange(ExchangeMic.Nfsa);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nfsa");

            target = new Exchange(ExchangeMic.Nfsd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nfsd");

            target = new Exchange(ExchangeMic.Xstm);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xstm");

            target = new Exchange(ExchangeMic.Nmra);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nmra");

            target = new Exchange(ExchangeMic.Nodx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nodx");

            target = new Exchange(ExchangeMic.Nxus);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nxus");

            target = new Exchange(ExchangeMic.Nypc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nypc");

            target = new Exchange(ExchangeMic.Ollc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ollc");

            target = new Exchange(ExchangeMic.Opra);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Opra");

            target = new Exchange(ExchangeMic.Otcm);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Otcm");

            target = new Exchange(ExchangeMic.Otcb);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Otcb");

            target = new Exchange(ExchangeMic.Otcq);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Otcq");

            target = new Exchange(ExchangeMic.Pinc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Pinc");

            target = new Exchange(ExchangeMic.Pini);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Pini");

            target = new Exchange(ExchangeMic.Pinl);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Pinl");

            target = new Exchange(ExchangeMic.Pinx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Pinx");

            target = new Exchange(ExchangeMic.Psgm);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Psgm");

            target = new Exchange(ExchangeMic.Cave);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cave");

            target = new Exchange(ExchangeMic.Pdqx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Pdqx");

            target = new Exchange(ExchangeMic.Pdqd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Pdqd");

            target = new Exchange(ExchangeMic.Pipe);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Pipe");

            target = new Exchange(ExchangeMic.Prse);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Prse");

            target = new Exchange(ExchangeMic.Pulx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Pulx");

            target = new Exchange(ExchangeMic.Ricx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ricx");

            target = new Exchange(ExchangeMic.Ricd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ricd");

            target = new Exchange(ExchangeMic.Scxs);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Scxs");

            target = new Exchange(ExchangeMic.Sgma);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Sgma");

            target = new Exchange(ExchangeMic.Shaw);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Shaw");

            target = new Exchange(ExchangeMic.Shad);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Shad");

            target = new Exchange(ExchangeMic.Soho);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Soho");

            target = new Exchange(ExchangeMic.Sstx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Sstx");

            target = new Exchange(ExchangeMic.Tera);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Tera");

            target = new Exchange(ExchangeMic.Tfsu);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Tfsu");

            target = new Exchange(ExchangeMic.Them);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Them");

            target = new Exchange(ExchangeMic.Thre);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Thre");

            target = new Exchange(ExchangeMic.Tmid);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Tmid");

            target = new Exchange(ExchangeMic.Tpse);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Tpse");

            target = new Exchange(ExchangeMic.Trck);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Trck");

            target = new Exchange(ExchangeMic.Trux);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Trux");

            target = new Exchange(ExchangeMic.Tru1);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Tru1");

            target = new Exchange(ExchangeMic.Tru2);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Tru2");

            target = new Exchange(ExchangeMic.Trwb);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Trwb");

            target = new Exchange(ExchangeMic.Bndd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bndd");

            target = new Exchange(ExchangeMic.Twsf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Twsf");

            target = new Exchange(ExchangeMic.Dwsf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Dwsf");

            target = new Exchange(ExchangeMic.Tsad);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Tsad");

            target = new Exchange(ExchangeMic.Tsbx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Tsbx");

            target = new Exchange(ExchangeMic.Tsef);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Tsef");

            target = new Exchange(ExchangeMic.Ubsa);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ubsa");

            target = new Exchange(ExchangeMic.Ubsp);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ubsp");

            target = new Exchange(ExchangeMic.Vert);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Vert");

            target = new Exchange(ExchangeMic.Vfcm);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Vfcm");

            target = new Exchange(ExchangeMic.Virt);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Virt");

            target = new Exchange(ExchangeMic.Weed);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Weed");

            target = new Exchange(ExchangeMic.Xwee);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xwee");

            target = new Exchange(ExchangeMic.Welx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Welx");

            target = new Exchange(ExchangeMic.Wsag);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Wsag");

            target = new Exchange(ExchangeMic.Xaqs);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xaqs");

            target = new Exchange(ExchangeMic.Xbox);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xbox");

            target = new Exchange(ExchangeMic.Xcbo);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xcbo");

            target = new Exchange(ExchangeMic.C2Ox);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "C2Ox");

            target = new Exchange(ExchangeMic.Cbsx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cbsx");

            target = new Exchange(ExchangeMic.Xcbf);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xcbf");

            target = new Exchange(ExchangeMic.Xcbt);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xcbt");

            target = new Exchange(ExchangeMic.Fcbt);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Fcbt");

            target = new Exchange(ExchangeMic.Xkbt);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xkbt");

            target = new Exchange(ExchangeMic.Xcff);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xcff");

            target = new Exchange(ExchangeMic.Xchi);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xchi");

            target = new Exchange(ExchangeMic.Xcis);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xcis");

            target = new Exchange(ExchangeMic.Xcme);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xcme");

            target = new Exchange(ExchangeMic.Fcme);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Fcme");

            target = new Exchange(ExchangeMic.Glbx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Glbx");

            target = new Exchange(ExchangeMic.Ximm);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Ximm");

            target = new Exchange(ExchangeMic.Xiom);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xiom");

            target = new Exchange(ExchangeMic.Nyms);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nyms");

            target = new Exchange(ExchangeMic.Cmes);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cmes");

            target = new Exchange(ExchangeMic.Cbts);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cbts");

            target = new Exchange(ExchangeMic.Cecs);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Cecs");

            target = new Exchange(ExchangeMic.Xcur);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xcur");

            target = new Exchange(ExchangeMic.Xelx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xelx");

            target = new Exchange(ExchangeMic.Xfci);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xfci");

            target = new Exchange(ExchangeMic.Xgmx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xgmx");

            target = new Exchange(ExchangeMic.Xins);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xins");

            target = new Exchange(ExchangeMic.Iblx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Iblx");

            target = new Exchange(ExchangeMic.Icbx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Icbx");

            target = new Exchange(ExchangeMic.Icro);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Icro");

            target = new Exchange(ExchangeMic.Iidx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Iidx");

            target = new Exchange(ExchangeMic.Rcbx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Rcbx");

            target = new Exchange(ExchangeMic.Mocx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mocx");

            target = new Exchange(ExchangeMic.Xisx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xisx");

            target = new Exchange(ExchangeMic.Xisa);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xisa");

            target = new Exchange(ExchangeMic.Xise);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xise");

            target = new Exchange(ExchangeMic.Mcry);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Mcry");

            target = new Exchange(ExchangeMic.Gmni);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Gmni");

            target = new Exchange(ExchangeMic.Xmer);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xmer");

            target = new Exchange(ExchangeMic.Xmge);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xmge");

            target = new Exchange(ExchangeMic.Xnas);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xnas");

            target = new Exchange(ExchangeMic.Xbxo);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xbxo");

            target = new Exchange(ExchangeMic.Bosd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Bosd");

            target = new Exchange(ExchangeMic.Nasd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nasd");

            target = new Exchange(ExchangeMic.Xbrt);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xbrt");

            target = new Exchange(ExchangeMic.Xncm);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xncm");

            target = new Exchange(ExchangeMic.Xndq);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xndq");

            target = new Exchange(ExchangeMic.Xngs);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xngs");

            target = new Exchange(ExchangeMic.Xnim);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xnim");

            target = new Exchange(ExchangeMic.Xnms);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xnms");

            target = new Exchange(ExchangeMic.Xpbt);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xpbt");

            target = new Exchange(ExchangeMic.Xphl);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xphl");

            target = new Exchange(ExchangeMic.Xpho);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xpho");

            target = new Exchange(ExchangeMic.Xpor);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xpor");

            target = new Exchange(ExchangeMic.Xpsx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xpsx");

            target = new Exchange(ExchangeMic.Espd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Espd");

            target = new Exchange(ExchangeMic.Xbos);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xbos");

            target = new Exchange(ExchangeMic.Xnym);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xnym");

            target = new Exchange(ExchangeMic.Xcec);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xcec");

            target = new Exchange(ExchangeMic.Xnye);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xnye");

            target = new Exchange(ExchangeMic.Xnyl);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xnyl");

            target = new Exchange(ExchangeMic.Xnys);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xnys");

            target = new Exchange(ExchangeMic.Aldp);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Aldp");

            target = new Exchange(ExchangeMic.Amxo);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Amxo");

            target = new Exchange(ExchangeMic.Arcd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Arcd");

            target = new Exchange(ExchangeMic.Arco);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Arco");

            target = new Exchange(ExchangeMic.Arcx);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Arcx");

            target = new Exchange(ExchangeMic.Nysd);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Nysd");

            target = new Exchange(ExchangeMic.Xase);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xase");

            target = new Exchange(ExchangeMic.Xnli);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xnli");

            target = new Exchange(ExchangeMic.Xoch);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xoch");

            target = new Exchange(ExchangeMic.Xotc);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xotc");

            target = new Exchange(ExchangeMic.Xsef);
            Assert.AreEqual(ExchangeCountry.UnitedStates, target.Country, "Xsef");

            target = new Exchange(ExchangeMic.Bilt);
            Assert.AreEqual(ExchangeCountry.NoCountry, target.Country, "Bilt");

            target = new Exchange(ExchangeMic.Xoff);
            Assert.AreEqual(ExchangeCountry.NoCountry, target.Country, "Xoff");

            target = new Exchange(ExchangeMic.Xxxx);
            Assert.AreEqual(ExchangeCountry.NoCountry, target.Country, "Xxxx");

            target = new Exchange(ExchangeMic.Xxxx);
            Assert.AreEqual(ExchangeCountry.NoCountry, target.Country, "Xxxx");
        }

        [TestMethod]
        public void Exchange_TimeZone_CorrectResult()
        {
            var target = new Exchange(ExchangeMic.Egsi);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Egsi");

            target = new Exchange(ExchangeMic.Xwbo);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xwbo");

            target = new Exchange(ExchangeMic.Exaa);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Exaa");

            target = new Exchange(ExchangeMic.Wbah);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Wbah");

            target = new Exchange(ExchangeMic.Wbdm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Wbdm");

            target = new Exchange(ExchangeMic.Wbgf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Wbgf");

            target = new Exchange(ExchangeMic.Xvie);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xvie");

            target = new Exchange(ExchangeMic.Beam);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Beam");

            target = new Exchange(ExchangeMic.Bmts);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Bmts");

            target = new Exchange(ExchangeMic.Mtsd);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mtsd");

            target = new Exchange(ExchangeMic.Mtsf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mtsf");

            target = new Exchange(ExchangeMic.Blpx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Blpx");

            target = new Exchange(ExchangeMic.Xbru);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xbru");

            target = new Exchange(ExchangeMic.Alxb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Alxb");

            target = new Exchange(ExchangeMic.Enxb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Enxb");

            target = new Exchange(ExchangeMic.Mlxb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mlxb");

            target = new Exchange(ExchangeMic.Tnlb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Tnlb");

            target = new Exchange(ExchangeMic.Vpxb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Vpxb");

            target = new Exchange(ExchangeMic.Xbrd);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xbrd");

            target = new Exchange(ExchangeMic.Dasi);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dasi");

            target = new Exchange(ExchangeMic.Dktc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dktc");

            target = new Exchange(ExchangeMic.Gxgr);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Gxgr");

            target = new Exchange(ExchangeMic.Gxgf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Gxgf");

            target = new Exchange(ExchangeMic.Gxgm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Gxgm");

            target = new Exchange(ExchangeMic.Jbsi);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Jbsi");

            target = new Exchange(ExchangeMic.Npga);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Npga");

            target = new Exchange(ExchangeMic.Snsi);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Snsi");

            target = new Exchange(ExchangeMic.Xcse);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xcse");

            target = new Exchange(ExchangeMic.Dcse);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dcse");

            target = new Exchange(ExchangeMic.Fndk);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Fndk");

            target = new Exchange(ExchangeMic.Dndk);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dndk");

            target = new Exchange(ExchangeMic.Mcse);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mcse");

            target = new Exchange(ExchangeMic.Mndk);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mndk");

            target = new Exchange(ExchangeMic.Coal);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Coal");

            target = new Exchange(ExchangeMic.Epex);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Epex");

            target = new Exchange(ExchangeMic.Exse);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Exse");

            target = new Exchange(ExchangeMic.Fmts);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Fmts");

            target = new Exchange(ExchangeMic.Gmtf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Gmtf");

            target = new Exchange(ExchangeMic.Lchc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Lchc");

            target = new Exchange(ExchangeMic.Natx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Natx");

            target = new Exchange(ExchangeMic.Xafr);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xafr");

            target = new Exchange(ExchangeMic.Xbln);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xbln");

            target = new Exchange(ExchangeMic.Xpar);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xpar");

            target = new Exchange(ExchangeMic.Alxp);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Alxp");

            target = new Exchange(ExchangeMic.Mtch);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mtch");

            target = new Exchange(ExchangeMic.Xmat);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xmat");

            target = new Exchange(ExchangeMic.Xmli);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xmli");

            target = new Exchange(ExchangeMic.Xmon);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xmon");

            target = new Exchange(ExchangeMic.Xspm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xspm");

            target = new Exchange(ExchangeMic.Xpow);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xpow");

            target = new Exchange(ExchangeMic.Xpsf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xpsf");

            target = new Exchange(ExchangeMic.Xpot);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xpot");

            target = new Exchange(ExchangeMic.X360T);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "X360T");

            target = new Exchange(ExchangeMic.Baad);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Baad");

            target = new Exchange(ExchangeMic.Cats);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cats");

            target = new Exchange(ExchangeMic.Dapa);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dapa");

            target = new Exchange(ExchangeMic.Dbox);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dbox");

            target = new Exchange(ExchangeMic.Auto);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Auto");

            target = new Exchange(ExchangeMic.Ecag);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ecag");

            target = new Exchange(ExchangeMic.Ficx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ficx");

            target = new Exchange(ExchangeMic.Hsbt);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hsbt");

            target = new Exchange(ExchangeMic.Tgat);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Tgat");

            target = new Exchange(ExchangeMic.Xgat);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xgat");

            target = new Exchange(ExchangeMic.Xgrm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xgrm");

            target = new Exchange(ExchangeMic.Vwdx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Vwdx");

            target = new Exchange(ExchangeMic.Xber);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xber");

            target = new Exchange(ExchangeMic.Bera);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Bera");

            target = new Exchange(ExchangeMic.Berb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Berb");

            target = new Exchange(ExchangeMic.Berc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Berc");

            target = new Exchange(ExchangeMic.Eqta);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Eqta");

            target = new Exchange(ExchangeMic.Eqtb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Eqtb");

            target = new Exchange(ExchangeMic.Eqtc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Eqtc");

            target = new Exchange(ExchangeMic.Eqtd);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Eqtd");

            target = new Exchange(ExchangeMic.Xeqt);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeqt");

            target = new Exchange(ExchangeMic.Zobx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Zobx");

            target = new Exchange(ExchangeMic.Xdus);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xdus");

            target = new Exchange(ExchangeMic.Dusa);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dusa");

            target = new Exchange(ExchangeMic.Dusb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dusb");

            target = new Exchange(ExchangeMic.Dusc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dusc");

            target = new Exchange(ExchangeMic.Dusd);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dusd");

            target = new Exchange(ExchangeMic.Xqtx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xqtx");

            target = new Exchange(ExchangeMic.Xecb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xecb");

            target = new Exchange(ExchangeMic.Xecc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xecc");

            target = new Exchange(ExchangeMic.Xeee);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeee");

            target = new Exchange(ExchangeMic.Xeeo);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeeo");

            target = new Exchange(ExchangeMic.Xeer);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeer");

            target = new Exchange(ExchangeMic.Xetr);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xetr");

            target = new Exchange(ExchangeMic.Xeub);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeub");

            target = new Exchange(ExchangeMic.Xeta);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeta");

            target = new Exchange(ExchangeMic.Xetb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xetb");

            target = new Exchange(ExchangeMic.Xeup);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeup");

            target = new Exchange(ExchangeMic.Xeum);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeum");

            target = new Exchange(ExchangeMic.Xere);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xere");

            target = new Exchange(ExchangeMic.Xert);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xert");

            target = new Exchange(ExchangeMic.Xeur);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeur");

            target = new Exchange(ExchangeMic.Xfra);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xfra");

            target = new Exchange(ExchangeMic.Fraa);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Fraa");

            target = new Exchange(ExchangeMic.Frab);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Frab");

            target = new Exchange(ExchangeMic.Xdbc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xdbc");

            target = new Exchange(ExchangeMic.Xdbv);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xdbv");

            target = new Exchange(ExchangeMic.Xdbx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xdbx");

            target = new Exchange(ExchangeMic.Xham);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xham");

            target = new Exchange(ExchangeMic.Hama);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hama");

            target = new Exchange(ExchangeMic.Hamb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hamb");

            target = new Exchange(ExchangeMic.Hamm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hamm");

            target = new Exchange(ExchangeMic.Hamn);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hamn");

            target = new Exchange(ExchangeMic.Haml);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Haml");

            target = new Exchange(ExchangeMic.Xhan);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xhan");

            target = new Exchange(ExchangeMic.Hana);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hana");

            target = new Exchange(ExchangeMic.Hanb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hanb");

            target = new Exchange(ExchangeMic.Xinv);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xinv");

            target = new Exchange(ExchangeMic.Xmun);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xmun");

            target = new Exchange(ExchangeMic.Muna);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Muna");

            target = new Exchange(ExchangeMic.Munb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Munb");

            target = new Exchange(ExchangeMic.Mund);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mund");

            target = new Exchange(ExchangeMic.Munc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Munc");

            target = new Exchange(ExchangeMic.Xsco);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xsco");

            target = new Exchange(ExchangeMic.Xsc1);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xsc1");

            target = new Exchange(ExchangeMic.Xsc2);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xsc2");

            target = new Exchange(ExchangeMic.Xsc3);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xsc3");

            target = new Exchange(ExchangeMic.Xstu);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xstu");

            target = new Exchange(ExchangeMic.Euwx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Euwx");

            target = new Exchange(ExchangeMic.Stua);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Stua");

            target = new Exchange(ExchangeMic.Stub);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Stub");

            target = new Exchange(ExchangeMic.Xstf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xstf");

            target = new Exchange(ExchangeMic.Stuc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Stuc");

            target = new Exchange(ExchangeMic.Stud);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Stud");

            target = new Exchange(ExchangeMic.Xxsc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xxsc");

            target = new Exchange(ExchangeMic.Cgit);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cgit");

            target = new Exchange(ExchangeMic.Cggd);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cggd");

            target = new Exchange(ExchangeMic.Cgcm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cgcm");

            target = new Exchange(ExchangeMic.Cgqt);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cgqt");

            target = new Exchange(ExchangeMic.Cgdb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cgdb");

            target = new Exchange(ExchangeMic.Cgeb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cgeb");

            target = new Exchange(ExchangeMic.Cgtr);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cgtr");

            target = new Exchange(ExchangeMic.Cgqd);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cgqd");

            target = new Exchange(ExchangeMic.Cgnd);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cgnd");

            target = new Exchange(ExchangeMic.Emid);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Emid");

            target = new Exchange(ExchangeMic.Emdr);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Emdr");

            target = new Exchange(ExchangeMic.Emir);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Emir");

            target = new Exchange(ExchangeMic.Emib);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Emib");

            target = new Exchange(ExchangeMic.Etlx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Etlx");

            target = new Exchange(ExchangeMic.Fbsi);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Fbsi");

            target = new Exchange(ExchangeMic.Hmtf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hmtf");

            target = new Exchange(ExchangeMic.Hmod);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hmod");

            target = new Exchange(ExchangeMic.Hrfq);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hrfq");

            target = new Exchange(ExchangeMic.Mtso);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mtso");

            target = new Exchange(ExchangeMic.Bond);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Bond");

            target = new Exchange(ExchangeMic.Mtsc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mtsc");

            target = new Exchange(ExchangeMic.Mtsm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mtsm");

            target = new Exchange(ExchangeMic.Ssob);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ssob");

            target = new Exchange(ExchangeMic.Xgme);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xgme");

            target = new Exchange(ExchangeMic.Xmil);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xmil");

            target = new Exchange(ExchangeMic.Mtah);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mtah");

            target = new Exchange(ExchangeMic.Etfp);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Etfp");

            target = new Exchange(ExchangeMic.Mivx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mivx");

            target = new Exchange(ExchangeMic.Motx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Motx");

            target = new Exchange(ExchangeMic.Mtaa);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mtaa");

            target = new Exchange(ExchangeMic.Sedx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Sedx");

            target = new Exchange(ExchangeMic.Xaim);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xaim");

            target = new Exchange(ExchangeMic.Xdmi);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xdmi");

            target = new Exchange(ExchangeMic.Xmot);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xmot");

            target = new Exchange(ExchangeMic.Bmex);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Bmex");

            target = new Exchange(ExchangeMic.Mabx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mabx");

            target = new Exchange(ExchangeMic.Send);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Send");

            target = new Exchange(ExchangeMic.Xbar);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xbar");

            target = new Exchange(ExchangeMic.Xbil);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xbil");

            target = new Exchange(ExchangeMic.Xdrf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xdrf");

            target = new Exchange(ExchangeMic.Xlat);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xlat");

            target = new Exchange(ExchangeMic.Xmad);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xmad");

            target = new Exchange(ExchangeMic.Xmce);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xmce");

            target = new Exchange(ExchangeMic.Xmrv);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xmrv");

            target = new Exchange(ExchangeMic.Xval);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xval");

            target = new Exchange(ExchangeMic.Merf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Merf");

            target = new Exchange(ExchangeMic.Xmpw);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xmpw");

            target = new Exchange(ExchangeMic.Marf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Marf");

            target = new Exchange(ExchangeMic.Bmcl);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Bmcl");

            target = new Exchange(ExchangeMic.Sbar);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Sbar");

            target = new Exchange(ExchangeMic.Sbil);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Sbil");

            target = new Exchange(ExchangeMic.Bmea);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Bmea");

            target = new Exchange(ExchangeMic.Ibgh);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ibgh");

            target = new Exchange(ExchangeMic.Mibg);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mibg");

            target = new Exchange(ExchangeMic.Omel);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Omel");

            target = new Exchange(ExchangeMic.Pave);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Pave");

            target = new Exchange(ExchangeMic.Xdpa);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xdpa");

            target = new Exchange(ExchangeMic.Xnaf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xnaf");

            target = new Exchange(ExchangeMic.Cryd);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cryd");

            target = new Exchange(ExchangeMic.Cryx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cryx");

            target = new Exchange(ExchangeMic.Napa);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Napa");

            target = new Exchange(ExchangeMic.Sebx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Sebx");

            target = new Exchange(ExchangeMic.Ensx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ensx");

            target = new Exchange(ExchangeMic.Sebs);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Sebs");

            target = new Exchange(ExchangeMic.Xngm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xngm");

            target = new Exchange(ExchangeMic.Nmtf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Nmtf");

            target = new Exchange(ExchangeMic.Xndx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xndx");

            target = new Exchange(ExchangeMic.Xnmr);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xnmr");

            target = new Exchange(ExchangeMic.Xsat);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xsat");

            target = new Exchange(ExchangeMic.Xsto);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xsto");

            target = new Exchange(ExchangeMic.Fnse);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Fnse");

            target = new Exchange(ExchangeMic.Xopv);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xopv");

            target = new Exchange(ExchangeMic.Csto);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Csto");

            target = new Exchange(ExchangeMic.Dsto);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dsto");

            target = new Exchange(ExchangeMic.Dnse);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dnse");

            target = new Exchange(ExchangeMic.Msto);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Msto");

            target = new Exchange(ExchangeMic.Mnse);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mnse");

            target = new Exchange(ExchangeMic.Dked);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dked");

            target = new Exchange(ExchangeMic.Fied);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Fied");

            target = new Exchange(ExchangeMic.Noed);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Noed");

            target = new Exchange(ExchangeMic.Seed);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Seed");

            target = new Exchange(ExchangeMic.Pned);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Pned");

            target = new Exchange(ExchangeMic.Euwb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Euwb");

            target = new Exchange(ExchangeMic.Uswb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Uswb");

            target = new Exchange(ExchangeMic.Dkfi);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dkfi");

            target = new Exchange(ExchangeMic.Nofi);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Nofi");

            target = new Exchange(ExchangeMic.Ebon);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ebon");

            target = new Exchange(ExchangeMic.Onse);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Onse");

            target = new Exchange(ExchangeMic.Esto);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Esto");

            target = new Exchange(ExchangeMic.Aixe);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Aixe");

            target = new Exchange(ExchangeMic.Dots);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Dots");

            target = new Exchange(ExchangeMic.Ebss);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ebss");

            target = new Exchange(ExchangeMic.Ebsc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ebsc");

            target = new Exchange(ExchangeMic.Euch);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Euch");

            target = new Exchange(ExchangeMic.Eusp);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Eusp");

            target = new Exchange(ExchangeMic.Eurm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Eurm");

            target = new Exchange(ExchangeMic.Eusc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Eusc");

            target = new Exchange(ExchangeMic.S3fm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "S3fm");

            target = new Exchange(ExchangeMic.Stox);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Stox");

            target = new Exchange(ExchangeMic.Xscu);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xscu");

            target = new Exchange(ExchangeMic.Xstv);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xstv");

            target = new Exchange(ExchangeMic.Xstx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xstx");

            target = new Exchange(ExchangeMic.Ubsg);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ubsg");

            target = new Exchange(ExchangeMic.Ubsf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ubsf");

            target = new Exchange(ExchangeMic.Ubsc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ubsc");

            target = new Exchange(ExchangeMic.Vlex);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Vlex");

            target = new Exchange(ExchangeMic.Xbrn);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xbrn");

            target = new Exchange(ExchangeMic.Xswx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xswx");

            target = new Exchange(ExchangeMic.Xqmh);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xqmh");

            target = new Exchange(ExchangeMic.Xvtx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xvtx");

            target = new Exchange(ExchangeMic.Xbtr);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xbtr");

            target = new Exchange(ExchangeMic.Xswm);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xswm");

            target = new Exchange(ExchangeMic.Xsls);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xsls");

            target = new Exchange(ExchangeMic.Xicb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xicb");

            target = new Exchange(ExchangeMic.Zkbx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Zkbx");

            target = new Exchange(ExchangeMic.Kmux);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Kmux");

            target = new Exchange(ExchangeMic.Clmx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Clmx");

            target = new Exchange(ExchangeMic.Hchc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Hchc");

            target = new Exchange(ExchangeMic.Ndex);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ndex");

            target = new Exchange(ExchangeMic.Imco);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Imco");

            target = new Exchange(ExchangeMic.Imeq);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Imeq");

            target = new Exchange(ExchangeMic.Ndxs);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Ndxs");

            target = new Exchange(ExchangeMic.Nlpx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Nlpx");

            target = new Exchange(ExchangeMic.Xams);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xams");

            target = new Exchange(ExchangeMic.Tnla);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Tnla");

            target = new Exchange(ExchangeMic.Xeuc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeuc");

            target = new Exchange(ExchangeMic.Xeue);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeue");

            target = new Exchange(ExchangeMic.Xeui);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xeui");

            target = new Exchange(ExchangeMic.Xems);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xems");

            target = new Exchange(ExchangeMic.Xnxc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xnxc");

            target = new Exchange(ExchangeMic.Fish);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Fish");

            target = new Exchange(ExchangeMic.Fshx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Fshx");

            target = new Exchange(ExchangeMic.Icas);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Icas");

            target = new Exchange(ExchangeMic.Nexo);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Nexo");

            target = new Exchange(ExchangeMic.Nops);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Nops");

            target = new Exchange(ExchangeMic.Norx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Norx");

            target = new Exchange(ExchangeMic.Eleu);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Eleu");

            target = new Exchange(ExchangeMic.Else);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Else");

            target = new Exchange(ExchangeMic.Elno);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Elno");

            target = new Exchange(ExchangeMic.Eluk);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Eluk");

            target = new Exchange(ExchangeMic.Frei);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Frei");

            target = new Exchange(ExchangeMic.Bulk);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Bulk");

            target = new Exchange(ExchangeMic.Stee);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Stee");

            target = new Exchange(ExchangeMic.Nosc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Nosc");

            target = new Exchange(ExchangeMic.Notc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Notc");

            target = new Exchange(ExchangeMic.Oslc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Oslc");

            target = new Exchange(ExchangeMic.Xdnb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xdnb");

            target = new Exchange(ExchangeMic.Xima);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xima");

            target = new Exchange(ExchangeMic.Xosl);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xosl");

            target = new Exchange(ExchangeMic.Xoam);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xoam");

            target = new Exchange(ExchangeMic.Xoas);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xoas");

            target = new Exchange(ExchangeMic.Nibr);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Nibr");

            target = new Exchange(ExchangeMic.Merd);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Merd");

            target = new Exchange(ExchangeMic.Merk);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Merk");

            target = new Exchange(ExchangeMic.Xosc);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xosc");

            target = new Exchange(ExchangeMic.Xoad);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xoad");

            target = new Exchange(ExchangeMic.Xosd);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xosd");

            target = new Exchange(ExchangeMic.Cclx);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Cclx");

            target = new Exchange(ExchangeMic.Mibl);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mibl");

            target = new Exchange(ExchangeMic.Rbcb);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Rbcb");

            target = new Exchange(ExchangeMic.Rbsi);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Rbsi");

            target = new Exchange(ExchangeMic.Xlux);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xlux");

            target = new Exchange(ExchangeMic.Emtf);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Emtf");

            target = new Exchange(ExchangeMic.Xves);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xves");

            target = new Exchange(ExchangeMic.Fgex);
            Assert.AreEqual(new TimeSpan(2, 0, 0), target.TimeZone, "Fgex");

            target = new Exchange(ExchangeMic.Xhel);
            Assert.AreEqual(new TimeSpan(2, 0, 0), target.TimeZone, "Xhel");

            target = new Exchange(ExchangeMic.Fnfi);
            Assert.AreEqual(new TimeSpan(2, 0, 0), target.TimeZone, "Fnfi");

            target = new Exchange(ExchangeMic.Dhel);
            Assert.AreEqual(new TimeSpan(2, 0, 0), target.TimeZone, "Dhel");

            target = new Exchange(ExchangeMic.Dnfi);
            Assert.AreEqual(new TimeSpan(2, 0, 0), target.TimeZone, "Dnfi");

            target = new Exchange(ExchangeMic.Mhel);
            Assert.AreEqual(new TimeSpan(2, 0, 0), target.TimeZone, "Mhel");

            target = new Exchange(ExchangeMic.Mnfi);
            Assert.AreEqual(new TimeSpan(2, 0, 0), target.TimeZone, "Mnfi");

            target = new Exchange(ExchangeMic.Cand);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cand");

            target = new Exchange(ExchangeMic.Canx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Canx");

            target = new Exchange(ExchangeMic.Chic);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Chic");

            target = new Exchange(ExchangeMic.Xcx2);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcx2");

            target = new Exchange(ExchangeMic.Cotc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cotc");

            target = new Exchange(ExchangeMic.Ifca);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ifca");

            target = new Exchange(ExchangeMic.Ivzx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ivzx");

            target = new Exchange(ExchangeMic.Lica);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Lica");

            target = new Exchange(ExchangeMic.Matn);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Matn");

            target = new Exchange(ExchangeMic.Neoe);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Neoe");

            target = new Exchange(ExchangeMic.Ngxc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ngxc");

            target = new Exchange(ExchangeMic.Omga);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Omga");

            target = new Exchange(ExchangeMic.Lynx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Lynx");

            target = new Exchange(ExchangeMic.Tmxs);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Tmxs");

            target = new Exchange(ExchangeMic.Xats);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xats");

            target = new Exchange(ExchangeMic.Xbbk);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xbbk");

            target = new Exchange(ExchangeMic.Xcnq);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcnq");

            target = new Exchange(ExchangeMic.Pure);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Pure");

            target = new Exchange(ExchangeMic.Xcxd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcxd");

            target = new Exchange(ExchangeMic.Xicx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xicx");

            target = new Exchange(ExchangeMic.Xmoc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xmoc");

            target = new Exchange(ExchangeMic.Xmod);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xmod");

            target = new Exchange(ExchangeMic.Xtse);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xtse");

            target = new Exchange(ExchangeMic.Xtsx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xtsx");

            target = new Exchange(ExchangeMic.Xtnx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xtnx");

            target = new Exchange(ExchangeMic.Aats);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Aats");

            target = new Exchange(ExchangeMic.Advt);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Advt");

            target = new Exchange(ExchangeMic.Aqua);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Aqua");

            target = new Exchange(ExchangeMic.Atdf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Atdf");

            target = new Exchange(ExchangeMic.Core);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Core");

            target = new Exchange(ExchangeMic.Baml);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Baml");

            target = new Exchange(ExchangeMic.Mlvx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mlvx");

            target = new Exchange(ExchangeMic.Mlco);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mlco");

            target = new Exchange(ExchangeMic.Barx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Barx");

            target = new Exchange(ExchangeMic.Bard);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bard");

            target = new Exchange(ExchangeMic.Barl);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Barl");

            target = new Exchange(ExchangeMic.Bcdx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bcdx");

            target = new Exchange(ExchangeMic.Bats);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bats");

            target = new Exchange(ExchangeMic.Bato);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bato");

            target = new Exchange(ExchangeMic.Baty);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Baty");

            target = new Exchange(ExchangeMic.Bzxd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bzxd");

            target = new Exchange(ExchangeMic.Byxd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Byxd");

            target = new Exchange(ExchangeMic.Bbsf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bbsf");

            target = new Exchange(ExchangeMic.Bgcf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bgcf");

            target = new Exchange(ExchangeMic.Fncs);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Fncs");

            target = new Exchange(ExchangeMic.Bgcd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bgcd");

            target = new Exchange(ExchangeMic.Bhsf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bhsf");

            target = new Exchange(ExchangeMic.Bids);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bids");

            target = new Exchange(ExchangeMic.Bltd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bltd");

            target = new Exchange(ExchangeMic.Bpol);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bpol");

            target = new Exchange(ExchangeMic.Bnyc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bnyc");

            target = new Exchange(ExchangeMic.Vtex);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Vtex");

            target = new Exchange(ExchangeMic.Nyfx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nyfx");

            target = new Exchange(ExchangeMic.Btec);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Btec");

            target = new Exchange(ExchangeMic.Icsu);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Icsu");

            target = new Exchange(ExchangeMic.Cded);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cded");

            target = new Exchange(ExchangeMic.Cgmi);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cgmi");

            target = new Exchange(ExchangeMic.Cicx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cicx");

            target = new Exchange(ExchangeMic.Lqfi);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Lqfi");

            target = new Exchange(ExchangeMic.Cblc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cblc");

            target = new Exchange(ExchangeMic.Cmsf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cmsf");

            target = new Exchange(ExchangeMic.Cred);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cred");

            target = new Exchange(ExchangeMic.Caes);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Caes");

            target = new Exchange(ExchangeMic.Cslp);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cslp");

            target = new Exchange(ExchangeMic.Dbsx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Dbsx");

            target = new Exchange(ExchangeMic.Deal);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Deal");

            target = new Exchange(ExchangeMic.Edge);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Edge");

            target = new Exchange(ExchangeMic.Eddp);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Eddp");

            target = new Exchange(ExchangeMic.Edga);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Edga");

            target = new Exchange(ExchangeMic.Edgd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Edgd");

            target = new Exchange(ExchangeMic.Edgx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Edgx");

            target = new Exchange(ExchangeMic.Edgo);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Edgo");

            target = new Exchange(ExchangeMic.Egmt);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Egmt");

            target = new Exchange(ExchangeMic.Eris);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Eris");

            target = new Exchange(ExchangeMic.Fast);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Fast");

            target = new Exchange(ExchangeMic.Finr);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Finr");

            target = new Exchange(ExchangeMic.Finn);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Finn");

            target = new Exchange(ExchangeMic.Fino);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Fino");

            target = new Exchange(ExchangeMic.Finy);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Finy");

            target = new Exchange(ExchangeMic.Xadf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xadf");

            target = new Exchange(ExchangeMic.Ootc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ootc");

            target = new Exchange(ExchangeMic.Fsef);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Fsef");

            target = new Exchange(ExchangeMic.Fxal);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Fxal");

            target = new Exchange(ExchangeMic.Fxcm);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Fxcm");

            target = new Exchange(ExchangeMic.G1xx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "G1xx");

            target = new Exchange(ExchangeMic.Gllc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Gllc");

            target = new Exchange(ExchangeMic.Glps);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Glps");

            target = new Exchange(ExchangeMic.Glpx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Glpx");

            target = new Exchange(ExchangeMic.Gotc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Gotc");

            target = new Exchange(ExchangeMic.Govx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Govx");

            target = new Exchange(ExchangeMic.Gree);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Gree");

            target = new Exchange(ExchangeMic.Gsco);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Gsco");

            target = new Exchange(ExchangeMic.Sgmt);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Sgmt");

            target = new Exchange(ExchangeMic.Gsef);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Gsef");

            target = new Exchange(ExchangeMic.Gtco);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Gtco");

            target = new Exchange(ExchangeMic.Gtsx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Gtsx");

            target = new Exchange(ExchangeMic.Gtxs);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Gtxs");

            target = new Exchange(ExchangeMic.Hegx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Hegx");

            target = new Exchange(ExchangeMic.Hppo);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Hppo");

            target = new Exchange(ExchangeMic.Hsfx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Hsfx");

            target = new Exchange(ExchangeMic.Icel);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Icel");

            target = new Exchange(ExchangeMic.Iexg);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Iexg");

            target = new Exchange(ExchangeMic.Iexd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Iexd");

            target = new Exchange(ExchangeMic.Ifus);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ifus");

            target = new Exchange(ExchangeMic.Iepa);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Iepa");

            target = new Exchange(ExchangeMic.Imfx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Imfx");

            target = new Exchange(ExchangeMic.Imag);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Imag");

            target = new Exchange(ExchangeMic.Imbd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Imbd");

            target = new Exchange(ExchangeMic.Imcr);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Imcr");

            target = new Exchange(ExchangeMic.Imen);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Imen");

            target = new Exchange(ExchangeMic.Imir);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Imir");

            target = new Exchange(ExchangeMic.Ifed);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ifed");

            target = new Exchange(ExchangeMic.Imcg);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Imcg");

            target = new Exchange(ExchangeMic.Imcc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Imcc");

            target = new Exchange(ExchangeMic.Ices);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ices");

            target = new Exchange(ExchangeMic.Imcs);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Imcs");

            target = new Exchange(ExchangeMic.Isda);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Isda");

            target = new Exchange(ExchangeMic.Itgi);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Itgi");

            target = new Exchange(ExchangeMic.Jefx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Jefx");

            target = new Exchange(ExchangeMic.Jlqd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Jlqd");

            target = new Exchange(ExchangeMic.Jpbx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Jpbx");

            target = new Exchange(ExchangeMic.Jpmx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Jpmx");

            target = new Exchange(ExchangeMic.Jses);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Jses");

            target = new Exchange(ExchangeMic.Jsjx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Jsjx");

            target = new Exchange(ExchangeMic.Knig);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Knig");

            target = new Exchange(ExchangeMic.Kncm);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Kncm");

            target = new Exchange(ExchangeMic.Knem);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Knem");

            target = new Exchange(ExchangeMic.Knli);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Knli");

            target = new Exchange(ExchangeMic.Knmx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Knmx");

            target = new Exchange(ExchangeMic.Ackf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ackf");

            target = new Exchange(ExchangeMic.Lasf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Lasf");

            target = new Exchange(ExchangeMic.Ledg);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ledg");

            target = new Exchange(ExchangeMic.Levl);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Levl");

            target = new Exchange(ExchangeMic.Lius);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Lius");

            target = new Exchange(ExchangeMic.Lifi);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Lifi");

            target = new Exchange(ExchangeMic.Liuh);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Liuh");

            target = new Exchange(ExchangeMic.Lqed);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Lqed");

            target = new Exchange(ExchangeMic.Ltaa);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ltaa");

            target = new Exchange(ExchangeMic.Lmnx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Lmnx");

            target = new Exchange(ExchangeMic.Mihi);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mihi");

            target = new Exchange(ExchangeMic.Xmio);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xmio");

            target = new Exchange(ExchangeMic.Mprl);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mprl");

            target = new Exchange(ExchangeMic.Msco);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Msco");

            target = new Exchange(ExchangeMic.Mspl);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mspl");

            target = new Exchange(ExchangeMic.Msrp);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Msrp");

            target = new Exchange(ExchangeMic.Mstx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mstx");

            target = new Exchange(ExchangeMic.Mslp);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mslp");

            target = new Exchange(ExchangeMic.Mtus);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mtus");

            target = new Exchange(ExchangeMic.Bvus);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bvus");

            target = new Exchange(ExchangeMic.Mtsb);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mtsb");

            target = new Exchange(ExchangeMic.Mtxx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mtxx");

            target = new Exchange(ExchangeMic.Mtxs);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mtxs");

            target = new Exchange(ExchangeMic.Mtxm);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mtxm");

            target = new Exchange(ExchangeMic.Mtxc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mtxc");

            target = new Exchange(ExchangeMic.Mtxa);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mtxa");

            target = new Exchange(ExchangeMic.Nblx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nblx");

            target = new Exchange(ExchangeMic.Nfsc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nfsc");

            target = new Exchange(ExchangeMic.Nfsa);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nfsa");

            target = new Exchange(ExchangeMic.Nfsd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nfsd");

            target = new Exchange(ExchangeMic.Xstm);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xstm");

            target = new Exchange(ExchangeMic.Nmra);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nmra");

            target = new Exchange(ExchangeMic.Nodx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nodx");

            target = new Exchange(ExchangeMic.Nxus);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nxus");

            target = new Exchange(ExchangeMic.Nypc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nypc");

            target = new Exchange(ExchangeMic.Ollc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ollc");

            target = new Exchange(ExchangeMic.Opra);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Opra");

            target = new Exchange(ExchangeMic.Otcm);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Otcm");

            target = new Exchange(ExchangeMic.Otcb);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Otcb");

            target = new Exchange(ExchangeMic.Otcq);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Otcq");

            target = new Exchange(ExchangeMic.Pinc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Pinc");

            target = new Exchange(ExchangeMic.Pini);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Pini");

            target = new Exchange(ExchangeMic.Pinl);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Pinl");

            target = new Exchange(ExchangeMic.Pinx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Pinx");

            target = new Exchange(ExchangeMic.Psgm);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Psgm");

            target = new Exchange(ExchangeMic.Cave);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cave");

            target = new Exchange(ExchangeMic.Pdqx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Pdqx");

            target = new Exchange(ExchangeMic.Pdqd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Pdqd");

            target = new Exchange(ExchangeMic.Pipe);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Pipe");

            target = new Exchange(ExchangeMic.Prse);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Prse");

            target = new Exchange(ExchangeMic.Pulx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Pulx");

            target = new Exchange(ExchangeMic.Ricx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ricx");

            target = new Exchange(ExchangeMic.Ricd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ricd");

            target = new Exchange(ExchangeMic.Scxs);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Scxs");

            target = new Exchange(ExchangeMic.Sgma);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Sgma");

            target = new Exchange(ExchangeMic.Shaw);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Shaw");

            target = new Exchange(ExchangeMic.Shad);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Shad");

            target = new Exchange(ExchangeMic.Soho);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Soho");

            target = new Exchange(ExchangeMic.Sstx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Sstx");

            target = new Exchange(ExchangeMic.Tera);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Tera");

            target = new Exchange(ExchangeMic.Tfsu);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Tfsu");

            target = new Exchange(ExchangeMic.Them);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Them");

            target = new Exchange(ExchangeMic.Thre);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Thre");

            target = new Exchange(ExchangeMic.Tmid);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Tmid");

            target = new Exchange(ExchangeMic.Tpse);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Tpse");

            target = new Exchange(ExchangeMic.Trck);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Trck");

            target = new Exchange(ExchangeMic.Trux);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Trux");

            target = new Exchange(ExchangeMic.Tru1);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Tru1");

            target = new Exchange(ExchangeMic.Tru2);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Tru2");

            target = new Exchange(ExchangeMic.Trwb);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Trwb");

            target = new Exchange(ExchangeMic.Bndd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bndd");

            target = new Exchange(ExchangeMic.Twsf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Twsf");

            target = new Exchange(ExchangeMic.Dwsf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Dwsf");

            target = new Exchange(ExchangeMic.Tsad);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Tsad");

            target = new Exchange(ExchangeMic.Tsbx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Tsbx");

            target = new Exchange(ExchangeMic.Tsef);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Tsef");

            target = new Exchange(ExchangeMic.Ubsa);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ubsa");

            target = new Exchange(ExchangeMic.Ubsp);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ubsp");

            target = new Exchange(ExchangeMic.Vert);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Vert");

            target = new Exchange(ExchangeMic.Vfcm);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Vfcm");

            target = new Exchange(ExchangeMic.Virt);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Virt");

            target = new Exchange(ExchangeMic.Weed);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Weed");

            target = new Exchange(ExchangeMic.Xwee);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xwee");

            target = new Exchange(ExchangeMic.Welx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Welx");

            target = new Exchange(ExchangeMic.Wsag);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Wsag");

            target = new Exchange(ExchangeMic.Xaqs);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xaqs");

            target = new Exchange(ExchangeMic.Xbox);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xbox");

            target = new Exchange(ExchangeMic.Xcbo);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcbo");

            target = new Exchange(ExchangeMic.C2Ox);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "C2Ox");

            target = new Exchange(ExchangeMic.Cbsx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cbsx");

            target = new Exchange(ExchangeMic.Xcbf);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcbf");

            target = new Exchange(ExchangeMic.Xcbt);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcbt");

            target = new Exchange(ExchangeMic.Fcbt);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Fcbt");

            target = new Exchange(ExchangeMic.Xkbt);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xkbt");

            target = new Exchange(ExchangeMic.Xcff);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcff");

            target = new Exchange(ExchangeMic.Xchi);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xchi");

            target = new Exchange(ExchangeMic.Xcis);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcis");

            target = new Exchange(ExchangeMic.Xcme);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcme");

            target = new Exchange(ExchangeMic.Fcme);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Fcme");

            target = new Exchange(ExchangeMic.Glbx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Glbx");

            target = new Exchange(ExchangeMic.Ximm);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Ximm");

            target = new Exchange(ExchangeMic.Xiom);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xiom");

            target = new Exchange(ExchangeMic.Nyms);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nyms");

            target = new Exchange(ExchangeMic.Cmes);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cmes");

            target = new Exchange(ExchangeMic.Cbts);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cbts");

            target = new Exchange(ExchangeMic.Cecs);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Cecs");

            target = new Exchange(ExchangeMic.Xcur);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcur");

            target = new Exchange(ExchangeMic.Xelx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xelx");

            target = new Exchange(ExchangeMic.Xfci);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xfci");

            target = new Exchange(ExchangeMic.Xgmx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xgmx");

            target = new Exchange(ExchangeMic.Xins);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xins");

            target = new Exchange(ExchangeMic.Iblx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Iblx");

            target = new Exchange(ExchangeMic.Icbx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Icbx");

            target = new Exchange(ExchangeMic.Icro);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Icro");

            target = new Exchange(ExchangeMic.Iidx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Iidx");

            target = new Exchange(ExchangeMic.Rcbx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Rcbx");

            target = new Exchange(ExchangeMic.Mocx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mocx");

            target = new Exchange(ExchangeMic.Xisx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xisx");

            target = new Exchange(ExchangeMic.Xisa);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xisa");

            target = new Exchange(ExchangeMic.Xise);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xise");

            target = new Exchange(ExchangeMic.Mcry);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Mcry");

            target = new Exchange(ExchangeMic.Gmni);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Gmni");

            target = new Exchange(ExchangeMic.Xmer);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xmer");

            target = new Exchange(ExchangeMic.Xmge);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xmge");

            target = new Exchange(ExchangeMic.Xnas);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xnas");

            target = new Exchange(ExchangeMic.Xbxo);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xbxo");

            target = new Exchange(ExchangeMic.Bosd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Bosd");

            target = new Exchange(ExchangeMic.Nasd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nasd");

            target = new Exchange(ExchangeMic.Xbrt);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xbrt");

            target = new Exchange(ExchangeMic.Xncm);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xncm");

            target = new Exchange(ExchangeMic.Xndq);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xndq");

            target = new Exchange(ExchangeMic.Xngs);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xngs");

            target = new Exchange(ExchangeMic.Xnim);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xnim");

            target = new Exchange(ExchangeMic.Xnms);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xnms");

            target = new Exchange(ExchangeMic.Xpbt);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xpbt");

            target = new Exchange(ExchangeMic.Xphl);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xphl");

            target = new Exchange(ExchangeMic.Xpho);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xpho");

            target = new Exchange(ExchangeMic.Xpor);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xpor");

            target = new Exchange(ExchangeMic.Xpsx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xpsx");

            target = new Exchange(ExchangeMic.Espd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Espd");

            target = new Exchange(ExchangeMic.Xbos);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xbos");

            target = new Exchange(ExchangeMic.Xnym);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xnym");

            target = new Exchange(ExchangeMic.Xcec);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xcec");

            target = new Exchange(ExchangeMic.Xnye);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xnye");

            target = new Exchange(ExchangeMic.Xnyl);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xnyl");

            target = new Exchange(ExchangeMic.Xnys);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xnys");

            target = new Exchange(ExchangeMic.Aldp);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Aldp");

            target = new Exchange(ExchangeMic.Amxo);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Amxo");

            target = new Exchange(ExchangeMic.Arcd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Arcd");

            target = new Exchange(ExchangeMic.Arco);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Arco");

            target = new Exchange(ExchangeMic.Arcx);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Arcx");

            target = new Exchange(ExchangeMic.Nysd);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Nysd");

            target = new Exchange(ExchangeMic.Xase);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xase");

            target = new Exchange(ExchangeMic.Xnli);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xnli");

            target = new Exchange(ExchangeMic.Xoch);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xoch");

            target = new Exchange(ExchangeMic.Xotc);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xotc");

            target = new Exchange(ExchangeMic.Xsef);
            Assert.AreEqual(new TimeSpan(-5, 0, 0), target.TimeZone, "Xsef");

            target = new Exchange(ExchangeMic.X3579);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "X3579");

            target = new Exchange(ExchangeMic.Afdl);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Afdl");

            target = new Exchange(ExchangeMic.Ampx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ampx");

            target = new Exchange(ExchangeMic.Anzl);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Anzl");

            target = new Exchange(ExchangeMic.Aqxe);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Aqxe");

            target = new Exchange(ExchangeMic.Arax);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Arax");

            target = new Exchange(ExchangeMic.Atlb);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Atlb");

            target = new Exchange(ExchangeMic.Autx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Autx");

            target = new Exchange(ExchangeMic.Autp);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Autp");

            target = new Exchange(ExchangeMic.Autb);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Autb");

            target = new Exchange(ExchangeMic.Balt);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Balt");

            target = new Exchange(ExchangeMic.Bltx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bltx");

            target = new Exchange(ExchangeMic.Bapa);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bapa");

            target = new Exchange(ExchangeMic.Bcrm);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bcrm");

            target = new Exchange(ExchangeMic.Baro);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Baro");

            target = new Exchange(ExchangeMic.Bark);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bark");

            target = new Exchange(ExchangeMic.Bart);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bart");

            target = new Exchange(ExchangeMic.Bcxe);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bcxe");

            target = new Exchange(ExchangeMic.Bate);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bate");

            target = new Exchange(ExchangeMic.Chix);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Chix");

            target = new Exchange(ExchangeMic.Batd);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Batd");

            target = new Exchange(ExchangeMic.Chid);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Chid");

            target = new Exchange(ExchangeMic.Batf);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Batf");

            target = new Exchange(ExchangeMic.Chio);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Chio");

            target = new Exchange(ExchangeMic.Batp);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Batp");

            target = new Exchange(ExchangeMic.Botc);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Botc");

            target = new Exchange(ExchangeMic.Lisx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Lisx");

            target = new Exchange(ExchangeMic.Bgci);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bgci");

            target = new Exchange(ExchangeMic.Bgcb);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bgcb");

            target = new Exchange(ExchangeMic.Bkln);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bkln");

            target = new Exchange(ExchangeMic.Bklf);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bklf");

            target = new Exchange(ExchangeMic.Blox);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Blox");

            target = new Exchange(ExchangeMic.Bmtf);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bmtf");

            target = new Exchange(ExchangeMic.Boat);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Boat");

            target = new Exchange(ExchangeMic.Bosc);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bosc");

            target = new Exchange(ExchangeMic.Brnx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Brnx");

            target = new Exchange(ExchangeMic.Btee);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Btee");

            target = new Exchange(ExchangeMic.Ebsm);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ebsm");

            target = new Exchange(ExchangeMic.Ebsd);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ebsd");

            target = new Exchange(ExchangeMic.Ebsi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ebsi");

            target = new Exchange(ExchangeMic.Nexy);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nexy");

            target = new Exchange(ExchangeMic.Ccml);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ccml");

            target = new Exchange(ExchangeMic.Cco2);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cco2");

            target = new Exchange(ExchangeMic.Cgme);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cgme");

            target = new Exchange(ExchangeMic.Chev);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Chev");

            target = new Exchange(ExchangeMic.Blnk);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Blnk");

            target = new Exchange(ExchangeMic.Cmee);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cmee");

            target = new Exchange(ExchangeMic.Cmec);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cmec");

            target = new Exchange(ExchangeMic.Cmed);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cmed");

            target = new Exchange(ExchangeMic.Cmmt);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cmmt");

            target = new Exchange(ExchangeMic.Cryp);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cryp");

            target = new Exchange(ExchangeMic.Cseu);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cseu");

            target = new Exchange(ExchangeMic.Cscf);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cscf");

            target = new Exchange(ExchangeMic.Csbx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Csbx");

            target = new Exchange(ExchangeMic.Sics);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Sics");

            target = new Exchange(ExchangeMic.Csin);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Csin");

            target = new Exchange(ExchangeMic.Cssi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cssi");

            target = new Exchange(ExchangeMic.Dbes);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Dbes");

            target = new Exchange(ExchangeMic.Dbix);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Dbix");

            target = new Exchange(ExchangeMic.Dbdc);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Dbdc");

            target = new Exchange(ExchangeMic.Dbcx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Dbcx");

            target = new Exchange(ExchangeMic.Dbcr);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Dbcr");

            target = new Exchange(ExchangeMic.Dbmo);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Dbmo");

            target = new Exchange(ExchangeMic.Dbse);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Dbse");

            target = new Exchange(ExchangeMic.Dowg);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Dowg");

            target = new Exchange(ExchangeMic.Echo);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Echo");

            target = new Exchange(ExchangeMic.Embx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Embx");

            target = new Exchange(ExchangeMic.Encl);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Encl");

            target = new Exchange(ExchangeMic.Eqld);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Eqld");

            target = new Exchange(ExchangeMic.Exeu);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Exeu");

            target = new Exchange(ExchangeMic.Exmp);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Exmp");

            target = new Exchange(ExchangeMic.Exor);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Exor");

            target = new Exchange(ExchangeMic.Exvp);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Exvp");

            target = new Exchange(ExchangeMic.Exbo);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Exbo");

            target = new Exchange(ExchangeMic.Exlp);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Exlp");

            target = new Exchange(ExchangeMic.Exdc);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Exdc");

            target = new Exchange(ExchangeMic.Exsi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Exsi");

            target = new Exchange(ExchangeMic.Excp);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Excp");

            target = new Exchange(ExchangeMic.Exot);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Exot");

            target = new Exchange(ExchangeMic.Fair);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Fair");

            target = new Exchange(ExchangeMic.Fisu);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Fisu");

            target = new Exchange(ExchangeMic.Fxgb);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Fxgb");

            target = new Exchange(ExchangeMic.Gemx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gemx");

            target = new Exchange(ExchangeMic.Gfic);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gfic");

            target = new Exchange(ExchangeMic.Gfif);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gfif");

            target = new Exchange(ExchangeMic.Gfin);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gfin");

            target = new Exchange(ExchangeMic.Gfir);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gfir");

            target = new Exchange(ExchangeMic.Gmeg);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gmeg");

            target = new Exchange(ExchangeMic.Xldx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xldx");

            target = new Exchange(ExchangeMic.Xgdx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xgdx");

            target = new Exchange(ExchangeMic.Xgsx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xgsx");

            target = new Exchange(ExchangeMic.Xgcx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xgcx");

            target = new Exchange(ExchangeMic.Grif);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Grif");

            target = new Exchange(ExchangeMic.Grio);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Grio");

            target = new Exchange(ExchangeMic.Grse);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Grse");

            target = new Exchange(ExchangeMic.Gsib);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gsib");

            target = new Exchange(ExchangeMic.Bisi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bisi");

            target = new Exchange(ExchangeMic.Gsil);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gsil");

            target = new Exchange(ExchangeMic.Gssi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gssi");

            target = new Exchange(ExchangeMic.Gsbx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gsbx");

            target = new Exchange(ExchangeMic.Hpcs);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Hpcs");

            target = new Exchange(ExchangeMic.Hsbc);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Hsbc");

            target = new Exchange(ExchangeMic.Ibal);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ibal");

            target = new Exchange(ExchangeMic.Icap);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Icap");

            target = new Exchange(ExchangeMic.Icah);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Icah");

            target = new Exchange(ExchangeMic.Icen);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Icen");

            target = new Exchange(ExchangeMic.Icse);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Icse");

            target = new Exchange(ExchangeMic.Ictq);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ictq");

            target = new Exchange(ExchangeMic.Wclk);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Wclk");

            target = new Exchange(ExchangeMic.Igdl);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Igdl");

            target = new Exchange(ExchangeMic.Ifeu);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ifeu");

            target = new Exchange(ExchangeMic.Cxrt);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cxrt");

            target = new Exchange(ExchangeMic.Iflo);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Iflo");

            target = new Exchange(ExchangeMic.Ifll);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ifll");

            target = new Exchange(ExchangeMic.Ifut);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ifut");

            target = new Exchange(ExchangeMic.Iflx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Iflx");

            target = new Exchange(ExchangeMic.Ifen);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ifen");

            target = new Exchange(ExchangeMic.Cxot);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Cxot");

            target = new Exchange(ExchangeMic.Ifls);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ifls");

            target = new Exchange(ExchangeMic.Inve);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Inve");

            target = new Exchange(ExchangeMic.Iswa);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Iswa");

            target = new Exchange(ExchangeMic.Jpcb);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Jpcb");

            target = new Exchange(ExchangeMic.Jpsi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Jpsi");

            target = new Exchange(ExchangeMic.Jssi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Jssi");

            target = new Exchange(ExchangeMic.Kleu);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Kleu");

            target = new Exchange(ExchangeMic.Lcur);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Lcur");

            target = new Exchange(ExchangeMic.Liqu);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Liqu");

            target = new Exchange(ExchangeMic.Liqh);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Liqh");

            target = new Exchange(ExchangeMic.Liqf);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Liqf");

            target = new Exchange(ExchangeMic.Lmax);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Lmax");

            target = new Exchange(ExchangeMic.Lmad);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Lmad");

            target = new Exchange(ExchangeMic.Lmae);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Lmae");

            target = new Exchange(ExchangeMic.Lmaf);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Lmaf");

            target = new Exchange(ExchangeMic.Lmao);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Lmao");

            target = new Exchange(ExchangeMic.Lmec);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Lmec");

            target = new Exchange(ExchangeMic.Lotc);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Lotc");

            target = new Exchange(ExchangeMic.Pldx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Pldx");

            target = new Exchange(ExchangeMic.Lppm);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Lppm");

            target = new Exchange(ExchangeMic.Mael);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mael");

            target = new Exchange(ExchangeMic.Mcur);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mcur");

            target = new Exchange(ExchangeMic.Mcxs);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mcxs");

            target = new Exchange(ExchangeMic.Mcxr);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mcxr");

            target = new Exchange(ExchangeMic.Mfgl);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mfgl");

            target = new Exchange(ExchangeMic.Mfxc);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mfxc");

            target = new Exchange(ExchangeMic.Mfxa);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mfxa");

            target = new Exchange(ExchangeMic.Mfxr);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mfxr");

            target = new Exchange(ExchangeMic.Mhip);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mhip");

            target = new Exchange(ExchangeMic.Mlxn);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mlxn");

            target = new Exchange(ExchangeMic.Mlax);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mlax");

            target = new Exchange(ExchangeMic.Mleu);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mleu");

            target = new Exchange(ExchangeMic.Mlve);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mlve");

            target = new Exchange(ExchangeMic.Msip);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Msip");

            target = new Exchange(ExchangeMic.Mssi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mssi");

            target = new Exchange(ExchangeMic.Mufp);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mufp");

            target = new Exchange(ExchangeMic.Muti);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Muti");

            target = new Exchange(ExchangeMic.Mytr);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mytr");

            target = new Exchange(ExchangeMic.N2Ex);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "N2Ex");

            target = new Exchange(ExchangeMic.Ndcm);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ndcm");

            target = new Exchange(ExchangeMic.Nexs);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nexs");

            target = new Exchange(ExchangeMic.Nexx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nexx");

            target = new Exchange(ExchangeMic.Nexf);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nexf");

            target = new Exchange(ExchangeMic.Nexg);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nexg");

            target = new Exchange(ExchangeMic.Next);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Next");

            target = new Exchange(ExchangeMic.Nexn);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nexn");

            target = new Exchange(ExchangeMic.Nexd);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nexd");

            target = new Exchange(ExchangeMic.Nexl);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nexl");

            target = new Exchange(ExchangeMic.Noff);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Noff");

            target = new Exchange(ExchangeMic.Nosi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nosi");

            target = new Exchange(ExchangeMic.Nuro);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nuro");

            target = new Exchange(ExchangeMic.Xnlx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xnlx");

            target = new Exchange(ExchangeMic.Nurd);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nurd");

            target = new Exchange(ExchangeMic.Nxeu);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nxeu");

            target = new Exchange(ExchangeMic.Otce);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Otce");

            target = new Exchange(ExchangeMic.Peel);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Peel");

            target = new Exchange(ExchangeMic.Xrsp);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xrsp");

            target = new Exchange(ExchangeMic.Xphx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xphx");

            target = new Exchange(ExchangeMic.Pieu);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Pieu");

            target = new Exchange(ExchangeMic.Pirm);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Pirm");

            target = new Exchange(ExchangeMic.Ppex);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ppex");

            target = new Exchange(ExchangeMic.Qwix);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Qwix");

            target = new Exchange(ExchangeMic.Rbce);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Rbce");

            target = new Exchange(ExchangeMic.Rbct);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Rbct");

            target = new Exchange(ExchangeMic.Rtsi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Rtsi");

            target = new Exchange(ExchangeMic.Rbsx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Rbsx");

            target = new Exchange(ExchangeMic.Rtsl);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Rtsl");

            target = new Exchange(ExchangeMic.Trfw);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Trfw");

            target = new Exchange(ExchangeMic.Tral);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tral");

            target = new Exchange(ExchangeMic.Secf);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Secf");

            target = new Exchange(ExchangeMic.Sedr);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Sedr");

            target = new Exchange(ExchangeMic.Sgmx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Sgmx");

            target = new Exchange(ExchangeMic.Sgmy);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Sgmy");

            target = new Exchange(ExchangeMic.Shar);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Shar");

            target = new Exchange(ExchangeMic.Spec);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Spec");

            target = new Exchange(ExchangeMic.Sprz);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Sprz");

            target = new Exchange(ExchangeMic.Ssex);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ssex");

            target = new Exchange(ExchangeMic.Stal);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Stal");

            target = new Exchange(ExchangeMic.Stan);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Stan");

            target = new Exchange(ExchangeMic.Stsi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Stsi");

            target = new Exchange(ExchangeMic.Swap);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Swap");

            target = new Exchange(ExchangeMic.Tcml);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tcml");

            target = new Exchange(ExchangeMic.Tfsv);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tfsv");

            target = new Exchange(ExchangeMic.Fxop);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Fxop");

            target = new Exchange(ExchangeMic.Tpie);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tpie");

            target = new Exchange(ExchangeMic.Trax);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Trax");

            target = new Exchange(ExchangeMic.Trde);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Trde");

            target = new Exchange(ExchangeMic.Nave);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Nave");

            target = new Exchange(ExchangeMic.Tcds);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tcds");

            target = new Exchange(ExchangeMic.Trdx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Trdx");

            target = new Exchange(ExchangeMic.Tfsg);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tfsg");

            target = new Exchange(ExchangeMic.Parx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Parx");

            target = new Exchange(ExchangeMic.Elix);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Elix");

            target = new Exchange(ExchangeMic.Treu);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Treu");

            target = new Exchange(ExchangeMic.Trea);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Trea");

            target = new Exchange(ExchangeMic.Treo);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Treo");

            target = new Exchange(ExchangeMic.Trqx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Trqx");

            target = new Exchange(ExchangeMic.Trqm);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Trqm");

            target = new Exchange(ExchangeMic.Trqs);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Trqs");

            target = new Exchange(ExchangeMic.Trqa);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Trqa");

            target = new Exchange(ExchangeMic.Trsi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Trsi");

            target = new Exchange(ExchangeMic.Ubsb);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ubsb");

            target = new Exchange(ExchangeMic.Ubsy);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ubsy");

            target = new Exchange(ExchangeMic.Ubsl);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ubsl");

            target = new Exchange(ExchangeMic.Ubse);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ubse");

            target = new Exchange(ExchangeMic.Ubsi);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ubsi");

            target = new Exchange(ExchangeMic.Ukpx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ukpx");

            target = new Exchange(ExchangeMic.Vcmo);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Vcmo");

            target = new Exchange(ExchangeMic.Vega);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Vega");

            target = new Exchange(ExchangeMic.Wins);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Wins");

            target = new Exchange(ExchangeMic.Winx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Winx");

            target = new Exchange(ExchangeMic.Xalt);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xalt");

            target = new Exchange(ExchangeMic.Xcor);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xcor");

            target = new Exchange(ExchangeMic.Xgcl);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xgcl");

            target = new Exchange(ExchangeMic.Xlbm);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xlbm");

            target = new Exchange(ExchangeMic.Xlch);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xlch");

            target = new Exchange(ExchangeMic.Xldn);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xldn");

            target = new Exchange(ExchangeMic.Xsmp);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xsmp");

            target = new Exchange(ExchangeMic.Ensy);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ensy");

            target = new Exchange(ExchangeMic.Xlme);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xlme");

            target = new Exchange(ExchangeMic.Xlon);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xlon");

            target = new Exchange(ExchangeMic.Aimx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Aimx");

            target = new Exchange(ExchangeMic.Xlod);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xlod");

            target = new Exchange(ExchangeMic.Xlom);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xlom");

            target = new Exchange(ExchangeMic.Xmts);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xmts");

            target = new Exchange(ExchangeMic.Hung);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Hung");

            target = new Exchange(ExchangeMic.Ukgd);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Ukgd");

            target = new Exchange(ExchangeMic.Amts);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Amts");

            target = new Exchange(ExchangeMic.Emts);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Emts");

            target = new Exchange(ExchangeMic.Gmts);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Gmts");

            target = new Exchange(ExchangeMic.Imts);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Imts");

            target = new Exchange(ExchangeMic.Mczk);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mczk");

            target = new Exchange(ExchangeMic.Mtsa);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mtsa");

            target = new Exchange(ExchangeMic.Mtsg);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mtsg");

            target = new Exchange(ExchangeMic.Mtss);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mtss");

            target = new Exchange(ExchangeMic.Rmts);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Rmts");

            target = new Exchange(ExchangeMic.Smts);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Smts");

            target = new Exchange(ExchangeMic.Vmts);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Vmts");

            target = new Exchange(ExchangeMic.Bvuk);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Bvuk");

            target = new Exchange(ExchangeMic.Port);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Port");

            target = new Exchange(ExchangeMic.Mtsw);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mtsw");

            target = new Exchange(ExchangeMic.Xsga);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xsga");

            target = new Exchange(ExchangeMic.Xswb);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xswb");

            target = new Exchange(ExchangeMic.Xtup);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xtup");

            target = new Exchange(ExchangeMic.Tpeq);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tpeq");

            target = new Exchange(ExchangeMic.Tben);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tben");

            target = new Exchange(ExchangeMic.Tbla);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tbla");

            target = new Exchange(ExchangeMic.Tpcd);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tpcd");

            target = new Exchange(ExchangeMic.Tpfd);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tpfd");

            target = new Exchange(ExchangeMic.Tpre);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tpre");

            target = new Exchange(ExchangeMic.Tpsd);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tpsd");

            target = new Exchange(ExchangeMic.Xtpe);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xtpe");

            target = new Exchange(ExchangeMic.Tpel);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tpel");

            target = new Exchange(ExchangeMic.Tpsl);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Tpsl");

            target = new Exchange(ExchangeMic.Xubs);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xubs");

            target = new Exchange(ExchangeMic.Xice);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xice");

            target = new Exchange(ExchangeMic.Dice);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Dice");

            target = new Exchange(ExchangeMic.Fnis);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Fnis");

            target = new Exchange(ExchangeMic.Dnis);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Dnis");

            target = new Exchange(ExchangeMic.Mice);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mice");

            target = new Exchange(ExchangeMic.Mnis);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Mnis");

            target = new Exchange(ExchangeMic.Omic);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Omic");

            target = new Exchange(ExchangeMic.Opex);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Opex");

            target = new Exchange(ExchangeMic.Omip);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Omip");

            target = new Exchange(ExchangeMic.Xlis);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Xlis");

            target = new Exchange(ExchangeMic.Alxl);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Alxl");

            target = new Exchange(ExchangeMic.Enxl);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Enxl");

            target = new Exchange(ExchangeMic.Mfox);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Mfox");

            target = new Exchange(ExchangeMic.Wqxl);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.TimeZone, "Wqxl");

            target = new Exchange(ExchangeMic.Xxxx);
            Assert.AreEqual(new TimeSpan(0, 0, 0), target.TimeZone, "Xxxx");
        }

        [TestMethod]
        public void Exchange_IsEuronext_CorrectResult()
        {
            var target = new Exchange(ExchangeMic.Xbru);
            Assert.IsTrue(target.IsEuronext,  "Xbru");

            target = new Exchange(ExchangeMic.Alxb);
            Assert.IsTrue(target.IsEuronext,  "Alxb");

            target = new Exchange(ExchangeMic.Enxb);
            Assert.IsTrue(target.IsEuronext,  "Enxb");

            target = new Exchange(ExchangeMic.Mlxb);
            Assert.IsTrue(target.IsEuronext,  "Mlxb");

            target = new Exchange(ExchangeMic.Tnlb);
            Assert.IsTrue(target.IsEuronext,  "Tnlb");

            target = new Exchange(ExchangeMic.Vpxb);
            Assert.IsTrue(target.IsEuronext,  "Vpxb");

            target = new Exchange(ExchangeMic.Xbrd);
            Assert.IsTrue(target.IsEuronext,  "Xbrd");

            target = new Exchange(ExchangeMic.Xpar);
            Assert.IsTrue(target.IsEuronext,  "Xpar");

            target = new Exchange(ExchangeMic.Alxp);
            Assert.IsTrue(target.IsEuronext,  "Alxp");

            target = new Exchange(ExchangeMic.Xmat);
            Assert.IsTrue(target.IsEuronext,  "Xmat");

            target = new Exchange(ExchangeMic.Xmli);
            Assert.IsTrue(target.IsEuronext,  "Xmli");

            target = new Exchange(ExchangeMic.Xmon);
            Assert.IsTrue(target.IsEuronext,  "Xmon");

            target = new Exchange(ExchangeMic.Xspm);
            Assert.IsTrue(target.IsEuronext,  "Xspm");

            target = new Exchange(ExchangeMic.Xlis);
            Assert.IsTrue(target.IsEuronext,  "Xlis");

            target = new Exchange(ExchangeMic.Alxl);
            Assert.IsTrue(target.IsEuronext,  "Alxl");

            target = new Exchange(ExchangeMic.Enxl);
            Assert.IsTrue(target.IsEuronext,  "Enxl");

            target = new Exchange(ExchangeMic.Mfox);
            Assert.IsTrue(target.IsEuronext,  "Mfox");

            target = new Exchange(ExchangeMic.Wqxl);
            Assert.IsTrue(target.IsEuronext,  "Wqxl");

            target = new Exchange(ExchangeMic.Xams);
            Assert.IsTrue(target.IsEuronext,  "Xams");

            target = new Exchange(ExchangeMic.Tnla);
            Assert.IsTrue(target.IsEuronext,  "Tnla");

            target = new Exchange(ExchangeMic.Xeuc);
            Assert.IsTrue(target.IsEuronext,  "Xeuc");

            target = new Exchange(ExchangeMic.Xeue);
            Assert.IsTrue(target.IsEuronext,  "Xeue");

            target = new Exchange(ExchangeMic.Xeui);
            Assert.IsTrue(target.IsEuronext,  "Xeui");

            target = new Exchange(ExchangeMic.Xldn);
            Assert.IsTrue(target.IsEuronext,  "Xldn");

            target = new Exchange(ExchangeMic.Xsmp);
            Assert.IsTrue(target.IsEuronext,  "Xsmp");

            target = new Exchange(ExchangeMic.Ensy);
            Assert.IsTrue(target.IsEuronext,  "Ensy");

            target = new Exchange(ExchangeMic.Egsi);
            Assert.IsFalse(target.IsEuronext,  "Egsi");

            target = new Exchange(ExchangeMic.Xwbo);
            Assert.IsFalse(target.IsEuronext,  "Xwbo");

            target = new Exchange(ExchangeMic.Exaa);
            Assert.IsFalse(target.IsEuronext,  "Exaa");

            target = new Exchange(ExchangeMic.Wbah);
            Assert.IsFalse(target.IsEuronext,  "Wbah");

            target = new Exchange(ExchangeMic.Wbdm);
            Assert.IsFalse(target.IsEuronext,  "Wbdm");

            target = new Exchange(ExchangeMic.Wbgf);
            Assert.IsFalse(target.IsEuronext,  "Wbgf");

            target = new Exchange(ExchangeMic.Xvie);
            Assert.IsFalse(target.IsEuronext,  "Xvie");

            target = new Exchange(ExchangeMic.Beam);
            Assert.IsFalse(target.IsEuronext,  "Beam");

            target = new Exchange(ExchangeMic.Bmts);
            Assert.IsFalse(target.IsEuronext,  "Bmts");

            target = new Exchange(ExchangeMic.Mtsd);
            Assert.IsFalse(target.IsEuronext,  "Mtsd");

            target = new Exchange(ExchangeMic.Mtsf);
            Assert.IsFalse(target.IsEuronext,  "Mtsf");

            target = new Exchange(ExchangeMic.Blpx);
            Assert.IsFalse(target.IsEuronext,  "Blpx");

            target = new Exchange(ExchangeMic.Cand);
            Assert.IsFalse(target.IsEuronext,  "Cand");

            target = new Exchange(ExchangeMic.Canx);
            Assert.IsFalse(target.IsEuronext,  "Canx");

            target = new Exchange(ExchangeMic.Chic);
            Assert.IsFalse(target.IsEuronext,  "Chic");

            target = new Exchange(ExchangeMic.Xcx2);
            Assert.IsFalse(target.IsEuronext,  "Xcx2");

            target = new Exchange(ExchangeMic.Cotc);
            Assert.IsFalse(target.IsEuronext,  "Cotc");

            target = new Exchange(ExchangeMic.Ifca);
            Assert.IsFalse(target.IsEuronext,  "Ifca");

            target = new Exchange(ExchangeMic.Ivzx);
            Assert.IsFalse(target.IsEuronext,  "Ivzx");

            target = new Exchange(ExchangeMic.Lica);
            Assert.IsFalse(target.IsEuronext,  "Lica");

            target = new Exchange(ExchangeMic.Matn);
            Assert.IsFalse(target.IsEuronext,  "Matn");

            target = new Exchange(ExchangeMic.Neoe);
            Assert.IsFalse(target.IsEuronext,  "Neoe");

            target = new Exchange(ExchangeMic.Ngxc);
            Assert.IsFalse(target.IsEuronext,  "Ngxc");

            target = new Exchange(ExchangeMic.Omga);
            Assert.IsFalse(target.IsEuronext,  "Omga");

            target = new Exchange(ExchangeMic.Lynx);
            Assert.IsFalse(target.IsEuronext,  "Lynx");

            target = new Exchange(ExchangeMic.Tmxs);
            Assert.IsFalse(target.IsEuronext,  "Tmxs");

            target = new Exchange(ExchangeMic.Xats);
            Assert.IsFalse(target.IsEuronext,  "Xats");

            target = new Exchange(ExchangeMic.Xbbk);
            Assert.IsFalse(target.IsEuronext,  "Xbbk");

            target = new Exchange(ExchangeMic.Xcnq);
            Assert.IsFalse(target.IsEuronext,  "Xcnq");

            target = new Exchange(ExchangeMic.Pure);
            Assert.IsFalse(target.IsEuronext,  "Pure");

            target = new Exchange(ExchangeMic.Xcxd);
            Assert.IsFalse(target.IsEuronext,  "Xcxd");

            target = new Exchange(ExchangeMic.Xicx);
            Assert.IsFalse(target.IsEuronext,  "Xicx");

            target = new Exchange(ExchangeMic.Xmoc);
            Assert.IsFalse(target.IsEuronext,  "Xmoc");

            target = new Exchange(ExchangeMic.Xmod);
            Assert.IsFalse(target.IsEuronext,  "Xmod");

            target = new Exchange(ExchangeMic.Xtse);
            Assert.IsFalse(target.IsEuronext,  "Xtse");

            target = new Exchange(ExchangeMic.Xtsx);
            Assert.IsFalse(target.IsEuronext,  "Xtsx");

            target = new Exchange(ExchangeMic.Xtnx);
            Assert.IsFalse(target.IsEuronext,  "Xtnx");

            target = new Exchange(ExchangeMic.Dasi);
            Assert.IsFalse(target.IsEuronext,  "Dasi");

            target = new Exchange(ExchangeMic.Dktc);
            Assert.IsFalse(target.IsEuronext,  "Dktc");

            target = new Exchange(ExchangeMic.Gxgr);
            Assert.IsFalse(target.IsEuronext,  "Gxgr");

            target = new Exchange(ExchangeMic.Gxgf);
            Assert.IsFalse(target.IsEuronext,  "Gxgf");

            target = new Exchange(ExchangeMic.Gxgm);
            Assert.IsFalse(target.IsEuronext,  "Gxgm");

            target = new Exchange(ExchangeMic.Jbsi);
            Assert.IsFalse(target.IsEuronext,  "Jbsi");

            target = new Exchange(ExchangeMic.Npga);
            Assert.IsFalse(target.IsEuronext,  "Npga");

            target = new Exchange(ExchangeMic.Snsi);
            Assert.IsFalse(target.IsEuronext,  "Snsi");

            target = new Exchange(ExchangeMic.Xcse);
            Assert.IsFalse(target.IsEuronext,  "Xcse");

            target = new Exchange(ExchangeMic.Dcse);
            Assert.IsFalse(target.IsEuronext,  "Dcse");

            target = new Exchange(ExchangeMic.Fndk);
            Assert.IsFalse(target.IsEuronext,  "Fndk");

            target = new Exchange(ExchangeMic.Dndk);
            Assert.IsFalse(target.IsEuronext,  "Dndk");

            target = new Exchange(ExchangeMic.Mcse);
            Assert.IsFalse(target.IsEuronext,  "Mcse");

            target = new Exchange(ExchangeMic.Mndk);
            Assert.IsFalse(target.IsEuronext,  "Mndk");

            target = new Exchange(ExchangeMic.Fgex);
            Assert.IsFalse(target.IsEuronext,  "Fgex");

            target = new Exchange(ExchangeMic.Xhel);
            Assert.IsFalse(target.IsEuronext,  "Xhel");

            target = new Exchange(ExchangeMic.Fnfi);
            Assert.IsFalse(target.IsEuronext,  "Fnfi");

            target = new Exchange(ExchangeMic.Dhel);
            Assert.IsFalse(target.IsEuronext,  "Dhel");

            target = new Exchange(ExchangeMic.Dnfi);
            Assert.IsFalse(target.IsEuronext,  "Dnfi");

            target = new Exchange(ExchangeMic.Mhel);
            Assert.IsFalse(target.IsEuronext,  "Mhel");

            target = new Exchange(ExchangeMic.Mnfi);
            Assert.IsFalse(target.IsEuronext,  "Mnfi");

            target = new Exchange(ExchangeMic.Coal);
            Assert.IsFalse(target.IsEuronext,  "Coal");

            target = new Exchange(ExchangeMic.Epex);
            Assert.IsFalse(target.IsEuronext,  "Epex");

            target = new Exchange(ExchangeMic.Exse);
            Assert.IsFalse(target.IsEuronext,  "Exse");

            target = new Exchange(ExchangeMic.Fmts);
            Assert.IsFalse(target.IsEuronext,  "Fmts");

            target = new Exchange(ExchangeMic.Gmtf);
            Assert.IsFalse(target.IsEuronext,  "Gmtf");

            target = new Exchange(ExchangeMic.Lchc);
            Assert.IsFalse(target.IsEuronext,  "Lchc");

            target = new Exchange(ExchangeMic.Natx);
            Assert.IsFalse(target.IsEuronext,  "Natx");

            target = new Exchange(ExchangeMic.Xafr);
            Assert.IsFalse(target.IsEuronext,  "Xafr");

            target = new Exchange(ExchangeMic.Xbln);
            Assert.IsFalse(target.IsEuronext,  "Xbln");

            target = new Exchange(ExchangeMic.Mtch);
            Assert.IsFalse(target.IsEuronext,  "Mtch");

            target = new Exchange(ExchangeMic.Xpow);
            Assert.IsFalse(target.IsEuronext,  "Xpow");

            target = new Exchange(ExchangeMic.Xpsf);
            Assert.IsFalse(target.IsEuronext,  "Xpsf");

            target = new Exchange(ExchangeMic.Xpot);
            Assert.IsFalse(target.IsEuronext,  "Xpot");

            target = new Exchange(ExchangeMic.X360T);
            Assert.IsFalse(target.IsEuronext,  "X360T");

            target = new Exchange(ExchangeMic.Baad);
            Assert.IsFalse(target.IsEuronext,  "Baad");

            target = new Exchange(ExchangeMic.Cats);
            Assert.IsFalse(target.IsEuronext,  "Cats");

            target = new Exchange(ExchangeMic.Dapa);
            Assert.IsFalse(target.IsEuronext,  "Dapa");

            target = new Exchange(ExchangeMic.Dbox);
            Assert.IsFalse(target.IsEuronext,  "Dbox");

            target = new Exchange(ExchangeMic.Auto);
            Assert.IsFalse(target.IsEuronext,  "Auto");

            target = new Exchange(ExchangeMic.Ecag);
            Assert.IsFalse(target.IsEuronext,  "Ecag");

            target = new Exchange(ExchangeMic.Ficx);
            Assert.IsFalse(target.IsEuronext,  "Ficx");

            target = new Exchange(ExchangeMic.Hsbt);
            Assert.IsFalse(target.IsEuronext,  "Hsbt");

            target = new Exchange(ExchangeMic.Tgat);
            Assert.IsFalse(target.IsEuronext,  "Tgat");

            target = new Exchange(ExchangeMic.Xgat);
            Assert.IsFalse(target.IsEuronext,  "Xgat");

            target = new Exchange(ExchangeMic.Xgrm);
            Assert.IsFalse(target.IsEuronext,  "Xgrm");

            target = new Exchange(ExchangeMic.Vwdx);
            Assert.IsFalse(target.IsEuronext,  "Vwdx");

            target = new Exchange(ExchangeMic.Xber);
            Assert.IsFalse(target.IsEuronext,  "Xber");

            target = new Exchange(ExchangeMic.Bera);
            Assert.IsFalse(target.IsEuronext,  "Bera");

            target = new Exchange(ExchangeMic.Berb);
            Assert.IsFalse(target.IsEuronext,  "Berb");

            target = new Exchange(ExchangeMic.Berc);
            Assert.IsFalse(target.IsEuronext,  "Berc");

            target = new Exchange(ExchangeMic.Eqta);
            Assert.IsFalse(target.IsEuronext,  "Eqta");

            target = new Exchange(ExchangeMic.Eqtb);
            Assert.IsFalse(target.IsEuronext,  "Eqtb");

            target = new Exchange(ExchangeMic.Eqtc);
            Assert.IsFalse(target.IsEuronext,  "Eqtc");

            target = new Exchange(ExchangeMic.Eqtd);
            Assert.IsFalse(target.IsEuronext,  "Eqtd");

            target = new Exchange(ExchangeMic.Xeqt);
            Assert.IsFalse(target.IsEuronext,  "Xeqt");

            target = new Exchange(ExchangeMic.Zobx);
            Assert.IsFalse(target.IsEuronext,  "Zobx");

            target = new Exchange(ExchangeMic.Xdus);
            Assert.IsFalse(target.IsEuronext,  "Xdus");

            target = new Exchange(ExchangeMic.Dusa);
            Assert.IsFalse(target.IsEuronext,  "Dusa");

            target = new Exchange(ExchangeMic.Dusb);
            Assert.IsFalse(target.IsEuronext,  "Dusb");

            target = new Exchange(ExchangeMic.Dusc);
            Assert.IsFalse(target.IsEuronext,  "Dusc");

            target = new Exchange(ExchangeMic.Dusd);
            Assert.IsFalse(target.IsEuronext,  "Dusd");

            target = new Exchange(ExchangeMic.Xqtx);
            Assert.IsFalse(target.IsEuronext,  "Xqtx");

            target = new Exchange(ExchangeMic.Xecb);
            Assert.IsFalse(target.IsEuronext,  "Xecb");

            target = new Exchange(ExchangeMic.Xecc);
            Assert.IsFalse(target.IsEuronext,  "Xecc");

            target = new Exchange(ExchangeMic.Xeee);
            Assert.IsFalse(target.IsEuronext,  "Xeee");

            target = new Exchange(ExchangeMic.Xeeo);
            Assert.IsFalse(target.IsEuronext,  "Xeeo");

            target = new Exchange(ExchangeMic.Xeer);
            Assert.IsFalse(target.IsEuronext,  "Xeer");

            target = new Exchange(ExchangeMic.Xetr);
            Assert.IsFalse(target.IsEuronext,  "Xetr");

            target = new Exchange(ExchangeMic.Xeub);
            Assert.IsFalse(target.IsEuronext,  "Xeub");

            target = new Exchange(ExchangeMic.Xeta);
            Assert.IsFalse(target.IsEuronext,  "Xeta");

            target = new Exchange(ExchangeMic.Xetb);
            Assert.IsFalse(target.IsEuronext,  "Xetb");

            target = new Exchange(ExchangeMic.Xeup);
            Assert.IsFalse(target.IsEuronext,  "Xeup");

            target = new Exchange(ExchangeMic.Xeum);
            Assert.IsFalse(target.IsEuronext,  "Xeum");

            target = new Exchange(ExchangeMic.Xere);
            Assert.IsFalse(target.IsEuronext,  "Xere");

            target = new Exchange(ExchangeMic.Xert);
            Assert.IsFalse(target.IsEuronext,  "Xert");

            target = new Exchange(ExchangeMic.Xeur);
            Assert.IsFalse(target.IsEuronext,  "Xeur");

            target = new Exchange(ExchangeMic.Xfra);
            Assert.IsFalse(target.IsEuronext,  "Xfra");

            target = new Exchange(ExchangeMic.Fraa);
            Assert.IsFalse(target.IsEuronext,  "Fraa");

            target = new Exchange(ExchangeMic.Frab);
            Assert.IsFalse(target.IsEuronext,  "Frab");

            target = new Exchange(ExchangeMic.Xdbc);
            Assert.IsFalse(target.IsEuronext,  "Xdbc");

            target = new Exchange(ExchangeMic.Xdbv);
            Assert.IsFalse(target.IsEuronext,  "Xdbv");

            target = new Exchange(ExchangeMic.Xdbx);
            Assert.IsFalse(target.IsEuronext,  "Xdbx");

            target = new Exchange(ExchangeMic.Xham);
            Assert.IsFalse(target.IsEuronext,  "Xham");

            target = new Exchange(ExchangeMic.Hama);
            Assert.IsFalse(target.IsEuronext,  "Hama");

            target = new Exchange(ExchangeMic.Hamb);
            Assert.IsFalse(target.IsEuronext,  "Hamb");

            target = new Exchange(ExchangeMic.Hamm);
            Assert.IsFalse(target.IsEuronext,  "Hamm");

            target = new Exchange(ExchangeMic.Hamn);
            Assert.IsFalse(target.IsEuronext,  "Hamn");

            target = new Exchange(ExchangeMic.Haml);
            Assert.IsFalse(target.IsEuronext,  "Haml");

            target = new Exchange(ExchangeMic.Xhan);
            Assert.IsFalse(target.IsEuronext,  "Xhan");

            target = new Exchange(ExchangeMic.Hana);
            Assert.IsFalse(target.IsEuronext,  "Hana");

            target = new Exchange(ExchangeMic.Hanb);
            Assert.IsFalse(target.IsEuronext,  "Hanb");

            target = new Exchange(ExchangeMic.Xinv);
            Assert.IsFalse(target.IsEuronext,  "Xinv");

            target = new Exchange(ExchangeMic.Xmun);
            Assert.IsFalse(target.IsEuronext,  "Xmun");

            target = new Exchange(ExchangeMic.Muna);
            Assert.IsFalse(target.IsEuronext,  "Muna");

            target = new Exchange(ExchangeMic.Munb);
            Assert.IsFalse(target.IsEuronext,  "Munb");

            target = new Exchange(ExchangeMic.Mund);
            Assert.IsFalse(target.IsEuronext,  "Mund");

            target = new Exchange(ExchangeMic.Munc);
            Assert.IsFalse(target.IsEuronext,  "Munc");

            target = new Exchange(ExchangeMic.Xsco);
            Assert.IsFalse(target.IsEuronext,  "Xsco");

            target = new Exchange(ExchangeMic.Xsc1);
            Assert.IsFalse(target.IsEuronext,  "Xsc1");

            target = new Exchange(ExchangeMic.Xsc2);
            Assert.IsFalse(target.IsEuronext,  "Xsc2");

            target = new Exchange(ExchangeMic.Xsc3);
            Assert.IsFalse(target.IsEuronext,  "Xsc3");

            target = new Exchange(ExchangeMic.Xstu);
            Assert.IsFalse(target.IsEuronext,  "Xstu");

            target = new Exchange(ExchangeMic.Euwx);
            Assert.IsFalse(target.IsEuronext,  "Euwx");

            target = new Exchange(ExchangeMic.Stua);
            Assert.IsFalse(target.IsEuronext,  "Stua");

            target = new Exchange(ExchangeMic.Stub);
            Assert.IsFalse(target.IsEuronext,  "Stub");

            target = new Exchange(ExchangeMic.Xstf);
            Assert.IsFalse(target.IsEuronext,  "Xstf");

            target = new Exchange(ExchangeMic.Stuc);
            Assert.IsFalse(target.IsEuronext,  "Stuc");

            target = new Exchange(ExchangeMic.Stud);
            Assert.IsFalse(target.IsEuronext,  "Stud");

            target = new Exchange(ExchangeMic.Xxsc);
            Assert.IsFalse(target.IsEuronext,  "Xxsc");

            target = new Exchange(ExchangeMic.Xice);
            Assert.IsFalse(target.IsEuronext,  "Xice");

            target = new Exchange(ExchangeMic.Dice);
            Assert.IsFalse(target.IsEuronext,  "Dice");

            target = new Exchange(ExchangeMic.Fnis);
            Assert.IsFalse(target.IsEuronext,  "Fnis");

            target = new Exchange(ExchangeMic.Dnis);
            Assert.IsFalse(target.IsEuronext,  "Dnis");

            target = new Exchange(ExchangeMic.Mice);
            Assert.IsFalse(target.IsEuronext,  "Mice");

            target = new Exchange(ExchangeMic.Mnis);
            Assert.IsFalse(target.IsEuronext,  "Mnis");

            target = new Exchange(ExchangeMic.Cgit);
            Assert.IsFalse(target.IsEuronext,  "Cgit");

            target = new Exchange(ExchangeMic.Cggd);
            Assert.IsFalse(target.IsEuronext,  "Cggd");

            target = new Exchange(ExchangeMic.Cgcm);
            Assert.IsFalse(target.IsEuronext,  "Cgcm");

            target = new Exchange(ExchangeMic.Cgqt);
            Assert.IsFalse(target.IsEuronext,  "Cgqt");

            target = new Exchange(ExchangeMic.Cgdb);
            Assert.IsFalse(target.IsEuronext,  "Cgdb");

            target = new Exchange(ExchangeMic.Cgeb);
            Assert.IsFalse(target.IsEuronext,  "Cgeb");

            target = new Exchange(ExchangeMic.Cgtr);
            Assert.IsFalse(target.IsEuronext,  "Cgtr");

            target = new Exchange(ExchangeMic.Cgqd);
            Assert.IsFalse(target.IsEuronext,  "Cgqd");

            target = new Exchange(ExchangeMic.Cgnd);
            Assert.IsFalse(target.IsEuronext,  "Cgnd");

            target = new Exchange(ExchangeMic.Emid);
            Assert.IsFalse(target.IsEuronext,  "Emid");

            target = new Exchange(ExchangeMic.Emdr);
            Assert.IsFalse(target.IsEuronext,  "Emdr");

            target = new Exchange(ExchangeMic.Emir);
            Assert.IsFalse(target.IsEuronext,  "Emir");

            target = new Exchange(ExchangeMic.Emib);
            Assert.IsFalse(target.IsEuronext,  "Emib");

            target = new Exchange(ExchangeMic.Etlx);
            Assert.IsFalse(target.IsEuronext,  "Etlx");

            target = new Exchange(ExchangeMic.Fbsi);
            Assert.IsFalse(target.IsEuronext,  "Fbsi");

            target = new Exchange(ExchangeMic.Hmtf);
            Assert.IsFalse(target.IsEuronext,  "Hmtf");

            target = new Exchange(ExchangeMic.Hmod);
            Assert.IsFalse(target.IsEuronext,  "Hmod");

            target = new Exchange(ExchangeMic.Hrfq);
            Assert.IsFalse(target.IsEuronext,  "Hrfq");

            target = new Exchange(ExchangeMic.Mtso);
            Assert.IsFalse(target.IsEuronext,  "Mtso");

            target = new Exchange(ExchangeMic.Bond);
            Assert.IsFalse(target.IsEuronext,  "Bond");

            target = new Exchange(ExchangeMic.Mtsc);
            Assert.IsFalse(target.IsEuronext,  "Mtsc");

            target = new Exchange(ExchangeMic.Mtsm);
            Assert.IsFalse(target.IsEuronext,  "Mtsm");

            target = new Exchange(ExchangeMic.Ssob);
            Assert.IsFalse(target.IsEuronext,  "Ssob");

            target = new Exchange(ExchangeMic.Xgme);
            Assert.IsFalse(target.IsEuronext,  "Xgme");

            target = new Exchange(ExchangeMic.Xmil);
            Assert.IsFalse(target.IsEuronext,  "Xmil");

            target = new Exchange(ExchangeMic.Mtah);
            Assert.IsFalse(target.IsEuronext,  "Mtah");

            target = new Exchange(ExchangeMic.Etfp);
            Assert.IsFalse(target.IsEuronext,  "Etfp");

            target = new Exchange(ExchangeMic.Mivx);
            Assert.IsFalse(target.IsEuronext,  "Mivx");

            target = new Exchange(ExchangeMic.Motx);
            Assert.IsFalse(target.IsEuronext,  "Motx");

            target = new Exchange(ExchangeMic.Mtaa);
            Assert.IsFalse(target.IsEuronext,  "Mtaa");

            target = new Exchange(ExchangeMic.Sedx);
            Assert.IsFalse(target.IsEuronext,  "Sedx");

            target = new Exchange(ExchangeMic.Xaim);
            Assert.IsFalse(target.IsEuronext,  "Xaim");

            target = new Exchange(ExchangeMic.Xdmi);
            Assert.IsFalse(target.IsEuronext,  "Xdmi");

            target = new Exchange(ExchangeMic.Xmot);
            Assert.IsFalse(target.IsEuronext,  "Xmot");

            target = new Exchange(ExchangeMic.Cclx);
            Assert.IsFalse(target.IsEuronext,  "Cclx");

            target = new Exchange(ExchangeMic.Mibl);
            Assert.IsFalse(target.IsEuronext,  "Mibl");

            target = new Exchange(ExchangeMic.Rbcb);
            Assert.IsFalse(target.IsEuronext,  "Rbcb");

            target = new Exchange(ExchangeMic.Rbsi);
            Assert.IsFalse(target.IsEuronext,  "Rbsi");

            target = new Exchange(ExchangeMic.Xlux);
            Assert.IsFalse(target.IsEuronext,  "Xlux");

            target = new Exchange(ExchangeMic.Emtf);
            Assert.IsFalse(target.IsEuronext,  "Emtf");

            target = new Exchange(ExchangeMic.Xves);
            Assert.IsFalse(target.IsEuronext,  "Xves");

            target = new Exchange(ExchangeMic.Fish);
            Assert.IsFalse(target.IsEuronext,  "Fish");

            target = new Exchange(ExchangeMic.Fshx);
            Assert.IsFalse(target.IsEuronext,  "Fshx");

            target = new Exchange(ExchangeMic.Icas);
            Assert.IsFalse(target.IsEuronext,  "Icas");

            target = new Exchange(ExchangeMic.Nexo);
            Assert.IsFalse(target.IsEuronext,  "Nexo");

            target = new Exchange(ExchangeMic.Nops);
            Assert.IsFalse(target.IsEuronext,  "Nops");

            target = new Exchange(ExchangeMic.Norx);
            Assert.IsFalse(target.IsEuronext,  "Norx");

            target = new Exchange(ExchangeMic.Eleu);
            Assert.IsFalse(target.IsEuronext,  "Eleu");

            target = new Exchange(ExchangeMic.Else);
            Assert.IsFalse(target.IsEuronext,  "Else");

            target = new Exchange(ExchangeMic.Elno);
            Assert.IsFalse(target.IsEuronext,  "Elno");

            target = new Exchange(ExchangeMic.Eluk);
            Assert.IsFalse(target.IsEuronext,  "Eluk");

            target = new Exchange(ExchangeMic.Frei);
            Assert.IsFalse(target.IsEuronext,  "Frei");

            target = new Exchange(ExchangeMic.Bulk);
            Assert.IsFalse(target.IsEuronext,  "Bulk");

            target = new Exchange(ExchangeMic.Stee);
            Assert.IsFalse(target.IsEuronext,  "Stee");

            target = new Exchange(ExchangeMic.Nosc);
            Assert.IsFalse(target.IsEuronext,  "Nosc");

            target = new Exchange(ExchangeMic.Notc);
            Assert.IsFalse(target.IsEuronext,  "Notc");

            target = new Exchange(ExchangeMic.Oslc);
            Assert.IsFalse(target.IsEuronext,  "Oslc");

            target = new Exchange(ExchangeMic.Xdnb);
            Assert.IsFalse(target.IsEuronext,  "Xdnb");

            target = new Exchange(ExchangeMic.Xima);
            Assert.IsFalse(target.IsEuronext,  "Xima");

            target = new Exchange(ExchangeMic.Xosl);
            Assert.IsFalse(target.IsEuronext,  "Xosl");

            target = new Exchange(ExchangeMic.Xoam);
            Assert.IsFalse(target.IsEuronext,  "Xoam");

            target = new Exchange(ExchangeMic.Xoas);
            Assert.IsFalse(target.IsEuronext,  "Xoas");

            target = new Exchange(ExchangeMic.Nibr);
            Assert.IsFalse(target.IsEuronext,  "Nibr");

            target = new Exchange(ExchangeMic.Merd);
            Assert.IsFalse(target.IsEuronext,  "Merd");

            target = new Exchange(ExchangeMic.Merk);
            Assert.IsFalse(target.IsEuronext,  "Merk");

            target = new Exchange(ExchangeMic.Xosc);
            Assert.IsFalse(target.IsEuronext,  "Xosc");

            target = new Exchange(ExchangeMic.Xoad);
            Assert.IsFalse(target.IsEuronext,  "Xoad");

            target = new Exchange(ExchangeMic.Xosd);
            Assert.IsFalse(target.IsEuronext,  "Xosd");

            target = new Exchange(ExchangeMic.Omic);
            Assert.IsFalse(target.IsEuronext,  "Omic");

            target = new Exchange(ExchangeMic.Opex);
            Assert.IsFalse(target.IsEuronext,  "Opex");

            target = new Exchange(ExchangeMic.Omip);
            Assert.IsFalse(target.IsEuronext,  "Omip");

            target = new Exchange(ExchangeMic.Bmex);
            Assert.IsFalse(target.IsEuronext,  "Bmex");

            target = new Exchange(ExchangeMic.Mabx);
            Assert.IsFalse(target.IsEuronext,  "Mabx");

            target = new Exchange(ExchangeMic.Send);
            Assert.IsFalse(target.IsEuronext,  "Send");

            target = new Exchange(ExchangeMic.Xbar);
            Assert.IsFalse(target.IsEuronext,  "Xbar");

            target = new Exchange(ExchangeMic.Xbil);
            Assert.IsFalse(target.IsEuronext,  "Xbil");

            target = new Exchange(ExchangeMic.Xdrf);
            Assert.IsFalse(target.IsEuronext,  "Xdrf");

            target = new Exchange(ExchangeMic.Xlat);
            Assert.IsFalse(target.IsEuronext,  "Xlat");

            target = new Exchange(ExchangeMic.Xmad);
            Assert.IsFalse(target.IsEuronext,  "Xmad");

            target = new Exchange(ExchangeMic.Xmce);
            Assert.IsFalse(target.IsEuronext,  "Xmce");

            target = new Exchange(ExchangeMic.Xmrv);
            Assert.IsFalse(target.IsEuronext,  "Xmrv");

            target = new Exchange(ExchangeMic.Xval);
            Assert.IsFalse(target.IsEuronext,  "Xval");

            target = new Exchange(ExchangeMic.Merf);
            Assert.IsFalse(target.IsEuronext,  "Merf");

            target = new Exchange(ExchangeMic.Xmpw);
            Assert.IsFalse(target.IsEuronext,  "Xmpw");

            target = new Exchange(ExchangeMic.Marf);
            Assert.IsFalse(target.IsEuronext,  "Marf");

            target = new Exchange(ExchangeMic.Bmcl);
            Assert.IsFalse(target.IsEuronext,  "Bmcl");

            target = new Exchange(ExchangeMic.Sbar);
            Assert.IsFalse(target.IsEuronext,  "Sbar");

            target = new Exchange(ExchangeMic.Sbil);
            Assert.IsFalse(target.IsEuronext,  "Sbil");

            target = new Exchange(ExchangeMic.Bmea);
            Assert.IsFalse(target.IsEuronext,  "Bmea");

            target = new Exchange(ExchangeMic.Ibgh);
            Assert.IsFalse(target.IsEuronext,  "Ibgh");

            target = new Exchange(ExchangeMic.Mibg);
            Assert.IsFalse(target.IsEuronext,  "Mibg");

            target = new Exchange(ExchangeMic.Omel);
            Assert.IsFalse(target.IsEuronext,  "Omel");

            target = new Exchange(ExchangeMic.Pave);
            Assert.IsFalse(target.IsEuronext,  "Pave");

            target = new Exchange(ExchangeMic.Xdpa);
            Assert.IsFalse(target.IsEuronext,  "Xdpa");

            target = new Exchange(ExchangeMic.Xnaf);
            Assert.IsFalse(target.IsEuronext,  "Xnaf");

            target = new Exchange(ExchangeMic.Cryd);
            Assert.IsFalse(target.IsEuronext,  "Cryd");

            target = new Exchange(ExchangeMic.Cryx);
            Assert.IsFalse(target.IsEuronext,  "Cryx");

            target = new Exchange(ExchangeMic.Napa);
            Assert.IsFalse(target.IsEuronext,  "Napa");

            target = new Exchange(ExchangeMic.Sebx);
            Assert.IsFalse(target.IsEuronext,  "Sebx");

            target = new Exchange(ExchangeMic.Ensx);
            Assert.IsFalse(target.IsEuronext,  "Ensx");

            target = new Exchange(ExchangeMic.Sebs);
            Assert.IsFalse(target.IsEuronext,  "Sebs");

            target = new Exchange(ExchangeMic.Xngm);
            Assert.IsFalse(target.IsEuronext,  "Xngm");

            target = new Exchange(ExchangeMic.Nmtf);
            Assert.IsFalse(target.IsEuronext,  "Nmtf");

            target = new Exchange(ExchangeMic.Xndx);
            Assert.IsFalse(target.IsEuronext,  "Xndx");

            target = new Exchange(ExchangeMic.Xnmr);
            Assert.IsFalse(target.IsEuronext,  "Xnmr");

            target = new Exchange(ExchangeMic.Xsat);
            Assert.IsFalse(target.IsEuronext,  "Xsat");

            target = new Exchange(ExchangeMic.Xsto);
            Assert.IsFalse(target.IsEuronext,  "Xsto");

            target = new Exchange(ExchangeMic.Fnse);
            Assert.IsFalse(target.IsEuronext,  "Fnse");

            target = new Exchange(ExchangeMic.Xopv);
            Assert.IsFalse(target.IsEuronext,  "Xopv");

            target = new Exchange(ExchangeMic.Csto);
            Assert.IsFalse(target.IsEuronext,  "Csto");

            target = new Exchange(ExchangeMic.Dsto);
            Assert.IsFalse(target.IsEuronext,  "Dsto");

            target = new Exchange(ExchangeMic.Dnse);
            Assert.IsFalse(target.IsEuronext,  "Dnse");

            target = new Exchange(ExchangeMic.Msto);
            Assert.IsFalse(target.IsEuronext,  "Msto");

            target = new Exchange(ExchangeMic.Mnse);
            Assert.IsFalse(target.IsEuronext,  "Mnse");

            target = new Exchange(ExchangeMic.Dked);
            Assert.IsFalse(target.IsEuronext,  "Dked");

            target = new Exchange(ExchangeMic.Fied);
            Assert.IsFalse(target.IsEuronext,  "Fied");

            target = new Exchange(ExchangeMic.Noed);
            Assert.IsFalse(target.IsEuronext,  "Noed");

            target = new Exchange(ExchangeMic.Seed);
            Assert.IsFalse(target.IsEuronext,  "Seed");

            target = new Exchange(ExchangeMic.Pned);
            Assert.IsFalse(target.IsEuronext,  "Pned");

            target = new Exchange(ExchangeMic.Euwb);
            Assert.IsFalse(target.IsEuronext,  "Euwb");

            target = new Exchange(ExchangeMic.Uswb);
            Assert.IsFalse(target.IsEuronext,  "Uswb");

            target = new Exchange(ExchangeMic.Dkfi);
            Assert.IsFalse(target.IsEuronext,  "Dkfi");

            target = new Exchange(ExchangeMic.Nofi);
            Assert.IsFalse(target.IsEuronext,  "Nofi");

            target = new Exchange(ExchangeMic.Ebon);
            Assert.IsFalse(target.IsEuronext,  "Ebon");

            target = new Exchange(ExchangeMic.Onse);
            Assert.IsFalse(target.IsEuronext,  "Onse");

            target = new Exchange(ExchangeMic.Esto);
            Assert.IsFalse(target.IsEuronext,  "Esto");

            target = new Exchange(ExchangeMic.Aixe);
            Assert.IsFalse(target.IsEuronext,  "Aixe");

            target = new Exchange(ExchangeMic.Dots);
            Assert.IsFalse(target.IsEuronext,  "Dots");

            target = new Exchange(ExchangeMic.Ebss);
            Assert.IsFalse(target.IsEuronext,  "Ebss");

            target = new Exchange(ExchangeMic.Ebsc);
            Assert.IsFalse(target.IsEuronext,  "Ebsc");

            target = new Exchange(ExchangeMic.Euch);
            Assert.IsFalse(target.IsEuronext,  "Euch");

            target = new Exchange(ExchangeMic.Eusp);
            Assert.IsFalse(target.IsEuronext,  "Eusp");

            target = new Exchange(ExchangeMic.Eurm);
            Assert.IsFalse(target.IsEuronext,  "Eurm");

            target = new Exchange(ExchangeMic.Eusc);
            Assert.IsFalse(target.IsEuronext,  "Eusc");

            target = new Exchange(ExchangeMic.S3fm);
            Assert.IsFalse(target.IsEuronext,  "S3fm");

            target = new Exchange(ExchangeMic.Stox);
            Assert.IsFalse(target.IsEuronext,  "Stox");

            target = new Exchange(ExchangeMic.Xscu);
            Assert.IsFalse(target.IsEuronext,  "Xscu");

            target = new Exchange(ExchangeMic.Xstv);
            Assert.IsFalse(target.IsEuronext,  "Xstv");

            target = new Exchange(ExchangeMic.Xstx);
            Assert.IsFalse(target.IsEuronext,  "Xstx");

            target = new Exchange(ExchangeMic.Ubsg);
            Assert.IsFalse(target.IsEuronext,  "Ubsg");

            target = new Exchange(ExchangeMic.Ubsf);
            Assert.IsFalse(target.IsEuronext,  "Ubsf");

            target = new Exchange(ExchangeMic.Ubsc);
            Assert.IsFalse(target.IsEuronext,  "Ubsc");

            target = new Exchange(ExchangeMic.Vlex);
            Assert.IsFalse(target.IsEuronext,  "Vlex");

            target = new Exchange(ExchangeMic.Xbrn);
            Assert.IsFalse(target.IsEuronext,  "Xbrn");

            target = new Exchange(ExchangeMic.Xswx);
            Assert.IsFalse(target.IsEuronext,  "Xswx");

            target = new Exchange(ExchangeMic.Xqmh);
            Assert.IsFalse(target.IsEuronext,  "Xqmh");

            target = new Exchange(ExchangeMic.Xvtx);
            Assert.IsFalse(target.IsEuronext,  "Xvtx");

            target = new Exchange(ExchangeMic.Xbtr);
            Assert.IsFalse(target.IsEuronext,  "Xbtr");

            target = new Exchange(ExchangeMic.Xswm);
            Assert.IsFalse(target.IsEuronext,  "Xswm");

            target = new Exchange(ExchangeMic.Xsls);
            Assert.IsFalse(target.IsEuronext,  "Xsls");

            target = new Exchange(ExchangeMic.Xicb);
            Assert.IsFalse(target.IsEuronext,  "Xicb");

            target = new Exchange(ExchangeMic.Zkbx);
            Assert.IsFalse(target.IsEuronext,  "Zkbx");

            target = new Exchange(ExchangeMic.Kmux);
            Assert.IsFalse(target.IsEuronext,  "Kmux");

            target = new Exchange(ExchangeMic.Clmx);
            Assert.IsFalse(target.IsEuronext,  "Clmx");

            target = new Exchange(ExchangeMic.Hchc);
            Assert.IsFalse(target.IsEuronext,  "Hchc");

            target = new Exchange(ExchangeMic.Ndex);
            Assert.IsFalse(target.IsEuronext,  "Ndex");

            target = new Exchange(ExchangeMic.Imco);
            Assert.IsFalse(target.IsEuronext,  "Imco");

            target = new Exchange(ExchangeMic.Imeq);
            Assert.IsFalse(target.IsEuronext,  "Imeq");

            target = new Exchange(ExchangeMic.Ndxs);
            Assert.IsFalse(target.IsEuronext,  "Ndxs");

            target = new Exchange(ExchangeMic.Nlpx);
            Assert.IsFalse(target.IsEuronext,  "Nlpx");

            target = new Exchange(ExchangeMic.Xems);
            Assert.IsFalse(target.IsEuronext,  "Xems");

            target = new Exchange(ExchangeMic.Xnxc);
            Assert.IsFalse(target.IsEuronext,  "Xnxc");

            target = new Exchange(ExchangeMic.X3579);
            Assert.IsFalse(target.IsEuronext,  "X3579");

            target = new Exchange(ExchangeMic.Afdl);
            Assert.IsFalse(target.IsEuronext,  "Afdl");

            target = new Exchange(ExchangeMic.Ampx);
            Assert.IsFalse(target.IsEuronext,  "Ampx");

            target = new Exchange(ExchangeMic.Anzl);
            Assert.IsFalse(target.IsEuronext,  "Anzl");

            target = new Exchange(ExchangeMic.Aqxe);
            Assert.IsFalse(target.IsEuronext,  "Aqxe");

            target = new Exchange(ExchangeMic.Arax);
            Assert.IsFalse(target.IsEuronext,  "Arax");

            target = new Exchange(ExchangeMic.Atlb);
            Assert.IsFalse(target.IsEuronext,  "Atlb");

            target = new Exchange(ExchangeMic.Autx);
            Assert.IsFalse(target.IsEuronext,  "Autx");

            target = new Exchange(ExchangeMic.Autp);
            Assert.IsFalse(target.IsEuronext,  "Autp");

            target = new Exchange(ExchangeMic.Autb);
            Assert.IsFalse(target.IsEuronext,  "Autb");

            target = new Exchange(ExchangeMic.Balt);
            Assert.IsFalse(target.IsEuronext,  "Balt");

            target = new Exchange(ExchangeMic.Bltx);
            Assert.IsFalse(target.IsEuronext,  "Bltx");

            target = new Exchange(ExchangeMic.Bapa);
            Assert.IsFalse(target.IsEuronext,  "Bapa");

            target = new Exchange(ExchangeMic.Bcrm);
            Assert.IsFalse(target.IsEuronext,  "Bcrm");

            target = new Exchange(ExchangeMic.Baro);
            Assert.IsFalse(target.IsEuronext,  "Baro");

            target = new Exchange(ExchangeMic.Bark);
            Assert.IsFalse(target.IsEuronext,  "Bark");

            target = new Exchange(ExchangeMic.Bart);
            Assert.IsFalse(target.IsEuronext,  "Bart");

            target = new Exchange(ExchangeMic.Bcxe);
            Assert.IsFalse(target.IsEuronext,  "Bcxe");

            target = new Exchange(ExchangeMic.Bate);
            Assert.IsFalse(target.IsEuronext,  "Bate");

            target = new Exchange(ExchangeMic.Chix);
            Assert.IsFalse(target.IsEuronext,  "Chix");

            target = new Exchange(ExchangeMic.Batd);
            Assert.IsFalse(target.IsEuronext,  "Batd");

            target = new Exchange(ExchangeMic.Chid);
            Assert.IsFalse(target.IsEuronext,  "Chid");

            target = new Exchange(ExchangeMic.Batf);
            Assert.IsFalse(target.IsEuronext,  "Batf");

            target = new Exchange(ExchangeMic.Chio);
            Assert.IsFalse(target.IsEuronext,  "Chio");

            target = new Exchange(ExchangeMic.Batp);
            Assert.IsFalse(target.IsEuronext,  "Batp");

            target = new Exchange(ExchangeMic.Botc);
            Assert.IsFalse(target.IsEuronext,  "Botc");

            target = new Exchange(ExchangeMic.Lisx);
            Assert.IsFalse(target.IsEuronext,  "Lisx");

            target = new Exchange(ExchangeMic.Bgci);
            Assert.IsFalse(target.IsEuronext,  "Bgci");

            target = new Exchange(ExchangeMic.Bgcb);
            Assert.IsFalse(target.IsEuronext,  "Bgcb");

            target = new Exchange(ExchangeMic.Bkln);
            Assert.IsFalse(target.IsEuronext,  "Bkln");

            target = new Exchange(ExchangeMic.Bklf);
            Assert.IsFalse(target.IsEuronext,  "Bklf");

            target = new Exchange(ExchangeMic.Blox);
            Assert.IsFalse(target.IsEuronext,  "Blox");

            target = new Exchange(ExchangeMic.Bmtf);
            Assert.IsFalse(target.IsEuronext,  "Bmtf");

            target = new Exchange(ExchangeMic.Boat);
            Assert.IsFalse(target.IsEuronext,  "Boat");

            target = new Exchange(ExchangeMic.Bosc);
            Assert.IsFalse(target.IsEuronext,  "Bosc");

            target = new Exchange(ExchangeMic.Brnx);
            Assert.IsFalse(target.IsEuronext,  "Brnx");

            target = new Exchange(ExchangeMic.Btee);
            Assert.IsFalse(target.IsEuronext,  "Btee");

            target = new Exchange(ExchangeMic.Ebsm);
            Assert.IsFalse(target.IsEuronext,  "Ebsm");

            target = new Exchange(ExchangeMic.Ebsd);
            Assert.IsFalse(target.IsEuronext,  "Ebsd");

            target = new Exchange(ExchangeMic.Ebsi);
            Assert.IsFalse(target.IsEuronext,  "Ebsi");

            target = new Exchange(ExchangeMic.Nexy);
            Assert.IsFalse(target.IsEuronext,  "Nexy");

            target = new Exchange(ExchangeMic.Ccml);
            Assert.IsFalse(target.IsEuronext,  "Ccml");

            target = new Exchange(ExchangeMic.Cco2);
            Assert.IsFalse(target.IsEuronext,  "Cco2");

            target = new Exchange(ExchangeMic.Cgme);
            Assert.IsFalse(target.IsEuronext,  "Cgme");

            target = new Exchange(ExchangeMic.Chev);
            Assert.IsFalse(target.IsEuronext,  "Chev");

            target = new Exchange(ExchangeMic.Blnk);
            Assert.IsFalse(target.IsEuronext,  "Blnk");

            target = new Exchange(ExchangeMic.Cmee);
            Assert.IsFalse(target.IsEuronext,  "Cmee");

            target = new Exchange(ExchangeMic.Cmec);
            Assert.IsFalse(target.IsEuronext,  "Cmec");

            target = new Exchange(ExchangeMic.Cmed);
            Assert.IsFalse(target.IsEuronext,  "Cmed");

            target = new Exchange(ExchangeMic.Cmmt);
            Assert.IsFalse(target.IsEuronext,  "Cmmt");

            target = new Exchange(ExchangeMic.Cryp);
            Assert.IsFalse(target.IsEuronext,  "Cryp");

            target = new Exchange(ExchangeMic.Cseu);
            Assert.IsFalse(target.IsEuronext,  "Cseu");

            target = new Exchange(ExchangeMic.Cscf);
            Assert.IsFalse(target.IsEuronext,  "Cscf");

            target = new Exchange(ExchangeMic.Csbx);
            Assert.IsFalse(target.IsEuronext,  "Csbx");

            target = new Exchange(ExchangeMic.Sics);
            Assert.IsFalse(target.IsEuronext,  "Sics");

            target = new Exchange(ExchangeMic.Csin);
            Assert.IsFalse(target.IsEuronext,  "Csin");

            target = new Exchange(ExchangeMic.Cssi);
            Assert.IsFalse(target.IsEuronext,  "Cssi");

            target = new Exchange(ExchangeMic.Dbes);
            Assert.IsFalse(target.IsEuronext,  "Dbes");

            target = new Exchange(ExchangeMic.Dbix);
            Assert.IsFalse(target.IsEuronext,  "Dbix");

            target = new Exchange(ExchangeMic.Dbdc);
            Assert.IsFalse(target.IsEuronext,  "Dbdc");

            target = new Exchange(ExchangeMic.Dbcx);
            Assert.IsFalse(target.IsEuronext,  "Dbcx");

            target = new Exchange(ExchangeMic.Dbcr);
            Assert.IsFalse(target.IsEuronext,  "Dbcr");

            target = new Exchange(ExchangeMic.Dbmo);
            Assert.IsFalse(target.IsEuronext,  "Dbmo");

            target = new Exchange(ExchangeMic.Dbse);
            Assert.IsFalse(target.IsEuronext,  "Dbse");

            target = new Exchange(ExchangeMic.Dowg);
            Assert.IsFalse(target.IsEuronext,  "Dowg");

            target = new Exchange(ExchangeMic.Echo);
            Assert.IsFalse(target.IsEuronext,  "Echo");

            target = new Exchange(ExchangeMic.Embx);
            Assert.IsFalse(target.IsEuronext,  "Embx");

            target = new Exchange(ExchangeMic.Encl);
            Assert.IsFalse(target.IsEuronext,  "Encl");

            target = new Exchange(ExchangeMic.Eqld);
            Assert.IsFalse(target.IsEuronext,  "Eqld");

            target = new Exchange(ExchangeMic.Exeu);
            Assert.IsFalse(target.IsEuronext,  "Exeu");

            target = new Exchange(ExchangeMic.Exmp);
            Assert.IsFalse(target.IsEuronext,  "Exmp");

            target = new Exchange(ExchangeMic.Exor);
            Assert.IsFalse(target.IsEuronext,  "Exor");

            target = new Exchange(ExchangeMic.Exvp);
            Assert.IsFalse(target.IsEuronext,  "Exvp");

            target = new Exchange(ExchangeMic.Exbo);
            Assert.IsFalse(target.IsEuronext,  "Exbo");

            target = new Exchange(ExchangeMic.Exlp);
            Assert.IsFalse(target.IsEuronext,  "Exlp");

            target = new Exchange(ExchangeMic.Exdc);
            Assert.IsFalse(target.IsEuronext,  "Exdc");

            target = new Exchange(ExchangeMic.Exsi);
            Assert.IsFalse(target.IsEuronext,  "Exsi");

            target = new Exchange(ExchangeMic.Excp);
            Assert.IsFalse(target.IsEuronext,  "Excp");

            target = new Exchange(ExchangeMic.Exot);
            Assert.IsFalse(target.IsEuronext,  "Exot");

            target = new Exchange(ExchangeMic.Fair);
            Assert.IsFalse(target.IsEuronext,  "Fair");

            target = new Exchange(ExchangeMic.Fisu);
            Assert.IsFalse(target.IsEuronext,  "Fisu");

            target = new Exchange(ExchangeMic.Fxgb);
            Assert.IsFalse(target.IsEuronext,  "Fxgb");

            target = new Exchange(ExchangeMic.Gemx);
            Assert.IsFalse(target.IsEuronext,  "Gemx");

            target = new Exchange(ExchangeMic.Gfic);
            Assert.IsFalse(target.IsEuronext,  "Gfic");

            target = new Exchange(ExchangeMic.Gfif);
            Assert.IsFalse(target.IsEuronext,  "Gfif");

            target = new Exchange(ExchangeMic.Gfin);
            Assert.IsFalse(target.IsEuronext,  "Gfin");

            target = new Exchange(ExchangeMic.Gfir);
            Assert.IsFalse(target.IsEuronext,  "Gfir");

            target = new Exchange(ExchangeMic.Gmeg);
            Assert.IsFalse(target.IsEuronext,  "Gmeg");

            target = new Exchange(ExchangeMic.Xldx);
            Assert.IsFalse(target.IsEuronext,  "Xldx");

            target = new Exchange(ExchangeMic.Xgdx);
            Assert.IsFalse(target.IsEuronext,  "Xgdx");

            target = new Exchange(ExchangeMic.Xgsx);
            Assert.IsFalse(target.IsEuronext,  "Xgsx");

            target = new Exchange(ExchangeMic.Xgcx);
            Assert.IsFalse(target.IsEuronext,  "Xgcx");

            target = new Exchange(ExchangeMic.Grif);
            Assert.IsFalse(target.IsEuronext,  "Grif");

            target = new Exchange(ExchangeMic.Grio);
            Assert.IsFalse(target.IsEuronext,  "Grio");

            target = new Exchange(ExchangeMic.Grse);
            Assert.IsFalse(target.IsEuronext,  "Grse");

            target = new Exchange(ExchangeMic.Gsib);
            Assert.IsFalse(target.IsEuronext,  "Gsib");

            target = new Exchange(ExchangeMic.Bisi);
            Assert.IsFalse(target.IsEuronext,  "Bisi");

            target = new Exchange(ExchangeMic.Gsil);
            Assert.IsFalse(target.IsEuronext,  "Gsil");

            target = new Exchange(ExchangeMic.Gssi);
            Assert.IsFalse(target.IsEuronext,  "Gssi");

            target = new Exchange(ExchangeMic.Gsbx);
            Assert.IsFalse(target.IsEuronext,  "Gsbx");

            target = new Exchange(ExchangeMic.Hpcs);
            Assert.IsFalse(target.IsEuronext,  "Hpcs");

            target = new Exchange(ExchangeMic.Hsbc);
            Assert.IsFalse(target.IsEuronext,  "Hsbc");

            target = new Exchange(ExchangeMic.Ibal);
            Assert.IsFalse(target.IsEuronext,  "Ibal");

            target = new Exchange(ExchangeMic.Icap);
            Assert.IsFalse(target.IsEuronext,  "Icap");

            target = new Exchange(ExchangeMic.Icah);
            Assert.IsFalse(target.IsEuronext,  "Icah");

            target = new Exchange(ExchangeMic.Icen);
            Assert.IsFalse(target.IsEuronext,  "Icen");

            target = new Exchange(ExchangeMic.Icse);
            Assert.IsFalse(target.IsEuronext,  "Icse");

            target = new Exchange(ExchangeMic.Ictq);
            Assert.IsFalse(target.IsEuronext,  "Ictq");

            target = new Exchange(ExchangeMic.Wclk);
            Assert.IsFalse(target.IsEuronext,  "Wclk");

            target = new Exchange(ExchangeMic.Igdl);
            Assert.IsFalse(target.IsEuronext,  "Igdl");

            target = new Exchange(ExchangeMic.Ifeu);
            Assert.IsFalse(target.IsEuronext,  "Ifeu");

            target = new Exchange(ExchangeMic.Cxrt);
            Assert.IsFalse(target.IsEuronext,  "Cxrt");

            target = new Exchange(ExchangeMic.Iflo);
            Assert.IsFalse(target.IsEuronext,  "Iflo");

            target = new Exchange(ExchangeMic.Ifll);
            Assert.IsFalse(target.IsEuronext,  "Ifll");

            target = new Exchange(ExchangeMic.Ifut);
            Assert.IsFalse(target.IsEuronext,  "Ifut");

            target = new Exchange(ExchangeMic.Iflx);
            Assert.IsFalse(target.IsEuronext,  "Iflx");

            target = new Exchange(ExchangeMic.Ifen);
            Assert.IsFalse(target.IsEuronext,  "Ifen");

            target = new Exchange(ExchangeMic.Cxot);
            Assert.IsFalse(target.IsEuronext,  "Cxot");

            target = new Exchange(ExchangeMic.Ifls);
            Assert.IsFalse(target.IsEuronext,  "Ifls");

            target = new Exchange(ExchangeMic.Inve);
            Assert.IsFalse(target.IsEuronext,  "Inve");

            target = new Exchange(ExchangeMic.Iswa);
            Assert.IsFalse(target.IsEuronext,  "Iswa");

            target = new Exchange(ExchangeMic.Jpcb);
            Assert.IsFalse(target.IsEuronext,  "Jpcb");

            target = new Exchange(ExchangeMic.Jpsi);
            Assert.IsFalse(target.IsEuronext,  "Jpsi");

            target = new Exchange(ExchangeMic.Jssi);
            Assert.IsFalse(target.IsEuronext,  "Jssi");

            target = new Exchange(ExchangeMic.Kleu);
            Assert.IsFalse(target.IsEuronext,  "Kleu");

            target = new Exchange(ExchangeMic.Lcur);
            Assert.IsFalse(target.IsEuronext,  "Lcur");

            target = new Exchange(ExchangeMic.Liqu);
            Assert.IsFalse(target.IsEuronext,  "Liqu");

            target = new Exchange(ExchangeMic.Liqh);
            Assert.IsFalse(target.IsEuronext,  "Liqh");

            target = new Exchange(ExchangeMic.Liqf);
            Assert.IsFalse(target.IsEuronext,  "Liqf");

            target = new Exchange(ExchangeMic.Lmax);
            Assert.IsFalse(target.IsEuronext,  "Lmax");

            target = new Exchange(ExchangeMic.Lmad);
            Assert.IsFalse(target.IsEuronext,  "Lmad");

            target = new Exchange(ExchangeMic.Lmae);
            Assert.IsFalse(target.IsEuronext,  "Lmae");

            target = new Exchange(ExchangeMic.Lmaf);
            Assert.IsFalse(target.IsEuronext,  "Lmaf");

            target = new Exchange(ExchangeMic.Lmao);
            Assert.IsFalse(target.IsEuronext,  "Lmao");

            target = new Exchange(ExchangeMic.Lmec);
            Assert.IsFalse(target.IsEuronext,  "Lmec");

            target = new Exchange(ExchangeMic.Lotc);
            Assert.IsFalse(target.IsEuronext,  "Lotc");

            target = new Exchange(ExchangeMic.Pldx);
            Assert.IsFalse(target.IsEuronext,  "Pldx");

            target = new Exchange(ExchangeMic.Lppm);
            Assert.IsFalse(target.IsEuronext,  "Lppm");

            target = new Exchange(ExchangeMic.Mael);
            Assert.IsFalse(target.IsEuronext,  "Mael");

            target = new Exchange(ExchangeMic.Mcur);
            Assert.IsFalse(target.IsEuronext,  "Mcur");

            target = new Exchange(ExchangeMic.Mcxs);
            Assert.IsFalse(target.IsEuronext,  "Mcxs");

            target = new Exchange(ExchangeMic.Mcxr);
            Assert.IsFalse(target.IsEuronext,  "Mcxr");

            target = new Exchange(ExchangeMic.Mfgl);
            Assert.IsFalse(target.IsEuronext,  "Mfgl");

            target = new Exchange(ExchangeMic.Mfxc);
            Assert.IsFalse(target.IsEuronext,  "Mfxc");

            target = new Exchange(ExchangeMic.Mfxa);
            Assert.IsFalse(target.IsEuronext,  "Mfxa");

            target = new Exchange(ExchangeMic.Mfxr);
            Assert.IsFalse(target.IsEuronext,  "Mfxr");

            target = new Exchange(ExchangeMic.Mhip);
            Assert.IsFalse(target.IsEuronext,  "Mhip");

            target = new Exchange(ExchangeMic.Mlxn);
            Assert.IsFalse(target.IsEuronext,  "Mlxn");

            target = new Exchange(ExchangeMic.Mlax);
            Assert.IsFalse(target.IsEuronext,  "Mlax");

            target = new Exchange(ExchangeMic.Mleu);
            Assert.IsFalse(target.IsEuronext,  "Mleu");

            target = new Exchange(ExchangeMic.Mlve);
            Assert.IsFalse(target.IsEuronext,  "Mlve");

            target = new Exchange(ExchangeMic.Msip);
            Assert.IsFalse(target.IsEuronext,  "Msip");

            target = new Exchange(ExchangeMic.Mssi);
            Assert.IsFalse(target.IsEuronext,  "Mssi");

            target = new Exchange(ExchangeMic.Mufp);
            Assert.IsFalse(target.IsEuronext,  "Mufp");

            target = new Exchange(ExchangeMic.Muti);
            Assert.IsFalse(target.IsEuronext,  "Muti");

            target = new Exchange(ExchangeMic.Mytr);
            Assert.IsFalse(target.IsEuronext,  "Mytr");

            target = new Exchange(ExchangeMic.N2Ex);
            Assert.IsFalse(target.IsEuronext,  "N2Ex");

            target = new Exchange(ExchangeMic.Ndcm);
            Assert.IsFalse(target.IsEuronext,  "Ndcm");

            target = new Exchange(ExchangeMic.Nexs);
            Assert.IsFalse(target.IsEuronext,  "Nexs");

            target = new Exchange(ExchangeMic.Nexx);
            Assert.IsFalse(target.IsEuronext,  "Nexx");

            target = new Exchange(ExchangeMic.Nexf);
            Assert.IsFalse(target.IsEuronext,  "Nexf");

            target = new Exchange(ExchangeMic.Nexg);
            Assert.IsFalse(target.IsEuronext,  "Nexg");

            target = new Exchange(ExchangeMic.Next);
            Assert.IsFalse(target.IsEuronext,  "Next");

            target = new Exchange(ExchangeMic.Nexn);
            Assert.IsFalse(target.IsEuronext,  "Nexn");

            target = new Exchange(ExchangeMic.Nexd);
            Assert.IsFalse(target.IsEuronext,  "Nexd");

            target = new Exchange(ExchangeMic.Nexl);
            Assert.IsFalse(target.IsEuronext,  "Nexl");

            target = new Exchange(ExchangeMic.Noff);
            Assert.IsFalse(target.IsEuronext,  "Noff");

            target = new Exchange(ExchangeMic.Nosi);
            Assert.IsFalse(target.IsEuronext,  "Nosi");

            target = new Exchange(ExchangeMic.Nuro);
            Assert.IsFalse(target.IsEuronext,  "Nuro");

            target = new Exchange(ExchangeMic.Xnlx);
            Assert.IsFalse(target.IsEuronext,  "Xnlx");

            target = new Exchange(ExchangeMic.Nurd);
            Assert.IsFalse(target.IsEuronext,  "Nurd");

            target = new Exchange(ExchangeMic.Nxeu);
            Assert.IsFalse(target.IsEuronext,  "Nxeu");

            target = new Exchange(ExchangeMic.Otce);
            Assert.IsFalse(target.IsEuronext,  "Otce");

            target = new Exchange(ExchangeMic.Peel);
            Assert.IsFalse(target.IsEuronext,  "Peel");

            target = new Exchange(ExchangeMic.Xrsp);
            Assert.IsFalse(target.IsEuronext,  "Xrsp");

            target = new Exchange(ExchangeMic.Xphx);
            Assert.IsFalse(target.IsEuronext,  "Xphx");

            target = new Exchange(ExchangeMic.Pieu);
            Assert.IsFalse(target.IsEuronext,  "Pieu");

            target = new Exchange(ExchangeMic.Pirm);
            Assert.IsFalse(target.IsEuronext,  "Pirm");

            target = new Exchange(ExchangeMic.Ppex);
            Assert.IsFalse(target.IsEuronext,  "Ppex");

            target = new Exchange(ExchangeMic.Qwix);
            Assert.IsFalse(target.IsEuronext,  "Qwix");

            target = new Exchange(ExchangeMic.Rbce);
            Assert.IsFalse(target.IsEuronext,  "Rbce");

            target = new Exchange(ExchangeMic.Rbct);
            Assert.IsFalse(target.IsEuronext,  "Rbct");

            target = new Exchange(ExchangeMic.Rtsi);
            Assert.IsFalse(target.IsEuronext,  "Rtsi");

            target = new Exchange(ExchangeMic.Rbsx);
            Assert.IsFalse(target.IsEuronext,  "Rbsx");

            target = new Exchange(ExchangeMic.Rtsl);
            Assert.IsFalse(target.IsEuronext,  "Rtsl");

            target = new Exchange(ExchangeMic.Trfw);
            Assert.IsFalse(target.IsEuronext,  "Trfw");

            target = new Exchange(ExchangeMic.Tral);
            Assert.IsFalse(target.IsEuronext,  "Tral");

            target = new Exchange(ExchangeMic.Secf);
            Assert.IsFalse(target.IsEuronext,  "Secf");

            target = new Exchange(ExchangeMic.Sedr);
            Assert.IsFalse(target.IsEuronext,  "Sedr");

            target = new Exchange(ExchangeMic.Sgmx);
            Assert.IsFalse(target.IsEuronext,  "Sgmx");

            target = new Exchange(ExchangeMic.Sgmy);
            Assert.IsFalse(target.IsEuronext,  "Sgmy");

            target = new Exchange(ExchangeMic.Shar);
            Assert.IsFalse(target.IsEuronext,  "Shar");

            target = new Exchange(ExchangeMic.Spec);
            Assert.IsFalse(target.IsEuronext,  "Spec");

            target = new Exchange(ExchangeMic.Sprz);
            Assert.IsFalse(target.IsEuronext,  "Sprz");

            target = new Exchange(ExchangeMic.Ssex);
            Assert.IsFalse(target.IsEuronext,  "Ssex");

            target = new Exchange(ExchangeMic.Stal);
            Assert.IsFalse(target.IsEuronext,  "Stal");

            target = new Exchange(ExchangeMic.Stan);
            Assert.IsFalse(target.IsEuronext,  "Stan");

            target = new Exchange(ExchangeMic.Stsi);
            Assert.IsFalse(target.IsEuronext,  "Stsi");

            target = new Exchange(ExchangeMic.Swap);
            Assert.IsFalse(target.IsEuronext,  "Swap");

            target = new Exchange(ExchangeMic.Tcml);
            Assert.IsFalse(target.IsEuronext,  "Tcml");

            target = new Exchange(ExchangeMic.Tfsv);
            Assert.IsFalse(target.IsEuronext,  "Tfsv");

            target = new Exchange(ExchangeMic.Fxop);
            Assert.IsFalse(target.IsEuronext,  "Fxop");

            target = new Exchange(ExchangeMic.Tpie);
            Assert.IsFalse(target.IsEuronext,  "Tpie");

            target = new Exchange(ExchangeMic.Trax);
            Assert.IsFalse(target.IsEuronext,  "Trax");

            target = new Exchange(ExchangeMic.Trde);
            Assert.IsFalse(target.IsEuronext,  "Trde");

            target = new Exchange(ExchangeMic.Nave);
            Assert.IsFalse(target.IsEuronext,  "Nave");

            target = new Exchange(ExchangeMic.Tcds);
            Assert.IsFalse(target.IsEuronext,  "Tcds");

            target = new Exchange(ExchangeMic.Trdx);
            Assert.IsFalse(target.IsEuronext,  "Trdx");

            target = new Exchange(ExchangeMic.Tfsg);
            Assert.IsFalse(target.IsEuronext,  "Tfsg");

            target = new Exchange(ExchangeMic.Parx);
            Assert.IsFalse(target.IsEuronext,  "Parx");

            target = new Exchange(ExchangeMic.Elix);
            Assert.IsFalse(target.IsEuronext,  "Elix");

            target = new Exchange(ExchangeMic.Treu);
            Assert.IsFalse(target.IsEuronext,  "Treu");

            target = new Exchange(ExchangeMic.Trea);
            Assert.IsFalse(target.IsEuronext,  "Trea");

            target = new Exchange(ExchangeMic.Treo);
            Assert.IsFalse(target.IsEuronext,  "Treo");

            target = new Exchange(ExchangeMic.Trqx);
            Assert.IsFalse(target.IsEuronext,  "Trqx");

            target = new Exchange(ExchangeMic.Trqm);
            Assert.IsFalse(target.IsEuronext,  "Trqm");

            target = new Exchange(ExchangeMic.Trqs);
            Assert.IsFalse(target.IsEuronext,  "Trqs");

            target = new Exchange(ExchangeMic.Trqa);
            Assert.IsFalse(target.IsEuronext,  "Trqa");

            target = new Exchange(ExchangeMic.Trsi);
            Assert.IsFalse(target.IsEuronext,  "Trsi");

            target = new Exchange(ExchangeMic.Ubsb);
            Assert.IsFalse(target.IsEuronext,  "Ubsb");

            target = new Exchange(ExchangeMic.Ubsy);
            Assert.IsFalse(target.IsEuronext,  "Ubsy");

            target = new Exchange(ExchangeMic.Ubsl);
            Assert.IsFalse(target.IsEuronext,  "Ubsl");

            target = new Exchange(ExchangeMic.Ubse);
            Assert.IsFalse(target.IsEuronext,  "Ubse");

            target = new Exchange(ExchangeMic.Ubsi);
            Assert.IsFalse(target.IsEuronext,  "Ubsi");

            target = new Exchange(ExchangeMic.Ukpx);
            Assert.IsFalse(target.IsEuronext,  "Ukpx");

            target = new Exchange(ExchangeMic.Vcmo);
            Assert.IsFalse(target.IsEuronext,  "Vcmo");

            target = new Exchange(ExchangeMic.Vega);
            Assert.IsFalse(target.IsEuronext,  "Vega");

            target = new Exchange(ExchangeMic.Wins);
            Assert.IsFalse(target.IsEuronext,  "Wins");

            target = new Exchange(ExchangeMic.Winx);
            Assert.IsFalse(target.IsEuronext,  "Winx");

            target = new Exchange(ExchangeMic.Xalt);
            Assert.IsFalse(target.IsEuronext,  "Xalt");

            target = new Exchange(ExchangeMic.Xcor);
            Assert.IsFalse(target.IsEuronext,  "Xcor");

            target = new Exchange(ExchangeMic.Xgcl);
            Assert.IsFalse(target.IsEuronext,  "Xgcl");

            target = new Exchange(ExchangeMic.Xlbm);
            Assert.IsFalse(target.IsEuronext,  "Xlbm");

            target = new Exchange(ExchangeMic.Xlch);
            Assert.IsFalse(target.IsEuronext,  "Xlch");

            target = new Exchange(ExchangeMic.Xlme);
            Assert.IsFalse(target.IsEuronext,  "Xlme");

            target = new Exchange(ExchangeMic.Xlon);
            Assert.IsFalse(target.IsEuronext,  "Xlon");

            target = new Exchange(ExchangeMic.Aimx);
            Assert.IsFalse(target.IsEuronext,  "Aimx");

            target = new Exchange(ExchangeMic.Xlod);
            Assert.IsFalse(target.IsEuronext,  "Xlod");

            target = new Exchange(ExchangeMic.Xlom);
            Assert.IsFalse(target.IsEuronext,  "Xlom");

            target = new Exchange(ExchangeMic.Xmts);
            Assert.IsFalse(target.IsEuronext,  "Xmts");

            target = new Exchange(ExchangeMic.Hung);
            Assert.IsFalse(target.IsEuronext,  "Hung");

            target = new Exchange(ExchangeMic.Ukgd);
            Assert.IsFalse(target.IsEuronext,  "Ukgd");

            target = new Exchange(ExchangeMic.Amts);
            Assert.IsFalse(target.IsEuronext,  "Amts");

            target = new Exchange(ExchangeMic.Emts);
            Assert.IsFalse(target.IsEuronext,  "Emts");

            target = new Exchange(ExchangeMic.Gmts);
            Assert.IsFalse(target.IsEuronext,  "Gmts");

            target = new Exchange(ExchangeMic.Imts);
            Assert.IsFalse(target.IsEuronext,  "Imts");

            target = new Exchange(ExchangeMic.Mczk);
            Assert.IsFalse(target.IsEuronext,  "Mczk");

            target = new Exchange(ExchangeMic.Mtsa);
            Assert.IsFalse(target.IsEuronext,  "Mtsa");

            target = new Exchange(ExchangeMic.Mtsg);
            Assert.IsFalse(target.IsEuronext,  "Mtsg");

            target = new Exchange(ExchangeMic.Mtss);
            Assert.IsFalse(target.IsEuronext,  "Mtss");

            target = new Exchange(ExchangeMic.Rmts);
            Assert.IsFalse(target.IsEuronext,  "Rmts");

            target = new Exchange(ExchangeMic.Smts);
            Assert.IsFalse(target.IsEuronext,  "Smts");

            target = new Exchange(ExchangeMic.Vmts);
            Assert.IsFalse(target.IsEuronext,  "Vmts");

            target = new Exchange(ExchangeMic.Bvuk);
            Assert.IsFalse(target.IsEuronext,  "Bvuk");

            target = new Exchange(ExchangeMic.Port);
            Assert.IsFalse(target.IsEuronext,  "Port");

            target = new Exchange(ExchangeMic.Mtsw);
            Assert.IsFalse(target.IsEuronext,  "Mtsw");

            target = new Exchange(ExchangeMic.Xsga);
            Assert.IsFalse(target.IsEuronext,  "Xsga");

            target = new Exchange(ExchangeMic.Xswb);
            Assert.IsFalse(target.IsEuronext,  "Xswb");

            target = new Exchange(ExchangeMic.Xtup);
            Assert.IsFalse(target.IsEuronext,  "Xtup");

            target = new Exchange(ExchangeMic.Tpeq);
            Assert.IsFalse(target.IsEuronext,  "Tpeq");

            target = new Exchange(ExchangeMic.Tben);
            Assert.IsFalse(target.IsEuronext,  "Tben");

            target = new Exchange(ExchangeMic.Tbla);
            Assert.IsFalse(target.IsEuronext,  "Tbla");

            target = new Exchange(ExchangeMic.Tpcd);
            Assert.IsFalse(target.IsEuronext,  "Tpcd");

            target = new Exchange(ExchangeMic.Tpfd);
            Assert.IsFalse(target.IsEuronext,  "Tpfd");

            target = new Exchange(ExchangeMic.Tpre);
            Assert.IsFalse(target.IsEuronext,  "Tpre");

            target = new Exchange(ExchangeMic.Tpsd);
            Assert.IsFalse(target.IsEuronext,  "Tpsd");

            target = new Exchange(ExchangeMic.Xtpe);
            Assert.IsFalse(target.IsEuronext,  "Xtpe");

            target = new Exchange(ExchangeMic.Tpel);
            Assert.IsFalse(target.IsEuronext,  "Tpel");

            target = new Exchange(ExchangeMic.Tpsl);
            Assert.IsFalse(target.IsEuronext,  "Tpsl");

            target = new Exchange(ExchangeMic.Xubs);
            Assert.IsFalse(target.IsEuronext,  "Xubs");

            target = new Exchange(ExchangeMic.Aats);
            Assert.IsFalse(target.IsEuronext,  "Aats");

            target = new Exchange(ExchangeMic.Advt);
            Assert.IsFalse(target.IsEuronext,  "Advt");

            target = new Exchange(ExchangeMic.Aqua);
            Assert.IsFalse(target.IsEuronext,  "Aqua");

            target = new Exchange(ExchangeMic.Atdf);
            Assert.IsFalse(target.IsEuronext,  "Atdf");

            target = new Exchange(ExchangeMic.Core);
            Assert.IsFalse(target.IsEuronext,  "Core");

            target = new Exchange(ExchangeMic.Baml);
            Assert.IsFalse(target.IsEuronext,  "Baml");

            target = new Exchange(ExchangeMic.Mlvx);
            Assert.IsFalse(target.IsEuronext,  "Mlvx");

            target = new Exchange(ExchangeMic.Mlco);
            Assert.IsFalse(target.IsEuronext,  "Mlco");

            target = new Exchange(ExchangeMic.Barx);
            Assert.IsFalse(target.IsEuronext,  "Barx");

            target = new Exchange(ExchangeMic.Bard);
            Assert.IsFalse(target.IsEuronext,  "Bard");

            target = new Exchange(ExchangeMic.Barl);
            Assert.IsFalse(target.IsEuronext,  "Barl");

            target = new Exchange(ExchangeMic.Bcdx);
            Assert.IsFalse(target.IsEuronext,  "Bcdx");

            target = new Exchange(ExchangeMic.Bats);
            Assert.IsFalse(target.IsEuronext,  "Bats");

            target = new Exchange(ExchangeMic.Bato);
            Assert.IsFalse(target.IsEuronext,  "Bato");

            target = new Exchange(ExchangeMic.Baty);
            Assert.IsFalse(target.IsEuronext,  "Baty");

            target = new Exchange(ExchangeMic.Bzxd);
            Assert.IsFalse(target.IsEuronext,  "Bzxd");

            target = new Exchange(ExchangeMic.Byxd);
            Assert.IsFalse(target.IsEuronext,  "Byxd");

            target = new Exchange(ExchangeMic.Bbsf);
            Assert.IsFalse(target.IsEuronext,  "Bbsf");

            target = new Exchange(ExchangeMic.Bgcf);
            Assert.IsFalse(target.IsEuronext,  "Bgcf");

            target = new Exchange(ExchangeMic.Fncs);
            Assert.IsFalse(target.IsEuronext,  "Fncs");

            target = new Exchange(ExchangeMic.Bgcd);
            Assert.IsFalse(target.IsEuronext,  "Bgcd");

            target = new Exchange(ExchangeMic.Bhsf);
            Assert.IsFalse(target.IsEuronext,  "Bhsf");

            target = new Exchange(ExchangeMic.Bids);
            Assert.IsFalse(target.IsEuronext,  "Bids");

            target = new Exchange(ExchangeMic.Bltd);
            Assert.IsFalse(target.IsEuronext,  "Bltd");

            target = new Exchange(ExchangeMic.Bpol);
            Assert.IsFalse(target.IsEuronext,  "Bpol");

            target = new Exchange(ExchangeMic.Bnyc);
            Assert.IsFalse(target.IsEuronext,  "Bnyc");

            target = new Exchange(ExchangeMic.Vtex);
            Assert.IsFalse(target.IsEuronext,  "Vtex");

            target = new Exchange(ExchangeMic.Nyfx);
            Assert.IsFalse(target.IsEuronext,  "Nyfx");

            target = new Exchange(ExchangeMic.Btec);
            Assert.IsFalse(target.IsEuronext,  "Btec");

            target = new Exchange(ExchangeMic.Icsu);
            Assert.IsFalse(target.IsEuronext,  "Icsu");

            target = new Exchange(ExchangeMic.Cded);
            Assert.IsFalse(target.IsEuronext,  "Cded");

            target = new Exchange(ExchangeMic.Cgmi);
            Assert.IsFalse(target.IsEuronext,  "Cgmi");

            target = new Exchange(ExchangeMic.Cicx);
            Assert.IsFalse(target.IsEuronext,  "Cicx");

            target = new Exchange(ExchangeMic.Lqfi);
            Assert.IsFalse(target.IsEuronext,  "Lqfi");

            target = new Exchange(ExchangeMic.Cblc);
            Assert.IsFalse(target.IsEuronext,  "Cblc");

            target = new Exchange(ExchangeMic.Cmsf);
            Assert.IsFalse(target.IsEuronext,  "Cmsf");

            target = new Exchange(ExchangeMic.Cred);
            Assert.IsFalse(target.IsEuronext,  "Cred");

            target = new Exchange(ExchangeMic.Caes);
            Assert.IsFalse(target.IsEuronext,  "Caes");

            target = new Exchange(ExchangeMic.Cslp);
            Assert.IsFalse(target.IsEuronext,  "Cslp");

            target = new Exchange(ExchangeMic.Dbsx);
            Assert.IsFalse(target.IsEuronext,  "Dbsx");

            target = new Exchange(ExchangeMic.Deal);
            Assert.IsFalse(target.IsEuronext,  "Deal");

            target = new Exchange(ExchangeMic.Edge);
            Assert.IsFalse(target.IsEuronext,  "Edge");

            target = new Exchange(ExchangeMic.Eddp);
            Assert.IsFalse(target.IsEuronext,  "Eddp");

            target = new Exchange(ExchangeMic.Edga);
            Assert.IsFalse(target.IsEuronext,  "Edga");

            target = new Exchange(ExchangeMic.Edgd);
            Assert.IsFalse(target.IsEuronext,  "Edgd");

            target = new Exchange(ExchangeMic.Edgx);
            Assert.IsFalse(target.IsEuronext,  "Edgx");

            target = new Exchange(ExchangeMic.Edgo);
            Assert.IsFalse(target.IsEuronext,  "Edgo");

            target = new Exchange(ExchangeMic.Egmt);
            Assert.IsFalse(target.IsEuronext,  "Egmt");

            target = new Exchange(ExchangeMic.Eris);
            Assert.IsFalse(target.IsEuronext,  "Eris");

            target = new Exchange(ExchangeMic.Fast);
            Assert.IsFalse(target.IsEuronext,  "Fast");

            target = new Exchange(ExchangeMic.Finr);
            Assert.IsFalse(target.IsEuronext,  "Finr");

            target = new Exchange(ExchangeMic.Finn);
            Assert.IsFalse(target.IsEuronext,  "Finn");

            target = new Exchange(ExchangeMic.Fino);
            Assert.IsFalse(target.IsEuronext,  "Fino");

            target = new Exchange(ExchangeMic.Finy);
            Assert.IsFalse(target.IsEuronext,  "Finy");

            target = new Exchange(ExchangeMic.Xadf);
            Assert.IsFalse(target.IsEuronext,  "Xadf");

            target = new Exchange(ExchangeMic.Ootc);
            Assert.IsFalse(target.IsEuronext,  "Ootc");

            target = new Exchange(ExchangeMic.Fsef);
            Assert.IsFalse(target.IsEuronext,  "Fsef");

            target = new Exchange(ExchangeMic.Fxal);
            Assert.IsFalse(target.IsEuronext,  "Fxal");

            target = new Exchange(ExchangeMic.Fxcm);
            Assert.IsFalse(target.IsEuronext,  "Fxcm");

            target = new Exchange(ExchangeMic.G1xx);
            Assert.IsFalse(target.IsEuronext,  "G1xx");

            target = new Exchange(ExchangeMic.Gllc);
            Assert.IsFalse(target.IsEuronext,  "Gllc");

            target = new Exchange(ExchangeMic.Glps);
            Assert.IsFalse(target.IsEuronext,  "Glps");

            target = new Exchange(ExchangeMic.Glpx);
            Assert.IsFalse(target.IsEuronext,  "Glpx");

            target = new Exchange(ExchangeMic.Gotc);
            Assert.IsFalse(target.IsEuronext,  "Gotc");

            target = new Exchange(ExchangeMic.Govx);
            Assert.IsFalse(target.IsEuronext,  "Govx");

            target = new Exchange(ExchangeMic.Gree);
            Assert.IsFalse(target.IsEuronext,  "Gree");

            target = new Exchange(ExchangeMic.Gsco);
            Assert.IsFalse(target.IsEuronext,  "Gsco");

            target = new Exchange(ExchangeMic.Sgmt);
            Assert.IsFalse(target.IsEuronext,  "Sgmt");

            target = new Exchange(ExchangeMic.Gsef);
            Assert.IsFalse(target.IsEuronext,  "Gsef");

            target = new Exchange(ExchangeMic.Gtco);
            Assert.IsFalse(target.IsEuronext,  "Gtco");

            target = new Exchange(ExchangeMic.Gtsx);
            Assert.IsFalse(target.IsEuronext,  "Gtsx");

            target = new Exchange(ExchangeMic.Gtxs);
            Assert.IsFalse(target.IsEuronext,  "Gtxs");

            target = new Exchange(ExchangeMic.Hegx);
            Assert.IsFalse(target.IsEuronext,  "Hegx");

            target = new Exchange(ExchangeMic.Hppo);
            Assert.IsFalse(target.IsEuronext,  "Hppo");

            target = new Exchange(ExchangeMic.Hsfx);
            Assert.IsFalse(target.IsEuronext,  "Hsfx");

            target = new Exchange(ExchangeMic.Icel);
            Assert.IsFalse(target.IsEuronext,  "Icel");

            target = new Exchange(ExchangeMic.Iexg);
            Assert.IsFalse(target.IsEuronext,  "Iexg");

            target = new Exchange(ExchangeMic.Iexd);
            Assert.IsFalse(target.IsEuronext,  "Iexd");

            target = new Exchange(ExchangeMic.Ifus);
            Assert.IsFalse(target.IsEuronext,  "Ifus");

            target = new Exchange(ExchangeMic.Iepa);
            Assert.IsFalse(target.IsEuronext,  "Iepa");

            target = new Exchange(ExchangeMic.Imfx);
            Assert.IsFalse(target.IsEuronext,  "Imfx");

            target = new Exchange(ExchangeMic.Imag);
            Assert.IsFalse(target.IsEuronext,  "Imag");

            target = new Exchange(ExchangeMic.Imbd);
            Assert.IsFalse(target.IsEuronext,  "Imbd");

            target = new Exchange(ExchangeMic.Imcr);
            Assert.IsFalse(target.IsEuronext,  "Imcr");

            target = new Exchange(ExchangeMic.Imen);
            Assert.IsFalse(target.IsEuronext,  "Imen");

            target = new Exchange(ExchangeMic.Imir);
            Assert.IsFalse(target.IsEuronext,  "Imir");

            target = new Exchange(ExchangeMic.Ifed);
            Assert.IsFalse(target.IsEuronext,  "Ifed");

            target = new Exchange(ExchangeMic.Imcg);
            Assert.IsFalse(target.IsEuronext,  "Imcg");

            target = new Exchange(ExchangeMic.Imcc);
            Assert.IsFalse(target.IsEuronext,  "Imcc");

            target = new Exchange(ExchangeMic.Ices);
            Assert.IsFalse(target.IsEuronext,  "Ices");

            target = new Exchange(ExchangeMic.Imcs);
            Assert.IsFalse(target.IsEuronext,  "Imcs");

            target = new Exchange(ExchangeMic.Isda);
            Assert.IsFalse(target.IsEuronext,  "Isda");

            target = new Exchange(ExchangeMic.Itgi);
            Assert.IsFalse(target.IsEuronext,  "Itgi");

            target = new Exchange(ExchangeMic.Jefx);
            Assert.IsFalse(target.IsEuronext,  "Jefx");

            target = new Exchange(ExchangeMic.Jlqd);
            Assert.IsFalse(target.IsEuronext,  "Jlqd");

            target = new Exchange(ExchangeMic.Jpbx);
            Assert.IsFalse(target.IsEuronext,  "Jpbx");

            target = new Exchange(ExchangeMic.Jpmx);
            Assert.IsFalse(target.IsEuronext,  "Jpmx");

            target = new Exchange(ExchangeMic.Jses);
            Assert.IsFalse(target.IsEuronext,  "Jses");

            target = new Exchange(ExchangeMic.Jsjx);
            Assert.IsFalse(target.IsEuronext,  "Jsjx");

            target = new Exchange(ExchangeMic.Knig);
            Assert.IsFalse(target.IsEuronext,  "Knig");

            target = new Exchange(ExchangeMic.Kncm);
            Assert.IsFalse(target.IsEuronext,  "Kncm");

            target = new Exchange(ExchangeMic.Knem);
            Assert.IsFalse(target.IsEuronext,  "Knem");

            target = new Exchange(ExchangeMic.Knli);
            Assert.IsFalse(target.IsEuronext,  "Knli");

            target = new Exchange(ExchangeMic.Knmx);
            Assert.IsFalse(target.IsEuronext,  "Knmx");

            target = new Exchange(ExchangeMic.Ackf);
            Assert.IsFalse(target.IsEuronext,  "Ackf");

            target = new Exchange(ExchangeMic.Lasf);
            Assert.IsFalse(target.IsEuronext,  "Lasf");

            target = new Exchange(ExchangeMic.Ledg);
            Assert.IsFalse(target.IsEuronext,  "Ledg");

            target = new Exchange(ExchangeMic.Levl);
            Assert.IsFalse(target.IsEuronext,  "Levl");

            target = new Exchange(ExchangeMic.Lius);
            Assert.IsFalse(target.IsEuronext,  "Lius");

            target = new Exchange(ExchangeMic.Lifi);
            Assert.IsFalse(target.IsEuronext,  "Lifi");

            target = new Exchange(ExchangeMic.Liuh);
            Assert.IsFalse(target.IsEuronext,  "Liuh");

            target = new Exchange(ExchangeMic.Lqed);
            Assert.IsFalse(target.IsEuronext,  "Lqed");

            target = new Exchange(ExchangeMic.Ltaa);
            Assert.IsFalse(target.IsEuronext,  "Ltaa");

            target = new Exchange(ExchangeMic.Lmnx);
            Assert.IsFalse(target.IsEuronext,  "Lmnx");

            target = new Exchange(ExchangeMic.Mihi);
            Assert.IsFalse(target.IsEuronext,  "Mihi");

            target = new Exchange(ExchangeMic.Xmio);
            Assert.IsFalse(target.IsEuronext,  "Xmio");

            target = new Exchange(ExchangeMic.Mprl);
            Assert.IsFalse(target.IsEuronext,  "Mprl");

            target = new Exchange(ExchangeMic.Msco);
            Assert.IsFalse(target.IsEuronext,  "Msco");

            target = new Exchange(ExchangeMic.Mspl);
            Assert.IsFalse(target.IsEuronext,  "Mspl");

            target = new Exchange(ExchangeMic.Msrp);
            Assert.IsFalse(target.IsEuronext,  "Msrp");

            target = new Exchange(ExchangeMic.Mstx);
            Assert.IsFalse(target.IsEuronext,  "Mstx");

            target = new Exchange(ExchangeMic.Mslp);
            Assert.IsFalse(target.IsEuronext,  "Mslp");

            target = new Exchange(ExchangeMic.Mtus);
            Assert.IsFalse(target.IsEuronext,  "Mtus");

            target = new Exchange(ExchangeMic.Bvus);
            Assert.IsFalse(target.IsEuronext,  "Bvus");

            target = new Exchange(ExchangeMic.Mtsb);
            Assert.IsFalse(target.IsEuronext,  "Mtsb");

            target = new Exchange(ExchangeMic.Mtxx);
            Assert.IsFalse(target.IsEuronext,  "Mtxx");

            target = new Exchange(ExchangeMic.Mtxs);
            Assert.IsFalse(target.IsEuronext,  "Mtxs");

            target = new Exchange(ExchangeMic.Mtxm);
            Assert.IsFalse(target.IsEuronext,  "Mtxm");

            target = new Exchange(ExchangeMic.Mtxc);
            Assert.IsFalse(target.IsEuronext,  "Mtxc");

            target = new Exchange(ExchangeMic.Mtxa);
            Assert.IsFalse(target.IsEuronext,  "Mtxa");

            target = new Exchange(ExchangeMic.Nblx);
            Assert.IsFalse(target.IsEuronext,  "Nblx");

            target = new Exchange(ExchangeMic.Nfsc);
            Assert.IsFalse(target.IsEuronext,  "Nfsc");

            target = new Exchange(ExchangeMic.Nfsa);
            Assert.IsFalse(target.IsEuronext,  "Nfsa");

            target = new Exchange(ExchangeMic.Nfsd);
            Assert.IsFalse(target.IsEuronext,  "Nfsd");

            target = new Exchange(ExchangeMic.Xstm);
            Assert.IsFalse(target.IsEuronext,  "Xstm");

            target = new Exchange(ExchangeMic.Nmra);
            Assert.IsFalse(target.IsEuronext,  "Nmra");

            target = new Exchange(ExchangeMic.Nodx);
            Assert.IsFalse(target.IsEuronext,  "Nodx");

            target = new Exchange(ExchangeMic.Nxus);
            Assert.IsFalse(target.IsEuronext,  "Nxus");

            target = new Exchange(ExchangeMic.Nypc);
            Assert.IsFalse(target.IsEuronext,  "Nypc");

            target = new Exchange(ExchangeMic.Ollc);
            Assert.IsFalse(target.IsEuronext,  "Ollc");

            target = new Exchange(ExchangeMic.Opra);
            Assert.IsFalse(target.IsEuronext,  "Opra");

            target = new Exchange(ExchangeMic.Otcm);
            Assert.IsFalse(target.IsEuronext,  "Otcm");

            target = new Exchange(ExchangeMic.Otcb);
            Assert.IsFalse(target.IsEuronext,  "Otcb");

            target = new Exchange(ExchangeMic.Otcq);
            Assert.IsFalse(target.IsEuronext,  "Otcq");

            target = new Exchange(ExchangeMic.Pinc);
            Assert.IsFalse(target.IsEuronext,  "Pinc");

            target = new Exchange(ExchangeMic.Pini);
            Assert.IsFalse(target.IsEuronext,  "Pini");

            target = new Exchange(ExchangeMic.Pinl);
            Assert.IsFalse(target.IsEuronext,  "Pinl");

            target = new Exchange(ExchangeMic.Pinx);
            Assert.IsFalse(target.IsEuronext,  "Pinx");

            target = new Exchange(ExchangeMic.Psgm);
            Assert.IsFalse(target.IsEuronext,  "Psgm");

            target = new Exchange(ExchangeMic.Cave);
            Assert.IsFalse(target.IsEuronext,  "Cave");

            target = new Exchange(ExchangeMic.Pdqx);
            Assert.IsFalse(target.IsEuronext,  "Pdqx");

            target = new Exchange(ExchangeMic.Pdqd);
            Assert.IsFalse(target.IsEuronext,  "Pdqd");

            target = new Exchange(ExchangeMic.Pipe);
            Assert.IsFalse(target.IsEuronext,  "Pipe");

            target = new Exchange(ExchangeMic.Prse);
            Assert.IsFalse(target.IsEuronext,  "Prse");

            target = new Exchange(ExchangeMic.Pulx);
            Assert.IsFalse(target.IsEuronext,  "Pulx");

            target = new Exchange(ExchangeMic.Ricx);
            Assert.IsFalse(target.IsEuronext,  "Ricx");

            target = new Exchange(ExchangeMic.Ricd);
            Assert.IsFalse(target.IsEuronext,  "Ricd");

            target = new Exchange(ExchangeMic.Scxs);
            Assert.IsFalse(target.IsEuronext,  "Scxs");

            target = new Exchange(ExchangeMic.Sgma);
            Assert.IsFalse(target.IsEuronext,  "Sgma");

            target = new Exchange(ExchangeMic.Shaw);
            Assert.IsFalse(target.IsEuronext,  "Shaw");

            target = new Exchange(ExchangeMic.Shad);
            Assert.IsFalse(target.IsEuronext,  "Shad");

            target = new Exchange(ExchangeMic.Soho);
            Assert.IsFalse(target.IsEuronext,  "Soho");

            target = new Exchange(ExchangeMic.Sstx);
            Assert.IsFalse(target.IsEuronext,  "Sstx");

            target = new Exchange(ExchangeMic.Tera);
            Assert.IsFalse(target.IsEuronext,  "Tera");

            target = new Exchange(ExchangeMic.Tfsu);
            Assert.IsFalse(target.IsEuronext,  "Tfsu");

            target = new Exchange(ExchangeMic.Them);
            Assert.IsFalse(target.IsEuronext,  "Them");

            target = new Exchange(ExchangeMic.Thre);
            Assert.IsFalse(target.IsEuronext,  "Thre");

            target = new Exchange(ExchangeMic.Tmid);
            Assert.IsFalse(target.IsEuronext,  "Tmid");

            target = new Exchange(ExchangeMic.Tpse);
            Assert.IsFalse(target.IsEuronext,  "Tpse");

            target = new Exchange(ExchangeMic.Trck);
            Assert.IsFalse(target.IsEuronext,  "Trck");

            target = new Exchange(ExchangeMic.Trux);
            Assert.IsFalse(target.IsEuronext,  "Trux");

            target = new Exchange(ExchangeMic.Tru1);
            Assert.IsFalse(target.IsEuronext,  "Tru1");

            target = new Exchange(ExchangeMic.Tru2);
            Assert.IsFalse(target.IsEuronext,  "Tru2");

            target = new Exchange(ExchangeMic.Trwb);
            Assert.IsFalse(target.IsEuronext,  "Trwb");

            target = new Exchange(ExchangeMic.Bndd);
            Assert.IsFalse(target.IsEuronext,  "Bndd");

            target = new Exchange(ExchangeMic.Twsf);
            Assert.IsFalse(target.IsEuronext,  "Twsf");

            target = new Exchange(ExchangeMic.Dwsf);
            Assert.IsFalse(target.IsEuronext,  "Dwsf");

            target = new Exchange(ExchangeMic.Tsad);
            Assert.IsFalse(target.IsEuronext,  "Tsad");

            target = new Exchange(ExchangeMic.Tsbx);
            Assert.IsFalse(target.IsEuronext,  "Tsbx");

            target = new Exchange(ExchangeMic.Tsef);
            Assert.IsFalse(target.IsEuronext,  "Tsef");

            target = new Exchange(ExchangeMic.Ubsa);
            Assert.IsFalse(target.IsEuronext,  "Ubsa");

            target = new Exchange(ExchangeMic.Ubsp);
            Assert.IsFalse(target.IsEuronext,  "Ubsp");

            target = new Exchange(ExchangeMic.Vert);
            Assert.IsFalse(target.IsEuronext,  "Vert");

            target = new Exchange(ExchangeMic.Vfcm);
            Assert.IsFalse(target.IsEuronext,  "Vfcm");

            target = new Exchange(ExchangeMic.Virt);
            Assert.IsFalse(target.IsEuronext,  "Virt");

            target = new Exchange(ExchangeMic.Weed);
            Assert.IsFalse(target.IsEuronext,  "Weed");

            target = new Exchange(ExchangeMic.Xwee);
            Assert.IsFalse(target.IsEuronext,  "Xwee");

            target = new Exchange(ExchangeMic.Welx);
            Assert.IsFalse(target.IsEuronext,  "Welx");

            target = new Exchange(ExchangeMic.Wsag);
            Assert.IsFalse(target.IsEuronext,  "Wsag");

            target = new Exchange(ExchangeMic.Xaqs);
            Assert.IsFalse(target.IsEuronext,  "Xaqs");

            target = new Exchange(ExchangeMic.Xbox);
            Assert.IsFalse(target.IsEuronext,  "Xbox");

            target = new Exchange(ExchangeMic.Xcbo);
            Assert.IsFalse(target.IsEuronext,  "Xcbo");

            target = new Exchange(ExchangeMic.C2Ox);
            Assert.IsFalse(target.IsEuronext,  "C2Ox");

            target = new Exchange(ExchangeMic.Cbsx);
            Assert.IsFalse(target.IsEuronext,  "Cbsx");

            target = new Exchange(ExchangeMic.Xcbf);
            Assert.IsFalse(target.IsEuronext,  "Xcbf");

            target = new Exchange(ExchangeMic.Xcbt);
            Assert.IsFalse(target.IsEuronext,  "Xcbt");

            target = new Exchange(ExchangeMic.Fcbt);
            Assert.IsFalse(target.IsEuronext,  "Fcbt");

            target = new Exchange(ExchangeMic.Xkbt);
            Assert.IsFalse(target.IsEuronext,  "Xkbt");

            target = new Exchange(ExchangeMic.Xcff);
            Assert.IsFalse(target.IsEuronext,  "Xcff");

            target = new Exchange(ExchangeMic.Xchi);
            Assert.IsFalse(target.IsEuronext,  "Xchi");

            target = new Exchange(ExchangeMic.Xcis);
            Assert.IsFalse(target.IsEuronext,  "Xcis");

            target = new Exchange(ExchangeMic.Xcme);
            Assert.IsFalse(target.IsEuronext,  "Xcme");

            target = new Exchange(ExchangeMic.Fcme);
            Assert.IsFalse(target.IsEuronext,  "Fcme");

            target = new Exchange(ExchangeMic.Glbx);
            Assert.IsFalse(target.IsEuronext,  "Glbx");

            target = new Exchange(ExchangeMic.Ximm);
            Assert.IsFalse(target.IsEuronext,  "Ximm");

            target = new Exchange(ExchangeMic.Xiom);
            Assert.IsFalse(target.IsEuronext,  "Xiom");

            target = new Exchange(ExchangeMic.Nyms);
            Assert.IsFalse(target.IsEuronext,  "Nyms");

            target = new Exchange(ExchangeMic.Cmes);
            Assert.IsFalse(target.IsEuronext,  "Cmes");

            target = new Exchange(ExchangeMic.Cbts);
            Assert.IsFalse(target.IsEuronext,  "Cbts");

            target = new Exchange(ExchangeMic.Cecs);
            Assert.IsFalse(target.IsEuronext,  "Cecs");

            target = new Exchange(ExchangeMic.Xcur);
            Assert.IsFalse(target.IsEuronext,  "Xcur");

            target = new Exchange(ExchangeMic.Xelx);
            Assert.IsFalse(target.IsEuronext,  "Xelx");

            target = new Exchange(ExchangeMic.Xfci);
            Assert.IsFalse(target.IsEuronext,  "Xfci");

            target = new Exchange(ExchangeMic.Xgmx);
            Assert.IsFalse(target.IsEuronext,  "Xgmx");

            target = new Exchange(ExchangeMic.Xins);
            Assert.IsFalse(target.IsEuronext,  "Xins");

            target = new Exchange(ExchangeMic.Iblx);
            Assert.IsFalse(target.IsEuronext,  "Iblx");

            target = new Exchange(ExchangeMic.Icbx);
            Assert.IsFalse(target.IsEuronext,  "Icbx");

            target = new Exchange(ExchangeMic.Icro);
            Assert.IsFalse(target.IsEuronext,  "Icro");

            target = new Exchange(ExchangeMic.Iidx);
            Assert.IsFalse(target.IsEuronext,  "Iidx");

            target = new Exchange(ExchangeMic.Rcbx);
            Assert.IsFalse(target.IsEuronext,  "Rcbx");

            target = new Exchange(ExchangeMic.Mocx);
            Assert.IsFalse(target.IsEuronext,  "Mocx");

            target = new Exchange(ExchangeMic.Xisx);
            Assert.IsFalse(target.IsEuronext,  "Xisx");

            target = new Exchange(ExchangeMic.Xisa);
            Assert.IsFalse(target.IsEuronext,  "Xisa");

            target = new Exchange(ExchangeMic.Xise);
            Assert.IsFalse(target.IsEuronext,  "Xise");

            target = new Exchange(ExchangeMic.Mcry);
            Assert.IsFalse(target.IsEuronext,  "Mcry");

            target = new Exchange(ExchangeMic.Gmni);
            Assert.IsFalse(target.IsEuronext,  "Gmni");

            target = new Exchange(ExchangeMic.Xmer);
            Assert.IsFalse(target.IsEuronext,  "Xmer");

            target = new Exchange(ExchangeMic.Xmge);
            Assert.IsFalse(target.IsEuronext,  "Xmge");

            target = new Exchange(ExchangeMic.Xnas);
            Assert.IsFalse(target.IsEuronext,  "Xnas");

            target = new Exchange(ExchangeMic.Xbxo);
            Assert.IsFalse(target.IsEuronext,  "Xbxo");

            target = new Exchange(ExchangeMic.Bosd);
            Assert.IsFalse(target.IsEuronext,  "Bosd");

            target = new Exchange(ExchangeMic.Nasd);
            Assert.IsFalse(target.IsEuronext,  "Nasd");

            target = new Exchange(ExchangeMic.Xbrt);
            Assert.IsFalse(target.IsEuronext,  "Xbrt");

            target = new Exchange(ExchangeMic.Xncm);
            Assert.IsFalse(target.IsEuronext,  "Xncm");

            target = new Exchange(ExchangeMic.Xndq);
            Assert.IsFalse(target.IsEuronext,  "Xndq");

            target = new Exchange(ExchangeMic.Xngs);
            Assert.IsFalse(target.IsEuronext,  "Xngs");

            target = new Exchange(ExchangeMic.Xnim);
            Assert.IsFalse(target.IsEuronext,  "Xnim");

            target = new Exchange(ExchangeMic.Xnms);
            Assert.IsFalse(target.IsEuronext,  "Xnms");

            target = new Exchange(ExchangeMic.Xpbt);
            Assert.IsFalse(target.IsEuronext,  "Xpbt");

            target = new Exchange(ExchangeMic.Xphl);
            Assert.IsFalse(target.IsEuronext,  "Xphl");

            target = new Exchange(ExchangeMic.Xpho);
            Assert.IsFalse(target.IsEuronext,  "Xpho");

            target = new Exchange(ExchangeMic.Xpor);
            Assert.IsFalse(target.IsEuronext,  "Xpor");

            target = new Exchange(ExchangeMic.Xpsx);
            Assert.IsFalse(target.IsEuronext,  "Xpsx");

            target = new Exchange(ExchangeMic.Espd);
            Assert.IsFalse(target.IsEuronext,  "Espd");

            target = new Exchange(ExchangeMic.Xbos);
            Assert.IsFalse(target.IsEuronext,  "Xbos");

            target = new Exchange(ExchangeMic.Xnym);
            Assert.IsFalse(target.IsEuronext,  "Xnym");

            target = new Exchange(ExchangeMic.Xcec);
            Assert.IsFalse(target.IsEuronext,  "Xcec");

            target = new Exchange(ExchangeMic.Xnye);
            Assert.IsFalse(target.IsEuronext,  "Xnye");

            target = new Exchange(ExchangeMic.Xnyl);
            Assert.IsFalse(target.IsEuronext,  "Xnyl");

            target = new Exchange(ExchangeMic.Xnys);
            Assert.IsFalse(target.IsEuronext,  "Xnys");

            target = new Exchange(ExchangeMic.Aldp);
            Assert.IsFalse(target.IsEuronext,  "Aldp");

            target = new Exchange(ExchangeMic.Amxo);
            Assert.IsFalse(target.IsEuronext,  "Amxo");

            target = new Exchange(ExchangeMic.Arcd);
            Assert.IsFalse(target.IsEuronext,  "Arcd");

            target = new Exchange(ExchangeMic.Arco);
            Assert.IsFalse(target.IsEuronext,  "Arco");

            target = new Exchange(ExchangeMic.Arcx);
            Assert.IsFalse(target.IsEuronext,  "Arcx");

            target = new Exchange(ExchangeMic.Nysd);
            Assert.IsFalse(target.IsEuronext,  "Nysd");

            target = new Exchange(ExchangeMic.Xase);
            Assert.IsFalse(target.IsEuronext,  "Xase");

            target = new Exchange(ExchangeMic.Xnli);
            Assert.IsFalse(target.IsEuronext,  "Xnli");

            target = new Exchange(ExchangeMic.Xoch);
            Assert.IsFalse(target.IsEuronext,  "Xoch");

            target = new Exchange(ExchangeMic.Xotc);
            Assert.IsFalse(target.IsEuronext,  "Xotc");

            target = new Exchange(ExchangeMic.Xsef);
            Assert.IsFalse(target.IsEuronext,  "Xsef");

            target = new Exchange(ExchangeMic.Bilt);
            Assert.IsFalse(target.IsEuronext,  "Bilt");

            target = new Exchange(ExchangeMic.Xoff);
            Assert.IsFalse(target.IsEuronext,  "Xoff");

            target = new Exchange(ExchangeMic.Xxxx);
            Assert.IsFalse(target.IsEuronext,  "Xxxx");
        }

        [TestMethod]
        public void Exchange_Mic_CorrectResult()
        {
            var target = new Exchange(EuronextMic.Xbru);
            Assert.AreEqual(ExchangeMic.Xbru, target.Mic, "Xbru");

            target = new Exchange(EuronextMic.Alxb);
            Assert.AreEqual(ExchangeMic.Alxb, target.Mic, "Alxb");

            target = new Exchange(EuronextMic.Enxb);
            Assert.AreEqual(ExchangeMic.Enxb, target.Mic, "Enxb");

            target = new Exchange(EuronextMic.Mlxb);
            Assert.AreEqual(ExchangeMic.Mlxb, target.Mic, "Mlxb");

            target = new Exchange(EuronextMic.Tnlb);
            Assert.AreEqual(ExchangeMic.Tnlb, target.Mic, "Tnlb");

            target = new Exchange(EuronextMic.Vpxb);
            Assert.AreEqual(ExchangeMic.Vpxb, target.Mic, "Vpxb");

            target = new Exchange(EuronextMic.Xbrd);
            Assert.AreEqual(ExchangeMic.Xbrd, target.Mic, "Xbrd");

            target = new Exchange(EuronextMic.Xpar);
            Assert.AreEqual(ExchangeMic.Xpar, target.Mic, "Xpar");

            target = new Exchange(EuronextMic.Alxp);
            Assert.AreEqual(ExchangeMic.Alxp, target.Mic, "Alxp");

            target = new Exchange(EuronextMic.Xmat);
            Assert.AreEqual(ExchangeMic.Xmat, target.Mic, "Xmat");

            target = new Exchange(EuronextMic.Xmli);
            Assert.AreEqual(ExchangeMic.Xmli, target.Mic, "Xmli");

            target = new Exchange(EuronextMic.Xmon);
            Assert.AreEqual(ExchangeMic.Xmon, target.Mic, "Xmon");

            target = new Exchange(EuronextMic.Xspm);
            Assert.AreEqual(ExchangeMic.Xspm, target.Mic, "Xspm");

            target = new Exchange(EuronextMic.Xlis);
            Assert.AreEqual(ExchangeMic.Xlis, target.Mic, "Xlis");

            target = new Exchange(EuronextMic.Alxl);
            Assert.AreEqual(ExchangeMic.Alxl, target.Mic, "Alxl");

            target = new Exchange(EuronextMic.Enxl);
            Assert.AreEqual(ExchangeMic.Enxl, target.Mic, "Enxl");

            target = new Exchange(EuronextMic.Mfox);
            Assert.AreEqual(ExchangeMic.Mfox, target.Mic, "Mfox");

            target = new Exchange(EuronextMic.Wqxl);
            Assert.AreEqual(ExchangeMic.Wqxl, target.Mic, "Wqxl");

            target = new Exchange(EuronextMic.Xams);
            Assert.AreEqual(ExchangeMic.Xams, target.Mic, "Xams");

            target = new Exchange(EuronextMic.Tnla);
            Assert.AreEqual(ExchangeMic.Tnla, target.Mic, "Tnla");

            target = new Exchange(EuronextMic.Xeuc);
            Assert.AreEqual(ExchangeMic.Xeuc, target.Mic, "Xeuc");

            target = new Exchange(EuronextMic.Xeue);
            Assert.AreEqual(ExchangeMic.Xeue, target.Mic, "Xeue");

            target = new Exchange(EuronextMic.Xeui);
            Assert.AreEqual(ExchangeMic.Xeui, target.Mic, "Xeui");

            target = new Exchange(EuronextMic.Xldn);
            Assert.AreEqual(ExchangeMic.Xldn, target.Mic, "Xldn");

            target = new Exchange(EuronextMic.Xsmp);
            Assert.AreEqual(ExchangeMic.Xsmp, target.Mic, "Xsmp");

            target = new Exchange(EuronextMic.Ensy);
            Assert.AreEqual(ExchangeMic.Ensy, target.Mic, "Ensy");

            target = new Exchange(EuronextMic.Xxxx);
            Assert.AreEqual(ExchangeMic.Xxxx, target.Mic, "Xxxx");
        }
    }
}
