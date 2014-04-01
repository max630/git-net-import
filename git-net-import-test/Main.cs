using System;
using System.Collections.Generic;
using System.IO;
using gitnetimport;

namespace gitnetimporttest {
    class MainClass {
        public static void Main(string[] args)
        {
            var i = new Import();

            using (var w = new BinaryWriter(Console.OpenStandardOutput())) {
                i.Run(w, "master", new Import.Commit[] {});
            }
        }
    }
}
