// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MacroHandler.cs" company="Oswald Maskens">
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
    using System.Collections.Generic;

    internal class MacroHandler
    {
        private const int PadAmount = 80;
        private readonly List<IMacro> macros;

        public MacroHandler(List<IMacro> macros)
        {
            if (macros == null || !macros.TrueForAll(m => m != null))
            {
                throw new ArgumentNullException("macros");
            }

            this.macros = macros;
        }

        public List<string> HandleFile(List<SourceLine> lines)
        {
            var output = new List<string>();

            foreach (var line in lines)
            {
                var handled = false;

                foreach (var macro in this.macros)
                {
                    if (macro.Match(line) && !handled)
                    {
                        handled = true;

                        var macroOutput = macro.Handle(line);

                        // Add comment to first output line
                        var firstLine = line.Comment == string.Empty
                                            ? string.Format("{0}{1}", line.Alignment, macroOutput[0])
                                                    .PadRight(PadAmount)
                                            : string.Format("{0}{1} ;{2}", line.Alignment, macroOutput[0], line.Comment)
                                                    .PadRight(PadAmount);

                        macroOutput[0] = string.Format(
                            "{0} ;#{1} | {2}{3}", 
                            firstLine, 
                            line.Number.ToString().PadRight(6), 
                            line.Alignment, 
                            line.Text);

                        // Add spaces everywhere
                        for (var j = 1; j < macroOutput.Count; j++)
                        {
                            macroOutput[j] = string.Format("{0}{1}", line.Alignment, macroOutput[j]).PadRight(PadAmount);
                        }

                        output.AddRange(macroOutput);
                    }
                }

                if (!handled)
                {
                    var newCode = line.Comment == string.Empty
                                      ? string.Format("{0}{1}", line.Alignment, line.Instruction).PadRight(PadAmount)
                                      : string.Format("{0}{1} ;{2}", line.Alignment, line.Instruction, line.Comment)
                                              .PadRight(PadAmount);

                    output.Add(
                        string.Format(
                            "{0} ;#{1} | {2}{3}", 
                            newCode, 
                            line.Number.ToString().PadRight(6), 
                            line.Alignment, 
                            line.Text));
                }
            }

            return output;
        }
    }
}
