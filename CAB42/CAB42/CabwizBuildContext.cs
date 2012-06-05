//-----------------------------------------------------------------------
// <copyright file="CabwizBuildContext.cs" company="42A Consulting">
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
    /// This class builds multiple build tasks by using
    /// </summary>
    public class CabwizBuildContext : BuildTaskContext
    {
        private CabwizTextWriter cabwizTextWriter;

        /// <summary>
        /// Initializes a new instance of the CabwizProjectBuilder class.
        /// </summary>
        public CabwizBuildContext()
        {
            this.Cabwiz = new Cabwiz.CabwizApplication()
            {
                NoUninstall = false,
                Compress = true
            };
        }

        /// <summary>
        /// Gets or sets the Cabwiz Application execution context.
        /// </summary>
        public Cabwiz.CabwizApplication Cabwiz { get; set; }

        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();

            if (this.Cabwiz != null)
            {
                this.Cabwiz.Dispose();
                this.Cabwiz = null;
            }
        }

        /// <inheritdoc />
        public override IBuildResult Build(IBuildTask[] tasks, IBuildFeedback feedback)
        {
            // Wrap the feedback object to a textwriter instance so we may channel 
            // the output from the cabwiz.exe application to the feedback object.
            this.cabwizTextWriter = new CabwizTextWriter(feedback);

            this.Cabwiz.StandardOutput = this.cabwizTextWriter;
            this.Cabwiz.StandardError = this.cabwizTextWriter;

            return base.Build(tasks, feedback);
        }

        /// <inheritdoc />
        protected override bool BuildTask(IBuildTask task, IBuildFeedback feedback)
        {
            if (this.cabwizTextWriter != null)
            {
                this.cabwizTextWriter.SetActiveTask(task);
            }

            try
            {
                var cabwizTask = task as ICabwizBuildTask;
                if (cabwizTask != null)
                {
                    return cabwizTask.Build(this.Cabwiz, feedback);
                }
                else
                {
                    return base.BuildTask(task, feedback);
                }
            }
            finally
            {
                if (this.cabwizTextWriter != null)
                {
                    this.cabwizTextWriter.SetActiveTask(null);
                }
            }
        }
    }
}
