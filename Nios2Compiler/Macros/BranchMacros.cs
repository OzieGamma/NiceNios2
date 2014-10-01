// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BranchMacros.cs" company="Oswald Maskens">
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

    public static class BranchMacros
    {
        public static List<IMacro> All
        {
            get
            {
                return new List<IMacro> { Bge, BgeImm, Blt, BltImm, Bne, BneImm, Beq, BeqImm };
            }
        }

        public static IMacro Bge
        {
            get
            {
                return new BranchMacro(">=", "bge");
            }
        }

        public static IMacro BgeImm
        {
            get
            {
                return new BranchMacroImm(">=", "bge");
            }
        }

        public static IMacro Blt
        {
            get
            {
                return new BranchMacro("<", "blt");
            }
        }

        public static IMacro BltImm
        {
            get
            {
                return new BranchMacroImm("<", "blt");
            }
        }

        public static IMacro Bne
        {
            get
            {
                return new BranchMacro("!=", "bne");
            }
        }

        public static IMacro BneImm
        {
            get
            {
                return new BranchMacroImm("!=", "bne");
            }
        }

        public static IMacro Beq
        {
            get
            {
                return new BranchMacro("==", "beq");
            }
        }

        public static IMacro BeqImm
        {
            get
            {
                return new BranchMacroImm("==", "beq");
            }
        }

        private sealed class BranchMacro : IMacro
        {
            private readonly string opp;
            private readonly string pattern;

            public BranchMacro(string symbol, string opp)
            {
                this.pattern = string.Format("^if ([a-zA-Z0-9]+) {0} ([a-zA-Z0-9]+) goto ([0-9a-zA-Z+_]+)$", symbol);
                this.opp = opp;
            }

            public bool Match(SourceLine line)
            {
                var match = Regex.Match(line.Instruction, this.pattern);

                var rA = match.Groups[1].Value;
                var rB = match.Groups[2].Value;

                return match.Success && Registers.IsValid(rA) && Registers.IsValid(rB);
            }

            public List<string> Handle(SourceLine line)
            {
                var output = new List<string>();
                var match = Regex.Match(line.Instruction, this.pattern);

                var rA = match.Groups[1].Value;
                var rB = match.Groups[2].Value;
                var imm = match.Groups[3].Value;

                output.Add(string.Format("{0} {1}, {2}, {3}", this.opp, rA, rB, imm));

                return output;
            }
        }

        private sealed class BranchMacroImm : IMacro
        {
            private readonly string opp;
            private readonly string pattern;

            public BranchMacroImm(string symbol, string opp)
            {
                this.pattern = string.Format(
                    "^if ([a-zA-Z0-9]+) {0} (-?[0-9][xX]?[0-9a-fA-F]*) goto ([0-9a-zA-Z+_]+)$", 
                    symbol);
                this.opp = opp;
            }

            public bool Match(SourceLine line)
            {
                var match = Regex.Match(line.Instruction, this.pattern);

                var rA = match.Groups[1].Value;

                return match.Success && Registers.IsValid(rA);
            }

            public List<string> Handle(SourceLine line)
            {
                var output = new List<string>();
                var match = Regex.Match(line.Instruction, this.pattern);

                var rA = match.Groups[1].Value;
                var val = match.Groups[2].Value;
                var imm = match.Groups[3].Value;

                if (val == "0")
                {
                    output.Add(string.Format("{0} {1}, zero, {2}", this.opp, rA, imm));
                }
                else
                {
                    output.Add(string.Format("addi t7, zero, {0}", val));
                    output.Add(string.Format("{0} {1}, t7, {2}", this.opp, rA, imm));
                }

                return output;
            }
        }
    }
}
