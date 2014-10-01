// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemMacros.cs" company="Oswald Maskens">
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

    public static class MemMacros
    {
        public static List<IMacro> All
        {
            get
            {
                return new List<IMacro> { StwImm, StwImmOffset, StwReg, StwRegOffset, Ldw, LdwOffset };
            }
        }

        public static IMacro StwImm
        {
            get
            {
                return new StwImmMacro();
            }
        }

        public static IMacro StwImmOffset
        {
            get
            {
                return new StwImmOffsetMacro();
            }
        }

        public static IMacro StwReg
        {
            get
            {
                return new StwRegMacro();
            }
        }

        public static IMacro StwRegOffset
        {
            get
            {
                return new StwRegOffsetMacro();
            }
        }

        public static IMacro Ldw
        {
            get
            {
                return new LdwMacro();
            }
        }

        public static IMacro LdwOffset
        {
            get
            {
                return new LdwOffsetMacro();
            }
        }

        private sealed class LdwMacro : IMacro
        {
            private const string Pattern = @"^([a-zA-Z0-9]+) = M\[([0-9a-zA-Z+_]+)\]$";

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

                output.Add(string.Format("ldw {0}, {1}(zero)", rA, imm));

                return output;
            }
        }

        private sealed class LdwOffsetMacro : IMacro
        {
            private const string Pattern = @"^([a-zA-Z0-9]+) = M\[([0-9a-zA-Z+_]+\([0-9a-zA-Z]+\))\]$";

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

                output.Add(string.Format("ldw {0}, {1}", rA, imm));

                return output;
            }
        }

        private sealed class StwImmMacro : IMacro
        {
            private const string Pattern = @"^M\[([0-9a-zA-Z+_]+)\] = (-?[0-9][xX]?[0-9a-fA-F]*)$";

            public bool Match(SourceLine line)
            {
                return Regex.IsMatch(line.Instruction, Pattern);
            }

            public List<string> Handle(SourceLine line)
            {
                var output = new List<string>();
                var match = Regex.Match(line.Instruction, Pattern);

                var imm = match.Groups[1].Value;
                var val = match.Groups[2].Value;

                if (val == "0")
                {
                    output.Add(string.Format("stw zero, {0}(zero)", imm));
                }
                else
                {
                    output.Add(string.Format("addi t7, zero, {0}", val));
                    output.Add(string.Format("stw t7, {0}(zero)", imm));
                }

                return output;
            }
        }

        private sealed class StwImmOffsetMacro : IMacro
        {
            private const string Pattern = @"^M\[([0-9a-zA-Z+_]+\([0-9a-zA-Z]+\))\] = (-?[0-9][xX]?[0-9a-fA-F]*)$";

            public bool Match(SourceLine line)
            {
                return Regex.IsMatch(line.Instruction, Pattern);
            }

            public List<string> Handle(SourceLine line)
            {
                var output = new List<string>();
                var match = Regex.Match(line.Instruction, Pattern);

                var imm = match.Groups[1].Value;
                var val = match.Groups[2].Value;

                if (val == "0")
                {
                    output.Add(string.Format("stw zero, {0}", imm));
                }
                else
                {
                    output.Add(string.Format("addi t7, zero, {0}", val));
                    output.Add(string.Format("stw t7, {0}", imm));
                }

                return output;
            }
        }

        private sealed class StwRegMacro : IMacro
        {
            private const string Pattern = @"^M\[([0-9a-zA-Z+_]+)\] = ([a-zA-Z0-9]+)$";

            public bool Match(SourceLine line)
            {
                var match = Regex.Match(line.Instruction, Pattern);

                var rA = match.Groups[2].Value;

                return match.Success && Registers.IsValid(rA);
            }

            public List<string> Handle(SourceLine line)
            {
                var output = new List<string>();
                var match = Regex.Match(line.Instruction, Pattern);

                var imm = match.Groups[1].Value;
                var rA = match.Groups[2].Value;

                output.Add(string.Format("stw {0}, {1}(zero)", rA, imm));

                return output;
            }
        }

        private sealed class StwRegOffsetMacro : IMacro
        {
            private const string Pattern = @"^M\[([0-9a-zA-Z+_]+\([0-9a-zA-Z]+\))\] = ([a-zA-Z0-9]+)$";

            public bool Match(SourceLine line)
            {
                var match = Regex.Match(line.Instruction, Pattern);

                var rA = match.Groups[2].Value;

                return match.Success && Registers.IsValid(rA);
            }

            public List<string> Handle(SourceLine line)
            {
                var output = new List<string>();
                var match = Regex.Match(line.Instruction, Pattern);

                var imm = match.Groups[1].Value;
                var rA = match.Groups[2].Value;

                output.Add(string.Format("stw {0}, {1}", rA, imm));

                return output;
            }
        }
    }
}
