// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegMacros.cs" company="Oswald Maskens">
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

    public static class RegMacros
    {
        public static List<IMacro> All
        {
            get
            {
                return new List<IMacro> { Add, Sub, And, Or, Xor, Nor, Sll, Srl };
            }
        }

        public static IMacro Add
        {
            get
            {
                return new RegMacro("\\+", "add");
            }
        }

        public static IMacro Sub
        {
            get
            {
                return new RegMacro("-", "sub");
            }
        }

        public static IMacro And
        {
            get
            {
                return new RegMacro("and", "and");
            }
        }

        public static IMacro Or
        {
            get
            {
                return new RegMacro("or", "or");
            }
        }

        public static IMacro Xor
        {
            get
            {
                return new RegMacro("xor", "xor");
            }
        }

        public static IMacro Nor
        {
            get
            {
                return new RegMacro("nor", "nor");
            }
        }

        public static IMacro Sll
        {
            get
            {
                return new RegMacro("<<", "sll");
            }
        }

        public static IMacro Srl
        {
            get
            {
                return new RegMacro(">>", "srl");
            }
        }

        private sealed class RegMacro : IMacro
        {
            private readonly string opp;
            private readonly string pattern;

            public RegMacro(string symbol, string opp)
            {
                this.pattern = string.Format("^([a-zA-Z0-9]+) = ([a-zA-Z0-9]+) {0} ([a-zA-Z0-9]+)$", symbol);
                this.opp = opp;
            }

            public bool Match(SourceLine line)
            {
                var match = Regex.Match(line.Instruction, this.pattern);

                var rC = match.Groups[1].Value;
                var rA = match.Groups[2].Value;
                var rB = match.Groups[3].Value;

                return match.Success && Registers.IsValid(rC) && Registers.IsValid(rA) && Registers.IsValid(rB);
            }

            public List<string> Handle(SourceLine line)
            {
                var output = new List<string>();
                var match = Regex.Match(line.Instruction, this.pattern);

                var rC = match.Groups[1].Value;
                var rA = match.Groups[2].Value;
                var rB = match.Groups[3].Value;

                output.Add(string.Format("{0} {1}, {2}, {3}", this.opp, rC, rA, rB));

                return output;
            }
        }
    }
}
