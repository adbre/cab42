//-----------------------------------------------------------------------
// <copyright file="BuildMessageCollection.cs" company="42A Consulting">
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
    /// A collection of BuildMessage objects.
    /// </summary>
    public class BuildMessageCollection : List<BuildMessage>
    {
        /// <summary>
        /// Adds an object to the end of the list.
        /// </summary>
        /// <param name="description">The description of the build message.</param>
        /// <param name="file">The file associated with the build message.</param>
        /// <param name="line">The line in the file associated with the build message.</param>
        /// <param name="column">The column in of the specified line.</param>
        /// <param name="type">The message type</param>
        /// <returns>The object added to the list.</returns>
        public virtual BuildMessage Add(string description, string file, int line, int column, BuildMessageType type)
        {
            var item = new BuildMessage(description, file, line, column, type);

            this.Add(item);

            return item;
        }

        /// <summary>
        /// Adds an object to the end of the list.
        /// </summary>
        /// <param name="description">The description of the build message.</param>
        /// <param name="file">The file associated with the build message.</param>
        /// <param name="line">The line in the file associated with the build message.</param>
        /// <param name="type">The message type</param>
        /// <returns>The object added to the list.</returns>
        public virtual BuildMessage Add(string description, string file, int line, BuildMessageType type)
        {
            var item = new BuildMessage(description, file, line, type);

            this.Add(item);

            return item;
        }

        /// <summary>
        /// Adds an object to the end of the list.
        /// </summary>
        /// <param name="description">The description of the build message.</param>
        /// <param name="file">The file associated with the build message.</param>
        /// <param name="type">The message type</param>
        /// <returns>The object added to the list.</returns>
        public virtual BuildMessage Add(string description, string file, BuildMessageType type)
        {
            var item = new BuildMessage(description, file, type);

            this.Add(item);

            return item;
        }

        /// <summary>
        /// Adds an object to the end of the list.
        /// </summary>
        /// <param name="description">The description of the build message.</param>
        /// <param name="type">The message type</param>
        /// <returns>The object added to the list.</returns>
        public virtual BuildMessage Add(string description, BuildMessageType type)
        {
            var item = new BuildMessage(description, type);

            this.Add(item);

            return item;
        }

        /// <summary>
        /// Adds an object to the end of the list.
        /// </summary>
        /// <param name="x">A exception object describing a error.</param>
        /// <returns>The object added to the list.</returns>
        public virtual BuildMessage Add(Exception x)
        {
            var item = new BuildMessage(x);

            this.Add(item);

            return item;
        }
    }
}
