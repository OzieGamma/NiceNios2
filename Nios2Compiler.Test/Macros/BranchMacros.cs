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

namespace Nios2Compiler.Test.Macros
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Nios2Compiler.Test.Helpers;

    [TestClass]
    public class BranchMacros
    {
        [TestMethod]
        public void BeqMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.BranchMacros.Beq, "if t0 == t3 goto alpha");
        }

        [TestMethod]
        public void BeqImmMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.BranchMacros.BeqImm, "if t0 == 0 goto alpha");
        }

        [TestMethod]
        public void BgeMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.BranchMacros.Bge, "if t0 >= t3 goto alpha");
        }

        [TestMethod]
        public void BgeImmMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.BranchMacros.BgeImm, "if t0 >= 0 goto alpha");
        }

        [TestMethod]
        public void BltMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.BranchMacros.Blt, "if t0 < t3 goto alpha");
        }

        [TestMethod]
        public void BltImmMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.BranchMacros.BltImm, "if t0 < 8 goto alpha");
        }

        [TestMethod]
        public void BneMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.BranchMacros.Bne, "if t0 != t3 goto alpha");
        }

        [TestMethod]
        public void BneImmMatchTest()
        {
            MacroHelper.ShouldMatch(Nios2Compiler.Macros.BranchMacros.BneImm, "if t0 != 0X67 goto alpha");
        }
    }
}
