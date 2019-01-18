using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PostcodeParser.Test
{
    [TestClass]
    public class PostcodeTests
    {
        [TestMethod]
        public void Postcode_Test()
        {
            foreach (var test in GetTestData())
            {
                var postcode = new Postcode(test.Postcode);
                Assert.AreEqual(test.OutwardCode, postcode.OutwardCode);
                Assert.AreEqual(test.Area, postcode.Area);
                Assert.AreEqual(test.District, postcode.District);
                Assert.AreEqual(test.InwardCode, postcode.InwardCode);
                Assert.AreEqual(test.Sector, postcode.Sector);
                Assert.AreEqual(test.Unit, postcode.Unit);
                Assert.AreEqual(test.IsValid, postcode.IsValid);
                Assert.AreEqual(test.Normalized, postcode.ToString());
            }

        }
        private static IEnumerable<PostcodeTestDto> GetTestData()
        {
            return new List<PostcodeTestDto>
            {
                new PostcodeTestDto
                {
                    Postcode = "EC",
                    OutwardCode = string.Empty,
                    Area = string.Empty,
                    District = string.Empty,
                    InwardCode = string.Empty,
                    Sector = string.Empty,
                    Unit = string.Empty,
                    IsValid = false,
                    Normalized = string.Empty
                },
                new PostcodeTestDto
                {
                    Postcode = "EC1A 1",
                    OutwardCode = "EC1A",
                    Area = "EC",
                    District = "1A",
                    InwardCode = string.Empty,
                    Sector = string.Empty,
                    Unit = string.Empty,
                    IsValid = true,
                    Normalized = "EC1A"
                },
                new PostcodeTestDto
                {
                    Postcode = string.Empty,
                    OutwardCode = string.Empty,
                    Area = string.Empty,
                    District = string.Empty,
                    InwardCode = string.Empty,
                    Sector = string.Empty,
                    Unit = string.Empty,
                    IsValid = false,
                    Normalized = string.Empty
                },
                new PostcodeTestDto
                {
                    Postcode = "W1A",
                    OutwardCode = "W1A",
                    Area = "W",
                    District = "1A",
                    InwardCode = string.Empty,
                    Sector = string.Empty,
                    Unit = string.Empty,
                    IsValid = true,
                    Normalized = "W1A"
                },
                new PostcodeTestDto
                {
                    Postcode = "EC1A1BB",
                    OutwardCode = "EC1A",
                    Area = "EC",
                    District = "1A",
                    InwardCode = "1BB",
                    Sector = "EC1A 1",
                    Unit = "BB",
                    IsValid = true,
                    Normalized = "EC1A 1BB"
                },
                new PostcodeTestDto
                {
                    Postcode = "w1A 0ax",
                    OutwardCode = "W1A",
                    Area = "W",
                    District = "1A",
                    InwardCode = "0AX",
                    Sector = "W1A 0",
                    Unit = "AX",
                    IsValid = true,
                    Normalized = "W1A 0AX"
                },
                new PostcodeTestDto
                {
                    Postcode = "M 1 1 A E",
                    OutwardCode = "M1",
                    Area = "M",
                    District = "1",
                    InwardCode = "1AE",
                    Sector = "M1 1",
                    Unit = "AE",
                    IsValid = true,
                    Normalized = "M1 1AE",
                },
                new PostcodeTestDto
                {
                    Postcode = "b 33 8 th",
                    OutwardCode = "B33",
                    Area = "B",
                    District = "33",
                    InwardCode = "8TH",
                    Sector = "B33 8",
                    Unit = "TH",
                    IsValid = true,
                    Normalized = "B33 8TH",
                },
                new PostcodeTestDto
                {
                    Postcode = "cr26xh",
                    OutwardCode = "CR2",
                    Area = "CR",
                    District = "2",
                    InwardCode = "6XH",
                    Sector = "CR2 6",
                    Unit = "XH",
                    IsValid = true,
                    Normalized = "CR2 6XH",
                },
                new PostcodeTestDto
                {
                    Postcode = "dn55 1PT",
                    OutwardCode = "DN55",
                    Area = "DN",
                    District = "55",
                    InwardCode = "1PT",
                    Sector = "DN55 1",
                    Unit = "PT",
                    IsValid = true,
                    Normalized = "DN55 1PT",
                }
            };
        }
    }
}
