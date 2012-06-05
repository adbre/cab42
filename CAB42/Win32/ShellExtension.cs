//-----------------------------------------------------------------------
// <copyright file="ShellExtension.cs" company="42A Consulting">
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
namespace C42A.Win32
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Win32;

    /// <summary>
    /// Provides a class used to create a shell extension in Microsoft Explorer.
    /// </summary>
    public class ShellExtension
    {
        /// <summary>
        /// The class root
        /// </summary>
        private const string ClassesRoot = @"Software\Classes";

        /// <summary>
        /// the registry key for the Open command
        /// </summary>
        private const string OpenCommandKey = @"shell\open\command";

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellExtension"/> class.
        /// </summary>
        /// <param name="className">The name of the shell class.</param>
        public ShellExtension(string className)
        {
            this.ClassName = className;

            this.OpenCommand = CreateOpenCommand();
        }

        /// <summary>
        /// Gets the name of the shell class.
        /// </summary>
        public string ClassName { get; private set; }

        /// <summary>
        /// Gets or sets the open command for this shell class.
        /// </summary>
        public string OpenCommand { get; set; }
        
        /// <summary>
        /// Creates a commonly used open command for a application.
        /// </summary>
        /// <param name="appName">The absolute path to the application's executable.</param>
        /// <returns>A formatted open command string.</returns>
        /// <remarks>
        ///     <para>
        ///         This method will return a command line arguments string in two parts, 
        ///         the first part being the value of <paramref name="appName"/> (enclosed within double quotations).
        ///         The second part will be %1, also enclosed within double quotations, representing the filename when 
        ///         clicking on a item in Explorer.
        ///         The parts is separated with a single space character.
        ///     </para>
        /// </remarks>
        public static string CreateOpenCommand(string appName = null)
        {
            if (appName == null)
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();

                appName = assembly.Location;
            }

            return string.Format(@"""{0}"" ""%1""", appName);
        }

        /// <summary>
        /// Saves the open command for this shell class in the registry.
        /// </summary>
        /// <returns>A value indicating whether the value was successfully saved or not.</returns>
        public bool Save()
        {
            var info = new ClassInformation(this.ClassName)
            {
                DefaultOpenCommand = this.OpenCommand
            };

            return SetClassInformation(info);
        }

        /// <summary>
        /// Determines whether the current <see cref="OpenCommand"/> equals to the value stored in the registry for this class.
        /// </summary>
        /// <returns>A value indicating whether the current opencommand is the one saved in the registry.</returns>
        public bool IsDefaultApplication()
        {
            if (!string.IsNullOrEmpty(this.OpenCommand))
            {
                var info = GetClassInformation(this.ClassName);

                if (!string.IsNullOrEmpty(info.DefaultOpenCommand))
                {
                    if (this.OpenCommand.Equals(info.DefaultOpenCommand, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a <see cref="RegistryKey"/> for a shell class.
        /// </summary>
        /// <param name="className">The name of the shell class.</param>
        /// <param name="create">A value indicating whether the registry key should be created if it already doesn't exist.</param>
        /// <returns>A <see cref="RegistryKey"/>.</returns>
        private static RegistryKey GetClassKey(string className, bool create)
        {
            var softwareClasses = Registry.CurrentUser.OpenSubKey(ClassesRoot, create);

            if (softwareClasses == null)
            {
                throw new InvalidOperationException("Could not open the classes root");
            }

            if (create)
            {
                softwareClasses.DeleteSubKeyTree(className, false);
            }

            var classKey = softwareClasses.OpenSubKey(className, create);

            if (classKey == null && create)
            {
                classKey = softwareClasses.CreateSubKey(className);
            }

            return classKey;
        }

        /// <summary>
        /// Reads class information from the registry.
        /// </summary>
        /// <param name="className">The name of the shell class to get information about.</param>
        /// <returns>A <see cref="ClassInformation"/>.</returns>
        private static ClassInformation GetClassInformation(string className)
        {
            var info = new ClassInformation(className);

            var classKey = GetClassKey(className, false);

            if (classKey != null)
            {
                var commandKey = classKey.OpenSubKey(OpenCommandKey, false);

                if (commandKey != null)
                {
                    var commandValue = commandKey.GetValue(string.Empty);

                    if (commandValue != null)
                    {
                        info.DefaultOpenCommand = commandValue.ToString();
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// Writes to the registry the specified class information.
        /// </summary>
        /// <param name="info">The information to write.</param>
        /// <returns>A value indicating success.</returns>
        private static bool SetClassInformation(ClassInformation info)
        {
            var classKey = GetClassKey(info.ClassName, true);

            if (classKey == null)
            {
                throw new InvalidOperationException("Failed to open a writable instance of the class key");
            }

            classKey.SetValue(string.Empty, string.Format("{0} File", info.ClassName));

            if (!string.IsNullOrEmpty(info.DefaultOpenCommand))
            {
                if (!SetShellOpenCommand(classKey, info.DefaultOpenCommand))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Writes the open command to the specified class.
        /// </summary>
        /// <param name="classKey">A <see cref="RegistryKey"/> representing the shell class.</param>
        /// <param name="command">The command to set.</param>
        /// <returns>A value indicating success.</returns>
        private static bool SetShellOpenCommand(RegistryKey classKey, string command)
        {
            // this first call should always return null, as the entire class tree has been deleted
            // when we called the 'GetClassKey' method with the 'create' parameter set to TRUE
            var commandKey = classKey.OpenSubKey(OpenCommandKey, true);

            if (commandKey == null)
            {
                commandKey = classKey.CreateSubKey(OpenCommandKey);
            }

            if (commandKey != null)
            {
                commandKey.SetValue(string.Empty, command, RegistryValueKind.String);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Describes a shell class.
        /// </summary>
        private class ClassInformation
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ClassInformation"/> class.
            /// </summary>
            public ClassInformation()
            {
                // nothing
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ClassInformation"/> class.
            /// </summary>
            /// <param name="className">The name of the shell class this object should represent.</param>
            public ClassInformation(string className)
                : this()
            {
                this.ClassName = className;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ClassInformation"/> class.
            /// </summary>
            /// <param name="className">The name of the shell class this object should represent.</param>
            /// <param name="command">The open command this shell class object should begin with.</param>
            public ClassInformation(string className, string command)
                : this(className)
            {
                this.DefaultOpenCommand = command;
            }

            /// <summary>
            /// Gets or sets the name of the shell class.
            /// </summary>
            public string ClassName { get; set; }

            /// <summary>
            /// Gets or sets the default open command for this shell class.
            /// </summary>
            public string DefaultOpenCommand { get; set; }
        }
    }
}
