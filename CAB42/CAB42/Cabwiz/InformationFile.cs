//-----------------------------------------------------------------------
// <copyright file="InformationFile.cs" company="42A Consulting">
//     Copyright 2011 42A Consulting
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//     
//     http://www.apache.org/licenses/LICENSE-2.0
//     
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
// </copyright>
//-----------------------------------------------------------------------
namespace C42A.CAB42.Cabwiz
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A class to build a Smart Device CABWIZ information file.
    /// </summary>
    /// <remarks>
    ///     <para>The specification for the output file can be found on MSDN http://msdn.microsoft.com/en-us/library/aa448654.aspx </para>
    /// </remarks>
    public class InformationFile
    {
        public const string DefaultExtension = ".inf";

        private InformationFileSection[] builtInSections;
        private string fileName;

        public InformationFile(string appName)
        {
            if (string.IsNullOrEmpty(appName))
            {
                throw new ArgumentNullException("appName");
            }

            this.Encoding = Encoding.UTF8;

            this.Version = new VersionSection();
            this.CEStrings = new CEStringsSection();
            this.Strings = new StringsSection();
            this.CEDevice = new CEDeviceSection();
            this.DefaultInstallation = new DefaultInstallSection();
            this.SourceDiskNames = new SourceDisksNamesSection();
            this.SourceDiskFiles = new SourceDisksFileSection();
            this.DestinationDir = new DestinationDirSection();
            this.AddReg = new AddRegSection(this.DefaultInstallation.AddReg);

            this.builtInSections = new InformationFileSection[] {
                this.Version,
                this.CEStrings,
                this.Strings,
                this.CEDevice,
                this.DefaultInstallation,
                this.SourceDiskNames,
                this.SourceDiskFiles,
                this.DestinationDir,
                this.AddReg
            };

            this.Sections = new List<InformationFileSection>(this.builtInSections);
            this.CopyFileSections = new List<CopyFileListSection>();
            this.ShortcutSections = new List<CEShortcutsSection>();

            this.CEStrings.AppName = appName;
            this.FileName = appName;
        }

        public InformationFile(string appName, Version version)
            : this(appName)
        {
            this.ApplicationVersion = version;
            this.FileName = string.Format("{0}-{1}", appName, version);
        }

        public InformationFile(string appName, string version)
            : this(appName, System.Version.Parse(version))
        {
            // nothing
        }

        public Version ApplicationVersion { get; private set; }

        public string FileName
        {
            get
            {
                return this.fileName;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }

                var fi = new System.IO.FileInfo(value);

                if (string.IsNullOrEmpty(fi.Extension))
                {
                    fi = new System.IO.FileInfo(string.Concat(value, DefaultExtension));
                }

                this.fileName = fi.FullName;
            }
        }

        public Encoding Encoding { get; set; }

        public VersionSection Version { get; private set; }

        public CEStringsSection CEStrings { get; private set; }

        public StringsSection Strings { get; private set; }

        public CEDeviceSection CEDevice { get; private set; }

        public DefaultInstallSection DefaultInstallation { get; private set; }

        public SourceDisksNamesSection SourceDiskNames { get; private set; }

        public SourceDisksFileSection SourceDiskFiles { get; private set; }

        public DestinationDirSection DestinationDir { get; private set; }

        public List<CopyFileListSection> CopyFileSections { get; private set; }

        public AddRegSection AddReg { get; private set; }

        public List<CEShortcutsSection> ShortcutSections { get; private set; }

        public List<InformationFileSection> Sections { get; private set; }

        public void AddFile(string fileName)
        {
            this.AddFile(fileName, null, null);
        }

        public void AddFile(string fileName, string subDir)
        {
            this.AddFile(fileName, subDir, null);
        }

        public void AddFile(string fileName, string subDir, string destinationFileName)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);

            if (!fileInfo.Exists)
            {
                throw new System.IO.FileNotFoundException("The specified file was not found", fileName);
            }

            var copyFilesSectionName = string.Format("Files.Common{0}", this.CopyFileSections.Count + 1);

            int sourceDisk = this.SourceDiskNames.Add(fileInfo.DirectoryName);
            
            this.SourceDiskFiles.Add(fileInfo.Name, sourceDisk);

            this.DestinationDir.Add(copyFilesSectionName, subDir);

            var copyFiles = new CopyFileListSection(copyFilesSectionName, destinationFileName, fileInfo.Name);

            this.CopyFileSections.Add(copyFiles);
            this.DefaultInstallation.CopyFiles.Add(copyFilesSectionName);
        }

        public CEShortcutItem AddShortcutToFile(string targetFileName, string shortcutFilename, string destinationFolder)
        {
            CEShortcutsSection shortcuts = null;

            if (this.ShortcutSections.Count == 0)
            {
                shortcuts = new CEShortcutsSection("Shortcuts");

                this.ShortcutSections.Add(shortcuts);
            }
            else
            {
                shortcuts = this.ShortcutSections[0];
            }

            return shortcuts.AddShortcutToFile(shortcutFilename, targetFileName, destinationFolder);
        }

        public CEShortcutItem AddStartMenuShortcutToFile(string targetFileName, string shortcutFileName, string startMenuSubDirectory = null)
        {
            string path = SpecialFolderMacros.StartMenu;

            if (!string.IsNullOrEmpty(startMenuSubDirectory))
            {
                path = System.IO.Path.Combine(path, startMenuSubDirectory);
            }

            return this.AddShortcutToFile(targetFileName, shortcutFileName, path);
        }

        public void WriteInformationFile()
        {
            var fileInfo = new System.IO.FileInfo(this.FileName);

            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            foreach (var shortcutSection in this.ShortcutSections)
            {
                this.DefaultInstallation.CEShortcuts.Add(shortcutSection.SectionName);
                this.DestinationDir.Add(shortcutSection.SectionName, SpecialFolderMacros.StartMenu);
            }

            using (var s = System.IO.File.Open(this.FileName, System.IO.FileMode.Create))
            {
                this.WriteSections(s, this.Encoding, this.Sections);
                this.WriteSections(s, this.Encoding, this.CopyFileSections);
                this.WriteSections(s, this.Encoding, this.ShortcutSections);

                s.Flush();
            }
        }

        internal static void WriteLine(System.IO.Stream s, Encoding encoding)
        {
            WriteLine(s, encoding, null);
        }

        internal static void WriteLine(System.IO.Stream s, Encoding encoding, string value)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            if (!string.IsNullOrEmpty(value))
            {
                value = string.Concat(value, Environment.NewLine);
            }
            else
            {
                value = Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(value))
            {
                var bytes = encoding.GetBytes(value);

                s.Write(bytes, 0, bytes.Length);
            }
        }

        private void WriteSections(System.IO.Stream s, Encoding encoding, IEnumerable<InformationFileSection> sections)
        {
            if (sections != null)
            {
                foreach (var section in sections)
                {
                    section.WriteSection(s, encoding);

                    WriteLine(s, encoding);
                }
            }
        }
    }
}
