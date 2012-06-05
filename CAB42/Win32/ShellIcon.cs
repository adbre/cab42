//-----------------------------------------------------------------------
// <copyright file="ShellIcon.cs" company="42A Consulting">
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
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Provides methods for retreiving a Icon image for a file based from the system.
    /// </summary>
    public static class ShellIcon
    {
        /// <summary>
        /// Placeholder for SHGFI flags.
        /// </summary>
        /// <remarks>
        ///     See MSDN documentation
        ///     http://msdn.microsoft.com/en-us/library/bb762179(v=vs.85).aspx
        /// </remarks>
        private enum SHGFIFlags : uint
        {
            /// <summary>
            /// Version 5.0. Apply the appropriate overlays to the file's icon. The SHGFI_ICON flag must also be set.
            /// </summary>
            SHGFI_ADDOVERLAYS = 0x000000020,

            /// <summary>
            /// Modify SHGFI_ATTRIBUTES to indicate that the dwAttributes member of the SHFILEINFO structure at psfi 
            /// contains the specific attributes that are desired. These attributes are passed to IShellFolder::GetAttributesOf.
            /// If this flag is not specified, 0xFFFFFFFF is passed to IShellFolder::GetAttributesOf, requesting all attributes. 
            /// This flag cannot be specified with the SHGFI_ICON flag.
            /// </summary>
            SHGFI_ATTR_SPECIFIED = 0x000020000,

            /// <summary>
            /// Retrieve the item attributes. The attributes are copied to the dwAttributes member of the structure specified in 
            /// the psfi parameter. These are the same attributes that are obtained from IShellFolder::GetAttributesOf.
            /// </summary>
            SHGFI_ATTRIBUTES = 0x000000800,

            /// <summary>
            /// Retrieve the display name for the file. The name is copied to the szDisplayName member of the structure specified 
            /// in psfi. The returned display name uses the long file name, if there is one, rather than the 8.3 form of the file name.
            /// </summary>
            SHGFI_DISPLAYNAME = 0x000000200,

            /// <summary>
            /// Retrieve the type of the executable file if pszPath identifies an executable file. The information is packed into 
            /// the return value. This flag cannot be specified with any other flags.
            /// </summary>
            SHGFI_EXETYPE = 0x000002000,

            /// <summary>
            /// Retrieve the handle to the icon that represents the file and the index of the icon within the system image list. 
            /// The handle is copied to the hIcon member of the structure specified by psfi, and the index is copied to the iIcon member.
            /// </summary>
            SHGFI_ICON = 0x000000100,

            /// <summary>
            /// Retrieve the name of the file that contains the icon representing the file specified by pszPath, as returned by the 
            /// IExtractIcon::GetIconLocation method of the file's icon handler. Also retrieve the icon index within that file. The 
            /// name of the file containing the icon is copied to the szDisplayName member of the structure specified by psfi. The 
            /// icon's index is copied to that structure's iIcon member.
            /// </summary>
            SHGFI_ICONLOCATION = 0x000001000,

            /// <summary>
            /// Modify SHGFI_ICON, causing the function to retrieve the file's large icon. The SHGFI_ICON flag must also be set.
            /// </summary>
            SHGFI_LARGEICON = 0x000000000,

            /// <summary>
            /// Modify SHGFI_ICON, causing the function to add the link overlay to the file's icon. The SHGFI_ICON flag must also be set.
            /// </summary>
            SHGFI_LINKOVERLAY = 0x000008000,

            /// <summary>
            /// Modify SHGFI_ICON, causing the function to retrieve the file's open icon. Also used to modify SHGFI_SYSICONINDEX, causing 
            /// the function to return the handle to the system image list that contains the file's small open icon. A container object 
            /// displays an open icon to indicate that the container is open. The SHGFI_ICON and/or SHGFI_SYSICONINDEX flag must also be 
            /// set.
            /// </summary>
            SHGFI_OPENICON = 0x000000002,

            /// <summary>
            /// Version 5.0. Return the index of the overlay icon. The value of the overlay index is returned in the upper eight bits of 
            /// the iIcon member of the structure specified by psfi. This flag requires that the SHGFI_ICON be set as well.
            /// </summary>
            SHGFI_OVERLAYINDEX = 0x000000040,

            /// <summary>
            /// Indicate that pszPath is the address of an ITEMIDLIST structure rather than a path name.
            /// </summary>
            SHGFI_PIDL = 0x000000008,

            /// <summary>
            /// Modify SHGFI_ICON, causing the function to blend the file's icon with the system highlight color. The SHGFI_ICON flag must 
            /// also be set.
            /// </summary>
            SHGFI_SELECTED = 0x000010000,

            /// <summary>
            /// Modify SHGFI_ICON, causing the function to retrieve a Shell-sized icon. If this flag is not specified the function sizes the
            /// icon according to the system metric values. The SHGFI_ICON flag must also be set.
            /// </summary>
            SHGFI_SHELLICONSIZE = 0x000000004,

            /// <summary>
            /// Modify SHGFI_ICON, causing the function to retrieve the file's small icon. Also used to modify SHGFI_SYSICONINDEX, causing 
            /// the function to return the handle to the system image list that contains small icon images. The SHGFI_ICON and/or 
            /// SHGFI_SYSICONINDEX flag must also be set.
            /// </summary>
            SHGFI_SMALLICON = 0x000000001,

            /// <summary>
            /// Retrieve the index of a system image list icon. If successful, the index is copied to the iIcon member of psfi. The return 
            /// value is a handle to the system image list. Only those images whose indices are successfully copied to iIcon are valid. 
            /// Attempting to access other images in the system image list will result in undefined behavior.
            /// </summary>
            SHGFI_SYSICONINDEX = 0x000004000,

            /// <summary>
            /// Retrieve the string that describes the file's type. The string is copied to the szTypeName member of the structure specified 
            /// in psfi.
            /// </summary>
            SHGFI_TYPENAME = 0x000000400,

            /// <summary>
            /// Indicates that the function should not attempt to access the file specified by pszPath. Rather, it should act as if the file 
            /// specified by pszPath exists with the file attributes passed in dwFileAttributes. This flag cannot be combined with the 
            /// SHGFI_ATTRIBUTES, SHGFI_EXETYPE, or SHGFI_PIDL flags.
            /// </summary>
            SHGFI_USEFILEATTRIBUTES = 0x000000010,
        }

        /// <summary>
        /// Gets a icon for the specified file.
        /// </summary>
        /// <param name="fileInfo">The file to retreive the icon for.</param>
        /// <param name="largeIcon">True if a large icon is to be returned, otherwise false.</param>
        /// <returns>A icon</returns>
        public static Icon GetFileIcon(this System.IO.FileInfo fileInfo, bool largeIcon = false)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException("fileInfo");
            }

            if (!string.IsNullOrEmpty(fileInfo.Extension))
            {
                return GetFileIcon(fileInfo.Name, largeIcon);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns a Icon image for the filetype determined by <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">A filename for the file type to get the icon image for.</param>
        /// <param name="largeIcon">If true, a large icon will be returned.</param>
        /// <returns>A <see cref="Icon"/> image for the specified filetype.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> is null or empty.</exception>
        /// <exception cref="ArgumentException">Could not extract the file extension part from <paramref name="fileName"/>.</exception>
        public static Icon GetFileIcon(string fileName, bool largeIcon)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            string extension = Path.GetExtension(fileName);
            fileName = "*" + extension; // use wildcard in case file doesn't exist

            var flags = SHGFIFlags.SHGFI_ICON | SHGFIFlags.SHGFI_USEFILEATTRIBUTES;
            
            if (largeIcon)
            {
                flags |= SHGFIFlags.SHGFI_LARGEICON;
            }
            else
            {
                flags |= SHGFIFlags.SHGFI_SMALLICON;
            }

            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr imageHandle;
            imageHandle = SHGetFileInfo(
                    fileName,
                    (uint)System.IO.FileAttributes.Normal,
                    ref shinfo,
                    (uint)Marshal.SizeOf(shinfo),
                    (uint)flags);

            try
            {
                return Icon.FromHandle(shinfo.hIcon);
            }
            catch
            {
                // file does not longer exist or is not available
                return null;
            }
        }

        /// <summary>
        /// Retrieves information about an object in the file system, such as a file, folder, directory, or drive root.
        /// </summary>
        /// <param name="pszPath">A pointer to a null-terminated string of maximum length MAX_PATH that contains the path and file name. Both absolute and relative paths are valid.</param>
        /// <param name="dwFileAttributes">A combination of one or more file attribute flags (FILE_ATTRIBUTE_ values as defined in Winnt.h). If uFlags does not include the SHGFI_USEFILEATTRIBUTES flag, this parameter is ignored.</param>
        /// <param name="psi">Pointer to a SHFILEINFO structure to receive the file information.</param>
        /// <param name="cbSizeFileInfo">The size, in bytes, of the SHFILEINFO structure pointed to by the psfi parameter.</param>
        /// <param name="uFlags">The flags that specify the file information to retrieve. This parameter can be a combination of the following values.</param>
        /// <returns>Returns a value whose meaning depends on the uFlags parameter.</returns>
        /// <remarks>
        ///     Please consider the MSDN documentation on this function
        ///     http://msdn.microsoft.com/en-us/library/bb762179(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Preserve parameter names cross for external calls.")]
        private static extern IntPtr SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psi,
            uint cbSizeFileInfo,
            uint uFlags);

        /// <summary>
        /// Contains information about a file object.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Preserve parameter names cross for external calls.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Preserve parameter names cross for external calls.")]
        private struct SHFILEINFO
        {
            /// <summary>
            /// A handle to the icon that represents the file. You are responsible for destroying this handle with DestroyIcon when you no longer need it.
            /// </summary>
            public IntPtr hIcon;

            /// <summary>
            /// The index of the icon image within the system image list.
            /// </summary>
            public IntPtr iIcon;

            /// <summary>
            /// An array of values that indicates the attributes of the file object. For information about these values, see the IShellFolder::GetAttributesOf method.
            /// </summary>
            public uint dwAttributes;

            /// <summary>
            /// A string that contains the name of the file as it appears in the Windows Shell, or the path and file name of the file that contains the icon representing the file.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            /// <summary>
            /// A string that describes the type of file.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }
    }
}
