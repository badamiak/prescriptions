using NUnit.Framework;
using Prescriptions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using System.IO;
using Prescriptions.Model.Drugs;

namespace Prescriptions.Tests.Services
{
    [TestFixture]
    public class DrugsImportServiceTest
    {
        private DrugsCollection result;
        private Drug testDrug;

        public DrugsImportServiceTest()
        {
            var service = new DrugsImportService();
            var testedLibraryDir = new FileInfo(this.GetType().Assembly.Location).Directory;
            var files = testedLibraryDir.GetFiles("drugsTest.xml", SearchOption.AllDirectories).First();

            this.result = service.Import(files.FullName);
            this.testDrug = this.result.Drugs.Where(x => x.EAN == "5909990002306").First();
        }

        [Test]
        public void ParsedSpecificDrug()
        {
            result.ShouldNotBeNull();
        }

        [Test]
        public void ParsedBL7()
        {
            testDrug.BL7.ShouldBe("8085922");
        }

        [Test]
        public void ParsedEAN()
        {
            testDrug.EAN.ShouldBe("5909990002306");
        }
        [Test]
        public void ParsedPsychotrope()
        {
            testDrug.Psychotrope.ShouldBe("False");
        }
        [Test]
        public void ParsedSenior()
        {
            testDrug.Senior.ShouldBe("True");
        }
        [Test]
        public void ParsedVaccine()
        {
            testDrug.Vaccine.ShouldBe("True");
        }
        [Test]
        public void ParsedPrice()
        {
            testDrug.Price.ShouldBe("123,45");
        }
        [Test]
        public void ParsedName()
        {
            testDrug.Name.ShouldBe("Test name");
        }
        [Test]
        public void ParsedInternationalName()
        {
            testDrug.InternationalName.ShouldBe("Test international name");
        }
        [Test]
        public void ParsedForm()
        {
            testDrug.Form.ShouldBe("Test form");
        }
        [Test]
        public void ParsedDosage()
        {
            testDrug.Dosage.ShouldBe("Test dosage");
        }
        [Test]
        public void ParsedPackaging()
        {
            testDrug.Packaging.ShouldBe("Test packaging");
        }
        [Test]
        public void ParsedRefundationsShouldContain4Objects()
        {
            testDrug.Refunds.Count.ShouldBe(4);
        }
        [TestCase(RefundLevel.Full, "full")]
        [TestCase(RefundLevel.LumpSum, "lump sum")]
        [TestCase(RefundLevel.FiftyPercent, "fifty")]
        [TestCase(RefundLevel.ThirtyPercent, "thirty")]
        public void ParsedRefundationFull(RefundLevel level, string description)
        {
            testDrug.Refunds.First(x => x.Level == level).Value.ShouldBe(description);
        }
    }
}
