// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Registers.cs" company="Oswald Maskens">
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
    using System.Collections.Generic;
    using System.Linq;

    internal static class Registers
    {
        static Registers()
        {
            var regs = new List<string>();

            // r0 - r31
            regs.AddRange(GenerateRegisters("r", 32));

            // zero
            regs.Add("zero");

            // at
            regs.Add("at");

            // v0 - v1
            regs.AddRange(GenerateRegisters("v", 2));

            // a0 - a3
            regs.AddRange(GenerateRegisters("a", 4));

            // t0 - t7
            regs.AddRange(GenerateRegisters("t", 8));

            // s0 - s7
            regs.AddRange(GenerateRegisters("s", 8));

            // et, bt, gp, sp, fp, ea, ba, ra
            regs.AddRange(new[] { "et", "bt", "gp", "sp", "fp", "ea", "ba", "ra" });

            RegisterNames = regs;
        }

        public static IReadOnlyList<string> RegisterNames { get; private set; }

        public static bool IsValid(string registerName)
        {
            return RegisterNames.Contains(registerName);
        }

        private static IEnumerable<string> GenerateRegisters(string prefix, int amount)
        {
            var regs = new List<string>();

            for (var i = 0; i < amount; i++)
            {
                regs.Add(prefix + i);
            }

            return regs;
        }
    }
}
