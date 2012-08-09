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
        [TestClass]
        public class Open : ResourceTests
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
            public void SetProjectVersionAndReleaseName()
            {
                var options = new ProgramOptions() { FileName = @"NonExistingFile.c42", };
                options.Variables.Add("Version", "v1.2.3.4-5-abc");

                var projectInfo = ProjectInfo.Open(options);

                Assert.AreEqual(new Version(1, 2, 3, 4), projectInfo.ProjectVersion);
                Assert.AreEqual("v1.2.3.4-5-abc", projectInfo.ReleaseName);
            }

            [TestMethod]
            public void GitSha1()
            {
                var options = new ProgramOptions() { FileName = @"NonExistingFile.c42", };
                options.Variables.Add("Version", "a1b2c3e4f5g6h7i8j9k0");

                var projectInfo = ProjectInfo.Open(options);

                Assert.AreEqual(null, projectInfo.ProjectVersion);
                Assert.AreEqual("a1b2c3e4f5g6h7i8j9k0", projectInfo.ReleaseName);
            }

            [TestMethod]
            public void VariableCreated()
            {
                var options = new ProgramOptions() { FileName = @"NonExistingFile.c42", };
                options.Variables.Add("Foo", "bar");

                var projectInfo = ProjectInfo.Open(options);

                Assert.AreEqual("bar", projectInfo.GlobalUserVariables.Where(v => v.Key == "Foo").Select(v => v.Value.Value).FirstOrDefault());
            }
        }
    }
}