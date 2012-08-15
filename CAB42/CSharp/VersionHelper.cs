// -----------------------------------------------------------------------
// <copyright file="VersionHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace C42A.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class VersionHelper
    {
        public static Version ParseGitDescription(string input)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentException("Input must not be empty or contain only whitespaces.", "input");

            var match = Regex.Match(input, @"(?<major>[0-9]+)(\.(?<minor>[0-9]+)(\.(?<build>[0-9]+)(\.(?<revision>[0-9]+))?)?)?(\-(?<releaseName>.*))?$");
            if (match.Success)
            {
                var invariant = CultureInfo.InvariantCulture;
                return new Version(
                    Convert.ToInt32(match.Groups["major"].Value, invariant),
                    match.Groups["minor"].Success ? Convert.ToInt32(match.Groups["minor"].Value, invariant) : 0,
                    match.Groups["build"].Success ? Convert.ToInt32(match.Groups["build"].Value, invariant) : 0,
                    match.Groups["revision"].Success ? Convert.ToInt32(match.Groups["revision"].Value, invariant) : 0
                    );
            }
            else
            {
                Version version;
                if (!Version.TryParse(input, out version)) throw new FormatException(string.Format("Could not extract version information from input: {0}", input));
                return version;
            }
        }
    }
}
