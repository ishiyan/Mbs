using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators
{
    [TestClass]
    public class SyntheticDataGeneratorGenerationTests
    {
        private class MockSyntheticDataGenerator : ISyntheticDataGenerator<Scalar>
        {
            internal const double StartValue = 1.1;
            internal static readonly DateTime StartDateTime = new DateTime(2000, 1, 1);

            private DateTime dateTime = StartDateTime;
            private double value = StartValue;

            public string Name => "Mock name";

            public string Moniker => "Mock moniker";

            public Scalar GenerateNext()
            {
                dateTime = dateTime.AddDays(1);
                return new Scalar(dateTime, ++value);
            }

            public IEnumerable<Scalar> GenerateNext(int count)
            {
                while (count-- > 0)
                    yield return GenerateNext();
            }

            public void Reset()
            {
                dateTime = StartDateTime;
                value = StartValue;
            }
        }

        // ReSharper disable InconsistentNaming

        [TestMethod]
        public async Task SyntheticDataGeneratorGeneration_GenerateOutputAsync_ValidInput_CorrectOutput()
        {
            const int count = 3;
            var generator = new MockSyntheticDataGenerator();

            var result = await SyntheticDataGeneratorGeneration<Scalar>.GenerateOutputAsync(generator, count);

            Assert.AreEqual(generator.Name, result.Name, "name is correct");
            Assert.AreEqual(generator.Moniker, result.Moniker, "moniker is correct");

            Assert.IsNotNull(result.Data, "data is not null");
            Assert.AreEqual(count, result.Data.Length, "data length is correct");

            Assert.AreEqual(MockSyntheticDataGenerator.StartDateTime.AddDays(1), result.Data[0].Time, "data[0] time is correct");
            Assert.AreEqual(MockSyntheticDataGenerator.StartValue + 1, result.Data[0].Value, "data[0] value is correct");

            Assert.AreEqual(MockSyntheticDataGenerator.StartDateTime.AddDays(2), result.Data[1].Time, "data[1] time is correct");
            Assert.AreEqual(MockSyntheticDataGenerator.StartValue + 2, result.Data[1].Value, "data[1] value is correct");

            Assert.AreEqual(MockSyntheticDataGenerator.StartDateTime.AddDays(3), result.Data[2].Time, "data[2] time is correct");
            Assert.AreEqual(MockSyntheticDataGenerator.StartValue + 3, result.Data[2].Value, "data[2] value is correct");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SyntheticDataGeneratorGeneration_GenerateOutputAsync_CountIsZero_Exception()
        {
            await SyntheticDataGeneratorGeneration<Scalar>.GenerateOutputAsync(new MockSyntheticDataGenerator(), 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SyntheticDataGeneratorGeneration_GenerateOutputAsync_CountIsNegative_Exception()
        {
            await SyntheticDataGeneratorGeneration<Scalar>.GenerateOutputAsync(new MockSyntheticDataGenerator(), -1);
        }
    }
}
