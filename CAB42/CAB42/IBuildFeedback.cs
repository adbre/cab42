//-----------------------------------------------------------------------
// <copyright file="IBuildFeedback.cs" company="42A Consulting">
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

namespace C42A.CAB42
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A interface for pumping feedback to the user from a build process.
    /// </summary>
    public interface IBuildFeedback : IDisposable
    {
        void AddMessage(BuildMessage message);

        void AddMessage(string description, string file, int line, int column, BuildMessageType type);

        void AddMessage(Exception x);

        void WriteLine();

        void WriteLine(string value);

        void WriteLine(string format, params object[] args);

        void Write(string value);
    }
}
