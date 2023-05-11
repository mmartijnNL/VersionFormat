using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VersionFormat;

namespace VersionFormatTests
{
    internal class StringExtensionsTests
    {
        [Theory]
        [TestCase("not a sections {section 1}.{section 2};", new string[] { "{section 1}", "{section 2}" })]
        [TestCase("test{abc}xyz", new string[] { "{abc}" })]
        public void StringSectionsTests(string input, string[] expectedResult)
        {
            var result = input.StringSections();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expectedResult.Length));
            if (expectedResult.Length > 0)
            {
                Assert.That(result.First, Is.EqualTo(expectedResult.First()));
                Assert.That(result.Last, Is.EqualTo(expectedResult.Last()));
            }
        }
    }
}
