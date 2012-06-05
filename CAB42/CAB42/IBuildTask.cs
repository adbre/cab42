//-----------------------------------------------------------------------
// <copyright file="IBuildTask.cs" company="42A Consulting">
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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// When implemented, this interface provides access to a build task.
    /// </summary>
    public interface IBuildTask
    {
        /// <summary>
        /// Gets the name of the project for this build task.
        /// </summary>
        string ProjectName { get; }

        /// <summary>
        /// Gets the name of the configuration for this build task.
        /// </summary>
        string Configuration { get; }

        /// <summary>
        /// Executes the build task.
        /// </summary>
        /// <param name="feedback">A object which can channel feedback from the build processes back to the end-user.</param>
        /// <returns>A value indicating whether the build task executed without any problems.</returns>
        bool Build(IBuildFeedback feedback);
    }
}
