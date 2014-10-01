// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImmMacros.cs" company="Oswald Maskens">
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

    public static class ImmMacros
    {
        public static List<IMacro> All
        {
            get
            {
                return new List<IMacro> { Addi, Andi, Ori, Xori, Slli, Srli };
            }
        }

        public static IMacro Addi
        {
            get
            {
                return new ImmMacro("\\+", "addi");
            }
        }

        public static IMacro Andi
        {
            get
            {
                return new ImmMacro("and", "andi");
            }
        }

        public static IMacro Ori
        {
            get
            {
                return new ImmMacro("or", "ori");
            }
        }

        public static IMacro Xori
        {
            get
            {
                return new ImmMacro("xor", "xori");
            }
        }

        public static IMacro Slli
        {
            get
            {
                return new ImmMacro("<<", "slli");
            }
        }

        public static IMacro Srli
        {
            get
            {
                return new ImmMacro(">>", "srli");
            }
        }

        private sealed class ImmMacro : IMacro
        {
            private readonly string opp;
            private readonly string pattern;

            public ImmMacro(string symbol, string opp)
            {
                this.pattern = string.Format(
                    "^([a-zA-Z0-9]+) = ([a-zA-Z0-9]+) {0} (-?[0-9][xX]?[0-9a-fA-F]*)$", 
                    symbol);
                this.opp = opp;
            }

            public bool Match(SourceLine line)
            {
                var match = Regex.Match(line.Instruction, this.pattern);

                var rB = match.Groups[1].Value;
                var rA = match.Groups[2].Value;

                return match.Success && Registers.IsValid(rB) && Registers.IsValid(rA);
            }

            public List<string> Handle(SourceLine line)
            {
                var output = new List<string>();
                var match = Regex.Match(line.Instruction, this.pattern);

                var rB = match.Groups[1].Value;
                var rA = match.Groups[2].Value;
                var imm = match.Groups[3].Value;

                output.Add(string.Format("{0} {1}, {2}, {3}", this.opp, rB, rA, imm));

                return output;
            }
        }
    }
}
