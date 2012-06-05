//-----------------------------------------------------------------------
// <copyright file="FileUtils.cs" company="42A Consulting">
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
namespace C42A.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides utility methods file file operations and information.
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// The maximum number of tries to attempt to find a temporary filename based on a specified filename.
        /// </summary>
        public const int GetTempFileNameMaxTries = 512;

        /// <summary>
        /// Returns the name of the root directory.
        /// </summary>
        /// <param name="path">The path which to extract the root directory part from.</param>
        /// <returns>The root directory name.</returns>
        public static string GetTopDirectory(string path)
        {
            string directoryName;

            while (!string.IsNullOrEmpty(path))
            {
                directoryName = System.IO.Path.GetDirectoryName(path);

                if (string.IsNullOrEmpty(directoryName))
                {
                    return path;
                }
                else
                {
                    path = directoryName;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the relative path between two absolute paths.
        /// </summary>
        /// <param name="from">The absolute path to calculate the relative path from.</param>
        /// <param name="to">The absolute path to calculate the relative path to.</param>
        /// <returns>The relative path from <paramref name="from"/> to <paramref name="to"/>.</returns>
        public static string GetRelativePath(string from, string to)
        {
            if (!System.IO.Path.IsPathRooted(from))
            {
                throw new ArgumentException("The path must be rooted.", "from");
            }

            if (!System.IO.Path.IsPathRooted(to))
            {
                throw new ArgumentException("The path must be rooted.", "to");
            }

            var fromUri = new Uri(from);
            var toUri = new Uri(to);

            return fromUri.MakeRelativeUri(toUri).ToString();
        }

        /// <summary>
        /// Writes a byte array to a file on the harddrive using a specified filename and opens that file.
        /// </summary>
        /// <param name="fileName">the filename to use</param>
        /// <param name="data">the data to write</param>
        /// <remarks>
        ///     <para>
        ///         This method attempts to create a temporary file on the harddrive using the specified filename 
        ///         and writes the specified byte array to that file.
        ///     </para>
        ///     <para>
        ///         This method uses the GetTempFileName() method to attempt to find a usable temporary filename.
        ///         If GetTempFileName() returns a null value, the System.Path.GetTempFileName() will be used.
        ///     </para>
        ///     <para>
        ///         The possible filenames to be used are, if fileName is for example "myFile.txt", in this order:
        ///         1. myFile.txt
        ///         2. myFile[1].txt
        ///         3. abc123.txt
        ///         4. abc123.tmp
        ///     </para>
        /// </remarks>
        public static void OpenFile(string fileName, byte[] data)
        {
            var tmpFile = GetTempFileName(fileName);

            // If tmpFile is not null, we have a valid temporary filename
            // that is not already taken.
            // (e.g. %TEMP%\MyDocument[2].pdf)
            if (tmpFile != null)
            {
                // Save the file.
                SaveFile(tmpFile, data);
            }
            else
            {
                // the tmpFile value is null, 
                // so we'll try to let the system create a temporary file to use
                // (e.g. %TEMP%\abc123.tmp)
                tmpFile = Path.GetTempFileName();

                // Save the file
                SaveFile(tmpFile, data);

                // The next step can only be performed if the extension can be found.
                if (Path.GetExtension(fileName) != null)
                {
                    // To be able to open the file we must change the extension
                    // (currently .tmp) to the correct extension to let the
                    // system decide which program to use to open the file with.
                    var targetFile = Path.Combine(
                            Path.GetDirectoryName(tmpFile),
                            Path.GetFileNameWithoutExtension(tmpFile) + Path.GetExtension(fileName));

                    File.Move(tmpFile, targetFile);
                }
            }

            // Open the file
            OpenFile(tmpFile);
        }

        /// <summary>
        /// Writes a byte array to the specified file on the drive.
        /// </summary>
        /// <param name="fileName">the file to write</param>
        /// <param name="data">the data to write</param>
        public static void SaveFile(string fileName, byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                SaveFile(fileName, ms);

                ms.Close();
                ms.Dispose();
            }
        }

        /// <summary>
        /// Writes from a source stream to a specified file until the source stream has reached the end.
        /// </summary>
        /// <param name="fileName">the target file path</param>
        /// <param name="source">the source stream from which to read</param>
        public static void SaveFile(string fileName, Stream source)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                int read = 0;
                byte[] buffer = new byte[1024];

                while (true)
                {
                    read = source.Read(buffer, 0, buffer.Length);

                    if (read <= 0)
                    {
                        break;
                    }

                    fs.Write(buffer, 0, read);
                }
            }
        }

        /// <summary>
        /// Returns a temporary filename based on the specified filename.
        /// </summary>
        /// <param name="fileName">The filename to use as a basis for a temporary file.</param>
        /// <returns>A filename in the stystem's temporary directory. A null reference if unsucsessful.</returns>
        /// <remarks>
        ///     <para>
        ///         Uses a specified filename to find a available (non-existing) filename in the system's temporary directory.
        ///     </para>
        ///     <para>
        ///         If %TEMP%\filename.txt exists, %TEMP%\filename[n].txt is returned.
        ///     </para>
        ///     <para>
        ///         A null value is returned if the method was unsuccessful by a maximum number of attempts 
        ///         (defined by the GetTempFileNameMaxTries field).
        ///     </para>
        /// </remarks>
        /// <exception cref="ArgumentException">fileName contain one or more invalid characters defined by Path.GetInvalidFileNameChars()</exception>
        /// <exception cref="ArgumentException">fileName has zero characters</exception>
        /// <exception cref="ArgumentNullException">fileName is a null reference</exception>
        public static string GetTempFileName(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }

            if (fileName.Length <= 0 || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                throw new ArgumentException("the specified filename is either zero-lengthed or contains invalid characters", "fileName");
            }

            string tmpPath = Path.GetTempPath();

            string tmpFile = Path.Combine(tmpPath, fileName);

            if (!File.Exists(tmpFile))
            {
                return tmpFile;
            }
            else
            {
                for (int i = 0; i < GetTempFileNameMaxTries; i++)
                {
                    var tmpFileName = string.Format(
                        "{1}[{0}]{2}",
                        i,
                        Path.GetFileNameWithoutExtension(fileName),
                        Path.GetExtension(fileName));

                    tmpFile = Path.Combine(tmpPath, tmpFileName);

                    if (!File.Exists(tmpFile))
                    {
                        return tmpFile;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Opens a specified file and can delete the file upon exit (used for temporary files).
        /// </summary>
        /// <param name="filename">the file to open</param>
        /// <param name="deleteWhenDone">a value indicating wether to delete the file or not when the process is done</param>
        /// <remarks>
        ///     <para>
        ///         This method uses shell execution to open the specified file.
        ///     </para>
        ///     <para>
        ///         If deleteWhenDone is true, a new thread is created that waits for the 
        ///         process started in the shell execution to exit.
        ///         The thread calls the Process.WaitForExit() method to wait for the 
        ///         process to exit.
        ///         Either if the thread is aborted or when the process has exited,
        ///         the specified filename will be deleted if deleteWhenDone is true.
        ///     </para>
        /// </remarks>
        public static void OpenFile(string filename, bool deleteWhenDone)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(filename);

            System.Threading.Thread th = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(OpenPdfDocument_WaitForExit));

            if (!deleteWhenDone)
            {
                th.Start(new OpenFileProcessInfo()
                {
                    Process = p,
                    Filename = filename,
                    DeleteWhenDone = deleteWhenDone
                });
            }
        }

        /// <summary>
        ///  Opens a specified file and deletes the file upon exit (used for temporary files).
        /// </summary>
        /// <param name="filename">The file to open.</param>
        public static void OpenFile(string filename)
        {
            OpenFile(filename, true);
        }

        /// <summary>
        /// Callback target for the OpenFile method.
        /// </summary>
        /// <param name="arg">A <see cref="OpenFileProcessInfo"/> object.</param>
        private static void OpenPdfDocument_WaitForExit(object arg)
        {
            OpenFileProcessInfo pi = null;

            try
            {
                pi = arg as OpenFileProcessInfo;

                if (pi != null && File.Exists(pi.Filename))
                {
                    pi.Process.WaitForExit();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                // do nothing, exit
            }
            catch
            {
                // ignore exceptions, exit
            }
            finally
            {
                if (pi != null && pi.DeleteWhenDone)
                {
                    try
                    {
                        File.Delete(pi.Filename);
                    }
                    catch
                    {
                        // do nothing, suppress this error message.
                        // The user may not have permission to delete this file,
                        // or the file has already been deleted/moved.
                    }
                }
            }
        }

        /// <summary>
        /// Data container for the thread callback.
        /// </summary>
        private class OpenFileProcessInfo
        {
            /// <summary>
            /// Gets or sets the Process associated with this context.
            /// </summary>
            public System.Diagnostics.Process Process { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the file being monitored should be deleted when the process exits.
            /// </summary>
            public bool DeleteWhenDone { get; set; }

            /// <summary>
            /// Gets or sets the absolute path to the file to be watched.
            /// </summary>
            public string Filename { get; set; }
        }
    }
}
