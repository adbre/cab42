//-----------------------------------------------------------------------
// <copyright file="SizeFormat.cs" company="42A Consulting">
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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides a class to format size structures to human readable strings.
    /// </summary>
    public static class SizeFormat
    {
        /// <summary>
        /// The SI units.
        /// </summary>
        private static readonly SizeFormatItem[] si = new SizeFormatItem[] 
        {
            new SizeFormatItem(10, 3, "kB", "kilobyte"),
            new SizeFormatItem(10, 6, "MB", "megabyte"),
            new SizeFormatItem(10, 9, "GB", "gigabyte"),
            new SizeFormatItem(10, 12, "TB", "terabyte"),
            new SizeFormatItem(10, 15, "PB", "petabyte"),
            new SizeFormatItem(10, 18, "EB", "exabyte"),
            new SizeFormatItem(10, 21, "ZB", "zettabyte"),
            new SizeFormatItem(10, 24, "YB", "yottabyte")
        };

        /// <summary>
        /// The binary units.
        /// </summary>
        private static readonly SizeFormatItem[] bi = new SizeFormatItem[] 
        {
            new SizeFormatItem(2, 10, "KiB", "kibibyte"),
            new SizeFormatItem(2, 20, "MiB", "mibibyte"),
            new SizeFormatItem(2, 30, "GiB", "gibibyte"),
            new SizeFormatItem(2, 40, "TiB", "tebibyte"),
            new SizeFormatItem(2, 50, "PiB", "pebibyte"),
            new SizeFormatItem(2, 60, "EiB", "exbibyte"),
            new SizeFormatItem(2, 70, "ZiB", "zebibyte"),
            new SizeFormatItem(2, 80, "YiB", "yobibyte")
        };

        /// <summary>
        /// Returns a human readable string representing a 64-bit integer size using SI suffixes.
        /// </summary>
        /// <param name="size">a Int64 defining the size</param>
        /// <returns>A human readable string representing a Int64 size</returns>
        public static string ToHumanReadableSize(this long size)
        {
            return ToHumanReadableSize(size, SizeFormatStandards.SI);
        }

        /// <summary>
        /// Returns a human readable string representing the specified Int64 size using a specified size suffix format method.
        /// </summary>
        /// <param name="size">a Int64 defining the size</param>
        /// <param name="standard">the method/suffixes to use</param>
        /// <returns>A human readable string of a given size.</returns>
        public static string ToHumanReadableSize(this long size, SizeFormatStandards standard)
        {
            var items = GetSizeFormatItems(standard);

            if (items == null)
            {
                throw new ArgumentOutOfRangeException("standard", "The specified standard is not supported");
            }

            var q = items.OrderByDescending(s => s.Power);

            double pce = 0;
            string factor = "B";

            foreach (SizeFormatItem s in q)
            {
                pce = Math.Pow(s.Base, s.Power);
                if (size >= pce)
                {
                    factor = s.Factor;
                    break;
                }
            }

            var value = pce > 0 ? size / pce : size;

            return string.Format(
                "{0:0.00} {1}",
                value,
                factor);
        }

        /// <summary>
        /// Gets the size format array for the specified standard.
        /// </summary>
        /// <param name="standard">The size formatting standard.</param>
        /// <returns>An array of <see cref="SizeFormatItem"/>, representing the specified standard.</returns>
        private static SizeFormatItem[] GetSizeFormatItems(SizeFormatStandards standard)
        {
            switch (standard)
            {
                case SizeFormatStandards.SI: return si;
                case SizeFormatStandards.Binary: return bi;
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// A class which describes a size quantity for a given standard.
        /// </summary>
        private class SizeFormatItem
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SizeFormatItem"/> class.
            /// </summary>
            /// <param name="base">The x value in a pow operation.</param>
            /// <param name="power">The y value in a pow operation.</param>
            /// <param name="factor">The short suffix/prefix for the size quantity.</param>
            /// <param name="name">A long name describing the size quantity.</param>
            public SizeFormatItem(int @base, int power, string factor, string name)
            {
                this.Base = @base;
                this.Power = power;
                this.Quantity = Math.Pow(@base, power);
                this.Factor = factor;
                this.Name = name;
            }

            /// <summary>
            /// Gets the x value for a pow operation.
            /// </summary>
            public int Base { get; private set; }

            /// <summary>
            /// Gets the y value for the pow operation.
            /// </summary>
            public int Power { get; private set; }

            /// <summary>
            /// Gets a value wich is equal to pow(<see cref="Base"/>, <see cref="Power"/>).
            /// </summary>
            public double Quantity { get; private set; }

            /// <summary>
            /// Gets the short name for this size quantity. Example "MB".
            /// </summary>
            public string Factor { get; private set; }

            /// <summary>
            /// Gets the long name for this size quanity. Example "megabytes".
            /// </summary>
            public string Name { get; private set; }
        }
    }
}
