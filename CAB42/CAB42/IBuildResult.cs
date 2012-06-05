//-----------------------------------------------------------------------
// <copyright file="IBuildResult.cs" company="42A Consulting">
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
    /// Describes a result from a build process.
    /// </summary>
    public interface IBuildResult
    {
        /// <summary>
        /// Gets a value indicating whether the build process completed without errors.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the total number of tasks which was to be performed.
        /// </summary>
        int TotalSteps { get; }

        /// <summary>
        /// Gets the number of successful tasks.
        /// </summary>
        int TasksSuccesful { get; }

        /// <summary>
        /// Gets the number of aborted tasks.
        /// </summary>
        int TasksAborted { get; }

        /// <summary>
        /// Gets the number of skipped tasks.
        /// </summary>
        int TasksSkipped { get; }
    }
}
