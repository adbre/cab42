using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.C42A
{
    using global::C42A;

    [TestClass]
    public class ProgramOptionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNull()
        {
            ProgramOptions.Parse(null);
        }

        [TestMethod]
        public void StartWinForms()
        {
            var options = ProgramOptions.Parse(new string[0]);
            Assert.IsTrue(options.StartWindowsFormsApplication);
        }

        [TestMethod]
        public void StartWinForms2()
        {
            var options = ProgramOptions.Parse(new[] { "open" });
            Assert.IsTrue(options.StartWindowsFormsApplication);
        }

        [TestMethod]
        public void StartWinFormsWithFile()
        {
            var options = ProgramOptions.Parse(new[] { @"NonExistingFile.c42" });
            Assert.IsTrue(options.StartWindowsFormsApplication);
            Assert.AreEqual(@"NonExistingFile.c42", options.FileName);
        }

        [TestMethod]
        public void StartWinFormsWithFile2()
        {
            var options = ProgramOptions.Parse(new[] { "open", @"NonExistingFile.c42" });
            Assert.IsTrue(options.StartWindowsFormsApplication);
            Assert.AreEqual(@"NonExistingFile.c42", options.FileName);
        }

        [TestMethod]
        public void Build()
        {
            var options = ProgramOptions.Parse(new[] { "build", @"NonExistingFile.c42" });
            Assert.IsFalse(options.StartWindowsFormsApplication);
            Assert.AreEqual(@"NonExistingFile.c42", options.FileName);
        }

        [TestMethod]
        public void BuildWithProperties()
        {
            var options =
                ProgramOptions.Parse(new[] { "build", @"NonExistingFile.c42", "--set-variable", "Version", "1.1" });
            Assert.IsFalse(options.StartWindowsFormsApplication);
            Assert.AreEqual(@"NonExistingFile.c42", options.FileName);
            Assert.AreEqual(
                @"1.1", options.Variables.Where(v => v.Key == "Version").Select(v => v.Value).FirstOrDefault());
        }
    }
}
