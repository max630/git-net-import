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
            public byte[] content;
        }
    
        public struct Commit {
            public string committerName;
            public string committerEmail;
            public DateTime committerDateTime;
            public DateTimeOffset committerZone;
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
                writeData(output, utf8.GetBytes(commit.message));
                foreach (var path in commit.deletedFiles) {
                    output.Write(ascii.GetBytes(string.Format("filedelete {0}\x0a", QuotePath(path))));
                }
                foreach (var file in commit.changedFiles) {
                    output.Write(ascii.GetBytes(string.Format("filemodify 100644 inline {0}\x0a", QuotePath(file.path))));
                    writeData(output, file.content);
                }
            }
        }
        
        public void writeData(BinaryWriter output, byte[] data)
        {
            output.Write(ascii.GetBytes(string.Format("data {0}\x0a", data.Length)));
            output.Write(data);
            output.Write(ascii.GetBytes("\x0a"));
        }

        public string QuotePath(string path)
        {
            var w = new StringWriter();
            w.Write("\"");
            foreach (var c in path) {
                if (c == '\\' || c == '\x0a' || c == '\"')
                    w.Write("\\");
                w.Write(c);
            }
            w.Write("\"");
            return w.ToString();
        }
        
        public static string FormatDate(DateTime dateTime, DateTimeOffset tzOffset)
        {
            return dateTime.ToString("ddd MMM d HH:mm:ss yyyy") + " " + tzOffset.ToString("zzz").Replace(":", "");
        }
    }
}

