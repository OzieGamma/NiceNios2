using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nios2Compiler.Test.Helpers
{
    internal static class MacroHelper
    {
        public static void ShouldMatch(IMacro macro, string instr)
        {
            Assert.IsTrue(macro.Match(new SourceLine {Instruction = instr}));
            var matches = MacrosList.All.FindAll(m => m.Match(new SourceLine {Instruction = instr}));
            if (matches.Count != 1)
            {
                throw new AssertFailedException("Several Macros match: " + instr);
            }
        }

        public static void ShouldNotMatch(IMacro macro, string instr)
        {
            Assert.IsFalse(macro.Match(new SourceLine {Instruction = instr}));
        }
    }
}