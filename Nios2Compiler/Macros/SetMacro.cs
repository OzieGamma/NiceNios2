// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetMacro.cs" company="Oswald Maskens">
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
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class SetMacros
    {
        public static List<IMacro> All
        {
            get
            {
                return new List<IMacro> { Reg, Imm };
            }
        }

        public static IMacro Reg
        {
            get
            {
                return new RegSetMacro();
            }
        }

        public static IMacro Imm
        {
            get
            {
                return new ImmSetMacro();
            }
        }

        private sealed class ImmSetMacro : IMacro
        {
            private const string Pattern = "^([a-zA-Z0-9]+) = (-?[0-9][xX]?[0-9a-fA-F]*)$";

            public bool Match(SourceLine line)
            {
                var match = Regex.Match(line.Instruction, Pattern);

                var rA = match.Groups[1].Value;

                return match.Success && Registers.IsValid(rA);
            }

            public List<string> Handle(SourceLine line)
            {
                var output = new List<string>();
                var match = Regex.Match(line.Instruction, Pattern);

                var rA = match.Groups[1].Value;
                var imm = match.Groups[2].Value;

                output.Add(string.Format("addi {0}, zero, {1}", rA, imm));

                return output;
            }
        }

        private sealed class RegSetMacro : IMacro
        {
            private const string Pattern = "^([a-zA-Z0-9]+) = ([a-zA-Z0-9]+)$";

            public bool Match(SourceLine line)
            {
                var match = Regex.Match(line.Instruction, Pattern);

                var rB = match.Groups[1].Value;
                var rA = match.Groups[2].Value;

                return match.Success && Registers.IsValid(rA) && Registers.IsValid(rB);
            }

            public List<string> Handle(SourceLine line)
            {
                var output = new List<string>();
                var match = Regex.Match(line.Instruction, Pattern);

                var rB = match.Groups[1].Value;
                var rA = match.Groups[2].Value;

                output.Add(string.Format("add {0}, {1}, zero", rB, rA));

                return output;
            }
        }
    }
}
