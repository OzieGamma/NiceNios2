// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PushMacro.cs" company="Oswald Maskens">
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

namespace Nios2Compiler.Macros
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class PushMacro : IMacro
    {
        private const string Pattern = "^push ([a-zA-Z0-9]+)$";

        public bool Match(SourceLine line)
        {
            return Regex.IsMatch(line.Instruction, Pattern);
        }

        public List<string> Handle(SourceLine line)
        {
            var output = new List<string>();
            var match = Regex.Match(line.Instruction, Pattern);

            var register = match.Groups[1].Value;
            output.Add("addi sp, sp, -4");
            output.Add(string.Format("stw {0}, 0(sp)", register));

            if (!Registers.IsValid(register))
            {
                Console.WriteLine("Unknown register at line {0}", line.Number);
            }

            return output;
        }
    }
}
