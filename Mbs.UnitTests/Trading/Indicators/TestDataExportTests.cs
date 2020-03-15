using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;
using Mbs.Trading.Indicators.GeraldGoertzel;
using Mbs.Trading.Indicators.JohnBollinger;

namespace Mbs.UnitTests.Trading.Indicators
{
    [TestClass]
    public class TestDataExportTests
    {
        private const string FilePrefix = "d:\\test_data_";
        private const int Middle = (int)BollingerBands.OutputKind.MiddleMovingAverageValue;
        private const int StDev = (int)BollingerBands.OutputKind.StandardDeviationValue;
        private const int Lower = (int)BollingerBands.OutputKind.LowerBandValue;
        private const int Upper = (int)BollingerBands.OutputKind.UpperBandValue;
        private const int PercentB = (int)BollingerBands.OutputKind.PercentBandValue;
        private const int BandWidth = (int)BollingerBands.OutputKind.BandWidthValue;
        private const int LowerUpper = (int)BollingerBands.OutputKind.LowerUpperBand;

        #region Test data
        private static readonly double[] Open =
        {
            92.500000,91.500000,95.155000,93.970000,95.500000,94.500000,95.000000,91.500000,91.815000,91.125000,93.875000,
            97.500000,98.815000,92.000000,91.125000,91.875000,93.405000,89.750000,89.345000,92.250000,89.780000,
            87.940000,87.595000,85.220000,83.500000,83.500000,81.250000,85.125000,88.125000,87.500000,85.250000,
            86.000000,87.190000,86.125000,89.000000,88.625000,86.000000,85.500000,84.750000,85.250000,84.250000,
            86.750000,86.940000,89.315000,89.940000,90.815000,91.190000,91.345000,89.595000,91.000000,89.750000,
            88.750000,88.315000,84.345000,83.500000,84.000000,86.000000,85.530000,87.500000,88.500000,90.000000,
            88.655000,89.500000,91.565000,92.000000,93.000000,92.815000,91.750000,92.000000,91.375000,89.750000,
            88.750000,85.440000,83.500000,84.875000,98.625000,96.690000,102.375000,106.000000,104.625000,102.500000,
            104.250000,104.000000,106.125000,106.065000,105.940000,105.625000,108.625000,110.250000,110.565000,117.000000,
            120.750000,118.000000,119.125000,119.125000,117.815000,116.375000,115.155000,111.250000,111.500000,116.690000,
            116.000000,113.620000,111.750000,114.560000,113.620000,118.120000,119.870000,116.620000,115.870000,115.060000,
            115.870000,117.500000,119.870000,119.250000,120.190000,122.870000,123.870000,122.250000,123.120000,123.310000,
            124.000000,123.000000,124.810000,130.000000,130.880000,132.500000,131.000000,132.500000,134.000000,137.440000,
            135.750000,138.310000,138.000000,136.380000,136.500000,132.000000,127.500000,127.620000,124.000000,123.620000,
            125.000000,126.370000,126.250000,125.940000,124.000000,122.750000,120.000000,120.000000,122.000000,123.620000,
            121.500000,120.120000,123.750000,122.750000,125.000000,128.500000,128.380000,123.870000,124.370000,122.750000,
            123.370000,122.000000,122.620000,125.000000,124.250000,124.370000,125.620000,126.500000,128.380000,128.880000,
            131.500000,132.500000,137.500000,134.630000,132.000000,134.000000,132.000000,131.380000,126.500000,128.750000,
            127.190000,127.500000,120.500000,126.620000,123.000000,122.060000,121.000000,121.000000,118.000000,122.000000,
            122.250000,119.120000,115.000000,113.500000,114.000000,110.810000,106.500000,106.440000,108.000000,107.000000,
            108.620000,93.000000,93.750000,94.250000,94.870000,95.500000,94.500000,97.000000,98.500000,96.750000,
            95.870000,94.440000,92.750000,90.500000,95.060000,94.620000,97.500000,96.000000,96.000000,94.620000,
            94.870000,94.000000,99.000000,105.500000,108.810000,105.000000,105.940000,104.940000,103.690000,102.560000,
            103.440000,109.810000,113.000000,117.000000,116.250000,120.500000,111.620000,108.120000,110.190000,107.750000,
            108.000000,110.690000,109.060000,108.500000,109.870000,109.120000,109.690000,109.560000,110.440000,109.690000,
            109.190000
        };

        private static readonly double[] High =
        {
            93.250000,94.940000,96.375000,96.190000,96.000000,94.720000,95.000000,93.720000,92.470000,92.750000,96.250000,
            99.625000,99.125000,92.750000,91.315000,93.250000,93.405000,90.655000,91.970000,92.250000,90.345000,
            88.500000,88.250000,85.500000,84.440000,84.750000,84.440000,89.405000,88.125000,89.125000,87.155000,
            87.250000,87.375000,88.970000,90.000000,89.845000,86.970000,85.940000,84.750000,85.470000,84.470000,
            88.500000,89.470000,90.000000,92.440000,91.440000,92.970000,91.720000,91.155000,91.750000,90.000000,
            88.875000,89.000000,85.250000,83.815000,85.250000,86.625000,87.940000,89.375000,90.625000,90.750000,
            88.845000,91.970000,93.375000,93.815000,94.030000,94.030000,91.815000,92.000000,91.940000,89.750000,
            88.750000,86.155000,84.875000,85.940000,99.375000,103.280000,105.375000,107.625000,105.250000,104.500000,
            105.500000,106.125000,107.940000,106.250000,107.000000,108.750000,110.940000,110.940000,114.220000,123.000000,
            121.750000,119.815000,120.315000,119.375000,118.190000,116.690000,115.345000,113.000000,118.315000,116.870000,
            116.750000,113.870000,114.620000,115.310000,116.000000,121.690000,119.870000,120.870000,116.750000,116.500000,
            116.000000,118.310000,121.500000,122.000000,121.440000,125.750000,127.750000,124.190000,124.440000,125.750000,
            124.690000,125.310000,132.000000,131.310000,132.250000,133.880000,133.500000,135.500000,137.440000,138.690000,
            139.190000,138.500000,138.130000,137.500000,138.880000,132.130000,129.750000,128.500000,125.440000,125.120000,
            126.500000,128.690000,126.620000,126.690000,126.000000,123.120000,121.870000,124.000000,127.000000,124.440000,
            122.500000,123.750000,123.810000,124.500000,127.870000,128.560000,129.630000,124.870000,124.370000,124.870000,
            123.620000,124.060000,125.870000,125.190000,125.620000,126.000000,128.500000,126.750000,129.750000,132.690000,
            133.940000,136.500000,137.690000,135.560000,133.560000,135.000000,132.380000,131.440000,130.880000,129.630000,
            127.250000,127.810000,125.000000,126.810000,124.750000,122.810000,122.250000,121.060000,120.000000,123.250000,
            122.750000,119.190000,115.060000,116.690000,114.870000,110.870000,107.250000,108.870000,109.000000,108.500000,
            113.060000,93.000000,94.620000,95.120000,96.000000,95.560000,95.310000,99.000000,98.810000,96.810000,
            95.940000,94.440000,92.940000,93.940000,95.500000,97.060000,97.500000,96.250000,96.370000,95.000000,
            94.870000,98.250000,105.120000,108.440000,109.870000,105.000000,106.000000,104.940000,104.500000,104.440000,
            106.310000,112.870000,116.500000,119.190000,121.000000,122.120000,111.940000,112.750000,110.190000,107.940000,
            109.690000,111.060000,110.440000,110.120000,110.310000,110.440000,110.000000,110.750000,110.500000,110.500000,
            109.500000
        };

        private static readonly double[] Low =
        {
            90.750000,91.405000,94.250000,93.500000,92.815000,93.500000,92.000000,89.750000,89.440000,90.625000,92.750000,
            96.315000,96.030000,88.815000,86.750000,90.940000,88.905000,88.780000,89.250000,89.750000,87.500000,
            86.530000,84.625000,82.280000,81.565000,80.875000,81.250000,84.065000,85.595000,85.970000,84.405000,
            85.095000,85.500000,85.530000,87.875000,86.565000,84.655000,83.250000,82.565000,83.440000,82.530000,
            85.065000,86.875000,88.530000,89.280000,90.125000,90.750000,89.000000,88.565000,90.095000,89.000000,
            86.470000,84.000000,83.315000,82.000000,83.250000,84.750000,85.280000,87.190000,88.440000,88.250000,
            87.345000,89.280000,91.095000,89.530000,91.155000,92.000000,90.530000,89.970000,88.815000,86.750000,
            85.065000,82.030000,81.500000,82.565000,96.345000,96.470000,101.155000,104.250000,101.750000,101.720000,
            101.720000,103.155000,105.690000,103.655000,104.000000,105.530000,108.530000,108.750000,107.750000,117.000000,
            118.000000,116.000000,118.500000,116.530000,116.250000,114.595000,110.875000,110.500000,110.720000,112.620000,
            114.190000,111.190000,109.440000,111.560000,112.440000,117.500000,116.060000,116.560000,113.310000,112.560000,
            114.000000,114.750000,118.870000,119.000000,119.750000,122.620000,123.000000,121.750000,121.560000,123.120000,
            122.190000,122.750000,124.370000,128.000000,129.500000,130.810000,130.630000,132.130000,133.880000,135.380000,
            135.750000,136.190000,134.500000,135.380000,133.690000,126.060000,126.870000,123.500000,122.620000,122.750000,
            123.560000,125.810000,124.620000,124.370000,121.810000,118.190000,118.060000,117.560000,121.000000,121.120000,
            118.940000,119.810000,121.000000,122.000000,124.500000,126.560000,123.500000,121.250000,121.060000,122.310000,
            121.000000,120.870000,122.060000,122.750000,122.690000,122.870000,125.500000,124.250000,128.000000,128.380000,
            130.690000,131.630000,134.380000,132.000000,131.940000,131.940000,129.560000,123.750000,126.000000,126.250000,
            124.370000,121.440000,120.440000,121.370000,121.690000,120.000000,119.620000,115.500000,116.750000,119.060000,
            119.060000,115.060000,111.060000,113.120000,110.000000,105.000000,104.690000,103.870000,104.690000,105.440000,
            107.000000,89.000000,92.500000,92.120000,94.620000,92.810000,94.250000,96.250000,96.370000,93.690000,
            93.500000,90.000000,90.190000,90.500000,92.120000,94.120000,94.870000,93.000000,93.870000,93.000000,
            92.620000,93.560000,98.370000,104.440000,106.000000,101.810000,104.120000,103.370000,102.120000,102.250000,
            103.370000,107.940000,112.500000,115.440000,115.500000,112.250000,107.560000,106.560000,106.870000,104.500000,
            105.750000,108.620000,107.750000,108.060000,108.000000,108.190000,108.120000,109.060000,108.750000,108.560000,
            106.620000
        };

        private static readonly double[] Close =
        {
            91.500000,94.815000,94.375000,95.095000,93.780000,94.625000,92.530000,92.750000,90.315000,92.470000,96.125000,
            97.250000,98.500000,89.875000,91.000000,92.815000,89.155000,89.345000,91.625000,89.875000,88.375000,
            87.625000,84.780000,83.000000,83.500000,81.375000,84.440000,89.250000,86.375000,86.250000,85.250000,
            87.125000,85.815000,88.970000,88.470000,86.875000,86.815000,84.875000,84.190000,83.875000,83.375000,
            85.500000,89.190000,89.440000,91.095000,90.750000,91.440000,89.000000,91.000000,90.500000,89.030000,
            88.815000,84.280000,83.500000,82.690000,84.750000,85.655000,86.190000,88.940000,89.280000,88.625000,
            88.500000,91.970000,91.500000,93.250000,93.500000,93.155000,91.720000,90.000000,89.690000,88.875000,
            85.190000,83.375000,84.875000,85.940000,97.250000,99.875000,104.940000,106.000000,102.500000,102.405000,
            104.595000,106.125000,106.000000,106.065000,104.625000,108.625000,109.315000,110.500000,112.750000,123.000000,
            119.625000,118.750000,119.250000,117.940000,116.440000,115.190000,111.875000,110.595000,118.125000,116.000000,
            116.000000,112.000000,113.750000,112.940000,116.000000,120.500000,116.620000,117.000000,115.250000,114.310000,
            115.500000,115.870000,120.690000,120.190000,120.750000,124.750000,123.370000,122.940000,122.560000,123.120000,
            122.560000,124.620000,129.250000,131.000000,132.250000,131.000000,132.810000,134.000000,137.380000,137.810000,
            137.880000,137.250000,136.310000,136.250000,134.630000,128.250000,129.000000,123.870000,124.810000,123.000000,
            126.250000,128.380000,125.370000,125.690000,122.250000,119.370000,118.500000,123.190000,123.500000,122.190000,
            119.310000,123.310000,121.120000,123.370000,127.370000,128.500000,123.870000,122.940000,121.750000,124.440000,
            122.000000,122.370000,122.940000,124.000000,123.190000,124.560000,127.250000,125.870000,128.860000,132.000000,
            130.750000,134.750000,135.000000,132.380000,133.310000,131.940000,130.000000,125.370000,130.130000,127.120000,
            125.190000,122.000000,125.000000,123.000000,123.500000,120.060000,121.000000,117.750000,119.870000,122.000000,
            119.190000,116.370000,113.500000,114.250000,110.000000,105.060000,107.000000,107.870000,107.000000,107.120000,
            107.000000,91.000000,93.940000,93.870000,95.500000,93.000000,94.940000,98.250000,96.750000,94.810000,
            94.370000,91.560000,90.250000,93.940000,93.620000,97.000000,95.000000,95.870000,94.060000,94.620000,
            93.750000,98.000000,103.940000,107.870000,106.060000,104.500000,105.000000,104.190000,103.060000,103.420000,
            105.270000,111.870000,116.000000,116.620000,118.280000,113.370000,109.000000,109.700000,109.250000,107.000000,
            109.190000,110.000000,109.200000,110.120000,108.000000,108.620000,109.750000,109.810000,109.000000,108.750000,
            107.870000
        };

        private static readonly double[] Volume =
        {
            4077500,4955900,4775300,4155300,4593100,3631300,3382800,4954200,4500000,3397500,4204500,
            6321400,10203600,19043900,11692000,9553300,8920300,5970900,5062300,3705600,5865600,
            5603000,5811900,8483800,5995200,5408800,5430500,6283800,5834800,4515500,4493300,
            4346100,3700300,4600200,4557200,4323600,5237500,7404100,4798400,4372800,3872300,
            10750800,5804800,3785500,5014800,3507700,4298800,4842500,3952200,3304700,3462000,
            7253900,9753100,5953000,5011700,5910800,4916900,4135000,4054200,3735300,2921900,
            2658400,4624400,4372200,5831600,4268600,3059200,4495500,3425000,3630800,4168100,
            5966900,7692800,7362500,6581300,19587700,10378600,9334700,10467200,5671400,5645000,
            4518600,4519500,5569700,4239700,4175300,4995300,4776600,4190000,6035300,12168900,
            9040800,5780300,4320800,3899100,3221400,3455500,4304200,4703900,8316300,10553900,
            6384800,7163300,7007800,5114100,5263800,6666100,7398400,5575000,4852300,4298100,
            4900500,4887700,6964800,4679200,9165000,6469800,6792000,4423800,5231900,4565600,
            6235200,5225900,8261400,5912500,3545600,5714500,6653900,6094500,4799200,5050800,
            5648900,4726300,5585600,5124800,7630200,14311600,8793600,8874200,6966600,5525500,
            6515500,5291900,5711700,4327700,4568000,6859200,5757500,7367000,6144100,4052700,
            5849700,5544700,5032200,4400600,4894100,5140000,6610900,7585200,5963100,6045500,
            8443300,6464700,6248300,4357200,4774700,6216900,6266900,5584800,5284500,7554500,
            7209500,8424800,5094500,4443600,4591100,5658400,6094100,14862200,7544700,6985600,
            8093000,7590000,7451300,7078000,7105300,8778800,6643900,10563900,7043100,6438900,
            8057700,14240000,17872300,7831100,8277700,15017800,14183300,13921100,9683000,9187300,
            11380500,69447300,26673600,13768400,11371600,9872200,9450500,11083300,9552800,11108400,
            10374200,16701900,13741900,8523600,9551900,8680500,7151700,9673100,6264700,8541600,
            8358000,18720800,19683100,13682500,10668100,9710600,3113100,5682000,5763600,5340000,
            6220800,14680500,9933000,11329500,8145300,16644700,12593800,7138100,7442300,9442300,
            7123600,7680600,4839800,4775500,4008800,4533600,3741100,4084800,2685200,3438000,
            2870500
        };
        #endregion

        [TestMethod]
        public void ExportBb()
        {
            var scalarArray = new Scalar[Close.Length];
            var date = new DateTime(2011, 1, 1);
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                date = date.AddDays(1);
            var list = new List<string>
            {
                "export const dataOhlcvDaily: Ohlcv[] = ["
            };
            for (int i = 0; i < 252; ++i)
            {
                scalarArray[i] = new Scalar { Time = date, Value = Close[i] };
                // Javascript Date constructor accepts zero-based months.
                list.Add(Invariant($"    {{ time: new Date({date.Year}, {date.Month - 1}, {date.Day}), open: {Open[i]}, high: {High[i]}, low: {Low[i]}, close: {Close[i]}, volume: {Volume[i]} }},"));
                date = date.AddDays(1);
                while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    date = date.AddDays(1);
            }
            list.Add("];");
            File.WriteAllLines(Invariant($"{FilePrefix}_candlesticks.ts"), list);

            var parameters = new BollingerBands.Parameters();
            var target = new BollingerBands(parameters, new[] { Middle, StDev, Lower, Upper, PercentB, BandWidth, LowerUpper });
            var metadata = target.Metadata;
            var outputs = target.Update(scalarArray).ToList();

            list.Clear();
            // ReSharper disable once StringLiteralTypo
            list.Add("// tslint:disable:max-line-length");
            list.Add("");
            list.Add("export const metaBb: Object = {");
            list.Add("    name:'Test data', indicators:[");
            list.Add($"        {{kind:'{metadata.IndicatorType}', outputs:[");
            foreach (var m in metadata.Outputs)
            {
                list.Add($"            {{kind:{m.Kind}, type:'{m.Type}', name:'{m.Name}', description:'{m.Description}'}},");
            }
            list.Add("        ]}");
            list.Add("    ]");
            list.Add("};");
            list.Add("");
            list.Add("export const dataBb: Object[] = [");
            for (int i = 0; i < outputs.Count; ++i)
            {
                var comma3 = i == outputs.Count - 1 ? "" : ",";
                var ohlcv = new Ohlcv(scalarArray[i].Time, Open[i], High[i], Low[i], Close[i], Volume[i]);
                // Javascript Date constructor accepts zero-based months.
                var str = Invariant(
                    $"    {{ ohlcv: {{time:new Date({ohlcv.Time.Year},{ohlcv.Time.Month - 1},{ohlcv.Time.Day}),open:{ohlcv.Open},high:{ohlcv.High},low:{ohlcv.Low},close:{ohlcv.Close},volume:{ohlcv.Volume}}}, indicators:[");
                for (int k = 0; k < 1; ++k)
                {
                    var comma2 = k == 0 ? "" : ",";
                    str += "{outputs:[";
                    for (int j = 0; j < outputs[i].Outputs.Length; ++j)
                    {
                        var comma = j == outputs[i].Outputs.Length - 1 ? "" : ",";
                        var o = outputs[i].Outputs[j];
                        if (o is Scalar s)
                        {
                            str += Invariant($"{{value:{s.Value}}}{comma}");
                        }
                        else if (o is Band b)
                        {
                            str += Invariant($"{{lower:{b.FirstValue},upper:{b.SecondValue}}}{comma}");
                        }
                    }
                    str = str + "]}" + comma2;
                }
                str = str + "]}" + comma3;
                list.Add(str);
            }
            list.Add("};");
            // ReSharper disable once StringLiteralTypo
            list.Add("// tslint:enable:");
            list.Add("");
            File.WriteAllLines(Invariant($"{FilePrefix}_combined.ts"), list);

            for (int j = 0; j < 7; ++j)
            {
                list.Clear();
                // ReSharper disable once StringLiteralTypo
                list.Add("// tslint:disable:max-line-length");
                list.Add("");
                list.Add(Invariant($"// kind: {metadata.Outputs[j].Kind}"));
                list.Add(Invariant($"// name: {metadata.Outputs[j].Name}"));
                list.Add(Invariant($"// description: {metadata.Outputs[j].Description}"));

                if (j == 6)
                {
                    list.Add(Invariant($"export const data_{j}: Band[] = ["));
                    for (int i = 0; i < 252; ++i)
                    {
                        var band = (Band)outputs[i].Outputs[j];
                        // Javascript Date constructor accepts zero-based months.
                        list.Add(Invariant($"    {{ time: new Date({band.Time.Year}, {band.Time.Month - 1}, {band.Time.Day}), lower: {band.FirstValue}, upper: {band.SecondValue} }},"));
                    }
                }
                else
                {
                    list.Add($"export const data_{j}: Scalar[] = [");
                    for (int i = 0; i < 252; ++i)
                    {
                        var scalar = (Scalar)outputs[i].Outputs[j];
                        // Javascript Date constructor accepts zero-based months.
                        list.Add(Invariant($"    {{ time: new Date({scalar.Time.Year}, {scalar.Time.Month - 1}, {scalar.Time.Day}), value: {scalar.Value} }},"));
                    }
                }
                list.Add("];");
                // ReSharper disable once StringLiteralTypo
                list.Add("// tslint:enable:");
                list.Add("");
                File.WriteAllLines(Invariant($"{FilePrefix}_output_{j}.ts"), list);
            }
        }

        [TestMethod]
        public void Test()
        {
            var ohlcvArray = new Ohlcv[Close.Length];
            var date = new DateTime(2011, 1, 1);
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                date = date.AddDays(1);
            for (int i = 0; i < 252; ++i)
            {
                ohlcvArray[i] = new Ohlcv { Time = date, Open = Open[i], High = High[i], Low = Low[i], Close = Close[i], Volume = Volume[i] };
                // Javascript Date constructor accepts zero-based months.
                date = date.AddDays(1);
                while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    date = date.AddDays(1);
            }

            var parameters1 = new GoertzelSpectrum.Parameters { PeriodResolution = 10, MaxPeriod = 64 };
            var target1 = new GoertzelSpectrum(parameters1, new[] { 0 });
            var outputs = target1.Update(ohlcvArray).ToList();
            var length = ((HeatMap) outputs[^1].Outputs[0]).Values.Length;

            var res = target1.PeriodResolution;
            var min = Math.Min(target1.MinParameterValue, target1.MaxParameterValue);
            var max = Math.Max(target1.MinParameterValue, target1.MaxParameterValue);

            var plus = new double[length];
            var minus = new double[length];
            for (int i = 0; i < length; ++i)
            {
                plus[i] = min + i / res;
                minus[length - 1 - i] = max - i / res;
            }
            for (int i = 0; i < length; ++i)
            {
                var s = $"{i}, plus = {plus[i]}, minus = {minus[i]}";
                Console.WriteLine(s);
            }
        }

        [TestMethod]
        public void ExportGoertzelSpectrum()
        {
            var ohlcvArray = new Ohlcv[Close.Length];
            var date = new DateTime(2011, 1, 1);
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                date = date.AddDays(1);
            for (int i = 0; i < 252; ++i)
            {
                ohlcvArray[i] = new Ohlcv { Time = date, Open = Open[i], High = High[i], Low = Low[i], Close = Close[i], Volume = Volume[i] };
                date = date.AddDays(1);
                while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    date = date.AddDays(1);
            }

            var parameters = new GoertzelSpectrum.Parameters{ PeriodResolution = 10, MaxPeriod = 28, IsSpectralDilationCompensation = false };
            var target = new GoertzelSpectrum(parameters, new[] { 0, 1, 2, 3 });
            var metadata = target.Metadata;
            var outputs = target.Update(ohlcvArray).ToList();

            var list = new List<string>();
            for (int j = 0; j < 4; ++j)
            {
                list.Clear();
                // ReSharper disable once StringLiteralTypo
                list.Add("// tslint:disable:max-line-length");
                list.Add("");
                list.Add(Invariant($"// kind: {metadata.Outputs[j].Kind}"));
                list.Add(Invariant($"// name: {metadata.Outputs[j].Name}"));
                list.Add(Invariant($"// description: {metadata.Outputs[j].Description}"));

                list.Add($"export const data_{j}: HeatMap[] = [");
                for (int i = 0; i < outputs.Count; ++i)
                {
                    var heatMap = (HeatMap)outputs[i].Outputs[j];
                    // Javascript Date constructor accepts zero-based months.
                    var line = Invariant($"    {{ time: new Date({heatMap.Time.Year}, {heatMap.Time.Month - 1}, {heatMap.Time.Day}), parameterFirst: {heatMap.ParameterFirst}, parameterLast: {heatMap.ParameterLast}, parameterResolution: {heatMap.ParameterResolution}, valueMin: {heatMap.ValueMin}, valueMax: {heatMap.ValueMax}, ");

                    var array = "values: [";
                    if (heatMap.Values == null)
                        array += "]";
                    else
                    {
                        for (int k = 0; k < heatMap.Values.Length; ++k)
                        {
                            array += Invariant($"{heatMap.Values[k]}");
                            if (k < heatMap.Values.Length - 1)
                            {
                                array += ", ";
                            }
                        }
                        array += "]";
                    }

                    line += array;
                    line += "}";
                    if (i < outputs.Count - 1)
                        line += ",";

                    list.Add(line);
                }
                list.Add("];");
                // ReSharper disable once StringLiteralTypo
                list.Add("// tslint:enable:");
                list.Add("");
                File.WriteAllLines(Invariant($"{FilePrefix}_output_goertzel_{j}.ts"), list);
            }
        }

        [TestMethod]
        public void ExportBollingerBandsAndGoetrzel()
        {
            var ohlcvArray = new Ohlcv[Close.Length];
            var date = new DateTime(2011, 1, 1);
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                date = date.AddDays(1);
            for (int i = 0; i < 252; ++i)
            {
                ohlcvArray[i] = new Ohlcv { Time = date, Open = Open[i], High = High[i], Low = Low[i], Close = Close[i], Volume = Volume[i] };
                date = date.AddDays(1);
                while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    date = date.AddDays(1);
            }

            var inputs = new[]
            {
                new IndicatorInput
                {
                    IndicatorType = IndicatorType.BollingerBands,
                    Parameters = new BollingerBands.Parameters(),
                    OutputKinds = new[] { Middle, Lower, Upper, PercentB, BandWidth, LowerUpper }
                },
                new IndicatorInput
                {
                    IndicatorType = IndicatorType.GoertzelSpectrum,
                    Parameters = new GoertzelSpectrum.Parameters{ PeriodResolution = 10, MaxPeriod = 28, IsSpectralDilationCompensation = false },
                    OutputKinds = new[] { (int)GoertzelSpectrum.OutputKind.PowerSpectrumNormalizedToZeroOne }
                }
            };
            var indicators = IndicatorFactory.Create(inputs).ToArray();
            var metadata = new[]
            {
                indicators[0].Metadata,
                indicators[1].Metadata
            };
            var outputs = new List<IndicatorOutput[]>();
            foreach (var ohlcv in ohlcvArray)
            {
                var output = new[]
                {
                    indicators[0].Update(ohlcv),
                    indicators[1].Update(ohlcv)
                };
                outputs.Add(output);
            }

            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                MaxDepth = 1280,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            //options.Converters.Add(new NonLoopingJsonConverter());
            // options.Converters.Add(new HandleSpecialDoublesAsStrings());
            options.Converters.Add(new HandleSpecialDoublesAsStringsNewtonsoftCompat());

            string json = JsonSerializer.Serialize(metadata, options);
            File.WriteAllText("d:\\test-data-bb-goertzel-metadata.json", json);
            //json = JsonSerializer.Serialize(outputs, options);
            json = "System.Text.Json.JsonException: The object or value could not be serialized. Path: $.Outputs.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone.Clone. ---> System.InvalidOperationException: CurrentDepth (1000) is equal to or larger than the maximum allowed depth of 1000. Cannot write the next JSON object or array.";
            File.WriteAllText("d:\\test-data-bb-goertzel-outputs.json", json);
            /*using (FileStream fs = File.Create("d:\\test-data-bb-goertzel-combined.json"))
            {
                await JsonSerializer.SerializeAsync(fs, outputs);
            }*/

            var list = new List<string>
            {
                // ReSharper disable once StringLiteralTypo
                "// tslint:disable:max-line-length",
                "",
                "export const testDataMeta: Object[] = ["
            };
            for (int j = 0; j < metadata.Length; ++j)
            {
                var meta = metadata[j];
                list.Add($"  {{kind:'{meta.IndicatorType}', outputs:[");
                for (int i = 0; i < meta.Outputs.Length; ++i)
                {
                    var m = meta.Outputs[i];
                    var s = $"    {{kind:{m.Kind}, type:'{m.Type}', name:'{m.Name}', description:'{m.Description}'}}";
                    if (i < meta.Outputs.Length - 1)
                    {
                        s += ",";
                    }
                    list.Add(s);
                }
                list.Add(j == metadata.Length - 1 ? "  ]}" : "  ]},");
            }
            list.Add("];");
            list.Add("");
            list.Add("export const testDataOutputs: Object[] = [");
            for (int i = 0; i < outputs.Count; ++i)
            {
                var row = outputs[i];
                list.Add("  {");
                list.Add("    indicators: [");
                var lenInd = row.Length;
                var comma3 = i == outputs.Count - 1 ? "" : ",";
                for (int k = 0; k < lenInd; ++k)
                {
                    var indOut = row[k].Outputs;
                    var lenOut = indOut.Length;
                    list.Add("      {");
                    var comma2 = k == lenInd - 1 ? "" : ",";
                    list.Add("        outputs: [");
                    for (int j = 0; j < lenOut; ++j)
                    {
                        var comma = j == lenOut - 1 ? "" : ",";
                        var o = indOut[j];
                        var str = "          ";
                        switch (o)
                        {
                            case Scalar s:
                            {
                                var t = DateTimeToString(s.Time);
                                str += Invariant($"{{{t}, value:{s.Value}}}{comma}");
                                break;
                            }
                            case Band b:
                            {
                                var t = DateTimeToString(b.Time);
                                str += Invariant($"{{{t}, lower:{b.FirstValue}, upper:{b.SecondValue}}}{comma}");
                                break;
                            }
                            case HeatMap h:
                            {
                                var t = DateTimeToString(h.Time);
                                str += Invariant($"{{{t}, parameterFirst:{h.ParameterFirst}, parameterLast:{h.ParameterLast}, parameterResolution:{h.ParameterResolution}, valueMin:{h.ValueMin}, valueMax:{h.ValueMax}, ");
                                var array = "values:[";
                                if (h.Values == null)
                                    array += "]";
                                else
                                {
                                    var nLen = h.Values.Length;
                                    for (int n = 0; n < nLen; ++n)
                                    {
                                        array += Invariant($"{h.Values[n]}");
                                        if (n < nLen - 1)
                                        {
                                            array += ", ";
                                        }
                                    }
                                    array += "]";
                                }
                                str += array;
                                str += "}";
                                str += comma;
                                break;
                            }
                        }
                        list.Add(str);
                    }
                    list.Add("        ]");
                    list.Add($"      }}{comma2}");
                }
                list.Add("    ]");
                list.Add($"  }}{comma3}");
            }
            list.Add("];");
            // ReSharper disable once StringLiteralTypo
            list.Add("// tslint:enable:");
            list.Add("");
            File.WriteAllLines("d:\\test-data-bb-goertzel-combined.ts", list);
        }

        private static string DateTimeToString(DateTime dateTime)
        {
            // Javascript Date constructor accepts zero-based months.
            return Invariant($"time:new Date({dateTime.Year},{dateTime.Month - 1},{dateTime.Day})");
        }
    }

    internal class HandleSpecialDoublesAsStrings : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType == JsonTokenType.String ? double.Parse(reader.GetString()) : reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            if (double.IsFinite(value))
            {
                writer.WriteNumberValue(value);
            }
            else
            {
                writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
            }
        }
    }

    internal class HandleSpecialDoublesAsStringsNewtonsoftCompat : JsonConverter<double>
    {
        private static readonly JsonEncodedText Nan = JsonEncodedText.Encode("NaN");
        private static readonly JsonEncodedText PositiveInfinity = JsonEncodedText.Encode("Infinity");
        private static readonly JsonEncodedText NegativeInfinity = JsonEncodedText.Encode("-Infinity");

        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string specialDouble = reader.GetString();
                switch (specialDouble)
                {
                    case "Infinity":
                        return double.PositiveInfinity;
                    case "-Infinity":
                        return double.NegativeInfinity;
                    default:
                        return double.NaN;
                }
            }
            return reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            if (double.IsFinite(value))
            {
                writer.WriteNumberValue(value);
            }
            else
            {
                if (double.IsPositiveInfinity(value))
                {
                    writer.WriteStringValue(PositiveInfinity);
                }
                else if (double.IsNegativeInfinity(value))
                {
                    writer.WriteStringValue(NegativeInfinity);
                }
                else
                {
                    writer.WriteStringValue(Nan);
                }
            }
        }
    }

    public class NonLoopingJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => true;

        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        {

            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(NonLoopingConverterInner<>).MakeGenericType(new[] { type }),
                BindingFlags.Instance | BindingFlags.Public, binder: null, args: new object[] { }, culture: null);

            return converter;
        }

        private class NonLoopingConverterInner<TValue> : JsonConverter<TValue>
        {
            public override TValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return JsonSerializer.Deserialize<TValue>(ref reader, options);
            }

            public override void Write(Utf8JsonWriter writer, TValue value, JsonSerializerOptions options /*options ignored*/)
            {
                SafeJsonSerializer.Serialize(value, writer);
            }
        }
    }

    public static class SafeJsonSerializer
    {
        static readonly MethodInfo SerializeEnumerableMethod = typeof(SafeJsonSerializer).GetMethod("SerializeEnumerable", BindingFlags.Static | BindingFlags.NonPublic);

        public static void Serialize<T>(T obj, Utf8JsonWriter jw) => Serialize(obj, null, jw, new List<int> { });

        static void Serialize<T>(T obj, string propertyName, Utf8JsonWriter jw, List<int> hashCodes, bool isContainerType = false)
        {
            var jsonValueType = GetJsonValueKind(obj);
            if (jsonValueType == JsonValueKind.Array || jsonValueType == JsonValueKind.Object)
            {
                var hashCode = obj.GetHashCode();
                if (hashCodes.Contains(hashCode))
                    return;
                hashCodes.Add(hashCode);
            }

            if (isContainerType && jsonValueType == JsonValueKind.Null)
                return;

            if (propertyName != null)
            {
                jw.WritePropertyName(propertyName);
            }

            if (obj != null && obj.GetType().IsEnum)
            {
                jw.WriteStringValue(Enum.GetName(obj.GetType(), obj));
                return;
            }

            switch (jsonValueType)
            {
                case JsonValueKind.Undefined:
                    if (propertyName == null)
                        return;
                    jw.WriteNullValue();
                    break;
                case JsonValueKind.Null:
                    jw.WriteNullValue();
                    break;
                case JsonValueKind.True:
                    jw.WriteBooleanValue(true);
                    break;
                case JsonValueKind.False:
                    jw.WriteBooleanValue(false);
                    break;
                case JsonValueKind.String:
                    var result = JsonSerializer.Serialize(obj).Replace("\u0022", "");
                    jw.WriteStringValue(result);
                    break;
                case JsonValueKind.Number:
                    var num = Convert.ToDecimal(obj);
                    jw.WriteNumberValue(num);
                    break;
                case JsonValueKind.Array:
                    jw.WriteStartArray();
                    try
                    {
                        List<object> list = new List<object>();
                        if (typeof(IEnumerable).IsAssignableFrom(obj.GetType()))
                        {
                            IEnumerable items = (IEnumerable)obj;
                            foreach (var item in items)
                                list.Add(item);
                        }
                        else if (obj is IEnumerable<object>)
                            foreach (var item in obj as IEnumerable<object>)
                                list.Add(item);
                        else if (obj is IOrderedEnumerable<object>)
                            foreach (var item in obj as IOrderedEnumerable<object>)
                                list.Add(item);

                        SerializeEnumerable(list, jw, hashCodes);
                    }
                    catch
                    {

                        //upon failure, use reflection and generic SerializeEnumerable method
                        Type[] args = obj.GetType().GetGenericArguments();
                        Type itemType = args[0];

                        MethodInfo genericM = SerializeEnumerableMethod.MakeGenericMethod(itemType);
                        genericM.Invoke(null, new object[] { obj, propertyName, jw, hashCodes });
                    }
                    jw.WriteEndArray();
                    break;
                case JsonValueKind.Object:
                    jw.WriteStartObject();
                    var type = obj.GetType();
                    if (type.IsIDictionary())
                    {
                        var dict = obj as IDictionary;
                        foreach (var key in dict.Keys)
                            Serialize(dict[key], key.ToString(), jw, hashCodes);
                    }
                    else
                    {
                        foreach (var prop in type.GetProperties().Where(t => t.DeclaringType.FullName != "System.Linq.Dynamic.Core.DynamicClass"))
                        {
                            var containerType = IsContainerType(prop.PropertyType);
                            Serialize(prop.GetValue(obj), prop.Name, jw, hashCodes, containerType);
                        }
                    }
                    jw.WriteEndObject();
                    break;
                default:
                    return;
            }
        }

        static void SerializeEnumerable<T>(IEnumerable<T> obj, Utf8JsonWriter jw, List<int> hashCodes)
        {
            foreach (var item in obj)
                Serialize(item, null, jw, hashCodes);
        }

        static JsonValueKind GetJsonValueKind(object obj)
        {
            if (obj == null)
                return JsonValueKind.Null;
            var type = obj.GetType();
            if (type.IsArray)
                return JsonValueKind.Array;
            if (type.IsIDictionary())
                return JsonValueKind.Object;
            if (type.IsIEnumerable())
                return JsonValueKind.Array;
            if (type.IsNumber())
                return JsonValueKind.Number;
            if (type == typeof(bool))
            {
                var bObj = (bool)obj;
                return bObj ? JsonValueKind.True : JsonValueKind.False;
            }

            if (type == typeof(string) ||
                type == typeof(DateTime) ||
                type == typeof(DateTimeOffset) ||
                type == typeof(TimeSpan) ||
                type.IsPrimitive)
                return JsonValueKind.String;
            if ((type.GetProperties()?.Length ?? 0) > 0)
                return JsonValueKind.Object;
            return JsonValueKind.Undefined;
        }

        static bool IsContainerType(Type type)
        {
            if (type.IsArray)
                return true;
            if (type.IsIDictionary())
                return true;
            if (type.IsIEnumerable())
                return true;
            if (type.IsNumber())
                return false;
            if (type == typeof(bool))
            {
                return false;
            }

            if (type == typeof(string) ||
                type == typeof(DateTime) ||
                type == typeof(DateTimeOffset) ||
                type == typeof(TimeSpan) ||
                type.IsPrimitive)
                return false;
            if ((type.GetProperties()?.Length ?? 0) > 0)
                return true;
            if (type == typeof(object))
                return true;
            return false;
        }
    }

    internal static class TypeExtensions
    {
        internal static bool IsIEnumerable(this Type type)
        {
            return type != typeof(string) && type.GetInterfaces().Contains(typeof(IEnumerable));
        }
        internal static bool IsIDictionary(this Type type)
        {
            return
                type.GetInterfaces().Contains(typeof(IDictionary))
                || (type.IsGenericType && typeof(Dictionary<,>).IsAssignableFrom(type.GetGenericTypeDefinition()));
        }
        internal static bool IsNumber(this Type type)
        {
            return type == typeof(byte)
                || type == typeof(ushort)
                || type == typeof(short)
                || type == typeof(uint)
                || type == typeof(int)
                || type == typeof(ulong)
                || type == typeof(long)
                || type == typeof(decimal)
                || type == typeof(double)
                || type == typeof(float);
        }
    }
}
