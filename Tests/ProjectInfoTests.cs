namespace Tests
{
    using System;
    using System.Linq;

    using C42A;
    using C42A.CAB42;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProjectInfoTests
    {
        [TestMethod]
        public void SetProjectVersion()
        {
            var options = new ProgramOptions() { FileName = @"NonExistingFile.c42", };
            options.Variables.Add("Version", "1.2.3.4");

            var projectInfo = ProjectInfo.Open(options);

            Assert.AreEqual(new Version(1, 2, 3, 4), projectInfo.ProjectVersion);
        }

        [TestMethod]
        public void VersionDoesNotSetReleaseName()
        {
            var options = new ProgramOptions() { FileName = @"NonExistingFile.c42", };
            options.Variables.Add("Version", "v1.2.3.4-5-abc");

            var projectInfo = ProjectInfo.Open(options);

            Assert.AreEqual(new Version(1, 2, 3, 4), projectInfo.ProjectVersion);
            Assert.AreEqual(null, projectInfo.ReleaseName);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void VersionBadFormat()
        {
            var options = new ProgramOptions() { FileName = @"NonExistingFile.c42", };
            options.Variables.Add("Version", "a1b2c3e4f5g6h7i8j9k0");

            ProjectInfo.Open(options);
        }

        [TestMethod]
        public void VariableCreated()
        {
            var options = new ProgramOptions() { FileName = @"NonExistingFile.c42", };
            options.Variables.Add("Foo", "bar");

            var projectInfo = ProjectInfo.Open(options);

            Assert.AreEqual(
                "bar",
                projectInfo.GlobalUserVariables.Where(v => v.Key == "Foo").Select(v => v.Value.Value).FirstOrDefault());
        }

        [TestMethod]
        public void SysVariablesIsNotOverwrittenByUserVariable()
        {
            var options = new ProgramOptions() { FileName = @"NonExistingFile.c42", };
            options.Variables.Add("Version", "v2.1.3.0-14-ged5ff9d");

            var projectInfo = ProjectInfo.Open(options);
            var actual = projectInfo.ParseVariables(null, "$(Version)");
            Assert.AreEqual("2.1.3.0", actual);
        }
    }
}