using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.C42A.CSharp
{
    using global::C42A.CSharp;

    [TestClass]
    public class VersionHelperTests
    {
        [TestMethod]
        public void FromGitDescription1()
        {
            Version actual;

            actual = VersionHelper.ParseGitDescription("v2-14-deadbee");
            Assert.AreEqual(new Version(2, 0, 0, 0), actual);
            actual = VersionHelper.ParseGitDescription("v2.1-14-deadbee");
            Assert.AreEqual(new Version(2, 1, 0, 0), actual);
            actual = VersionHelper.ParseGitDescription("v2.1.3-14-deadbee");
            Assert.AreEqual(new Version(2, 1, 3, 0), actual);
            actual = VersionHelper.ParseGitDescription("v2.1.3.4-14-deadbee");
            Assert.AreEqual(new Version(2, 1, 3, 4), actual);

            actual = VersionHelper.ParseGitDescription("v2");
            Assert.AreEqual(new Version(2, 0, 0, 0), actual);
            actual = VersionHelper.ParseGitDescription("v2.1");
            Assert.AreEqual(new Version(2, 1, 0, 0), actual);
            actual = VersionHelper.ParseGitDescription("v2.1.3");
            Assert.AreEqual(new Version(2, 1, 3, 0), actual);
            actual = VersionHelper.ParseGitDescription("v2.1.3.4");
            Assert.AreEqual(new Version(2, 1, 3, 4), actual);

            actual = VersionHelper.ParseGitDescription("2-14-deadbee");
            Assert.AreEqual(new Version(2, 0, 0, 0), actual);
            actual = VersionHelper.ParseGitDescription("2.1-14-deadbee");
            Assert.AreEqual(new Version(2, 1, 0, 0), actual);
            actual = VersionHelper.ParseGitDescription("2.1.3-14-deadbee");
            Assert.AreEqual(new Version(2, 1, 3, 0), actual);
            actual = VersionHelper.ParseGitDescription("2.1.3.4-14-deadbee");
            Assert.AreEqual(new Version(2, 1, 3, 4), actual);

            actual = VersionHelper.ParseGitDescription("2");
            Assert.AreEqual(new Version(2, 0, 0, 0), actual);
            actual = VersionHelper.ParseGitDescription("2.1");
            Assert.AreEqual(new Version(2, 1, 0, 0), actual);
            actual = VersionHelper.ParseGitDescription("2.1.3");
            Assert.AreEqual(new Version(2, 1, 3, 0), actual);
            actual = VersionHelper.ParseGitDescription("2.1.3.4");
            Assert.AreEqual(new Version(2, 1, 3, 4), actual);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void FromGitDescription_ArgumentNull()
        {
            VersionHelper.ParseGitDescription(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void FromGitDescription_ArgumentEmpty()
        {
            VersionHelper.ParseGitDescription(string.Empty);
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void FromGitDescription_BadFormat()
        {
            VersionHelper.ParseGitDescription("a.b.c.d");
        }
    }
}
