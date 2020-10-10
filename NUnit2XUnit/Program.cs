// Copyright (C) 2019 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System;
using System.IO;

namespace NUnit2XUnit
{

    static class Program
    {
        static void ConvertFile(string inputPath, string outputPath = null)
        {
            string module = File.ReadAllText(inputPath);

        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: nunit2xunit path-to-folder");
                return;
            }

            void ConvertDir(string dir)
            {
                foreach (string file in Directory.GetFiles(dir, "*.cs"))
                {
                    ConvertFile(file);
                }

                foreach (string file in Directory.GetDirectories(dir))
                    ConvertDir(file);
            }

            string path = Path.GetFullPath(args[0]);
            ConvertDir(path);
        }
    }
}