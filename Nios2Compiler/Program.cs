// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Oswald Maskens">
//   Copyright 2013-2014 Oswald Maskens
//   
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Nios2Compiler
{
    using System;
    using System.IO;
    using System.Linq;

    internal static class Program
    {
        private static readonly MacroHandler MacroHandler;

        static Program()
        {
            MacroHandler = new MacroHandler(MacrosList.All);
        }

        private static void Main(string[] args)
        {
            if (args == null)
            {
                args = new[] { Console.ReadLine() };
            }

            foreach (var s in args)
            {
                var filePath = Path.Combine(Environment.CurrentDirectory, s);
                var outputPath = Path.Combine(
                    Path.GetDirectoryName(filePath), 
                    Path.GetFileNameWithoutExtension(filePath) + "-out.asm");
                var outputPathNoComment = Path.Combine(
                    Path.GetDirectoryName(filePath), 
                    Path.GetFileNameWithoutExtension(filePath) + "-out-no-comment.asm");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Invalid argument");
                }
                else
                {
                    Console.WriteLine("Processing: {0}", s);

                    // Parse
                    var source = Parser.Parse(File.ReadAllLines(filePath).ToList());

                    // Handle Macros
                    var output = MacroHandler.HandleFile(source);

                    File.WriteAllLines(outputPath, output);

                    // No comment
                    var sourceWithNoComments = Parser.Parse(output);
                    File.WriteAllLines(outputPathNoComment, sourceWithNoComments.Select(l => l.Instruction));
                }

                Console.WriteLine("done with {0}", s);
            }

            // Console.Read();
        }
    }
}
