using System;
using System.Collections.Generic;
using System.IO;
using gitnetimport;

namespace gitnetimporttest {
    class MainClass {
        public static void Main(string[] args)
        {
            var i = new Import();

            var datapath = args[0];
            using (var w = new BinaryWriter(Console.OpenStandardOutput())) {
                i.Run(w, "master", new [] {
                    new Import.Commit {
                        committerName = "Максим Кириллов",
                        committerEmail = "maksim.kirillov@example.com",
                        committerDateTime = DateTime.Now,
                        committerZone = DateTimeOffset.Now,
                        message = "trying",
                        deletedFiles = new string[0],
                        changedFiles = new [] { new Import.ChangedFile { path = "verse.txt", content = File.ReadAllBytes(datapath + "/verse-koi8.txt") } }
                    },
                    new Import.Commit {
                        committerName = "Максим Кириллов",
                        committerEmail = "maksim.kirillov@example.com",
                        committerDateTime = DateTime.Now,
                        committerZone = DateTimeOffset.Now,
                        message = "trying other encoding",
                        deletedFiles = new string[0],
                        changedFiles = new [] { new Import.ChangedFile { path = "verse.txt", content = File.ReadAllBytes(datapath + "/verse-cp1251.txt") } }
                    },
                    new Import.Commit {
                        committerName = "Максим Кириллов",
                        committerEmail = "maksim.kirillov@example.com",
                        committerDateTime = DateTime.Now,
                        committerZone = DateTimeOffset.Now,
                        message = "once more (очень сложно)",
                        deletedFiles = new string[0],
                        changedFiles = new [] { new Import.ChangedFile { path = "verse.txt", content = File.ReadAllBytes(datapath + "/verse-utf.txt") } }
                    },
                    new Import.Commit {
                        committerName = "",
                        committerEmail = "maksim.kirillov@example.com",
                        committerDateTime = DateTime.Now,
                        committerZone = DateTimeOffset.Now,
                        message = "given up, just push image",
                        deletedFiles = new [] { "verse.txt" },
                        changedFiles = new [] { new Import.ChangedFile { path = "verse.png", content = File.ReadAllBytes(datapath + "/verse-img.png") } }
                    },
                });
            }
        }
    }
}
