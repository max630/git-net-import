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
            public string message;
            public IEnumerable<ChangedFile> changedFiles;
            public IEnumerable<string> deletedFiles;
        }

        System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
        System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
           
        public void Run(BinaryWriter output, string branch, IEnumerable<Commit> commits)
        {
            foreach (var commit in commits) {
                output.Write(ascii.GetBytes("commit refs/heads/" + branch + "\x0a"));
                output.Write(utf8.GetBytes("committer" + (commit.committerName.Length > 0?" ":"")
                                                       + commit.committerName
                                                       + " <" + commit.committerEmail + "> "
                                                       + FormatDate(commit.committerDateTime, commit.committerZone)
                                                       + "\x0a"));
                
            }
        }
        
        public void writeDate(BinaryWriter output, byte[] data)
        {
            output.Write(ascii.GetBytes(string.Format("data {0}\x0a", data.Length)));
            output.Write(data);
            output.Write(ascii.GetBytes("\x0a"));
        }
        
        public static string FormatDate(DateTime dateTime, TimeZone timeZone)
        {
            return "";
        }
    }
}

