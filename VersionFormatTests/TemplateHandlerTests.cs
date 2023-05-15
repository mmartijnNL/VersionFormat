using NUnit.Framework;
using System.Collections.Generic;
using System;
using VersionFormat;

namespace VersionFormatTests
{
    internal struct TestData
    {
        public string input;
        public string expectedResult;
    }

    internal class TemplateHandlerTests
    {
        [Theory]
        [TestCaseSource(nameof(Cases))]
        public void GetVersionFromTemplateTest(TestData data)
        {
            var result = TemplateHandler.GetVersionFromTemplate(data.input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(data.expectedResult));
        }

        private static IEnumerable<TestData> Cases()
        {
            yield return new TestData { 
                input = "{yyyy}", 
                expectedResult = DateTime.Now.Year.ToString() 
            };
            yield return new TestData { 
                input = "{major}.{minor}.{build}-CI-{yyyyMMdd}-{HHmmss}", 
                expectedResult = $"1.0.0-CI-{DateTime.Now:yyyyMMdd}-{DateTime.Now:HHmmss}" 
            };
            yield return new TestData { 
                input = "{major}.{minor}.{build}-CI-{-DSTyyyyMMdd}-{-DSTHHmmss}", 
                expectedResult = $"1.0.0-CI-{(DateTime.Now.ToUniversalTime() + TimeZoneInfo.Local.BaseUtcOffset):yyyyMMdd}-{(DateTime.Now.ToUniversalTime() + TimeZoneInfo.Local.BaseUtcOffset):HHmmss}" 
            };
        }
    }
}