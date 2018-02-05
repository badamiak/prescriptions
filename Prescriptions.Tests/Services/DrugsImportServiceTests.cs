using NUnit.Framework;
using Prescriptions.Services;
using System.Linq;
using System.Text;
using Shouldly;
using System.IO;
using Prescriptions.Tests.Properties;
using Prescriptions.API.Model.Drugs;

namespace Prescriptions.Tests.Services
{
    [TestFixture]
    public class DrugsImportServiceTest
    {
        private DrugsCollection result;
        private Drug testDrug;
        private FileInfo testFile;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            var service = new DrugsImportService();
            var testedLibraryDir = new FileInfo(this.GetType().Assembly.Location).Directory;

            this.testFile = new FileInfo(Path.GetTempFileName());
            using (var writer = this.testFile.OpenWrite())
            {
                var encoded = Encoding.UTF8.GetBytes(Resources.TestXml);
                writer.Write(encoded, 0, encoded.Length);
            }

            this.result = service.Import(this.testFile.FullName);
            this.testDrug = this.result.Drugs.Where(x => x.EAN == "5909990002306").First();
        }

        [OneTimeTearDown]
        public void TearDownFixture()
        {
            this.testFile.Delete();
        }

        [Test]
        public void ParsedSpecificDrug()
        {
            this.result.ShouldNotBeNull();
        }

        [Test]
        public void ParsedBL7()
        {
            this.testDrug.BL7.ShouldBe("8085922");
        }

        [Test]
        public void ParsedEAN()
        {
            this.testDrug.EAN.ShouldBe("5909990002306");
        }
        [Test]
        public void ParsedPsychotrope()
        {
            this.testDrug.Psychotrope.ShouldBe("False");
        }
        [Test]
        public void ParsedSenior()
        {
            this.testDrug.Senior.ShouldBe("True");
        }
        [Test]
        public void ParsedVaccine()
        {
            this.testDrug.Vaccine.ShouldBe("True");
        }
        [Test]
        public void ParsedPrice()
        {
            this.testDrug.Price.ShouldBe("123,45");
        }
        [Test]
        public void ParsedName()
        {
            this.testDrug.Name.ShouldBe("Test name");
        }
        [Test]
        public void ParsedInternationalName()
        {
            this.testDrug.InternationalName.ShouldBe("Test international name");
        }
        [Test]
        public void ParsedForm()
        {
            this.testDrug.Form.ShouldBe("Test form");
        }
        [Test]
        public void ParsedDosage()
        {
            this.testDrug.Dosage.ShouldBe("Test dosage");
        }
        [Test]
        public void ParsedPackaging()
        {
            this.testDrug.Packaging.ShouldBe("Test packaging");
        }
        [Test]
        public void ParsedRefundationsShouldContain4Objects()
        {
            this.testDrug.Refunds.ToList().Count.ShouldBe(4);
        }
        [TestCase(RefundLevel.Full, "full")]
        [TestCase(RefundLevel.LumpSum, "lump sum")]
        [TestCase(RefundLevel.FiftyPercent, "fifty")]
        [TestCase(RefundLevel.ThirtyPercent, "thirty")]
        public void ParsedRefundationFull(RefundLevel level, string description)
        {
            this.testDrug.Refunds.First(x => x.Level == level).Value.ShouldBe(description);
        }
    }
}
