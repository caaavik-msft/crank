﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Crank.Models
{
    public class Source
    {
        /// <summary>
        /// The name of a branch, or a commit hash starting with '#'
        /// </summary>
        public string BranchOrCommit { get; set; } = "";
        public string Repository { get; set; }
        public bool InitSubmodules { get; set; }
        public string LocalFolder { get; set; }

        /// <summary>
        /// When set, will specify where the source data will be copied to on the agent.
        /// If not provided, will use the source name as the destination folder.
        /// </summary>
        public string DestinationFolder { get; set; }

        /// <summary>
        /// When set by the controller, the server uses it to reuse the same source folder.
        /// The value should vary when the source does. When the server can't find the source folder, the LocalFolder property is cleared
        /// such that the controller doesn't send any local source.
        /// </summary>
        public string SourceKey { get; set; }

        /// <summary>
        /// Set in the agent to indicate that the source is cached in the given directory.
        /// </summary>
        public string CacheDirectory { get; set; }

        // When set, contains the location of the uploaded source code
        public Attachment SourceCode { get; set; }
    }
}
