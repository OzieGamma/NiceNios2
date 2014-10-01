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

namespace Nios2Compiler.Test.Macros
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Nios2Compiler.Test.Helpers;

    [TestClass]
    public class MemMacros
    {
        [TestMethod]
        public void StwImmMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.MemMacros.StwImm, "M[PADDLE] = 8");
        }

        [TestMethod]
        public void StwImmOffsetMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.MemMacros.StwImmOffset, "M[PADDLE(t2)] = 8");
        }

        [TestMethod]
        public void StwRegMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.MemMacros.StwReg, "M[PADDLE+4] = t2");
        }

        [TestMethod]
        public void StwRegOffsetMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.MemMacros.StwRegOffset, "M[PADDLE(a3)] = t4");
        }

        [TestMethod]
        public void LdwMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.MemMacros.Ldw, "t2 = M[PADDLE+4]");
        }

        [TestMethod]
        public void LdwOffsetMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.MemMacros.LdwOffset, "t2 = M[PADDLE(t5)]");
        }
    }
}
