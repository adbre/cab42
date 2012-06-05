// -----------------------------------------------------------------------
// <copyright file="BuildTaskContext.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace C42A.CAB42
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class BuildTaskContext : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the BuildTaskContext class.
        /// </summary>
        public BuildTaskContext()
        {
            // nothing else to do here.
        }

        /// <summary>
        /// Disposes all managed an unmanaged resources used by this object.
        /// </summary>
        public virtual void Dispose()
        {
            // nothing to do here.
        }

        /// <summary>
        /// Executes a collection of build tasks using the specified feedback object.
        /// </summary>
        /// <param name="tasks">An array of build tasks to execute.</param>
        /// <returns>The build result for all tasks.</returns>
        public virtual IBuildResult Build(IBuildTask[] tasks, IBuildFeedback feedback)
        {
            if (tasks == null)
            {
                throw new ArgumentNullException("tasks");
            }

            if (feedback == null)
            {
                throw new ArgumentNullException("feedback");
            }

            var result = new BuildResult()
            {
                Success = true,
                TotalSteps = tasks.Length
            };

            foreach (var task in tasks)
            {
                try
                {
                    feedback.WriteLine("------ Build started: {0} ------", task.ToString());

                    result.Success = this.BuildTask(task, feedback);

                    feedback.WriteLine();
                }
                catch (Exception x)
                {
                    var message = new BuildMessage(x)
                    {
                        Project = task.ProjectName,
                        Configuration = task.Configuration
                    };

                    feedback.AddMessage(message);

                    result.Success = false;
                }

                if (result.Success)
                {
                    result.TasksSuccesful++;
                }
                else
                {
                    feedback.Write("aborting build... ");

                    result.TasksAborted++;

                    break;
                }
            }

            if (!result.Success)
            {
                feedback.WriteLine("aborted!");
                feedback.WriteLine();
            }

            feedback.WriteLine(
                "========== Build: {1} succeeded, {2} failed, {3} skipped ==========",
                result.Success ? "succeeded" : "failed",
                result.TasksSuccesful,
                result.TasksAborted,
                result.TasksSkipped,
                result.TotalSteps);

            return result;
        }

        /// <summary>
        /// Executes the Build method of the specified task.
        /// </summary>
        /// <param name="task">The build task to execute.</param>
        /// <param name="feedback">The feedback object.</param>
        /// <returns>The value returned by the task's Build method.</returns>
        protected virtual bool BuildTask(IBuildTask task, IBuildFeedback feedback)
        {
            return task.Build(feedback);
        }

        /// <summary>
        /// Implementation of the IBuildResult class.
        /// </summary>
        private class BuildResult : IBuildResult
        {
            /// <inheritdoc />
            public bool Success { get; set; }

            /// <inheritdoc />
            public int TotalSteps { get; set; }

            /// <inheritdoc />
            public int TasksSuccesful { get; set; }

            /// <inheritdoc />
            public int TasksAborted { get; set; }

            /// <inheritdoc />
            public int TasksSkipped
            {
                get
                {
                    return this.TotalSteps - this.TasksAborted - this.TasksSuccesful;
                }
            }
        }
    }
}
