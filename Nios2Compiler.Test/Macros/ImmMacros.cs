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

namespace Nios2Compiler.Test.Macros
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Nios2Compiler.Test.Helpers;

    [TestClass]
    public class ImmMacros
    {
        private readonly IMacro macro = Nios2Compiler.Macros.ImmMacros.Addi;

        [TestMethod]
        public void AddiMatchTest()
        {
            MacroHelper.ShouldMatch(this.macro, "t1 = t1 + 56");
        }

        [TestMethod]
        public void AddiNegMatchTest()
        {
            MacroHelper.ShouldMatch(this.macro, "t1 = t1 + -56");
        }

        [TestMethod]
        public void AddiHexMatchTest()
        {
            MacroHelper.ShouldMatch(this.macro, "t1 = t1 + 0XF6");
        }

        [TestMethod]
        public void AddiInvalidMatchTest()
        {
            MacroHelper.ShouldNotMatch(this.macro, "t1 = t2 + 56.68957");
        }

        [TestMethod]
        public void AddiRegInvalidMatchTest()
        {
            MacroHelper.ShouldNotMatch(this.macro, "t1 = t2 + t6");
        }
    }
}
