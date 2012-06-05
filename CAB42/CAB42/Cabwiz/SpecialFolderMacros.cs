//-----------------------------------------------------------------------
// <copyright file="SpecialFolderMacros.cs" company="42A Consulting">
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
    /// <summary>
    /// Provides a collection of macro strings used for special folders on Windows CE used when creating .inf files.
    /// </summary>
    /// <remarks>
    ///     These macro strings are documented on MSDN on http://msdn.microsoft.com/en-us/library/ms924764.aspx
    /// </remarks>
    public static class SpecialFolderMacros
    {
        /// <summary>
        /// Gets the macro string for the Fonts Folder, for example "Windows\Fonts".
        /// </summary>
        public const string Fonts = "%CE15%";

        /// <summary>
        /// Gets the macro string for the Start Menu Games Folder, for example "Windows\Start Menu\Games".
        /// </summary>
        public const string Games = "%CE14%";

        /// <summary>
        /// Gets the macro string for the Games Files Folder, for example "Program Files\Games".
        /// </summary>
        public const string GamesFiles = "%CE8%";

        /// <summary>
        /// Gets the macro string for the My Documents Folder, for example "My Documents".
        /// </summary>
        public const string MyDocuments = "%CE5%";

        /// <summary>
        /// Gets the macro string for the Program Files Folder, for example "Program Files".
        /// </summary>
        public const string ProgramFiles = "%CE1%";

        /// <summary>
        /// Gets the macro string for the Start Menu Programs Folder, for example "Windows\Start Menu\Programs".
        /// </summary>
        public const string Programs = "%CE11%";

        /// <summary>
        /// Gets the macro string for the Start Menu Folder, for example "Windows\Start Menu".
        /// </summary>
        public const string StartMenu = "%CE17%";

        /// <summary>
        /// Gets the macro string for the Startup Folder, for example "Windows\StartUp".
        /// </summary>
        public const string Startup = "%CE4%";

        /// <summary>
        /// Gets the macro string for the Windows Folder, for example "Windows".
        /// </summary>
        public const string Windows = "%CE2%";

        /// <summary>
        /// Gets a empty string (not yet supported).
        /// </summary>
        public const string GlobalAssemblyCache = null;

        /// <summary>
        /// Gets the macro string for the Installation directory.
        /// </summary>
        public const string InstallationDirectory = "%InstallDir%";
    }
}
