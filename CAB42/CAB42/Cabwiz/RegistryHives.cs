//-----------------------------------------------------------------------
// <copyright file="RegistryHives.cs" company="42A Consulting">
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
    public enum RegistryHives : int
    {
        /// <summary>
        /// The same as HKEY_CLASSES_ROOT
        /// </summary>
        HKCR,

        /// <summary>
        /// The same as HKEY_CURRENT_USER
        /// </summary>
        HKCU,

        /// <summary>
        /// The same as HKEY_LOCAL_MACHINE
        /// </summary>
        HKLM,

        /// <summary>
        /// The registry key hive 'HKEY_CLASSES_ROOT'
        /// </summary>
        HKEY_CLASSES_ROOT,


        /// <summary>
        /// The registry key hive 'HKEY_CURRENT_USER'
        /// </summary>
        HKEY_CURRENT_USER,


        /// <summary>
        /// The registry key hive 'HKEY_LOCAL_MACHINE'
        /// </summary>
        HKEY_LOCAL_MACHINE
    }
}
