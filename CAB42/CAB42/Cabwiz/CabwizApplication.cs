//-----------------------------------------------------------------------
// <copyright file="CabwizApplication.cs" company="42A Consulting">
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
    using System.Windows.Forms;

    /// <summary>
    /// A class for invoking/calling the cabwiz executable.
    /// </summary>
    public class CabwizApplication : IDisposable
    {
        /// <summary>
        /// The default path and filename to cabwiz.exe
        /// </summary>
        private const string DefaultFileName = @"C:\Program Files (x86)\Microsoft Visual Studio 9.0\SmartDevices\SDK\SDKTools\cabwiz.exe";

        /// <summary>
        /// Initializes a new instance of the <see cref="CabwizApplication"/> class.
        /// </summary>
        public CabwizApplication()
        {
            this.FileName = DefaultFileName;
        }

        /// <summary>
        /// Gets or sets the filename to the cabwiz executable.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a path to a file where error output from cabwiz.exe will be written to.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If null or empty, no error log file will be used and error data will be written 
        ///         to the standard output and standard error streams.
        ///     </para>
        /// </remarks>
        public string ErrorLog { get; set; }

        /// <summary>
        /// Gets or sets a path where the cabinet files will be created.
        /// </summary>
        public string DestinationDirectory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the application can be uninstalled or not.
        /// </summary>
        public bool NoUninstall { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the cabinet files will be compressed or not.
        /// </summary>
        public bool Compress { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="System.IO.TextWriter"/> for the standard error stream.
        /// </summary>
        public System.IO.TextWriter StandardError { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="System.IO.TextWriter"/> for the standard output stream.
        /// </summary>
        public System.IO.TextWriter StandardOutput { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether cabwiz.exe has been called or not by this class already.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether there is a pending cancel request.
        /// </summary>
        private bool CancelRequested { get; set; }

        /// <summary>
        /// Quickly call cabwiz.exe.
        /// </summary>
        /// <param name="iniFile">The inf file object.</param>
        /// <param name="nouninstall">The <see cref="NoUninstall"/> property will be set to this value.</param>
        /// <param name="compress">The <see cref="Compress"/> property will be set to this value.</param>
        /// <returns>The cabwiz.exe exit code. 0 indicates success, a non-zero value indicates failure.</returns>
        public static int QuickRun(InformationFile iniFile, bool nouninstall, bool compress)
        {
            var app = new CabwizApplication();

            app.NoUninstall = nouninstall;
            app.Compress = compress;

            return app.Run(iniFile);
        }

        /// <summary>
        /// Disposes any managed resources used by this class.
        /// </summary>
        public void Dispose()
        {
            if (this.StandardError != null)
            {
                this.StandardError.Dispose();
            }

            if (this.StandardOutput != null)
            {
                this.StandardOutput.Dispose();
            }
        }

        /// <summary>
        /// Executes and calls cabwiz.exe using the specified inf file.
        /// </summary>
        /// <param name="iniFile">A <see cref="InformationFile"/> to be written to the hard-drive.</param>
        /// <returns>The cabwiz.exe exit code. 0 indicates success, a non-zero value indicates failure.</returns>
        public int Run(InformationFile iniFile)
        {
            if (iniFile == null)
            {
                throw new ArgumentNullException("iniFile");
            }

            iniFile.WriteInformationFile();

            return this.Run(iniFile.FileName);
        }

        /// <summary>
        /// Executes and calls cabwiz.exe using the specified inf file.
        /// </summary>
        /// <param name="informationFile">A absolute path to the .inf file which is to be used by cabwiz.exe</param>
        /// <returns>The cabwiz.exe exit code. 0 indicates success, a non-zero value indicates failure.</returns>
        public int Run(string informationFile)
        {
            if (this.IsRunning)
            {
                throw new InvalidOperationException("The application is already running.");
            }
            else
            {
                this.IsRunning = true;

                if (!System.IO.Directory.Exists(this.DestinationDirectory))
                {
                    System.IO.Directory.CreateDirectory(this.DestinationDirectory);
                }

                try
                {
                    using (System.Diagnostics.Process p = new System.Diagnostics.Process())
                    {
                        p.StartInfo.FileName = this.FileName;
                        p.StartInfo.Arguments = this.GetArguments(informationFile);
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.UseShellExecute = false;

                        p.Start();

                        if (this.StandardOutput != null)
                        {
                            OutputForward.BeginReadToEnd(this.StandardOutput, p.StandardOutput);
                        }

                        if (this.StandardError != null)
                        {
                            OutputForward.BeginReadToEnd(this.StandardError, p.StandardError);
                        }

                        while (!p.HasExited)
                        {
                            if (this.CancelRequested)
                            {
                                p.WaitForExit(500);

                                if (!p.HasExited)
                                {
                                    p.Kill();
                                }
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(300);
                            }
                        }

                        return p.ExitCode;
                    }
                }
                finally
                {
                    this.IsRunning = false;
                }
            }
        }

        /// <summary>
        /// Attempts to cancel any current cabwiz process invoked by this process.
        /// </summary>
        public void Cancel()
        {
            this.CancelRequested = true;
        }

        /// <summary>
        /// Gets the command line, as it would be if <see cref="Run"/> was to be executed.
        /// </summary>
        /// <param name="informationFile">The information file to be used when creating the command line.</param>
        /// <returns>A command line <see cref="System.String"/>.</returns>
        public string PreviewCommandLine(InformationFile informationFile)
        {
            return this.PreviewCommandLine(informationFile.FileName);
        }

        /// <summary>
        /// Gets the command line, as it would be if <see cref="Run"/> was to be executed.
        /// </summary>
        /// <param name="informationFile">The information file to be used wh
        public string PreviewCommandLine(string informationFile)
        {
            return string.Concat("\"", this.FileName, "\" ", this.GetArguments(informationFile));
        }

        /// <summary>
        /// Gets the command line arguments for a information file and current settings.
        /// </summary>
        /// <param name="informationFile">The information file to use as the first argument.</param>
        /// <returns>A command line string.</returns>
        private string GetArguments(string informationFile)
        {
            var arguments = new List<string>();

            arguments.Add(string.Format(@"""{0}""", informationFile));

            if (!string.IsNullOrEmpty(this.DestinationDirectory))
                arguments.Add(string.Format(@"/dest ""{0}""", this.DestinationDirectory));

            if (!string.IsNullOrEmpty(this.ErrorLog))
                arguments.Add(string.Format(@"/err ""{0}""", this.ErrorLog));

            if (this.NoUninstall)
                arguments.Add("/nouninstall");

            if (this.Compress)
                arguments.Add("/compress");

            return string.Join(" ", arguments.ToArray());
        }

        /// <summary>
        /// A class which will forward data from a <see cref="System.IO.TextReader"/> to a text <see cref="System.IO.TextWriter"/>.
        /// </summary>
        private class OutputForward
        {
            /// <summary>
            /// Gets or sets the underlying writer.
            /// </summary>
            public System.IO.TextWriter Writer { get; set; }

            /// <summary>
            /// Gets or sets the underlying reader.
            /// </summary>
            public System.IO.TextReader Reader { get; set; }

            /// <summary>
            /// Will run <see cref="ReadToEnd"/> asynchronly.
            /// </summary>
            /// <param name="writer">The target stream.</param>
            /// <param name="reader">The source stream.</param>
            public static void BeginReadToEnd(System.IO.TextWriter writer, System.IO.TextReader reader)
            {
                var f = new OutputForward()
                {
                    Writer = writer,
                    Reader = reader
                };

                new MethodInvoker(f.ReadToEnd).BeginInvoke(null, null);
            }

            /// <summary>
            /// This method will read one line at a time from <see cref="Reader"/> until the the stream is closed or has reach it's end.
            /// </summary>
            public void ReadToEnd()
            {
                if (this.Writer == null)
                {
                    throw new InvalidOperationException("The writer is null.");
                }

                if (this.Reader == null)
                {
                    throw new InvalidOperationException("The reader is null.");
                }

                string line;
                while ((line = this.Reader.ReadLine()) != null)
                {
                    this.Writer.WriteLine(line);
                }
            }
        }
    }
}
