// This program is free software. It comes without any warranty, to the
// extent permitted by applicable law. You can redistribute it and/or
// modify it under the terms of the Do What The Fuck You Want To Public
// License, Version 2, as published by Sam Hocevar. See
// http://www.wtfpl.net/ for more details.

using System;
using System.IO;
using System.Collections.Generic;

namespace gitnetimport {
    public class Import {
        public struct ChangedFile {
            public string path;
            public IEnumerable<byte> content;
        }
    
        public struct Commit {
            public string committerName;
            public string committerEmail;
            public DateTime committerDateTime;
            public TimeZone committerZone;
            public IEnumerable<ChangedFile> changedFiles;
            public IEnumerable<string> deletedFiles;
        }
    
        public static void Run(Stream output, IEnumerable<Commit> commits)
        {
        }
    }
}

