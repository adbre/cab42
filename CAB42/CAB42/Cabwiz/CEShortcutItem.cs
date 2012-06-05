//-----------------------------------------------------------------------
// <copyright file="CEShortcutItem.cs" company="42A Consulting">
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

    public class CEShortcutItem
    {
        public CEShortcutItem(string shortcutFileName, bool isShortcutToFile, string target)
        {
            this.ShortcutFileName = shortcutFileName;
            this.IsShortcutToFile = isShortcutToFile;
            this.Target = target;
        }

        public CEShortcutItem(string shortcutFileName, bool isShortcutToFile, string target, string standardDestinationPath)
            : this(shortcutFileName, isShortcutToFile, target)
        {
            this.StandardDestinationPath = standardDestinationPath;
        }

        public string ShortcutFileName { get; set; }

        public bool IsShortcutToFile { get; set; }
        
        public string Target { get; set; }

        public string StandardDestinationPath { get; set; }

        public override string ToString()
        {
            string format;

            if (string.IsNullOrEmpty(this.StandardDestinationPath))
            {
                format = "\"{0}\",{1},\"{2}\"";
            }
            else
            {
                format = "\"{0}\",{1},\"{2}\",\"{3}\"";
            }
            
            return string.Format(
                format,
                this.ShortcutFileName,
                this.IsShortcutToFile ? 0 : 1,
                this.Target,
                this.StandardDestinationPath);
        }
    }
}
