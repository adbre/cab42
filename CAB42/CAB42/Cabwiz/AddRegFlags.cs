//-----------------------------------------------------------------------
// <copyright file="AddRegFlags.cs" company="42A Consulting">
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

    /// <summary>
    /// The AddRegFlags as per MSDN documentation.
    /// </summary>
    /// <remarks>
    ///     For more information see the MSDN documentation
    ///     http://msdn.microsoft.com/en-us/library/ms938375.aspx
    /// </remarks>
    [Flags]
    public enum AddRegFlags
    {
        /// <summary>
        /// If the registry key exists, do not overwrite it.
        /// This flag can be used with all other flags in this table.
        /// </summary>
        FLG_ADDREG_NOCLOBBER = 0x00000002,

        /// <summary>
        /// The REG_SZ registry data type.
        /// </summary>
        FLG_ADDREG_TYPE_SZ = 0x00000000,

        /// <summary>
        /// The REG_MULTI_SZ registry data type.
        /// The value field that follows can be a list of strings separated by commas.
        /// </summary>
        FLG_ADDREG_TYPE_MULTI_SZ = 0x0001000,

        /// <summary>
        /// The REG_BINARY registry data type.
        /// The value field that follows must be a list of numeric values separated by 
        /// commas, one byte per field, and must not use the 0x hexadecimal prefix.
        /// </summary>
        FLG_ADDREG_TYPE_BINARY = 0x00000001,

        /// <summary>
        /// The REG_DWORD data type.
        /// Only the noncompatible format in the Win32 Setup .inf documentation is supported.
        /// </summary>
        FLG_ADDREG_TYPE_DWORD = 0x00010001,
    }
}
